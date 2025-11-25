using System;
using System.Windows.Forms;

namespace Crossword_Filler
{
	public partial class CrossWordInfo : Form
	{
		public CrossWordInfo()
		{
			InitializeComponent();
			this.AutoScaleMode = AutoScaleMode.Dpi;
			BtnOK.DialogResult = DialogResult.OK;
		}
		private void CrossWordInfo_Load(object sender, EventArgs e)
		{
			TbReference.Select(0, 0);
			TbReference.Focus();
		}
		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}
		private void BtnOK_Click(object sender, System.EventArgs e)
		{
			Close();
		}


	}
}
