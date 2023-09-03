using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ASMPad
{
    public partial class PatchDialog : Form
    {
        public PatchDialog()
        {
            InitializeComponent();
            Start.Checked = true;
        }

        private void noBytes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("4567\x08".IndexOf(e.KeyChar) == -1)
                e.Handled = true;
            //normally you'd only use 4-6. (unless you're stupid)
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int length = hackSpot.Text.Length;
            if (!hackSpot.Text.StartsWith("$"))
                MessageBox.Show("Routine should begin with $", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (hackSpot.Text == string.Empty)
                MessageBox.Show("No routine specified to hack.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (noBytes.Text == string.Empty)
                MessageBox.Show("Number of bytes not specified.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            StringBuilder pc = new StringBuilder();

            int nopnop;
            nopnop = int.Parse(noBytes.Text) - 4;

            pc.AppendLine("; Definitions");
            pc.AppendLine("!Freespace = $128000");
            pc.AppendLine("");
            pc.AppendLine("; ======================================================");
            pc.AppendLine("; " + pjName.Text);
            pc.AppendLine("; ======================================================");
            pc.AppendLine("");
            pc.AppendLine("Header");
            pc.AppendLine("LoROM");
            pc.AppendLine("");
            pc.AppendLine("; ======================================================");
            pc.AppendLine("; Patch Code + RATS Tag");
            pc.AppendLine("; ======================================================");
            pc.AppendLine("");
            pc.AppendLine("org " + hackSpot.Text);
            pc.AppendLine("JSL MainCode");

            //Add NOPs if necessary..
            if (int.Parse(noBytes.Text) > 4)
                pc.AppendLine("NOP #" + nopnop.ToString());

            pc.AppendLine(";===========================");
            pc.AppendLine("org !Freespace");
            pc.AppendLine("!CodeSize = Ending-Routine  ");
            pc.AppendLine(@"db ""STAR""");
            pc.AppendLine("dw !CodeSize-$01");
            pc.AppendLine("dw !CodeSize-$01^$FFFF");
            pc.AppendLine("Routine:");
            pc.AppendLine(";===========================");
            pc.AppendLine("MainCode:");

            if (Start.Checked)
            {
                pc.AppendLine("");
                pc.AppendLine(oldCode.Text);
                pc.AppendLine("");
                pc.AppendLine(";====================");
                pc.AppendLine(";Your Code Goes Here ");
                pc.AppendLine(";====================");
            }
            else
            {
                pc.AppendLine("");
                pc.AppendLine(";====================;");
                pc.AppendLine(";Your Code Goes Here ;");
                pc.AppendLine(";====================;");
                pc.AppendLine("");
                pc.AppendLine(oldCode.Text);
            }
            pc.AppendLine("Ending:");
            (Owner as Main).LG.cs.AppendText("\r\nCreated new patch..");
            (Owner as Main)._getdata(pc.ToString());

            Close();
        }


        private void hackSpot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("^$0123456789ABCDEFabcdef\x08".IndexOf(e.KeyChar) == -1)
                e.Handled = true;
        }

        private void hackSpot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V))
                (sender as TextBox).Paste();
            if (e.KeyData == (Keys.Control | Keys.A))
                (sender as TextBox).SelectAll();
            if (e.KeyData == (Keys.Control | Keys.C))
                (sender as TextBox).Copy();
            if (e.KeyData == (Keys.Control | Keys.Z))
                (sender as TextBox).Undo();
            if (e.KeyData == (Keys.Control | Keys.X))
                (sender as TextBox).Cut();
        }

        private void generic_SelectedIndexChanged(object sender, EventArgs e)
        {
            //I've added generic routines in the combo-box. You can add your own and more
            //if you like. These are the most commons one.

            if (generic.SelectedIndex == 0)
            {
                hackSpot.Text = "$008DC4";
                noBytes.Text = "5";
                oldCode.Text = "LDA #$02\r\nSTA $420B";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 1)
            {
                hackSpot.Text = "$008F25";
                noBytes.Text = "6";
                oldCode.Text = "INC $0DBF\r\nLDA $0DBF";
                End.Checked = true;
            }
            else if (generic.SelectedIndex == 2)
            {
                hackSpot.Text = "$008F55";
                noBytes.Text = "6";
                oldCode.Text = "STX $0F16\r\nSTA $0F17";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 3)
            {
                hackSpot.Text = "$008F7E";
                noBytes.Text = "6";
                oldCode.Text = "STX $0F13\r\nSTA $0F14";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 4)
            {
                hackSpot.Text = "$009E2C";
                noBytes.Text = "6";
                oldCode.Text = "STA $0DBE\r\nSTZ $0DBF";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 5)
            {
                hackSpot.Text = "$00F2E0";
                noBytes.Text = "4";
                oldCode.Text = "LDA $19\r\nBNE $04";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 6)
            {
                hackSpot.Text = "$00F5B3";
                noBytes.Text = "5";
                oldCode.Text = "LDY #$04\r\nSTY $1DF9";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 7)
            {
                hackSpot.Text = "$00F606";
                noBytes.Text = "4";
                oldCode.Text = "LDA #$90\r\nSTA $7D";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 8)
            {
                hackSpot.Text = "$00FEA8";
                noBytes.Text = "5";
                oldCode.Text = "LDX #$09\r\nLDA $170B,x";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 9)
            {
                hackSpot.Text = "$01C561";
                noBytes.Text = "4";
                oldCode.Text = "LDA #$02\r\nSTA $71";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 10)
            {
                hackSpot.Text = "$01C580";
                noBytes.Text = "4";
                oldCode.Text = "LDA #$FF\r\nSTA $1490";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 11)
            {
                hackSpot.Text = "$01C598";
                noBytes.Text = "4";
                oldCode.Text = "LDA #$02\r\nSTA $19";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 12)
            {
                hackSpot.Text = "$01C5EC";
                noBytes.Text = "5";
                oldCode.Text = "LDA #$20\r\nSTA $149B";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 13)
            {
                hackSpot.Text = "$0485BB";
                noBytes.Text = "5";
                oldCode.Text = "LDA #$08\r\nSTA $007B";
                Start.Checked = true;
            }
            else if (generic.SelectedIndex == 14)
            {
                hackSpot.Text = "$0491DB";
                noBytes.Text = "5";
                oldCode.Text = "LDA #$02\r\nSTA $0DB1";
                Start.Checked = true;
            }

        }

        private void select_CheckedChanged_1(object sender, EventArgs e)
        {
            generic.Enabled = select.Checked;
        }
    }
}
