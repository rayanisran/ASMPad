namespace ASMPad
{
    partial class SidePane
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SidePane));
            this.Tabs = new System.Windows.Forms.TabControl();
            this.propertiesTabPage = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.calltip = new System.Windows.Forms.CheckBox();
            this.smartindent = new System.Windows.Forms.CheckBox();
            this.whitespace = new System.Windows.Forms.CheckBox();
            this.tabwidth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.showle = new System.Windows.Forms.CheckBox();
            this.hal = new System.Windows.Forms.CheckBox();
            this.halcol = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lnwidth = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.wpcol = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.loadfile = new System.Windows.Forms.RadioButton();
            this.enableautocomp = new System.Windows.Forms.CheckBox();
            this.loadboth = new System.Windows.Forms.RadioButton();
            this.loaddef = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.selfor = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.selback = new System.Windows.Forms.Panel();
            this.documentOutlineTabPage = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Italicize = new System.Windows.Forms.CheckBox();
            this.boldadd = new System.Windows.Forms.CheckBox();
            this.boldcom = new System.Windows.Forms.CheckBox();
            this.boldok = new System.Windows.Forms.CheckBox();
            this.ok = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.okkey = new System.Windows.Forms.TextBox();
            this.hl = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pancom = new System.Windows.Forms.Panel();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panadd = new System.Windows.Forms.Panel();
            this.panlab = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.boldkey = new System.Windows.Forms.CheckBox();
            this.pankey = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.keybox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.boldop = new System.Windows.Forms.CheckBox();
            this.panop = new System.Windows.Forms.Panel();
            this.opbox = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.Tabs.SuspendLayout();
            this.propertiesTabPage.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabwidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnwidth)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.documentOutlineTabPage.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.propertiesTabPage);
            this.Tabs.Controls.Add(this.documentOutlineTabPage);
            this.Tabs.Controls.Add(this.tabPage1);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Multiline = true;
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(290, 533);
            this.Tabs.TabIndex = 9;
            // 
            // propertiesTabPage
            // 
            this.propertiesTabPage.Controls.Add(this.groupBox5);
            this.propertiesTabPage.Controls.Add(this.button1);
            this.propertiesTabPage.Controls.Add(this.groupBox10);
            this.propertiesTabPage.Controls.Add(this.groupBox3);
            this.propertiesTabPage.Location = new System.Drawing.Point(4, 22);
            this.propertiesTabPage.Name = "propertiesTabPage";
            this.propertiesTabPage.Size = new System.Drawing.Size(282, 507);
            this.propertiesTabPage.TabIndex = 0;
            this.propertiesTabPage.Text = "Document Properties";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.calltip);
            this.groupBox5.Controls.Add(this.smartindent);
            this.groupBox5.Controls.Add(this.whitespace);
            this.groupBox5.Controls.Add(this.tabwidth);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.showle);
            this.groupBox5.Controls.Add(this.hal);
            this.groupBox5.Controls.Add(this.halcol);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.lnwidth);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.wpcol);
            this.groupBox5.Location = new System.Drawing.Point(5, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(273, 164);
            this.groupBox5.TabIndex = 72;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Text Editing";
            // 
            // calltip
            // 
            this.calltip.AutoSize = true;
            this.calltip.Checked = global::ASMPad.Properties.Settings.Default.calltips;
            this.calltip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.calltip.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "calltips", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.calltip.Location = new System.Drawing.Point(14, 143);
            this.calltip.Name = "calltip";
            this.calltip.Size = new System.Drawing.Size(99, 17);
            this.calltip.TabIndex = 71;
            this.calltip.Text = "Enable Tooltips";
            this.Tooltip.SetToolTip(this.calltip, "When set, it will allow tooltips to be shown. Currently, tooltips do not show for" +
                    " RAM/ROM Addresses.");
            this.calltip.UseVisualStyleBackColor = true;
            // 
            // smartindent
            // 
            this.smartindent.AutoSize = true;
            this.smartindent.Checked = global::ASMPad.Properties.Settings.Default.smartindent;
            this.smartindent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smartindent.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "smartindent", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.smartindent.Location = new System.Drawing.Point(14, 22);
            this.smartindent.Name = "smartindent";
            this.smartindent.Size = new System.Drawing.Size(86, 17);
            this.smartindent.TabIndex = 70;
            this.smartindent.Text = "Smart Indent";
            this.Tooltip.SetToolTip(this.smartindent, "When set, it will try to automatically indent code.");
            this.smartindent.UseVisualStyleBackColor = true;
            // 
            // whitespace
            // 
            this.whitespace.AutoSize = true;
            this.whitespace.Checked = global::ASMPad.Properties.Settings.Default.whitespace;
            this.whitespace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.whitespace.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "whitespace", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.whitespace.Location = new System.Drawing.Point(14, 56);
            this.whitespace.Name = "whitespace";
            this.whitespace.Size = new System.Drawing.Size(118, 17);
            this.whitespace.TabIndex = 0;
            this.whitespace.Text = "Show Whitespaces";
            this.Tooltip.SetToolTip(this.whitespace, "When set, it will display whitespaces for spaces and tabs.");
            this.whitespace.UseVisualStyleBackColor = true;
            // 
            // tabwidth
            // 
            this.tabwidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ASMPad.Properties.Settings.Default, "tabwidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tabwidth.Location = new System.Drawing.Point(83, 117);
            this.tabwidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.tabwidth.Name = "tabwidth";
            this.tabwidth.Size = new System.Drawing.Size(43, 20);
            this.tabwidth.TabIndex = 68;
            this.Tooltip.SetToolTip(this.tabwidth, "Changes the width of a tab space.");
            this.tabwidth.Value = global::ASMPad.Properties.Settings.Default.tabwidth;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 69;
            this.label4.Text = "Tab Width";
            // 
            // showle
            // 
            this.showle.AutoSize = true;
            this.showle.Checked = global::ASMPad.Properties.Settings.Default.showidentguides;
            this.showle.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "showidentguides", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.showle.Location = new System.Drawing.Point(14, 39);
            this.showle.Name = "showle";
            this.showle.Size = new System.Drawing.Size(145, 17);
            this.showle.TabIndex = 2;
            this.showle.Text = "Show Indentation Guides";
            this.Tooltip.SetToolTip(this.showle, "When set, it will show indentation guides.");
            this.showle.UseVisualStyleBackColor = true;
            // 
            // hal
            // 
            this.hal.AutoSize = true;
            this.hal.Checked = global::ASMPad.Properties.Settings.Default.hal;
            this.hal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hal.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "hal", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.hal.Location = new System.Drawing.Point(14, 73);
            this.hal.Name = "hal";
            this.hal.Size = new System.Drawing.Size(127, 17);
            this.hal.TabIndex = 3;
            this.hal.Text = "Highlight Current Line";
            this.Tooltip.SetToolTip(this.hal, "When set, the active line will be highlighted.");
            this.hal.UseVisualStyleBackColor = true;
            // 
            // halcol
            // 
            this.halcol.BackColor = global::ASMPad.Properties.Settings.Default.halcol;
            this.halcol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.halcol.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::ASMPad.Properties.Settings.Default, "halcol", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.halcol.Location = new System.Drawing.Point(197, 73);
            this.halcol.Name = "halcol";
            this.halcol.Size = new System.Drawing.Size(16, 16);
            this.halcol.TabIndex = 63;
            this.Tooltip.SetToolTip(this.halcol, "Click to change the active line\'s background color.");
            this.halcol.Click += new System.EventHandler(this.halcol_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Line Margin Width";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(165, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "Color";
            // 
            // lnwidth
            // 
            this.lnwidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ASMPad.Properties.Settings.Default, "MarginWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lnwidth.Location = new System.Drawing.Point(109, 96);
            this.lnwidth.Maximum = 80;
            this.lnwidth.Name = "lnwidth";
            this.lnwidth.Size = new System.Drawing.Size(104, 45);
            this.lnwidth.TabIndex = 4;
            this.lnwidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Tooltip.SetToolTip(this.lnwidth, "Changes the line-margin width. The higher it is, the longer the margin.");
            this.lnwidth.Value = global::ASMPad.Properties.Settings.Default.MarginWidth;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "Color";
            // 
            // wpcol
            // 
            this.wpcol.BackColor = global::ASMPad.Properties.Settings.Default.whitespacecol;
            this.wpcol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wpcol.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::ASMPad.Properties.Settings.Default, "whitespacecol", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.wpcol.Location = new System.Drawing.Point(197, 55);
            this.wpcol.Name = "wpcol";
            this.wpcol.Size = new System.Drawing.Size(16, 16);
            this.wpcol.TabIndex = 65;
            this.Tooltip.SetToolTip(this.wpcol, "Click to change the whitespace color.");
            this.wpcol.Click += new System.EventHandler(this.wpcol_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 328);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 25);
            this.button1.TabIndex = 71;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseHover += new System.EventHandler(this.button1_MouseHover);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.loadfile);
            this.groupBox10.Controls.Add(this.enableautocomp);
            this.groupBox10.Controls.Add(this.loadboth);
            this.groupBox10.Controls.Add(this.loaddef);
            this.groupBox10.Location = new System.Drawing.Point(8, 232);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(268, 94);
            this.groupBox10.TabIndex = 67;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Autocompletion";
            // 
            // loadfile
            // 
            this.loadfile.AutoSize = true;
            this.loadfile.Location = new System.Drawing.Point(22, 38);
            this.loadfile.Name = "loadfile";
            this.loadfile.Size = new System.Drawing.Size(138, 17);
            this.loadfile.TabIndex = 48;
            this.loadfile.TabStop = true;
            this.loadfile.Text = "Load definitions from file";
            this.Tooltip.SetToolTip(this.loadfile, "When set, the auto-completion list is loaded from the definitions in the active d" +
                    "ocument.");
            this.loadfile.UseVisualStyleBackColor = true;
            // 
            // enableautocomp
            // 
            this.enableautocomp.AutoSize = true;
            this.enableautocomp.Checked = global::ASMPad.Properties.Settings.Default.autocomp;
            this.enableautocomp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableautocomp.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "autocomp", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableautocomp.Location = new System.Drawing.Point(10, 19);
            this.enableautocomp.Name = "enableautocomp";
            this.enableautocomp.Size = new System.Drawing.Size(134, 17);
            this.enableautocomp.TabIndex = 60;
            this.enableautocomp.Text = "Enable autocompletion";
            this.Tooltip.SetToolTip(this.enableautocomp, "When set, an autocompletion pop-up list will be shown when you type !.");
            this.enableautocomp.UseVisualStyleBackColor = true;
            // 
            // loadboth
            // 
            this.loadboth.AutoSize = true;
            this.loadboth.Location = new System.Drawing.Point(22, 71);
            this.loadboth.Name = "loadboth";
            this.loadboth.Size = new System.Drawing.Size(146, 17);
            this.loadboth.TabIndex = 49;
            this.loadboth.TabStop = true;
            this.loadboth.Text = "Load definitions from both";
            this.Tooltip.SetToolTip(this.loadboth, "When set, the auto-completion list will load definitions from the active document" +
                    " as well as ones in the file.");
            this.loadboth.UseVisualStyleBackColor = true;
            // 
            // loaddef
            // 
            this.loaddef.AutoSize = true;
            this.loaddef.Location = new System.Drawing.Point(22, 54);
            this.loaddef.Name = "loaddef";
            this.loaddef.Size = new System.Drawing.Size(154, 17);
            this.loaddef.TabIndex = 47;
            this.loaddef.TabStop = true;
            this.loaddef.Text = "Load definitions from def.txt";
            this.Tooltip.SetToolTip(this.loaddef, "When set, the auto-completion list will load definitions from a file.");
            this.loaddef.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.selfor);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.selback);
            this.groupBox3.Location = new System.Drawing.Point(8, 169);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(265, 60);
            this.groupBox3.TabIndex = 50;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Selection";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 39);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Fore Color";
            // 
            // selfor
            // 
            this.selfor.BackColor = global::ASMPad.Properties.Settings.Default.selforcl;
            this.selfor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selfor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::ASMPad.Properties.Settings.Default, "selforcl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.selfor.Location = new System.Drawing.Point(72, 37);
            this.selfor.Name = "selfor";
            this.selfor.Size = new System.Drawing.Size(16, 16);
            this.selfor.TabIndex = 48;
            this.Tooltip.SetToolTip(this.selfor, "Click to change the selected text\'s backcolor.");
            this.selfor.Click += new System.EventHandler(this.selfor_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Back Color";
            // 
            // selback
            // 
            this.selback.BackColor = global::ASMPad.Properties.Settings.Default.selbackcl;
            this.selback.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selback.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::ASMPad.Properties.Settings.Default, "selbackcl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.selback.Location = new System.Drawing.Point(72, 18);
            this.selback.Name = "selback";
            this.selback.Size = new System.Drawing.Size(16, 16);
            this.selback.TabIndex = 47;
            this.Tooltip.SetToolTip(this.selback, "Click to change the selected text\'s forecolor.");
            this.selback.Click += new System.EventHandler(this.selback_Click);
            // 
            // documentOutlineTabPage
            // 
            this.documentOutlineTabPage.Controls.Add(this.listBox1);
            this.documentOutlineTabPage.Location = new System.Drawing.Point(4, 22);
            this.documentOutlineTabPage.Name = "documentOutlineTabPage";
            this.documentOutlineTabPage.Size = new System.Drawing.Size(282, 507);
            this.documentOutlineTabPage.TabIndex = 1;
            this.documentOutlineTabPage.Text = "Templates";
            // 
            // listBox1
            // 
            this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Items.AddRange(new object[] {
            "Activate ON/OFF switch",
            "Active P-Switch (blue)",
            "Alternate Animation (2)",
            "Alternate Animation (3)",
            "Brighten Screen",
            "Chase",
            "Darken Screen ($07)",
            "Darken Screen ($0A)",
            "Follow",
            "Generate Bullets",
            "Generate Custom Sprite",
            "Generate Fireballs",
            "Generate Multiple Custom Sprites",
            "Generate Multiple Normal Sprites",
            "Generate Normal Sprite",
            "Generate Parabombs",
            "Generate Random Custom Sprite",
            "Generate Random Normal Sprite",
            "Jump",
            "Jump (higher frequency)",
            "Jump (highest frequency)",
            "Loopy Lights",
            "Make Carryable",
            "Make Invisible",
            "Posion Mario",
            "Push",
            "Random Movement",
            "Roar",
            "Shake on Jump",
            "Shake on Jump (more time)",
            "Spawn Message",
            "Spawn Message 2",
            "Spit Fireballs",
            "Throw Bones",
            "Throw Bones (fast)",
            "Throw Hammer",
            "Throw Hammer (fast)",
            "Walk",
            "Wavy Motion"});
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(282, 507);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 2;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.editToolStripMenuItem,
            this.renameToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(132, 92);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.addToolStripMenuItem.Text = "Add..";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            this.addToolStripMenuItem.MouseEnter += new System.EventHandler(this.addToolStripMenuItem_MouseEnter);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.deleteToolStripMenuItem.Text = "Delete..";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            this.deleteToolStripMenuItem.MouseEnter += new System.EventHandler(this.deleteToolStripMenuItem_MouseEnter);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.editToolStripMenuItem.Text = "Edit..";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            this.editToolStripMenuItem.MouseEnter += new System.EventHandler(this.editToolStripMenuItem_MouseEnter);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.renameToolStripMenuItem.Text = "Rename..";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            this.renameToolStripMenuItem.MouseEnter += new System.EventHandler(this.renameToolStripMenuItem_MouseEnter);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.button8);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(282, 507);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Syntax Highlighting";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownHeight = 120;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Items.AddRange(new object[] {
            "Scheme 1",
            "Scheme 2",
            "Scheme 3",
            "Scheme 4",
            "Scheme 5",
            "Scheme 6",
            "Scheme 7",
            "Scheme 8",
            "Scheme 9"});
            this.comboBox1.Location = new System.Drawing.Point(60, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            this.Tooltip.SetToolTip(this.comboBox1, "Choose a scheme for syntax highlighting.");
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 73;
            this.label9.Text = "Scheme";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(9, 474);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(90, 25);
            this.button8.TabIndex = 72;
            this.button8.Text = "Update";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            this.button8.MouseHover += new System.EventHandler(this.button8_MouseHover);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Italicize);
            this.groupBox1.Controls.Add(this.boldadd);
            this.groupBox1.Controls.Add(this.boldcom);
            this.groupBox1.Controls.Add(this.boldok);
            this.groupBox1.Controls.Add(this.ok);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.okkey);
            this.groupBox1.Controls.Add(this.hl);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pancom);
            this.groupBox1.Controls.Add(this.checkBox7);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.panadd);
            this.groupBox1.Controls.Add(this.panlab);
            this.groupBox1.Location = new System.Drawing.Point(8, 274);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 194);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Other";
            // 
            // Italicize
            // 
            this.Italicize.AutoSize = true;
            this.Italicize.Checked = global::ASMPad.Properties.Settings.Default.CommentItal;
            this.Italicize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Italicize.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "CommentItal", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Italicize.Location = new System.Drawing.Point(197, 93);
            this.Italicize.Name = "Italicize";
            this.Italicize.Size = new System.Drawing.Size(61, 17);
            this.Italicize.TabIndex = 21;
            this.Italicize.Text = "Italicize";
            this.Italicize.UseVisualStyleBackColor = true;
            // 
            // boldadd
            // 
            this.boldadd.AutoSize = true;
            this.boldadd.Checked = global::ASMPad.Properties.Settings.Default.boldaddr;
            this.boldadd.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "boldaddr", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.boldadd.Location = new System.Drawing.Point(148, 118);
            this.boldadd.Name = "boldadd";
            this.boldadd.Size = new System.Drawing.Size(47, 17);
            this.boldadd.TabIndex = 20;
            this.boldadd.Text = "Bold";
            this.boldadd.UseVisualStyleBackColor = true;
            // 
            // boldcom
            // 
            this.boldcom.AutoSize = true;
            this.boldcom.Checked = global::ASMPad.Properties.Settings.Default.boldcom;
            this.boldcom.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "boldcom", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.boldcom.Location = new System.Drawing.Point(148, 93);
            this.boldcom.Name = "boldcom";
            this.boldcom.Size = new System.Drawing.Size(47, 17);
            this.boldcom.TabIndex = 19;
            this.boldcom.Text = "Bold";
            this.boldcom.UseVisualStyleBackColor = true;
            // 
            // boldok
            // 
            this.boldok.AutoSize = true;
            this.boldok.Checked = global::ASMPad.Properties.Settings.Default.boldok;
            this.boldok.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "boldok", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.boldok.Location = new System.Drawing.Point(191, 59);
            this.boldok.Name = "boldok";
            this.boldok.Size = new System.Drawing.Size(47, 17);
            this.boldok.TabIndex = 18;
            this.boldok.Text = "Bold";
            this.boldok.UseVisualStyleBackColor = true;
            // 
            // ok
            // 
            this.ok.BackColor = global::ASMPad.Properties.Settings.Default.ok;
            this.ok.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ok.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::ASMPad.Properties.Settings.Default, "ok", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ok.Location = new System.Drawing.Point(169, 60);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(16, 16);
            this.ok.TabIndex = 17;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(167, 31);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "&Set Color";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Other Keywords";
            // 
            // okkey
            // 
            this.okkey.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okkey.Location = new System.Drawing.Point(6, 33);
            this.okkey.Multiline = true;
            this.okkey.Name = "okkey";
            this.okkey.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.okkey.Size = new System.Drawing.Size(155, 49);
            this.okkey.TabIndex = 6;
            this.okkey.Text = "l w b A X Y";
            this.Tooltip.SetToolTip(this.okkey, "Another set of keywords that can be used for highlighting.");
            // 
            // hl
            // 
            this.hl.AutoSize = true;
            this.hl.Checked = global::ASMPad.Properties.Settings.Default.hl;
            this.hl.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "hl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.hl.Location = new System.Drawing.Point(19, 170);
            this.hl.Name = "hl";
            this.hl.Size = new System.Drawing.Size(101, 17);
            this.hl.TabIndex = 14;
            this.hl.Text = "Highlight Labels";
            this.hl.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Comment";
            // 
            // pancom
            // 
            this.pancom.BackColor = global::ASMPad.Properties.Settings.Default.commentclr;
            this.pancom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pancom.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::ASMPad.Properties.Settings.Default, "commentclr", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pancom.Location = new System.Drawing.Point(126, 93);
            this.pancom.Name = "pancom";
            this.pancom.Size = new System.Drawing.Size(16, 16);
            this.pancom.TabIndex = 5;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Checked = global::ASMPad.Properties.Settings.Default.boldlab;
            this.checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox7.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "boldlab", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox7.Location = new System.Drawing.Point(148, 143);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(47, 17);
            this.checkBox7.TabIndex = 13;
            this.checkBox7.Text = "Bold";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(61, 114);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(59, 23);
            this.button6.TabIndex = 9;
            this.button6.Text = "&Set Color";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(61, 141);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(59, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "&Set Color";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Labels";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(61, 88);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(59, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "&Set Color";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Addresses";
            // 
            // panadd
            // 
            this.panadd.BackColor = global::ASMPad.Properties.Settings.Default.addclr;
            this.panadd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panadd.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::ASMPad.Properties.Settings.Default, "addclr", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.panadd.Location = new System.Drawing.Point(126, 117);
            this.panadd.Name = "panadd";
            this.panadd.Size = new System.Drawing.Size(16, 16);
            this.panadd.TabIndex = 5;
            // 
            // panlab
            // 
            this.panlab.BackColor = global::ASMPad.Properties.Settings.Default.labclr;
            this.panlab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panlab.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::ASMPad.Properties.Settings.Default, "labclr", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.panlab.Location = new System.Drawing.Point(126, 143);
            this.panlab.Name = "panlab";
            this.panlab.Size = new System.Drawing.Size(16, 16);
            this.panlab.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.boldkey);
            this.groupBox2.Controls.Add(this.pankey);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.keybox);
            this.groupBox2.Location = new System.Drawing.Point(8, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 106);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Keywords";
            // 
            // boldkey
            // 
            this.boldkey.AutoSize = true;
            this.boldkey.Checked = global::ASMPad.Properties.Settings.Default.boldkey;
            this.boldkey.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "boldkey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.boldkey.Location = new System.Drawing.Point(191, 70);
            this.boldkey.Name = "boldkey";
            this.boldkey.Size = new System.Drawing.Size(47, 17);
            this.boldkey.TabIndex = 6;
            this.boldkey.Text = "Bold";
            this.boldkey.UseVisualStyleBackColor = true;
            // 
            // pankey
            // 
            this.pankey.BackColor = global::ASMPad.Properties.Settings.Default.keywordclr;
            this.pankey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pankey.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::ASMPad.Properties.Settings.Default, "keywordclr", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pankey.Location = new System.Drawing.Point(191, 48);
            this.pankey.Name = "pankey";
            this.pankey.Size = new System.Drawing.Size(16, 16);
            this.pankey.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(189, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "&Set Color";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // keybox
            // 
            this.keybox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keybox.Location = new System.Drawing.Point(6, 21);
            this.keybox.Multiline = true;
            this.keybox.Name = "keybox";
            this.keybox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.keybox.Size = new System.Drawing.Size(177, 78);
            this.keybox.TabIndex = 4;
            this.keybox.Text = "Header LoROM org db pc dcb star dw dl incbin incsrc print macro endmacro";
            this.Tooltip.SetToolTip(this.keybox, "These words will be highlighted as keywords.");
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.boldop);
            this.groupBox4.Controls.Add(this.panop);
            this.groupBox4.Controls.Add(this.opbox);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Location = new System.Drawing.Point(8, 33);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(268, 133);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Opcodes";
            // 
            // boldop
            // 
            this.boldop.AutoSize = true;
            this.boldop.Checked = global::ASMPad.Properties.Settings.Default.boldop;
            this.boldop.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASMPad.Properties.Settings.Default, "boldop", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.boldop.Location = new System.Drawing.Point(191, 69);
            this.boldop.Name = "boldop";
            this.boldop.Size = new System.Drawing.Size(47, 17);
            this.boldop.TabIndex = 5;
            this.boldop.Text = "Bold";
            this.boldop.UseVisualStyleBackColor = true;
            // 
            // panop
            // 
            this.panop.BackColor = global::ASMPad.Properties.Settings.Default.opcodeclr;
            this.panop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panop.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::ASMPad.Properties.Settings.Default, "opcodeclr", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.panop.Location = new System.Drawing.Point(191, 47);
            this.panop.Name = "panop";
            this.panop.Size = new System.Drawing.Size(16, 16);
            this.panop.TabIndex = 4;
            // 
            // opbox
            // 
            this.opbox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opbox.Location = new System.Drawing.Point(6, 18);
            this.opbox.Multiline = true;
            this.opbox.Name = "opbox";
            this.opbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.opbox.Size = new System.Drawing.Size(177, 108);
            this.opbox.TabIndex = 3;
            this.opbox.Text = resources.GetString("opbox.Text");
            this.Tooltip.SetToolTip(this.opbox, "These words will be highlighted as opcodes.");
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(189, 18);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "&Set Color";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.Filter = "*.xml*";
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
            this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
            this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
            // 
            // Tooltip
            // 
            this.Tooltip.AutoPopDelay = 5000;
            this.Tooltip.InitialDelay = 100;
            this.Tooltip.ReshowDelay = 40;
            this.Tooltip.ShowAlways = true;
            // 
            // SidePane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 533);
            this.Controls.Add(this.Tabs);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SidePane";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SidePane_FormClosing);
            this.Load += new System.EventHandler(this.SidePane_Load);
            this.Tabs.ResumeLayout(false);
            this.propertiesTabPage.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabwidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnwidth)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.documentOutlineTabPage.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage documentOutlineTabPage;
        private System.Windows.Forms.TabPage propertiesTabPage;
        private System.Windows.Forms.TrackBar lnwidth;
        private System.Windows.Forms.CheckBox hal;
        private System.Windows.Forms.CheckBox showle;
        private System.Windows.Forms.CheckBox whitespace;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.Panel selfor;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.Panel selback;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel halcol;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel wpcol;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton loadfile;
        public System.Windows.Forms.CheckBox enableautocomp;
        private System.Windows.Forms.RadioButton loadboth;
        private System.Windows.Forms.RadioButton loaddef;
        private System.Windows.Forms.CheckBox smartindent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown tabwidth;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panlab;
        private System.Windows.Forms.Panel panadd;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel pancom;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pankey;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox keybox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panop;
        private System.Windows.Forms.TextBox opbox;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.CheckBox hl;
        private System.Windows.Forms.Panel ok;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox okkey;
        private System.Windows.Forms.CheckBox calltip;
        public System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox boldadd;
        private System.Windows.Forms.CheckBox boldcom;
        private System.Windows.Forms.CheckBox boldok;
        private System.Windows.Forms.CheckBox boldkey;
        private System.Windows.Forms.CheckBox boldop;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        public System.Windows.Forms.Button button8;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip Tooltip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.CheckBox Italicize;
    }
}