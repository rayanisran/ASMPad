namespace ASMPad
{
	partial class DocForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocForm));
            this.scintilla = new ScintillaNet.Scintilla();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem55 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem54 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem56 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem58 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem57 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem59 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem60 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.codeSnippetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.makeMacroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.commentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indentCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.identToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unindentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toUppercaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toLowercaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compactCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.countBytesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.scintilla)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scintilla
            // 
            this.scintilla.CallTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.scintilla.ConfigurationManager.CustomLocation = "ASM.xml";
            this.scintilla.ConfigurationManager.Language = "TRASM";
            this.scintilla.ContextMenuStrip = this.contextMenuStrip1;
            this.scintilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintilla.Folding.IsEnabled = false;
            this.scintilla.Indentation.BackspaceUnindents = true;
            this.scintilla.Indentation.SmartIndentType = ScintillaNet.SmartIndent.CPP;
            this.scintilla.IsBraceMatching = true;
            this.scintilla.Lexing.Lexer = ScintillaNet.Lexer.Asm;
            this.scintilla.Lexing.LexerName = "asm";
            this.scintilla.Lexing.LineCommentPrefix = "";
            this.scintilla.Lexing.StreamCommentPrefix = "";
            this.scintilla.Lexing.StreamCommentSufix = "";
            this.scintilla.Lexing.WordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_$#:!";
            this.scintilla.LineWrap.VisualFlags = ScintillaNet.WrapVisualFlag.End;
            this.scintilla.Location = new System.Drawing.Point(0, 0);
            this.scintilla.Margins.Margin0.Width = 40;
            this.scintilla.Margins.Margin1.AutoToggleMarkerNumber = 0;
            this.scintilla.Margins.Margin1.IsClickable = true;
            this.scintilla.Margins.Margin2.Width = 16;
            this.scintilla.Name = "scintilla";
            this.scintilla.Scrolling.EndAtLastLine = false;
            this.scintilla.Scrolling.HorizontalWidth = 1000;
            this.scintilla.Selection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(211)))), ((int)(((byte)(255)))));
            this.scintilla.Selection.BackColorUnfocused = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(211)))), ((int)(((byte)(255)))));
            this.scintilla.Selection.ForeColor = System.Drawing.Color.Transparent;
            this.scintilla.Selection.ForeColorUnfocused = System.Drawing.Color.Transparent;
            this.scintilla.Size = new System.Drawing.Size(292, 266);
            this.scintilla.Snippets.IsOneKeySelectionEmbedEnabled = true;
            this.scintilla.Styles.BraceBad.ForeColor = System.Drawing.Color.Maroon;
            this.scintilla.Styles.BraceLight.ForeColor = System.Drawing.Color.Red;
            this.scintilla.Styles.CallTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.scintilla.Styles.Default.BackColor = System.Drawing.SystemColors.Window;
            this.scintilla.Styles.Default.CharacterSet = ScintillaNet.CharacterSet.Ansi;
            this.scintilla.Styles.Default.FontName = "Consolas";
            this.scintilla.Styles.Default.Size = 9.75F;
            this.scintilla.Styles.LineNumber.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.scintilla.TabIndex = 0;
            this.scintilla.CallTipClick += new System.EventHandler<ScintillaNet.CallTipClickEventArgs>(this.scintilla_CallTipClick);
            this.scintilla.CharAdded += new System.EventHandler<ScintillaNet.CharAddedEventArgs>(this.scintilla_CharAdded);
            this.scintilla.ModifiedChanged += new System.EventHandler(this.scintilla_ModifiedChanged);
            this.scintilla.DwellStart += new System.EventHandler<ScintillaNet.ScintillaMouseEventArgs>(this.scintilla_DwellStart);
            this.scintilla.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.scintilla_Scroll);
            this.scintilla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.scintilla_KeyDown);
            this.scintilla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scintilla_KeyPress);
            this.scintilla.KeyUp += new System.Windows.Forms.KeyEventHandler(this.scintilla_KeyUp);
            this.scintilla.MouseClick += new System.Windows.Forms.MouseEventHandler(this.scintilla_MouseClick);
            this.scintilla.MouseHover += new System.EventHandler(this.scintilla_MouseHover);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem55,
            this.toolStripMenuItem54,
            this.toolStripSeparator1,
            this.toolStripMenuItem56,
            this.toolStripMenuItem58,
            this.toolStripMenuItem57,
            this.toolStripMenuItem59,
            this.toolStripMenuItem60,
            this.toolStripSeparator3,
            this.codeSnippetToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.makeMacroToolStripMenuItem,
            this.toolStripMenuItem2,
            this.indentCodeToolStripMenuItem,
            this.transformToolStripMenuItem,
            this.compactCodeToolStripMenuItem,
            this.countBytesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(202, 352);
            // 
            // toolStripMenuItem55
            // 
            this.toolStripMenuItem55.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem55.Image")));
            this.toolStripMenuItem55.Name = "toolStripMenuItem55";
            this.toolStripMenuItem55.ShortcutKeyDisplayString = "Ctrl+Y";
            this.toolStripMenuItem55.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItem55.Text = "Redo";
            this.toolStripMenuItem55.Click += new System.EventHandler(this.toolStripMenuItem55_Click);
            // 
            // toolStripMenuItem54
            // 
            this.toolStripMenuItem54.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem54.Image")));
            this.toolStripMenuItem54.Name = "toolStripMenuItem54";
            this.toolStripMenuItem54.ShortcutKeyDisplayString = "Ctrl+Z";
            this.toolStripMenuItem54.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItem54.Text = "Undo";
            this.toolStripMenuItem54.Click += new System.EventHandler(this.toolStripMenuItem54_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // toolStripMenuItem56
            // 
            this.toolStripMenuItem56.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem56.Image")));
            this.toolStripMenuItem56.Name = "toolStripMenuItem56";
            this.toolStripMenuItem56.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.toolStripMenuItem56.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItem56.Text = "Cut";
            this.toolStripMenuItem56.Click += new System.EventHandler(this.toolStripMenuItem56_Click);
            // 
            // toolStripMenuItem58
            // 
            this.toolStripMenuItem58.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem58.Image")));
            this.toolStripMenuItem58.Name = "toolStripMenuItem58";
            this.toolStripMenuItem58.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem58.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItem58.Text = "Copy";
            this.toolStripMenuItem58.Click += new System.EventHandler(this.toolStripMenuItem58_Click);
            // 
            // toolStripMenuItem57
            // 
            this.toolStripMenuItem57.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem57.Image")));
            this.toolStripMenuItem57.Name = "toolStripMenuItem57";
            this.toolStripMenuItem57.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.toolStripMenuItem57.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItem57.Text = "Paste";
            this.toolStripMenuItem57.Click += new System.EventHandler(this.toolStripMenuItem57_Click);
            // 
            // toolStripMenuItem59
            // 
            this.toolStripMenuItem59.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem59.Image")));
            this.toolStripMenuItem59.Name = "toolStripMenuItem59";
            this.toolStripMenuItem59.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItem59.Text = "Delete";
            this.toolStripMenuItem59.Click += new System.EventHandler(this.toolStripMenuItem59_Click);
            // 
            // toolStripMenuItem60
            // 
            this.toolStripMenuItem60.Name = "toolStripMenuItem60";
            this.toolStripMenuItem60.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.toolStripMenuItem60.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItem60.Text = "Select All";
            this.toolStripMenuItem60.Click += new System.EventHandler(this.toolStripMenuItem60_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(198, 6);
            // 
            // codeSnippetToolStripMenuItem
            // 
            this.codeSnippetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("codeSnippetToolStripMenuItem.Image")));
            this.codeSnippetToolStripMenuItem.Name = "codeSnippetToolStripMenuItem";
            this.codeSnippetToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+<";
            this.codeSnippetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemcomma)));
            this.codeSnippetToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.codeSnippetToolStripMenuItem.Text = "Insert Snippets";
            this.codeSnippetToolStripMenuItem.Click += new System.EventHandler(this.codeSnippetToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.toolStripMenuItem1.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItem1.Text = "Add To Snippets";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            this.toolStripMenuItem1.MouseEnter += new System.EventHandler(this.toolStripMenuItem1_MouseEnter);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // makeMacroToolStripMenuItem
            // 
            this.makeMacroToolStripMenuItem.Name = "makeMacroToolStripMenuItem";
            this.makeMacroToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.makeMacroToolStripMenuItem.Text = "Selection As Macro";
            this.makeMacroToolStripMenuItem.Click += new System.EventHandler(this.makeMacroToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commentToolStripMenuItem,
            this.uncommentToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItem2.Text = "Comments";
            // 
            // commentToolStripMenuItem
            // 
            this.commentToolStripMenuItem.Name = "commentToolStripMenuItem";
            this.commentToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.commentToolStripMenuItem.Text = "Comment Selection";
            this.commentToolStripMenuItem.Click += new System.EventHandler(this.commentToolStripMenuItem_Click);
            // 
            // uncommentToolStripMenuItem
            // 
            this.uncommentToolStripMenuItem.Name = "uncommentToolStripMenuItem";
            this.uncommentToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.uncommentToolStripMenuItem.Text = "Uncomment Selection";
            this.uncommentToolStripMenuItem.Click += new System.EventHandler(this.uncommentToolStripMenuItem_Click);
            // 
            // indentCodeToolStripMenuItem
            // 
            this.indentCodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.identToolStripMenuItem,
            this.unindentToolStripMenuItem});
            this.indentCodeToolStripMenuItem.Name = "indentCodeToolStripMenuItem";
            this.indentCodeToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.indentCodeToolStripMenuItem.Text = "Identation";
            // 
            // identToolStripMenuItem
            // 
            this.identToolStripMenuItem.Name = "identToolStripMenuItem";
            this.identToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.identToolStripMenuItem.Text = "Ident";
            this.identToolStripMenuItem.Click += new System.EventHandler(this.identToolStripMenuItem_Click);
            // 
            // unindentToolStripMenuItem
            // 
            this.unindentToolStripMenuItem.Name = "unindentToolStripMenuItem";
            this.unindentToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.unindentToolStripMenuItem.Text = "Unindent";
            this.unindentToolStripMenuItem.Click += new System.EventHandler(this.unindentToolStripMenuItem_Click);
            // 
            // transformToolStripMenuItem
            // 
            this.transformToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toUppercaseToolStripMenuItem,
            this.toLowercaseToolStripMenuItem});
            this.transformToolStripMenuItem.Name = "transformToolStripMenuItem";
            this.transformToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.transformToolStripMenuItem.Text = "Transform";
            // 
            // toUppercaseToolStripMenuItem
            // 
            this.toUppercaseToolStripMenuItem.Name = "toUppercaseToolStripMenuItem";
            this.toUppercaseToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.toUppercaseToolStripMenuItem.Text = "To Uppercase";
            this.toUppercaseToolStripMenuItem.Click += new System.EventHandler(this.toUppercaseToolStripMenuItem_Click);
            // 
            // toLowercaseToolStripMenuItem
            // 
            this.toLowercaseToolStripMenuItem.Name = "toLowercaseToolStripMenuItem";
            this.toLowercaseToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.toLowercaseToolStripMenuItem.Text = "To Lowercase";
            this.toLowercaseToolStripMenuItem.Click += new System.EventHandler(this.toLowercaseToolStripMenuItem_Click);
            // 
            // compactCodeToolStripMenuItem
            // 
            this.compactCodeToolStripMenuItem.Name = "compactCodeToolStripMenuItem";
            this.compactCodeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.compactCodeToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.compactCodeToolStripMenuItem.Text = "Single Line";
            this.compactCodeToolStripMenuItem.Click += new System.EventHandler(this.compactCodeToolStripMenuItem_Click);
            // 
            // countBytesToolStripMenuItem
            // 
            this.countBytesToolStripMenuItem.Name = "countBytesToolStripMenuItem";
            this.countBytesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.countBytesToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.countBytesToolStripMenuItem.Text = "Count Stats";
            this.countBytesToolStripMenuItem.Click += new System.EventHandler(this.countBytesToolStripMenuItem_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "asm";
            this.saveFileDialog.Filter = "ASM Files (*.asm)|*.asm|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            this.saveFileDialog.Title = "Save File";
            // 
            // DocForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.scintilla);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocForm";
            this.Text = "Untitled.asm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocumentForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.scintilla)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private ScintillaNet.Scintilla scintilla;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem56;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem58;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem57;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem59;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem60;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem codeSnippetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem makeMacroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem commentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uncommentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indentCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem identToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unindentToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem goToDefinitionToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem definitionToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem labelToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem macroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toUppercaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toLowercaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compactCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem countBytesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem55;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem54;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}