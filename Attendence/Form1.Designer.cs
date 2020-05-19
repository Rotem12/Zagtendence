namespace Attendence
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.captureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getCopyPastaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copy2ColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.buttonClipboard = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.textBoxOops = new System.Windows.Forms.TextBox();
            this.buttonOopsClear = new System.Windows.Forms.Button();
            this.pictureBoxSS = new System.Windows.Forms.PictureBox();
            this.checkBoxSaveSS = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonToggleList = new System.Windows.Forms.Button();
            this.textBoxFull = new System.Windows.Forms.TextBox();
            this.numTolerance = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxGrey = new System.Windows.Forms.CheckBox();
            this.checkBoxScores = new System.Windows.Forms.CheckBox();
            this.scoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTolerance)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.captureToolStripMenuItem,
            this.getCopyPastaToolStripMenuItem,
            this.copy2ColumnToolStripMenuItem,
            this.scoresToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(356, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // captureToolStripMenuItem
            // 
            this.captureToolStripMenuItem.Name = "captureToolStripMenuItem";
            this.captureToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.captureToolStripMenuItem.Text = "Capture";
            this.captureToolStripMenuItem.Click += new System.EventHandler(this.captureToolStripMenuItem_Click);
            // 
            // getCopyPastaToolStripMenuItem
            // 
            this.getCopyPastaToolStripMenuItem.Name = "getCopyPastaToolStripMenuItem";
            this.getCopyPastaToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.getCopyPastaToolStripMenuItem.Text = "Get Column";
            this.getCopyPastaToolStripMenuItem.Click += new System.EventHandler(this.getCopyPastaToolStripMenuItem_Click);
            // 
            // copy2ColumnToolStripMenuItem
            // 
            this.copy2ColumnToolStripMenuItem.Name = "copy2ColumnToolStripMenuItem";
            this.copy2ColumnToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.copy2ColumnToolStripMenuItem.Text = "Add From Copy";
            this.copy2ColumnToolStripMenuItem.Click += new System.EventHandler(this.copy2ColumnToolStripMenuItem_Click);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResult.BackColor = System.Drawing.Color.White;
            this.textBoxResult.Location = new System.Drawing.Point(0, 96);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResult.Size = new System.Drawing.Size(173, 225);
            this.textBoxResult.TabIndex = 8;
            this.textBoxResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Count:";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(56, 45);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(13, 13);
            this.labelCount.TabIndex = 10;
            this.labelCount.Text = "0";
            // 
            // buttonClipboard
            // 
            this.buttonClipboard.Image = global::Attendence.Properties.Resources.iconfinder_clipboard_sign_out_25957;
            this.buttonClipboard.Location = new System.Drawing.Point(94, 71);
            this.buttonClipboard.Name = "buttonClipboard";
            this.buttonClipboard.Size = new System.Drawing.Size(25, 25);
            this.buttonClipboard.TabIndex = 11;
            this.toolTip.SetToolTip(this.buttonClipboard, "Copy To Clipboard");
            this.buttonClipboard.UseVisualStyleBackColor = true;
            this.buttonClipboard.Click += new System.EventHandler(this.buttonClipboard_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(121, 71);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(53, 25);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Clear";
            this.toolTip.SetToolTip(this.buttonClear, "Clear Attendence List");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // textBoxOops
            // 
            this.textBoxOops.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOops.Location = new System.Drawing.Point(174, 96);
            this.textBoxOops.Multiline = true;
            this.textBoxOops.Name = "textBoxOops";
            this.textBoxOops.ReadOnly = true;
            this.textBoxOops.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOops.Size = new System.Drawing.Size(182, 112);
            this.textBoxOops.TabIndex = 13;
            // 
            // buttonOopsClear
            // 
            this.buttonOopsClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOopsClear.Location = new System.Drawing.Point(303, 71);
            this.buttonOopsClear.Name = "buttonOopsClear";
            this.buttonOopsClear.Size = new System.Drawing.Size(53, 25);
            this.buttonOopsClear.TabIndex = 14;
            this.buttonOopsClear.Text = "Clear";
            this.toolTip.SetToolTip(this.buttonOopsClear, "Clear Garbage Text");
            this.buttonOopsClear.UseVisualStyleBackColor = true;
            this.buttonOopsClear.Click += new System.EventHandler(this.buttonOopsClear_Click);
            // 
            // pictureBoxSS
            // 
            this.pictureBoxSS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSS.Location = new System.Drawing.Point(174, 209);
            this.pictureBoxSS.Name = "pictureBoxSS";
            this.pictureBoxSS.Size = new System.Drawing.Size(182, 112);
            this.pictureBoxSS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSS.TabIndex = 15;
            this.pictureBoxSS.TabStop = false;
            // 
            // checkBoxSaveSS
            // 
            this.checkBoxSaveSS.AutoSize = true;
            this.checkBoxSaveSS.Checked = true;
            this.checkBoxSaveSS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSaveSS.Location = new System.Drawing.Point(236, 53);
            this.checkBoxSaveSS.Name = "checkBoxSaveSS";
            this.checkBoxSaveSS.Size = new System.Drawing.Size(108, 17);
            this.checkBoxSaveSS.TabIndex = 17;
            this.checkBoxSaveSS.Text = "Save Screenshot";
            this.checkBoxSaveSS.UseVisualStyleBackColor = true;
            // 
            // buttonToggleList
            // 
            this.buttonToggleList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonToggleList.Location = new System.Drawing.Point(0, 71);
            this.buttonToggleList.Name = "buttonToggleList";
            this.buttonToggleList.Size = new System.Drawing.Size(53, 25);
            this.buttonToggleList.TabIndex = 18;
            this.buttonToggleList.Text = "Recent";
            this.toolTip.SetToolTip(this.buttonToggleList, "Display Full/Recent List");
            this.buttonToggleList.UseVisualStyleBackColor = true;
            this.buttonToggleList.Click += new System.EventHandler(this.buttonToggleList_Click);
            // 
            // textBoxFull
            // 
            this.textBoxFull.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFull.BackColor = System.Drawing.Color.White;
            this.textBoxFull.Location = new System.Drawing.Point(0, 96);
            this.textBoxFull.Multiline = true;
            this.textBoxFull.Name = "textBoxFull";
            this.textBoxFull.ReadOnly = true;
            this.textBoxFull.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFull.Size = new System.Drawing.Size(173, 225);
            this.textBoxFull.TabIndex = 19;
            this.textBoxFull.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // numTolerance
            // 
            this.numTolerance.Location = new System.Drawing.Point(256, 73);
            this.numTolerance.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTolerance.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTolerance.Name = "numTolerance";
            this.numTolerance.Size = new System.Drawing.Size(41, 20);
            this.numTolerance.TabIndex = 20;
            this.numTolerance.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Tolerance:";
            // 
            // checkBoxGrey
            // 
            this.checkBoxGrey.AutoSize = true;
            this.checkBoxGrey.Location = new System.Drawing.Point(236, 37);
            this.checkBoxGrey.Name = "checkBoxGrey";
            this.checkBoxGrey.Size = new System.Drawing.Size(101, 17);
            this.checkBoxGrey.TabIndex = 16;
            this.checkBoxGrey.Text = "Greyscale Scan";
            this.checkBoxGrey.UseVisualStyleBackColor = true;
            // 
            // checkBoxScores
            // 
            this.checkBoxScores.AutoSize = true;
            this.checkBoxScores.Location = new System.Drawing.Point(140, 37);
            this.checkBoxScores.Name = "checkBoxScores";
            this.checkBoxScores.Size = new System.Drawing.Size(87, 17);
            this.checkBoxScores.TabIndex = 22;
            this.checkBoxScores.Text = "Scan Scores";
            this.checkBoxScores.UseVisualStyleBackColor = true;
            // 
            // scoresToolStripMenuItem
            // 
            this.scoresToolStripMenuItem.Name = "scoresToolStripMenuItem";
            this.scoresToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.scoresToolStripMenuItem.Text = "Get Scores";
            this.scoresToolStripMenuItem.Click += new System.EventHandler(this.scoresToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 321);
            this.Controls.Add(this.checkBoxScores);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numTolerance);
            this.Controls.Add(this.buttonToggleList);
            this.Controls.Add(this.checkBoxSaveSS);
            this.Controls.Add(this.checkBoxGrey);
            this.Controls.Add(this.pictureBoxSS);
            this.Controls.Add(this.buttonOopsClear);
            this.Controls.Add(this.textBoxOops);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonClipboard);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.textBoxFull);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Attendence - V1.0.5";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTolerance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem captureToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.ToolStripMenuItem getCopyPastaToolStripMenuItem;
        private System.Windows.Forms.Button buttonClipboard;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TextBox textBoxOops;
        private System.Windows.Forms.Button buttonOopsClear;
        private System.Windows.Forms.PictureBox pictureBoxSS;
        private System.Windows.Forms.CheckBox checkBoxSaveSS;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonToggleList;
        private System.Windows.Forms.TextBox textBoxFull;
        private System.Windows.Forms.NumericUpDown numTolerance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem copy2ColumnToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxGrey;
        private System.Windows.Forms.CheckBox checkBoxScores;
        private System.Windows.Forms.ToolStripMenuItem scoresToolStripMenuItem;
    }
}

