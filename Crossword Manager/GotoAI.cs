using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Crossword_Filler
{
	public partial class GotoAI : Form
	{
		private RadioButton RadioPerplexity;
		private RadioButton RadioChatGPT;
		private RadioButton RadioGemini;
		private System.Windows.Forms.Button BtnKnown;
		private System.Windows.Forms.Button BtnGoto;
		private System.Windows.Forms.Button tnCancel;
		private Label label1;
		private Label label2;
		private Label label3;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;
		private RadioButton RadioCustom;
		private System.Windows.Forms.TextBox TbCustomAI;
		public RichTextBox RtbPrompt;
		private RadioButton RadioPoe;

		public GotoAI()
		{
			InitializeComponent();
			this.AutoScaleMode = AutoScaleMode.Dpi;
		}

		private void GotoAI_Load(object sender, EventArgs e)
		{
			SetFocusEnd(RtbPrompt);
		}
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			RadioPerplexity = new RadioButton();
			RadioChatGPT = new RadioButton();
			RadioGemini = new RadioButton();
			RadioPoe = new RadioButton();
			BtnKnown = new Button();
			BtnGoto = new Button();
			tnCancel = new Button();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			toolTip1 = new ToolTip(components);
			TbCustomAI = new TextBox();
			RtbPrompt = new RichTextBox();
			RadioCustom = new RadioButton();
			SuspendLayout();
			// 
			// RadioPerplexity
			// 
			RadioPerplexity.AutoSize = true;
			RadioPerplexity.Checked = true;
			RadioPerplexity.Location = new System.Drawing.Point(71, 10);
			RadioPerplexity.Name = "RadioPerplexity";
			RadioPerplexity.Size = new System.Drawing.Size(76, 19);
			RadioPerplexity.TabIndex = 0;
			RadioPerplexity.TabStop = true;
			RadioPerplexity.Text = "Perplexity";
			RadioPerplexity.UseVisualStyleBackColor = true;
			RadioPerplexity.CheckedChanged += RadioPerplexity_CheckedChanged;
			// 
			// RadioChatGPT
			// 
			RadioChatGPT.AutoSize = true;
			RadioChatGPT.Location = new System.Drawing.Point(71, 26);
			RadioChatGPT.Name = "RadioChatGPT";
			RadioChatGPT.Size = new System.Drawing.Size(82, 19);
			RadioChatGPT.TabIndex = 1;
			RadioChatGPT.TabStop = true;
			RadioChatGPT.Text = "Chapt GPT";
			RadioChatGPT.UseVisualStyleBackColor = true;
			RadioChatGPT.CheckedChanged += RadioChatGPT_CheckedChanged;
			// 
			// RadioGemini
			// 
			RadioGemini.AutoSize = true;
			RadioGemini.Location = new System.Drawing.Point(71, 42);
			RadioGemini.Name = "RadioGemini";
			RadioGemini.Size = new System.Drawing.Size(63, 19);
			RadioGemini.TabIndex = 2;
			RadioGemini.TabStop = true;
			RadioGemini.Text = "Gemini";
			RadioGemini.UseVisualStyleBackColor = true;
			RadioGemini.CheckedChanged += RadioGemini_CheckedChanged;
			// 
			// RadioPoe
			// 
			RadioPoe.AutoSize = true;
			RadioPoe.Location = new System.Drawing.Point(71, 58);
			RadioPoe.Name = "RadioPoe";
			RadioPoe.Size = new System.Drawing.Size(45, 19);
			RadioPoe.TabIndex = 3;
			RadioPoe.TabStop = true;
			RadioPoe.Text = "Poe";
			RadioPoe.UseVisualStyleBackColor = true;
			RadioPoe.CheckedChanged += RadioPoe_CheckedChanged;
			// 
			// BtnKnown
			// 
			BtnKnown.Location = new System.Drawing.Point(22, 203);
			BtnKnown.Name = "BtnKnown";
			BtnKnown.Size = new System.Drawing.Size(79, 24);
			BtnKnown.TabIndex = 4;
			BtnKnown.Text = "Add Known";
			toolTip1.SetToolTip(BtnKnown, "Will add the solution to date. Edit as necessary");
			BtnKnown.UseVisualStyleBackColor = true;
			BtnKnown.Click += BtnKnown_Click;
			// 
			// BtnGoto
			// 
			BtnGoto.Location = new System.Drawing.Point(105, 203);
			BtnGoto.Name = "BtnGoto";
			BtnGoto.Size = new System.Drawing.Size(63, 24);
			BtnGoto.TabIndex = 5;
			BtnGoto.Text = "Goto AI";
			toolTip1.SetToolTip(BtnGoto, "Will go to the AI website. May need to register. \r\nOnce the Prompt shows CTRL-V to paste the text");
			BtnGoto.UseVisualStyleBackColor = true;
			BtnGoto.Click += BtnGoto_Click;
			// 
			// tnCancel
			// 
			tnCancel.Location = new System.Drawing.Point(173, 203);
			tnCancel.Name = "tnCancel";
			tnCancel.Size = new System.Drawing.Size(63, 24);
			tnCancel.TabIndex = 6;
			tnCancel.Text = "Cancel";
			tnCancel.UseVisualStyleBackColor = true;
			tnCancel.Click += tnCancel_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(63, 185);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(204, 15);
			label1.TabIndex = 8;
			label1.Text = "At the AI Prompt press Ctrl-V to paste";
			// 
			// label2
			// 
			label2.Location = new System.Drawing.Point(3, 99);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(55, 65);
			label2.TabIndex = 9;
			label2.Text = "Enter/ Edit Clue Prompt:";
			// 
			// label3
			// 
			label3.AutoEllipsis = true;
			label3.Location = new System.Drawing.Point(5, 12);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(55, 32);
			label3.TabIndex = 10;
			label3.Text = "Select LLM AI:";
			// 
			// TbCustomAI
			// 
			TbCustomAI.Location = new System.Drawing.Point(156, 70);
			TbCustomAI.Name = "TbCustomAI";
			TbCustomAI.Size = new System.Drawing.Size(121, 23);
			TbCustomAI.TabIndex = 12;
			toolTip1.SetToolTip(TbCustomAI, "Enter URL fr custom AI");
			TbCustomAI.Click += TbCustomAI_Click;
			// 
			// RtbPrompt
			// 
			RtbPrompt.BorderStyle = BorderStyle.FixedSingle;
			RtbPrompt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			RtbPrompt.ForeColor = System.Drawing.Color.Black;
			RtbPrompt.Location = new System.Drawing.Point(64, 95);
			RtbPrompt.Name = "RtbPrompt";
			RtbPrompt.Size = new System.Drawing.Size(213, 87);
			RtbPrompt.TabIndex = 13;
			RtbPrompt.Text = "Solve crossword clue which may be cryptic: ";
			toolTip1.SetToolTip(RtbPrompt, "Displays the Prompt. Can edit as necessary");
			// 
			// RadioCustom
			// 
			RadioCustom.AutoSize = true;
			RadioCustom.Location = new System.Drawing.Point(71, 75);
			RadioCustom.Name = "RadioCustom";
			RadioCustom.Size = new System.Drawing.Size(81, 19);
			RadioCustom.TabIndex = 11;
			RadioCustom.TabStop = true;
			RadioCustom.Text = "Custom AI";
			RadioCustom.UseVisualStyleBackColor = true;
			// 
			// GotoAI
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			AutoScaleMode = AutoScaleMode.Dpi;
			ClientSize = new System.Drawing.Size(289, 233);
			Controls.Add(RtbPrompt);
			Controls.Add(TbCustomAI);
			Controls.Add(RadioCustom);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(tnCancel);
			Controls.Add(BtnGoto);
			Controls.Add(BtnKnown);
			Controls.Add(RadioPoe);
			Controls.Add(RadioGemini);
			Controls.Add(RadioChatGPT);
			Controls.Add(RadioPerplexity);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "GotoAI";
			ShowInTaskbar = false;
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.Manual;
			Text = "Select AI";
			Load += GotoAI_Load;
			ResumeLayout(false);
			PerformLayout();

		}
		private void tnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private void BtnGoto_Click(object sender, EventArgs e)
		{
			string searchURL = "";
			if (RtbPrompt.Text == "")
			{
				SetFocusEnd(RtbPrompt);
				return;
			}
			if (RadioPerplexity.Checked == true)
			{
				searchURL = "https://www.perplexity.ai/";
			}
			else if (RadioGemini.Checked == true)
			{
				searchURL = "https://gemini.google.com/app";
			}
			else if (RadioPoe.Checked == true)
			{
				searchURL = "https://poe.com/";
			}
			else if (RadioChatGPT.Checked == true)
			{
				searchURL = "https://chatgpt.com/";
			}
			else if (RadioCustom.Checked == true)
			{
				searchURL = TbCustomAI.Text;
			}
			Clipboard.SetText(RtbPrompt.Text);
			if (searchURL == "")
			{
				MessageBox.Show("No URL for Custom AI", "Custom AI");
				SetFocusEnd(RtbPrompt);
				return;
			}
			Process.Start("chrome.exe", searchURL);
		}
		private void BtnKnown_Click(object sender, EventArgs e)
		{
			ClueExplorer clueExplorerForm = Application.OpenForms.OfType<ClueExplorer>().FirstOrDefault();
			string soln = clueExplorerForm.WordFromLetters("TbSoln");
			string solnLen = clueExplorerForm.labWordLen.Text;
			if (soln.Replace("_", "") == "" || soln.Replace("_", "").Replace("/", "") == "")
			{
				MessageBox.Show("Nothing known", "Solution");
				SetFocusEnd(RtbPrompt);
				return;
			}
			solnLen = solnLen.Replace("Words", "").Replace("(", "").Replace(")", "").Replace(" ", "");
			if (soln.IndexOf("/") < 0)
			{
				soln = "\r\n" + "\r\n" + "Letters known: '" + soln + "'. Underscore '_' means not known. Word length is " + solnLen;
			}
			else
			{
				soln = "\r\n" + "\r\n" + "Letters known: '" + soln + "'. Underscore '_' means not known and slash '/' is the words divider. Muliple words of length " + solnLen;
			}
			RtbPrompt.Text = RtbPrompt.Text + soln;
		}
		private void RadioPerplexity_CheckedChanged(object sender, EventArgs e)
		{
			SetFocusEnd(RtbPrompt);
		}
		private void RadioChatGPT_CheckedChanged(object sender, EventArgs e)
		{
			SetFocusEnd(RtbPrompt);
		}
		private void RadioGemini_CheckedChanged(object sender, EventArgs e)
		{
			SetFocusEnd(RtbPrompt);
		}
		private void RadioPoe_CheckedChanged(object sender, EventArgs e)
		{
			SetFocusEnd(RtbPrompt);
		}
		private void SetFocusEnd(RichTextBox rtb)
		{
			rtb.Focus();
			rtb.Select(rtb.Text.Length, 0);
			// tb.SelectionStart = tb.Text.Length;
			// tb.SelectionLength = 0;
		}
		private void TbCustomAI_Click(object sender, EventArgs e)
		{
			RadioCustom.Checked = true;
		}


	}
}
