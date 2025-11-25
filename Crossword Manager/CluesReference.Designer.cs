namespace Crossword_Filler
{
	partial class CluesReference
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
			BtnDelete = new System.Windows.Forms.Button();
			label13 = new System.Windows.Forms.Label();
			TbClueLookUp = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			BtnAddClue = new System.Windows.Forms.Button();
			TbClueDefinition = new System.Windows.Forms.TextBox();
			TbClueText = new System.Windows.Forms.TextBox();
			BtnClose = new System.Windows.Forms.Button();
			LbClueSelect = new System.Windows.Forms.ListBox();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			TbLookup = new System.Windows.Forms.TextBox();
			timer1 = new System.Windows.Forms.Timer(components);
			SuspendLayout();
			// 
			// BtnDelete
			// 
			BtnDelete.Location = new System.Drawing.Point(134, 78);
			BtnDelete.Name = "BtnDelete";
			BtnDelete.Size = new System.Drawing.Size(66, 23);
			BtnDelete.TabIndex = 54;
			BtnDelete.Text = "Delete";
			toolTip1.SetToolTip(BtnDelete, "Delete any duplicate or incorrect/updated meanings");
			BtnDelete.UseVisualStyleBackColor = true;
			BtnDelete.Click += BtnDelete_Click;
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(134, 5);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(57, 15);
			label13.TabIndex = 53;
			label13.Text = "Meaning:";
			// 
			// TbClueLookUp
			// 
			TbClueLookUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			TbClueLookUp.Location = new System.Drawing.Point(134, 22);
			TbClueLookUp.Multiline = true;
			TbClueLookUp.Name = "TbClueLookUp";
			TbClueLookUp.Size = new System.Drawing.Size(128, 54);
			TbClueLookUp.TabIndex = 52;
			toolTip1.SetToolTip(TbClueLookUp, "Shows the meanings(s)");
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label9.Location = new System.Drawing.Point(134, 112);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(116, 15);
			label9.TabIndex = 51;
			label9.Text = "New Clue Defintion";
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(134, 169);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(57, 15);
			label7.TabIndex = 49;
			label7.Text = "Meaning:";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(134, 128);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(58, 15);
			label6.TabIndex = 48;
			label6.Text = "Clue Text:";
			// 
			// BtnAddClue
			// 
			BtnAddClue.Location = new System.Drawing.Point(134, 213);
			BtnAddClue.Name = "BtnAddClue";
			BtnAddClue.Size = new System.Drawing.Size(75, 23);
			BtnAddClue.TabIndex = 46;
			BtnAddClue.Text = "Add";
			toolTip1.SetToolTip(BtnAddClue, "Add to the master list. Will request confirmation is it already exists");
			BtnAddClue.UseVisualStyleBackColor = true;
			BtnAddClue.Click += btnAdd_Click;
			// 
			// TbClueDefinition
			// 
			TbClueDefinition.Location = new System.Drawing.Point(134, 185);
			TbClueDefinition.Name = "TbClueDefinition";
			TbClueDefinition.Size = new System.Drawing.Size(126, 23);
			TbClueDefinition.TabIndex = 45;
			toolTip1.SetToolTip(TbClueDefinition, "Add meaning");
			// 
			// TbClueText
			// 
			TbClueText.Location = new System.Drawing.Point(134, 143);
			TbClueText.Name = "TbClueText";
			TbClueText.Size = new System.Drawing.Size(126, 23);
			TbClueText.TabIndex = 44;
			toolTip1.SetToolTip(TbClueText, "Add new clue text");
			// 
			// BtnClose
			// 
			BtnClose.Location = new System.Drawing.Point(133, 253);
			BtnClose.Name = "BtnClose";
			BtnClose.Size = new System.Drawing.Size(75, 23);
			BtnClose.TabIndex = 55;
			BtnClose.Text = "Close";
			BtnClose.UseVisualStyleBackColor = true;
			BtnClose.Click += BtnClose_Click;
			// 
			// LbClueSelect
			// 
			LbClueSelect.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
			LbClueSelect.FormattingEnabled = true;
			LbClueSelect.ItemHeight = 15;
			LbClueSelect.Location = new System.Drawing.Point(8, 31);
			LbClueSelect.Name = "LbClueSelect";
			LbClueSelect.Size = new System.Drawing.Size(120, 229);
			LbClueSelect.TabIndex = 56;
			LbClueSelect.SelectedIndexChanged += LbClueSelect_SelectedIndexChanged;
			// 
			// TbLookup
			// 
			TbLookup.Location = new System.Drawing.Point(12, 5);
			TbLookup.Name = "TbLookup";
			TbLookup.Size = new System.Drawing.Size(116, 23);
			TbLookup.TabIndex = 57;
			TbLookup.Text = "Enter Search";
			toolTip1.SetToolTip(TbLookup, "Enter lookup word");
			TbLookup.WordWrap = false;
			TbLookup.TextChanged += TbLookup_TextChanged;
			// 
			// CluesReference
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			ClientSize = new System.Drawing.Size(272, 280);
			Controls.Add(TbLookup);
			Controls.Add(LbClueSelect);
			Controls.Add(BtnClose);
			Controls.Add(BtnDelete);
			Controls.Add(label13);
			Controls.Add(TbClueLookUp);
			Controls.Add(label9);
			Controls.Add(label7);
			Controls.Add(label6);
			Controls.Add(BtnAddClue);
			Controls.Add(TbClueDefinition);
			Controls.Add(TbClueText);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "CluesReference";
			ShowInTaskbar = false;
			SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Clues Lookup Reference";
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnDelete;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox TbClueLookUp;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button BtnAddClue;
		private System.Windows.Forms.TextBox TbClueDefinition;
		private System.Windows.Forms.TextBox TbClueText;
		public System.Windows.Forms.Button BtnClose;
		private System.Windows.Forms.ListBox LbClueSelect;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TextBox TbLookup;
		private System.Windows.Forms.Timer timer1;
	}
}