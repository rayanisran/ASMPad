#region Using Directives
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ASMPad.Properties;
using ScintillaNet;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;

#endregion Using Directives

namespace ASMPad
{
    public partial class DocForm : DockContent
    {
        #region Fields
        private string _filePath;
        public string wp = string.Empty;
        #endregion Fields

        #region Properties
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public Scintilla Scintilla
        {
            get
            {
                return scintilla;
            }
        }
        #endregion Properties

        #region Constructor
        public DocForm()
        {
            InitializeComponent();
            scintilla.NativeInterface.SetMouseDwellTime(200);
            TopMost = false;
            scintilla.NativeInterface.CallTipSetFore(ColorTranslator.ToOle(Color.Black));
            scintilla.NativeInterface.CallTipSetBack(ColorTranslator.ToOle(ColorTranslator.FromHtml("#FCFEFF")));
            scintilla.Margins.Margin2.Width = 0;
        }
        #endregion Constructor

        #region Misc Handlers

        private void DocumentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Scintilla.Modified)
            {
                //Load a confirmation dialog whether to save the file or not. fI don't really find this necessary though..
                string message = String.Format(
                    CultureInfo.CurrentCulture,
                    "The text in the {0} file has changed.{1}{2}Do you want to save the changes?",
                    Text.TrimEnd(' ', '*'),
                    Environment.NewLine,
                    Environment.NewLine);

                DialogResult dr = MessageBox.Show(this, message, Program.Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.Cancel)
                {
                    //Don't close it.
                    e.Cancel = true;
                    return;
                }
                else if (dr == DialogResult.Yes)
                {
                    //Save it before closing.
                    e.Cancel = !Save();
                    return;
                }
                else if (dr == DialogResult.No)
                    ((Main)ParentForm).LG.cs.AppendText("\r\nClosed file " + this.Text.TrimEnd('*') + ".");
            }
            else
                ((Main)ParentForm).LG.cs.AppendText("\r\nClosed " + this.Text + ".");
        }

        private void scintilla_ModifiedChanged(object sender, EventArgs e)
        {
            AddOrRemoveAsteric();
        }

        #endregion Misc Handlers

        private void scintilla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Insert)
                ((Main)ParentForm).LG.cs.AppendText("\r\nPress insert to toggle overtype mode.");

            if (e.Control == true)
                _highlightlbl();
            ((Main)ParentForm).linNo.Text = Scintilla.Lines.Current.Next + "/" + Scintilla.Lines.Count
            + ", Pos: " + Scintilla.CurrentPos;
        }

        private void scintilla_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Main)ParentForm).linNo.Text = Scintilla.Lines.Current.Next + "/" + Scintilla.Lines.Count
    + ", Pos: " + Scintilla.CurrentPos;
        }

        private void scintilla_MouseClick(object sender, MouseEventArgs e)
        {
            ((Main)ParentForm).linNo.Text = Scintilla.Lines.Current.Next + "/" + Scintilla.Lines.Count
    + ", Pos: " + Scintilla.CurrentPos;
        }

        private void scintilla_KeyUp(object sender, KeyEventArgs e)
        {
            ((Main)ParentForm).linNo.Text = Scintilla.Lines.Current.Next + "/" + Scintilla.Lines.Count
    + ", Pos: " + Scintilla.CurrentPos;
        }

        #region Methods

        public void CompDef()
        {
            ((Main)ParentForm)._autocomp.Sort();
            scintilla.AutoComplete.Show(((Main)ParentForm)._autocomp);
        }

        public void AddDef()
        {
            ((Main)ParentForm)._autocomp.Clear();
            Regex Reg = new Regex(@"!\w+", RegexOptions.Multiline);
            MatchCollection myMatchCollection = Reg.Matches(scintilla.Text);
            foreach (Match myMatch in myMatchCollection)
            {
                if (!((Main)ParentForm)._autocomp.Contains(myMatch.Value.Substring(1)))
                    ((Main)ParentForm)._autocomp.Add(myMatch.Value.Substring(1));
            }
        }

        private void LoadDefFromFile()
        {
            try
            {
                (((Main)ParentForm)._autocomp).Clear();
                StringReader str = new StringReader(Settings.Default.listdef);
                do
                {
                    string ln = str.ReadLine();
                    string adder = ln.Substring(0, ln.IndexOf(" "));
                    ((Main)ParentForm)._autocomp.Add(adder);
                }
                while (str.Peek() != -1);
                str.Close();

                ((Main)ParentForm)._autocomp.Sort();
                scintilla.AutoComplete.Show(((Main)ParentForm)._autocomp);
            }
            catch (Exception A) { MessageBox.Show("Error occured: " + A.ToString()); }
        }

        private void LoadBoth()
        {
            try
            {
                (((Main)ParentForm)._autocomp).Clear();
                StringReader str = new StringReader(Settings.Default.listdef);
                do
                {
                    string ln = str.ReadLine();
                    string adder = ln.Substring(0, ln.IndexOf(" "));
                    ((Main)ParentForm)._autocomp.Add(adder);
                }
                while (str.Peek() != -1);
                str.Close();

                Regex Reg = new Regex(@"!\w+", RegexOptions.Multiline);
                MatchCollection myMatchCollection = Reg.Matches(scintilla.Text);
                foreach (Match myMatch in myMatchCollection)
                {
                    if (!((Main)ParentForm)._autocomp.Contains(myMatch.Value.Substring(1)))
                        ((Main)ParentForm)._autocomp.Add(myMatch.Value.Substring(1));
                }

                ((Main)ParentForm)._autocomp.Sort();
                scintilla.AutoComplete.Show(((Main)ParentForm)._autocomp);
            }
            catch (Exception A) { MessageBox.Show("Error occured: " + A.ToString()); }
        }

        public bool Save()
        {
            if (String.IsNullOrEmpty(_filePath))
                return SaveAs();

            return Save(_filePath);
        }

        public bool SaveAs()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _filePath = saveFileDialog.FileName;
                Stream str;
                if ((str = saveFileDialog.OpenFile()) != null)
                {
                    ((Main)ParentForm).RFName = saveFileDialog.FileName;
                    ((Main)ParentForm).RFHMenu.AddFile(_filePath);
                    ((Main)ParentForm).ActiveDocument.Text = Path.GetFileName(_filePath);
                    ((Main)ParentForm).Text = "ASMPad :: " + Path.GetFileName(_filePath);
                    scintilla.Modified = false;
                }
                str.Close();
                return Save(_filePath);
            }

            return false;
        }

        public bool Save(string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                scintilla.Text = ((Main)ParentForm).TranslateDef(scintilla.Text);
                bw.Write(scintilla.RawText, 0, scintilla.RawText.Length - 1); // Omit trailing NULL.
                ((Main)ParentForm).LG.cs.AppendText("\r\nSaved file " + _filePath + ".");
                ((Main)ParentForm).active = true;
            }
            scintilla.Modified = false;
            return true;
        }

        public bool ExportAsHtml()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                string fileName = (Text.EndsWith(" *") ? Text.Substring(0, Text.Length - 2) : Text);
                dialog.Filter = "HTML Files (*.html;*.htm)|*.html;*.htm|All Files (*.*)|*.*";
                dialog.FileName = fileName + ".html";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    scintilla.Lexing.Colorize(); // Make sure the document is current
                    using (StreamWriter sw = new StreamWriter(dialog.FileName))
                        scintilla.ExportHtml(sw, fileName, false);

                    ((Main)ParentForm).LG.cs.AppendText("\r\nExported " + dialog.FileName + " as HTML file.");
                    return true;
                }
            }

            return false;
        }

        public void AddOrRemoveAsteric()
        {
            if (scintilla.Modified)
            {
                if (!Text.EndsWith(" *"))
                    Text += " *";
            }
            else
            {
                if (Text.EndsWith(" *"))
                    Text = Text.Substring(0, Text.Length - 2);
            }
        }
        #endregion

        //List<string> ar = new List<string>();

        public void _highlightlbl()
        {
            wp = string.Empty;
            Regex r = new Regex(@"\w+[:\b]");
            MatchCollection m = r.Matches(Scintilla.Text);
            for (int i = 0; i < m.Count; i++)
            {
                if (!wp.Contains(m[i].Value.Substring(0, m[i].Value.Length - 1)))
                    wp += " " + m[i].Value.Substring(0, m[i].Value.Length - 1);
            }
            wp = wp.ToLower();
            Scintilla.Lexing.Keywords[3] = wp;

            //New method of highlighting labels.

            //Before we add the word, we have to check that the word exists.
            //Add the word to the Scintilla list.
            //Of course, only add it if it's a label.

            //Get the current word, and add it.

            //string current_word = scintilla.GetWordFromPosition(scintilla.CurrentPos);
            //if (current_word != string.Empty && current_word.EndsWith(":") && current_word.Length > 3)
            //    ar.Add(current_word.TrimEnd(':'));

            //string keywords = string.Empty;
            //foreach (string s in ar)
            //{
            //    if (scintilla.Text.Contains(s))
            //        keywords += " " + s;
            //}

            ////for (int i = 0; i < ar.Count; i++)
            ////{
            ////    if (!scintilla.Text.Contains(ar[i]))
            ////        ar.RemoveAt(i);
            ////    else
            ////        keywords += " " + ar[i];
            ////}

            //MessageBox.Show(keywords);
            //Scintilla.Lexing.Keywords[3] = keywords;
        }

        string keywords = string.Empty;

        private void scintilla_CharAdded(object sender, CharAddedEventArgs e)
        {
            if (((Main)ParentForm)._isrecording && ((Main)ParentForm)._paused != true)
                ((Main)ParentForm)._recorded += e.Ch;

            if (Settings.Default.hl && this.Text != "all.log" && this.Text.ToLower() != "smwdisc.txt")
            {
                if (e.Ch == ':' || e.Ch == ' ')
                {
                    _highlightlbl();
                    //string current_word = scintilla.GetWordFromPosition(scintilla.CurrentPos);
                    //if (current_word != string.Empty && current_word.EndsWith(":") && current_word.Length > 3 && !ar.Contains(current_word.TrimEnd(':')))
                    //    ar.Add(current_word.TrimEnd(':'));

                    //foreach (string s in ar)
                    //{
                    //    if (scintilla.Text.Contains(s))
                    //        scintilla.Lexing.Keywords[3] += s + " ";
                    //    else
                    //        scintilla.Lexing.Keywords[3] = scintilla.Lexing.Keywords[3].Replace(s, "");
                    //}
                }
                wp = string.Empty;

                //Autocompletion.
                if (Settings.Default.autocomp == true)
                {
                    if (e.Ch == '!' && !scintilla.PositionIsOnComment(scintilla.CurrentPos, Lexer.Asm))
                    {
                        if (Settings.Default.defboth == true)
                            LoadBoth();
                        else if (Settings.Default.loaddefs == true)
                            LoadDefFromFile();
                        else if (Settings.Default.loadfile == true)
                        {
                            if (((Main)ParentForm)._autocomp.Count == 0)
                                return;
                            CompDef();
                        }
                    }
                    if (e.Ch == '=' || e.Ch == 'e')
                        AddDef();
                }
            }
        }

        #region Context Menu
        private void toolStripMenuItem56_Click(object sender, EventArgs e)
        {
            Scintilla.Clipboard.Cut();
        }

        private void toolStripMenuItem58_Click(object sender, EventArgs e)
        {
            Scintilla.Clipboard.Copy();
        }

        private void toolStripMenuItem57_Click(object sender, EventArgs e)
        {
            Scintilla.Clipboard.Paste();
        }

        private void toolStripMenuItem59_Click(object sender, EventArgs e)
        {
            Scintilla.Selection.Clear();
        }

        private void toolStripMenuItem60_Click(object sender, EventArgs e)
        {
            Scintilla.Selection.SelectAll();
        }

        private void toUppercaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla.Text = Scintilla.Text.ToUpper();
        }

        private void toLowercaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla.Text = Scintilla.Text.ToLower();
        }

        private void countBytesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Main)ParentForm).CountData();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Scintilla.Selection.Text == string.Empty || !File.Exists(Application.StartupPath + "\\Snippets.xml"))
                MessageBox.Show("Failed to add code to snippets. Ensure that the file Snippets.xml exists and that you've selected some code to use as a snippet.", "Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                AddSnippet x = new AddSnippet();
                x.code.Text = Scintilla.Selection.Text;
                x.ShowDialog(this);
            }
        }

        private void codeSnippetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Main)ParentForm).LoadSnippet();
        }

        private void commentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla.Selection.Text = ((Main)ParentForm).ToggleComments(true, Scintilla.Selection.Text);
            SendKeys.Send("{BACKSPACE}");
        }

        private void uncommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla.Selection.Text = ((Main)ParentForm).ToggleComments(false, Scintilla.Selection.Text);
            SendKeys.Send("{BACKSPACE}");
        }

        private void identToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla.Selection.Text = ((Main)ParentForm).IndentCode(true, Scintilla.Selection.Text);
        }

        private void unindentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla.Selection.Text = ((Main)ParentForm).IndentCode(false, Scintilla.Selection.Text);
        }

        private void compactCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla.Selection.Text = ((Main)ParentForm).JoinLines(Scintilla.Selection.Text);
        }

        private void makeMacroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla.Selection.Text = "macro _macro()\r\n" + Scintilla.Selection.Text + "\r\nendmacro ";
        }
        #endregion

        private void toolStripMenuItem55_Click(object sender, EventArgs e)
        {
            Scintilla.UndoRedo.Redo();
        }

        private void toolStripMenuItem54_Click(object sender, EventArgs e)
        {
            Scintilla.UndoRedo.Undo();
        }

        private void toolStripMenuItem1_MouseEnter(object sender, EventArgs e)
        {
            ((Main)ParentForm).info.Text = "Adds the selection to the snippet list.";
        }

        string[] overloads = new string[]
        {
            "RAM Address", "ROM Address", "Index", "Buffer Array", "Return Interrupt" };

        private void scintilla_CallTipClick(object sender, CallTipClickEventArgs e)
        {
        }

        string word = string.Empty;
        string maindesc = string.Empty;

        private void Errors()
        {
            for (int i = 0; i < ((Main)ParentForm).str.Count; i++)
            {
                if (scintilla.Lines.Current.Number.ToString() == ((Main)ParentForm).str[i])
                    scintilla.CallTip.Show(((Main)ParentForm).errors[i]);
            }
        }

        private void scintilla_DwellStart(object sender, ScintillaMouseEventArgs e)
        {
            ((Main)ParentForm).info.Text = "Ready.";
            //return;
            int m = scintilla.NativeInterface.MarkerGet(scintilla.Lines.Current.Number);
            if (m == 4)
            {
                Errors();
                return;
            }
            if (!Settings.Default.calltips)
                return;

            //word = scintilla.GetWordFromPosition(scintilla.CurrentPos);
            word = scintilla.GetWordFromPosition(e.Position);

            if (word == string.Empty || word == null)
            {
                scintilla.CallTip.Hide();
                return;
            }
            if (word.ToLower() == "$7fc800")
            {
                string x = @"Map16 High Byte Table in a clean ROM ($7FFE00-$7FFFFF are unused in horizontal levels, but used in vertical ones).
In a hacked ROM, the following are also placed here:
$7FFF00 (32 bytes) is used for LevelNames 2.0 by Ice Man
$7FFFDF (10 bytes) is used by BMF's LevelNames code
$7FFFF0 (10 bytes) is used by FuSoYa's SMB3 Pipe Code.
$7FFFF0 (4 bytes) is used by BMF's LevelASM code
$7FFFF8 (8 bytes) is used by LM demo recording/playing ASM";
                scintilla.CallTip.Show(x, e.Position);
                return;
            }
            else if (word.ToLower() == "$7f9c7b")
            {
                string x = @"Empty, untouched RAM on a clean ROM.
In a hacked ROM:
$7FAB10 (12 bytes) is used by Sprite Tool to check a number of things.
Bit 2 indicates if the first extra bit is set.
Bit 3 indicates if it's a custom sprite.
Bit 7 indicates if it has been initialized.
$7FAB1C (12 bytes) is used by Sprite Tool to indicate whether the sprite being processed uses custom code, maybe for other things?
$7FAB28 (12 bytes) is used by Sprite Tool to indicate the extra property byte 1 from the cfg file
$7FAB34 (12 bytes) is used by Sprite Tool to indicate the extra property byte 2 from the cfg file
$7FAB9E (12 bytes) is used by Sprite Tool to indicate the custom sprite number

$7FBC00-$7FC7FF area:
Empty on an unhacked ROM, used incrementally for Lunar Magic hacks. More space will be used here as more hacks are changed or added to Lunar Magic.

In version 1.6x:
$7FC000 : $00A bytes, Misc

In version 1.70+:
$7FBC00 : $400 bytes, BG data used by VRAM modification
$7FC000 : $1A bytes, Misc
$7FC070 : $8E bytes, new ExAnimation
$7FC300 : $400 bytes, BG data used by VRAM modification";
                scintilla.CallTip.Show(x, e.Position);
                return;
            }
            else if (word.StartsWith("$") && word.Length > 1)
            {
                //First check regs.txt, hehe.


                if (File.Exists(Application.StartupPath + "\\RAM.htm"))
                {
                    string p = File.ReadAllText(Application.StartupPath + "\\RAM.htm");
                    try
                    {
                        string desc = string.Empty;
                        string a = string.Empty;
                        string x = string.Empty;
                        string d = string.Empty;
                        if (word.ToUpper().StartsWith("$7E")) // Trim trailing $7E:
                            word = "$" + word.Substring(3);

                        string substr = string.Empty;

                        if (word.Length < 5)
                            substr = @"<tr><td class=""normal border nowrap"">$7E:" + word.ToUpper().Substring(1).PadLeft(4, '0');
                        else
                            substr = @"<tr><td class=""normal border nowrap"">$7E:" + word.ToUpper().Substring(1);

                        if (!p.Contains(substr))
                        {
                            //return;
                            //Hmm, it isn't in the RAM Map's $7E bank.
                            //We have to check if it's a part of some other address, check the bytes, subtract it from
                            //the address, if it's still not there then maybe it's part of bank $7F?
                            //substr = @"<tr><td class=""normal border nowrap"">" + word.ToUpper().Substring(0, 3) + ":" + word.Substring(3).PadLeft(4, '0');

                            //$7F1234
                            if (word.Length != 6)
                                return;

                            substr = @"<tr><td class=""normal border nowrap"">$7F:" + word.ToUpper().Substring(3);

                            if (p.Contains(substr))
                            {
                                desc = p.Substring(p.IndexOf(substr), 1000);
                                a = @"</td><td class=""normal border"">";
                                x = desc.Substring(desc.IndexOf(a) + 31);
                                d = desc.Substring(desc.IndexOf("</td></tr>"));
                                maindesc = desc.Substring(desc.IndexOf(a) + 31, x.Length - d.Length);
                                maindesc = maindesc.Replace("<br>", string.Empty).Replace(":<br>", ":").Replace("\r", string.Empty);
                                scintilla.CallTip.Show(maindesc, e.Position);
                                return;
                            }
                        }
                        else
                        {
                            desc = p.Substring(p.IndexOf(substr), 5000);
                            a = @"</td><td class=""normal border"">";
                            x = desc.Substring(desc.IndexOf(a) + 31);
                            d = desc.Substring(desc.IndexOf("</td></tr>"));
                            maindesc = desc.Substring(desc.IndexOf(a) + 31, x.Length - d.Length);
                            maindesc = maindesc.Replace("<br>", string.Empty).Replace(":<br>", ":").Replace("\r", string.Empty);
                            scintilla.CallTip.Show(maindesc, e.Position);
                            return;
                        }
                    }
                    catch (Exception) { word = string.Empty; return; };
                    maindesc = maindesc.Replace("<br>", string.Empty).Replace(":<br>", ":").Replace("\r", string.Empty);
                    scintilla.CallTip.Show(maindesc, e.Position);
                    return;
                }
            }
            if (word.Length > 2 && word.StartsWith("#") && word.Substring(1, 1) == "$")
            {
                try
                {
                    scintilla.CallTip.Show("Hexadecimal number " + word + ".\nDecimal equivalent: " +
                int.Parse(word.Substring(2), System.Globalization.NumberStyles.HexNumber), e.Position);
                }
                catch (Exception) { word = string.Empty; return; };
            }

            else if (word.Length > 1 && word.StartsWith("#"))
            {
                try
                {
                    int num = int.Parse(word.Substring(1));
                    scintilla.CallTip.Show("Decimal number " + word + ".\nHexadecimal equivalent: " + Convert.ToString(num, 16).ToUpper().PadLeft(6, '0'), e.Position);
                }
                catch (Exception) { word = string.Empty; return; };
            }

            else if (word.EndsWith(":") && word.Length > 2)
            {
                scintilla.CallTip.Show("Table/Label " + word.Replace(":", string.Empty), e.Position);
            }
            else if (word.StartsWith("!") && word.Length > 1 && word.Substring(1, 1) != "!")
            {
                scintilla.CallTip.Show("Definition " + word, e.Position);
            }
            else if (word.ToLower() == "header" || word.ToLower() == "warnpc" || word.ToLower() == "hirom" || word.ToLower() == "org" ||
                word.ToLower() == "org" || word.ToLower() == "savepc" || word.ToLower() == "loadpc" || word.ToLower()
                == "fillbyte" || word.ToLower() == "namespace" || word.ToLower() == "macro" || word.ToLower() == "endmacro"
                || word.ToLower() == "incbin" || word.ToLower() == "incsrc" || word.ToLower() == "pc" || word.ToLower() == "lorom" || word.ToLower() == "print")
            {
                scintilla.CallTip.Show("This is a misc command used by xkas, too lazy to document it.");
            }
            else if (word.ToLower() == "db" || word.ToLower() == "dd" || word.ToLower() == "dw" || word.ToLower()
                == "dl")
            {
                scintilla.CallTip.Show("db - Defines 8-bit data (db $xx).\ndw - Defines 16-bit data (dw $xxxx)\ndl - Defines 24-bit data (dl $xxxxxx).\ndd is rarely used.", e.Position);
            }
            else
            {
                try
                {
                    Assembly _assembly = Assembly.GetExecutingAssembly();
                    TextReader _reader = new StreamReader(_assembly.GetManifestResourceStream("ASMPad.Resources.finder.txt"));
                    string reader = _reader.ReadToEnd();
                    reader = reader.Substring(reader.IndexOf("OPERANDHELL"));
                    StringReader str = new StringReader(reader);
                    do
                    {
                        string a = str.ReadLine();
                        if (a.Length > 3)
                        {
                            if (word.ToUpper() == a.Substring(3, 3)) // Ignore the opc in opcAND..
                            {
                                //Copy from n - n(y)^2's inverse trig.
                                //opcAND to returnAND;
                                int startpos = reader.IndexOf("opc" + word.ToUpper()) + 3;
                                int endpos = reader.IndexOf("return" + word.ToUpper());
                                scintilla.CallTip.Show(reader.Substring(startpos, endpos - startpos).Replace("\r", ""), e.Position);
                                return;
                            }
                        }
                    }
                    while (str.Peek() != -1);
                    str.Close();
                }
                catch (Exception) { word = string.Empty; return; }
            }
        }

        private void scintilla_Scroll(object sender, ScrollEventArgs e)
        {
            scintilla.CallTip.Hide();
        }

        private void scintilla_MouseHover(object sender, EventArgs e)
        {
        }
    }
}