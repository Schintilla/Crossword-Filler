namespace Crossword_Filler
{
	partial class CrosswordForm
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnLoad2 = new System.Windows.Forms.Button();
            this.txtJsonOutput = new System.Windows.Forms.TextBox();
            this.rtbAcrossClues = new System.Windows.Forms.RichTextBox();
            this.rtbDownClues = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnSavePuz = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtJsonOutput2 = new System.Windows.Forms.TextBox();
            this.BtnRawRTF = new System.Windows.Forms.Button();
            this.DGVSolution1 = new System.Windows.Forms.DataGridView();
            this.DGVUser1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbAcrossClues2 = new System.Windows.Forms.RichTextBox();
            this.rtbDownClues2 = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DGVSolution2 = new System.Windows.Forms.DataGridView();
            this.DGVUser2 = new System.Windows.Forms.DataGridView();
            this.BtnRawRTF2 = new System.Windows.Forms.Button();
            this.LblUserGrid1 = new System.Windows.Forms.Label();
            this.LblUserGrid2 = new System.Windows.Forms.Label();
            this.LblSolGrid1 = new System.Windows.Forms.Label();
            this.LblSolGrid2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.hexViewerRichTextBox = new System.Windows.Forms.RichTextBox();
            this.hexViewerRichTextBox2 = new System.Windows.Forms.RichTextBox();
            this.BtnShowClueHex = new System.Windows.Forms.Button();
            this.BtnParseHex = new System.Windows.Forms.Button();
            this.BtnParseHexAcross = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVSolution1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVUser1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVSolution2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVUser2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(34, 473);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(104, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnLoad2
            // 
            this.btnLoad2.Location = new System.Drawing.Point(609, 473);
            this.btnLoad2.Name = "btnLoad2";
            this.btnLoad2.Size = new System.Drawing.Size(104, 23);
            this.btnLoad2.TabIndex = 13;
            this.btnLoad2.Text = "Load";
            this.btnLoad2.Click += new System.EventHandler(this.btnLoad2_Click);
            // 
            // txtJsonOutput
            // 
            this.txtJsonOutput.Location = new System.Drawing.Point(13, 20);
            this.txtJsonOutput.Multiline = true;
            this.txtJsonOutput.Name = "txtJsonOutput";
            this.txtJsonOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtJsonOutput.Size = new System.Drawing.Size(166, 447);
            this.txtJsonOutput.TabIndex = 4;
            // 
            // rtbAcrossClues
            // 
            this.rtbAcrossClues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbAcrossClues.CausesValidation = false;
            this.rtbAcrossClues.HideSelection = false;
            this.rtbAcrossClues.Location = new System.Drawing.Point(185, 127);
            this.rtbAcrossClues.Margin = new System.Windows.Forms.Padding(0);
            this.rtbAcrossClues.Name = "rtbAcrossClues";
            this.rtbAcrossClues.Size = new System.Drawing.Size(187, 160);
            this.rtbAcrossClues.TabIndex = 5;
            this.rtbAcrossClues.Text = "";
            // 
            // rtbDownClues
            // 
            this.rtbDownClues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbDownClues.Location = new System.Drawing.Point(185, 306);
            this.rtbDownClues.Margin = new System.Windows.Forms.Padding(0);
            this.rtbDownClues.Name = "rtbDownClues";
            this.rtbDownClues.Size = new System.Drawing.Size(187, 160);
            this.rtbDownClues.TabIndex = 6;
            this.rtbDownClues.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSavePuz
            // 
            this.btnSavePuz.Location = new System.Drawing.Point(522, 526);
            this.btnSavePuz.Name = "btnSavePuz";
            this.btnSavePuz.Size = new System.Drawing.Size(75, 29);
            this.btnSavePuz.TabIndex = 8;
            this.btnSavePuz.Text = "SaveAs";
            this.btnSavePuz.UseVisualStyleBackColor = true;
            this.btnSavePuz.Click += new System.EventHandler(this.btnSavePuz_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(225, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Load puz or ipuz";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(192, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "ACROSS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(192, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "DOWN";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtJsonOutput2
            // 
            this.txtJsonOutput2.Location = new System.Drawing.Point(579, 20);
            this.txtJsonOutput2.Multiline = true;
            this.txtJsonOutput2.Name = "txtJsonOutput2";
            this.txtJsonOutput2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtJsonOutput2.Size = new System.Drawing.Size(166, 447);
            this.txtJsonOutput2.TabIndex = 19;
            // 
            // BtnRawRTF
            // 
            this.BtnRawRTF.Location = new System.Drawing.Point(297, 473);
            this.BtnRawRTF.Name = "BtnRawRTF";
            this.BtnRawRTF.Size = new System.Drawing.Size(123, 23);
            this.BtnRawRTF.TabIndex = 20;
            this.BtnRawRTF.Text = "Down Extracted Text";
            this.BtnRawRTF.Click += new System.EventHandler(this.BtnRawRTF_Click);
            // 
            // DGVSolution1
            // 
            this.DGVSolution1.AllowUserToAddRows = false;
            this.DGVSolution1.AllowUserToDeleteRows = false;
            this.DGVSolution1.AllowUserToResizeColumns = false;
            this.DGVSolution1.AllowUserToResizeRows = false;
            this.DGVSolution1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVSolution1.Location = new System.Drawing.Point(379, 311);
            this.DGVSolution1.Name = "DGVSolution1";
            this.DGVSolution1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DGVSolution1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGVSolution1.Size = new System.Drawing.Size(184, 150);
            this.DGVSolution1.TabIndex = 22;
            // 
            // DGVUser1
            // 
            this.DGVUser1.AllowUserToAddRows = false;
            this.DGVUser1.AllowUserToDeleteRows = false;
            this.DGVUser1.AllowUserToResizeColumns = false;
            this.DGVUser1.AllowUserToResizeRows = false;
            this.DGVUser1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVUser1.Location = new System.Drawing.Point(379, 131);
            this.DGVUser1.Name = "DGVUser1";
            this.DGVUser1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DGVUser1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGVUser1.Size = new System.Drawing.Size(184, 150);
            this.DGVUser1.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(801, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "Load puz or ipuz";
            // 
            // rtbAcrossClues2
            // 
            this.rtbAcrossClues2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbAcrossClues2.CausesValidation = false;
            this.rtbAcrossClues2.HideSelection = false;
            this.rtbAcrossClues2.Location = new System.Drawing.Point(751, 129);
            this.rtbAcrossClues2.Margin = new System.Windows.Forms.Padding(0);
            this.rtbAcrossClues2.Name = "rtbAcrossClues2";
            this.rtbAcrossClues2.Size = new System.Drawing.Size(187, 160);
            this.rtbAcrossClues2.TabIndex = 5;
            this.rtbAcrossClues2.Text = "";
            // 
            // rtbDownClues2
            // 
            this.rtbDownClues2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbDownClues2.Location = new System.Drawing.Point(751, 309);
            this.rtbDownClues2.Margin = new System.Windows.Forms.Padding(0);
            this.rtbDownClues2.Name = "rtbDownClues2";
            this.rtbDownClues2.Size = new System.Drawing.Size(187, 160);
            this.rtbDownClues2.TabIndex = 6;
            this.rtbDownClues2.Text = "";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(751, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "ACROSS";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(751, 290);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 16);
            this.label6.TabIndex = 18;
            this.label6.Text = "DOWN";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DGVSolution2
            // 
            this.DGVSolution2.AllowUserToAddRows = false;
            this.DGVSolution2.AllowUserToDeleteRows = false;
            this.DGVSolution2.AllowUserToResizeColumns = false;
            this.DGVSolution2.AllowUserToResizeRows = false;
            this.DGVSolution2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVSolution2.Location = new System.Drawing.Point(946, 311);
            this.DGVSolution2.Name = "DGVSolution2";
            this.DGVSolution2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DGVSolution2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGVSolution2.Size = new System.Drawing.Size(184, 150);
            this.DGVSolution2.TabIndex = 22;
            // 
            // DGVUser2
            // 
            this.DGVUser2.AllowUserToAddRows = false;
            this.DGVUser2.AllowUserToDeleteRows = false;
            this.DGVUser2.AllowUserToResizeColumns = false;
            this.DGVUser2.AllowUserToResizeRows = false;
            this.DGVUser2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVUser2.Location = new System.Drawing.Point(946, 131);
            this.DGVUser2.Name = "DGVUser2";
            this.DGVUser2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DGVUser2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGVUser2.Size = new System.Drawing.Size(184, 150);
            this.DGVUser2.TabIndex = 23;
            // 
            // BtnRawRTF2
            // 
            this.BtnRawRTF2.Location = new System.Drawing.Point(823, 474);
            this.BtnRawRTF2.Name = "BtnRawRTF2";
            this.BtnRawRTF2.Size = new System.Drawing.Size(75, 23);
            this.BtnRawRTF2.TabIndex = 20;
            this.BtnRawRTF2.Text = "Raw RTF";
            this.BtnRawRTF2.Click += new System.EventHandler(this.BtnRawRTF2_Click);
            // 
            // LblUserGrid1
            // 
            this.LblUserGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LblUserGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblUserGrid1.Location = new System.Drawing.Point(382, 113);
            this.LblUserGrid1.Name = "LblUserGrid1";
            this.LblUserGrid1.Size = new System.Drawing.Size(187, 16);
            this.LblUserGrid1.TabIndex = 25;
            this.LblUserGrid1.Text = "PuzUserGrid";
            this.LblUserGrid1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblUserGrid2
            // 
            this.LblUserGrid2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LblUserGrid2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblUserGrid2.Location = new System.Drawing.Point(946, 113);
            this.LblUserGrid2.Name = "LblUserGrid2";
            this.LblUserGrid2.Size = new System.Drawing.Size(187, 16);
            this.LblUserGrid2.TabIndex = 26;
            this.LblUserGrid2.Text = "User - Not Defined";
            this.LblUserGrid2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblSolGrid1
            // 
            this.LblSolGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LblSolGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSolGrid1.Location = new System.Drawing.Point(382, 292);
            this.LblSolGrid1.Name = "LblSolGrid1";
            this.LblSolGrid1.Size = new System.Drawing.Size(187, 16);
            this.LblSolGrid1.TabIndex = 27;
            this.LblSolGrid1.Text = "PuzSolutionGrid";
            this.LblSolGrid1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblSolGrid2
            // 
            this.LblSolGrid2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LblSolGrid2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSolGrid2.Location = new System.Drawing.Point(946, 292);
            this.LblSolGrid2.Name = "LblSolGrid2";
            this.LblSolGrid2.Size = new System.Drawing.Size(187, 16);
            this.LblSolGrid2.TabIndex = 28;
            this.LblSolGrid2.Text = "Solution";
            this.LblSolGrid2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label11.Location = new System.Drawing.Point(3, 2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(566, 502);
            this.label11.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label7.Location = new System.Drawing.Point(573, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(566, 502);
            this.label7.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(518, 507);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 16);
            this.label12.TabIndex = 31;
            this.label12.Text = "Active puzzle";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hexViewerRichTextBox
            // 
            this.hexViewerRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexViewerRichTextBox.Font = new System.Drawing.Font("Baskerville Old Face", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexViewerRichTextBox.Location = new System.Drawing.Point(185, 20);
            this.hexViewerRichTextBox.Name = "hexViewerRichTextBox";
            this.hexViewerRichTextBox.Size = new System.Drawing.Size(378, 90);
            this.hexViewerRichTextBox.TabIndex = 34;
            this.hexViewerRichTextBox.Text = "";
            // 
            // hexViewerRichTextBox2
            // 
            this.hexViewerRichTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexViewerRichTextBox2.Location = new System.Drawing.Point(752, 21);
            this.hexViewerRichTextBox2.Name = "hexViewerRichTextBox2";
            this.hexViewerRichTextBox2.Size = new System.Drawing.Size(378, 90);
            this.hexViewerRichTextBox2.TabIndex = 35;
            this.hexViewerRichTextBox2.Text = "";
            // 
            // BtnShowClueHex
            // 
            this.BtnShowClueHex.Location = new System.Drawing.Point(185, 473);
            this.BtnShowClueHex.Name = "BtnShowClueHex";
            this.BtnShowClueHex.Size = new System.Drawing.Size(106, 23);
            this.BtnShowClueHex.TabIndex = 36;
            this.BtnShowClueHex.Text = "Raw Clues Bytes";
            this.BtnShowClueHex.Click += new System.EventHandler(this.BtnShowClueHex_Click);
            // 
            // BtnParseHex
            // 
            this.BtnParseHex.Location = new System.Drawing.Point(331, 507);
            this.BtnParseHex.Name = "BtnParseHex";
            this.BtnParseHex.Size = new System.Drawing.Size(152, 23);
            this.BtnParseHex.TabIndex = 37;
            this.BtnParseHex.Text = "Down Formatted Clue Bytes";
            this.BtnParseHex.Click += new System.EventHandler(this.BtnParseHex_Click);
            // 
            // BtnParseHexAcross
            // 
            this.BtnParseHexAcross.Location = new System.Drawing.Point(158, 507);
            this.BtnParseHexAcross.Name = "BtnParseHexAcross";
            this.BtnParseHexAcross.Size = new System.Drawing.Size(167, 23);
            this.BtnParseHexAcross.TabIndex = 38;
            this.BtnParseHexAcross.Text = "Across Formatted Clue Bytes";
            this.BtnParseHexAcross.Click += new System.EventHandler(this.BtnParseHexAcross_Click);
            // 
            // CrosswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1137, 562);
            this.Controls.Add(this.BtnParseHexAcross);
            this.Controls.Add(this.BtnParseHex);
            this.Controls.Add(this.BtnShowClueHex);
            this.Controls.Add(this.hexViewerRichTextBox2);
            this.Controls.Add(this.hexViewerRichTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.LblSolGrid2);
            this.Controls.Add(this.LblSolGrid1);
            this.Controls.Add(this.LblUserGrid2);
            this.Controls.Add(this.LblUserGrid1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DGVUser2);
            this.Controls.Add(this.DGVSolution2);
            this.Controls.Add(this.DGVUser1);
            this.Controls.Add(this.DGVSolution1);
            this.Controls.Add(this.BtnRawRTF2);
            this.Controls.Add(this.BtnRawRTF);
            this.Controls.Add(this.txtJsonOutput2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSavePuz);
            this.Controls.Add(this.rtbDownClues2);
            this.Controls.Add(this.rtbAcrossClues2);
            this.Controls.Add(this.rtbDownClues);
            this.Controls.Add(this.rtbAcrossClues);
            this.Controls.Add(this.txtJsonOutput);
            this.Controls.Add(this.btnLoad2);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Name = "CrosswordForm";
            this.Text = "Puz & IPuz Import & Export";
            this.Load += new System.EventHandler(this.CrosswordForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVSolution1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVUser1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVSolution2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVUser2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Button btnLoad2;
		private System.Windows.Forms.TextBox txtJsonOutput;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button btnSavePuz;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtJsonOutput2;
		private System.Windows.Forms.Button BtnRawRTF;
		private System.Windows.Forms.DataGridView DGVSolution1;
		private System.Windows.Forms.DataGridView DGVUser1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DataGridView DGVSolution2;
		private System.Windows.Forms.DataGridView DGVUser2;
		private System.Windows.Forms.Button BtnRawRTF2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label LblUserGrid1;
		private System.Windows.Forms.Label LblUserGrid2;
		private System.Windows.Forms.Label LblSolGrid1;
		private System.Windows.Forms.Label LblSolGrid2;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.RichTextBox hexViewerRichTextBox;
		private System.Windows.Forms.RichTextBox hexViewerRichTextBox2;
		public System.Windows.Forms.RichTextBox rtbAcrossClues;
		public System.Windows.Forms.RichTextBox rtbDownClues;
		public System.Windows.Forms.RichTextBox rtbAcrossClues2;
		public System.Windows.Forms.RichTextBox rtbDownClues2;
		private System.Windows.Forms.Button BtnShowClueHex;
		private System.Windows.Forms.Button BtnParseHex;
		private System.Windows.Forms.Button BtnParseHexAcross;
	}
}