using System;
using System.Windows.Forms;

namespace Crossword_Filler
{
	public partial class About : Form
	{
		public About(Form1 form1)
		{
			InitializeComponent();
			this.AutoScaleMode = AutoScaleMode.Dpi;
			//this.Close();
			DialogResult = System.Windows.Forms.DialogResult.OK;
		}
		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}
	}
}