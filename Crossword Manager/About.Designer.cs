namespace Crossword_Filler
{
	partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.BtnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PBLogo = new System.Windows.Forms.PictureBox();
            this.RtbFeatures = new System.Windows.Forms.RichTextBox();
            this.BtnDonate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnClose.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.ForeColor = System.Drawing.Color.Black;
            this.BtnClose.Location = new System.Drawing.Point(63, 221);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 30);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panel1
            // 
            //this.panel1.BackgroundImage = global::Crossword_Filler.Properties.Resources.cross2;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.PBLogo);
            this.panel1.Controls.Add(this.RtbFeatures);
            this.panel1.Controls.Add(this.BtnDonate);
            this.panel1.Controls.Add(this.BtnClose);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 258);
            this.panel1.TabIndex = 2;
            // 
            // PBLogo
            // 
            //this.PBLogo.Image = global::Crossword_Filler.Properties.Resources.cross;
            this.PBLogo.Location = new System.Drawing.Point(12, 12);
            this.PBLogo.Name = "PBLogo";
            this.PBLogo.Size = new System.Drawing.Size(126, 117);
            this.PBLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBLogo.TabIndex = 4;
            this.PBLogo.TabStop = false;
            // 
            // RtbFeatures
            // 
            this.RtbFeatures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RtbFeatures.ForeColor = System.Drawing.Color.DarkRed;
            this.RtbFeatures.Location = new System.Drawing.Point(12, 12);
            this.RtbFeatures.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.RtbFeatures.Name = "RtbFeatures";
            this.RtbFeatures.Size = new System.Drawing.Size(290, 203);
            this.RtbFeatures.TabIndex = 3;
            this.RtbFeatures.Text = resources.GetString("RtbFeatures.Text");
            // 
            // BtnDonate
            // 
            this.BtnDonate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnDonate.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnDonate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDonate.ForeColor = System.Drawing.Color.Black;
            this.BtnDonate.Location = new System.Drawing.Point(163, 221);
            this.BtnDonate.Name = "BtnDonate";
            this.BtnDonate.Size = new System.Drawing.Size(75, 30);
            this.BtnDonate.TabIndex = 2;
            this.BtnDonate.Text = "Donate";
            this.BtnDonate.UseVisualStyleBackColor = false;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CancelButton = this.BtnClose;
            this.ClientSize = new System.Drawing.Size(318, 262);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About - eCross 2025 by Scintilla";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PBLogo)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		public System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.RichTextBox RtbFeatures;
		public System.Windows.Forms.PictureBox PBLogo;
		public System.Windows.Forms.Button BtnClose;
		public System.Windows.Forms.Button BtnDonate;
	}
}