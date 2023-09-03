using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ASMPad.Properties;

namespace ASMPad
{
    public partial class EmulatorPath : Form
    {
        public EmulatorPath()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Browser(ep, "emulator");
        }

        private void Browser(TextBox txt, string emulator)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open " + emulator + " file";
            ofd.Filter = "Executable (*.exe)|*.exe|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txt.Text = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Browser(dp, "debugger");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (Owner as Main).LG.cs.AppendText("\r\nConfigured emulator and debugger path.");
            Settings.Default._empath = ep.Text;
            Settings.Default.debugpath = dp.Text;
            Settings.Default.Save();
            Close();
        }

        private void EmulatorPath_Load(object sender, EventArgs e)
        {
            ep.Text = Settings.Default._empath;
            dp.Text = Settings.Default.debugpath;
        }
    }
}
