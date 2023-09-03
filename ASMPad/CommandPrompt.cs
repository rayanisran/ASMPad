using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ASMPad
{
    public partial class CommandPrompt : DockContent
    {
        public delegate void UpdateOutputCallback(string text);
        private StreamWriter m_Writer;
        private bool m_Command;
        private readonly string initLocation;

        public CommandPrompt(string location)
        {
            Initialize();
            if(location != null)
                initLocation = "cd " + location;

            //DockContent.TabPageContextMenu
            //PopupEventHandler.PopupEventHandler(void (object, PopupEventArgs) target)
        }

        private void Initialize()
        {
            InitializeComponent();
            this.Load += new EventHandler(CommandPrompt_Load);
            this.TabText = "Output";
            this.m_Command = false;
            this.cmbInput.KeyDown += new KeyEventHandler(InputKeyPress);
            this.rtbOutput.GotFocus += new EventHandler(rtbOutput_GotFocus);
        }

        void rtbOutput_GotFocus(object sender, EventArgs e)
        {
            //this.cmbInput.Focus();
        }

        void CommandPrompt_Load(object sender, EventArgs e)
        {
            cs.AppendText("\r\nLoaded output window.");
            this.StartCommandPrompt();
            this.cmbInput.Focus();
            this.RunCommand(initLocation);
        }

        private void InputKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.RunCommand(this.cmbInput.Text);
                cs.AppendText("\r\nRan command " + cmbInput.Text + ".");
            }
        }

        private void StartCommandPrompt()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;

            cmd.OutputDataReceived += new DataReceivedEventHandler(HandleOutput);
            cmd.ErrorDataReceived += new DataReceivedEventHandler(HandleError);

            cmd.Start();
            this.m_Writer = cmd.StandardInput;
            cmd.BeginErrorReadLine();
            cmd.BeginOutputReadLine();
        }

        void HandleError(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
                this.rtbOutput.Invoke(new UpdateOutputCallback(this.UpdateError), new object[] { e.Data });
        }

        void HandleOutput(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
                this.rtbOutput.Invoke(new UpdateOutputCallback(this.UpdateOutput), new object[] { e.Data });
        }

        private void UpdateOutput(string text)
        {
            if (!this.m_Command && text.IndexOf(">cd") == text.Length - 3)
                return;

            int start = this.rtbOutput.TextLength;
            this.rtbOutput.AppendText(text + System.Environment.NewLine);

            if (this.m_Command)
            {
                this.rtbOutput.Select(start, text.Length);
                this.rtbOutput.SelectionColor = Color.Yellow;
                this.rtbOutput.Select(this.rtbOutput.TextLength, 0);
                this.m_Command = false;
            }

            this.rtbOutput.ScrollToCaret();
        }

        private void UpdateError(string text)
        {
            int start = this.rtbOutput.TextLength;
            this.rtbOutput.AppendText(text + System.Environment.NewLine);
            this.rtbOutput.Select(start, text.Length);
            this.rtbOutput.SelectionColor = Color.Red;
            this.rtbOutput.Select(this.rtbOutput.TextLength, 0);
            this.rtbOutput.ScrollToCaret();
        }

        public void RunScript(string script, string workingDirectory)
        {
            int index = workingDirectory.IndexOf(":\\");
            if (index > 0)
            {
                this.m_Writer.WriteLine(workingDirectory.Substring(0, index + 1));
            }
            this.m_Writer.WriteLine("cd " + workingDirectory);
            string[] commands = Regex.Split(script, System.Environment.NewLine);
            foreach (string command in commands)
            {
                if (!string.IsNullOrEmpty(command))
                    this.m_Writer.WriteLine(command);
            }
        }

        public void RunCommand(string command)
        {
            if (command != null)
            {
                this.m_Writer.WriteLine(command);
                this.m_Command = true;
                if (!this.cmbInput.Items.Contains(command))
                {
                    this.cmbInput.Items.Add(command);
                }
                this.cmbInput.Text = "";
                this.m_Writer.WriteLine("cd");
            }
        }

        private void cmbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyValue == (char)Keys.A)
                cmbInput.SelectAll();
        }

        private void CommandPrompt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void getHelpToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            ((Main)ParentForm).info.Text = "Get help for this error.";
        }

        private void getHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                int i = listView1.SelectedItems[0].Index;
                string s = listView1.Items[i].SubItems[1].Text;

                if (s.Contains("invalid opcode or command"))
                    TS.SetToolTip(listView1, "The following error means " +
                        s.Substring(s.IndexOf("[") + 1, s.Length - s.IndexOf("[") - 2) + @" is not a valid opcode or command which xkas can interpret. 
Perhaps you made a transcription error and meant to write something else instead, or have failed to use the proper syntax (e.g. forgetting a parameter).");

                else if (s.Contains("label") && s.Contains("not found"))
                    TS.SetToolTip(listView1, "The following error means the a label with the name " +
                        s.Substring(7, s.IndexOf("]") - s.IndexOf("[") - 1) + @" has been referenced which does not exist
in the current file. Adding a label with this name (" + s.Substring(7, s.IndexOf("]") - s.IndexOf("[") - 1) + ":) in a routine will fix this error.");

                else if (s.Contains("positive branch too long, exceeded bounds") || s.Contains("negative branch too long, exceeded bounds"))
                {
                    string label = s.Substring(s.IndexOf("[") + 1, s.Length - s.IndexOf("[") - 2);
                    TS.SetToolTip(listView1, @"The following errors means that in between a label reference and a label (e.g. BEQ Label and Label:), you have
too much code (or too many bytes) in between. For a BEQ/BCC/BNE/BCS/BMI/BPL, this is 128 bytes while for a JMP this is 8000 bytes. There are three ways to fix this:

1. Reduce the number of bytes in between the label reference and label. This may not be possible.
2. Use a JMP instead as it has a longer range.
3. Instead of writing " + label + @", write the opposite of the branching command (BNE for BEQ, BCS for BCC etc.) and then a JMP command, e.g.

BNE +" + Environment.NewLine + "JMP " + label.Substring(3) + @"
+

That will use JMP instead while still keeping the same code, giving a longer branching range.");
                }

                //define not declared (yet?)
                else if (s.Contains("define not declared (yet?)"))
                    TS.SetToolTip(listView1, @"The following error means that a definition (e.g. !Def) has been referenced on this line, but has not been declared (e.g. !Def = $40)
If it has been declared, then the definition must be on a line that comes before the reference. 
Otherwise, it can be fixed by removing the line which contains the referenced definition.");

                else if (s.Contains("macro declaration without matching endmacro tag"))
                    TS.SetToolTip(listView1, @"This line contains a macro, but the macro has not actually being 'closed' with an endmacro tag.
To fix this error, you must end the macro with the endmacro() keyword.");

                else if (s.Contains("invalid macro declaration"))
                    TS.SetToolTip(listView1, @"The following error means that you have incorrectly declared a macro. To insert a macro, press Alt+1 in the active document.");

                else if (s.Contains("broken macro argument"))
                    TS.SetToolTip(listView1, @"The following error means that you haven't setup macro arguments properly. The correct way would be e.g. macro _m (<arg1>, <arg2>, <arg3>).");
      
                else
                    TS.SetToolTip(listView1, "Sorry, no help exists for this error. Perhaps you should ask on the SMWC Forums instead.");
            }
        }

        private void listView1_MouseEnter(object sender, EventArgs e)
        {
            if (listView1.Items.Count < 1)
                listView1.ContextMenuStrip = null;
            else
                listView1.ContextMenuStrip = contextMenuStrip1;
        }

        private void copyErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetDataObject(listView1.Items[listView1.SelectedItems[0].Index].SubItems[1].Text, true);
        }

        private void goToLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Main)ParentForm).dockPanel1.ActiveDocument.DockHandler.Activate();
            int selectednum = int.Parse(listView1.FocusedItem.Text);
            ((Main)ParentForm).ActiveDocument.Scintilla.GoTo.Line(selectednum - 1); // We count the 0.
        }
    }
}
