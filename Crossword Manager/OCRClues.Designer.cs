namespace Crossword_Filler
{
	partial class OCRClues
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
			TbRichTextDownClues = new System.Windows.Forms.RichTextBox();
			TbRichTextAcrossClues = new System.Windows.Forms.RichTextBox();
			BtnScreenAcross = new System.Windows.Forms.Button();
			BtnScreenDown = new System.Windows.Forms.Button();
			BtnPictureBox = new System.Windows.Forms.Button();
			BtnClose = new System.Windows.Forms.Button();
			BtnClear = new System.Windows.Forms.Button();
			BtnFinalFormat = new System.Windows.Forms.Button();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			CbBracket = new System.Windows.Forms.CheckBox();
			BtnReset = new System.Windows.Forms.Button();
			chkMedian = new System.Windows.Forms.CheckBox();
			chkKMeans = new System.Windows.Forms.CheckBox();
			chkDenoise = new System.Windows.Forms.CheckBox();
			trackScale = new System.Windows.Forms.TrackBar();
			trackBlockSize = new System.Windows.Forms.TrackBar();
			trackC = new System.Windows.Forms.TrackBar();
			trackSharpen = new System.Windows.Forms.TrackBar();
			BtnShowLF = new System.Windows.Forms.Button();
			BtnRemoveLF = new System.Windows.Forms.Button();
			BtnSetDefault = new System.Windows.Forms.Button();
			BtnLoadDefault = new System.Windows.Forms.Button();
			BtnWordLengths = new System.Windows.Forms.Button();
			PictureBoxDown = new System.Windows.Forms.PictureBox();
			PictureBoxAcross = new System.Windows.Forms.PictureBox();
			chkClahe = new System.Windows.Forms.CheckBox();
			chkAdaptive = new System.Windows.Forms.CheckBox();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			LabMissingA = new System.Windows.Forms.Label();
			groupBox1 = new System.Windows.Forms.GroupBox();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			RadioWinOCR = new System.Windows.Forms.RadioButton();
			RadioPSMBlock = new System.Windows.Forms.RadioButton();
			RadioPSMColumn = new System.Windows.Forms.RadioButton();
			RadioPSMAuto = new System.Windows.Forms.RadioButton();
			groupBox2 = new System.Windows.Forms.GroupBox();
			lblScale = new System.Windows.Forms.Label();
			BtnInfo5 = new System.Windows.Forms.Button();
			BtnInfo4 = new System.Windows.Forms.Button();
			BtnIPUpdate = new System.Windows.Forms.Button();
			BtnUpdateIP = new System.Windows.Forms.Button();
			BtnPreProcessing = new System.Windows.Forms.Button();
			lblSharpen = new System.Windows.Forms.Label();
			lblBlockSize = new System.Windows.Forms.Label();
			lblC = new System.Windows.Forms.Label();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			BtnNumbers = new System.Windows.Forms.Button();
			BtnInfo2 = new System.Windows.Forms.Button();
			BtnInfo1 = new System.Windows.Forms.Button();
			LblSpellChkA = new System.Windows.Forms.Label();
			LblMissingQtyA = new System.Windows.Forms.Label();
			LblMissingWdLenA = new System.Windows.Forms.Label();
			LblLFErrorA = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			BtnCheckAllErrors = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)trackScale).BeginInit();
			((System.ComponentModel.ISupportInitialize)trackBlockSize).BeginInit();
			((System.ComponentModel.ISupportInitialize)trackC).BeginInit();
			((System.ComponentModel.ISupportInitialize)trackSharpen).BeginInit();
			((System.ComponentModel.ISupportInitialize)PictureBoxDown).BeginInit();
			((System.ComponentModel.ISupportInitialize)PictureBoxAcross).BeginInit();
			groupBox1.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			groupBox2.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			SuspendLayout();
			// 
			// TbRichTextDownClues
			// 
			TbRichTextDownClues.BorderStyle = System.Windows.Forms.BorderStyle.None;
			TbRichTextDownClues.Location = new System.Drawing.Point(388, 27);
			TbRichTextDownClues.Margin = new System.Windows.Forms.Padding(0);
			TbRichTextDownClues.Name = "TbRichTextDownClues";
			TbRichTextDownClues.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			TbRichTextDownClues.Size = new System.Drawing.Size(183, 465);
			TbRichTextDownClues.TabIndex = 28;
			TbRichTextDownClues.Text = "";
			TbRichTextDownClues.Click += TbRichTextDownClues_Click;
			// 
			// TbRichTextAcrossClues
			// 
			TbRichTextAcrossClues.BorderStyle = System.Windows.Forms.BorderStyle.None;
			TbRichTextAcrossClues.Location = new System.Drawing.Point(9, 27);
			TbRichTextAcrossClues.Margin = new System.Windows.Forms.Padding(0);
			TbRichTextAcrossClues.Name = "TbRichTextAcrossClues";
			TbRichTextAcrossClues.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			TbRichTextAcrossClues.Size = new System.Drawing.Size(183, 465);
			TbRichTextAcrossClues.TabIndex = 26;
			TbRichTextAcrossClues.Text = "";
			TbRichTextAcrossClues.Click += TbRichTextClues_Click;
			// 
			// BtnScreenAcross
			// 
			BtnScreenAcross.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			BtnScreenAcross.Location = new System.Drawing.Point(43, 2);
			BtnScreenAcross.Name = "BtnScreenAcross";
			BtnScreenAcross.Size = new System.Drawing.Size(75, 23);
			BtnScreenAcross.TabIndex = 29;
			BtnScreenAcross.Text = "Across";
			toolTip1.SetToolTip(BtnScreenAcross, "Select for scaning the Across List. Exclude any heading");
			BtnScreenAcross.UseVisualStyleBackColor = true;
			BtnScreenAcross.Click += BtnScreenAcross_Click;
			// 
			// BtnScreenDown
			// 
			BtnScreenDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			BtnScreenDown.Location = new System.Drawing.Point(428, 2);
			BtnScreenDown.Name = "BtnScreenDown";
			BtnScreenDown.Size = new System.Drawing.Size(75, 23);
			BtnScreenDown.TabIndex = 30;
			BtnScreenDown.Text = "Down";
			toolTip1.SetToolTip(BtnScreenDown, "Select Down List. Exclude any heading");
			BtnScreenDown.UseVisualStyleBackColor = true;
			BtnScreenDown.Click += BtnScreenDown_Click;
			// 
			// BtnPictureBox
			// 
			BtnPictureBox.BackgroundImage = Properties.Resources.add;
			BtnPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			BtnPictureBox.Location = new System.Drawing.Point(780, 524);
			BtnPictureBox.Name = "BtnPictureBox";
			BtnPictureBox.Size = new System.Drawing.Size(85, 38);
			BtnPictureBox.TabIndex = 33;
			toolTip1.SetToolTip(BtnPictureBox, "Add the Clues to the Crossword");
			BtnPictureBox.UseVisualStyleBackColor = true;
			BtnPictureBox.Click += BtnPictureBox_Click;
			// 
			// BtnClose
			// 
			BtnClose.Location = new System.Drawing.Point(873, 523);
			BtnClose.Name = "BtnClose";
			BtnClose.Size = new System.Drawing.Size(75, 38);
			BtnClose.TabIndex = 34;
			BtnClose.Text = "Close";
			toolTip1.SetToolTip(BtnClose, "Back to ImageDisplay Form");
			BtnClose.UseVisualStyleBackColor = true;
			BtnClose.Click += BtnClose_Click;
			// 
			// BtnClear
			// 
			BtnClear.Location = new System.Drawing.Point(10, 521);
			BtnClear.Name = "BtnClear";
			BtnClear.Size = new System.Drawing.Size(75, 42);
			BtnClear.TabIndex = 37;
			BtnClear.Text = "Clear All";
			BtnClear.UseVisualStyleBackColor = true;
			BtnClear.Click += BtnClear_Click;
			// 
			// BtnFinalFormat
			// 
			BtnFinalFormat.Location = new System.Drawing.Point(672, 542);
			BtnFinalFormat.Name = "BtnFinalFormat";
			BtnFinalFormat.Size = new System.Drawing.Size(93, 23);
			BtnFinalFormat.TabIndex = 40;
			BtnFinalFormat.Text = "Final Format";
			toolTip1.SetToolTip(BtnFinalFormat, "Select after comparing OCR with the original and correcting the text\r\nThsi will do the final parsing and ready to be added");
			BtnFinalFormat.UseVisualStyleBackColor = true;
			BtnFinalFormat.Click += BtnFinalFormat_Click;
			// 
			// CbBracket
			// 
			CbBracket.AutoSize = true;
			CbBracket.Font = new System.Drawing.Font("Segoe UI", 9F);
			CbBracket.Location = new System.Drawing.Point(126, 0);
			CbBracket.Margin = new System.Windows.Forms.Padding(0);
			CbBracket.Name = "CbBracket";
			CbBracket.Size = new System.Drawing.Size(68, 19);
			CbBracket.TabIndex = 67;
			CbBracket.Text = "Use \" ) \"";
			toolTip1.SetToolTip(CbBracket, "Instead of the /n added by the OCR this overrides it and uses the \r\nfinal bracket as the end of the clue line. May help");
			CbBracket.UseVisualStyleBackColor = true;
			// 
			// BtnReset
			// 
			BtnReset.Font = new System.Drawing.Font("Segoe UI", 9F);
			BtnReset.Location = new System.Drawing.Point(6, 288);
			BtnReset.Name = "BtnReset";
			BtnReset.Size = new System.Drawing.Size(59, 23);
			BtnReset.TabIndex = 71;
			BtnReset.Text = "Default";
			toolTip1.SetToolTip(BtnReset, "Displays scanned image data");
			BtnReset.UseVisualStyleBackColor = true;
			BtnReset.Click += BtnReset_Click;
			// 
			// chkMedian
			// 
			chkMedian.AutoSize = true;
			chkMedian.Font = new System.Drawing.Font("Segoe UI", 9F);
			chkMedian.Location = new System.Drawing.Point(0, 38);
			chkMedian.Margin = new System.Windows.Forms.Padding(0);
			chkMedian.Name = "chkMedian";
			chkMedian.Size = new System.Drawing.Size(165, 19);
			chkMedian.TabIndex = 68;
			chkMedian.Text = "Median Blur/Despeckle (3)";
			toolTip1.SetToolTip(chkMedian, "Smaller less bluring and for fine isolated noise");
			chkMedian.UseVisualStyleBackColor = true;
			// 
			// chkKMeans
			// 
			chkKMeans.AutoSize = true;
			chkKMeans.Font = new System.Drawing.Font("Segoe UI", 9F);
			chkKMeans.Location = new System.Drawing.Point(0, 19);
			chkKMeans.Margin = new System.Windows.Forms.Padding(0);
			chkKMeans.Name = "chkKMeans";
			chkKMeans.Size = new System.Drawing.Size(167, 19);
			chkKMeans.TabIndex = 82;
			chkKMeans.Text = "KMeans - Lower Colour (4)";
			toolTip1.SetToolTip(chkKMeans, "Lower reduces colors more. Reduced number of colours to k value");
			chkKMeans.UseVisualStyleBackColor = true;
			// 
			// chkDenoise
			// 
			chkDenoise.AutoSize = true;
			chkDenoise.Font = new System.Drawing.Font("Segoe UI", 9F);
			chkDenoise.Location = new System.Drawing.Point(0, 57);
			chkDenoise.Margin = new System.Windows.Forms.Padding(0);
			chkDenoise.Name = "chkDenoise";
			chkDenoise.Size = new System.Drawing.Size(85, 19);
			chkDenoise.TabIndex = 84;
			chkDenoise.Text = "Denoise (8)";
			toolTip1.SetToolTip(chkDenoise, "Higher smooths more but blurs text");
			chkDenoise.UseVisualStyleBackColor = true;
			// 
			// trackScale
			// 
			trackScale.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			trackScale.LargeChange = 20;
			trackScale.Location = new System.Drawing.Point(6, 114);
			trackScale.Margin = new System.Windows.Forms.Padding(0);
			trackScale.Maximum = 300;
			trackScale.Minimum = 100;
			trackScale.Name = "trackScale";
			trackScale.Size = new System.Drawing.Size(174, 45);
			trackScale.SmallChange = 20;
			trackScale.TabIndex = 72;
			toolTip1.SetToolTip(trackScale, "Block pixel size for calculating the threshld. \r\nSmaller means more sensitive");
			trackScale.Value = 100;
			// 
			// trackBlockSize
			// 
			trackBlockSize.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			trackBlockSize.Location = new System.Drawing.Point(6, 258);
			trackBlockSize.Name = "trackBlockSize";
			trackBlockSize.Size = new System.Drawing.Size(174, 45);
			trackBlockSize.TabIndex = 73;
			toolTip1.SetToolTip(trackBlockSize, "Block pixel size for calculating the threshld.\r\nSmaller means more sensitive");
			// 
			// trackC
			// 
			trackC.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			trackC.Location = new System.Drawing.Point(6, 216);
			trackC.Name = "trackC";
			trackC.Size = new System.Drawing.Size(174, 45);
			trackC.TabIndex = 74;
			toolTip1.SetToolTip(trackC, "Subtracts from the adaptive threshold for the final threshold value.\r\nA positive c is higher threshold and more pixels black. Neg more pixels white");
			// 
			// trackSharpen
			// 
			trackSharpen.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			trackSharpen.Location = new System.Drawing.Point(6, 156);
			trackSharpen.Name = "trackSharpen";
			trackSharpen.Size = new System.Drawing.Size(174, 45);
			trackSharpen.TabIndex = 75;
			toolTip1.SetToolTip(trackSharpen, "Higher creates halos");
			// 
			// BtnShowLF
			// 
			BtnShowLF.Location = new System.Drawing.Point(593, 495);
			BtnShowLF.Name = "BtnShowLF";
			BtnShowLF.Size = new System.Drawing.Size(76, 23);
			BtnShowLF.TabIndex = 72;
			BtnShowLF.Text = "Show LF";
			toolTip1.SetToolTip(BtnShowLF, "Add the Clues to the Crossword");
			BtnShowLF.UseVisualStyleBackColor = true;
			BtnShowLF.Click += BtnShowLF_Click;
			// 
			// BtnRemoveLF
			// 
			BtnRemoveLF.Location = new System.Drawing.Point(593, 518);
			BtnRemoveLF.Name = "BtnRemoveLF";
			BtnRemoveLF.Size = new System.Drawing.Size(76, 23);
			BtnRemoveLF.TabIndex = 73;
			BtnRemoveLF.Text = "Remove LF";
			toolTip1.SetToolTip(BtnRemoveLF, "Add the Clues to the Crossword");
			BtnRemoveLF.UseVisualStyleBackColor = true;
			BtnRemoveLF.Click += BtnRemoveLF_Click;
			// 
			// BtnSetDefault
			// 
			BtnSetDefault.Font = new System.Drawing.Font("Segoe UI", 9F);
			BtnSetDefault.Location = new System.Drawing.Point(66, 288);
			BtnSetDefault.Name = "BtnSetDefault";
			BtnSetDefault.Size = new System.Drawing.Size(59, 23);
			BtnSetDefault.TabIndex = 86;
			BtnSetDefault.Text = "Save";
			toolTip1.SetToolTip(BtnSetDefault, "Displays scanned image data");
			BtnSetDefault.UseVisualStyleBackColor = true;
			BtnSetDefault.Click += BtnSetDefault_Click;
			// 
			// BtnLoadDefault
			// 
			BtnLoadDefault.Font = new System.Drawing.Font("Segoe UI", 9F);
			BtnLoadDefault.Location = new System.Drawing.Point(126, 288);
			BtnLoadDefault.Name = "BtnLoadDefault";
			BtnLoadDefault.Size = new System.Drawing.Size(59, 23);
			BtnLoadDefault.TabIndex = 87;
			BtnLoadDefault.Text = "Load";
			toolTip1.SetToolTip(BtnLoadDefault, "Displays scanned image data");
			BtnLoadDefault.UseVisualStyleBackColor = true;
			BtnLoadDefault.Click += BtnLoadDefault_Click;
			// 
			// BtnWordLengths
			// 
			BtnWordLengths.Location = new System.Drawing.Point(672, 517);
			BtnWordLengths.Name = "BtnWordLengths";
			BtnWordLengths.Size = new System.Drawing.Size(93, 24);
			BtnWordLengths.TabIndex = 94;
			BtnWordLengths.Text = "Word Lengths";
			toolTip1.SetToolTip(BtnWordLengths, "Select after comparing OCR with the original and correcting the text\r\nThsi will do the final parsing and ready to be added");
			BtnWordLengths.UseVisualStyleBackColor = true;
			BtnWordLengths.Click += BtnWordLengths_Click;
			// 
			// PictureBoxDown
			// 
			PictureBoxDown.Location = new System.Drawing.Point(580, 27);
			PictureBoxDown.Margin = new System.Windows.Forms.Padding(0);
			PictureBoxDown.Name = "PictureBoxDown";
			PictureBoxDown.Size = new System.Drawing.Size(183, 465);
			PictureBoxDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			PictureBoxDown.TabIndex = 42;
			PictureBoxDown.TabStop = false;
			// 
			// PictureBoxAcross
			// 
			PictureBoxAcross.Location = new System.Drawing.Point(198, 27);
			PictureBoxAcross.Margin = new System.Windows.Forms.Padding(0);
			PictureBoxAcross.Name = "PictureBoxAcross";
			PictureBoxAcross.Size = new System.Drawing.Size(183, 465);
			PictureBoxAcross.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			PictureBoxAcross.TabIndex = 43;
			PictureBoxAcross.TabStop = false;
			// 
			// chkClahe
			// 
			chkClahe.AutoSize = true;
			chkClahe.Font = new System.Drawing.Font("Segoe UI", 9F);
			chkClahe.Location = new System.Drawing.Point(0, 0);
			chkClahe.Margin = new System.Windows.Forms.Padding(0);
			chkClahe.Name = "chkClahe";
			chkClahe.Size = new System.Drawing.Size(173, 19);
			chkClahe.TabIndex = 81;
			chkClahe.Text = "CLAHE - Local Contrast Enh";
			chkClahe.UseVisualStyleBackColor = true;
			// 
			// chkAdaptive
			// 
			chkAdaptive.AutoSize = true;
			chkAdaptive.Font = new System.Drawing.Font("Segoe UI", 9F);
			chkAdaptive.Location = new System.Drawing.Point(9, 185);
			chkAdaptive.Name = "chkAdaptive";
			chkAdaptive.Size = new System.Drawing.Size(126, 19);
			chkAdaptive.TabIndex = 83;
			chkAdaptive.Text = "AdaptiveThreshold";
			chkAdaptive.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.ForeColor = System.Drawing.SystemColors.HotTrack;
			label3.Location = new System.Drawing.Point(120, 5);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(31, 15);
			label3.TabIndex = 45;
			label3.Text = "OCR";
			label3.Click += label3_Click;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label4.ForeColor = System.Drawing.Color.Brown;
			label4.Location = new System.Drawing.Point(506, 5);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(31, 15);
			label4.TabIndex = 46;
			label4.Text = "OCR";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label5.ForeColor = System.Drawing.Color.Brown;
			label5.Location = new System.Drawing.Point(602, 5);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(118, 15);
			label5.TabIndex = 47;
			label5.Text = "Down Clues - Image";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label6.ForeColor = System.Drawing.SystemColors.HotTrack;
			label6.Location = new System.Drawing.Point(215, 5);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(121, 15);
			label6.TabIndex = 48;
			label6.Text = "Across Clues - Image";
			// 
			// label1
			// 
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.Location = new System.Drawing.Point(766, 490);
			label1.Margin = new System.Windows.Forms.Padding(0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(119, 35);
			label1.TabIndex = 50;
			label1.Text = "Do a final review before clicking Add";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LabMissingA
			// 
			LabMissingA.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			LabMissingA.BackColor = System.Drawing.Color.White;
			LabMissingA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			LabMissingA.Font = new System.Drawing.Font("Segoe UI", 9F);
			LabMissingA.ForeColor = System.Drawing.Color.Black;
			LabMissingA.Location = new System.Drawing.Point(213, 516);
			LabMissingA.Name = "LabMissingA";
			LabMissingA.Size = new System.Drawing.Size(378, 18);
			LabMissingA.TabIndex = 57;
			LabMissingA.Text = "Missing: None";
			LabMissingA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox1
			// 
			groupBox1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			groupBox1.Controls.Add(tableLayoutPanel1);
			groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			groupBox1.Location = new System.Drawing.Point(767, 10);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(210, 103);
			groupBox1.TabIndex = 62;
			groupBox1.TabStop = false;
			groupBox1.Text = "OCR Options";
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.AutoSize = true;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
			tableLayoutPanel1.Controls.Add(RadioWinOCR, 0, 3);
			tableLayoutPanel1.Controls.Add(RadioPSMBlock, 0, 0);
			tableLayoutPanel1.Controls.Add(RadioPSMColumn, 0, 1);
			tableLayoutPanel1.Controls.Add(RadioPSMAuto, 0, 2);
			tableLayoutPanel1.Controls.Add(CbBracket, 1, 0);
			tableLayoutPanel1.Location = new System.Drawing.Point(10, 20);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 4;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel1.Size = new System.Drawing.Size(194, 78);
			tableLayoutPanel1.TabIndex = 97;
			// 
			// RadioWinOCR
			// 
			RadioWinOCR.AutoSize = true;
			RadioWinOCR.Font = new System.Drawing.Font("Segoe UI", 9F);
			RadioWinOCR.Location = new System.Drawing.Point(0, 57);
			RadioWinOCR.Margin = new System.Windows.Forms.Padding(0);
			RadioWinOCR.Name = "RadioWinOCR";
			RadioWinOCR.Size = new System.Drawing.Size(101, 19);
			RadioWinOCR.TabIndex = 68;
			RadioWinOCR.TabStop = true;
			RadioWinOCR.Text = "Windows OCR";
			RadioWinOCR.UseVisualStyleBackColor = true;
			RadioWinOCR.CheckedChanged += RadioWinOCR_CheckedChanged;
			RadioWinOCR.Click += RadioWinOCR_Click;
			// 
			// RadioPSMBlock
			// 
			RadioPSMBlock.AutoSize = true;
			RadioPSMBlock.Font = new System.Drawing.Font("Segoe UI", 9F);
			RadioPSMBlock.Location = new System.Drawing.Point(0, 0);
			RadioPSMBlock.Margin = new System.Windows.Forms.Padding(0);
			RadioPSMBlock.Name = "RadioPSMBlock";
			RadioPSMBlock.Size = new System.Drawing.Size(114, 19);
			RadioPSMBlock.TabIndex = 69;
			RadioPSMBlock.TabStop = true;
			RadioPSMBlock.Text = "Tess Single Block";
			RadioPSMBlock.UseVisualStyleBackColor = true;
			// 
			// RadioPSMColumn
			// 
			RadioPSMColumn.AutoSize = true;
			RadioPSMColumn.Font = new System.Drawing.Font("Segoe UI", 9F);
			RadioPSMColumn.Location = new System.Drawing.Point(0, 19);
			RadioPSMColumn.Margin = new System.Windows.Forms.Padding(0);
			RadioPSMColumn.Name = "RadioPSMColumn";
			RadioPSMColumn.Size = new System.Drawing.Size(103, 19);
			RadioPSMColumn.TabIndex = 52;
			RadioPSMColumn.TabStop = true;
			RadioPSMColumn.Text = "Tess Single Col";
			RadioPSMColumn.UseVisualStyleBackColor = true;
			RadioPSMColumn.Click += RadioPSMColumn_Click;
			// 
			// RadioPSMAuto
			// 
			RadioPSMAuto.AutoSize = true;
			RadioPSMAuto.Checked = true;
			RadioPSMAuto.Font = new System.Drawing.Font("Segoe UI", 9F);
			RadioPSMAuto.Location = new System.Drawing.Point(0, 38);
			RadioPSMAuto.Margin = new System.Windows.Forms.Padding(0);
			RadioPSMAuto.Name = "RadioPSMAuto";
			RadioPSMAuto.Size = new System.Drawing.Size(76, 19);
			RadioPSMAuto.TabIndex = 53;
			RadioPSMAuto.TabStop = true;
			RadioPSMAuto.Text = "Tess Auto";
			RadioPSMAuto.UseVisualStyleBackColor = true;
			RadioPSMAuto.Click += RadioPSMAuto_Click;
			// 
			// groupBox2
			// 
			groupBox2.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			groupBox2.Controls.Add(lblScale);
			groupBox2.Controls.Add(BtnInfo5);
			groupBox2.Controls.Add(BtnInfo4);
			groupBox2.Controls.Add(BtnLoadDefault);
			groupBox2.Controls.Add(BtnSetDefault);
			groupBox2.Controls.Add(BtnReset);
			groupBox2.Controls.Add(trackBlockSize);
			groupBox2.Controls.Add(BtnIPUpdate);
			groupBox2.Controls.Add(BtnUpdateIP);
			groupBox2.Controls.Add(BtnPreProcessing);
			groupBox2.Controls.Add(lblSharpen);
			groupBox2.Controls.Add(lblBlockSize);
			groupBox2.Controls.Add(chkAdaptive);
			groupBox2.Controls.Add(lblC);
			groupBox2.Controls.Add(trackSharpen);
			groupBox2.Controls.Add(trackC);
			groupBox2.Controls.Add(flowLayoutPanel1);
			groupBox2.Controls.Add(trackScale);
			groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			groupBox2.Location = new System.Drawing.Point(766, 115);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(211, 377);
			groupBox2.TabIndex = 63;
			groupBox2.TabStop = false;
			groupBox2.Text = "Image Pre-processing";
			// 
			// lblScale
			// 
			lblScale.AutoSize = true;
			lblScale.BackColor = System.Drawing.Color.Transparent;
			lblScale.Font = new System.Drawing.Font("Segoe UI", 9F);
			lblScale.Location = new System.Drawing.Point(6, 98);
			lblScale.Name = "lblScale";
			lblScale.Size = new System.Drawing.Size(66, 15);
			lblScale.TabIndex = 80;
			lblScale.Text = "Scale: 1.00x";
			// 
			// BtnInfo5
			// 
			BtnInfo5.BackgroundImage = Properties.Resources.information_348__1_;
			BtnInfo5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnInfo5.Font = new System.Drawing.Font("Script MT Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			BtnInfo5.Location = new System.Drawing.Point(154, 75);
			BtnInfo5.Margin = new System.Windows.Forms.Padding(0);
			BtnInfo5.Name = "BtnInfo5";
			BtnInfo5.Size = new System.Drawing.Size(24, 20);
			BtnInfo5.TabIndex = 98;
			BtnInfo5.Text = "i";
			BtnInfo5.UseVisualStyleBackColor = true;
			BtnInfo5.Click += BtnInfo5_Click;
			// 
			// BtnInfo4
			// 
			BtnInfo4.BackgroundImage = Properties.Resources.information_348__1_;
			BtnInfo4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnInfo4.Font = new System.Drawing.Font("Script MT Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			BtnInfo4.Location = new System.Drawing.Point(46, 348);
			BtnInfo4.Margin = new System.Windows.Forms.Padding(0);
			BtnInfo4.Name = "BtnInfo4";
			BtnInfo4.Size = new System.Drawing.Size(24, 20);
			BtnInfo4.TabIndex = 97;
			BtnInfo4.Text = "i";
			BtnInfo4.UseVisualStyleBackColor = true;
			BtnInfo4.Click += BtnInfo4_Click;
			// 
			// BtnIPUpdate
			// 
			BtnIPUpdate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			BtnIPUpdate.Location = new System.Drawing.Point(98, 320);
			BtnIPUpdate.Name = "BtnIPUpdate";
			BtnIPUpdate.Size = new System.Drawing.Size(74, 23);
			BtnIPUpdate.TabIndex = 88;
			BtnIPUpdate.Text = "Update";
			BtnIPUpdate.UseVisualStyleBackColor = true;
			BtnIPUpdate.Click += BtnIPUpdate_Click;
			// 
			// BtnUpdateIP
			// 
			BtnUpdateIP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			BtnUpdateIP.Location = new System.Drawing.Point(9, 320);
			BtnUpdateIP.Name = "BtnUpdateIP";
			BtnUpdateIP.Size = new System.Drawing.Size(83, 23);
			BtnUpdateIP.TabIndex = 85;
			BtnUpdateIP.Text = "New Profile";
			BtnUpdateIP.UseVisualStyleBackColor = true;
			BtnUpdateIP.Click += BtnUpdateIP_Click;
			// 
			// BtnPreProcessing
			// 
			BtnPreProcessing.Font = new System.Drawing.Font("Segoe UI", 9F);
			BtnPreProcessing.Location = new System.Drawing.Point(73, 348);
			BtnPreProcessing.Name = "BtnPreProcessing";
			BtnPreProcessing.Size = new System.Drawing.Size(69, 23);
			BtnPreProcessing.TabIndex = 70;
			BtnPreProcessing.Text = "Original";
			BtnPreProcessing.UseVisualStyleBackColor = true;
			BtnPreProcessing.Click += BtnPreProcessing_Click;
			// 
			// lblSharpen
			// 
			lblSharpen.AutoSize = true;
			lblSharpen.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			lblSharpen.Font = new System.Drawing.Font("Segoe UI", 9F);
			lblSharpen.Location = new System.Drawing.Point(6, 142);
			lblSharpen.Name = "lblSharpen";
			lblSharpen.Size = new System.Drawing.Size(77, 15);
			lblSharpen.TabIndex = 79;
			lblSharpen.Text = "Sharpen: 0.80";
			// 
			// lblBlockSize
			// 
			lblBlockSize.AutoSize = true;
			lblBlockSize.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			lblBlockSize.Font = new System.Drawing.Font("Segoe UI", 9F);
			lblBlockSize.Location = new System.Drawing.Point(6, 243);
			lblBlockSize.Name = "lblBlockSize";
			lblBlockSize.Size = new System.Drawing.Size(74, 15);
			lblBlockSize.TabIndex = 78;
			lblBlockSize.Text = "BlockSize: 15";
			// 
			// lblC
			// 
			lblC.AutoSize = true;
			lblC.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			lblC.Font = new System.Drawing.Font("Segoe UI", 9F);
			lblC.Location = new System.Drawing.Point(6, 203);
			lblC.Name = "lblC";
			lblC.Size = new System.Drawing.Size(77, 15);
			lblC.TabIndex = 77;
			lblC.Text = "Adaptive C: 9";
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.AutoSize = true;
			flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			flowLayoutPanel1.Controls.Add(chkClahe);
			flowLayoutPanel1.Controls.Add(chkKMeans);
			flowLayoutPanel1.Controls.Add(chkMedian);
			flowLayoutPanel1.Controls.Add(chkDenoise);
			flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			flowLayoutPanel1.Location = new System.Drawing.Point(7, 19);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(173, 76);
			flowLayoutPanel1.TabIndex = 97;
			flowLayoutPanel1.WrapContents = false;
			// 
			// BtnNumbers
			// 
			BtnNumbers.Location = new System.Drawing.Point(593, 542);
			BtnNumbers.Name = "BtnNumbers";
			BtnNumbers.Size = new System.Drawing.Size(76, 23);
			BtnNumbers.TabIndex = 68;
			BtnNumbers.Text = "Show Nos";
			BtnNumbers.UseVisualStyleBackColor = true;
			BtnNumbers.Click += BtnNumbers_Click;
			// 
			// BtnInfo2
			// 
			BtnInfo2.BackgroundImage = Properties.Resources.information_348__1_;
			BtnInfo2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnInfo2.Font = new System.Drawing.Font("Script MT Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			BtnInfo2.Location = new System.Drawing.Point(327, 543);
			BtnInfo2.Margin = new System.Windows.Forms.Padding(0);
			BtnInfo2.Name = "BtnInfo2";
			BtnInfo2.Size = new System.Drawing.Size(24, 20);
			BtnInfo2.TabIndex = 70;
			BtnInfo2.Text = "i";
			BtnInfo2.UseVisualStyleBackColor = true;
			BtnInfo2.Click += BtnInfo2_Click;
			// 
			// BtnInfo1
			// 
			BtnInfo1.BackgroundImage = Properties.Resources.information_348__1_;
			BtnInfo1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnInfo1.Font = new System.Drawing.Font("Script MT Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			BtnInfo1.Location = new System.Drawing.Point(708, 493);
			BtnInfo1.Margin = new System.Windows.Forms.Padding(0);
			BtnInfo1.Name = "BtnInfo1";
			BtnInfo1.Size = new System.Drawing.Size(24, 20);
			BtnInfo1.TabIndex = 71;
			BtnInfo1.Text = "i";
			BtnInfo1.UseVisualStyleBackColor = true;
			BtnInfo1.Click += BtnInfo1_Click;
			// 
			// LblSpellChkA
			// 
			LblSpellChkA.BackColor = System.Drawing.Color.White;
			LblSpellChkA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			LblSpellChkA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			LblSpellChkA.Location = new System.Drawing.Point(287, 498);
			LblSpellChkA.Name = "LblSpellChkA";
			LblSpellChkA.Size = new System.Drawing.Size(23, 17);
			LblSpellChkA.TabIndex = 75;
			LblSpellChkA.Text = "0";
			LblSpellChkA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LblMissingQtyA
			// 
			LblMissingQtyA.BackColor = System.Drawing.Color.White;
			LblMissingQtyA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			LblMissingQtyA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			LblMissingQtyA.Location = new System.Drawing.Point(378, 498);
			LblMissingQtyA.Name = "LblMissingQtyA";
			LblMissingQtyA.Size = new System.Drawing.Size(23, 17);
			LblMissingQtyA.TabIndex = 76;
			LblMissingQtyA.Text = "0";
			LblMissingQtyA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LblMissingWdLenA
			// 
			LblMissingWdLenA.BackColor = System.Drawing.Color.White;
			LblMissingWdLenA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			LblMissingWdLenA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			LblMissingWdLenA.Location = new System.Drawing.Point(464, 498);
			LblMissingWdLenA.Name = "LblMissingWdLenA";
			LblMissingWdLenA.Size = new System.Drawing.Size(23, 17);
			LblMissingWdLenA.TabIndex = 77;
			LblMissingWdLenA.Text = "0";
			LblMissingWdLenA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LblLFErrorA
			// 
			LblLFErrorA.BackColor = System.Drawing.Color.White;
			LblLFErrorA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			LblLFErrorA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			LblLFErrorA.Location = new System.Drawing.Point(554, 498);
			LblLFErrorA.Name = "LblLFErrorA";
			LblLFErrorA.Size = new System.Drawing.Size(23, 17);
			LblLFErrorA.TabIndex = 78;
			LblLFErrorA.Text = "0";
			LblLFErrorA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.BackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.ForeColor = System.Drawing.Color.Yellow;
			label2.Location = new System.Drawing.Point(207, 498);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(77, 15);
			label2.TabIndex = 79;
			label2.Text = "Word Errors:";
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.BackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label7.ForeColor = System.Drawing.Color.Yellow;
			label7.Location = new System.Drawing.Point(317, 498);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(58, 15);
			label7.TabIndex = 80;
			label7.Text = "Clue Nos:";
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.BackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label8.ForeColor = System.Drawing.Color.Yellow;
			label8.Location = new System.Drawing.Point(405, 498);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(59, 15);
			label8.TabIndex = 81;
			label8.Text = "Brackets:";
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.BackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			label9.ForeColor = System.Drawing.Color.Yellow;
			label9.Location = new System.Drawing.Point(495, 498);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(59, 15);
			label9.TabIndex = 82;
			label9.Text = "Extra LFs:";
			// 
			// label15
			// 
			label15.BackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			label15.Location = new System.Drawing.Point(205, 495);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(375, 44);
			label15.TabIndex = 93;
			// 
			// BtnCheckAllErrors
			// 
			BtnCheckAllErrors.Location = new System.Drawing.Point(363, 542);
			BtnCheckAllErrors.Name = "BtnCheckAllErrors";
			BtnCheckAllErrors.Size = new System.Drawing.Size(71, 24);
			BtnCheckAllErrors.TabIndex = 96;
			BtnCheckAllErrors.Text = "Check";
			BtnCheckAllErrors.UseVisualStyleBackColor = true;
			BtnCheckAllErrors.Click += BtnCheckAllErrors_Click;
			// 
			// OCRClues
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			ClientSize = new System.Drawing.Size(980, 570);
			Controls.Add(BtnCheckAllErrors);
			Controls.Add(groupBox2);
			Controls.Add(BtnWordLengths);
			Controls.Add(label9);
			Controls.Add(label8);
			Controls.Add(label7);
			Controls.Add(label2);
			Controls.Add(LblLFErrorA);
			Controls.Add(LblMissingWdLenA);
			Controls.Add(LblMissingQtyA);
			Controls.Add(LblSpellChkA);
			Controls.Add(BtnRemoveLF);
			Controls.Add(BtnShowLF);
			Controls.Add(BtnInfo1);
			Controls.Add(BtnInfo2);
			Controls.Add(BtnNumbers);
			Controls.Add(groupBox1);
			Controls.Add(LabMissingA);
			Controls.Add(label6);
			Controls.Add(label5);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(BtnFinalFormat);
			Controls.Add(BtnClear);
			Controls.Add(BtnClose);
			Controls.Add(BtnPictureBox);
			Controls.Add(BtnScreenDown);
			Controls.Add(BtnScreenAcross);
			Controls.Add(TbRichTextDownClues);
			Controls.Add(TbRichTextAcrossClues);
			Controls.Add(PictureBoxDown);
			Controls.Add(PictureBoxAcross);
			Controls.Add(label15);
			Controls.Add(label1);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			Name = "OCRClues";
			ShowInTaskbar = false;
			SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "OCR Clues List";
			Load += OCRClues_Load;
			((System.ComponentModel.ISupportInitialize)trackScale).EndInit();
			((System.ComponentModel.ISupportInitialize)trackBlockSize).EndInit();
			((System.ComponentModel.ISupportInitialize)trackC).EndInit();
			((System.ComponentModel.ISupportInitialize)trackSharpen).EndInit();
			((System.ComponentModel.ISupportInitialize)PictureBoxDown).EndInit();
			((System.ComponentModel.ISupportInitialize)PictureBoxAcross).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox TbRichTextDownClues;
		private System.Windows.Forms.RichTextBox TbRichTextAcrossClues;
		private System.Windows.Forms.Button BtnScreenAcross;
		private System.Windows.Forms.Button BtnScreenDown;
		private System.Windows.Forms.Button BtnPictureBox;
		private System.Windows.Forms.Button BtnClose;
		private System.Windows.Forms.Button BtnClear;
		private System.Windows.Forms.Button BtnFinalFormat;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.PictureBox PictureBoxDown;
		private System.Windows.Forms.PictureBox PictureBoxAcross;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label LabMissingA;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox CbBracket;
		private System.Windows.Forms.Button BtnPreProcessing;
		private System.Windows.Forms.Button BtnImageProcessing;
		private System.Windows.Forms.Button BtnReset;
		private System.Windows.Forms.TrackBar trackScale;
		private System.Windows.Forms.TrackBar trackBlockSize;
		private System.Windows.Forms.TrackBar trackC;
		private System.Windows.Forms.TrackBar trackSharpen;
		private System.Windows.Forms.Label lblC;
		private System.Windows.Forms.Label lblBlockSize;
		private System.Windows.Forms.Label lblSharpen;
		private System.Windows.Forms.Label lblScale;
		private System.Windows.Forms.CheckBox chkDenoise;
		private System.Windows.Forms.CheckBox chkMedian;
		private System.Windows.Forms.CheckBox chkAdaptive;
		private System.Windows.Forms.CheckBox chkClahe;
		private System.Windows.Forms.CheckBox chkKMeans;
		private System.Windows.Forms.Button BtnUpdateIP;
		private System.Windows.Forms.Button BtnNumbers;
		public System.Windows.Forms.Button BtnInfo2;
		public System.Windows.Forms.Button BtnInfo1;
		private System.Windows.Forms.Button BtnShowLF;
		private System.Windows.Forms.Button BtnRemoveLF;
		private System.Windows.Forms.Label LblSpellChkA;
		private System.Windows.Forms.Label LblMissingQtyA;
		private System.Windows.Forms.Label LblMissingWdLenA;
		private System.Windows.Forms.Label LblLFErrorA;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button BtnLoadDefault;
		private System.Windows.Forms.Button BtnSetDefault;
		private System.Windows.Forms.Button BtnIPUpdate;
		private System.Windows.Forms.Button BtnWordLengths;
		private System.Windows.Forms.Button BtnCheckAllErrors;
		public System.Windows.Forms.Button BtnInfo4;
		public System.Windows.Forms.Button BtnInfo5;
		private System.Windows.Forms.RadioButton RadioWinOCR;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.RadioButton RadioPSMAuto;
		private System.Windows.Forms.RadioButton RadioPSMBlock;
		private System.Windows.Forms.RadioButton RadioPSMColumn;
	}
}