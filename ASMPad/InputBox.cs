using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml.Linq;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System.Resources;

namespace ASMPad
{
    public partial class InputBox : DockContent
    {
        #region Fields
        //For poses.
        public List<string> list = new List<string>();
        public List<string> nums = new List<string>();

        //For sound effects.
        public List<string> txt = new List<string>();
        public List<string> bank = new List<string>();
        public List<string> num = new List<string>();

        //YXPPCCCT setter.
        public byte _sbyte;
        public byte _lbyte;
        public int x = 0;
        public int value = 0;
        #endregion

        public InputBox()
        {
            InitializeComponent();
            posenum.Checked = soundnum.Checked = true;
            _inputbox.Text = "00";
            inbox.Text = "$1DFA 00";
        }

        #region Searching opcodes, SFX and poses.
        private void _inputbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    string currentline = string.Empty;
                    Assembly _assembly = Assembly.GetExecutingAssembly();
                    TextReader _reader = new StreamReader(_assembly.GetManifestResourceStream("ASMPad.Resources.finder.txt"));
                    string reader = _reader.ReadToEnd();

                    reader = reader.Substring(reader.IndexOf("POSES") + 7, 8740).ToLower();
                    StringReader str = new StringReader(reader);

                    string s = _inputbox.Text.ToLower();
                    if (s.Length == 1)
                        s = s.PadLeft(2, '0');
                    do
                    {
                        currentline = str.ReadLine();
                        if (currentline.Contains(s))
                        {
                            list.Add(currentline.Substring(5));
                            nums.Add(currentline.Substring(0, 2));
                        }
                    }
                    while (str.Peek() != -1);
                    str.Close();
                    for (int i = 0; i < list.Count; i++)
                        _outbox.Text = _outbox.Text + nums[i] + ": " + list[i] + "\r\n";
                    if (posenum.Checked)
                        _outbox.Text = _outbox.Text.Substring(_outbox.Text.LastIndexOf(":") - 2);
                }
                catch (Exception) { }
            }
        }

        private void inbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            list.Clear();
            nums.Clear();
            _outbox.Text = string.Empty;

            if (e.KeyChar == (char)Keys.Enter)
            {
                string currentline = string.Empty;
                try
                {
                    //FOR THE NUMBERS
                    Assembly _assembly = Assembly.GetExecutingAssembly();
                    TextReader _reader = new StreamReader(_assembly.GetManifestResourceStream("ASMPad.Resources.finder.txt"));
                    string reader = _reader.ReadToEnd();
                    reader = reader.Substring(reader.IndexOf("SOUNDS") + 7, reader.IndexOf("OPERANDHELL") - reader.IndexOf("SOUNDS") + 7).ToLower();

                    if (soundnum.Checked)
                    {
                        switch (inbox.Text.Substring(0, 5))
                        {
                            case "$1DF9":
                                reader = reader.Substring(reader.IndexOf("$1DF9") + 7, 1300);
                                break;
                            case "$1DFA":
                                reader = reader.Substring(reader.IndexOf("$1DFA") + 7, 125);
                                break;
                            case "$1DFB":
                                reader = reader.Substring(reader.IndexOf("$1DFB") + 7, 582);
                                break;
                            case "$1DFC":
                                reader = reader.Substring(reader.IndexOf("$1DFC"));
                                break;
                        }

                        StringReader str = new StringReader(reader);
                        do
                        {
                            currentline = str.ReadLine();
                            if (-1 != currentline.IndexOf(inbox.Text.Substring(5)) || -1 != currentline.IndexOf(inbox.Text.Substring(6)))
                                result.Text = currentline.Substring(3);
                        }
                        while (str.Peek() != -1);
                        str.Close();
                    }

                    //FOR THE KEYWORDS
                    else
                    {
                        reader = reader.Substring(reader.IndexOf("$1DF9") + 9).ToLower();
                        StringReader str = new StringReader(reader);
                        string currentbank = "$1DF9";
                        do
                        {
                            currentline = str.ReadLine();
                            string s = inbox.Text.ToLower();

                            if (currentline.Contains(s)) // Find a match.
                            {
                                txt.Add(currentline.Substring(3));
                                num.Add(currentline.Substring(0, 2));
                                bank.Add(currentbank);
                            }

                            if (currentline.Contains("--$1df9--"))
                                currentbank = "$1DF9";
                            else if (currentline.Contains("--$1dfa--"))
                                currentbank = "$1DFC";
                            else if (currentline.Contains("--$1dfb--"))
                                currentbank = "$1DFB";
                            else if (currentline.Contains("--$1dfc--"))
                                currentbank = "$1DFA";
                        }
                        while (str.Peek() != -1);
                        str.Close();

                        for (int i = 0; i < txt.Count; i++)
                            result.Text = result.Text + bank[i] + ": " + num[i] + ": " + txt[i] + "\r\n";
                    }
                    if (result.Text == string.Empty)
                        result.Text = "No matches found.";
                }
                catch (Exception) { result.Text = "Invalid input or sound effect not found."; }
            }
        }

        private void inbox_TextChanged(object sender, EventArgs e)
        {
            txt.Clear();
            num.Clear();
            bank.Clear();
            result.Text = string.Empty;
        }

        private void _inputbox_TextChanged(object sender, EventArgs e)
        {
            list.Clear();
            nums.Clear();
            _outbox.Text = string.Empty;
        }
        string currentline = string.Empty;

        private void opscode1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    Assembly _assembly = Assembly.GetExecutingAssembly();
                    TextReader _reader = new StreamReader(_assembly.GetManifestResourceStream("ASMPad.Resources.finder.txt"));
                    string reader = _reader.ReadToEnd();

                    StringReader str = new StringReader(reader);
                    do
                    {
                        currentline = str.ReadLine();
                        string s = currentline.Substring(0, currentline.IndexOf("{"));
                        string opcode_l = s.Substring(7);
                        if (-1 != currentline.IndexOf(opcode1.Text) && opcode_l == opcode1.Text)
                        {
                            cycles1.Text = currentline.Substring(3, 1);
                            bytes1.Text = currentline.Substring(5, 1);
                            hex1.Text = currentline.Substring(0, 2);
                        }
                    }
                    while (str.Peek() != -1);
                    str.Close();
                }
                catch (Exception)
                { } 
            }
        }

        private void hex1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Assembly _assembly = Assembly.GetExecutingAssembly();
                TextReader _reader = new StreamReader(_assembly.GetManifestResourceStream("ASMPad.Resources.finder.txt"));
                string reader = _reader.ReadToEnd();

                reader = reader.Substring(0, reader.IndexOf("POSES"));
                StringReader str = new StringReader(reader);
                do
                {
                    currentline = str.ReadLine();
                    if (hex1.TextLength < 2)
                        return;
                    hex1.Text = hex1.Text.ToUpper();
                    if (currentline.StartsWith(hex1.Text))
                    {
                        cycles1.Text = currentline.Substring(3, 1);
                        bytes1.Text = currentline.Substring(5, 1);
                        string s = currentline.Substring(0, currentline.IndexOf("{"));
                        opcode1.Text = s.Substring(7);
                        op.Text = currentline.Substring(currentline.IndexOf("{") + 1);
                        if (op.Text == string.Empty)
                            op.Text = "No description added.";
                    }
                }
                while (str.Peek() != -1);
                str.Close();
            }
            catch (Exception) { }
        }

        private void hex1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("0123456789abcdefABCDEF\x08".IndexOf(e.KeyChar) == -1)
                e.Handled = true;
        }
        #endregion

        #region Converter
        private void ascii_TextChanged(object sender, EventArgs e)
        {
            converted.Text = Converttxt(ascii.Text);
            gfx28.Text = Convertgfx28(ascii.Text);
        }

        string Convertgfx28(string s)
        {
            string r = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= '0' && s[i] <= '9')
                    r += "$0" + s[i].ToString() + ",";
                else if (s[i] >= 'A' && s[i] <= 'F')
                    r += "$0" + s[i].ToString() + ",";
                else if (s[i] >= 'a' && s[i] <= 'f')
                    r += "$0" + ((char)(s[i] - 'a' + 'A')).ToString() + ",";
                else if (s[i] >= 'G' && s[i] <= 'Z')
                    r += "$" + ((byte)(s[i] - 'A' + 10)).ToString("X") + ",";
                else if (s[i] >= 'g' && s[i] <= 'z')
                    r += "$" + ((byte)(s[i] - 'a' + 10)).ToString("X") + ",";
                else if (s[i] == '.')
                    r += "$24,";
                else if (s[i] == ',')
                    r += "$25,";
                else if (s[i] == '*')
                    r += "$26,";
                else if (s[i] == '-')
                    r += "$27,";
                else if (s[i] == '!')
                    r += "$28,";
                else if (s[i] == '=')
                    r += "$77,";
                else if (s[i] == ':')
                    r += "$78,";
                else if (s[i] == '\'')
                    r += "$85,";
                else if (s[i] == '\"')
                    r += "$86,";
                else if (s[i] == ' ')
                    r += "$FC,";
                else if (s[i] == '\\')
                {
                    if (s.Length > i + 2)
                    {
                        i++;
                        byte a = 0;
                        if (s[i] >= '0' && s[i] <= '9')
                            a = (byte)((s[i] - '0') << 4);
                        else if (s[i] >= 'A' && s[i] <= 'F')
                            a = (byte)((s[i] - 'A' + 10) << 4);
                        else if (s[i] >= 'a' && s[i] <= 'f')
                            a = (byte)((s[i] - 'a' + 10) << 4);
                        else
                            return "";
                        i++;
                        if (s[i] >= '0' && s[i] <= '9')
                            a |= (byte)(s[i] - '0');
                        else if (s[i] >= 'A' && s[i] <= 'F')
                            a |= (byte)(s[i] - 'A' + 10);
                        else if (s[i] >= 'a' && s[i] <= 'f')
                            a |= (byte)(s[i] - 'a' + 10);
                        else
                            return "";
                        r += "$" + (a < 0x10 ? "0" : "") + a.ToString("X") + ",";
                    }
                    else
                        return "";
                }
                else
                    return "";
            }
            if (r.Length > 0)
            {
                r = r.Remove(r.Length - 1);
                return "db " + r + "      ;" + s;
            }
            else
                return "";
        }

        //GFX2A.bin
        string Converttxt(string s)
        {
            string r = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0')
                    r += "$22,";
                else if (s[i] == '1')
                    r += "$23,";
                else if (s[i] == '2')
                    r += "$24,";
                else if (s[i] == '3')
                    r += "$25,";
                else if (s[i] == '4')
                    r += "$26,";
                else if (s[i] == '5')
                    r += "$27,";
                else if (s[i] == '6')
                    r += "$28,";
                else if (s[i] == '7')
                    r += "$29,";
                else if (s[i] == ' ')
                    r += "$1F,";
                else if (s[i] == '\'')
                    r += "$5D,";
                else if (s[i] == '8')
                    r += "$2A,";
                else if (s[i] == '9')
                    r += "$2B,";
                else if (s[i] == 'A')
                    r += "$00,";
                else if (s[i] == 'B')
                    r += "$01,";
                else if (s[i] == 'C')
                    r += "$02,";
                else if (s[i] == 'D')
                    r += "$03,";
                else if (s[i] == 'E')
                    r += "$04,";
                else if (s[i] == 'F')
                    r += "$05,";
                else if (s[i] == 'G')
                    r += "$06,";
                else if (s[i] == 'H')
                    r += "$07,";
                else if (s[i] == 'I')
                    r += "$08,";
                else if (s[i] == 'J')
                    r += "$09,";
                else if (s[i] == 'K')
                    r += "$0A,";
                else if (s[i] == 'L')
                    r += "$0B,";
                else if (s[i] == 'M')
                    r += "$0C,";
                else if (s[i] == 'N')
                    r += "$0D,";
                else if (s[i] == 'O')
                    r += "$0E,";
                else if (s[i] == 'P')
                    r += "$0F,";
                else if (s[i] == 'Q')
                    r += "$10,";
                else if (s[i] == 'R')
                    r += "$11,";
                else if (s[i] == 'S')
                    r += "$12,";
                else if (s[i] == 'T')
                    r += "$13,";
                else if (s[i] == 'U')
                    r += "$14,";
                else if (s[i] == 'V')
                    r += "$15,";
                else if (s[i] == 'W')
                    r += "$16,";
                else if (s[i] == 'X')
                    r += "$17,";
                else if (s[i] == 'Y')
                    r += "$18,";
                else if (s[i] == 'Z')
                    r += "$19,";
                else if (s[i] == 'a')
                    r += "$40,";
                else if (s[i] == 'b')
                    r += "$41,";
                else if (s[i] == 'c')
                    r += "$42,";
                else if (s[i] == 'd')
                    r += "$43,";
                else if (s[i] == 'e')
                    r += "$44,";
                else if (s[i] == 'f')
                    r += "$45,";
                else if (s[i] == 'g')
                    r += "$46,";
                else if (s[i] == 'h')
                    r += "$47,";
                else if (s[i] == 'i')
                    r += "$48,";
                else if (s[i] == 'j')
                    r += "$49,";
                else if (s[i] == 'k')
                    r += "$4A,";
                else if (s[i] == 'l')
                    r += "$4B,";
                else if (s[i] == 'm')
                    r += "$4C,";
                else if (s[i] == 'n')
                    r += "$4D,";
                else if (s[i] == 'o')
                    r += "$4E,";
                else if (s[i] == 'p')
                    r += "$4F,";
                else if (s[i] == 'q')
                    r += "$50,";
                else if (s[i] == 'r')
                    r += "$51,";
                else if (s[i] == 's')
                    r += "$52,";
                else if (s[i] == 't')
                    r += "$53,";
                else if (s[i] == 'u')
                    r += "$54,";
                else if (s[i] == 'v')
                    r += "$55,";
                else if (s[i] == 'w')
                    r += "$56,";
                else if (s[i] == 'x')
                    r += "$57,";
                else if (s[i] == 'y')
                    r += "$58,";
                else if (s[i] == 'z')
                    r += "$59,";
                else if (s[i] == '.')
                    r += "$1B,";
                else if (s[i] == ',')
                    r += "$1D,";
                else if (s[i] == '?')
                    r += "$1E,";
                else if (s[i] == '-')
                    r += "$1C,";
                else if (s[i] == '!')
                    r += "$28,";
                else if (s[i] == '=')
                    r += "$77,";
                else if (s[i] == '#')
                    r += "$5A,";
                else if (s[i] == '(')
                    r += "$5B,";
                else if (s[i] == ')')
                    r += "$5C,";
                else if (s[i] == '\\')
                {
                    if (s.Length > i + 2)
                    {
                        i++;
                        byte a = 0;
                        if (s[i] >= '0' && s[i] <= '9')
                            a = (byte)((s[i] - '0') << 4);
                        else if (s[i] >= 'A' && s[i] <= 'F')
                            a = (byte)((s[i] - 'A' + 10) << 4);
                        else if (s[i] >= 'a' && s[i] <= 'f')
                            a = (byte)((s[i] - 'a' + 10) << 4);
                        else
                            return "";
                        i++;
                        if (s[i] >= '0' && s[i] <= '9')
                            a |= (byte)(s[i] - '0');
                        else if (s[i] >= 'A' && s[i] <= 'F')
                            a |= (byte)(s[i] - 'A' + 10);
                        else if (s[i] >= 'a' && s[i] <= 'f')
                            a |= (byte)(s[i] - 'a' + 10);
                        else
                            return "";
                        r += "$" + (a < 0x10 ? "0" : "") + a.ToString("X") + ",";
                    }
                    else
                        return "";
                }
                else
                    return "";
            }
            if (r.Length > 0)
            {
                r = r.Remove(r.Length - 1);
                return "db " + r + "      ;" + s;
            }
            else
                return "";
        }
        #endregion

        #region Calculator
        private void mathin_KeyPress(object sender, KeyPressEventArgs e)
        {
            //We'll build the code from the CSharp Code Provider, which explains why it doesn't run fast.
            //I guess I could have used a Math parser instead though.
            //Also we need to filter out all commands except the math one.
            if (e.KeyChar == (char)Keys.Enter)
            {
                    string _exp = System.Text.RegularExpressions.Regex.Replace(mathin.Text, @"\b\p{L}", m => "Math." + m.Value.ToUpper());
                    if (ProcessCommand(_exp) != -1)
                    {
                        mathout.Text = ProcessCommand(_exp).ToString();
                        hexcmd.Text = "$" + int.Parse(mathout.Text).ToString("X").PadLeft(4, '0');
                        bincmd.Text = "%" + Convert.ToString(int.Parse(mathout.Text), 2).PadLeft(8, '0');
                    }
            }
        }

        CSharpCodeProvider myCodeProvider = new CSharpCodeProvider();
        private double ProcessCommand(string command)
        {
            CompilerParameters cp = new CompilerParameters();
            cp.GenerateExecutable = false;  
            //The class.
            string TempModuleSource = "namespace ns{" + "using System;" + "class class1{" + "public static double Evaluate(){return " + command + ";}}} ";

            CompilerResults cr = myCodeProvider.CompileAssemblyFromSource(cp, TempModuleSource);
            if (cr.Errors.Count > 0)
            {
                MessageBox.Show("Expression cannot be calculated, please ucheck if the syntax is correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            else
            {
            MethodInfo Methinfo = cr.CompiledAssembly.GetType("ns.class1").GetMethod("Evaluate");
            return (double)Methinfo.Invoke(null, null);
            }
        }
        #endregion

        #region Property Setter
        private void decbox_TextChanged(object sender, EventArgs e)
        {
            value = int.Parse(decbox.Text);
            hexbox.Text = "$" + value.ToString("X").PadLeft(4, '0');
            binbox.Text = "%" + Convert.ToString(value, 2).PadLeft(8, '0');
        }

        private void palbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            x = Convert.ToInt32(_sbyte);
            x &= ~2;
            x &= ~4;
            x &= ~8; 
            _sbyte = Convert.ToByte(x);
            int index = 0;
            index = palbox.SelectedIndex * 2;
            _sbyte |= Convert.ToByte(index);
            decbox.Text = _sbyte.ToString();
        }

        private void yflip_CheckedChanged(object sender, EventArgs e){ _sbyte ^= 128; decbox.Text = _sbyte.ToString();}
        private void xflip_CheckedChanged(object sender, EventArgs e) {_sbyte ^= 64; decbox.Text = _sbyte.ToString(); }
        private void lowest_CheckedChanged(object sender, EventArgs e) { AddBit(0); }
        private void low_CheckedChanged(object sender, EventArgs e){ AddBit(1); }
        private void high_CheckedChanged(object sender, EventArgs e) { AddBit(2); }
        private void highest_CheckedChanged(object sender, EventArgs e) { AddBit(3); }
        
        //This will change the PP bit as required.
        private void AddBit(int num)
        {
            x = Convert.ToInt32(_sbyte);
            x &= ~16;
            x &= ~32;
            _sbyte = Convert.ToByte(x);
            _sbyte ^= (byte)(num * 16);
            decbox.Text = _sbyte.ToString();
        }


        private void tbit_CheckedChanged(object sender, EventArgs e)
        { _sbyte ^= 1;decbox.Text = _sbyte.ToString(); }
        #endregion

        #region Colors
        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            {
                colorDlg.Color = panel1.BackColor;
                colorDlg.FullOpen = true;
                if (colorDlg.ShowDialog() == DialogResult.OK)
                {
                    //HDMA colors.
                    int hdmared = ((colorDlg.Color.R / 0x08) + 0x20);
                    int hdmagreen = ((colorDlg.Color.G / 0x08) + 0x40);
                    int hdmablue = ((colorDlg.Color.B / 0x08) + 0x80);

                    snesred.Text = hdmared.ToString("X");
                    snesgreen.Text = hdmagreen.ToString("X");
                    snesblue.Text = hdmablue.ToString("X");

                    //Panel Color.
                    panel1.BackColor = colorDlg.Color;

                    //PC RBG
                    pcblue.Text = colorDlg.Color.B.ToString();
                    pcred.Text = colorDlg.Color.R.ToString();
                    pcgreen.Text = colorDlg.Color.G.ToString();

                    string _pccol = ScintillaNet.Utilities.ColorToHtml(colorDlg.Color).ToString().Substring(1);
                    pccol.Text = _pccol;
                    int num = Int32.Parse(_pccol, System.Globalization.NumberStyles.HexNumber);
                    snescol.Text = PC2SNES(num).ToString("X");
                    if (snescol.Text.Length == 3)
                        snescol.Text = '0' + snescol.Text;
                    else if (snescol.Text.Length == 2)
                        snescol.Text = "00" + snescol.Text;
                    else if (snescol.Text.Length == 1)
                        snescol.Text = "000" + snescol.Text;
                }
            }
        }

        private int PC2SNES(int PC)
        {
            int blue = (PC & 0xF8) >> 3;
            int green = (PC & 0xF800) >> 11;
            int red = (PC & 0xF80000) >> 19;
            int bgr = blue << 10;
            bgr += green << 5;
            bgr += red;
            return bgr;
        }
        #endregion

        #region Misc
        bool last_edited; // When the checkbox is ticked, we have to figure out which to changed.
        private void pc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                PCTOSNES();
        }
        private void snes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                SNESTOPC();
        }

        private void pc_TextChanged(object sender, EventArgs e)
        {
            //PCTOSNES();
        }

        private void snes_TextChanged(object sender, EventArgs e)
        {
            //SNESTOPC();
        }


        private void PCTOSNES()
        {
            try
            {
                int pcaddr = Int32.Parse(pc.Text, System.Globalization.NumberStyles.HexNumber);
                pcaddr -= header.Checked ? 512 : 0;
                int snesaddr = (int)((pcaddr << 1) & 0x7F0000) | ((pcaddr | 0x8000) & 0xFFFF);
                snes.Text = snesaddr.ToString("X").PadLeft(6, '0');
                last_edited = true;
            }
            catch (Exception) { }
        }

        private void SNESTOPC()
        {
            try
            {
                int snesaddr = Int32.Parse(snes.Text, System.Globalization.NumberStyles.HexNumber);
                int pcaddr = (snesaddr & 0xFF);
                pcaddr |= (snesaddr & 0x7F00);
                pcaddr |= (snesaddr & 0xFF0000) >> 1;
                pcaddr += (header.Checked ? 512 : 0);
                pc.Text = pcaddr.ToString("X").PadLeft(6, '0');
                last_edited = false;
            }
            catch (Exception) { }
        }

        //'Hide' the instance so that the reference isn't disposed, this is necessary to ensure we don't access
        //a disposed object. C# should already take care of this though..
        private void InputBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void header_CheckedChanged(object sender, EventArgs e)
        {
            if (!last_edited)
                SNESTOPC();
            else
                PCTOSNES();
        }
        #endregion

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            int number = ((e.X >> 4) + ((e.Y >> 4) * 0x10));
            ((Main)ParentForm).ActiveDocument.Scintilla.InsertText("$" + number.ToString("X").PadLeft(2, '0') +",");
            ((Main)ParentForm).ActiveDocument.Activate();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            using (Pen myPen = new Pen(Color.Red, 1))
            {
                //Grid.
                for (int i = 0; i <= 256; )
                {
                    e.Graphics.DrawLine(myPen, i, 0, i, 256); // V-Lines.
                    e.Graphics.DrawLine(myPen, 0, i, 256, i); // H-Lines.
                    i = i + 16;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Load graphics file";
            ofd.Filter = "Graphics Files (*.bin)|*.bin|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //byte[] _byte = File.ReadAllBytes(ofd.FileName);
                //FileStream FS = new FileStream(ofd.FileName, FileAccess.Read, true);
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            int number = ((e.X >> 4) + ((e.Y >> 4) * 0x10));
            ((Main)ParentForm).info.Text = "Tile: $" + number.ToString("X").PadLeft(2, '0');
        }
    }
}

