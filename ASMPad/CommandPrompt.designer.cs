namespace ASMPad
{
    partial class CommandPrompt
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommandPrompt));
            this.cmbInput = new System.Windows.Forms.ComboBox();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cs = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TSA = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.line = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.error = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.getHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TS = new System.Windows.Forms.ToolTip(this.components);
            this.Tabs.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.TSA.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbInput
            // 
            this.cmbInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbInput.BackColor = System.Drawing.Color.Black;
            this.cmbInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cmbInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmbInput.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbInput.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInput.ForeColor = System.Drawing.Color.Chartreuse;
            this.cmbInput.FormattingEnabled = true;
            this.cmbInput.IntegralHeight = false;
            this.cmbInput.Location = new System.Drawing.Point(3, 245);
            this.cmbInput.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.cmbInput.Name = "cmbInput";
            this.cmbInput.Size = new System.Drawing.Size(716, 23);
            this.cmbInput.TabIndex = 0;
            this.cmbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbInput_KeyDown);
            // 
            // rtbOutput
            // 
            this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbOutput.BackColor = System.Drawing.Color.Black;
            this.rtbOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbOutput.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbOutput.ForeColor = System.Drawing.Color.White;
            this.rtbOutput.Location = new System.Drawing.Point(0, 1);
            this.rtbOutput.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbOutput.Size = new System.Drawing.Size(724, 238);
            this.rtbOutput.TabIndex = 1;
            this.rtbOutput.Text = "";
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.tabPage3);
            this.Tabs.Controls.Add(this.tabPage1);
            this.Tabs.Controls.Add(this.tabPage2);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(730, 297);
            this.Tabs.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cs);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(722, 271);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Events";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cs
            // 
            this.cs.BackColor = System.Drawing.SystemColors.Window;
            this.cs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cs.Font = new System.Drawing.Font("Courier New", 8F);
            this.cs.Location = new System.Drawing.Point(0, 0);
            this.cs.Multiline = true;
            this.cs.Name = "cs";
            this.cs.ReadOnly = true;
            this.cs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.cs.Size = new System.Drawing.Size(722, 271);
            this.cs.TabIndex = 8;
            this.cs.Text = "Loaded ASMPad.";
            this.cs.WordWrap = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.rtbOutput);
            this.tabPage1.Controls.Add(this.cmbInput);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(722, 271);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Console";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.TSA);
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(722, 271);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Errors";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TSA
            // 
            this.TSA.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.TSA.Location = new System.Drawing.Point(3, 3);
            this.TSA.Name = "TSA";
            this.TSA.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.TSA.Size = new System.Drawing.Size(716, 25);
            this.TSA.TabIndex = 5;
            this.TSA.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Checked = true;
            this.toolStripButton1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(57, 22);
            this.toolStripButton1.Text = "Errors";
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.line,
            this.error,
            this.file});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(3, 36);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(721, 232);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseEnter += new System.EventHandler(this.listView1_MouseEnter);
            // 
            // line
            // 
            this.line.Text = "Line";
            this.line.Width = 42;
            // 
            // error
            // 
            this.error.Text = "Error";
            this.error.Width = 237;
            // 
            // file
            // 
            this.file.Text = "File";
            this.file.Width = 338;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getHelpToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyErrorToolStripMenuItem,
            this.goToLineToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 76);
            // 
            // getHelpToolStripMenuItem
            // 
            this.getHelpToolStripMenuItem.Name = "getHelpToolStripMenuItem";
            this.getHelpToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.getHelpToolStripMenuItem.Text = "Show Error Help..";
            this.getHelpToolStripMenuItem.Click += new System.EventHandler(this.getHelpToolStripMenuItem_Click);
            this.getHelpToolStripMenuItem.MouseEnter += new System.EventHandler(this.getHelpToolStripMenuItem_MouseEnter);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(170, 6);
            // 
            // copyErrorToolStripMenuItem
            // 
            this.copyErrorToolStripMenuItem.Name = "copyErrorToolStripMenuItem";
            this.copyErrorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.copyErrorToolStripMenuItem.Text = "Copy Error..";
            this.copyErrorToolStripMenuItem.Click += new System.EventHandler(this.copyErrorToolStripMenuItem_Click);
            // 
            // goToLineToolStripMenuItem
            // 
            this.goToLineToolStripMenuItem.Name = "goToLineToolStripMenuItem";
            this.goToLineToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.goToLineToolStripMenuItem.Text = "Go To Line..";
            this.goToLineToolStripMenuItem.Click += new System.EventHandler(this.goToLineToolStripMenuItem_Click);
            // 
            // TS
            // 
            this.TS.AutomaticDelay = 50;
            this.TS.AutoPopDelay = 2000000;
            this.TS.InitialDelay = 50;
            this.TS.IsBalloon = true;
            this.TS.ReshowDelay = 100000;
            this.TS.ShowAlways = true;
            this.TS.ToolTipTitle = "Help";
            // 
            // CommandPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 297);
            this.Controls.Add(this.Tabs);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Name = "CommandPrompt";
            this.TabText = "Output";
            this.Text = "Output";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommandPrompt_FormClosing);
            this.Tabs.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.TSA.ResumeLayout(false);
            this.TSA.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbInput;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader line;
        private System.Windows.Forms.ColumnHeader error;
        private System.Windows.Forms.ColumnHeader file;
        private System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.TabControl Tabs;
        public System.Windows.Forms.TextBox cs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem getHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip TSA;
        public System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolTip TS;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyErrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToLineToolStripMenuItem;
    }
}
