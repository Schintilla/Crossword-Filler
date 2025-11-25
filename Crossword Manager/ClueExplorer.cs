using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace Crossword_Filler
{
	public partial class ClueExplorer : Form
	{
		public System.Windows.Forms.TextBox TbClueExplore;
		public System.Windows.Forms.TextBox TbClueNo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button BtnMeaning;
		private System.Windows.Forms.Button BtnSynonym;
		private System.Windows.Forms.Button BtnClose;
		private System.Windows.Forms.Button BtnAnagram;
		private System.Windows.Forms.Button BtnSet;
		private System.Windows.Forms.Button BtnMatch;
		private System.Windows.Forms.Button BtnScramble;
		private System.Windows.Forms.Button BtnAI;
		private System.Windows.Forms.Button BtnMissing;
		private System.Windows.Forms.Button BtnReset;
		private System.Windows.Forms.Button BtnEnterSoln;
		public RadioButton RadioAcross;
		public RadioButton RadioDown;
		public System.Windows.Forms.TextBox TbSolution;
		private Dictionary<string, List<string>> dataRecords = new Dictionary<string, List<string>>();
		public System.Windows.Forms.Label labWordLen;
		private System.Windows.Forms.Label label11;
		public System.Windows.Forms.TextBox TbCurSoln;
		public System.Windows.Forms.TextBox TbRowNo;
		public System.Windows.Forms.TextBox TbColNo;
		private System.Windows.Forms.Button BtnRefVew;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox clickedTB;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;
		private string csvFilePath = "";
		private Form1 mainForm; // Reference to the main form
		private DataGridView dgv3;
		private System.Windows.Forms.Button BtnClueCopy;
		public TextBox TBClueReview;
		private Button BtnSolution;
		private Button BtnHint;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label5;
		private GroupBox groupBox1;
		private int posSoln;
		private int posGuess;
		private int posClueGuess;
		public TextBox tbWordLengths;
		private float scaleFactor;

		public ClueExplorer(Form1 form1)
		{
			InitializeComponent();
			this.AutoScaleMode = AutoScaleMode.Dpi;
			scaleFactor = this.DeviceDpi / 96.0f;
			mainForm = form1; // Assign the reference to the main form
			csvFilePath = Path.Combine(Application.StartupPath, "clue reference.csv");
			this.FormClosing += ClueExplorer_FormClosing;
			dgv3 = mainForm.dataGridView1;
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClueExplorer));
			TbClueExplore = new TextBox();
			TbSolution = new TextBox();
			BtnMeaning = new Button();
			BtnSynonym = new Button();
			BtnClose = new Button();
			BtnAnagram = new Button();
			BtnSet = new Button();
			BtnMatch = new Button();
			BtnScramble = new Button();
			BtnAI = new Button();
			BtnReset = new Button();
			BtnEnterSoln = new Button();
			RadioAcross = new RadioButton();
			RadioDown = new RadioButton();
			TbClueNo = new TextBox();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			BtnMissing = new Button();
			labWordLen = new Label();
			label11 = new Label();
			TbCurSoln = new TextBox();
			TbRowNo = new TextBox();
			TbColNo = new TextBox();
			BtnRefVew = new Button();
			label10 = new Label();
			label12 = new Label();
			toolTip1 = new ToolTip(components);
			TBClueReview = new TextBox();
			BtnSolution = new Button();
			BtnHint = new Button();
			BtnClueCopy = new Button();
			label14 = new Label();
			label5 = new Label();
			groupBox1 = new GroupBox();
			tbWordLengths = new TextBox();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// TbClueExplore
			// 
			TbClueExplore.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			TbClueExplore.Location = new Point(60, 159);
			TbClueExplore.Multiline = true;
			TbClueExplore.Name = "TbClueExplore";
			TbClueExplore.Size = new Size(185, 20);
			TbClueExplore.TabIndex = 0;
			toolTip1.SetToolTip(TbClueExplore, "Enter clue word play or anagram etc");
			TbClueExplore.Click += LetterBox_Click;
			// 
			// TbSolution
			// 
			TbSolution.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			TbSolution.Location = new Point(60, 63);
			TbSolution.Name = "TbSolution";
			TbSolution.Size = new Size(185, 22);
			TbSolution.TabIndex = 1;
			toolTip1.SetToolTip(TbSolution, "Enter likely solution or guesses");
			TbSolution.Click += LetterBox_Click;
			// 
			// BtnMeaning
			// 
			BtnMeaning.Location = new Point(76, 18);
			BtnMeaning.Name = "BtnMeaning";
			BtnMeaning.Size = new Size(75, 23);
			BtnMeaning.TabIndex = 2;
			BtnMeaning.Text = "Meaning";
			toolTip1.SetToolTip(BtnMeaning, resources.GetString("BtnMeaning.ToolTip"));
			BtnMeaning.UseVisualStyleBackColor = true;
			BtnMeaning.Click += BtnMeaning_Click;
			// 
			// BtnSynonym
			// 
			BtnSynonym.Location = new Point(155, 19);
			BtnSynonym.Name = "BtnSynonym";
			BtnSynonym.Size = new Size(75, 23);
			BtnSynonym.TabIndex = 3;
			BtnSynonym.Text = "Synonym";
			toolTip1.SetToolTip(BtnSynonym, "Will look up the synonym of the word in either Clue Review or Likely Solution\r\nIf there is text in both then select the one to look up");
			BtnSynonym.UseVisualStyleBackColor = true;
			BtnSynonym.Click += BtnSynonym_Click;
			// 
			// BtnClose
			// 
			BtnClose.Anchor = AnchorStyles.Bottom;
			BtnClose.Location = new Point(519, 268);
			BtnClose.Name = "BtnClose";
			BtnClose.Size = new Size(75, 23);
			BtnClose.TabIndex = 4;
			BtnClose.Text = "Close";
			BtnClose.UseVisualStyleBackColor = true;
			BtnClose.Click += BtnClose_Click;
			// 
			// BtnAnagram
			// 
			BtnAnagram.Location = new Point(313, 19);
			BtnAnagram.Name = "BtnAnagram";
			BtnAnagram.Size = new Size(75, 23);
			BtnAnagram.TabIndex = 5;
			BtnAnagram.Text = "Anagram";
			toolTip1.SetToolTip(BtnAnagram, "Will look up what anagrams are possible in either Likely Solution or Clue Review\r\nIf there is text in both then select the one to lookup\r\nWill only work on single words and not phrases ");
			BtnAnagram.UseVisualStyleBackColor = true;
			BtnAnagram.Click += BtnAnagram_Click;
			// 
			// BtnSet
			// 
			BtnSet.Location = new Point(251, 63);
			BtnSet.Name = "BtnSet";
			BtnSet.Size = new Size(75, 23);
			BtnSet.TabIndex = 6;
			BtnSet.Text = "Set";
			toolTip1.SetToolTip(BtnSet, "Add the Likely Solution for match assessment");
			BtnSet.UseVisualStyleBackColor = true;
			BtnSet.Click += BtnSet_Click;
			// 
			// BtnMatch
			// 
			BtnMatch.Location = new Point(251, 159);
			BtnMatch.Name = "BtnMatch";
			BtnMatch.Size = new Size(75, 23);
			BtnMatch.TabIndex = 7;
			BtnMatch.Text = "Match";
			toolTip1.SetToolTip(BtnMatch, "Will display each letter and indicate match with the likely solution");
			BtnMatch.UseVisualStyleBackColor = true;
			BtnMatch.Click += BtnMatch_Click;
			// 
			// BtnScramble
			// 
			BtnScramble.Location = new Point(332, 159);
			BtnScramble.Name = "BtnScramble";
			BtnScramble.Size = new Size(75, 23);
			BtnScramble.TabIndex = 8;
			BtnScramble.Text = "Scramble";
			toolTip1.SetToolTip(BtnScramble, "Will scramble the letters that have yet to be matched. Can only use with single words. Need to be the same length as the Likely Solution");
			BtnScramble.UseVisualStyleBackColor = true;
			BtnScramble.Click += BtnScramble_Click;
			// 
			// BtnAI
			// 
			BtnAI.Location = new Point(392, 19);
			BtnAI.Name = "BtnAI";
			BtnAI.Size = new Size(75, 23);
			BtnAI.TabIndex = 9;
			BtnAI.Text = "LLM AI";
			toolTip1.SetToolTip(BtnAI, "Will display the LLM AI lookup");
			BtnAI.UseVisualStyleBackColor = true;
			BtnAI.Click += BtnAI_Click;
			// 
			// BtnReset
			// 
			BtnReset.Location = new Point(332, 63);
			BtnReset.Name = "BtnReset";
			BtnReset.Size = new Size(75, 23);
			BtnReset.TabIndex = 10;
			BtnReset.Text = "Reset";
			toolTip1.SetToolTip(BtnReset, "Rest to the letters in teh crossword if any to date");
			BtnReset.UseVisualStyleBackColor = true;
			BtnReset.Click += BtnReset_Click;
			// 
			// BtnEnterSoln
			// 
			BtnEnterSoln.Location = new Point(413, 63);
			BtnEnterSoln.Name = "BtnEnterSoln";
			BtnEnterSoln.Size = new Size(75, 23);
			BtnEnterSoln.TabIndex = 11;
			BtnEnterSoln.Text = "Enter";
			toolTip1.SetToolTip(BtnEnterSoln, "If solution is found click to Enter into the the crossword");
			BtnEnterSoln.UseVisualStyleBackColor = true;
			BtnEnterSoln.Click += BtnEnterSoln_Click;
			// 
			// RadioAcross
			// 
			RadioAcross.AutoSize = true;
			RadioAcross.Checked = true;
			RadioAcross.Location = new Point(195, 4);
			RadioAcross.Name = "RadioAcross";
			RadioAcross.Size = new Size(69, 19);
			RadioAcross.TabIndex = 12;
			RadioAcross.TabStop = true;
			RadioAcross.Text = "ACROSS";
			toolTip1.SetToolTip(RadioAcross, "Switch to ACROSS if available");
			RadioAcross.UseVisualStyleBackColor = true;
			// 
			// RadioDown
			// 
			RadioDown.AutoSize = true;
			RadioDown.Location = new Point(270, 4);
			RadioDown.Name = "RadioDown";
			RadioDown.Size = new Size(62, 19);
			RadioDown.TabIndex = 13;
			RadioDown.TabStop = true;
			RadioDown.Text = "DOWN";
			toolTip1.SetToolTip(RadioDown, "Select DOWN if available");
			RadioDown.UseVisualStyleBackColor = true;
			// 
			// TbClueNo
			// 
			TbClueNo.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			TbClueNo.ForeColor = Color.Red;
			TbClueNo.Location = new Point(51, 3);
			TbClueNo.Name = "TbClueNo";
			TbClueNo.Size = new Size(44, 24);
			TbClueNo.TabIndex = 14;
			TbClueNo.TextAlign = HorizontalAlignment.Center;
			// 
			// label1
			// 
			label1.Location = new Point(6, 184);
			label1.Name = "label1";
			label1.Size = new Size(49, 37);
			label1.TabIndex = 15;
			label1.Text = "Clue Edit:";
			// 
			// label2
			// 
			label2.Location = new Point(6, 58);
			label2.Name = "label2";
			label2.Size = new Size(59, 37);
			label2.TabIndex = 16;
			label2.Text = "Likely Solution:";
			// 
			// label3
			// 
			label3.Location = new Point(6, 3);
			label3.Name = "label3";
			label3.Size = new Size(42, 35);
			label3.TabIndex = 17;
			label3.Text = "Clue No.";
			// 
			// label4
			// 
			label4.Location = new Point(6, 112);
			label4.Name = "label4";
			label4.Size = new Size(107, 18);
			label4.TabIndex = 18;
			label4.Text = "Clue Match:";
			// 
			// BtnMissing
			// 
			BtnMissing.Location = new Point(234, 19);
			BtnMissing.Name = "BtnMissing";
			BtnMissing.Size = new Size(75, 23);
			BtnMissing.TabIndex = 24;
			BtnMissing.Text = "Missing ?";
			toolTip1.SetToolTip(BtnMissing, "Will look up possible letters that might fill in the gaps in the Likely Solution\r\n");
			BtnMissing.UseVisualStyleBackColor = true;
			BtnMissing.Click += BtnMissing_Click;
			// 
			// labWordLen
			// 
			labWordLen.Location = new Point(343, 5);
			labWordLen.Name = "labWordLen";
			labWordLen.Size = new Size(53, 18);
			labWordLen.TabIndex = 31;
			labWordLen.Text = "Words:";
			// 
			// label11
			// 
			label11.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
			label11.Location = new Point(97, 8);
			label11.Name = "label11";
			label11.Size = new Size(90, 15);
			label11.TabIndex = 32;
			label11.Text = "Click a Clue No.";
			// 
			// TbCurSoln
			// 
			TbCurSoln.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			TbCurSoln.Location = new Point(567, 63);
			TbCurSoln.Name = "TbCurSoln";
			TbCurSoln.Size = new Size(82, 22);
			TbCurSoln.TabIndex = 34;
			// 
			// TbRowNo
			// 
			TbRowNo.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			TbRowNo.Location = new Point(567, 91);
			TbRowNo.Name = "TbRowNo";
			TbRowNo.Size = new Size(45, 22);
			TbRowNo.TabIndex = 35;
			// 
			// TbColNo
			// 
			TbColNo.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			TbColNo.Location = new Point(567, 119);
			TbColNo.Name = "TbColNo";
			TbColNo.Size = new Size(45, 22);
			TbColNo.TabIndex = 36;
			// 
			// BtnRefVew
			// 
			BtnRefVew.Location = new Point(4, 18);
			BtnRefVew.Name = "BtnRefVew";
			BtnRefVew.Size = new Size(68, 23);
			BtnRefVew.TabIndex = 37;
			BtnRefVew.Text = "Open dB";
			toolTip1.SetToolTip(BtnRefVew, "Will open the database or reference of clue definitions to date.");
			BtnRefVew.UseVisualStyleBackColor = true;
			BtnRefVew.Click += BtnRefVew_Click;
			// 
			// label10
			// 
			label10.Image = Properties.Resources.UpDown;
			label10.ImageAlign = ContentAlignment.MiddleLeft;
			label10.Location = new Point(130, 110);
			label10.Name = "label10";
			label10.Size = new Size(21, 18);
			label10.TabIndex = 38;
			label10.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.ImageAlign = ContentAlignment.MiddleLeft;
			label12.Location = new Point(157, 112);
			label12.Name = "label12";
			label12.Size = new Size(239, 15);
			label12.TabIndex = 39;
			label12.Text = "Select below and move into the above gaps ";
			label12.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// TBClueReview
			// 
			TBClueReview.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			TBClueReview.Location = new Point(60, 181);
			TBClueReview.Multiline = true;
			TBClueReview.Name = "TBClueReview";
			TBClueReview.Size = new Size(185, 40);
			TBClueReview.TabIndex = 44;
			toolTip1.SetToolTip(TBClueReview, "Dissect the clue for anagrams, word play etc and copy to the above text box to check for a match. A process of elimination");
			// 
			// BtnSolution
			// 
			BtnSolution.Location = new Point(130, 48);
			BtnSolution.Name = "BtnSolution";
			BtnSolution.Size = new Size(75, 23);
			BtnSolution.TabIndex = 45;
			BtnSolution.Text = "Solution";
			toolTip1.SetToolTip(BtnSolution, "Will display solution embedded in the Crossword");
			BtnSolution.UseVisualStyleBackColor = true;
			BtnSolution.Click += BtnSolution_Click;
			// 
			// BtnHint
			// 
			BtnHint.Location = new Point(229, 48);
			BtnHint.Name = "BtnHint";
			BtnHint.Size = new Size(75, 23);
			BtnHint.TabIndex = 46;
			BtnHint.Text = "Hint";
			toolTip1.SetToolTip(BtnHint, "Will display how the answer was derived or hint at the answer embedded in the Crossword");
			BtnHint.UseVisualStyleBackColor = true;
			BtnHint.Click += BtnHint_Click;
			// 
			// BtnClueCopy
			// 
			BtnClueCopy.Location = new Point(413, 159);
			BtnClueCopy.Name = "BtnClueCopy";
			BtnClueCopy.Size = new Size(75, 23);
			BtnClueCopy.TabIndex = 43;
			BtnClueCopy.Text = "Clue";
			toolTip1.SetToolTip(BtnClueCopy, "Copy the clue text for review. Does not apply to clues in image format");
			BtnClueCopy.UseVisualStyleBackColor = true;
			BtnClueCopy.Click += BtnClueCopy_Click;
			// 
			// label14
			// 
			label14.Location = new Point(252, 183);
			label14.Name = "label14";
			label14.Size = new Size(234, 46);
			label14.TabIndex = 47;
			label14.Text = "Look for anagram, synonyms, word play, reversals, containers, abbreviations etc. Paste words above to check the match";
			// 
			// label5
			// 
			label5.Location = new Point(6, 162);
			label5.Name = "label5";
			label5.Size = new Size(49, 16);
			label5.TabIndex = 48;
			label5.Text = "Check:";
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(BtnMissing);
			groupBox1.Controls.Add(BtnMeaning);
			groupBox1.Controls.Add(BtnSynonym);
			groupBox1.Controls.Add(BtnHint);
			groupBox1.Controls.Add(BtnAnagram);
			groupBox1.Controls.Add(BtnSolution);
			groupBox1.Controls.Add(BtnAI);
			groupBox1.Controls.Add(BtnRefVew);
			groupBox1.Location = new Point(8, 227);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(480, 75);
			groupBox1.TabIndex = 49;
			groupBox1.TabStop = false;
			groupBox1.Text = "Helpers:";
			// 
			// tbWordLengths
			// 
			tbWordLengths.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			tbWordLengths.ForeColor = Color.Red;
			tbWordLengths.Location = new Point(389, 3);
			tbWordLengths.Name = "tbWordLengths";
			tbWordLengths.Size = new Size(44, 25);
			tbWordLengths.TabIndex = 50;
			// 
			// ClueExplorer
			// 
			AutoScaleDimensions = new SizeF(96F, 96F);
			AutoScaleMode = AutoScaleMode.Dpi;
			ClientSize = new Size(495, 303);
			Controls.Add(tbWordLengths);
			Controls.Add(label14);
			Controls.Add(groupBox1);
			Controls.Add(TBClueReview);
			Controls.Add(BtnClueCopy);
			Controls.Add(label12);
			Controls.Add(label10);
			Controls.Add(TbColNo);
			Controls.Add(TbRowNo);
			Controls.Add(TbCurSoln);
			Controls.Add(label11);
			Controls.Add(labWordLen);
			Controls.Add(TbClueNo);
			Controls.Add(RadioDown);
			Controls.Add(RadioAcross);
			Controls.Add(BtnEnterSoln);
			Controls.Add(BtnReset);
			Controls.Add(BtnScramble);
			Controls.Add(BtnMatch);
			Controls.Add(BtnSet);
			Controls.Add(BtnClose);
			Controls.Add(TbSolution);
			Controls.Add(TbClueExplore);
			Controls.Add(label5);
			Controls.Add(label4);
			Controls.Add(label1);
			Controls.Add(label2);
			Controls.Add(label3);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "ClueExplorer";
			ShowInTaskbar = false;
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.Manual;
			Text = "Clue Explorer";
			FormClosing += ClueExplorer_FormClosing;
			Load += ClueExplorer_Load;
			groupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();

		}

		private int ScalePixelValue(int value)
		{
			// LogicalToDeviceUnits requires a Size or Point object.
			// We create a Size with the value in the width, use the method, and return the scaled Width.
			return this.LogicalToDeviceUnits(new Size(value, 0)).Width;
		}
		private void ClueExplorer_Load(object sender, EventArgs e)
		{
			//AdjustControlsForDPI();
			label12.SendToBack();
			label14.SendToBack();
			posSoln =label3.Bottom-5;
			posGuess=label2.Bottom-5;
			posClueGuess=label4.Bottom - 2;

			AddTextBoxes(19, posSoln, "TbSoln");
			AddTextBoxes(19, posGuess, "TbGuess");
			AddTextBoxes(19, posClueGuess, "TbClueGuess");
		}
		private void AdjustControlsForDPI()
		{
			foreach (Control control in this.Controls)
			{
				if (control is Label label)
				{
					LoadSVG.LblHeightTextForDPI(label);
				}
				else if (control is Button button)
				{
					LoadSVG.BtnHeightTextForDPI(button);
				}
			}
		}
		private void AddTextBoxes(int qty, int pos, string tbName)
		{
			Font boldFont = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
			for (int i = 0; i < qty; i++)
			{
				System.Windows.Forms.TextBox newTextBox = new System.Windows.Forms.TextBox();
				newTextBox.Name = tbName + i.ToString();
				newTextBox.Location = new System.Drawing.Point(15 + (i * 25), pos);
				newTextBox.Size = new System.Drawing.Size(25, 20);
				newTextBox.Font = boldFont;
				newTextBox.TextAlign = HorizontalAlignment.Center;
				newTextBox.BackColor = Color.White;
				newTextBox.Click += LetterBox_Click;
				//newTextBox.BringToFront();
				this.Controls.Add(newTextBox);
				this.Controls.SetChildIndex(newTextBox, 0);
			}
		}

		public void AddClueSelection(string direction, string cNum, string ans)
		{
			RadioAcross.CheckedChanged -= RadioAcross_CheckedChanged;
			RadioDown.CheckedChanged -= RadioDown_CheckedChanged;
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
			RadioAcross.CheckedChanged += RadioAcross_CheckedChanged;
			RadioDown.CheckedChanged += RadioDown_CheckedChanged;

			string clueWords = mainForm.CalcWordSplit(ans);
			TbSolution.Text = ans.Replace("_", "?");
			TbCurSoln.Text = ans;
			tbWordLengths.Text = "(" + clueWords.Substring(0, clueWords.Length - 1) + ")";
			TbClueNo.Text = cNum.Substring(0, 2);
			TbClueExplore.Text = "";
			BtnClueCopy.PerformClick();
			UpdateClueExplorer(ans, posSoln, "TbSoln");
			UpdateClueExplorer(ans, posGuess, "TbGuess");
			UpdateClueExplorer("", posClueGuess, "TbClueGuess");
		}
		private void RadioAcross_CheckedChanged(object sender, EventArgs e)
		{
			if (RadioAcross.Checked == false)
			{
				DirectionChange("D");
			}
		}
		private void RadioDown_CheckedChanged(object sender, EventArgs e)
		{
			if (RadioDown.Checked == false)
			{
				DirectionChange("A");
			}
		}
		private void DirectionChange (string direction)
		{
			mainForm.ClearDataGridBackColor();
			string ans = mainForm.ReadCrossword(direction+"*", dgv3.CurrentCell.RowIndex, dgv3.CurrentCell.ColumnIndex, dgv3);
			string cText = mainForm.HighlightClueText(TbClueNo.Text, direction);
			TbCurSoln.Text = ans;
			TbSolution.Text = ans.Replace("_", "?");
			string clueWords = mainForm.CalcWordSplit(ans);
			tbWordLengths.Text = "(" + clueWords.Substring(0, clueWords.Length - 1) + ")";
			UpdateClueExplorer(ans, posSoln, "TbSoln");
			UpdateClueExplorer(ans, posGuess, "TbGuess");
			TBClueReview.Text = cText;
		}
		public void UpdateClueExplorer(string soln, int pos, string tbName)
		{
			for (int i = 0; i < soln.Length; i++)
			{
				Control targetControl = this.Controls[tbName + i.ToString()];
				targetControl.BackColor = Color.White;
				targetControl.Text = soln.Substring(i, 1);
				//targetControl.BringToFront();
			}
			for (int i = soln.Length; i < 19; i++)
			{
				Control targetControl = this.Controls[tbName + i.ToString()];
				targetControl.BackColor = Color.LightGray;
				targetControl.Text = "";
				//targetControl.BringToFront();
			}
			//label3.SendToBack();
			//label2.SendToBack();
			//label4.SendToBack();

		}

		private void BtnSet_Click(object sender, EventArgs e)
		{
			string soln = TbSolution.Text;
			if (soln != "")
			{
				soln = soln.Replace("?", "_").ToUpper();
				soln = soln.Replace(" ", "/").ToUpper();
				UpdateClueExplorer(soln, posGuess, "TbGuess");
				string existSoln = WordFromLetters("TbSoln");
				UpdateClueExplorer(existSoln, posSoln, "TbSoln");
				UpdateClueExplorer("", posClueGuess, "TbClueGuess");
			}
		}
		private void BtnReset_Click(object sender, EventArgs e)
		{
			string ans = TbCurSoln.Text;
			UpdateClueExplorer(ans, posSoln, "TbSoln");
			UpdateClueExplorer(ans, posGuess, "TbGuess");
			UpdateClueExplorer("", posClueGuess, "TbClueGuess");
			string existSoln = WordFromLetters("TbSoln");
			TbSolution.Text = "";
		}
		private void BtnEnterSoln_Click(object sender, EventArgs e)
		{
			string soln = WordFromLetters("TbGuess");
			if (soln == "")
			{
				MessageBox.Show("No solution");
				return;
			}
			if (soln.Contains('_'))
			{
				MessageBox.Show("Solution still has ? in it");
				return;
			}
			if (soln.Length != TbCurSoln.Text.Length)
			{
				MessageBox.Show("Not the same lengths");
				return;
			}
			soln = soln.Replace("/", "");
			dgv3.EndEdit();
			int rNo = int.Parse(TbRowNo.Text);
			int cNo = int.Parse(TbColNo.Text);
			if (this.RadioAcross.Checked == true)
			{
				for (int i = 0; i < soln.Length; i++)
				{
					dgv3.Rows[rNo].Cells[cNo + i].Value = soln.Substring(i, 1);
					dgv3.Rows[rNo].Cells[cNo + i].Style.ForeColor = Color.Black;
				}
			}
			else
			{
				for (int i = 0; i < soln.Length; i++)
				{
					dgv3.Rows[rNo + i].Cells[cNo].Value = soln.Substring(i, 1);
					dgv3.Rows[rNo + i].Cells[cNo].Style.ForeColor = Color.Black;
				}
			}
			mainForm.updateSolnStatus();
		}
		private void BtnClueCopy_Click(object sender, EventArgs e)
		{
			TBClueReview.Text = "";
			if (mainForm.TbRichTextAcrossClues.Visible == true && TbClueNo.Text != "")
			{
				string direction = RadioAcross.Checked == true ? "A" : "D";
				string clue = mainForm.HighlightClueText(TbClueNo.Text, direction).Trim();
				clue = clue.Substring(2).Trim();
				TBClueReview.Text = clue;
			}
		}
		private void BtnMatch_Click(object sender, EventArgs e)
		{
			string soln = TbClueExplore.Text;
			UpdateClueExplorer("", posClueGuess, "TbClueGuess");
			string compare = WordFromLetters("TbGuess");
			UpdateClueExplorer(compare, posGuess, "TbGuess");
			string existSoln = WordFromLetters("TbSoln");
			UpdateClueExplorer(existSoln, posSoln, "TbSoln");
			if (soln != "")
			{
				soln = soln.Replace("?", "_").ToUpper();
				soln = soln.Replace(" ", "/");
				UpdateClueExplorer(soln, posClueGuess, "TbClueGuess");
				MatchLetters(soln, compare, "TbGuess");
				MatchLetters(soln, existSoln, "TbSoln");
				string clueWords = mainForm.CalcWordSplit(soln);
				label4.Text = "Clue Match (" + clueWords.Substring(0, clueWords.Length - 1) + ")";
			}
		}
		private void MatchLetters(string soln, string compare, string TbSelect)
		{
			int mIdx = 0;
			for (int j = 0; j < soln.Length; j++)
			{
				string omitChar = soln.Substring(j, 1);
				if (omitChar != "/" && omitChar != "_")
				{
					for (int i = 0; i < compare.Length; i++)
					{
						if (omitChar == compare.Substring(i, 1))
						{
							if (this.Controls[TbSelect + i.ToString()].BackColor != Color.Green)
							{
								mIdx = i + 1;
								break;
							}
						}
					}
					if (mIdx > 0)
					{
						this.Controls["TbClueGuess" + j.ToString()].BackColor = Color.Green;
						this.Controls[TbSelect + (mIdx - 1).ToString()].BackColor = Color.Green;
						mIdx = 0;
					}
				}
			}
		}
		private void BtnScramble_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(TbClueExplore.Text)) return;
			int wordLen1 = this.Controls.OfType<System.Windows.Forms.TextBox>().Count(tb => tb.Name.StartsWith("TbClueGuess") && !string.IsNullOrWhiteSpace(tb.Text));
			int wordLen2 = this.Controls.OfType<System.Windows.Forms.TextBox>().Count(tb => tb.Name.StartsWith("TbGuess") && !string.IsNullOrWhiteSpace(tb.Text));
			if (wordLen1 != wordLen2)
			{
				MessageBox.Show("Words need to be the same lemgth");
				return;
			}
			//Color lightgreenBG = Color.FromArgb(192, 255, 192);
			string wScramble = "";
			// max number of boxes is 20
			int j = 0;
			for (j = 0; j < 20; j++)
			{
				Control[] foundControls = this.Controls.Find("TbClueGuess" + j.ToString(), true);
				if (foundControls.Length != 0 && foundControls[0].Text != "")
				{
					System.Windows.Forms.TextBox sBox = (System.Windows.Forms.TextBox)this.Controls["TbClueGuess" + j];
					if (sBox.BackColor != Color.Green && sBox.BackColor != Color.LightGreen)
					{
						wScramble += sBox.Text; // Concatenate the values
					}
				}
				else
				{
					break;
				}
			}
			// j is the length
			if (string.IsNullOrEmpty(wScramble)) return;
			if (wScramble.Contains("/"))
			{
				MessageBox.Show("Can only scramble single words");
				return;
			}

			for (int k = 0; k < j; k++) // max boxes is 20
			{
				Control[] foundControls = this.Controls.Find("TbGuess" + k.ToString(), true);
				if (foundControls.Length != 0)
				{
					System.Windows.Forms.TextBox sBox = (System.Windows.Forms.TextBox)this.Controls["TbGuess" + k];
					System.Windows.Forms.TextBox cBox = (System.Windows.Forms.TextBox)this.Controls["TbClueGuess" + k];
					if (sBox.BackColor == Color.Green) // Adjust the color check as needed
					{
						cBox.Text = sBox.Text;
						cBox.BackColor = Color.Green;
					}
					else if (sBox.BackColor == Color.LightGreen)
					{
						cBox.Text = sBox.Text;
						cBox.BackColor = Color.LightGreen;
					}
					else
					{
						cBox.Text = "_";
						cBox.BackColor = Color.White; // Adjust the color as needed
					}
				}
				else
				{
					break;
				}
			}
			int jIndex = 0;
			string nClueLet = ScrambleWord(wScramble);
			for (int i = 0; i < j; i++)
			{
				System.Windows.Forms.TextBox cBox = (System.Windows.Forms.TextBox)this.Controls["TbClueGuess" + i];
				if (cBox.BackColor != Color.Green && cBox.BackColor != Color.LightGreen)
				{
					if (jIndex < nClueLet.Length)
					{
						cBox.Text = nClueLet[jIndex].ToString();
						jIndex++;
					}
				}
			}
		}
		public string ScrambleWord(string word)
		{
			// Convert the word into an array of characters
			char[] letterArray = word.ToCharArray();

			// Create a random number generator
			Random random = new Random();

			// Shuffle the array using Fisher-Yates algorithm
			for (int i = letterArray.Length - 1; i > 0; i--)
			{
				// Generate a random index
				int j = random.Next(0, i + 1);

				// Swap the letters
				char temp = letterArray[i];
				letterArray[i] = letterArray[j];
				letterArray[j] = temp;
			}

			// Convert the shuffled array back into a string
			return new string(letterArray);
		}
		private void LetterBox_Click(object sender, EventArgs e)
		{
			clickedTB = (System.Windows.Forms.TextBox)sender;
			if (clickedTB.Name.Contains("TbSolution") || clickedTB.Name.Contains("TbClueExplore"))
			{
				return;
			}
			// MessageBox.Show(clickedTB.Name + " clicked!");
			if (clickedTB.Name.Contains("TbClueGuess"))
			{
				if (clickedTB.BackColor == Color.Yellow)
				{
					clickedTB.BackColor = Color.White;
				}
				else if (clickedTB.BackColor == Color.White)
				{
					for (int j = 0; j < 20; j++)
					{
						Control[] foundControls = this.Controls.Find("TbClueGuess" + j.ToString(), true);
						if (foundControls.Length != 0)
						{
							System.Windows.Forms.TextBox sBox = (System.Windows.Forms.TextBox)this.Controls["TbClueGuess" + j];
							if (clickedTB.Name == "TbClueGuess" + j)
							{
								sBox.BackColor = Color.Yellow;
							}
							else if (sBox.BackColor == Color.Yellow)
							{
								sBox.BackColor = Color.White;
							}
						}
						else
						{
							break;
						}
					}
				}
			}
			if (clickedTB.Name.Contains("TbGuess"))
			{
				if (clickedTB.BackColor == Color.Green || (clickedTB.BackColor == Color.White && clickedTB.Text == ""))
				{
					return;
				}
				if (clickedTB.BackColor == Color.LightGreen)
				{
					for (int j = 0; j < 20; j++)
					{
						Control[] foundControls = this.Controls.Find("TbClueGuess" + j.ToString(), true);
						if (foundControls.Length != 0)
						{
							System.Windows.Forms.TextBox sBox = (System.Windows.Forms.TextBox)this.Controls["TbClueGuess" + j];
							if (sBox.Text == clickedTB.Text && sBox.BackColor == Color.LightGreen)
							{
								sBox.BackColor = Color.White;
								break;
							}
						}
						else
						{
							break;
						}
					}
					clickedTB.BackColor = Color.White;
					clickedTB.Text = "_";
				}
				if (clickedTB.Text == "_")
				{
					for (int j = 0; j < 20; j++)
					{
						Control[] foundControls = this.Controls.Find("TbClueGuess" + j.ToString(), true);
						if (foundControls.Length != 0)
						{
							System.Windows.Forms.TextBox sBox = (System.Windows.Forms.TextBox)this.Controls["TbClueGuess" + j];
							if (sBox.BackColor == Color.Yellow)
							{
								clickedTB.BackColor = Color.LightGreen;
								clickedTB.Text = sBox.Text;
								sBox.BackColor = Color.LightGreen;
							}
						}
						else
						{
							break;
						}
					}
				}
				clickedTB = null;
				// if clicked on light green then remove both green and remove letter
			}
		}

		public string WordFromLetters(string ctrl)
		{
			string word = "";
			for (int i = 0; i < 20; i++)
			{
				Control[] foundControls = this.Controls.Find(ctrl + i.ToString(), true);
				if (foundControls.Length == 0)
					break;
				else
				{
					word = word + this.Controls[ctrl + i.ToString()].Text;
					// this.Controls[ctrl + i.ToString()].BackColor = Color.White;
				}
			}
			if (word == "")
			{
				MessageBox.Show("No solution");
				return "";
			}
			word = word.ToUpper();
			return word;
		}

		private void BtnMeaning_Click(object sender, System.EventArgs e)
		{
			string searchURL;
			string wdSearch = LookUpWd();
			if (wdSearch == "")	{return;}
			if (wdSearch.IndexOf(" ") > 0)
			{
				searchURL = "https://www.onelook.com/thesaurus/?s=" + wdSearch;
			}
			else
			{
				searchURL = "https://www.merriam-webster.com/dictionary/" + wdSearch;
			}
			Process.Start(new ProcessStartInfo
			{
				FileName = searchURL,
				UseShellExecute = true
			});
		}
		private void BtnSynonym_Click(object sender, EventArgs e)
		{
			string wdSearch = LookUpWd();
			if (wdSearch == ""){return;}
			string searchURL = "https://www.thesaurus.com/browse/" + wdSearch;
			Process.Start(new ProcessStartInfo
			{
				FileName = searchURL,
				UseShellExecute = true
			});
		}
		private void BtnMissing_Click(object sender, EventArgs e)
		{
			string wd = WordFromLetters("TbGuess");
			if (wd == "" || wd.IndexOf("_") < 0){return;}
			string searchURL = "https://www.crosswordsolver.org/solve/" + wd;
			Process.Start(new ProcessStartInfo
			{
				FileName = searchURL,
				UseShellExecute = true
			});
		}
		private void BtnAnagram_Click(object sender, EventArgs e)
		{
			string wdSearch = LookUpWd();
			if (wdSearch == "") return;
			string searchURL = "https://word.tips/unscramble/" + wdSearch;
			Process.Start(new ProcessStartInfo
			{
				FileName = searchURL,
				UseShellExecute = true
			});
		}
		private void BtnAI_Click(object sender, EventArgs e)
		{
			string soln = WordFromLetters("TbSoln");
			if (soln.IndexOf("_") < 0){return;}
			if (Application.OpenForms["GotoAI"] == null)
			{
				GotoAI gotoAIForm = new GotoAI();
				gotoAIForm.Top = this.Top - ScalePixelValue(250);
				gotoAIForm.Left = this.Left + ScalePixelValue(20);
				string direction;
				if (mainForm.TbRichTextAcrossClues.Visible == true)
				{
					string clueNo = TbClueNo.Text;
					string clue = "";
					direction = RadioAcross.Checked == true ? "A" : "D";
					clue = mainForm.HighlightClueText(clueNo, direction);
					clue = clue.Substring(2);
					clue = clue.Replace("\r\n", "").Replace("\n", "");
					clue = clue.Replace("\u200B", "");
					clue = clue.Trim();
					gotoAIForm.RtbPrompt.Text = "Solve Crossword clue (may be cryptic): " + clue;
				}
				gotoAIForm.ShowDialog();
			}
			else
			{
				GotoAI gotoAIForm = Application.OpenForms.OfType<GotoAI>().FirstOrDefault();
				gotoAIForm.Close();
			}
		}
		private void BtnHint_Click(object sender, EventArgs e)
		{
			mainForm.HintDisplay();
		}
		private void BtnSolution_Click(object sender, EventArgs e)
		{
			mainForm.SolutionDisplay();
		}

		private string LookUpWd()
		{
			string wd;
			string wdExp = TbClueExplore.Text;
			string wdSoln = TbSolution.Text;
			if (wdExp == "" && wdSoln == "")
			{
				MessageBox.Show("No text to look up");
				return "";
			}
			if (wdExp != "" && wdSoln == "")
			{
				wd = wdExp;
			}
			else if (wdExp == "" && wdSoln != "")
			{
				wd = wdSoln;
			}
			else if (clickedTB != null && clickedTB.Name == "TbClueExplore")
			{
				wd = wdExp;
			}
			else if (clickedTB != null && clickedTB.Name == "TbSolution")
			{
				wd = wdSoln;
			}
			else
			{
				MessageBox.Show("Click the textbox to look up");
				return "";
			}
			clickedTB = null;
			return wd;
		}

		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private void ClueExplorer_FormClosing(object sender, FormClosingEventArgs e)
		{
			mainForm.ClearClueTextColour();
			mainForm.ClearDataGridBackColor();
		}

		// Cryptic Clue Database
		private void BtnRefVew_Click(object sender, EventArgs e)
		{
			CluesReference clueRefenceForm = new CluesReference();
			clueRefenceForm.Top = this.Top - ScalePixelValue(200);
			clueRefenceForm.Left = this.Left + ScalePixelValue(50);
			clueRefenceForm.Show();
		}

	}
}
