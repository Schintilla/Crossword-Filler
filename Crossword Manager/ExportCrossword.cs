using System;
using System.Windows.Forms;

namespace Crossword_Filler
{
	public partial class ExportCrossword : Form
	{
		public ExportCrossword()
		{
			InitializeComponent();
			this.AutoScaleMode = AutoScaleMode.Dpi;
		}
		private void ExportCrossword_Load(object sender, EventArgs e)
		{

		}
		private void BtnOK_Click(object sender, EventArgs e)
		{
			if (RadioCustom.Checked == true)
			{
				if (TBCustomStart.Text != "" && TBCustomEnd.Text != "")
				{
					if (int.Parse(TBCustomStart.Text) < 0 || int.Parse(TBCustomEnd.Text) > int.Parse(TbEnd1.Text))
					{
						MessageBox.Show("Invalid range");
						return;
					}
					if (int.Parse(TBCustomStart.Text) > int.Parse(TBCustomEnd.Text))
					{
						MessageBox.Show("Invalid range");
						return;
					}
				}
				else
				{
					MessageBox.Show("Invalid range");
					return;
				}
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		private void TBCustomStart_Enter(object sender, EventArgs e)
		{
			RadioCustom.Checked = true;
		}
		private void TBCustomEnd_Enter(object sender, EventArgs e)
		{
			RadioCustom.Checked = true;
		}

		private void RadioCurrent_Click(object sender, EventArgs e)
		{
			// RadioCurrent.Checked = true;
		}
		private void CheckBoxPUZ_CheckedChanged(object sender, EventArgs e)
		{
			RadioCurrent.Checked = true;
			if (CheckBoxPUZ.Checked && CheckBoxIPUZ.Checked)
			{
				CheckBoxIPUZ.Checked = false;
				RadioCurrent.Checked = true;
			}
			else if (CheckBoxPUZ.Checked == false && CheckBoxIPUZ.Checked == false)
			{
				RadioCurrent.Checked = false;
			}
		}
		private void CheckBoxIPUZ_CheckedChanged(object sender, EventArgs e)
		{
			RadioCurrent.Checked = true;
			if (CheckBoxPUZ.Checked && CheckBoxIPUZ.Checked)
			{
				CheckBoxPUZ.Checked = false;
				RadioCurrent.Checked = true;
			}
			else if (CheckBoxPUZ.Checked == false && CheckBoxIPUZ.Checked == false)
			{
				RadioCurrent.Checked = false;
			}
		}
		private void RadioCurrent_CheckedChanged(object sender, EventArgs e)
		{
			if (RadioCurrent.Checked == false)
			{
				CheckBoxPUZ.Checked = false;
				CheckBoxIPUZ.Checked = false;
			}
		}
		private void RadioFromCurrent_CheckedChanged(object sender, EventArgs e)
		{
			// CheckBoxPUZ.Checked = false;
			// CheckBoxIPUZ.Checked = false;
		}
		private void RadioAll_CheckedChanged(object sender, EventArgs e)
		{
			// CheckBoxPUZ.Checked = false;
			// CheckBoxIPUZ.Checked = false;
		}
		private void RadioCustom_CheckedChanged(object sender, EventArgs e)
		{
			// CheckBoxPUZ.Checked = false;
			// CheckBoxIPUZ.Checked = false;
		}
	}
}
