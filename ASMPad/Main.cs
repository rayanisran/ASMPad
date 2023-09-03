#region Using Directives
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WeifenLuo.WinFormsUI;
using WeifenLuo.WinFormsUI.Docking;
using ScintillaNet;
using System.Xml.Linq;
using ASMPad.Properties;
using System.Resources;
using System.Linq;
using Microsoft.Win32;
using System.Reflection;
#endregion Using Directives

namespace ASMPad
{
    public partial class Main : Form
    {
        #region Constants
        private const string DocTxt = "Untitled.asm (";
        private const int LineWidth = 40;
        #endregion Constants

        #region Fields
        public bool active = false;
        public int _newDocumentCount = 0;
        public List<string> _autocomp = new List<string>();
        public List<string> errors = new List<string>();
        public List<string> str = new List<string>();
        public MruStripMenu RFHMenu;
        public string _rompath; // ROM File path.
        static string regkey = "SOFTWARE\\ASMPad\\RecentFH";
        public string RFName;
        public bool _isrecording = false;
        public string _recorded = string.Empty;
        public bool _paused = false;
        public DocForm _x = null;
        #endregion Fields

        #region Properties
        public DocForm ActiveDocument
        {
            get
            {
                return dockPanel1.ActiveDocument as DocForm;
            }
        }
        public CommandPrompt LG
        {
            get
            {
                return Log as CommandPrompt;
            }
        }

        public Main _this
        {
            get
            {
                return this as Main;
            }
        }

        #endregion Properties

        #region Constructor
        public Main()
        {
            InitializeComponent();
            //Initialize the RFH component.
            RFHMenu = new MruStripMenuInline(recents, re, new MruStripMenu.ClickedHandler(refclick), regkey + "\\MRU", 16);
            RFHMenu.LoadFromRegistry();
            RFHMenu.MaxEntries = Decimal.ToInt32(Settings.Default.rfiles);
        }
        #endregion Constructor

        #region File Menu
        private void New_Click(object sender, EventArgs e) { NewDocument(); Log.cs.AppendText("\r\nCreated a new file.."); }
        private void Open_Click(object sender, EventArgs e) { OpenDocument(); }
        private void Reload_Click(object sender, EventArgs e) { ReloadDocument(); }
        private void Close_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveDocument.DockHandler.TabText == "Hex Editor" ||
                dockPanel1.ActiveDocument.DockHandler.TabText == "ROM Map" ||
                dockPanel1.ActiveDocument.DockHandler.TabText == "RAM Map")
                this.ActiveMdiChild.Close();
            else
            {
                if (ActiveDocument != null)
                    ActiveDocument.Close();
            }
        }

        private void quickOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                string dir = Application.StartupPath;
                string ID = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the file, for example \\sprites\\sprite.asm. The application directory is already added to the beginning of the path.", "Quick Load", "file.asm");
                if (ID.Length == 0)
                    return;
                if (!ID.StartsWith("\\"))
                    ID = "\\" + ID;
                if (!File.Exists(dir + ID))
                    Error("The file " + dir + ID + " doesn't exist.", "File not found!");
                else
                {
                    try
                    {
                        OpenFile(dir + ID);
                        Log.cs.AppendText("\r\nLoaded file " + dir + ID + ".");
                    }
                    catch (Exception) { MessageBox.Show("An error occured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void quickOpenToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Loads a file from the application's directory.";
        }

        private void toolStripMenuItem40_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\templates");
                Log.cs.AppendText("\r\nLoaded templates folder.");
            }
            catch (Exception)
            {
                Error("The templates folder doesn't exist.", "Error");
                Log.cs.AppendText("\r\nFailed to load templates folder!");
            }
        }

        private void toolStripMenuItem41_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Save();
            Close();
        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.ExportAsHtml();
        }

        private void loadTemplateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoadTemp LT = new LoadTemp();
            LT.ShowDialog(this);
            LT.ShowInTaskbar = false;
        }

        private void saveTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                string dir = Application.StartupPath + "\\templates\\";
                string ID = Microsoft.VisualBasic.Interaction.InputBox("Write down the name of the template. The template will be saved in the directory " + dir + ".", "Save Template", "Template");
                if (ID.Length == 0)
                    return;
                if (!Directory.Exists(dir))
                {
                    MessageBox.Show("The directory " + dir + " does not exist, creating it now.", "Creating Directory.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Directory.CreateDirectory(dir);
                }
                try
                {
                    if (!ID.EndsWith(".asm"))
                        ID = ID + ".asm";

                    if (File.Exists(dir + ID))
                    {
                        DialogResult dr = MessageBox.Show("The file " + dir + ID + " already exists, overwrite it?", "File already exists.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr != DialogResult.Yes)
                            return;
                    }
                    File.WriteAllText(dir + ID, ActiveDocument.Scintilla.Text);
                    Log.cs.AppendText("\r\nSaved template " + ID + ".");
                }
                catch (Exception) { MessageBox.Show("An error occured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        byte[] _rombytes;
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //When a ROM is loaded, store the path name in a variable.
            //Note that we enclose it with "" to access it from another path.
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ROM Files (*.smc)|*.smc|All Files (*.*)|*.*"; ;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _rombytes = File.ReadAllBytes(ofd.FileName);
                ActiveDocument.Scintilla.Text = _rombytes.Length.ToString();

                int remainder = _rombytes.Length & 0x7FFF;

                if (remainder != 512 || remainder == 0)
                {
                    Error("The file size of the ROM does not indicate a valid SNES ROM.", "Error");
                    LG.cs.AppendText("\r\nFailed to load ROM!");  
                }
                else
                {
                    _rompath = @"""" + ofd.FileName + @"""";
                    LoadedROM.Text = "Loaded ROM File: " + _rompath;
                    Log.cs.AppendText("\r\nLoaded ROM File " + _rompath + ".");
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Save();
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.SaveAs();
        }

        private void SaveAll_Click(object sender, EventArgs e)
        {
            foreach (DockContent _d in dockPanel1.Documents)
            {
                if (!_d.Text.Contains("ASMPad :: Hex Editor") && _d.Text != "ROM Map" && _d.Text != "RAM Map")
                {
                    ((DocForm)_d).Activate();
                    ((DocForm)_d).Save();
                }
            }
            Log.cs.AppendText("\r\nSaved all documents.");
        }
        #endregion

        #region Edit Menu

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Load a char buffer to process a triduo IP matrix from a HTTP DNS Construct..
            FontDialog FD = new FontDialog();
            FD.Font = Settings.Default.font;
            if (FD.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.font = FD.Font;
                Settings.Default.Save();
                UpdateFonts();
            }
        }

        private void UpdateFonts()
        {
            //Update fonts for all... 0 1 2 3 10 11
            ActiveDocument.Scintilla.Styles[0].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles[1].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles[2].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles[3].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles[4].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles[5].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles[6].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles[7].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles[8].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles[9].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles[10].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles[11].Font = Settings.Default.font;
            ActiveDocument.Scintilla.Styles.CallTip.Font = new Font("Consolas", 10);
            ActiveDocument.Scintilla.Styles.LineNumber.Font = Settings.Default.font;
        }

        private void makeMacroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                ActiveDocument.Scintilla.Selection.Text = "macro _macro()\r\n" + ActiveDocument.Scintilla.Selection.Text + "\r\nendmacro ";
                Log.cs.AppendText("\r\nconverted selection to macro.");
            }

        }
        private void indentCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Text = IndentCode(true, ActiveDocument.Scintilla.Selection.Text);
        }
        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                ActiveDocument.Scintilla.Selection.Text = JoinLines(ActiveDocument.Scintilla.Selection.Text);
            }
        }
        private void unindentCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Text = IndentCode(false, ActiveDocument.Scintilla.Selection.Text);
        }
        private void toolStripMenuItem39_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                ActiveDocument.Scintilla.Selection.Text = ActiveDocument.Scintilla.Selection.Text.ToLower();
                Log.cs.AppendText("\r\nConverted selection to lowercase.");
            }
        }
        private void toolStripMenuItem38_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                ActiveDocument.Scintilla.Selection.Text = ActiveDocument.Scintilla.Selection.Text.ToUpper();
                Log.cs.AppendText("\r\nConverted selection to uppercase.");
            }
        }

        private void toolStripMenuItem54_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.UndoRedo.Undo();
        }

        private void toolStripMenuItem55_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.UndoRedo.Redo();
        }

        private void toolStripMenuItem56_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Clipboard.Cut();
        }

        private void toolStripMenuItem58_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Clipboard.Copy();
        }

        private void toolStripMenuItem57_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Clipboard.Paste();
        }

        private void toolStripMenuItem59_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                ActiveDocument.Scintilla.Selection.Clear();
                Log.cs.AppendText("\r\nDeleted selected text.");
            }
        }

        private void toolStripMenuItem60_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Selection.SelectAll();
        }

        private void toolStripMenuItem42_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.FindReplace.ShowFind();
        }

        private void toolStripMenuItem43_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.FindReplace.ShowReplace();
        }

        private void toolStripMenuItem44_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.GoTo.ShowGoToDialog();
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                ActiveDocument.Scintilla.Selection.Text = ToggleComments(true, ActiveDocument.Scintilla.Selection.Text);
                SendKeys.Send("{BACKSPACE}");
            }
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                ActiveDocument.Scintilla.Selection.Text = ToggleComments(false, ActiveDocument.Scintilla.Selection.Text);
                SendKeys.Send("{BACKSPACE}");
            }
        }

        private void toolStripMenuItem53_Click(object sender, EventArgs e)
        {
            ActiveDocument.Scintilla.Text = TrimComm(ActiveDocument.Scintilla.Text);
        }

        public string TrimComm(string input)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                StringReader str = new StringReader(input);
                do
                {
                    string ln = str.ReadLine();

                    if (ln.StartsWith(";"))
                        ln = ln.Substring(0, ln.IndexOf(";"));

                    else if (ln.Contains(";"))
                    {
                        ln = ln.Substring(0, ln.IndexOf(";"));
                        sb.AppendLine(ln);
                    }
                    else
                        sb.AppendLine(ln);
                }
                while (str.Peek() != -1);
                str.Close();
                input = sb.ToString();
            }
            catch (Exception) { }
            return input;
        }

        public string ToggleComments(bool type, string input)
        {
            try
            {
                StringBuilder pc = new StringBuilder();
                StringReader str = new StringReader(ActiveDocument.Scintilla.Selection.Text);
                do
                {
                    string current = str.ReadLine();

                    if (type != true)
                        if (current.StartsWith(";"))
                            pc.AppendLine(current.Substring(current.IndexOf(";") + 1));
                        else
                            pc.AppendLine(current);
                    else
                        if (!current.StartsWith(";"))
                            pc.AppendLine(";" + current);
                        else
                            pc.AppendLine(current);
                }
                while (str.Peek() != -1);
                str.Close();

                input = pc.ToString();
                Log.cs.AppendText("\r\nAdded/removed comments to file " + ActiveDocument.Text + ".");
                return input;
            }
            catch (Exception) { return null; }
        }
        #endregion

        #region Format Menu

        private void _Wrap_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                if (_Wrap.Checked)
                    ActiveDocument.Scintilla.LineWrap.Mode = ScintillaNet.WrapMode.Word;
                else
                    ActiveDocument.Scintilla.LineWrap.Mode = ScintillaNet.WrapMode.None;
        }
        #endregion

        #region Help Menu
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutTool AT = new AboutTool();
            AT.ShowDialog(this);
        }
        private void sMWCForumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.smwcentral.net/?p=viewforum");
                //Log.cs.AppendText("\r\nLoaded SMWC Forums..");
            }
            catch (Exception)
            {
                Error("Failed to load. Make sure your internet connection is working.", "Error");
            }
        }
        #endregion

        #region Toolstrip Misc.

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            //Rather than showing a dialog for choosing a ROM, the one loaded
            //will be used.
            if (_rompath == string.Empty)
                Error("A ROM hasn't been loaded.", "Error");
            string p = _rompath.Substring(1, _rompath.LastIndexOf(@"""") - 1);
            if (!File.Exists(p) || (!File.Exists(Application.StartupPath + "\\slogger.exe")))
                Error("Logging failed because the ROM was not loaded, slogger was not found or the tool is not in the ROM's folder.", "Error");
            else
            {
                string X = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(Application.StartupPath);

                string _out = string.Empty;
                ProcessStartInfo runsp = new ProcessStartInfo("slogger.exe");
                runsp.Arguments = Path.GetFileName(p);
                runsp.CreateNoWindow = true;
                runsp.UseShellExecute = false;
                runsp.RedirectStandardOutput = true;
                using (Process process = Process.Start(runsp))
                {
                    using (StreamReader reader = process.StandardOutput)
                        _out = reader.ReadToEnd();
                }

                //Get the name of the ROM without the extension, we'll need this for finding the text file for it.
                string ext = Path.GetFileName(p);
                ext = ext.Remove(ext.Length - 4);

                Log.cs.AppendText("\r\n" + File.ReadAllText(Application.StartupPath + "\\" + ext + ".txt"));
                Log.Tabs.SelectTab(Log.Tabs.TabPages[0]);
                Log.Show(dockPanel1, DockState.DockBottom);
                Log.DockPanel.DockBottomPortion = 205;

                Log.cs.AppendText("\r\nLogged freespace in ROM " + _rompath + ".");

                //To prevent a clutter of logged textfiles, we'll simply delete them after the file has been read.
                try { File.Delete(Application.StartupPath + "\\" + ext + ".txt"); }
                catch (Exception) { }
                Directory.SetCurrentDirectory(X);
            }

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveDocument != null)
                {
                    if (_rompath == string.Empty || _rompath == null)
                    {
                        Error("Failed to patch ASM file to ROM because a ROM file was not loaded.", "Error");
                        openROMToolStripMenuItem.PerformClick();
                        return;
                    }

                    ActiveDocument.Scintilla.Text = TranslateDef(ActiveDocument.Scintilla.Text);
                    File.WriteAllText(Application.StartupPath + "\\tempasm.asm", ActiveDocument.Scintilla.Text);

                    string path = Directory.GetCurrentDirectory();
                    Directory.SetCurrentDirectory(Application.StartupPath);

                    string rompath = Path.GetDirectoryName(_rompath.TrimEnd('"').TrimStart('"')) + "\\"; // D:\\
                    string romtitle = Path.GetFileName(_rompath.TrimEnd('"').TrimStart('"'));            // style.smc
                    File.Copy(_rompath.TrimEnd('"').TrimStart('"'), rompath + "Backup of " + romtitle, true);                        // D:\\Backup of style.smc

                    ProcessStartInfo runsp = new ProcessStartInfo("xkas.exe");
                    runsp.Arguments = " tempasm.asm " + romtitle;
                    runsp.CreateNoWindow = true;
                    runsp.UseShellExecute = false;
                    runsp.RedirectStandardOutput = true;

                    using (Process process = Process.Start(runsp))
                    {
                        using (StreamReader reader = process.StandardOutput)
                            output = reader.ReadToEnd();
                    }
                    if (output.Contains("error"))
                    {
                        ShowErrors();
                        LG.cs.AppendText("\r\nThe file was patched to " + _rompath + ", but errors were reported when patching the file. A backup has been made in the application's folder.");
                        return;
                    }

                    LG.cs.AppendText("\r\nPatched " + ActiveDocument.Text.TrimEnd('*') + " to ROM " + _rompath + ".");
                    if (Settings.Default._runROM)
                        runEmulatorToolStripMenuItem.PerformClick();

                    Directory.SetCurrentDirectory(path);
                }
            }
            catch (Exception ex) { Error("Exception detected. Contents: " + ex.ToString(), "Error"); };
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            //Insert sprites.
            if (_rompath == string.Empty || !File.Exists(Application.StartupPath + "\\sprites.txt") || (!File.Exists(Application.StartupPath + "\\sprite_tool.exe")))
                Error("Failed to insert sprites because either the ROM was not loaded, the file sprites.txt was not found or sprite tool does not exist in the directory.", "Error");
            else
            {
                //The user might expect us to fix the definitions and add the main sprite routines
                //in the current file if he hasn't already added them.
                if (ActiveDocument.FilePath != null)
                {
                    ActiveDocument.Scintilla.Text = TranslateDef(ActiveDocument.Scintilla.Text);
                    File.WriteAllText(ActiveDocument.FilePath, ActiveDocument.Scintilla.Text);
                }

                string ar = Directory.GetCurrentDirectory();
                string _out = string.Empty;
                Directory.SetCurrentDirectory(Application.StartupPath);

                int p = _rompath.LastIndexOf(@"""");
                string n = _rompath.Substring(1, p - 1);

                ProcessStartInfo runsp = new ProcessStartInfo("sprite_tool.exe");
                runsp.Arguments = Path.GetFileName(n) + " sprites.txt";
                runsp.CreateNoWindow = true;
                runsp.UseShellExecute = false;
                runsp.RedirectStandardOutput = true;

                using (Process process = Process.Start(runsp))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        _out = reader.ReadToEnd();
                    }
                }
                if (_out.Contains("Sprites inserted successfully"))
                {
                    MessageBox.Show("All of the following sprites were successfully inserted into ROM " + _rompath + ":\n\n" + File.ReadAllText("sprites.txt"), "No Errors reported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (Settings.Default._runROMspr)
                        runEmulatorToolStripMenuItem.PerformClick();

                    Log.cs.AppendText("\r\nSuccesfully inserted all sprites in ROM " + _rompath + ".");
                }
                else
                {
                    if (_out.Contains("Refer to temp.log and tmpasm.asm"))
                    {
                        DialogResult dr = MessageBox.Show("Errors were reported during the assembling of a file.\r\n\r\nLoad temp.log to see the errors?", "Errors", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                Process.Start("notepad.exe", Application.StartupPath + "\\temp.log");
                            }
                            catch (Exception) { Error("Failed to load tmpasm.asm!", "Error"); }
                        }
                    }
                    Log.cs.AppendText("\r\n" + _out);
                    Log.Tabs.SelectTab(Log.Tabs.TabPages[0]);
                    Log.Show(dockPanel1, DockState.DockBottom);
                    Log.DockPanel.DockBottomPortion = 205;

                    //Log.cs.AppendText("\r\nErrors reporting during the assembling of sprites in ROM " + _rompath + ".");
                }
                Directory.SetCurrentDirectory(ar);
            }
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            //Show the patch dialog box.
            PatchDialog PD = new PatchDialog();
            PD.ShowDialog(this);
            PD.ShowInTaskbar = false;
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            //Insert a block.
            //I could have used a string builder, but the difference is almost negligible.
            DocForm Doc = new DocForm();
            SetProperties(Doc);
            Doc.Scintilla.Modified = false;
            Doc.Text = String.Format(CultureInfo.CurrentCulture, "{0}{1}", DocTxt, ++_newDocumentCount + ")");
            Doc.Show(dockPanel1, DockState.Document);
            incrementalsearcher.Searcher.Scintilla = Doc.Scintilla;
            string x = "db $42\n\n";
            x += "JMP MarioBelow : JMP MarioAbove : JMP MarioSide : JMP SpriteH : JMP SpriteV : JMP Fireball : JMP Cape : JMP MarioCorner : JMP MarioHead : JMP MarioBody";
            x += "\n\n";
            x += "MarioBelow:\n";
            x += "MarioAbove:\n";
            x += "MarioSide:\n\n\tRTL\n";
            x += "SpriteH:\n";
            x += "SpriteV:\n";
            x += "Fireball:\n";
            x += "Cape:\n";
            x += "MarioCorner:\nMarioHead:\nMarioBody:\n";
            x += "\tRTL";
            Doc.Scintilla.Text = x;
            Doc.Scintilla.GoTo.Line(7);
            Doc.Scintilla.InsertText("\t");
            Log.cs.AppendText("\r\nCreated a new block file..");
        }

        private void spriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Sprite type: Normal sprite.
            DocForm Doc = new DocForm();
            Doc.Scintilla.Modified = false;
            SetProperties(Doc);
            Doc.Text = String.Format(CultureInfo.CurrentCulture, "{0}{1}", DocTxt, ++_newDocumentCount + ")");
            Doc.Show(dockPanel1, DockState.Document);
            incrementalsearcher.Searcher.Scintilla = Doc.Scintilla;
            string x = ";==========\n;New Sprite\n;==========";
            x += "\n\n";
            x += @"print ""INIT"", pc";
            x += "\n\tJSR SUB_HORZ_POS\t;Face Mario, uncomment this line and the next two if you don't want that.\n\tTYA\n\tSTA $157C,x\n\tRTL\n\nReturn:\n\tRTL\n\n";
            x += @"print ""MAIN"", pc";
            x += "\n\tPHB\n\tPHK\n\tPLB\n\tJSR Run\n\tPLB\n\tRTL\n\n";
            x += "Run:\n\n\tRTL\n\n";//=========================================================\n";
            x += ";Sprite Routines\n";
            #region Routines
            x += @"
;=================;
;SUB_HORZ_POS
;=================;

SUB_HORZ_POS:
	LDY #$00
	LDA $D1
	SEC
	SBC $E4,x
	STA $0F
	LDA $D2
	SBC $14E0,x
	BPL SPR_L16
	INY
SPR_L16:
	RTS

;=================;
;SUB_VERT_POS
;=================;

SUB_VERT_POS
	LDY #$00
	LDA $D3
	SEC
	SBC $D8,x
	STA $0F
	LDA $D4
	SBC $14D4,x
	BPL SPR_L11
	INY
SPR_L11:
	RTS

;=================;
;SUB_OFF_SCREEN
;=================;

SPR_T12:
	db $40,$B0
SPR_T13:
	db $01,$FF
SPR_T14:
	db $30,$C0,$A0,$C0,$A0,$F0,$60,$90
	db $30,$C0,$A0,$80,$A0,$40,$60,$B0
SPR_T15:
	db $01,$FF,$01,$FF,$01,$FF,$01,$FF
	db $01,$FF,$01,$FF,$01,$00,$01,$FF

SUB_OFF_SCREEN_X1:
	LDA #$02
	BRA STORE_03
SUB_OFF_SCREEN_X2:
	LDA #$04
	BRA STORE_03
SUB_OFF_SCREEN_X3:
	LDA #$06
	BRA STORE_03
SUB_OFF_SCREEN_X4:
	LDA #$08
	BRA STORE_03
SUB_OFF_SCREEN_X5:
	LDA #$0A
	BRA STORE_03
SUB_OFF_SCREEN_X6:
	LDA #$0C
	BRA STORE_03
SUB_OFF_SCREEN_X7:
	LDA #$0E
STORE_03:
	STA $03
	BRA START_SUB
SUB_OFF_SCREEN_X0:
	STZ $03

START_SUB:
	JSR SUB_IS_OFF_SCREEN
	BEQ RETURN_35
	LDA $5B
	AND #$01
	BNE VERTICAL_LEVEL
	LDA $D8,x
	CLC
	ADC #$50
	LDA $14D4,x
	ADC #$00
	CMP #$02
	BPL ERASE_SPRITE
	LDA $167A,x
	AND #$04
	BNE RETURN_35
	LDA $13
	AND #$01
	ORA $03
	STA $01
	TAY
	LDA $1A
	CLC
	ADC SPR_T14,y
	ROL $00
	CMP $E4,x
	PHP
	LDA $1B
	LSR $00
	ADC SPR_T15,y
	PLP
	SBC $14E0,x
	STA $00
	LSR $01
	BCC SPR_L31
	EOR #$80
	STA $00
SPR_L31:
	LDA $00
	BPL RETURN_35
	ERASE_SPRITE:
	LDA $14C8,x
	CMP #$08
	BCC KILL_SPRITE
	LDY $161A,x
	CPY #$FF
	BEQ KILL_SPRITE
	LDA #$00
	STA $1938,y
KILL_SPRITE:
	STZ $14C8,x
RETURN_35:
	RTS

VERTICAL_LEVEL:
	LDA $167A,x
	AND #$04
	BNE RETURN_35
	LDA $13
	LSR A
	BCS RETURN_35
	LDA $E4,x
	CMP #$00
	LDA $14E0,x
	SBC #$00
	CMP #$02
	BCS ERASE_SPRITE
	LDA $13
	LSR A
	AND #$01
	STA $01
	TAY
	LDA $1C
	CLC
	ADC SPR_T12,y
	ROL $00
	CMP $D8,x
	PHP
	LDA $001D
	LSR $00
	ADC SPR_T13,y
	PLP
	SBC $14D4,x
	STA $00
	LDY $01
	BEQ SPR_L38
	EOR #$80
	STA $00
SPR_L38:
	LDA $00
	BPL RETURN_35
	BMI ERASE_SPRITE

SUB_IS_OFF_SCREEN:
	LDA $15A0,x
	ORA $186C,x
	RTS

;================;
;GET_DRAW_INFO
;================;

SPR_T1:
	db $0C,$1C
SPR_T2:
	db $01,$02

GET_DRAW_INFO:
	STZ $186C,x
	STZ $15A0,x
	LDA $E4,x
	CMP $1A
	LDA $14E0,x
	SBC $1B
	BEQ ON_SCREEN_X
	INC $15A0,x

ON_SCREEN_X:
	LDA $14E0,x
	XBA
	LDA $E4,x
	REP #$20
	SEC
	SBC $1A
	CLC
	ADC.w #$0040
	CMP.w #$0180
	SEP #$20
	ROL A
	AND #$01
	STA $15C4,x
	BNE INVALID

	LDY #$00
	LDA $1662,x
	AND #$20
	BEQ ON_SCREEN_LOOP
	INY
ON_SCREEN_LOOP:
	LDA $D8,x
	CLC
	ADC SPR_T1,y
	PHP
	CMP $1C
	ROL $00
	PLP
	LDA $14D4,x
	ADC #$00
	LSR $00
	SBC $1D
	BEQ ON_SCREEN_Y
	LDA $186C,x
	ORA SPR_T2,y
	STA $186C,x
ON_SCREEN_Y:
	DEY
	BPL ON_SCREEN_LOOP
	LDY $15EA,x
	LDA $E4,x
	SEC
	SBC $1A
	STA $00
	LDA $D8,x
	SEC
	SBC $1C
	STA $01
	RTS

INVALID:
	PLA
	PLA
	RTS";
            #endregion
            //x += ";======================\n";
            Log.cs.AppendText("\r\nCreated a new sprite file..");
            Doc.Scintilla.Text = x;
            Doc.Scintilla.GoTo.Line(22);
            Doc.Scintilla.InsertText("\t");
            Doc.Scintilla.InsertText("LDA $14C8,x\t\t;If the sprite is dead..\n\tCMP #$08\n\tBEQ Return\t\t;..return\n\tLDA $9D\n\tBNE Return\t\t;Also return if sprites are locked.\n\tJSR SUB_OFF_SCREEN\n\t");
        }

        private void generatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Sprite type: Generator or shooter sprite.
            //There's no difference between the two in terms of what's added to the document.
            DocForm Doc = new DocForm();
            Doc.Scintilla.Modified = false;
            SetProperties(Doc);
            Doc.Text = String.Format(CultureInfo.CurrentCulture, "{0}{1}", DocTxt, ++_newDocumentCount + ")");
            Doc.Show(dockPanel1, DockState.Document);
            incrementalsearcher.Searcher.Scintilla = Doc.Scintilla;
            SetProperties(Doc);
            string x = ";==========\n;New Sprite\n;==========";
            x += "\n\n";
            x += @"print ""INIT"", pc";
            x += "\n\tRTL\n\n";
            x += @"print ""MAIN"", pc";
            x += "\n\n\tRTL";

            Doc.Scintilla.Text = x;
            Doc.Scintilla.GoTo.Line(8);
            Doc.Scintilla.InsertText("\t");
            Log.cs.AppendText("\r\nCreated a new sprite file..");
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            PatchIPS();
        }

        string temp;
        private void _goto_MouseEnter(object sender, EventArgs e)
        {
            //When the user hovers into the combo box area, we populate the combo-box
            //With all the labels.
            if (ActiveDocument != null)
                temp = TrimComm(ActiveDocument.Scintilla.Text);

            info.Text = "Go to a label in the active document.";
        }
        private void _goto_DropDown(object sender, EventArgs e)
        {
            //First clear the previous items, and then add all labels using a regex.
            //We match all words exclduing comments that end with a colon.
            if (ActiveDocument != null)
            {
                _goto4.Items.Clear();
                string matches = string.Empty;
                Regex r = new Regex(@"\b\w+[:\b]");

                if (r != null)
                {
                    MatchCollection m = r.Matches(temp);
                    for (int i = 0; i < m.Count; i++)
                        _goto4.Items.Add(m[i]);
                }
            }
        }
        private void _goto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dockPanel1.ActiveDocument.DockHandler.Activate();
                GetLineFromString(_goto4.SelectedItem.ToString());
                Log.cs.AppendText("\r\nJumped to label " + _goto4.SelectedItem.ToString() + ".");
            }
            catch (Exception) { LG.cs.AppendText("\r\nFailed to jump to label."); };
        }
        public void GetLineFromString(string input)
        {
            foreach (Line ln in ActiveDocument.Scintilla.Lines)
            {
                if (ln.Text.Contains(input))
                    ActiveDocument.Scintilla.GoTo.Line(ln.Number);
            }
        }

        static bool ExactMatch(string input, string match)
        {
            return Regex.IsMatch(input, string.Format(@"\b{0}\b", Regex.Escape(match)));
        }

        #endregion

        #region Insert Menu

        private void codeSnippetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSnippet();
        }

        private void toolStripMenuItem35_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                ActiveDocument.Scintilla.InsertText(";===============================================\n");
                ActiveDocument.Scintilla.InsertText(";\n");
                ActiveDocument.Scintilla.InsertText(";\n");
                ActiveDocument.Scintilla.InsertText(";\n");
                ActiveDocument.Scintilla.InsertText(";===============================================\n");
                ActiveDocument.Scintilla.GoTo.Line(ActiveDocument.Scintilla.Lines.Current.Number - 4);
                ActiveDocument.Scintilla.GoTo.Position(ActiveDocument.Scintilla.CurrentPos + 1);
                Log.cs.AppendText("\r\nInserted block comment to file.");
            }
        }

        private void recordMacroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _recorded = "";
            _isrecording = true;
            Log.cs.AppendText("\r\nRecording macro..");
        }
        private void stopMacroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isrecording = false;
            Log.cs.AppendText("\r\nStopped recording macro..");
        }
        private void playbackMacroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                if (_recorded != string.Empty)
                {
                    ActiveDocument.Scintilla.InsertText(_recorded);
                    Log.cs.AppendText("\r\nInserted macro.");
                }
            }
        }
        private void toolStripMenuItem46_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null && ActiveDocument.Text.Contains(".asm"))
            {
                StringReader s = new StringReader(Settings.Default.listdef);
                StringBuilder X = new StringBuilder();
                do
                {
                    string p = s.ReadLine();

                    if (!p.StartsWith("!"))
                        p = "!" + p;

                    X.AppendLine(p);
                }
                while (s.Peek() != -1);
                s.Close();
                ActiveDocument.Scintilla.InsertText(X.ToString());
                Log.cs.AppendText("\r\nInserted definition file.");

                ActiveDocument.Activate();
            }
        }

        public void LoadSnippet()
        {
            if (ActiveDocument != null)
            {
                //First clear the snippet list to prevent the list from being added multiple times.
                ActiveDocument.Scintilla.Snippets.List.Clear();
                try
                {
                    //Load the XML document, and fetch the snippet stuff from there.
                    XDocument doc = XDocument.Load(Application.StartupPath + "\\Snippets.xml");
                    foreach (XElement xe in doc.Elements("Snippets").Elements("Snippet"))
                    {
                        string snippetName = (string)xe.Attribute("name");
                        string arg = snippetName.Replace(" ", "");
                        string snippetCode = xe.Element("SnippetCode").Value;
                        ActiveDocument.Scintilla.Snippets.List.Add(arg, snippetCode, '<');
                    }
                    ActiveDocument.Scintilla.Snippets.List.Sort();
                    ActiveDocument.Scintilla.Snippets.ShowSnippetList();
                }
                catch (Exception ex) // Normally an exception occurs if the file doesn't exist, or there's a bad format.
                {
                    Error("An error occured. Please check if the XML file is set up correctly. Error:\n\n" + ex.ToString().Substring(0, ex.ToString().IndexOf("at")), "Error");
                    Log.cs.AppendText("\r\nFailed to load Snippets.xml file. Check if it exists and is correctly formatted.");
                }
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Title = "Open ASM File";
                open.Filter = "ASM Files (*.asm)|*.asm|All Files (*.*)|*.*";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    ActiveDocument.Scintilla.InsertText(File.ReadAllText(open.FileName));
                    Log.cs.AppendText("\r\nInserted contents of file " + open.FileName + " to " + ActiveDocument.Text + ".");
                }
            }
        }


        private void labelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                int cl = ActiveDocument.Scintilla.Lines.Current.Number;
                string lbl = "Label:\r\n\r\n\tRTL\t;Replace with RTS if necessary.";
                ActiveDocument.Scintilla.InsertText(lbl);
                ActiveDocument.Scintilla.GoTo.Line(cl + 1);
                ActiveDocument.Scintilla.InsertText("\t");
            }
        }

        private void macroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                ActiveDocument.Scintilla.InsertText("macro _m(arg1, arg2)\n\nendmacro ");
                ActiveDocument.Scintilla.GoTo.Line(ActiveDocument.Scintilla.Lines.Current.Previous.Number);
                ActiveDocument.Scintilla.InsertText("\t");
                Log.cs.AppendText("\r\nInserted a macro sample..");
            }
        }

        private void rATSTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                int cl = ActiveDocument.Scintilla.Lines.Current.Number;
                string rats =
@";===========================;
!CodeSize = Ending-Routine
db ""STAR""
dw !CodeSize-$01            
dw !CodeSize-$01^$FFFF      
Routine:
;===========================;";
                ActiveDocument.Scintilla.InsertText(rats);
                ActiveDocument.Scintilla.Text += "\r\nEnding:";
                ActiveDocument.Scintilla.GoTo.Line(cl + 5);
                ActiveDocument.Scintilla.GoTo.Position(ActiveDocument.Scintilla.NativeInterface.GetLineEndPosition(cl + 5));
                ActiveDocument.Scintilla.InsertText("\n");
                Log.cs.AppendText("\r\nInserted a RATS Tag.");
            }
        }

        private void pointerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                int cl = ActiveDocument.Scintilla.Lines.Current.Number;
                string lbl = "\tLDA Addr,x\r\n\tASL A\r\n\tTAX\r\n\tJMP (Ptr,x)\r\nPtr:\r\n\tdw OptionA\r\n\tdw OptionB\r\n\r\nOptionA:\r\n\t";
                ActiveDocument.Scintilla.InsertText(lbl);
                Log.cs.AppendText("\r\nInserted a pointer sample..");
            }
        }

        private void tableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                int cl = ActiveDocument.Scintilla.Lines.Current.Number;
                string tbl = "\r\nTable:\r\n\t db $xx,$xx";
                ActiveDocument.Scintilla.InsertText(tbl);
                ActiveDocument.Scintilla.GoTo.Line(cl + 2);
                ActiveDocument.Scintilla.GoTo.Position(ActiveDocument.Scintilla.CurrentPos + 6);
                ActiveDocument.Scintilla.OverType = true; // Let the user overwrite the bytes..
                Log.cs.AppendText("\r\nInserted a sample table. Press Insert to toggle overtype mode.");
            }
        }

        private void toggleBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                Line currentLine = ActiveDocument.Scintilla.Lines.Current;
                if (ActiveDocument.Scintilla.Markers.GetMarkerMask(currentLine) == 0)
                    currentLine.AddMarker(0);
                else
                    currentLine.DeleteMarker(0);

                Log.cs.AppendText("\r\nToggled bookmark on line " + ActiveDocument.Scintilla.Lines.Current.Number + ".");
            }
        }

        private void previousBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                Line l = ActiveDocument.Scintilla.Lines.Current.FindPreviousMarker(1);
                if (l != null)
                {
                    l.Goto();
                    Log.cs.AppendText("\r\nJumped to previous marker.");
                }
            }
        }

        private void nextBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                Line l = ActiveDocument.Scintilla.Lines.Current.FindNextMarker(1);
                if (l != null)
                {
                    l.Goto();
                    Log.cs.AppendText("\r\nJumped to next marker.");
                }
            }
        }

        private void deleteAllBookmarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                ActiveDocument.Scintilla.Markers.DeleteAll(0);
                Log.cs.AppendText("\r\nRemoved all bookmarks from active file.");
            }
        }

        #endregion

        #region Methods

        public void Updater()
        {
            ActiveDocument.Scintilla.Focus();
            ActiveDocument.Scintilla.Lexing.Keywords[0] = Settings.Default._opcodes;    //Opcode keywords.
            ActiveDocument.Scintilla.Lexing.Keywords[2] = Settings.Default._misc;       //Misc. keywords.      
            ActiveDocument.Scintilla.Lexing.Keywords[1] = Settings.Default.okkey;

            ActiveDocument.Scintilla.Styles[0].ForeColor = Settings.Default.addclr;
            ActiveDocument.Scintilla.Styles[1].ForeColor = Settings.Default.commentclr;
            ActiveDocument.Scintilla.Styles[2].ForeColor = Settings.Default.addclr;
            ActiveDocument.Scintilla.Styles[3].ForeColor = Settings.Default.opcodeclr;
            ActiveDocument.Scintilla.Styles[11].ForeColor = Settings.Default.keywordclr;
            ActiveDocument.Scintilla.Styles[10].ForeColor = Settings.Default.ok;

            ActiveDocument.Scintilla.Styles[3].Bold = Settings.Default.boldop;
            ActiveDocument.Scintilla.Styles[0].Bold = Settings.Default.boldaddr;
            ActiveDocument.Scintilla.Styles[1].Bold = Settings.Default.boldcom;
            ActiveDocument.Scintilla.Styles[1].Italic = Settings.Default.CommentItal;
            ActiveDocument.Scintilla.Styles[2].Bold = Settings.Default.boldaddr;
            ActiveDocument.Scintilla.Styles[11].Bold = Settings.Default.boldkey;
            ActiveDocument.Scintilla.Styles[10].Bold = Settings.Default.boldok;

            // ActiveDocument.Scintilla.
            ActiveDocument.Scintilla.Lexing.Colorize();
            if (!Settings.Default.hl)
            {
                ActiveDocument.Scintilla.Lexing.Keywords[3] = string.Empty;
                ActiveDocument.Scintilla.Styles[12].Reset();
                ActiveDocument.Scintilla.Styles[6].Reset();
            }
            else
            {
                ActiveDocument.Scintilla.Styles[12].ForeColor = Settings.Default.labclr;
                ActiveDocument.Scintilla.Styles[12].Bold = Settings.Default.boldlab;
                ActiveDocument.Scintilla.Styles[6].Bold = Settings.Default.boldlab;
            }
            ActiveDocument.Scintilla.Lexing.Colorize();
        }

        public string JoinLines(string input)
        {
            try
            {
                string s = string.Empty;
                string p = input.Substring(input.Length - 1);
                StringReader str = new StringReader(input);
                do
                {
                    string current = str.ReadLine();
                    s = s + " : " + current.Trim();
                }
                while (str.Peek() != -1);
                str.Close();

                input = s.Substring(3) + p;
                SendKeys.Send("{BACKSPACE}");
                return input;
            }
            catch (Exception) { return null; }
        }

        public string IndentCode(bool type, string input)
        {
            try
            {
                //Indent if true.
                StringBuilder pc = new StringBuilder();
                StringReader str = new StringReader(input);
                do
                {
                    string current = str.ReadLine();
                    if (type)
                        pc.AppendLine("\t" + current.Trim());
                    else
                        pc.AppendLine(current.Trim());
                }
                while (str.Peek() != -1);
                str.Close();
                input = pc.ToString();
                SendKeys.Send("{BACKSPACE}");
                Log.cs.AppendText("\r\nIndented/unindented code.");
                return input;
            }
            catch (Exception) { return null; }
        }

        private void PatchIPS()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select unmodified ROM to patch to.";
            ofd.Filter = "ROM Files (*.smc)|*.smc|All Files (*.*|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            //File doesn't exist, return.
            if (!File.Exists(ofd.FileName))
                MessageBox.Show("File doesn't exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            string romname = ofd.FileName;
            string romtitle = Path.GetFileName(ofd.FileName);

            ofd.Title = "Select IPS File to patch ROM to.";
            ofd.Filter = "IPS Files (*.ips)|*.ips|All Files (*.*|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            if (!File.Exists(ofd.FileName))
                MessageBox.Show("File doesn't exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            string patchname = ofd.FileName;

            //Backup the .SMC file before the actual patching.
            if (Settings.Default._backupIPS == true)
            {
                string rompath = Path.GetDirectoryName(romname) + "\\";
                File.Copy(romname, rompath + "Backup of " + romtitle, true);
            }

            //Ugh, this routine is pretty big. First we have to load both the IPS and ROM file
            //and now we have to do the actual patching. Thanks to NN for the code.
            #region Patch IPS File
            FileStream romstream = new FileStream(romname, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            FileStream ipsstream = new FileStream(patchname, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            int lint = (int)ipsstream.Length;
            byte[] ipsbyte = new byte[ipsstream.Length];
            byte[] rombyte = new byte[romstream.Length];
            IAsyncResult romresult;
            IAsyncResult ipsresult = ipsstream.BeginRead(ipsbyte, 0, lint, null, null);
            ipsstream.EndRead(ipsresult);
            int ipson = 5;
            int totalrepeats = 0;
            int offset = 0;
            bool keepgoing = true;
            while (keepgoing == true)
            {
                offset = ipsbyte[ipson] * 0x10000 + ipsbyte[ipson + 1] * 0x100 + ipsbyte[ipson + 2];
                ipson++;
                ipson++;
                ipson++;
                if (ipsbyte[ipson] * 256 + ipsbyte[ipson + 1] == 0)
                {
                    ipson++;
                    ipson++;
                    totalrepeats = ipsbyte[ipson] * 256 + ipsbyte[ipson + 1];
                    ipson++;
                    ipson++;
                    byte[] repeatbyte = new byte[totalrepeats];
                    for (int ontime = 0; ontime < totalrepeats; ontime++)
                        repeatbyte[ontime] = ipsbyte[ipson];
                    romstream.Seek(offset, SeekOrigin.Begin);
                    romresult = romstream.BeginWrite(repeatbyte, 0, totalrepeats, null, null);
                    romstream.EndWrite(romresult);
                    ipson++;
                }
                else
                {
                    totalrepeats = ipsbyte[ipson] * 256 + ipsbyte[ipson + 1];
                    ipson++;
                    ipson++;
                    romstream.Seek(offset, SeekOrigin.Begin);
                    romresult = romstream.BeginWrite(ipsbyte, ipson, totalrepeats, null, null);
                    romstream.EndWrite(romresult);
                    ipson = ipson + totalrepeats;
                }
                if (ipsbyte[ipson] == 69 && ipsbyte[ipson + 1] == 79 && ipsbyte[ipson + 2] == 70)
                    keepgoing = false;
            }
            romstream.Close();
            ipsstream.Close();
            Log.cs.AppendText("\r\nPatched IPS File (" + romname + ") to (" + patchname + ").");
            #endregion
        }
        public static string Assemble(string assembler, string file)
        {
            string result = string.Empty;
            if (File.Exists(assembler) && File.Exists(file))
            {
                ProcessStartInfo P = new ProcessStartInfo(assembler);   // Assembler.
                P.Arguments = file;                                     // File to assemble.
                P.CreateNoWindow = true;
                P.UseShellExecute = false;
                P.RedirectStandardOutput = true;
                Process.Start(P);
                using (Process process = Process.Start(P))
                {
                    using (StreamReader str = process.StandardOutput)
                    {
                        result = str.ReadToEnd();
                    }
                }
            }
            return result;
        }

        public string TranslateDef(string txt)
        {
            //First we'll replace GetDrawInfo and SubOffScreen
            //I like this because I hate adding GetDrawInfo
            //and SubOffScreen manually.
            if (txt.Contains(".include(GetDrawInfo)"))
            {
                #region GetDrawInfo
                string x = @"DATA_03B75C:
db $0C,$1C
DATA_03B75E:
db $01,$02

GetDrawInfo:
STZ $186C,X
STZ $15A0,X
LDA $E4,X
CMP $1A
LDA $14E0,X
SBC $1B
BEQ ADDR_03B774
INC $15A0,X
ADDR_03B774:
LDA $14E0,X
XBA
LDA $E4,X
REP #$20
SEC
SBC $1A
CLC
ADC #$0040
CMP #$0180
SEP #$20
ROL
AND #$01
STA $15C4,X
BNE ADDR_03B7CF
LDY #$00
LDA $1662,X
AND #$20
BEQ ADDR_03B79A
INY
ADDR_03B79A:
LDA $D8,X
CLC
ADC DATA_03B75C,Y
PHP
CMP $1C
ROL $00
PLP
LDA $14D4,X
ADC #$00
LSR $00
SBC $1D
BEQ ADDR_03B7BA
LDA $186C,X
ORA DATA_03B75E,Y
STA $186C,X
ADDR_03B7BA:
DEY
BPL ADDR_03B79A
LDY $15EA,X
LDA $E4,X
SEC
SBC $1A
STA $00
LDA $D8,X
SEC
SBC $1C
STA $01
RTS

ADDR_03B7CF:
PLA
PLA
RTS";
                #endregion

                txt = txt.Replace(".include(GetDrawInfo)", x);
            }
            if (txt.Contains(".include(SubOffScreen)"))
            {
                #region SubOffScreen
                string x = @"SPR_T12:             
    db $40,$B0
SPR_T13:             
    db $01,$FF
SPR_T14:             
    db $30,$C0,$A0,$C0,$A0,$F0,$60,$90
    db $30,$C0,$A0,$80,$A0,$40,$60,$B0
SPR_T15:             
    db $01,$FF,$01,$FF,$01,$FF,$01,$FF
    db $01,$FF,$01,$FF,$01,$00,$01,$FF

SubOffScreen:
STZ $03

START_SUB:
JSR SUB_IS_OFF_SCREEN
BEQ RETURN_35
LDA $5B
AND #$01
BNE VERTICAL_LEVEL
LDA $D8,x
CLC
ADC #$50
LDA $14D4,x
ADC #$00
CMP #$02
BPL ERASE_SPRITE
LDA $167A,x
AND #$04
BNE RETURN_35
LDA $13
AND #$01
ORA $03
STA $01
TAY
LDA $1A
CLC
ADC SPR_T14,y
ROL $00
CMP $E4,x
PHP
LDA $1B
LSR $00
ADC SPR_T15,y
PLP
SBC $14E0,x
STA $00
LSR $01
BCC SPR_L31
EOR #$80
STA $00
SPR_L31:             
LDA $00
BPL RETURN_35
ERASE_SPRITE:
LDA $14C8,x
CMP #$08
BCC KILL_SPRITE
LDY $161A,x
CPY #$FF
BEQ KILL_SPRITE
LDA #$00
STA $1938,y
KILL_SPRITE:         
STZ $14C8,x
RETURN_35:           
RTS

VERTICAL_LEVEL:
LDA $167A,x
AND #$04
BNE RETURN_35
LDA $13
LSR A
BCS RETURN_35
LDA $E4,x
CMP #$00
LDA $14E0,x
SBC #$00
CMP #$02
BCS ERASE_SPRITE
LDA $13
LSR A
AND #$01
STA $01
TAY
LDA $1C
CLC
ADC SPR_T12,y
ROL $00
CMP $D8,x
PHP
LDA $001D
LSR $00
ADC SPR_T13,y
PLP
SBC $14D4,x
STA $00
LDY $01
BEQ SPR_L38
EOR #$80
STA $00
SPR_L38:
LDA $00
BPL RETURN_35
BMI ERASE_SPRITE

SUB_IS_OFF_SCREEN:
LDA $15A0,x
ORA $186C,x
RTS";
                #endregion

                txt = txt.Replace(".include(SubOffScreen)", x);
            }

            //We also have to translate the definitions in the current file.
            //For this, we have to add the definitions used and append them
            //at the top of the current file.
            if (Settings.Default.loaddefs == true || Settings.Default.defboth == true)
            {
                try
                {
                    StringBuilder pc = new StringBuilder();
                    StringReader str = new StringReader(Settings.Default.listdef);
                    do
                    {
                        string current = str.ReadLine();
                        //Anything after the space will be discarded.
                        string currentdef = current.Substring(0, current.IndexOf(" "));

                        //First check if the file contains the definition, only then we append it.
                        if (txt.Contains(currentdef))
                            if (!txt.Contains("!" + current))
                                pc.AppendLine("!" + current);
                    }
                    while (str.Peek() != -1);
                    str.Close();
                    txt = pc + txt;
                }
                catch (Exception) { }
            }
            return txt;
        }

        private void ReloadDocument()
        {
            if (ActiveDocument == null)
                Error("You must save the file before it can be re-loaded.", "Failed to reload.");
            else
            {
                ActiveDocument.Scintilla.Text = File.ReadAllText(ActiveDocument.FilePath);
                ActiveDocument.Scintilla.Modified = false;
                if (ActiveDocument.Text.Contains(" *"))
                    ActiveDocument.Text = ActiveDocument.Text.Substring(0, Text.Length - 2);
                Log.cs.AppendText("\r\nReloaded file " + ActiveDocument.Text + ".");
            }
        }

        private void Error(string error, string title)
        {
            //I used this fairly often, so I used it as a function.
            MessageBox.Show(error, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public DocForm NewDocument()
        {
            //First we'll make a new instance of the document and set it in the dock panel. We'll also set it
            //to use the application properties.

            DocForm Doc = new DocForm();
            SetProperties(Doc);
            Doc.Text = String.Format(CultureInfo.CurrentCulture, "{0}{1}", DocTxt, ++_newDocumentCount + ")");
            Doc.Show(dockPanel1);
            incrementalsearcher.Searcher.Scintilla = Doc.Scintilla;
            return Doc;
        }

        private void OpenDocument()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "Open ASM file(s).";
            openFileDialog.Filter = "ASM Files (*.asm)|*.asm|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            foreach (string filePath in openFileDialog.FileNames)
            {
                //Add the file to the list.
                //It is probably a bad idea adding it right at the beginning of the function..

                Stream str;
                if ((str = openFileDialog.OpenFile()) != null)
                {
                    RFName = openFileDialog.FileName;
                    RFHMenu.AddFile(filePath);
                }
                str.Close();

                //Ensure this file isn't already open.
                bool isOpen = false;
                string s = string.Empty;
                foreach (DockContent _d in dockPanel1.Documents)
                {
                    if (!_d.Text.Contains("ASMPad :: Hex Editor") && _d.Text != "ROM Map" && _d.Text != "RAM Map")
                    {
                        if (filePath.Equals(((DocForm)_d).FilePath, StringComparison.OrdinalIgnoreCase))
                        {
                            _d.Select();
                            this.Text = "ASMPad :: " + _d.Text;
                            isOpen = true;
                            break;
                        }
                    }
                }
                //Open the files.
                if (!isOpen)
                    OpenFile(filePath);
            }
        }

        private void refclick(int number, String filename)
        {
            //When a recent file item is clicked..
            //First check if the file exists, if it doesn't remove it.
            //Otherwise we'll load the document.

            if (!File.Exists(filename))
            {
                DialogResult XBA = MessageBox.Show("Can't find the file " + filename + ".\r\rRemove it from the list?", "File Error", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation);
                if (XBA == DialogResult.Yes)
                    RFHMenu.RemoveFile(number);
            }
            else
            {
                RFHMenu.SetFirstFile(number);
                bool isOpen = false;

                foreach (DockContent _d in dockPanel1.Documents)
                {
                    if (!_d.Text.Contains("ASMPad :: Hex Editor") && _d.Text != "ROM Map" && _d.Text != "RAM Map")
                    {
                        if (filename.Equals(((DocForm)_d).FilePath, StringComparison.OrdinalIgnoreCase))
                        {
                            ((DocForm)_d).Select();
                            isOpen = true;
                            break;
                        }
                    }
                }
                if (!isOpen)
                {
                    OpenFile(filename);
                    Log.cs.AppendText("\r\nLoaded file " + filename + " from recently used files.");
                }
            }
        }

        bool isOpen;
        public void CheckIfAlreadyOpen(string filePath)
        {
            foreach (DockContent _d in dockPanel1.Documents)
            {
                if (!_d.Text.Contains("ASMPad :: Hex Editor") && _d.Text != "ROM Map" && _d.Text != "RAM Map")
                {
                    if (filePath.Equals(((DocForm)_d).FilePath, StringComparison.OrdinalIgnoreCase))
                    {
                        _d.Select();
                        this.Text = "ASMPad :: " + _d.Text;
                        isOpen = true;
                        break;
                    }
                }
            }
            //Open the files.
            if (!isOpen)
                OpenFile(filePath);
        }

        public DocForm OpenFile(string filePath)
        {
            DocForm doc = new DocForm();
            SetProperties(doc);
            doc.Scintilla.Text = File.ReadAllText(filePath);
            doc.Scintilla.UndoRedo.EmptyUndoBuffer();
            doc.Scintilla.Modified = false;
            doc.Text = Path.GetFileName(filePath);
            doc.FilePath = filePath;
            doc.Show(dockPanel1);
            incrementalsearcher.Searcher.Scintilla = doc.Scintilla;
            RFHMenu.AddFile(filePath);
            return doc;
        }

        public void _getdata(string IO)
        {
            DocForm d = new DocForm();
            SetProperties(d);
            incrementalsearcher.Searcher.Scintilla = d.Scintilla;
            d.Scintilla.Modified = false;
            d.Text = String.Format(CultureInfo.CurrentCulture, "{0}{1}", DocTxt, ++_newDocumentCount + ")");
            d.Show(dockPanel1, DockState.Document);
            d.Scintilla.Text = IO;
        }

        public void SetProperties(DocForm Doc)
        {
            //We'll set the properties of the document here.
            //Note that these properties also change every document, because it's also called in the document
            //changed event.
            Doc.Scintilla.Margins.Margin0.Width = Settings.Default.MarginWidth;
            Doc.Scintilla.Selection.BackColorUnfocused =
            Doc.Scintilla.Selection.BackColor = Settings.Default.selbackcl;
            Doc.Scintilla.Selection.ForeColorUnfocused =
            Doc.Scintilla.Selection.ForeColor = Settings.Default.selforcl;

            Doc.Scintilla.Caret.HighlightCurrentLine = Settings.Default.hal;
            Doc.Scintilla.Caret.CurrentLineBackgroundColor = Settings.Default.halcol;

            Doc.Scintilla.Indentation.ShowGuides = Settings.Default.showidentguides;
            Doc.Scintilla.Indentation.TabWidth = (int)Settings.Default.tabwidth;

            if (Settings.Default.smartindent)
                Doc.Scintilla.Indentation.SmartIndentType = SmartIndent.CPP;
            else
                Doc.Scintilla.Indentation.SmartIndentType = SmartIndent.None;

            //Whitespace?
            if (Settings.Default.whitespace == true)
            {
                Doc.Scintilla.Whitespace.Mode = WhitespaceMode.VisibleAlways;
                Doc.Scintilla.Whitespace.ForeColor = Settings.Default.whitespacecol;
            }
            else
                Doc.Scintilla.Whitespace.Mode = WhitespaceMode.Invisible;

            ////Note: We have to set keywords and styles for the new document.
            ////The lexing for RAM Addresses is done in the text inserted event of the textbox.
            Doc.Scintilla.Lexing.Keywords[0] = Settings.Default._opcodes;    //Opcode keywords.
            Doc.Scintilla.Lexing.Keywords[2] = Settings.Default._misc;       //Misc. keywords.
            Doc.Scintilla.Lexing.Keywords[1] = Settings.Default.okkey;
            Doc.Scintilla.Styles[10].ForeColor = Settings.Default.ok;

            if (Settings.Default.hl == true)
            {
                try
                {
                    string wp = string.Empty;
                    Regex r = new Regex(@"\w+[:\b]");
                    MatchCollection m = r.Matches(Doc.Scintilla.Text);
                    for (int i = 0; i < m.Count; i++)
                    {
                        if (!wp.Contains(m[i].Value.Substring(0, m[i].Value.Length - 1)))
                            wp += " " + m[i].Value.Substring(0, m[i].Value.Length - 1);
                    }
                    wp = wp.ToLower();
                    Doc.Scintilla.Lexing.Keywords[3] = wp;
                }
                catch (Exception dydx) { MessageBox.Show(dydx.ToString()); }
                Doc.Scintilla.Styles[12].ForeColor = Settings.Default.labclr;
                Doc.Scintilla.Styles[12].Bold = Settings.Default.boldlab;
                Doc.Scintilla.Styles[6].Bold = Settings.Default.boldlab;
            }

            //A X Y S w b l
            //Doc.Scintilla.Lexing.Keywords[1] = "A X Y S w b l";
            Doc.Scintilla.Styles[10].ForeColor = Color.DarkViolet;

            Doc.Scintilla.Styles[0].ForeColor = Settings.Default.addclr;
            Doc.Scintilla.Styles[1].ForeColor = Settings.Default.commentclr;
            Doc.Scintilla.Styles[2].ForeColor = Settings.Default.addclr;
            Doc.Scintilla.Styles[3].ForeColor = Settings.Default.opcodeclr;
            Doc.Scintilla.Styles[6].ForeColor = Color.Black;
            Doc.Scintilla.Styles[11].ForeColor = Settings.Default.keywordclr;

            Doc.Scintilla.Styles[3].Bold = Settings.Default.boldop;
            Doc.Scintilla.Styles[0].Bold = Settings.Default.boldaddr;
            Doc.Scintilla.Styles[1].Bold = Settings.Default.boldcom;
            Doc.Scintilla.Styles[1].Italic = Settings.Default.CommentItal;
            Doc.Scintilla.Styles[2].Bold = Settings.Default.boldaddr;
            Doc.Scintilla.Styles[11].Bold = Settings.Default.boldkey;
            Doc.Scintilla.Styles[10].Bold = Settings.Default.boldok;
        }
        #endregion

        #region Tools Menu

        private void cancelRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isrecording)
            {
                _recorded = "";
                _isrecording = false;
                Log.cs.AppendText("\r\nCancelled macro recording.");
            }
            else
                Log.cs.AppendText("\r\nYou aren't recording a macro!");
        }

        private void pauseRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isrecording)
            {
                if (_paused)
                {
                    _paused = false;
                    Log.cs.AppendText("\r\nUnpaused macro recording.");
                }
                else
                {
                    _paused = true;
                    Log.cs.AppendText("\r\nPaused macro recording.");
                }
            }
            else
            {
                //_isrecording = true;
                Log.cs.AppendText("\r\nCan't unpause/pause as you aren't recording a macro!");
            }
        }

        private void commandPromptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Tabs.SelectTab(Log.Tabs.TabPages[1]);
            Log.Show(dockPanel1, DockState.DockBottom);
            Log.cs.AppendText("\r\nViewing Command Prompt.");
        }

        private void templatesWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            X.Tabs.SelectTab(X.Tabs.TabPages[1]);
            X.Show(dockPanel1, DockState.DockRight);
        }

        private void configureSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            X.Tabs.SelectTab(X.Tabs.TabPages[0]);
            X.Show(dockPanel1, DockState.DockRight);
        }

        private void eventWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Tabs.SelectTab(Log.Tabs.TabPages[0]);
            Log.Show(dockPanel1, DockState.DockBottom);
        }

        private void errorListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Tabs.SelectTab(Log.Tabs.TabPages[2]);
            Log.Show(dockPanel1, DockState.DockBottom);
        }

        Maps M = new Maps();
        private void rAMMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DockContent f in dockPanel1.Contents)
            {
                if (f == M && !f.IsHidden)
                {
                    Log.cs.AppendText("\r\nRAM Map is already open!");
                    return;
                }
            }
            LoadMap(Application.StartupPath + "\\RAM.htm", "RAM", M);
            Log.cs.AppendText("\r\nLoaded ROM Map from application directory.");
        }

        ROMMap R = new ROMMap();
        private void rOMMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DockContent f in dockPanel1.Contents)
            {
                if (f == R && !f.IsHidden)
                {
                    Log.cs.AppendText("\r\nROM Map is already open!");
                    return;
                }
            }
            LoadMap(Application.StartupPath + "\\ROM.htm", "ROM", R);
            Log.cs.AppendText("\r\nLoaded RAM Map from application directory.");
        }

        private void LoadMap(string path, string map, DockContent _map)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("The " + map + " Map was not found in the directory " +
                    Path.GetDirectoryName(path) + ".", "Map not found.");
                Log.cs.AppendText("\r\nFailed to load ROM/RAM Map!");
            }
            else
            {
                _map.Show(dockPanel1, DockState.Document);
                Log.cs.AppendText("\r\nLoaded ROM/RAM Map..");
            }
        }

        private void updateRAMMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry("www.google.com");
                Stopwatch s = new Stopwatch();
                s.Start();
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.DownloadFile("http://www.smwcentral.net/?p=map&type=ram", Application.StartupPath + "\\temp.htm");

                string input = File.ReadAllText(@"""" + Application.StartupPath + "\\RAM.htm" + @"""");
                string replacement = File.ReadAllText("temp.htm");
                replacement = replacement.Substring(replacement.IndexOf("$7E0000"), replacement.IndexOf("$7FFFF8 (8 bytes) is used by LM demo recording/playing ASM</td></tr>") - replacement.IndexOf("$7E0000") + 68);

                int startIndex = input.IndexOf("$7E0000");
                int endIndex = input.IndexOf("$7FFFF8 (8 bytes) is used by LM demo recording/playing ASM") + 68;

                var sb = new StringBuilder(input.Length - (endIndex - startIndex) + replacement.Length);

                sb.Append(input.Substring(0, startIndex));
                sb.Append(replacement);
                sb.Append(input.Substring(endIndex));

                string output = sb.ToString();
                File.WriteAllText(@"""" + Application.StartupPath + "\\RAM.htm" + @"""", output);
                s.Stop();
                MessageBox.Show("Done in " + s.Elapsed);
                Log.cs.AppendText("\r\nUpdated RAM Map..");
            }
            catch (Exception) { Error("Failed to update the ROM Map.", "Update failed."); }
        }

        private void updateROMMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry("www.google.com");
                Stopwatch s = new Stopwatch();
                s.Start();
                System.Net.WebClient wc = new System.Net.WebClient();
                string dir = @"""" + Application.StartupPath + "\\temp.htm" + @"""";
                wc.DownloadFile("http://www.smwcentral.net/?p=map&type=rom", dir);

                //Read them.
                string input = File.ReadAllText(@"""" + Application.StartupPath + "\\ROM.htm" + @"""");
                string replacement = File.ReadAllText(dir);

                replacement = replacement.Substring(replacement.IndexOf(">00000</td>"), replacement.IndexOf(@"Instrument data.</td></tr>") - replacement.IndexOf(">00000</td>"));

                int startIndex = input.IndexOf(">00000</td>");
                int endIndex = input.IndexOf(@"Instrument data.</td></tr>");
                var sb = new StringBuilder(input.Length - (endIndex - startIndex) + replacement.Length);

                sb.Append(input.Substring(0, startIndex));
                sb.Append(replacement);
                sb.Append(input.Substring(endIndex));

                string output = sb.ToString();
                File.WriteAllText(@"""" + Application.StartupPath + "\\ROM.htm" + @"""", output);
                s.Stop();
                MessageBox.Show("Done in " + s.Elapsed);
                Log.cs.AppendText("\r\nUpdated ROM Map..");

            }
            catch (Exception) { Error("Failed to update the ROM Map.", "Update failed."); }
        }

        InputBox IO = new InputBox();
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            IO.Show(dockPanel1, DockState.DockBottom);
            IO.DockPanel.DockBottomPortion = 192;
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUtil(1);
        }

        private void aSCIIToHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUtil(0);
        }

        private void LoadUtil(int num)
        {
            IO.Tabs.SelectTab(IO.Tabs.TabPages[num]);
            IO.Show(dockPanel1, DockState.DockBottom);
            IO.DockPanel.DockBottomPortion = 197;
            Log.cs.AppendText("\r\nLoaded utility.");
        }

        private void yXPPCCCTSetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUtil(3);
        }

        private void findOpcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUtil(4);
        }

        private void findPlayerPoseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUtil(5);
        }

        private void findSoundEffectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUtil(6);
        }

        private void otherUtilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUtil(2);
        }

        //SidePane SP = new SidePane();
        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            X.Tabs.SelectTab(X.Tabs.TabPages[2]);
            X.Show(dockPanel1, DockState.DockRight);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            //Note that only one hex-editor is shown.
            //Adding many tabs for hex editing files would cause a lot of lag.
            FormHexEditor FHE = new FormHexEditor();
            FHE.Show(dockPanel1, DockState.Document);
            Log.cs.AppendText("\r\nLoaded hex editor.");
        }
        #endregion

        #region Assemble Menu

        string apppath = Application.StartupPath + "\\xkas.exe";
        private void countFileStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //This should be a useful feature.
            //It counts the # of bytes, opcodes, definitions, labels, characters and lines.
            CountData();
        }


        private void toolStripMenuItem49_Click(object sender, EventArgs e)
        {
            try
            {
                RunEm(Settings.Default._empath);
            }
            catch (Exception) { Error("Failed to run emulator. Check if the emulator and ROM exist.", "Error"); };
        }

        private void RunEm(string settingpath)
        {
            if (settingpath == string.Empty)
            {
                Error("Define the emulator path.", "Error");
                EmulatorPath EP = new EmulatorPath();
                EP.ShowDialog(this);
                Log.cs.AppendText("\r\nConfigure the emulator/debugger path.");
            }
            else if (_rompath == null)
            {
                Error("You haven't defined a ROM to load!", "Error");
                Log.cs.AppendText("\r\nLoad a ROM before running the emulator/debugger.");
            }
            else
            {
                if (!File.Exists(settingpath))
                {
                    Error("Emulator or debugger not found.", "Error");
                    Log.cs.AppendText("\r\nCheck if the emulator and debugger paths exist.");
                }
                else
                //Haha, while its called Assemble the function can run any process.
                {
                    ProcessStartInfo P = new ProcessStartInfo(settingpath);   // Assembler.
                    P.Arguments = _rompath;                                   // File to assemble.
                    P.CreateNoWindow = true;
                    P.UseShellExecute = false;
                    P.RedirectStandardOutput = true;
                    Process.Start(P);
                    Log.cs.AppendText("\r\nRunning ROM " + _rompath + " in emulator.");
                }
            }
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            RunEm(Settings.Default.debugpath);
        }

        public void CountData()
        {
            string preserved = Directory.GetCurrentDirectory();

            if (!File.Exists(apppath))
                Error("Can't calculate file statistics as xkas doesn't exist in the tool's directory.", "Error");
            try
            {
                string p = ActiveDocument.Scintilla.Text + "\r\nprint bytes\r\nprint opcodes";
                File.WriteAllText(Application.StartupPath + "\\tempasm.asm", p);

                int num = 0;
                string bytes = string.Empty;
                string opcodes = string.Empty;

                Directory.SetCurrentDirectory(Application.StartupPath);
                string dirpath = Application.StartupPath + "\\tempasm.asm";
                string output = Assemble("xkas.exe", "tempasm.asm");

                StringReader str = new StringReader(output);
                do
                {
                    string current = str.ReadLine();
                    if (current.Length < 10 && num == 0)
                    {
                        bytes = current.Trim();
                        num++;
                    }
                    else if (current.Length < 10 && num > 0)
                        opcodes = current.Trim();
                }
                while (str.Peek() != -1);
                str.Close();

                string s = TrimComm(ActiveDocument.Scintilla.Text);

                string matches = string.Empty;
                Regex r = new Regex(@"\b\w+[:\b]");
                MatchCollection m = r.Matches(s);
                Regex Reg = new Regex(@"!\w+", RegexOptions.Multiline);
                MatchCollection autocomp = Reg.Matches(s);
                List<string> sigh = new List<string>();

                for (int i = 0; i < autocomp.Count; i++)
                {
                    if (!sigh.Contains(autocomp[i].Value))
                        sigh.Add(autocomp[i].Value);
                }

                Directory.SetCurrentDirectory(preserved);

                MessageBox.Show("Number of bytes: " + bytes + "\nNumber of opcodes: " + opcodes + "\nNumber of lines: " +
                    ActiveDocument.Scintilla.Lines.Count + "\nNumber of characters: " + ActiveDocument.Scintilla.Text.Length
                    + "\nNumber of labels: " + m.Count + "\nNumber of definitions used: " + sigh.Count,
                    "File Stats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.cs.AppendText("\r\nCounted file statistics.");
                File.Delete(Application.StartupPath + "\\tempasm.asm");
            }
            catch (Exception R) { Error("Failed to calculate.\n\nException details:\n\n" + R.ToString(), "Error"); }
        }

        private void outputBINFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                //We save the file, and then assemble it through TRASM to generate a .bin file.
                //I don't use xkas because xkas generates a 32kb file.

                string ps = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(Application.StartupPath);
                File.WriteAllText("tempasm.asm", ActiveDocument.Scintilla.Text);
                Assemble("trasm.exe", "tempasm.asm");
                Log.cs.AppendText("\r\nAssembled file " + ActiveDocument.Text + " as tempasm.bin in " + Application.StartupPath + ".");
                try { File.Delete("tempasm.err"); File.Delete("tempasm.asm"); }
                catch { }
                Directory.SetCurrentDirectory(ps);
            }
        }

        string output;
        CommandPrompt Log = new CommandPrompt(Application.StartupPath);
        Stopwatch _new = new Stopwatch();

        private void assembleFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
            {
                str.Clear();
                errors.Clear();
                //This routine is quite big.
                //We have to assemble the file through Xkas, report and show errors, highlight the
                //error lines, show tooltips for the errors and also show the error list.
                //First translate the definitions, save the file and assemble it.
                //ActiveDocument.Scintilla.Text = TranslateDef(ActiveDocument.Scintilla.Text);

                string p = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(Application.StartupPath);
                File.WriteAllText("tempasm.asm", ActiveDocument.Scintilla.Text);

                _new.Start();

                output = Assemble("xkas.exe", "tempasm.asm");
                _new.Stop();
                if (!output.Contains("error"))
                {
                    //Successful.    
                    try
                    {
                        ActiveDocument.Scintilla.Markers.DeleteAll();
                        //Log.Hide();
                        Log.listView1.Items.Clear();
                        info.Text = "No errors found when assembling the current file!";
                        Log.cs.AppendText("\r\nFile successfully assembled. (" + _new.Elapsed + ").");
                        Log.toolStripButton1.Text = "0 Error(s)";
                    }
                    catch (Exception) { }
                }
                else
                {
                    Log.cs.AppendText("\r\nErrors reported during the assembling of file " + ActiveDocument.Text + ". (" + _new.Elapsed + ").");
                    ShowErrors();
                }

                Directory.SetCurrentDirectory(p);
            }
        }

        public void ShowErrors()
        {
            ActiveDocument.Scintilla.Markers.DeleteAll();
            Log.Tabs.SelectTab(Log.Tabs.TabPages[2]);
            Log.Show(dockPanel1, DockState.DockBottom);
            Log.DockPanel.DockBottomPortion = 205;
            Log.listView1.Items.Clear();
            Log.listView1.ItemActivate += (a, b) => _gotoerror();     
            //if (Settings.Default.hel == true)
            //{
                string thisline;
                StringReader stream = new StringReader(output);
                //MessageBox.Show(output);
            ScintillaNet.Marker marker = ActiveDocument.Scintilla.Markers[2];
                marker.Symbol = ScintillaNet.MarkerSymbol.Background;
                Color errorcol = System.Drawing.ColorTranslator.FromHtml("#FA90A6");
                marker.BackColor = errorcol;
                do
                {
                    //Get the line numbers in a list string.
                    thisline = stream.ReadLine();
                    int what_ln = thisline.LastIndexOf(":");
                    int length = 0;
                    if (thisline.Contains("{"))
                    {
                        //If the line contains an error, we have to find the position.
                        //error: tempasm.asm: line 1{2}: invalid opcode or command [argsRTS]
                        //error: tempasm.asm: line 1{24}: invalid opcode or command [argsRTS]
                        //error: tempasm.asm: line 1{2}
                        int _void = thisline.IndexOf("{");
                        //error: tempasm.asm: line 1
                        length = _void - 25;
                    }
                    else
                    {
                        length = what_ln - 25;
                    }
                    try
                    {
                        int activecrap = int.Parse(thisline.Substring(25, length));
                        string trim = thisline.Substring(thisline.LastIndexOf(":") + 2);
                        errors.Add(trim);
                        str.Add((activecrap - 1).ToString());
                        ActiveDocument.Scintilla.Lines[activecrap - 1].AddMarker(marker);

                        ListViewItem item1 = new ListViewItem(activecrap.ToString());
                        item1.SubItems.Add(trim);
                        if (ActiveDocument.FilePath == null)
                        {
                            if (ActiveDocument.Text.Contains("*"))
                                item1.SubItems.Add(ActiveDocument.Text.Substring(0, ActiveDocument.Text.IndexOf("*")));
                            else
                                item1.SubItems.Add(ActiveDocument.Text);
                        }
                        else
                            item1.SubItems.Add(ActiveDocument.FilePath);
                        Log.listView1.Items.AddRange(new ListViewItem[] { item1 });
                    }
                    catch (Exception) { }
                }
                while (stream.Peek() != -1);
                stream.Close();
                Log.toolStripButton1.Text = str.Count + " Error(s)";
            //}
        }

        private void _gotoerror()
        {
            dockPanel1.ActiveDocument.DockHandler.Activate();
            int selectednum = int.Parse(Log.listView1.FocusedItem.Text);
            ActiveDocument.Scintilla.GoTo.Line(selectednum - 1); // We count the 0.
        }
        #endregion

        #region Other Misc Code

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save the recent file list to the registry!
            RFHMenu.SaveToRegistry();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            RFHMenu.RemoveAll();
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveDocument.DockHandler.TabText == "ROM Map" ||
            dockPanel1.ActiveDocument.DockHandler.TabText == "RAM Map")
            {
                foreach (Form d in this.MdiChildren)
                {
                    if (d.Text != dockPanel1.ActiveDocument.DockHandler.TabText)
                        d.Close();
                }
            }
            else if (dockPanel1.ActiveDocument.DockHandler.TabText == "Hex Editor")
            {
                foreach (DockContent d in this.MdiChildren)
                {
                    if (d.TabText != "Hex Editor")
                        d.Close();
                }
            }
            else
            {
                foreach (Form d in this.MdiChildren)
                {
                    if (d != ActiveDocument)
                        d.Close();
                }
            }
            LG.cs.AppendText("\r\nClosed all other files.");
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form d in this.MdiChildren)
                d.Close();

            LG.cs.AppendText("\r\nClosed all files.");
        }

        private void copyNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument == null || ActiveDocument.FilePath == null)
                return;
            else
                System.Windows.Forms.Clipboard.SetDataObject(ActiveDocument.FilePath, true);
        }

        private void copyNameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                System.Windows.Forms.Clipboard.SetDataObject(ActiveDocument.Text, true);
        }

        private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Return if there is no document, or there is no filepath.
            if (ActiveDocument == null || ActiveDocument.FilePath == null)
                return;
            else
                Process.Start(ActiveDocument.FilePath.Substring(0, ActiveDocument.FilePath.LastIndexOf("\\")));
        }

        private void contextMenuStrip1_Paint(object sender, PaintEventArgs e)
        {
            if (dockPanel1.ActiveDocument.DockHandler.TabText == "Hex Editor" ||
                dockPanel1.ActiveDocument.DockHandler.TabText == "ROM Map" ||
                dockPanel1.ActiveDocument.DockHandler.TabText == "RAM Map")
                return;

            Bitmap bmp = new Bitmap(
  System.Reflection.Assembly.GetEntryAssembly().
    GetManifestResourceStream("ASMPad.Resources.Save.ico"));

            if (ActiveDocument != null)
            {
                if (!contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1].Text.Contains("Save"))
                    contextMenuStrip1.Items.Add("Save", bmp);

                string fsdn = ActiveDocument.Text;
                if (fsdn.Contains("*"))
                    contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1].Text = "Save " + fsdn.Substring(0, fsdn.IndexOf("*")) + "..";
                else
                    contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1].Text = "Save " + ActiveDocument.Text + "..";
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.StartsWith("Save"))
            {
                contextMenuStrip1.Close();
                s1.PerformClick();
            }
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            if (ActiveDocument == null || ActiveDocument.FilePath == null)
                return;
            else
            {
                DialogResult dr = MessageBox.Show("Delete file " + ActiveDocument.FilePath + "?", "Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        string p = ActiveDocument.FilePath; File.Delete(ActiveDocument.FilePath); ActiveDocument.Close();
                        Log.cs.AppendText("\r\nClosed and deleted file " + p + ".");
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString()); }
                }
            }
        }
        #endregion

        SidePane X = new SidePane();
        #region Form Events

        private void UpdateAC()
        {
            _autocomp.Clear();
            Regex Reg = new Regex(@"!\w+", RegexOptions.Multiline);
            MatchCollection myMatchCollection = Reg.Matches(ActiveDocument.Scintilla.Text);
            foreach (Match myMatch in myMatchCollection)
            {
                if (!_autocomp.Contains(myMatch.Value.Substring(1)))
                    _autocomp.Add(myMatch.Value.Substring(1));
            }
        }

        private void UpdateList(bool type)
        {
            insmenu.Enabled = filemenu.Enabled = assemblemenu.Enabled = editmenu.Enabled =
            X.listBox1.Enabled = as1.Enabled = as2.Enabled = as3a.Enabled = as4.Enabled =
            as5.Enabled = as6.Enabled = as7.Enabled = s12.Enabled = s2.Enabled = s3.Enabled
            = incrementalsearcher.Enabled = _goto4.Enabled = macro1.Enabled =
            macro2.Enabled = macro3.Enabled = macro4.Enabled = macro5.Enabled = b1.Enabled
            = b2.Enabled = b3.Enabled = b4.Enabled = m6.Enabled = type;
            X.listBox1.Enabled = X.button1.Enabled = X.button8.Enabled = _gtm.Enabled =
            _gotodef.Enabled = format.Enabled = s1.Enabled = type;
        }

        private void toolStripComboBox2_DropDown(object sender, EventArgs e)
        {
            //Get all words that start with !.
            _gotodef.Items.Clear();
            Regex Reg = new Regex(@"!\w+", RegexOptions.Multiline);
            MatchCollection m = Reg.Matches(TrimComm(ActiveDocument.Scintilla.Text));
            for (int i = 0; i < m.Count; i++)
            {
                if (!_gotodef.Items.Contains(m[i]))
                    _gotodef.Items.Add(m[i]);
            }

            for (Int16 i = 0; i <= _gotodef.Items.Count - 2; i++)
            {
                for (int j = _gotodef.Items.Count - 1; j >= i + 1; j += -1)
                {
                    if (_gotodef.Items[i].ToString() == _gotodef.Items[j].ToString())
                        _gotodef.Items.RemoveAt(j);
                }
            }
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dockPanel1.ActiveDocument.DockHandler.Activate();
                foreach (Line ln in ActiveDocument.Scintilla.Lines)
                {
                    if (ln.Text.Contains(";"))
                    {
                        if (ln.Text.Substring(0, ln.Text.IndexOf(";")).Contains(_gotodef.Text) &&
                            ln.Text.Substring(0, ln.Text.IndexOf(";")).Contains("="))
                            ActiveDocument.Scintilla.GoTo.Line(ln.Number);
                    }
                    else
                    {
                        if (ln.Text.Contains(_gotodef.Text) && ln.Text.Contains("="))
                            ActiveDocument.Scintilla.GoTo.Line(ln.Number);
                    }
                }

                Log.cs.AppendText("\r\nJumped to definition " + _gotodef.SelectedItem.ToString() + ".");
            }
            catch (Exception) { LG.cs.AppendText("\r\nFailed to jump to definition."); };
        }

        private void _gotodef_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Jump to a definition's declaration in the current file.";
        }

        private void toolStripComboBox3_DropDown(object sender, EventArgs e)
        {
            _gtm.Items.Clear();
            string _m = string.Empty;

            foreach (Line l in ActiveDocument.Scintilla.Lines)
            {
                if (l.Text.ToLower().Contains("macro") && !l.Text.Contains("endmacro") && l.Text.Contains("("))
                {
                    if (l.Text.Contains(";"))
                    {
                        if (l.Text.Substring(0, l.Text.IndexOf(";")).Contains("macro"))
                        {
                            _m = l.Text.Trim().Substring(l.Text.IndexOf("macro"), l.Text.IndexOf("("));
                            _gtm.Items.Add(_m);
                        }
                    }
                    else
                    {
                        _m = l.Text.Trim().Substring(l.Text.IndexOf("macro"), l.Text.IndexOf("("));
                        _gtm.Items.Add(_m);
                    }
                }
            }
        }

        private void _gtm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetLineFromString(_gtm.SelectedItem.ToString());
                ActiveDocument.Activate();
                LG.cs.AppendText("\r\nJumped to " + (string)_gtm.SelectedItem);
            }
            catch (Exception) { LG.cs.AppendText("\r\nFailed to jump to macro."); };
        }

        private void _gtm_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Jumps to the selected macro in the file.";
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            if (active)
                return;

            if (e.FullPath == ActiveDocument.FilePath)
            {
                DialogResult dr = MessageBox.Show("ASMPad has detected that the current file " + ActiveDocument.FilePath + " has changed outside the editor. Do you want to reload it?", "Reload File?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    ReloadDocument();
                    active = true;
                }
            }
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            //Rename the current file.
            if (e.OldFullPath == ActiveDocument.FilePath)
            {
                DialogResult dr = MessageBox.Show("ASMPad has detected that the current file " + ActiveDocument.FilePath + " has been renamed outside the editor. Do you want to rename it in the editor now?", "Rename File?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                {
                    RenameDocument(e.FullPath, e.Name, e.OldFullPath);
                }
            }
        }

        private void RenameDocument(string newpath, string newname, string oldname)
        {
            //Rename the active document's text.
            //Change the path.
            ActiveDocument.Text = newname;
            ActiveDocument.FilePath = newpath;
            filelbl.Text = newpath;
            this.Text = "ASMPad :: " + newname;
            LG.cs.AppendText("\r\nRenamed file " + oldname + " to " + newname + ".");
            //Get rid of the RF value, and then add a new one.
            RFHMenu.RenameFile(oldname, newpath);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            X.Show(dockPanel1, DockState.DockRight);
            Log.Show(dockPanel1, DockState.DockBottom);
            Log.DockPanel.DockBottomPortion = 179;

            //if (_args != null && _args.Length != 0 && dockPanel1.DocumentsCount < 1)
            //{
            //    FileInfo fi = new FileInfo(_args[0]);
            //    if (fi.Exists)
            //    {
            //        OpenFile(fi.FullName);
            //        Log.cs.AppendText("\r\nLoaded file " + fi.FullName + " from command line.");
            //    }
            //}

            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Select();
        }

        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (dockPanel1.DocumentsCount == 0)
            {
                _newDocumentCount = 0;
                NewDocument();
                this.Text = "ASMPad :: " + ActiveDocument.Text;
                //filelbl.Text = "No File Loaded.";
                //linNo.Text = "";
                _goto4.Items.Clear();
                _gotodef.Items.Clear();
                _gtm.Items.Clear();
            }
            else
            {
                try
                {
                    if (dockPanel1.ActiveDocument.DockHandler.TabText == "RAM Map" ||
                        dockPanel1.ActiveDocument.DockHandler.TabText == "ROM Map")
                    {
                        this.Text = "ASMPad :: " + ActiveMdiChild.Text;
                        filelbl.Text = ActiveMdiChild.Text;
                        UpdateList(false);
                    }
                    else if (dockPanel1.ActiveDocument.DockHandler.TabText == "Hex Editor")
                    {
                        this.Text = ActiveMdiChild.Text;
                        filelbl.Text = ActiveMdiChild.Text;
                        UpdateList(false);
                    }
                    else
                    {
                        //Check file system watcher.

                        UpdateList(true);
                        UpdateFonts();
                        if (ActiveDocument.FilePath == null)
                            filelbl.Text = ActiveDocument.Text;
                        else
                        {
                            filelbl.Text = ActiveDocument.FilePath;
                            fileSystemWatcher1.Path = Path.GetDirectoryName(ActiveDocument.FilePath);
                        }

                        this.Text = "ASMPad :: " + ActiveDocument.Text;
                        incrementalsearcher.Searcher.Scintilla = ActiveDocument.Scintilla;
                        //Populate the new auto-completon list case, given that n^y = arcsec(-1(sqrt(args|i|)));
                        UpdateAC();
                        (Owner as DocForm)._highlightlbl();
                    }
                }
                catch (Exception) { }
            }
        }
        #endregion

        #region Status Info
        private void New_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Creates a new empty file.";
        }

        private void configureSettingsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Change document settings and other properties, affects all documents.";
        }

        private void templatesWindowToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "View the snippet window.";
        }

        private void eventWindowToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "View the event log.";
        }

        private void errorListToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "View the error output.";
        }

        private void setupDebuggerPathToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Configure the emulator and debugger path.";
        }

        private void closeToolStripMenuItem1_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Closes the document panel.";
        }

        private void resetZoomLevelToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Resets the zoom level.";
        }

        private void cancelRecordingToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Cancels macro recording.";
        }

        private void pauseRecordingToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Pauses macro recording.";
        }

        private void clearEventsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Clears all events logged into the events tab.";
        }

        private void openToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Opens a file or a selection of files.";
        }

        private void close_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Closes the current file.";
        }

        private void toolStripMenuItem45_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Closes all open files and creates an new one.";
        }

        private void toolStripMenuItem17_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Deletes the active document from your computer.";
        }

        private void toolStripMenuItem11_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Reloads the active document from your computer.";
        }

        private void saveToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Saves the active document.";
        }

        private void saveAsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Saves the active document with a new name.";
        }

        private void toolStripMenuItem5_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Saves all active documents.";
        }

        private void toolStripMenuItem31_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Exports the active file as a HTML file.";
        }

        private void toolStripMenuItem2_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Open an SMW ROM.";
        }

        private void toolStripMenuItem1_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Load or save the file as a template.";
        }

        private void toolStripMenuItem40_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Loads the template folder.";
        }

        private void loadTemplateToolStripMenuItem1_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Loads a template from the folder " + Application.StartupPath + "\\templates\\";
        }

        private void saveTemplateToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Saves the active document as a template.";
        }

        private void RFMenu_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Open a recently used file.";
        }

        private void toolStripMenuItem14_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Clears all recently used files.";
        }

        private void toolStripMenuItem41_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Saves the active document and then closes ASMPad.";
        }

        private void exitToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Closes ASMPad.";
        }

        private void blockToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Creates a new block with the offsets already added.";
        }

        private void patchToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Creates a new patch with the main code already added.";
        }

        private void spriteToolStripMenuItem1_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Creates a new sprite, with the INIT and MAIN routines already added.";
        }

        private void shooterGeneratorToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Creates a new shooter or generator, the INIT and MAIN routines are already added.";
        }

        private void toolStripMenuItem33_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Closes all documents except the active one.";
        }

        private void sMWCForumsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Opens the SMWC Forums in your default browser.";
        }

        private void _Wrap_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Toggles word wrap.";
        }

        private void unindentCodeToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Unindents the selected text.";
        }

        private void indentCodeToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Indents the selected text by one tab space.";
        }

        private void codeToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Indents opcode lines, labels and removes all comments.";
        }

        private void codeSnippetToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Loads the snippet list from the Snippets.xml file.";
        }

        private void toolStripMenuItem46_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Inserts the definitions from the definition file into the active document.";
        }

        private void fileToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Inserts the contents of a file into the active document.";
        }

        private void recordMacroToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Records keystrokes until the stop macro button is clicked.";
        }

        private void stopMacroToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Stops a macro.";
        }

        private void playbackMacroToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Inserts the last recorded macro into the active document.";
        }

        private void toolStripMenuItem28_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Toggles a bookmark on this line.";
        }

        private void toolStripMenuItem27_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Jumps to the previous bookmark in the active document.";
        }

        private void toolStripMenuItem29_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Jumps to the next bookmark in the active document.";
        }

        private void toolStripMenuItem30_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Removes all bookmarks from the active document.";
        }

        private void toolStripMenuItem35_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Adds a comment 'region' to the active document's cursor position.";
        }

        private void tableToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Adds a 8-bit table to the active document's cursor position.";
        }

        private void rATSTagToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Adds a RATS Tag to the active document's cursor position.";
        }

        private void pointerToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Adds a 16-bit pointer to the active document's cursor position.";
        }

        private void macroToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Adds a macro to the active document's cursor position.";
        }

        private void labelToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Adds a label to the active document's cursor position.";
        }

        private void assembleFileToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Assembles the active document, and reports errors (if any).";
        }

        private void outputBINFileToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Exports the current file as a .BIN.";
        }

        private void toolStripMenuItem48_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Inserts sprites from sprites.txt to the loaded ROM file.";
        }

        private void toolStripMenuItem47_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Patches an IPS file to a ROM.";
        }

        private void toolStripMenuItem34_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Patches the active document to the loaded ROM.";
        }

        private void toolStripMenuItem21_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Runs the ROM with a debugger (make sure the debugger path is configured).";
        }

        private void runEmulatorToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Runs the ROM with an emulator (make sure the emulator path is configured).";
        }

        private void countFileStatsToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            info.Text = "Shows the active document's byte, char, opcode, label, definition and line count.";
        }

        private void toolStripMenuItem9_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Loads a built-in hex editor.";
        }

        private void toolStripMenuItem12_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Loads the file explorer.";
        }

        private void commandPromptToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Loads a built-in command prompt.";
        }

        private void toolStripMenuItem22_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Loads the syntax highlighting configurator. You can change keywords and colors.";
        }

        private void updateROMMapToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Updates the ROM Map with the latest one from SMWCentral.";
        }

        private void updateRAMMapToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Updates the RAM Map with the latest one from SMWCentral.";
        }

        private void toolStripMenuItem3_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Loads a utility.";
        }

        private void toolStripMenuItem19_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Comments the selected text.";
        }

        private void toolStripMenuItem18_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Uncomments the selected text.";
        }

        private void toolStripMenuItem53_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Removes all comments from the file.";
        }

        private void toolStripMenuItem54_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Undoes the last action.";
        }

        private void toolStripMenuItem55_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Redoes the last action.";
        }

        private void toolStripMenuItem56_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Cuts the selection and places it on the clipboard.";
        }

        private void toolStripMenuItem58_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Copes the selection to the clipboard.";
        }

        private void toolStripMenuItem57_MouseDown(object sender, MouseEventArgs e)
        {
            info.Text = "Pastes text from the clipboard to the active document's cursor position.";
        }

        private void toolStripMenuItem59_MouseHover(object sender, EventArgs e)
        {
            info.Text = "Deletes the selected text.";
        }

        private void toolStripMenuItem60_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Selects all text in the active document.";
        }

        private void toolStripMenuItem42_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Loads the find/replace dialog window.";
        }

        private void toolStripMenuItem44_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Go to a specific line..";
        }

        private void makeMacroToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Makes a macro out of the selected text.";
        }

        private void toolStripMenuItem25_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Compacts a set of lines into one line and separates them with a colon.";
        }

        private void toolStripMenuItem39_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Transforms the selected text to lower case.";
        }

        private void toolStripMenuItem38_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Transforms the selected text to upper case.";
        }

        private void overtypeModeToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Toggles overtype mode.";
        }

        private void incrementalsearcher_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Find a word in the active document.";
        }

        private void toolStripButton11_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Logs freespace in the loaded ROM.";
        }

        private void copyNameToolStripMenuItem1_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Copies the name of the active document.";
        }

        private void copyNameToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Copies the path of the active document.";
        }

        private void openContainingFolderToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Opens the folder of the active document.";
        }
        #endregion

        private void _goto_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter && ActiveDocument.Scintilla.Text.Contains(_goto4.Text))
                {
                    GetLineFromString(_goto4.Text);
                    ActiveDocument.Activate();
                }
            }
            catch (Exception) { }
        }

        private void clearEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.cs.Clear();
        }

        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {
            try
            {
                DockState d = dockPanel1.ActiveContent.DockHandler.CheckDockState(false);
                if (d == DockState.Document && !((DockContent)dockPanel1.ActiveDocument).Text.Contains("ASMPad :: Hex Editor")
                    && ((DockContent)dockPanel1.ActiveDocument).Text != "ROM Map" && ((DockContent)dockPanel1.ActiveDocument).Text != "RAM Map")
                    dockPanel1.ContextMenuStrip = contextMenuStrip1;
                else
                    dockPanel1.ContextMenuStrip = None;
            }
            catch (Exception) { }
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dockPanel1.ActiveContent.DockHandler.Close();
        }

        private void resetZoomLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Zoom = 0;
        }

        private void setupDebuggerPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmulatorPath EP = new EmulatorPath();
            EP.ShowDialog(this);
        }

        private void vRAMMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUtil(7);
        }

        private void layer3TPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUtil(8);
        }

        private void exportAsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            info.Text = "Export the file as a HTML file or an assembled .BIN file.";
        } 
    }
}