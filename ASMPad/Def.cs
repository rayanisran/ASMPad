using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ASMPad.Properties;

namespace ASMPad
{
    public partial class Def : Form
    {
        public Def()
        {
            InitializeComponent();
        }

        private void Def_Load(object sender, EventArgs e)
        {
            textBox1.Text = Settings.Default.listdef;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Write to string.
            Settings.Default.listdef = textBox1.Text;
            Settings.Default.Save();
        }
    }
}
