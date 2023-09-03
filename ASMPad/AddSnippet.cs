using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace ASMPad
{
    public partial class AddSnippet : Form
    {
        public AddSnippet()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "Edit Snippet")
                {
                    //First we delete the old one.

                    XDocument doc = XDocument.Load(Application.StartupPath + "\\Snippets.xml");
                    doc.Descendants("Snippet").Where(xe => xe.Attribute("name") != null && xe.Attribute("name").Value == name.Text).SingleOrDefault().Remove();
                    doc.Save(Application.StartupPath + "\\Snippets.xml");

                    string p = File.ReadAllText(Application.StartupPath + "\\Snippets.xml");
                    p = p.Replace("</Snippets>", string.Empty);
                    p += (Environment.NewLine + "<Snippet name=" + @"""" + name.Text + @""">" + Environment.NewLine + "<SnippetCode>" + code.Text + Environment.NewLine + "</SnippetCode>" + Environment.NewLine + "</Snippet>" + Environment.NewLine + "</Snippets>");
                    File.WriteAllText(Application.StartupPath + "\\Snippets.XML", p);
                    (Owner as Main).LG.cs.AppendText("\r\nEdited snippet " + name.Text + ".");
                }
                else
                {
                    //Don't add the snippet if it's already there.
                    string p = File.ReadAllText(Application.StartupPath + "\\Snippets.xml");

                    if (p.Contains(@"<Snippet name=""" + name.Text + @""">"))
                    {
                        MessageBox.Show("Error: The snippets list already contains a snippet called " + name.Text + ", choose another name.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        name.SelectAll();
                        return;
                    }

                    p = p.Replace("</Snippets>", string.Empty);
                    p += (Environment.NewLine + "<Snippet name=" + @"""" + name.Text + @""">" + Environment.NewLine + "<SnippetCode>" + code.Text + Environment.NewLine + "</SnippetCode>" + Environment.NewLine + "</Snippet>" + Environment.NewLine + "</Snippets>");
                    File.WriteAllText(Application.StartupPath + "\\Snippets.XML", p);
                }
                this.Close();
            }
            catch (Exception) { MessageBox.Show("Adding snippet failed. Check if the file exists and is in a proper format.", "Failed to add snippet.", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void AddSnippet_Load(object sender, EventArgs e)
        {
            if (button1.Text == "Add Snippet")
                name.Select();
            else
                code.Select();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringReader str = new StringReader(code.Text);
            StringBuilder s = new StringBuilder();
            do
            {
                string args = str.ReadLine();
                s.AppendLine("\t" + args.Trim());
            }
            while (str.Peek() != -1);
            str.Close();
                
            code.Text = s.ToString();
            SendKeys.Send("{BACKSPACE}");
        }
    }
}
