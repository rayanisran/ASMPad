namespace ASMPad
{
    partial class PatchDialog
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pjName = new System.Windows.Forms.TextBox();
            this.hackSpot = new System.Windows.Forms.TextBox();
            this.noBytes = new System.Windows.Forms.TextBox();
            this.oldCode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.End = new System.Windows.Forms.RadioButton();
            this.Start = new System.Windows.Forms.RadioButton();
            this.generic = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.select = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(265, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(365, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 24);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Patch Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hijack Spot:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Restored Code:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "# of bytes in routine:";
            // 
            // pjName
            // 
            this.pjName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pjName.Location = new System.Drawing.Point(106, 10);
            this.pjName.MaxLength = 60;
            this.pjName.Name = "pjName";
            this.pjName.Size = new System.Drawing.Size(171, 20);
            this.pjName.TabIndex = 0;
            // 
            // hackSpot
            // 
            this.hackSpot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.hackSpot.Location = new System.Drawing.Point(106, 35);
            this.hackSpot.MaxLength = 7;
            this.hackSpot.Name = "hackSpot";
            this.hackSpot.Size = new System.Drawing.Size(171, 20);
            this.hackSpot.TabIndex = 1;
            this.hackSpot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hackSpot_KeyDown);
            this.hackSpot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hackSpot_KeyPress);
            // 
            // noBytes
            // 
            this.noBytes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.noBytes.Location = new System.Drawing.Point(106, 59);
            this.noBytes.MaxLength = 1;
            this.noBytes.Name = "noBytes";
            this.noBytes.Size = new System.Drawing.Size(171, 20);
            this.noBytes.TabIndex = 2;
            // 
            // oldCode
            // 
            this.oldCode.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oldCode.Location = new System.Drawing.Point(106, 84);
            this.oldCode.MaxLength = 25;
            this.oldCode.Multiline = true;
            this.oldCode.Name = "oldCode";
            this.oldCode.Size = new System.Drawing.Size(120, 74);
            this.oldCode.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(130, 13);
            this.label14.TabIndex = 45;
            this.label14.Text = "Where to restore old code";
            // 
            // End
            // 
            this.End.AutoSize = true;
            this.End.Location = new System.Drawing.Point(81, 28);
            this.End.Name = "End";
            this.End.Size = new System.Drawing.Size(44, 17);
            this.End.TabIndex = 44;
            this.End.TabStop = true;
            this.End.Text = "End";
            this.End.UseVisualStyleBackColor = true;
            // 
            // Start
            // 
            this.Start.AutoSize = true;
            this.Start.Location = new System.Drawing.Point(10, 27);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(47, 17);
            this.Start.TabIndex = 43;
            this.Start.TabStop = true;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            // 
            // generic
            // 
            this.generic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.generic.DropDownWidth = 160;
            this.generic.Enabled = false;
            this.generic.FormattingEnabled = true;
            this.generic.Items.AddRange(new object[] {
            "$008DC4 - NMI Start",
            "$008F25 - Get Coin",
            "$008F55 - Store Lives SB",
            "$008F7E - Store Coins SB",
            "$009E2C - Game Load",
            "$00F2E0 - Midway Collect",
            "$00F5B3 - Mario Hurt",
            "$00F606 - Mario Dead",
            "$00FEA8 - Shoot Fireball",
            "$01C561 - Get Mushroom",
            "$01C580 - Get Star",
            "$01C598 - Get Cape",
            "$01C5EC - Get Flower",
            "$0485BB - On Overworld",
            "$0491DB - Level Load"});
            this.generic.Location = new System.Drawing.Point(283, 57);
            this.generic.Name = "generic";
            this.generic.Size = new System.Drawing.Size(160, 21);
            this.generic.TabIndex = 42;
            this.generic.SelectedIndexChanged += new System.EventHandler(this.generic_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.End);
            this.groupBox1.Controls.Add(this.Start);
            this.groupBox1.Location = new System.Drawing.Point(265, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 50);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // select
            // 
            this.select.AutoSize = true;
            this.select.Location = new System.Drawing.Point(286, 34);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(129, 17);
            this.select.TabIndex = 47;
            this.select.Text = "Select generic routine";
            this.select.UseVisualStyleBackColor = true;
            this.select.CheckedChanged += new System.EventHandler(this.select_CheckedChanged_1);
            // 
            // PatchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 170);
            this.Controls.Add(this.select);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.generic);
            this.Controls.Add(this.oldCode);
            this.Controls.Add(this.noBytes);
            this.Controls.Add(this.hackSpot);
            this.Controls.Add(this.pjName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatchDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Patch..";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox pjName;
        public System.Windows.Forms.TextBox hackSpot;
        public System.Windows.Forms.TextBox noBytes;
        public System.Windows.Forms.TextBox oldCode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton End;
        private System.Windows.Forms.RadioButton Start;
        private System.Windows.Forms.ComboBox generic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox select;
    }
}