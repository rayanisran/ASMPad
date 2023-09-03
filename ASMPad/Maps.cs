using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using mshtml;

namespace ASMPad
{
    public partial class Maps : DockContent
    {
        public Maps()
        {
            InitializeComponent();
        }

        private void Maps_Load(object sender, EventArgs e)
        {
            try
            {
                map.Navigate(Application.StartupPath + "\\ram.htm");
            }
            catch (Exception) { }
        }

        private void searchtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                bool finder = FindNext(map, searchtxt.Text);
                if (!finder)
                {
                    MessageBox.Show("Did not find any (more) occurence(s) of the word " + searchtxt.Text, "No more matches.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    finder = true;
                }
            }
        }

        private bool FindNext(WebBrowser webBrowser, string text)
        {
            IHTMLDocument2 doc = (IHTMLDocument2)webBrowser.Document.DomDocument;
            IHTMLSelectionObject sel = (IHTMLSelectionObject)doc.selection;
            IHTMLTxtRange rng = (IHTMLTxtRange)sel.createRange();
            rng.collapse(false); // collapse the current selection so we start from the end of the previous range
            if (rng.findText(text, 220000, 0))
            {
                rng.select();
                return true;
            }
            else
                return false;
        }

        private void rampage_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control == true && e.KeyValue == (char)Keys.F)
            {
                searchtxt.Focus();
                searchtxt.Select();
            }
        }

        private void searchtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyValue == (char)Keys.A)
            {
                searchtxt.SelectAll();
            }
        }

        private void Maps_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
