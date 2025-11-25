namespace Crossword_Filler
{
	partial class ImportSelect
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
			components = new System.ComponentModel.Container();
			PictureBoxClues = new System.Windows.Forms.PictureBox();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			label2 = new System.Windows.Forms.Label();
			tbSolnStatus = new System.Windows.Forms.TextBox();
			BtnNext = new System.Windows.Forms.Button();
			BtnPrev = new System.Windows.Forms.Button();
			tbTotalCW = new System.Windows.Forms.TextBox();
			tbCWNo = new System.Windows.Forms.TextBox();
			bClose = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			referenceTextBox = new System.Windows.Forms.TextBox();
			BtnLoad = new System.Windows.Forms.Button();
			BtnImportCurrent = new System.Windows.Forms.Button();
			BtnImportAll = new System.Windows.Forms.Button();
			BtnReplace = new System.Windows.Forms.Button();
			NoCrossword = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			NoClues = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			CbSolns = new System.Windows.Forms.CheckBox();
			CbHints = new System.Windows.Forms.CheckBox();
			TbRichTextAcrossClues = new System.Windows.Forms.RichTextBox();
			TbRichTextDownClues = new System.Windows.Forms.RichTextBox();
			TbAuthor = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			TbTitle = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)PictureBoxClues).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			// 
			// PictureBoxClues
			// 
			PictureBoxClues.BackColor = System.Drawing.Color.White;
			PictureBoxClues.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			PictureBoxClues.Location = new System.Drawing.Point(722, 27);
			PictureBoxClues.Name = "PictureBoxClues";
			PictureBoxClues.Size = new System.Drawing.Size(363, 388);
			PictureBoxClues.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			PictureBoxClues.TabIndex = 13;
			PictureBoxClues.TabStop = false;
			// 
			// dataGridView1
			// 
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dataGridView1.ColumnHeadersVisible = false;
			dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
			dataGridView1.Location = new System.Drawing.Point(168, 6);
			dataGridView1.MultiSelect = false;
			dataGridView1.Name = "dataGridView1";
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
			dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			dataGridView1.ShowCellErrors = false;
			dataGridView1.ShowCellToolTips = false;
			dataGridView1.ShowEditingIcon = false;
			dataGridView1.ShowRowErrors = false;
			dataGridView1.Size = new System.Drawing.Size(548, 409);
			dataGridView1.TabIndex = 12;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label2.Location = new System.Drawing.Point(8, 295);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(86, 15);
			label2.TabIndex = 49;
			label2.Text = "Solved Status:";
			// 
			// tbSolnStatus
			// 
			tbSolnStatus.BackColor = System.Drawing.SystemColors.InactiveBorder;
			tbSolnStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			tbSolnStatus.Location = new System.Drawing.Point(100, 292);
			tbSolnStatus.Name = "tbSolnStatus";
			tbSolnStatus.Size = new System.Drawing.Size(60, 22);
			tbSolnStatus.TabIndex = 47;
			tbSolnStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// BtnNext
			// 
			BtnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnNext.Location = new System.Drawing.Point(112, 318);
			BtnNext.Name = "BtnNext";
			BtnNext.RightToLeft = System.Windows.Forms.RightToLeft.No;
			BtnNext.Size = new System.Drawing.Size(50, 54);
			BtnNext.TabIndex = 46;
			BtnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			BtnNext.UseVisualStyleBackColor = true;
			BtnNext.Click += BtnNext_Click;
			// 
			// BtnPrev
			// 
			BtnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnPrev.Location = new System.Drawing.Point(7, 318);
			BtnPrev.Name = "BtnPrev";
			BtnPrev.Size = new System.Drawing.Size(50, 54);
			BtnPrev.TabIndex = 45;
			BtnPrev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			BtnPrev.UseVisualStyleBackColor = true;
			BtnPrev.Click += BtnPrev_Click;
			// 
			// tbTotalCW
			// 
			tbTotalCW.BackColor = System.Drawing.SystemColors.InactiveBorder;
			tbTotalCW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			tbTotalCW.Location = new System.Drawing.Point(61, 349);
			tbTotalCW.Name = "tbTotalCW";
			tbTotalCW.Size = new System.Drawing.Size(45, 22);
			tbTotalCW.TabIndex = 44;
			tbTotalCW.TabStop = false;
			tbTotalCW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tbCWNo
			// 
			tbCWNo.BackColor = System.Drawing.SystemColors.InactiveBorder;
			tbCWNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			tbCWNo.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			tbCWNo.Location = new System.Drawing.Point(61, 320);
			tbCWNo.Name = "tbCWNo";
			tbCWNo.Size = new System.Drawing.Size(45, 26);
			tbCWNo.TabIndex = 43;
			tbCWNo.TabStop = false;
			tbCWNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// bClose
			// 
			bClose.Location = new System.Drawing.Point(36, 375);
			bClose.Name = "bClose";
			bClose.Size = new System.Drawing.Size(92, 33);
			bClose.TabIndex = 42;
			bClose.Text = "Close";
			bClose.UseVisualStyleBackColor = true;
			bClose.Click += bClose_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label1.Location = new System.Drawing.Point(8, 247);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(30, 15);
			label1.TabIndex = 52;
			label1.Text = "Ref:";
			// 
			// referenceTextBox
			// 
			referenceTextBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
			referenceTextBox.Location = new System.Drawing.Point(61, 243);
			referenceTextBox.Name = "referenceTextBox";
			referenceTextBox.Size = new System.Drawing.Size(96, 23);
			referenceTextBox.TabIndex = 51;
			// 
			// BtnLoad
			// 
			BtnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			BtnLoad.Location = new System.Drawing.Point(36, 27);
			BtnLoad.Name = "BtnLoad";
			BtnLoad.Size = new System.Drawing.Size(98, 50);
			BtnLoad.TabIndex = 53;
			toolTip1.SetToolTip(BtnLoad, "Load cjz, puz or ipuz file to import");
			BtnLoad.UseVisualStyleBackColor = true;
			BtnLoad.Click += BtnLoad_Click;
			// 
			// BtnImportCurrent
			// 
			BtnImportCurrent.Location = new System.Drawing.Point(36, 121);
			BtnImportCurrent.Name = "BtnImportCurrent";
			BtnImportCurrent.Size = new System.Drawing.Size(98, 23);
			BtnImportCurrent.TabIndex = 54;
			BtnImportCurrent.Text = "Append Current";
			toolTip1.SetToolTip(BtnImportCurrent, "Will need to save first if not already saved");
			BtnImportCurrent.UseVisualStyleBackColor = true;
			BtnImportCurrent.Click += BtnImportCurrent_Click;
			// 
			// BtnImportAll
			// 
			BtnImportAll.Location = new System.Drawing.Point(36, 166);
			BtnImportAll.Name = "BtnImportAll";
			BtnImportAll.Size = new System.Drawing.Size(98, 24);
			BtnImportAll.TabIndex = 55;
			BtnImportAll.Text = "Append All";
			toolTip1.SetToolTip(BtnImportAll, "Will add all the crosswords but flag if the Reference already exists \r\nWill need to save first if not already saved");
			BtnImportAll.UseVisualStyleBackColor = true;
			BtnImportAll.Click += BtnImportAll_Click;
			// 
			// BtnReplace
			// 
			BtnReplace.Location = new System.Drawing.Point(36, 97);
			BtnReplace.Name = "BtnReplace";
			BtnReplace.Size = new System.Drawing.Size(98, 23);
			BtnReplace.TabIndex = 56;
			BtnReplace.Text = "Replace Current";
			toolTip1.SetToolTip(BtnReplace, "Will need to save first if not already saved");
			BtnReplace.UseVisualStyleBackColor = true;
			BtnReplace.Click += BtnReplace_Click;
			// 
			// NoCrossword
			// 
			NoCrossword.AutoSize = true;
			NoCrossword.BackColor = System.Drawing.Color.White;
			NoCrossword.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			NoCrossword.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			NoCrossword.Location = new System.Drawing.Point(324, 199);
			NoCrossword.Name = "NoCrossword";
			NoCrossword.Size = new System.Drawing.Size(237, 40);
			NoCrossword.TabIndex = 57;
			NoCrossword.Text = "Load Crossword";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(8, 9);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(97, 15);
			label5.TabIndex = 58;
			label5.Text = "Load Crossword:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label3.Location = new System.Drawing.Point(8, 80);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(95, 15);
			label3.TabIndex = 59;
			label3.Text = "Import Current:";
			// 
			// NoClues
			// 
			NoClues.AutoSize = true;
			NoClues.BackColor = System.Drawing.Color.White;
			NoClues.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			NoClues.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			NoClues.Location = new System.Drawing.Point(766, 199);
			NoClues.Name = "NoClues";
			NoClues.Size = new System.Drawing.Size(189, 37);
			NoClues.TabIndex = 61;
			NoClues.Text = "Load Clues";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label6.Location = new System.Drawing.Point(8, 149);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(66, 15);
			label6.TabIndex = 62;
			label6.Text = "Import All:";
			// 
			// CbSolns
			// 
			CbSolns.AutoSize = true;
			CbSolns.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			CbSolns.Location = new System.Drawing.Point(12, 271);
			CbSolns.Name = "CbSolns";
			CbSolns.Size = new System.Drawing.Size(77, 19);
			CbSolns.TabIndex = 68;
			CbSolns.Text = "Solutions";
			toolTip1.SetToolTip(CbSolns, "Indicates if puz or ipuz has embedded Solution or not");
			CbSolns.UseVisualStyleBackColor = true;
			CbSolns.Click += CbSolns_Click;
			// 
			// CbHints
			// 
			CbHints.AutoSize = true;
			CbHints.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			CbHints.Location = new System.Drawing.Point(96, 271);
			CbHints.Name = "CbHints";
			CbHints.Size = new System.Drawing.Size(55, 19);
			CbHints.TabIndex = 69;
			CbHints.Text = "Hints";
			toolTip1.SetToolTip(CbHints, "Indicates if puz or ipuz has embedded Hints or not");
			CbHints.UseVisualStyleBackColor = true;
			CbHints.Click += CbHints_Click;
			// 
			// TbRichTextAcrossClues
			// 
			TbRichTextAcrossClues.BorderStyle = System.Windows.Forms.BorderStyle.None;
			TbRichTextAcrossClues.Location = new System.Drawing.Point(724, 27);
			TbRichTextAcrossClues.Margin = new System.Windows.Forms.Padding(0);
			TbRichTextAcrossClues.Name = "TbRichTextAcrossClues";
			TbRichTextAcrossClues.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			TbRichTextAcrossClues.Size = new System.Drawing.Size(183, 388);
			TbRichTextAcrossClues.TabIndex = 63;
			TbRichTextAcrossClues.Text = "";
			// 
			// TbRichTextDownClues
			// 
			TbRichTextDownClues.BorderStyle = System.Windows.Forms.BorderStyle.None;
			TbRichTextDownClues.Location = new System.Drawing.Point(912, 27);
			TbRichTextDownClues.Margin = new System.Windows.Forms.Padding(0);
			TbRichTextDownClues.Name = "TbRichTextDownClues";
			TbRichTextDownClues.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			TbRichTextDownClues.Size = new System.Drawing.Size(183, 388);
			TbRichTextDownClues.TabIndex = 64;
			TbRichTextDownClues.Text = "";
			// 
			// TbAuthor
			// 
			TbAuthor.BackColor = System.Drawing.SystemColors.InactiveBorder;
			TbAuthor.Location = new System.Drawing.Point(61, 221);
			TbAuthor.Name = "TbAuthor";
			TbAuthor.Size = new System.Drawing.Size(96, 23);
			TbAuthor.TabIndex = 70;
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label9.Location = new System.Drawing.Point(8, 225);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(49, 15);
			label9.TabIndex = 71;
			label9.Text = "Author:";
			// 
			// label10
			// 
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
			label10.Location = new System.Drawing.Point(782, 7);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(66, 19);
			label10.TabIndex = 72;
			label10.Text = "ACROSS";
			// 
			// label11
			// 
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
			label11.Location = new System.Drawing.Point(978, 6);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(57, 19);
			label11.TabIndex = 73;
			label11.Text = "DOWN";
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label8.Location = new System.Drawing.Point(8, 203);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(35, 15);
			label8.TabIndex = 76;
			label8.Text = "Title:";
			// 
			// TbTitle
			// 
			TbTitle.BackColor = System.Drawing.SystemColors.InactiveBorder;
			TbTitle.Location = new System.Drawing.Point(61, 199);
			TbTitle.Name = "TbTitle";
			TbTitle.Size = new System.Drawing.Size(96, 23);
			TbTitle.TabIndex = 75;
			// 
			// ImportSelect
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
			ClientSize = new System.Drawing.Size(1100, 422);
			Controls.Add(label8);
			Controls.Add(TbTitle);
			Controls.Add(label11);
			Controls.Add(label10);
			Controls.Add(label9);
			Controls.Add(TbAuthor);
			Controls.Add(CbHints);
			Controls.Add(CbSolns);
			Controls.Add(TbRichTextDownClues);
			Controls.Add(TbRichTextAcrossClues);
			Controls.Add(label6);
			Controls.Add(NoClues);
			Controls.Add(label3);
			Controls.Add(label5);
			Controls.Add(NoCrossword);
			Controls.Add(BtnReplace);
			Controls.Add(BtnImportAll);
			Controls.Add(BtnImportCurrent);
			Controls.Add(BtnLoad);
			Controls.Add(label1);
			Controls.Add(referenceTextBox);
			Controls.Add(label2);
			Controls.Add(tbSolnStatus);
			Controls.Add(BtnNext);
			Controls.Add(BtnPrev);
			Controls.Add(tbTotalCW);
			Controls.Add(tbCWNo);
			Controls.Add(bClose);
			Controls.Add(PictureBoxClues);
			Controls.Add(dataGridView1);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			Name = "ImportSelect";
			SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Import Options";
			Load += ImportSelect_Load;
			((System.ComponentModel.ISupportInitialize)PictureBoxClues).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion

		public System.Windows.Forms.PictureBox PictureBoxClues;
		public System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbSolnStatus;
		private System.Windows.Forms.Button BtnNext;
		private System.Windows.Forms.Button BtnPrev;
		public System.Windows.Forms.TextBox tbTotalCW;
		private System.Windows.Forms.TextBox tbCWNo;
		private System.Windows.Forms.Button bClose;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.TextBox referenceTextBox;
		private System.Windows.Forms.Button BtnLoad;
		private System.Windows.Forms.Button BtnImportCurrent;
		private System.Windows.Forms.Button BtnImportAll;
		private System.Windows.Forms.Button BtnReplace;
		private System.Windows.Forms.Label NoCrossword;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label NoClues;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ToolTip toolTip1;
		public System.Windows.Forms.RichTextBox TbRichTextAcrossClues;
		public System.Windows.Forms.RichTextBox TbRichTextDownClues;
		private System.Windows.Forms.CheckBox CbSolns;
		private System.Windows.Forms.CheckBox CbHints;
		public System.Windows.Forms.TextBox TbAuthor;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label8;
		public System.Windows.Forms.TextBox textBox1;
		public System.Windows.Forms.TextBox TbTitle;
	}
}