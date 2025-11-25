namespace Crossword_Filler
{
	partial class ExportCrossword
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>


		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			RadioCurrent = new System.Windows.Forms.RadioButton();
			RadioAll = new System.Windows.Forms.RadioButton();
			RadioFromCurrent = new System.Windows.Forms.RadioButton();
			RadioCustom = new System.Windows.Forms.RadioButton();
			TbCurCW1 = new System.Windows.Forms.TextBox();
			TbStart = new System.Windows.Forms.TextBox();
			TbEnd1 = new System.Windows.Forms.TextBox();
			TBCurCW2 = new System.Windows.Forms.TextBox();
			TBEnd2 = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			TBCustomEnd = new System.Windows.Forms.TextBox();
			TBCustomStart = new System.Windows.Forms.TextBox();
			BtnOK = new System.Windows.Forms.Button();
			BtnCancel = new System.Windows.Forms.Button();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			CheckBoxPUZ = new System.Windows.Forms.CheckBox();
			CheckBoxIPUZ = new System.Windows.Forms.CheckBox();
			CbAnswers = new System.Windows.Forms.CheckBox();
			CbSolution = new System.Windows.Forms.CheckBox();
			CbHints = new System.Windows.Forms.CheckBox();
			timer1 = new System.Windows.Forms.Timer(components);
			SuspendLayout();
			// 
			// RadioCurrent
			// 
			RadioCurrent.AutoSize = true;
			RadioCurrent.Location = new System.Drawing.Point(15, 17);
			RadioCurrent.Name = "RadioCurrent";
			RadioCurrent.Size = new System.Drawing.Size(120, 19);
			RadioCurrent.TabIndex = 0;
			RadioCurrent.Text = "Current Cossword";
			RadioCurrent.UseVisualStyleBackColor = true;
			RadioCurrent.CheckedChanged += RadioCurrent_CheckedChanged;
			RadioCurrent.Click += RadioCurrent_Click;
			// 
			// RadioAll
			// 
			RadioAll.AutoSize = true;
			RadioAll.Location = new System.Drawing.Point(15, 71);
			RadioAll.Name = "RadioAll";
			RadioAll.Size = new System.Drawing.Size(103, 19);
			RadioAll.TabIndex = 1;
			RadioAll.Text = "All Crosswords";
			RadioAll.UseVisualStyleBackColor = true;
			// 
			// RadioFromCurrent
			// 
			RadioFromCurrent.AutoSize = true;
			RadioFromCurrent.Location = new System.Drawing.Point(15, 47);
			RadioFromCurrent.Name = "RadioFromCurrent";
			RadioFromCurrent.Size = new System.Drawing.Size(129, 19);
			RadioFromCurrent.TabIndex = 2;
			RadioFromCurrent.Text = "From current to last";
			RadioFromCurrent.UseVisualStyleBackColor = true;
			// 
			// RadioCustom
			// 
			RadioCustom.AutoSize = true;
			RadioCustom.Location = new System.Drawing.Point(15, 96);
			RadioCustom.Name = "RadioCustom";
			RadioCustom.Size = new System.Drawing.Size(103, 19);
			RadioCustom.TabIndex = 3;
			RadioCustom.Text = "Custom Range";
			RadioCustom.UseVisualStyleBackColor = true;
			RadioCustom.CheckedChanged += RadioCustom_CheckedChanged;
			// 
			// TbCurCW1
			// 
			TbCurCW1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			TbCurCW1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			TbCurCW1.Location = new System.Drawing.Point(147, 15);
			TbCurCW1.Name = "TbCurCW1";
			TbCurCW1.Size = new System.Drawing.Size(44, 20);
			TbCurCW1.TabIndex = 4;
			TbCurCW1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TbStart
			// 
			TbStart.BackColor = System.Drawing.SystemColors.InactiveCaption;
			TbStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			TbStart.Location = new System.Drawing.Point(147, 69);
			TbStart.Name = "TbStart";
			TbStart.Size = new System.Drawing.Size(46, 20);
			TbStart.TabIndex = 5;
			TbStart.Text = "1";
			TbStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TbEnd1
			// 
			TbEnd1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			TbEnd1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			TbEnd1.Location = new System.Drawing.Point(215, 69);
			TbEnd1.Name = "TbEnd1";
			TbEnd1.Size = new System.Drawing.Size(44, 20);
			TbEnd1.TabIndex = 6;
			TbEnd1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TBCurCW2
			// 
			TBCurCW2.BackColor = System.Drawing.SystemColors.InactiveCaption;
			TBCurCW2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			TBCurCW2.Location = new System.Drawing.Point(147, 45);
			TBCurCW2.Name = "TBCurCW2";
			TBCurCW2.Size = new System.Drawing.Size(46, 20);
			TBCurCW2.TabIndex = 7;
			TBCurCW2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TBEnd2
			// 
			TBEnd2.BackColor = System.Drawing.SystemColors.InactiveCaption;
			TBEnd2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			TBEnd2.Location = new System.Drawing.Point(215, 45);
			TBEnd2.Name = "TBEnd2";
			TBEnd2.Size = new System.Drawing.Size(44, 20);
			TBEnd2.TabIndex = 8;
			TBEnd2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(195, 48);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(18, 15);
			label1.TabIndex = 9;
			label1.Text = "to";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(195, 72);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(18, 15);
			label2.TabIndex = 10;
			label2.Text = "to";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(195, 98);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(18, 15);
			label3.TabIndex = 13;
			label3.Text = "to";
			// 
			// TBCustomEnd
			// 
			TBCustomEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			TBCustomEnd.Location = new System.Drawing.Point(215, 94);
			TBCustomEnd.Name = "TBCustomEnd";
			TBCustomEnd.Size = new System.Drawing.Size(44, 20);
			TBCustomEnd.TabIndex = 12;
			TBCustomEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			TBCustomEnd.Enter += TBCustomEnd_Enter;
			// 
			// TBCustomStart
			// 
			TBCustomStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			TBCustomStart.Location = new System.Drawing.Point(147, 94);
			TBCustomStart.Name = "TBCustomStart";
			TBCustomStart.Size = new System.Drawing.Size(46, 20);
			TBCustomStart.TabIndex = 11;
			TBCustomStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			TBCustomStart.Enter += TBCustomStart_Enter;
			// 
			// BtnOK
			// 
			BtnOK.Location = new System.Drawing.Point(52, 156);
			BtnOK.Name = "BtnOK";
			BtnOK.Size = new System.Drawing.Size(75, 23);
			BtnOK.TabIndex = 14;
			BtnOK.Text = "OK";
			BtnOK.UseVisualStyleBackColor = true;
			BtnOK.Click += BtnOK_Click;
			// 
			// BtnCancel
			// 
			BtnCancel.Location = new System.Drawing.Point(141, 156);
			BtnCancel.Name = "BtnCancel";
			BtnCancel.Size = new System.Drawing.Size(75, 23);
			BtnCancel.TabIndex = 15;
			BtnCancel.Text = "Cancel";
			BtnCancel.UseVisualStyleBackColor = true;
			BtnCancel.Click += BtnCancel_Click;
			// 
			// CheckBoxPUZ
			// 
			CheckBoxPUZ.AutoSize = true;
			CheckBoxPUZ.Location = new System.Drawing.Point(210, 10);
			CheckBoxPUZ.Name = "CheckBoxPUZ";
			CheckBoxPUZ.Size = new System.Drawing.Size(48, 19);
			CheckBoxPUZ.TabIndex = 16;
			CheckBoxPUZ.Text = ".puz";
			CheckBoxPUZ.UseVisualStyleBackColor = true;
			CheckBoxPUZ.CheckedChanged += CheckBoxPUZ_CheckedChanged;
			// 
			// CheckBoxIPUZ
			// 
			CheckBoxIPUZ.AutoSize = true;
			CheckBoxIPUZ.Location = new System.Drawing.Point(210, 26);
			CheckBoxIPUZ.Name = "CheckBoxIPUZ";
			CheckBoxIPUZ.Size = new System.Drawing.Size(51, 19);
			CheckBoxIPUZ.TabIndex = 17;
			CheckBoxIPUZ.Text = ".ipuz";
			CheckBoxIPUZ.UseVisualStyleBackColor = true;
			CheckBoxIPUZ.CheckedChanged += CheckBoxIPUZ_CheckedChanged;
			// 
			// CbAnswers
			// 
			CbAnswers.AutoSize = true;
			CbAnswers.Checked = true;
			CbAnswers.CheckState = System.Windows.Forms.CheckState.Checked;
			CbAnswers.Location = new System.Drawing.Point(38, 127);
			CbAnswers.Name = "CbAnswers";
			CbAnswers.Size = new System.Drawing.Size(70, 19);
			CbAnswers.TabIndex = 18;
			CbAnswers.Text = "Answers";
			CbAnswers.UseVisualStyleBackColor = true;
			// 
			// CbSolution
			// 
			CbSolution.AutoSize = true;
			CbSolution.Checked = true;
			CbSolution.CheckState = System.Windows.Forms.CheckState.Checked;
			CbSolution.Location = new System.Drawing.Point(110, 127);
			CbSolution.Name = "CbSolution";
			CbSolution.Size = new System.Drawing.Size(70, 19);
			CbSolution.TabIndex = 19;
			CbSolution.Text = "Solution";
			CbSolution.UseVisualStyleBackColor = true;
			// 
			// CbHints
			// 
			CbHints.AutoSize = true;
			CbHints.Checked = true;
			CbHints.CheckState = System.Windows.Forms.CheckState.Checked;
			CbHints.Location = new System.Drawing.Point(180, 127);
			CbHints.Name = "CbHints";
			CbHints.Size = new System.Drawing.Size(54, 19);
			CbHints.TabIndex = 20;
			CbHints.Text = "Hints";
			CbHints.UseVisualStyleBackColor = true;
			// 
			// ExportCrossword
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			ClientSize = new System.Drawing.Size(272, 189);
			Controls.Add(CbHints);
			Controls.Add(CbSolution);
			Controls.Add(CbAnswers);
			Controls.Add(CheckBoxIPUZ);
			Controls.Add(CheckBoxPUZ);
			Controls.Add(BtnCancel);
			Controls.Add(BtnOK);
			Controls.Add(label3);
			Controls.Add(TBCustomEnd);
			Controls.Add(TBCustomStart);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(TBEnd2);
			Controls.Add(TBCurCW2);
			Controls.Add(TbEnd1);
			Controls.Add(TbStart);
			Controls.Add(TbCurCW1);
			Controls.Add(RadioCustom);
			Controls.Add(RadioFromCurrent);
			Controls.Add(RadioAll);
			Controls.Add(RadioCurrent);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "ExportCrossword";
			ShowInTaskbar = false;
			SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Export Options";
			Load += ExportCrossword_Load;
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.Button BtnCancel;
		public System.Windows.Forms.RadioButton RadioCurrent;
		public System.Windows.Forms.RadioButton RadioAll;
		public System.Windows.Forms.RadioButton RadioFromCurrent;
		public System.Windows.Forms.RadioButton RadioCustom;
		public System.Windows.Forms.TextBox TbCurCW1;
		public System.Windows.Forms.TextBox TbStart;
		public System.Windows.Forms.TextBox TbEnd1;
		public System.Windows.Forms.TextBox TBCurCW2;
		public System.Windows.Forms.TextBox TBEnd2;
		public System.Windows.Forms.TextBox TBCustomEnd;
		public System.Windows.Forms.TextBox TBCustomStart;
		private System.Windows.Forms.ToolTip toolTip1;
		public System.Windows.Forms.CheckBox CheckBoxPUZ;
		public System.Windows.Forms.CheckBox CheckBoxIPUZ;
		private System.Windows.Forms.CheckBox CbAnswers;
		private System.Windows.Forms.CheckBox CbSolution;
		private System.Windows.Forms.CheckBox CbHints;
		private System.Windows.Forms.Timer timer1;
	}
}