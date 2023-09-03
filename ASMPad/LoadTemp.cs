using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using ASMPad;
using ASMPad.Properties;
 
public partial class LoadTemp : Form
{
  string[] _files;
  string x;
  public string dir = Application.StartupPath + @"\templates\";
  public LoadTemp()
  {
    InitializeComponent();
  }

  private void loadtemplates_Load(object sender, EventArgs e)
  {
      //If the directory, doesn't exist, create it.
      if (!Directory.Exists(dir))
      {
          MessageBox.Show("The directory " + dir + " does not exist, creating it now.", "Creating Directory.", MessageBoxButtons.OK, MessageBoxIcon.Information);
          Directory.CreateDirectory(dir);
      }

      fileSystemWatcher1.Path = dir;

      //Now we'll add the files one by one.
      //And only .asm files.

      _files = Directory.GetFiles(dir);
      for (int i = 0; i < _files.Length; i++)
      {
          x = Path.GetFileName(_files[i]);
          if (x.EndsWith(".asm"))
          {
              if (!listBox1.Items.Contains(x))
                  listBox1.Items.Add(x);
          }
      }
  }

  private void button2_Click(object sender, EventArgs e)
  {
      Close();
  }

  private void button1_Click(object sender, EventArgs e)
  {
      _close();
  }

  private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
  {
      try { textBox1.Text = File.ReadAllText(Path.GetFullPath(dir + listBox1.SelectedItem)).Replace("\n", Environment.NewLine);}
      catch (Exception) { }
  }

  private void _close()
  {
      if (listBox1.SelectedIndex == -1)
          Close();
      else
      {
          string n = listBox1.SelectedItem.ToString();
          if (File.Exists(dir + n))
          {
              (Owner as Main)._getdata(File.ReadAllText(dir + n));
              Close();
              (Owner as Main).LG.cs.AppendText("\r\nLoaded template " + n + "..");
          }
          else
          {
              MessageBox.Show("File doesn't exist, removing it.", "Does not exist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
              listBox1.Items.Remove(listBox1.SelectedItem);
              SendKeys.Send("{UP}");
          }
      }
  }

  private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
  {
      _close();
  }

  private void listBox1_KeyDown(object sender, KeyEventArgs e)
  {
      if (e.KeyData == Keys.Delete)
      {
          DialogResult dr = MessageBox.Show("Delete this template?", "Delete template", MessageBoxButtons.YesNo);
          if (dr == DialogResult.Yes)
          {
                  string n = listBox1.SelectedItem.ToString();
                  File.Delete(dir + n);
                  listBox1.Items.Remove(listBox1.SelectedItem);
                  SendKeys.Send("{UP}");
          }
      }
  }

  private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
  {
      for (int i = 0; i < listBox1.Items.Count; i++)
          if (listBox1.Items[i].ToString() == e.Name)
              listBox1.Items.RemoveAt(i);
  }

  private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
  {
      listBox1.Items.Add(e.Name);
  }

  private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
  {
      for (int i = 0; i < listBox1.Items.Count; i++)
          if (listBox1.Items[i].ToString() == e.OldName)
          {
              listBox1.Items.RemoveAt(i);
              listBox1.Items.Add(e.Name);
          }
  }
}