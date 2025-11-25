namespace Crossword_Filler
{
	partial class EnterSolution
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing && (components != null))
		//	{
		//		components.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			TbSolution = new System.Windows.Forms.TextBox();
			LabSoln = new System.Windows.Forms.Label();
			TbExisting = new System.Windows.Forms.TextBox();
			LabExist = new System.Windows.Forms.Label();
			RadioAcross = new System.Windows.Forms.RadioButton();
			RadioDown = new System.Windows.Forms.RadioButton();
			BtnOK = new System.Windows.Forms.Button();
			BtnClose = new System.Windows.Forms.Button();
			textBox1 = new System.Windows.Forms.TextBox();
			TbRowNo = new System.Windows.Forms.TextBox();
			TbColNo = new System.Windows.Forms.TextBox();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			label1 = new System.Windows.Forms.Label();
			SuspendLayout();
			// 
			// TbSolution
			// 
			TbSolution.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			TbSolution.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			TbSolution.Location = new System.Drawing.Point(14, 108);
			TbSolution.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			TbSolution.Name = "TbSolution";
			TbSolution.Size = new System.Drawing.Size(231, 26);
			TbSolution.TabIndex = 0;
			TbSolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			toolTip1.SetToolTip(TbSolution, "Enter solution. Length has to match and cannot overwrite letters already known ");
			TbSolution.TextChanged += TbSolution_TextChanged;
			TbSolution.KeyPress += TbSolution_KeyPress;
			// 
			// LabSoln
			// 
			LabSoln.AutoSize = true;
			LabSoln.Location = new System.Drawing.Point(14, 90);
			LabSoln.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			LabSoln.Name = "LabSoln";
			LabSoln.Size = new System.Drawing.Size(54, 15);
			LabSoln.TabIndex = 1;
			LabSoln.Text = "Solution:";
			// 
			// TbExisting
			// 
			TbExisting.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			TbExisting.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			TbExisting.Location = new System.Drawing.Point(14, 55);
			TbExisting.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			TbExisting.Name = "TbExisting";
			TbExisting.Size = new System.Drawing.Size(231, 26);
			TbExisting.TabIndex = 2;
			TbExisting.TabStop = false;
			TbExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// LabExist
			// 
			LabExist.AutoSize = true;
			LabExist.Location = new System.Drawing.Point(14, 37);
			LabExist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			LabExist.Name = "LabExist";
			LabExist.Size = new System.Drawing.Size(50, 15);
			LabExist.TabIndex = 3;
			LabExist.Text = "Existing:";
			// 
			// RadioAcross
			// 
			RadioAcross.AutoSize = true;
			RadioAcross.Checked = true;
			RadioAcross.Location = new System.Drawing.Point(51, 8);
			RadioAcross.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			RadioAcross.Name = "RadioAcross";
			RadioAcross.Size = new System.Drawing.Size(69, 19);
			RadioAcross.TabIndex = 4;
			RadioAcross.TabStop = true;
			RadioAcross.Text = "ACROSS";
			RadioAcross.UseVisualStyleBackColor = true;
			RadioAcross.CheckedChanged += RadioAcross_CheckedChanged;
			// 
			// RadioDown
			// 
			RadioDown.AutoSize = true;
			RadioDown.Location = new System.Drawing.Point(139, 8);
			RadioDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			RadioDown.Name = "RadioDown";
			RadioDown.Size = new System.Drawing.Size(62, 19);
			RadioDown.TabIndex = 5;
			RadioDown.TabStop = true;
			RadioDown.Text = "DOWN";
			RadioDown.UseVisualStyleBackColor = true;
			RadioDown.CheckedChanged += RadioDown_CheckedChanged;
			// 
			// BtnOK
			// 
			BtnOK.Location = new System.Drawing.Point(306, 25);
			BtnOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			BtnOK.Name = "BtnOK";
			BtnOK.Size = new System.Drawing.Size(88, 27);
			BtnOK.TabIndex = 6;
			BtnOK.TabStop = false;
			BtnOK.Text = "OK";
			BtnOK.UseVisualStyleBackColor = true;
			BtnOK.Click += BtnOK_Click;
			// 
			// BtnClose
			// 
			BtnClose.Location = new System.Drawing.Point(306, 59);
			BtnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			BtnClose.Name = "BtnClose";
			BtnClose.Size = new System.Drawing.Size(88, 27);
			BtnClose.TabIndex = 7;
			BtnClose.TabStop = false;
			BtnClose.Text = "Close";
			BtnClose.UseVisualStyleBackColor = true;
			BtnClose.Click += BtnClose_Click;
			// 
			// textBox1
			// 
			textBox1.Location = new System.Drawing.Point(475, 25);
			textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(69, 23);
			textBox1.TabIndex = 8;
			// 
			// TbRowNo
			// 
			TbRowNo.Location = new System.Drawing.Point(476, 28);
			TbRowNo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			TbRowNo.Name = "TbRowNo";
			TbRowNo.Size = new System.Drawing.Size(69, 23);
			TbRowNo.TabIndex = 8;
			// 
			// TbColNo
			// 
			TbColNo.Location = new System.Drawing.Point(475, 62);
			TbColNo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			TbColNo.Name = "TbColNo";
			TbColNo.Size = new System.Drawing.Size(69, 23);
			TbColNo.TabIndex = 8;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(46, 141);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(138, 15);
			label1.TabIndex = 9;
			label1.Text = "SPACE to separate words";
			// 
			// EnterSolution
			// 
			AcceptButton = BtnOK;
			AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			CancelButton = BtnClose;
			ClientSize = new System.Drawing.Size(253, 162);
			Controls.Add(label1);
			Controls.Add(TbColNo);
			Controls.Add(TbRowNo);
			Controls.Add(textBox1);
			Controls.Add(BtnClose);
			Controls.Add(BtnOK);
			Controls.Add(RadioDown);
			Controls.Add(RadioAcross);
			Controls.Add(LabExist);
			Controls.Add(TbExisting);
			Controls.Add(LabSoln);
			Controls.Add(TbSolution);
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "EnterSolution";
			ShowInTaskbar = false;
			SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Clue No 24 Solution";
			FormClosing += EnterSolution_FormClosing;
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TbSolution;
		private System.Windows.Forms.Label LabSoln;
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.Button BtnClose;
		public System.Windows.Forms.TextBox TbExisting;
		public System.Windows.Forms.Label LabExist;
		public System.Windows.Forms.RadioButton RadioAcross;
		public System.Windows.Forms.RadioButton RadioDown;
		private System.Windows.Forms.TextBox textBox1;
		public System.Windows.Forms.TextBox TbRowNo;
		public System.Windows.Forms.TextBox TbColNo;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label1;
	}
}