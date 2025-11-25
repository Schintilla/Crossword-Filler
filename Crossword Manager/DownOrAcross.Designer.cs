namespace Crossword_Filler
{
	partial class DownOrAcross
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
            this.RadioAcross = new System.Windows.Forms.RadioButton();
            this.RadioDown = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // RadioAcross
            // 
            this.RadioAcross.Location = new System.Drawing.Point(26, 12);
            this.RadioAcross.Name = "RadioAcross";
            this.RadioAcross.Size = new System.Drawing.Size(57, 17);
            this.RadioAcross.TabIndex = 0;
            this.RadioAcross.Text = "Across";
            this.RadioAcross.UseVisualStyleBackColor = true;
            this.RadioAcross.Click += new System.EventHandler(this.RadioAcross_Click);
            // 
            // RadioDown
            // 
            this.RadioDown.Location = new System.Drawing.Point(94, 12);
            this.RadioDown.Name = "RadioDown";
            this.RadioDown.Size = new System.Drawing.Size(53, 17);
            this.RadioDown.TabIndex = 1;
            this.RadioDown.Text = "Down";
            this.RadioDown.UseVisualStyleBackColor = true;
            this.RadioDown.Click += new System.EventHandler(this.RadioDown_Click);
            // 
            // DownOrAcross
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(168, 44);
            this.Controls.Add(this.RadioDown);
            this.Controls.Add(this.RadioAcross);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownOrAcross";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Across or Down";
            this.Load += new System.EventHandler(this.DownOrAcross_Load);
            this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.RadioButton RadioAcross;
		public System.Windows.Forms.RadioButton RadioDown;
	}
}