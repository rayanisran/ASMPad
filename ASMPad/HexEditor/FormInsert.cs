using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace ASMPad
{
    public partial class FormInsert : Form
    {
        public FormInsert()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            hexBox.ByteProvider = new DynamicByteProvider(new ByteCollection());
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (getbyte().Length == 0)
                DialogResult = DialogResult.Cancel;
            else if (radioButton2.Checked && textBox1.Text.Length == 0)
                DialogResult = DialogResult.Cancel;
            else if (radioButton3.Checked && textBox2.Text.Length == 0)
                DialogResult = DialogResult.Cancel;
            else
                DialogResult = DialogResult.OK;
        }

        private void hexBox_Enter(object sender, EventArgs e)
        {
            hexBox.Focus();
        }

        private void FormInsert_Load(object sender, EventArgs e)
        {
            //if (radioButton2.Checked)
            //    textBox1.SelectAll();
            //if (radioButton3.Checked)
            //    textBox2.SelectAll();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public byte[] getbyte()
        {
            return ((DynamicByteProvider)hexBox.ByteProvider).Bytes.GetBytes();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("$0123456789ABCDEFabcdef\x08".IndexOf(e.KeyChar) == -1)
                e.Handled = true;
        }

        private void textBox1_KeyPres(object sender, KeyPressEventArgs e)
        {
            if ("^x0123456789ABCDEFabcdef\x08".IndexOf(e.KeyChar) == -1)
                e.Handled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Select();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Select();
        }
    }
}
