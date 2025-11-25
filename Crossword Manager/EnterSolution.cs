using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Crossword_Filler
{
	public partial class EnterSolution : Form
	{

		public void DisableRadioButtonAcrossEvent()
		{
			// Unsubscribe the event handler using the -= operator
			RadioAcross.CheckedChanged -= RadioAcross_CheckedChanged;
		}
		public void EnableRadioButtonAcrossEvent()
		{
			// Re-subscribe the event handler using the += operator
			RadioAcross.CheckedChanged += RadioAcross_CheckedChanged;
		}
		public void DisableRadioButtonDownEvent()
		{
			RadioDown.CheckedChanged -= RadioDown_CheckedChanged;
		}
		public void EnableRadioButtonDownEvent()
		{
			RadioDown.CheckedChanged += RadioDown_CheckedChanged;
		}

		private Form1 mainForm;
		private DataGridView dgv2;

		public EnterSolution(Form1 mForm)
		{
			InitializeComponent();
			this.AutoScaleMode = AutoScaleMode.Dpi;
			mainForm = mForm; // Store the reference
			dgv2 = mainForm.dataGridView1;
			this.FormClosing += EnterSolution_FormClosing; // Ensure this line is present
		}
		public void StartEnterSolution(string direction, string cNum, string ans)
		{
			DisableRadioButtonAcrossEvent();
			DisableRadioButtonDownEvent();
			if (direction == "D")
			{
				RadioDown.Checked = true;
				RadioAcross.Enabled = false;
				RadioDown.Enabled = true;
			}
			else if (direction == "A")
			{
				RadioAcross.Checked = true;
				RadioDown.Enabled = false;
				RadioAcross.Enabled = true;
			}
			else
			{
				RadioAcross.Checked = true;
				RadioDown.Enabled = true;
				RadioAcross.Enabled = true;
			}
			EnableRadioButtonAcrossEvent();
			EnableRadioButtonDownEvent();
			this.Text = "Clue No " + cNum.Substring(0, 2).Trim() + " Solution";
			string clueWords = mainForm.CalcWordSplit(ans);
			TbExisting.Text = ans;
			LabExist.Text = "Existing: (" + clueWords.Substring(0, clueWords.Length - 1) + ")";
			this.Top = mainForm.Top + ScalePixelValue(100);
			this.Left = mainForm.Left + ScalePixelValue(20);
			this.ShowDialog();
		}
		private int ScalePixelValue(int value)
		{
			// LogicalToDeviceUnits requires a Size or Point object.
			// We create a Size with the value in the width, use the method, and return the scaled Width.
			return this.LogicalToDeviceUnits(new Size(value, 0)).Width;
		}

		private void BtnOK_Click(object sender, System.EventArgs e)
		{
			string word1 = this.TbExisting.Text;
			string word2 = this.TbSolution.Text;
			if (word2 == "" || word2.Replace(" ", "") == "")
			{
				this.Close();
				return;
			}
			if (word2.Length != word1.Length)
			{
				MessageBox.Show("Not the same lengths");
				TbSolution.Focus();
				return;
			}
			word2 = word2.Replace(" ", "/");
			for (int i = 0; i < word1.Length; i++)
			{
				if (word1.Substring(i, 1) != "_")
				{
					if (word1.Substring(i, 1) != word2.Substring(i, 1))
					{
						MessageBox.Show("Overwrites existing letter(s) or words do not match");
						TbSolution.Focus();
						return;
					}
				}
			}
			word2 = word2.Replace("/", "");
			// DataGridView dgv2 = mainForm.dataGridView1;
			int rNo = int.Parse(TbRowNo.Text);
			int cNo = int.Parse(TbColNo.Text);

			if (this.RadioAcross.Checked == true)
			{
				for (int i = 0; i < word2.Length; i++)
				{
					dgv2.Rows[rNo].Cells[cNo + i].Value = word2.Substring(i, 1);
					dgv2.Rows[rNo].Cells[cNo + i].Style.ForeColor = Color.Black;
					dgv2.Rows[rNo].Cells[cNo + i].Style.BackColor = Color.White;
				}
			}
			else
			{
				for (int i = 0; i < word2.Length; i++)
				{
					dgv2.Rows[rNo + i].Cells[cNo].Value = word2.Substring(i, 1);
					dgv2.Rows[rNo + i].Cells[cNo].Style.ForeColor = Color.Black;
					dgv2.Rows[rNo + i].Cells[cNo].Style.BackColor = Color.White;
				}
			}
			mainForm.updateSolnStatus();
			this.Close();
		}
		private void TbSolution_KeyPress(object sender, KeyPressEventArgs e)
		{
			// Allow letters (a-z, A-Z) and space
			if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != (char)Keys.Back)
			{
				e.Handled = true; // Suppress the key press
			}
		}
		private void RadioAcross_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioAcross.Checked == false)
			{
				DataGridView dgv2 = mainForm.dataGridView1;
				mainForm.ClearDataGridBackColor();
				string ans = mainForm.ReadCrossword("D*", dgv2.CurrentCell.RowIndex, dgv2.CurrentCell.ColumnIndex, dgv2);
				string clueNo = new string(this.Text.SkipWhile(c => !char.IsDigit(c))
						 .TakeWhile(c => char.IsDigit(c))
						 .ToArray());
				mainForm.HighlightClueText(clueNo, "D");
				TbExisting.Text = ans;
				string clueWords = mainForm.CalcWordSplit(ans);
				LabExist.Text = "Existing (" + clueWords.Substring(0, clueWords.Length - 1) + ")";
				TbSolution.Focus();
			}
		}
		private void RadioDown_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioDown.Checked == false)
			{
				DataGridView dgv2 = mainForm.dataGridView1;
				mainForm.ClearDataGridBackColor();
				string ans = mainForm.ReadCrossword("A*", dgv2.CurrentCell.RowIndex, dgv2.CurrentCell.ColumnIndex, dgv2);
				string clueNo = new string(this.Text.SkipWhile(c => !char.IsDigit(c))
						 .TakeWhile(c => char.IsDigit(c))
						 .ToArray());
				mainForm.HighlightClueText(clueNo, "A");
				TbExisting.Text = ans;
				string clueWords = mainForm.CalcWordSplit(ans);
				LabExist.Text = "Existing (" + clueWords.Substring(0, clueWords.Length - 1) + ")";
				TbSolution.Focus();
			}
		}

		private void BtnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		private void EnterSolution_FormClosing(object sender, FormClosingEventArgs e)
		{
			mainForm.ClearDataGridBackColor();
			mainForm.ClearClueTextColour(); // text black
		}

		private void TbSolution_TextChanged(object sender, System.EventArgs e)
		{
			int j = 0;
			string cumLabel = "";
			LabSoln.Text = "";
			for (int i = 0; i < TbSolution.Text.Length; i++)
			{
				j++;
				if (TbSolution.Text.Substring(i, 1) == " ")
				{
					cumLabel = LabSoln.Text + ",";
					j = 0;
				}
				else
				{
					LabSoln.Text = cumLabel + j.ToString();
				}
			}
			LabSoln.Text = "Solution: (" + LabSoln.Text + ")";
		}
	}
}
