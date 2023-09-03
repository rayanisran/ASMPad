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
    public partial class ROMMap : DockContent
    {
        public ROMMap()
        {
            InitializeComponent();
        }

        private void ROMMap_Load(object sender, EventArgs e)
        {
            try
            {
                rompage.Navigate(Application.StartupPath + "\\ROM.htm");
            }
            catch (Exception) { }
        }


        private void rompage_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control == true && e.KeyValue == (char)Keys.F)
            {
                searchtxt.Focus();
                searchtxt.Select();
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

        private void searchtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                bool finder = FindNext(rompage, searchtxt.Text);
                if (!finder)
                {
                    MessageBox.Show("Did not find any (more) occurence(s) of the word " + searchtxt.Text, "No more matches.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void searchtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyValue == (char)Keys.A)
            {
                searchtxt.SelectAll();
            }
        }

        private void ROMMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
