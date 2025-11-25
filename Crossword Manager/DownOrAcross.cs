using System;
using System.Windows.Forms;

namespace Crossword_Filler
{
	public partial class DownOrAcross : Form
	{
		public DownOrAcross()
		{
			InitializeComponent();
			RadioAcross.Click -= RadioAcross_Click;
			RadioDown.Click -= RadioDown_Click;

		}
		private void DownOrAcross_Load(object sender, EventArgs e)
		{
			RadioAcross.Click += RadioAcross_Click;
			RadioDown.Click += RadioDown_Click;
		}

		private void RadioAcross_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void RadioDown_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
