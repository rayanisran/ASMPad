using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Be.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing.Drawing2D;
using ASMPad;

namespace ASMPad
{
    public partial class FormHexEditor : DockContent
    {
        FormFind _formFind = new FormFind();
        FormFindCancel _formFindCancel;
        FormGoTo _formGoto = new FormGoTo();
        FormInsert _formInsert = new FormInsert();
        byte[] _findBuffer = new byte[0];
        string _fileName;

        public FormHexEditor()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Initializes the hex editor´s main form
        /// </summary>
        void Init()
        {
            DisplayText();

            ManageAbility();
        }

        /// <summary>
        /// Updates the File size status label
        /// </summary>
        void UpdateFileSizeStatus()
        {
            if (this.hexBox.ByteProvider == null)
                this.fileSize.Text = "File Size: 0 bytes";//string.Empty;
            else
                this.fileSize.Text = "File Size: " + Util.GetDisplayBytes(this.hexBox.ByteProvider.Length);
        }

        /// <summary>
        /// Displays the file name in the Form´s text property
        /// </summary>
        /// <param name="fileName">the file name to display</param>
        void DisplayText()
        {
            if (_fileName != null && _fileName.Length > 0)
            {
                string textFormat = "{0}{1} - {2}";
                string sReadOnly = ((DynamicFileByteProvider)hexBox.ByteProvider).ReadOnly
                    ? HexEditor.strings.Readonly : "";
                string text = Path.GetFileName(_fileName);
                this.Text = string.Format(textFormat, text, sReadOnly, ProgramHex.SOFTWARENAME);
            }
            else
            {
                this.Text = ProgramHex.SOFTWARENAME;
            }
        }

        /// <summary>
        /// Manages enabling or disabling of menu items and toolstrip buttons.
        /// </summary>
        void ManageAbility()
        {
            if (hexBox.ByteProvider == null)
            {
                saveToolStripMenuItem.Enabled = toolStripButton3.Enabled = false;

                findToolStripMenuItem.Enabled = false;
                findNextToolStripMenuItem.Enabled = false;
                goToToolStripMenuItem.Enabled = false;

                selectAllToolStripMenuItem.Enabled = false;
            }
            else
            {
                saveToolStripMenuItem.Enabled = toolStripButton3.Enabled = hexBox.ByteProvider.HasChanges();

                findToolStripMenuItem.Enabled = true;
                findNextToolStripMenuItem.Enabled = true;
                goToToolStripMenuItem.Enabled = true;

                selectAllToolStripMenuItem.Enabled = true;
            }

            ManageAbilityForCopyAndPaste();
        }

        /// <summary>
        /// Manages enabling or disabling of menustrip items and toolstrip buttons for copy and paste
        /// </summary>
        void ManageAbilityForCopyAndPaste()
        {
            copyHexStringToolStripMenuItem.Enabled =
                toolStripMenuItem3.Enabled = copyToolStripMenuItem.Enabled = hexBox.CanCopy();

            toolStripButton4.Enabled = cutToolStripMenuItem.Enabled = hexBox.CanCut();
            toolStripMenuItem1.Enabled = pasteToolStripMenuItem.Enabled = hexBox.CanPaste();
            pasteHexToolStripMenuItem.Enabled = toolStripMenuItem2.Enabled = hexBox.CanPasteHex();
        }

        /// <summary>
        /// Shows the open file dialog.
        /// </summary>
        void OpenFile()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Opens a file.
        /// </summary>
        /// <param name="fileName">the file name of the file to open</param>

        string romtype;
        public void OpenFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                ProgramHex.ShowMessage(HexEditor.strings.FileDoesNotExist);
                return;
            }

            if (hexBox.ByteProvider != null)
            {
                if (CloseFile() == DialogResult.Cancel)
                    return;
            }

            byte[] b = File.ReadAllBytes(fileName);

            //Detect if it's a ROM or not.
                int remainder = b.Length & 0x7FFF;
                if (remainder == 0)
                    romtype = "Headerless ROM";
                else if (remainder != 512)
                {
                    DialogResult dr = MessageBox.Show("The size of this file indicates that it is not a valid SNES ROM. Open the file anyway?", "Not a ROM", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.No || dr == DialogResult.Cancel)
                        return;
                    romtype = Path.GetExtension(fileName).ToUpper() + " file";
                }
                else // headerered
                    romtype = "Headered ROM";

                b = null;
            try
            {
                DynamicFileByteProvider dynamicFileByteProvider;
                try
                {
                    // try to open in write mode
                    dynamicFileByteProvider = new DynamicFileByteProvider(fileName);
                    dynamicFileByteProvider.Changed += new EventHandler(byteProvider_Changed);
                    dynamicFileByteProvider.LengthChanged += new EventHandler(byteProvider_LengthChanged);
                }
                catch (IOException) // write mode failed
                {
                    try
                    {
                        // try to open in read-only mode
                        dynamicFileByteProvider = new DynamicFileByteProvider(fileName, true);
                        if (ProgramHex.ShowQuestion(HexEditor.strings.OpenReadonly) == DialogResult.No)
                        {
                            dynamicFileByteProvider.Dispose();
                            return;
                        }
                    }
                    catch (IOException) // read-only also failed
                    {
                        // file cannot be opened
                        ProgramHex.ShowError(HexEditor.strings.OpenFailed);
                        return;
                    }
                }
                hexBox.ByteProvider = dynamicFileByteProvider;
                _fileName = fileName;
                DisplayText();
                UpdateFileSizeStatus();
                recentFileHandler.AddFile(fileName);
                string fr = Path.GetFileName(fileName) + " - ASMPad :: Hex Editor";
                ((Main)ParentForm).filelbl.Text = fr;
                ((Main)ParentForm).Text = fr;
                ((Main)ParentForm).LG.cs.AppendText("\r\nLoaded file for hex editor : " + fileName + ".");
                filetype.Text = romtype;
                filepath.Text = fileName;
                reloadFileToolStripMenuItem.Enabled = true;
            }
            catch (Exception ex1)
            {
                ProgramHex.ShowError(ex1);
                return;
            }
            finally
            {
                ManageAbility();
            }
        }

        /// <summary>
        /// Saves the current file.
        /// </summary>
        void SaveFile()
        {
            if (hexBox.ByteProvider == null)
                return;

            try
            {
                DynamicFileByteProvider dynamicFileByteProvider = hexBox.ByteProvider as DynamicFileByteProvider;
                dynamicFileByteProvider.ApplyChanges();
                ((Main)ParentForm).LG.cs.AppendText("\r\nSaved file in hex editor.");
            }
            catch (Exception ex1)
            {
                ProgramHex.ShowError(ex1);
            }
            finally
            {
                ManageAbility();
            }
        }

        /// <summary>
        /// Closes the current file
        /// </summary>
        /// <returns>OK, if the current file was closed.</returns>
        DialogResult CloseFile()
        {
            if (hexBox.ByteProvider == null)
                return DialogResult.OK;

            try

            {
                if (hexBox.ByteProvider != null && hexBox.ByteProvider.HasChanges())
                {
                    DialogResult res = MessageBox.Show(HexEditor.strings.SaveChangesQuestion,
                        ProgramHex.SOFTWARENAME,
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning);

                    if (res == DialogResult.Yes)
                    {
                        SaveFile();
                        CleanUp();
                    }
                    else if (res == DialogResult.No)
                    {
                        CleanUp();
                    }
                    else if (res == DialogResult.Cancel)
                    {
                        return res;
                    }

                    return res;
                }
                else
                {
                    CleanUp();
                    return DialogResult.OK;
                }
            }
            finally
            {
                ManageAbility();
            }
        }

        void CleanUp()
        {
            ((Main)ParentForm).LG.cs.AppendText("\r\nClosed file from hex editor.");
            if (hexBox.ByteProvider != null)
            {
                IDisposable byteProvider = hexBox.ByteProvider as IDisposable;
                if (byteProvider != null)
                    byteProvider.Dispose();
                hexBox.ByteProvider = null;
            }
            _fileName = null;
            fileSize.Text = "0 bytes";
            lines.Text = "Ln 0 Col 0";
            offset.Text = "PC: 0x000000, SNES: $000000";
            filetype.Text = "Load a file.";
            DisplayText();
        }

        /// <summary>
        /// Opens the Find dialog
        /// </summary>
        void Find()
        {
            if (_formFind.ShowDialog() == DialogResult.OK)
            {
                _findBuffer = _formFind.GetFindBytes();
                FindNext();
            }
        }

        /// <summary>
        /// Find next match
        /// </summary>
        void FindNext()
        {
            if (_findBuffer.Length == 0)
            {
                Find();
                return;
            }

            // show cancel dialog
            _formFindCancel = new FormFindCancel();
            _formFindCancel.SetHexBox(hexBox);
            _formFindCancel.Closed += new EventHandler(FormFindCancel_Closed);
            _formFindCancel.Show();

            // block activation of main form
            Activated += new EventHandler(FocusToFormFindCancel);

            // start find process
            long res = hexBox.Find(_findBuffer, hexBox.SelectionStart + hexBox.SelectionLength);

            _formFindCancel.Dispose();

            // unblock activation of main form
            Activated -= new EventHandler(FocusToFormFindCancel);

            if (res == -1) // -1 = no match
            {
                MessageBox.Show(HexEditor.strings.FindOperationEndOfFile, ProgramHex.SOFTWARENAME,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (res == -2) // -2 = find was aborted
            {
                return;
            }
            else // something was found
            {
                if (!hexBox.Focused)
                    hexBox.Focus();
            }

            ManageAbility();
        }

        /// <summary>
        /// Aborts the current find process
        /// </summary>
        void FormFindCancel_Closed(object sender, EventArgs e)
        {
            hexBox.AbortFind();
        }

        /// <summary>
        /// Put focus back to the cancel form.
        /// </summary>
        void FocusToFormFindCancel(object sender, EventArgs e)
        {
            _formFindCancel.Focus();
        }

        /// <summary>
        /// Displays the goto byte dialog.
        /// </summary>
        /// 

        private int SNESTOPC(string snesaddr)
        {
            try
            {
                int snes = Int32.Parse(snesaddr, System.Globalization.NumberStyles.HexNumber);
                int pcaddr = (snes & 0xFF);
                pcaddr |= (snes & 0x7F00);
                pcaddr |= (snes & 0xFF0000) >> 1;
                pcaddr += 512;
                return pcaddr;
            }
            catch (Exception) { return -1; }
        }

        void OverwriteBytes(long address, byte[] b)
        {
            //Before we write bytes, actually check whether it doesn't overflow.
            for (int i = 0; i < b.Length; i++)
            {
                hexBox.ByteProvider.WriteByte(address + i, b[i]);
            }
        }

        void Insert()
        {
            _formInsert.textBox1.Text = offset.Text.Substring(4, offset.Text.IndexOf(",") - 4);
            _formInsert.textBox2.Text = offset.Text.Substring(offset.Text.LastIndexOf(":") + 2);

            if (_formInsert.radioButton2.Checked)
                _formInsert.textBox1.SelectAll();
            if (_formInsert.radioButton3.Checked)
                _formInsert.textBox2.SelectAll();

            _formInsert.radioButton1.Text = "Current offset (" + offset.Text.Substring(4, offset.Text.IndexOf(",") - 4) + ")";

            if (_formInsert.ShowDialog() == DialogResult.Cancel)
                return;
            if (hexBox.ByteProvider.Length == 0)
                return;

            try
            {
                byte[] b = _formInsert.getbyte();

                if (_formInsert.radioButton1.Checked)
                {
                    //Append at current offset.
                    if (!_formInsert.or.Checked)
                        hexBox.ByteProvider.InsertBytes(hexBox.SelectionStart, b);
                    else
                        OverwriteBytes(hexBox.SelectionStart, b);
                }
                else if (_formInsert.radioButton2.Checked)
                {
                    int decform = 0;
                    if (_formInsert.textBox1.Text.StartsWith("x"))
                        decform = int.Parse(_formInsert.textBox1.Text.Substring(1), System.Globalization.NumberStyles.HexNumber);
                    else
                        decform = int.Parse(_formInsert.textBox1.Text, System.Globalization.NumberStyles.HexNumber);
                    
                    if (_formInsert.or.Checked)
                        hexBox.ByteProvider.InsertBytes(decform, b);
                    else
                        OverwriteBytes(hexBox.SelectionStart, b);
                }
                else if (_formInsert.radioButton3.Checked)
                {
                    //Append at SNES offset.
                    //We get the SNES offset, convert it to PC.
                    int pcaddr = SNESTOPC(_formInsert.textBox2.Text.Substring(1));

                    if (_formInsert.or.Checked)
                        hexBox.ByteProvider.InsertBytes(pcaddr, b);
                    else
                        OverwriteBytes(hexBox.SelectionStart, b);
                }
                else if (_formInsert.radioButton4.Checked)
                {
                    //Append at EOF.
                    if (!_formInsert.or.Checked)
                        hexBox.ByteProvider.InsertBytes(hexBox.ByteProvider.Length, b);
                    else
                        OverwriteBytes(hexBox.SelectionStart, b);
                }
                hexBox.Invalidate();
            }
            catch (Exception exp) { MessageBox.Show("Failed to insert bytes. This was probably because the insertion byte array is larger than the file size. The error message is as follows: " + exp.ToString()); }
        }

        void Goto()
        {
            string pos = ((int)hexBox.SelectionStart).ToString();
            if (pos.Length > 1)
            {
                try
                {
                    pos = pos.Substring(0, pos.Length - 1);
                    _formGoto.SetDefaultValue(long.Parse(pos));
                }
                catch (Exception a) { MessageBox.Show(a.ToString()); return; }
            }

            //if (_formGoto.ShowDialog() == DialogResult.Cancel)
            //    return;

            if (_formGoto.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (_formGoto._bytego.Text.StartsWith("x"))
                    {
                        string n = (_formGoto._bytego.Text.Substring(1));
                        int hex2 = int.Parse(n, System.Globalization.NumberStyles.HexNumber);
                        long _l = long.Parse(hex2.ToString());
                        hexBox.SelectionStart = _l;
                    }
                    else if (_formGoto._bytego.Text.StartsWith("$"))
                    {
                        //Convert the LoROM Address to PC..
                        //009F67.
                        string addr = _formGoto._bytego.Text.Substring(1);
                        int result = SNESTOPC(addr);
                        hexBox.SelectionStart = result;
                    }
                    else
                    {
                        long _l = long.Parse(_formGoto._bytego.Text);
                        hexBox.SelectionStart = _l;
                    }
                    hexBox.SelectionLength = 1;
                    hexBox.Focus();
                    hexBox.Invalidate();
                    ShowInfo();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    hexBox.Focus();
                }
            }
        }

        /// <summary>
        /// Enables drag&drop
        /// </summary>
        void hexBox_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        /// <summary>
        /// Processes a file drop
        /// </summary>
        void hexBox_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] formats = e.Data.GetFormats();
            object oFileNames = e.Data.GetData(DataFormats.FileDrop);
            string[] fileNames = (string[])oFileNames;
            if (fileNames.Length == 1)
            {
                OpenFile(fileNames[0]);
            }
        }

        void hexBox_Copied(object sender, EventArgs e)
        {
            ManageAbilityForCopyAndPaste();
        }

        void hexBox_CopiedHex(object sender, EventArgs e)
        {
            ManageAbilityForCopyAndPaste();
        }

        void byteProvider_Changed(object sender, EventArgs e)
        {
            ManageAbility();
        }

        void byteProvider_LengthChanged(object sender, EventArgs e)
        {
            UpdateFileSizeStatus();
        }

        void open_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        void save_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        void cut_Click(object sender, EventArgs e)
        {
            this.hexBox.Cut();
        }

        private void copy_Click(object sender, EventArgs e)
        {
            this.hexBox.Copy();
        }

        void paste_Click(object sender, EventArgs e)
        {
            this.hexBox.Paste();
        }

        private void copyHex_Click(object sender, EventArgs e)
        {
            this.hexBox.CopyHex();
        }

        private void pasteHex_Click(object sender, EventArgs e)
        {
            this.hexBox.PasteHex();
        }

        void find_Click(object sender, EventArgs e)
        {
            this.Find();
        }

        void goTo_Click(object sender, EventArgs e)
        {
            this.Goto();
        }

        void Insert_Click(object sender, EventArgs e)
        {
            this.Insert();
        }

        void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.hexBox.SelectAll();
        }

        void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void recentFiles_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            RecentFileHandler.FileMenuItem fmi = (RecentFileHandler.FileMenuItem)e.ClickedItem;
            this.OpenFile( fmi.Filename);
            ((Main)ParentForm).LG.cs.AppendText("\r\nLoaded recently used file for hex-editing : " + fmi.Filename + ".");
        }

        private void menuStrip_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //Rectangle rect = new Rectangle(0, 0, menuStrip.Width, menuStrip.Height);
            //System.Drawing.Drawing2D.LinearGradientBrush b = new System.Drawing.Drawing2D.LinearGradientBrush(rect, Color.White, Color.LightSteelBlue, LinearGradientMode.Vertical);
            //g.FillRectangle(b, rect); 
        }

        private void reloadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Reload the active file.
            long _current_offset = hexBox.SelectionStart;
            this.OpenFile(_fileName);
            hexBox.SelectionStart = _current_offset;
            hexBox.SelectionLength = 1;
            hexBox.Select();

            this.statusStrip.Text = "Reloaded file " + _fileName;
        }

        private void hexBox_MouseClick(object sender, MouseEventArgs e)
        {
            ShowInfo();
        }

        private void hexBox_KeyDown(object sender, KeyEventArgs e)
        {
            ShowInfo();
        }

        private void hexBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ShowInfo();
        }

        private void hexBox_KeyUp(object sender, KeyEventArgs e)
        {
            ShowInfo();
        }

        private void ShowInfo()
        {
            int pcaddr = Int32.Parse(((int)hexBox.SelectionStart).ToString("X"), System.Globalization.NumberStyles.HexNumber);
            this.lines.Text = string.Format("Line {0}, Col {1}", hexBox.CurrentLine, hexBox.CurrentPositionInLine);
            this.offset.Text = string.Format("PC: 0x{0}, SNES: ${1}", hexBox.SelectionStart.ToString("X").PadLeft(6, '0'),
            PCTOSNES(pcaddr).ToString("X").PadLeft(6, '0'));
        }        
        
        private int PCTOSNES(int pcaddr)
        {
            try
            {
                int snesaddr = (int)((pcaddr << 1) & 0x7F0000) | ((pcaddr | 0x8000) & 0xFFFF);
                int remainder = (int)hexBox.ByteProvider.Length & 0x7FFF;
                if (remainder == 512)
                    snesaddr -= 512;
                return snesaddr;      
            }
            catch (Exception) { return -1; }
        }

        private void hexBox_SelectionLengthChanged_1(object sender, EventArgs e)
        {
            ShowInfo();
            ManageAbilityForCopyAndPaste();
            slength.Text = "Selection: " + hexBox.SelectionLength.ToString() + " bytes";
        }

        private void hexBox_SelectionStartChanged_1(object sender, EventArgs e)
        {
            ShowInfo();
            ManageAbilityForCopyAndPaste();
            slength.Text = "Selection: " + hexBox.SelectionLength.ToString() + " bytes";
        }

        private void copyFileSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(hexBox.ByteProvider.Length.ToString(), true);
        }

        private void copyFileLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lines.Text, true);
        }

        private void copyPCOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(offset.Text.Substring(4, offset.Text.IndexOf(",") - 4), true);
        }

        private void copySNESOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(offset.Text.Substring(offset.Text.LastIndexOf(":") + 2), true);
        }

        private void addRemoveSNESHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (filetype.Text != "Headered ROM" && filetype.Text != "Headerless ROM" || filetype.Text == string.Empty)
                {
                    MessageBox.Show("This file is not a valid ROM!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (filetype.Text == "Headered ROM")
                {
                    //Remove it..
                    hexBox.ByteProvider.DeleteBytes(0, 0x200);
                    //MessageBox.Show("The header has been removed from this ROM.", "Header removed.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    filetype.Text = "Headerless ROM";
                    hexBox.Invalidate();
                }
                else
                {
                    byte[] b = new byte[512];
                    hexBox.ByteProvider.InsertBytes(0, b);
                    filetype.Text = "Headered ROM";
                    hexBox.Invalidate();
                }
            }
            catch (Exception) { }
        }

        private void FormHexEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose(true);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFile();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            FormInsert F = new FormInsert();
            F.ShowDialog(this);
        }
    }
}