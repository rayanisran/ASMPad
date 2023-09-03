using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ASMPad.Properties;
using System.Xml.Linq;
using ScintillaNet;
using System.IO;
using System.Diagnostics;

namespace ASMPad
{
    public partial class SidePane : DockContent
    {
        public List<string> snippetcode = new List<string>();
        public SidePane()
        {
            InitializeComponent();
        }

        private void SidePane_Load(object sender, EventArgs e)
        {
                if (File.Exists(Application.StartupPath + "\\Snippets.xml"))
                    fileSystemWatcher1.Path = Application.StartupPath;
                else
                    MessageBox.Show("File Snippets.xml was not found in the application directory, the snippets will not be loaded."
                    , "Failed to load Snippets.xml", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                loaddef.Checked = Settings.Default.loaddefs;
                loadboth.Checked = Settings.Default.defboth;
                loadfile.Checked = Settings.Default.loadfile;
      
            //The templates listbox must contain all elements, and corresponding code snippets.
            try
            {
                PopulateList();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured when attempting to populate the templates list with snippets. Ensure that the Snippets.XML file exists and is formatted correctly. The snippets list will not be loaded."
                    , "Failed to load Snippets.xml", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ((Main)ParentForm).LG.cs.AppendText("\r\nLoaded document properties panel.");
        }

        private void PopulateList()
        {
            snippetcode.Clear();
            listBox1.Items.Clear();
            //Load the XML document, and fetch the snippet stuff from there.
            if (!File.Exists(Application.StartupPath + "\\Snippets.xml"))
                return;

            XDocument doc = XDocument.Load(Application.StartupPath + "\\Snippets.xml");
            foreach (XElement xe in doc.Elements("Snippets").Elements("Snippet"))
            {
                string snippetName = (string)xe.Attribute("name");
                string snippetCode = xe.Element("SnippetCode").Value;

                listBox1.Items.Add(snippetName);
                snippetcode.Add(snippetCode);
            }
        }

        #region Syntax Highlighting
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
                return;

            string selected = (string)listBox1.SelectedItem;
            XDocument doc = XDocument.Load(Application.StartupPath + "\\Snippets.xml");
            foreach (XElement xe in doc.Elements("Snippets").Elements("Snippet"))
            {
                if ((string)xe.Attribute("name") == selected)
                    ((Main)ParentForm).ActiveDocument.Scintilla.InsertText(xe.Element("SnippetCode").Value);
                ((Main)ParentForm).ActiveDocument.Activate(); 


            }                                
            ((Main)ParentForm).LG.cs.AppendText("\r\nInserted snippet " + selected);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ShowClr(panop);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowClr(pankey);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowClr(pancom);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowClr(panadd);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowClr(panlab);
        }

        private void ShowClr(Panel panel)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = panel.BackColor;
            colorDlg.FullOpen = true;
            if (colorDlg.ShowDialog() == DialogResult.OK)
                panel.BackColor = colorDlg.Color;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Settings.Default.boldaddr = boldadd.Checked;
            Settings.Default.boldcom = boldcom.Checked;
            Settings.Default.CommentItal = Italicize.Checked;
            Settings.Default.boldkey = boldkey.Checked;
            Settings.Default.boldok = boldok.Checked;
            Settings.Default.boldop = boldop.Checked;
            Settings.Default._misc = keybox.Text.ToLower();
            Settings.Default._opcodes = opbox.Text.ToLower();
            Settings.Default.opcodeclr = panop.BackColor;
            Settings.Default.addclr = panadd.BackColor;
            Settings.Default.commentclr = pancom.BackColor;
            Settings.Default.keywordclr = pankey.BackColor;
            Settings.Default.labclr = panlab.BackColor;
            Settings.Default.boldlab = checkBox7.Checked;
            Settings.Default.ok = ok.BackColor;
            Settings.Default.okkey = okkey.Text.ToLower();
            Settings.Default.hl = hl.Checked;
            Settings.Default.Save();

            //A problem with this feature is that the active document now becomes
            //the last one instead of the one being worked on.
            //To fix this, we could preserve the tab index of the active document
            //but I can't find a TabIndex for that.
            //So instead we'll just check if the name matches.

            string preserved_name = ((Main)ParentForm).ActiveDocument.Text;
            foreach (DockContent _d in ((Main)ParentForm).dockPanel1.Documents)
            {
                if (!_d.Text.Contains("ASMPad :: Hex Editor") && _d.Text != "ROM Map" && _d.Text != "RAM Map")
                {
                    ((DocForm)_d).Activate();
                    ((Main)ParentForm).Updater();
                }
            }

            foreach (DockContent _d in ((Main)ParentForm).dockPanel1.Documents)
            {
                if (!_d.Text.Contains("ASMPad :: Hex Editor") && _d.Text != "ROM Map" && _d.Text != "RAM Map")
                {
                    if (preserved_name.Equals(((DocForm)_d).Text
                        , StringComparison.OrdinalIgnoreCase))
                        ((DocForm)_d).Select();
                }
            }
            ((Main)ParentForm).LG.cs.AppendText("\r\nUpdated syntax highlighting configuration.");
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //Config
            Settings.Default.calltips = calltip.Checked;
            Settings.Default.autocomp = enableautocomp.Checked;
            Settings.Default.loaddefs = loaddef.Checked;
            Settings.Default.loadfile = loadfile.Checked;
            Settings.Default.defboth = loadboth.Checked;

            //Visualization
            Settings.Default.whitespace = whitespace.Checked;
            Settings.Default.whitespacecol = wpcol.BackColor;
            Settings.Default.hal = hal.Checked;
            Settings.Default.halcol = halcol.BackColor;
            Settings.Default.selbackcl = selback.BackColor;
            Settings.Default.selforcl = selfor.BackColor;
            Settings.Default.MarginWidth = lnwidth.Value;
            Settings.Default.tabwidth = tabwidth.Value;
            Settings.Default.smartindent = smartindent.Checked;
            Settings.Default.showidentguides = showle.Checked;
            Settings.Default.Save();

            string preserved_name = ((Main)ParentForm).ActiveDocument.Text;
            foreach (DockContent _d in ((Main)ParentForm).dockPanel1.Documents)
            {
                if (!_d.Text.Contains("ASMPad :: Hex Editor") && _d.Text != "ROM Map" && _d.Text != "RAM Map")
                {
                    ((DocForm)_d).Activate();
                    //((Main)ParentForm).Updater();
                    ((Main)ParentForm).SetProperties((DocForm)_d);
                }
            }

            foreach (DockContent _d in ((Main)ParentForm).dockPanel1.Documents)
            {
                if (!_d.Text.Contains("ASMPad :: Hex Editor") && _d.Text != "ROM Map" && _d.Text != "RAM Map")
                {
                    if (preserved_name.Equals(((DocForm)_d).Text
                        , StringComparison.OrdinalIgnoreCase))
                        ((DocForm)_d).Select();
                }
            }

            ((Main)ParentForm).LG.cs.AppendText("\r\nUpdated document properties.");
        }
        private void wpcol_Click(object sender, EventArgs e)
        {
            ShowClr(wpcol);
        }

        private void halcol_Click(object sender, EventArgs e)
        {
            ShowClr(halcol);
        }

        private void selfor_Click(object sender, EventArgs e)
        {
            ShowClr(selfor);
        }

        private void selback_Click(object sender, EventArgs e)
        {
            ShowClr(selback);
        }

        private void SidePane_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowClr(ok);
        }

        #region Color Schemes
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Change the scheme!
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    panop.BackColor = ColorTranslator.FromHtml("#BF1741");
                    panadd.BackColor = ColorTranslator.FromHtml("#1F9900");
                    pancom.BackColor = ColorTranslator.FromHtml("#4961EB");
                    pankey.BackColor = ColorTranslator.FromHtml("#8F4413");
                    ok.BackColor = ColorTranslator.FromHtml("#E82A99");
                    panlab.BackColor = ColorTranslator.FromHtml("#BA4C87");
                    break;
                case 1:
                    panop.BackColor = ColorTranslator.FromHtml("#C48908"); // Lime
                    panadd.BackColor = ColorTranslator.FromHtml("#607800"); // Seagreen.
                    pancom.BackColor = ColorTranslator.FromHtml("#237D00"); // Greeny Green.
                    pankey.BackColor = ColorTranslator.FromHtml("#B00000"); // Yellowy-green.
                    ok.BackColor =     ColorTranslator.FromHtml("#B07800"); // Lemon.
                    panlab.BackColor = ColorTranslator.FromHtml("#A16223"); // Orange?
                    break;
                case 2:
                    panop.BackColor = ColorTranslator.FromHtml("#B400F0");
                    panadd.BackColor = ColorTranslator.FromHtml("#FA63FF");
                    pancom.BackColor = ColorTranslator.FromHtml("#A10003");
                    pankey.BackColor = ColorTranslator.FromHtml("#7D5F8C");
                    ok.BackColor = ColorTranslator.FromHtml("#D100C0");
                    panlab.BackColor = ColorTranslator.FromHtml("#C90065");
                    break;
                case 3:
                    panop.BackColor = ColorTranslator.FromHtml("#C472B9");
                    panadd.BackColor = ColorTranslator.FromHtml("#65A6A6");
                    pancom.BackColor = ColorTranslator.FromHtml("#2F5E1E");
                    pankey.BackColor = ColorTranslator.FromHtml("#47939E");
                    ok.BackColor = ColorTranslator.FromHtml("#CC8585");
                    panlab.BackColor = ColorTranslator.FromHtml("#8A4848");
                    break;
                case 4:
                    panop.BackColor = ColorTranslator.FromHtml("#C20218");
                    panadd.BackColor = ColorTranslator.FromHtml("#DE853C");
                    pancom.BackColor = ColorTranslator.FromHtml("#753D00");
                    pankey.BackColor = ColorTranslator.FromHtml("#CC2A1F");
                    ok.BackColor = ColorTranslator.FromHtml("#B53544");
                    panlab.BackColor = ColorTranslator.FromHtml("#C45454");
                    break;
                case 5:
                    panop.BackColor = ColorTranslator.FromHtml("#0011FF");
                    panadd.BackColor = ColorTranslator.FromHtml("#7219D1");
                    pancom.BackColor = ColorTranslator.FromHtml("#045200");
                    pankey.BackColor = ColorTranslator.FromHtml("#009463");
                    ok.BackColor = ColorTranslator.FromHtml("#0077FF");
                    panlab.BackColor = ColorTranslator.FromHtml("#000275");
                    break;
                case 6:
                    panop.BackColor = ColorTranslator.FromHtml("#52A2F2");
                    panadd.BackColor = ColorTranslator.FromHtml("#C482FA");
                    pancom.BackColor = ColorTranslator.FromHtml("#5E5E5E");
                    pankey.BackColor = ColorTranslator.FromHtml("#AB7835");
                    ok.BackColor = ColorTranslator.FromHtml("#F5996C");
                    panlab.BackColor = ColorTranslator.FromHtml("#6000A1");
                    break;
                case 7: // Normal
                    panop.BackColor = ColorTranslator.FromHtml("#8080FF");
                    panadd.BackColor = ColorTranslator.FromHtml("#BF00BF");
                    pancom.BackColor = Color.Green;
                    pankey.BackColor = ColorTranslator.FromHtml("#00AD8E");
                    ok.BackColor = Color.DarkOrchid;
                    panlab.BackColor = Color.DarkSlateGray;
                    break;
                case 8: // CHOCO
                    panop.BackColor = ColorTranslator.FromHtml("#B5960D");
                    panadd.BackColor = ColorTranslator.FromHtml("#BA6D3D");
                    pancom.BackColor = ColorTranslator.FromHtml("#5C5241");
                    pankey.BackColor = ColorTranslator.FromHtml("#944343");
                    ok.BackColor = ColorTranslator.FromHtml("#9E6747");
                    panlab.BackColor = Color.Chocolate;
                    break;
            }
        }
        #endregion

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            PopulateList();
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            PopulateList();
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            PopulateList();
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            PopulateList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this snippet?", "Confirm Delete",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            //Open the XML File, and find the snippet.
            //Delete it.
            //Then PopulateList().

            try
            {
                string q = listBox1.SelectedItem.ToString();
                XDocument doc = XDocument.Load(Application.StartupPath + "\\Snippets.xml");
                doc.Descendants("Snippet").Where(xe => xe.Attribute("name") != null && xe.Attribute("name").Value == (string)listBox1.SelectedItem).SingleOrDefault().Remove();
                doc.Save(Application.StartupPath + "\\Snippets.xml");
                ((Main)ParentForm).LG.cs.AppendText("\r\nDeleted snippet " + q + ".");
            }
            catch (Exception dydx)
            {
                MessageBox.Show("Failed to delete snippet.\r\n\r\n" + dydx.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            string ID = Microsoft.VisualBasic.Interaction.InputBox("Enter the new name of the snippet.", "Rename Snippet", (string)listBox1.SelectedItem);
            if (ID.Length == 0)
                return;

            try
            {
                string x = File.ReadAllText(Application.StartupPath + "\\Snippets.xml");
                x = x.Replace(@"<Snippet name=""" + (string)listBox1.SelectedItem + @""">", @"<Snippet name=""" + ID + @""">");
                File.WriteAllText(Application.StartupPath + "\\Snippets.xml", x);
                PopulateList();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to rename snippet.\r\n\r\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            if (File.Exists(Application.StartupPath + "\\Snippets.xml"))
            {
                AddSnippet x = new AddSnippet();
                x.Text = "Edit Snippet";
                x.name.ReadOnly = true;
                x.name.Text = (string)listBox1.SelectedItem;           
                string scode = string.Empty;

                //Get the old snippet code.
                XDocument doc = XDocument.Load(Application.StartupPath + "\\Snippets.xml");
                foreach (XElement xe in doc.Elements("Snippets").Elements("Snippet"))
                {
                    if ((string)xe.Attribute("name") == (string)listBox1.SelectedItem)
                        scode = xe.Element("SnippetCode").Value;
                }
                scode = scode.Replace("\n", Environment.NewLine);
                x.code.Text = scode;
                x.code.Select();
                x.button1.Text = "Edit Snippet";
                x.ShowDialog(this);
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                deleteToolStripMenuItem.PerformClick();
        }

        private void deleteToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            ((Main)ParentForm).info.Text = "Delete a code snippet.";
        }

        private void editToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            ((Main)ParentForm).info.Text = "Edit a code snippet.";
        }

        private void renameToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            ((Main)ParentForm).info.Text = "Rename a code snippet.";
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            if (!button8.Enabled)
                Tooltip.Show("This will update syntax configuration, it's currently disabled as you're not on a document form at the moment.", button8, 4000, button8.Location.X, button8.Location.Y);
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            if (!button1.Enabled)
                Tooltip.Show("This will update document settings, it's currently disabled as you're not on a document form at the moment.", button1, 4000, button1.Location.X, button1.Location.Y);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSnippet x = new AddSnippet();
            x.ShowDialog(this);
        }

        private void addToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            ((Main)ParentForm).info.Text = "Add a new snippet to the list.";
        }
    }
}
