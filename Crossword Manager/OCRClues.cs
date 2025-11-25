using ExCSS;
using NHunspell;
using OpenAI.Chat;
using OpenCvSharp.Text;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;

namespace Crossword_Filler
{
	public partial class OCRClues : Form
	{
		private Form1 mainForm;
		private string extractedText;
		private ImageDisplayForm imageForm;
		private System.Drawing.Image capturedImage;
		public Bitmap capturedCluesImage;
		public Bitmap originalCluesImage;
		private System.Drawing.Image capturedImageAcross;
		private System.Drawing.Image capturedImageDown;
		private bool correctedAcross;
		private bool correctedDown;
		private bool newAcross;
		private bool newDown;
		private string AcrossDown;
		private string wrappedTextAcross;
		private string wrappedTextDown;
		private List<string> acrossNos;
		private List<string> downNos;
		private Hunspell hunspell;
		public Bitmap preprocessed;
		private Bitmap inputBmp;
		private string activeProcessingForm;
		private string lastProcessedA;
		private string lastProcessedD;
		private PictureBox PbProcessed;
		private Color rtbSelected;
		private float scaleFactor;
		// private int ScaleValue;
		private Dictionary<string, ProcessedImageData> processedImageData = new Dictionary<string, ProcessedImageData>();

		//private bool showingLineFeeds;

		public OCRClues(Form1 form1, ImageDisplayForm imageDisplayForm)
		{
			InitializeComponent();
			this.AutoScaleMode = AutoScaleMode.Dpi;
			SetupIPControls();
			LoadDictionaries();
			SetDpiAwareButtonIcons();
			mainForm = form1;
			imageForm = imageDisplayForm;
			TbRichTextAcrossClues.WordWrap = true;
			TbRichTextAcrossClues.Multiline = true;
			TbRichTextDownClues.WordWrap = true;
			TbRichTextDownClues.Multiline = true;
			PictureBoxAcross.SizeMode = PictureBoxSizeMode.Normal;
			PictureBoxDown.SizeMode = PictureBoxSizeMode.Normal;
			PictureBoxAcross.Paint += PictureBoxAcross_Paint;
			PictureBoxDown.Paint += PictureBoxDown_Paint;
		}
		public OCRClues()
		{
			InitializeComponent();
			SetupIPControls();
			LoadDictionaries();
			TbRichTextAcrossClues.WordWrap = true;
			TbRichTextAcrossClues.Multiline = true;
			TbRichTextDownClues.WordWrap = true;
			TbRichTextDownClues.Multiline = true;
			PictureBoxAcross.SizeMode = PictureBoxSizeMode.Normal;
			PictureBoxDown.SizeMode = PictureBoxSizeMode.Normal;
			PictureBoxAcross.Paint += PictureBoxAcross_Paint;
			PictureBoxDown.Paint += PictureBoxDown_Paint;
		}
		private void OCRClues_Load(object sender, EventArgs e)
		{
			ImageDisplayForm imageDisplayForm = System.Windows.Forms.Application.OpenForms.OfType<ImageDisplayForm>().FirstOrDefault();
			if (mainForm.clueNo == null || imageDisplayForm.newGridLoaded == false)
			{
				MessageBox.Show("Load & Scan grid first to obtain Clue Nos", "Clue Numbers");
				Close();
			}
			scaleFactor = this.DeviceDpi / 96f;
			// ScaleValue(int value) => (int)(value * scaleFactor);
			rtbSelected = System.Drawing.Color.LightYellow;
			activeProcessingForm = "0";
			lastProcessedA = "0";
			lastProcessedD = "0";
			ClueNosParsing();
		}
		private void ClueNosParsing()
		{
			acrossNos = new List<string>();
			downNos = new List<string>();
			for (int row = 0; row < Form1.rowCnt; row++)
			{
				for (int col = 0; col < Form1.colCnt; col++)
				{
					string cellData = mainForm.clueNo[row, col];
					if (!string.IsNullOrEmpty(cellData))
					{
						int firstSpaceIndex = cellData.IndexOf(' ');
						if (firstSpaceIndex > 0)
						{
							string numberStr = cellData.Substring(0, firstSpaceIndex);
							string stringData = cellData.Substring(firstSpaceIndex + 1);
							if (int.TryParse(numberStr, out int numberKey))
							{
								if (stringData.Contains("B"))
								{
									acrossNos.Add(numberKey.ToString());
									downNos.Add(numberKey.ToString());
								}
								else if (stringData.Contains("A"))
								{
									acrossNos.Add(numberKey.ToString());
								}
								else if (stringData.Contains("D"))
								{
									downNos.Add(numberKey.ToString());
								}
							}
						}
					}
				}
			}
		}
		private void SetupIPControls()
		{
			TrackBar tb = trackScale;
			tb.Minimum = 100;
			tb.Maximum = 300;
			tb.Value = 200;
			tb.TickFrequency = 25;
			tb.SmallChange = 10;
			tb.LargeChange = 25;
			tb.ValueChanged += Control_ValueChanged;
			lblScale.Text = "Scale: 2.00x";

			trackSharpen.Minimum = 0;
			trackSharpen.Maximum = 160;
			trackSharpen.Value = 80;
			trackSharpen.TickFrequency = 20;
			trackSharpen.ValueChanged += Control_ValueChanged;
			lblSharpen.Text = "Sharpen: 0.80";

			trackBlockSize.Minimum = 3;
			trackBlockSize.Maximum = 51;
			trackBlockSize.Value = 15;
			trackBlockSize.TickFrequency = 2;
			trackBlockSize.SmallChange = 2;
			trackBlockSize.LargeChange = 4;
			trackBlockSize.ValueChanged += Control_ValueChanged;
			lblBlockSize.Text = "BlockSize: 15";

			trackC.Minimum = 0;
			trackC.Maximum = 20;
			trackC.Value = 9;
			trackC.TickFrequency = 1;
			trackC.ValueChanged += Control_ValueChanged;
			lblC.Text = "Adaptive C: 9";

			StoreImageSettings("Default");
		}
		private void SetDpiAwareButtonIcons()
		{
			//BtnInfo1
			foreach (Button button in this.Controls.OfType<Button>())
			{
				string resourceName = LoadSVG.GetButtonResourceName(button.Name.Substring(0, button.Name.Length - 1));
				string svgString = LoadSVG.GetSvgStringFromResource(resourceName);
				if (svgString != null)
				{
					button.Image = null;
					int height = (int)(button.Height - (button.Height * 0.20));
					int width = (int)(button.Width - (button.Width * 0.20));
					button.Image = LoadSVG.GetSvgImage(svgString, width, height);
					button.ImageAlign = ContentAlignment.MiddleCenter;

				}
			}
		}

		// AI
		private async void AITextCorrect_Click(object sender, EventArgs e)
		{
			// Assuming you have a TextBox named 'inputTextBox' and a TextBox named 'outputTextBox'
			string input = "This is texttocorrect";

			if (string.IsNullOrWhiteSpace(input))
			{
				MessageBox.Show("Please enter text to correct.");
				return;
			}

			// Call the asynchronous correction method
			string correctedText = await CorrectTextAsync(input);

			// Display the result
			string txt = correctedText;
			MessageBox.Show(txt);
		}
		private async Task<string> CorrectTextAsync(string textToCorrect)
		{
			// ... (API Key retrieval code remains the same)
			string apiKey = Environment.GetEnvironmentVariable("OpenAIKey");
			if (string.IsNullOrEmpty(apiKey))
			{
				return "Error: API key not found. Check environment variables/user secrets.";
			}

			try
			{
				var chatClient = new ChatClient("gpt-4o-mini", apiKey);
				var messages = new List<ChatMessage>()
		{
			new SystemChatMessage("You are a helpful assistant that corrects grammar and spelling mistakes. Only return the corrected text, nothing else."),
			new UserChatMessage(textToCorrect)
		};

				ChatCompletion completion = await chatClient.CompleteChatAsync(messages);

				MessageBox.Show("test");

				// *** THE FINAL FIX IS HERE ***
				// Access the first choice's message content using the [0] indexer
				//if (completion.Choices != null && completion.Choices.Count > 0)
				//{
				//	// This line should now work correctly:
				//	return completion.Choices[0].Message.Content;
				//}
				//else
				//{
				return "Error: The API returned no choices in the response.";
				//}
			}
			catch (Exception ex)
			{
				return $"An error occurred: {ex.Message}";
			}
		}

		// Image Pre-processing
		private void BtnPreProcessing_Click(object sender, EventArgs e) //Original Form
		{
			string direction = TbRichTextAcrossClues.BackColor == rtbSelected ? "Across" : "Down";
			System.Drawing.Image img = TbRichTextAcrossClues.BackColor == rtbSelected ? capturedImageAcross : capturedImageDown;
			bool fmOpen = false;
			activeProcessingForm = "0";
			foreach (Form form in System.Windows.Forms.Application.OpenForms)
			{
				if (form.Text.Contains("Original") && form.Text.Contains(direction))
				{
					fmOpen = true;
					form.Select();
					break;
				}
			}
			if (fmOpen == false)
			{
				ProcessedImageForm(img, "Original", 0);
			}
			ReDoOCR("No pre-processing");
			StoreImageSettings(direction.Substring(0, 1) + activeProcessingForm);
		}
		private void BtnUpdateIP_Click(object sender, EventArgs e) //New Processing
		{
			if (capturedImageAcross == null && capturedImageDown == null) return;
			string direction = TbRichTextAcrossClues.BackColor == rtbSelected ? "Across" : "Down";
			int maxForms = 3;
			int fmNo = 0;
			for (int i = 1; i < maxForms + 1; i++)
			{
				string keyToFind = direction.Substring(0, 1) + i.ToString();
				if (processedImageData.ContainsKey(keyToFind) == false)
				{
					fmNo = i;
					break;
				}
			}
			activeProcessingForm = fmNo.ToString();
			if (fmNo != 0)
			{
				System.Drawing.Image img = TbRichTextAcrossClues.BackColor == rtbSelected ? capturedImageAcross : capturedImageDown;
				inputBmp = new Bitmap(img);
				UpdatePreviewNow();
				ProcessedImageForm(preprocessed, "Processed", fmNo);
				ReDoOCR("With pre-processing");
				StoreImageSettings(direction.Substring(0, 1) + activeProcessingForm);
			}
		}
		private void BtnIPUpdate_Click(object sender, EventArgs e) //Update Profile
		{
			Form pf = System.Windows.Forms.Application.OpenForms
								 .OfType<Form>()
								 .FirstOrDefault(f => f.Text.Last().ToString() == activeProcessingForm);
			if (pf != null)
			{
				string direction = TbRichTextAcrossClues.BackColor == rtbSelected ? "Across" : "Down";
				StoreImageSettings(direction.Substring(0, 1) + activeProcessingForm);
			}
		}
		private void ReviewIPOCRChanges() //Review OCR impact
		{
			Form pf = System.Windows.Forms.Application.OpenForms
					 .OfType<Form>()
					 .FirstOrDefault(f => f.Text.Last().ToString() == activeProcessingForm);
			if (pf != null)
			{
				if (activeProcessingForm != "0")
				{
					string direction = TbRichTextAcrossClues.BackColor == rtbSelected ? "Across" : "Down";
					UpdatePreviewNow();
					int width = preprocessed.Width;
					int height = preprocessed.Height;
					PictureBox pb = pf.Controls.OfType<PictureBox>().FirstOrDefault();
					pb.Image = preprocessed;
					pb.Size = new System.Drawing.Size(width, 400);
					pb.SizeMode = PictureBoxSizeMode.Normal;
					pf.Size = new System.Drawing.Size(pb.Width + 40, 250);
					pf.Text = $"{direction}: Processed - {width}x{height} #{activeProcessingForm}";
					ReDoOCR("Updated processing");
				}
				pf.Activate();
			}
		}
		private void ActivateIPForm()
		{
			Form pf = System.Windows.Forms.Application.OpenForms
					 .OfType<Form>()
					 .FirstOrDefault(f => f.Text.Last().ToString() == activeProcessingForm);
			if (pf != null)
			{
				pf.Activate();
			}
		}

		private void Control_ValueChanged(object sender, EventArgs e)
		{
			lblScale.Text = $"Scale: {(trackScale.Value / 100.0):0.00}x";
			lblSharpen.Text = $"Sharpen: {(trackSharpen.Value / 100.0):0.00}";
			// ensure blocksize odd
			int bs = trackBlockSize.Value;
			if ((bs & 1) == 0) bs++; // force odd
			lblBlockSize.Text = $"BlockSize: {bs}";
			lblC.Text = $"Adaptive C: {trackC.Value}";
			ReviewIPOCRChanges();
		}
		private void UpdatePreviewNow() // Call OpenCV Preprocessor
		{
			if (inputBmp == null)
			{
				//return;
			}
			double scale = trackScale.Value / 100.0;
			double sharpen = trackSharpen.Value / 100.0;
			int blockSize = trackBlockSize.Value;
			if ((blockSize & 1) == 0) blockSize++; // force odd
			double c = trackC.Value;
			bool useDenoise = chkDenoise.Checked;
			bool useMedian = chkMedian.Checked;
			bool useClahe = chkClahe.Checked;
			bool useAdaptiveThreshold = chkAdaptive.Checked;
			bool useKMeans = chkKMeans.Checked;
			int kMeansK = 4;
			// Run preprocessing (catch exceptions)
			preprocessed = null;
			try
			{
				preprocessed = OcrPreprocessing.PreprocessForOcr(
					inputBmp,
					scale: scale,
					useDenoise: chkDenoise.Checked,
					useMedian: chkMedian.Checked,
					useClahe: chkClahe.Checked,
					useAdaptiveThreshold: chkAdaptive.Checked,
					sharpen: (trackSharpen.Value > 0),
					sharpenAmount: (trackSharpen.Value / 100.0),
					useKMeans: chkKMeans.Checked,
					kMeansK: kMeansK,
					adaptiveBlockSize: blockSize,     // ensure blockSize is odd
					adaptiveC: trackC.Value
				);
				// Note: Our pipeline used boolean sharpen flag only; if you want to pass amount
				// you'd need to add a parameter to PreprocessForOcr for sharpen amount.
				// For now, we will re-run Unsharp with the desired amount after obtaining 'preprocessed' if it is BGR.
				if (preprocessed != null && sharpen > 0.0)
				{
					// If the current pipeline doesn't accept sharpen amount, do a light sharpening pass here.
					// Convert to Mat, apply UnsharpMask with amount = sharpen, and convert back.
					OpenCvSharp.Mat mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(preprocessed);
					OpenCvSharp.Mat sharpenedMat = OcrPreprocessing.UnsharpMask(mat, amount: sharpen, gaussianSize: 3);
					preprocessed.Dispose();
					preprocessed = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(sharpenedMat);
					mat.Dispose();
					sharpenedMat.Dispose();
				}
				// If our pipeline supports passing blockSize and c into adaptive threshold,
				// you'd add parameters — for now the helpers used defaults or control-level values inside PreprocessForOcr.
				//picProcessed.Image?.Dispose();
			}
			catch (Exception ex)
			{
				preprocessed?.Dispose();
				MessageBox.Show("Preprocessing failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private int ScalePixelValue(int value)
		{
			// LogicalToDeviceUnits requires a Size or Point object.
			return this.LogicalToDeviceUnits(new Size(value, 0)).Width;
		}
		private void ProcessedImageForm(System.Drawing.Image processedImage, string title, int fmNo)
		{
			if (processedImage == null) return;
			Size originalSize = new Size(processedImage.Width, processedImage.Height);
			Size scaledSize = this.LogicalToDeviceUnits(originalSize);
			int width = scaledSize.Width;
			int height = scaledSize.Height;
			RichTextBox rtb = TbRichTextAcrossClues.BackColor == rtbSelected ? TbRichTextAcrossClues : TbRichTextDownClues;
			string direction = TbRichTextAcrossClues.BackColor == rtbSelected ? "Across" : "Down";
			int x = this.Left + rtb.Left;
			x = direction == "Across" ? x + ScalePixelValue(190) : x - ScalePixelValue(350);
			int y = this.Top + rtb.Top + LoadSVG.ScaleDPIPixel(80);
			Form processedForm = new Form
			{
				FormBorderStyle = FormBorderStyle.FixedDialog,
				MinimizeBox = false,
				MaximizeBox = false,
				StartPosition = FormStartPosition.Manual,
			};
			int posDPIAware = ScalePixelValue(4);
			PbProcessed = new System.Windows.Forms.PictureBox
			{
				Location = new System.Drawing.Point(posDPIAware, posDPIAware),
				BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
				Size = new System.Drawing.Size(width, ScalePixelValue(400)),
				SizeMode = PictureBoxSizeMode.Normal,
				Image = processedImage
			};
			PbProcessed.Click += ProcessingForm_Click;
			processedForm.Controls.Add(PbProcessed);
			processedForm.AutoScaleMode = AutoScaleMode.Dpi;
			processedForm.Width = PbProcessed.Width + ScalePixelValue(40);
			processedForm.Height = ScalePixelValue(250);
			processedForm.Text = $"{direction}: {title} - {width}x{height}";
			processedForm.AutoScroll = true;
			processedForm.Click += ProcessingForm_Click;
			string key = "";
			string fmIndex = "0";
			if (title == "Original")
			{
				processedForm.Location = new Point(x - 10 * this.DeviceDpi / 96, y - 55 * this.DeviceDpi / 96);
				processedForm.Text = processedForm.Text + " #0";
				//processedForm.FormClosed += (s, args) => processedImageData.Remove(((Form)s).Text[0].ToString() + "0");
			}
			else
			{
				int xOffset = ScalePixelValue(40);
				int yOffset = ScalePixelValue(60);
				fmIndex = fmNo.ToString();
				processedForm.Location = new Point(x + xOffset * (fmNo - 1), y + yOffset * (fmNo - 1));
				processedForm.Text = processedForm.Text + " #" + fmIndex;
				processedForm.FormClosed += (s, args) =>
				{
					processedImageData.Remove(((Form)s).Text[0].ToString() + ((Form)s).Text.Last().ToString());
					activeProcessingForm = "0";
				};
			}
			key = direction.Substring(0, 1) + fmIndex;
			processedForm.Show(this);
			StoreImageSettings(key);
		}
		private void ProcessingForm_Click(object sender, EventArgs e)
		{
			Form clickedForm = null;
			if (sender is Form form)
			{
				clickedForm = form;
			}
			else if (sender is PictureBox pictureBox)
			{
				clickedForm = pictureBox.FindForm();
			}
			string direction = TbRichTextAcrossClues.BackColor == rtbSelected ? "A" : "D";
			//string fmNum = clickedForm.Text.Last().ToString();
			//if (fmNum == activeProcessingForm)
			//{
			//	string message = "Restore";
			//}
			if (clickedForm.Text.Contains("Original"))
			{
				activeProcessingForm = "0";
				SetImageSettings(direction + activeProcessingForm);
				ReDoOCR("No processing");
				StoreImageSettings(direction.Substring(0, 1) + activeProcessingForm);
			}
			else
			{
				activeProcessingForm = clickedForm.Text.Last().ToString();
				SetImageSettings(direction + activeProcessingForm);
				ReDoOCR("With processing");
				StoreImageSettings(direction.Substring(0, 1) + activeProcessingForm);
			}
			clickedForm.Activate();
		}

		//IP Settings I/O
		private void BtnReset_Click(object sender, EventArgs e) //Default
		{
			RadioPSMAuto.Checked = true;
			trackScale.Value = 200;
			trackSharpen.Value = 80;
			trackBlockSize.Value = 15;
			trackC.Value = 9;
			chkDenoise.Checked = false;
			chkMedian.Checked = false;
			chkClahe.Checked = false;
			chkAdaptive.Checked = false;
			chkKMeans.Checked = false;
		}
		private void BtnSetDefault_Click(object sender, EventArgs e)
		{
			StoreImageSettings("Default");
		}
		private void BtnLoadDefault_Click(object sender, EventArgs e)
		{
			SetImageSettings("Default");
		}
		private void StoreImageSettings(string key)
		{
			if (string.IsNullOrEmpty(key) == true || key.Length < 2)
			{
				return;
			}
			int chked = 3;
			if (RadioWinOCR.Checked)
			{
				chked = 0;
			}
			else if (RadioPSMBlock.Checked)
			{
				chked = 1;
			}
			else if (RadioPSMColumn.Checked)
			{
				chked = 2;
			}
			processedImageData[key] = new ProcessedImageData
			{
				OcrPMG = chked,
				Delimiter = CbBracket.Checked,
				Denoise = chkDenoise.Checked,
				Median = chkMedian.Checked,
				Clahe = chkClahe.Checked,
				Adaptive = chkAdaptive.Checked,
				KMeans = chkKMeans.Checked,
				Scale = trackScale.Value,
				Sharpness = trackSharpen.Value,
				BlockSize = trackBlockSize.Value,
				AdaptiveC = trackC.Value,
				Status = string.Join(";", LblSpellChkA.Text, LblMissingQtyA.Text, LblMissingWdLenA.Text, LblLFErrorA.Text, LabMissingA.Text)
			};
			ActivateIPForm();
		}
		private void SetImageSettings(string key)
		{
			if (processedImageData.ContainsKey(key) == false)
			{
				return;
			}
			ProcessedImageData ip = processedImageData[key];
			RadioPSMAuto.Checked = true;
			if (ip.OcrPMG == 0)
			{
				RadioWinOCR.Checked = true;
			}
			else if (ip.OcrPMG == 1)
			{
				RadioPSMBlock.Checked = true;
			}
			else if (ip.OcrPMG == 2)
			{
				RadioPSMColumn.Checked = true;
			}
			if (key.Contains("0") == false)
			{
				chkDenoise.Checked = ip.Denoise;
				chkMedian.Checked = ip.Median;
				chkClahe.Checked = ip.Clahe;
				chkAdaptive.Checked = ip.Adaptive;
				chkKMeans.Checked = ip.KMeans;
				trackScale.Value = ip.Scale;
				trackSharpen.Value = ip.Sharpness;
				trackBlockSize.Value = ip.BlockSize;
				trackC.Value = ip.AdaptiveC;
			}
			CbBracket.Checked = ip.Delimiter;
			string[] status = ip.Status.Split(';');
			LblSpellChkA.Text = status[0];
			LblMissingQtyA.Text = status[1];
			LblMissingWdLenA.Text = status[2];
			LblLFErrorA.Text = status[3];
			LabMissingA.Text = status[4];
			foreach (Label label in this.Controls.OfType<Label>())
			{
				if (label.Name.Last().ToString() == "A")
				{
					label.BackColor = label.Text == "0" ? Color.White : Color.LightPink;
				}
			}
			LabMissingA.BackColor = Color.White;
		}
		public class ProcessedImageData
		{
			public int OcrPMG { get; set; } = 3;
			public bool Delimiter { get; set; } = false;
			public bool Denoise { get; set; } = false;
			public bool Median { get; set; } = false;
			public bool Clahe { get; set; } = false;
			public bool Adaptive { get; set; } = false;
			public bool KMeans { get; set; } = false;
			public int Scale { get; set; } = 200;
			public int Sharpness { get; set; } = 80;
			public int BlockSize { get; set; } = 15;
			public int AdaptiveC { get; set; } = 9;
			public string Status { get; set; } = "0;0;0;0; ";
		}

		// OCR Status Check

		private void BtnCheckAllErrors_Click(object sender, EventArgs e)
		{
			RichTextBox rtb = TbRichTextAcrossClues.BackColor == rtbSelected ? TbRichTextAcrossClues : TbRichTextDownClues;
			if (!string.IsNullOrWhiteSpace(rtb.Text))
			{
				string direction = TbRichTextAcrossClues.BackColor == rtbSelected ? "Across" : "Down";
				if (BtnNumbers.Text == "Hide Nos")
				{
					HighlightNumbers(rtb);
				}
				if (BtnShowLF.Text == "Hide LF")
				{
					ToggleLFs(rtb);
				}
				ClueNosMissing(rtb.Text, direction);
				CheckLFs(rtb);
				CheckSpelling(rtb);
			}
		}
		private void LoadDictionaries()
		{
			string appPath = System.Windows.Forms.Application.StartupPath;
			string affFile = System.IO.Path.Combine(appPath, "Dictionaries\\index.aff");
			string dicFile = System.IO.Path.Combine(appPath, "Dictionaries\\index.dic");
			hunspell = new Hunspell(affFile, dicFile);
		}
		private void CheckSpelling(RichTextBox rtb)
		{
			if (string.IsNullOrWhiteSpace(rtb.Text)) return;
			int curSel = rtb.SelectionStart;
			rtb.SelectAll();
			rtb.SelectionColor = System.Drawing.Color.Black;
			int misspelt = 0;
			// Use a regex to match words, ignoring numbers and numbers in brackets
			//var words = System.Text.RegularExpressions.Regex.Matches(rtb.Text, @"[a-zA-Z]+");
			var words = System.Text.RegularExpressions.Regex.Matches(rtb.Text, @"(?<!\S)([a-zA-Z0-9]*[a-zA-Z][a-zA-Z0-9]*)+|[a-zA-Z]+");
			foreach (System.Text.RegularExpressions.Match match in words)
			{
				string word = match.Value;
				if (!hunspell.Spell(word))
				{
					rtb.SelectionStart = match.Index;
					rtb.SelectionLength = word.Length;
					rtb.SelectionColor = System.Drawing.Color.Red;
					misspelt++;
				}
			}
			rtb.Select(curSel, 0);
			//rtb.Focus();
			LblSpellChkA.Text = misspelt.ToString();
			LblSpellChkA.BackColor = LblSpellChkA.Text != "0" ? Color.LightPink : Color.White;
		}
		private void ClueNosMissing(string OCRText, string AcrossDown)
		{
			string[] clues;
			if (OCRText.Contains("\u200B") == false)
			{
				clues = OCRText.Split('\n');
			}
			else
			{
				clues = Regex.Split(OCRText, @"(\n\u200B)");
			}
			bool isBracket = true;
			string bracketMissing = "";
			List<string> cNos = AcrossDown == "Across" ? acrossNos : downNos;
			List<string> clueNosOCR = new List<string>();
			for (int i = 0; i < clues.Length; i++)
			{
				if (clues[i].Length > 10)
				{
					string lnStr = new string(clues[i].Trim().TakeWhile(char.IsDigit).ToArray());
					clueNosOCR.Add(lnStr);
					var regex = new Regex(@"\d+\)\s*$");
					if (!regex.IsMatch(clues[i]) == true)
					{
						isBracket = false;
						bracketMissing = bracketMissing + "," + lnStr;
					}
				}
			}
			bool hasMissing = cNos.Except(clueNosOCR).Any();
			if (hasMissing)
			{
				var missingItems = cNos.Except(clueNosOCR);
				LabMissingA.Text = "Missing Clues: " + string.Join(",", missingItems);
				LblMissingQtyA.Text = missingItems.Count().ToString();
				LblMissingWdLenA.Text = "0";
			}
			else
			{
				LblMissingQtyA.Text = "0";
				if (isBracket == true)
				{
					LabMissingA.Text = "Clues Nos and Brackets are correct";
					LblMissingWdLenA.Text = "0";
				}
				else
				{
					LabMissingA.Text = "Check Brackets: " +
						bracketMissing.Substring(1);
					LblMissingWdLenA.Text = (bracketMissing.Substring(1).Count(c => c == ',') + 1).ToString();
				}
			}
			LblMissingWdLenA.BackColor = LblMissingWdLenA.Text != "0" ? Color.LightPink : Color.White;
			LblMissingQtyA.BackColor = LblMissingQtyA.Text != "0" ? Color.LightPink : Color.White;

		}
		private void CheckLFs(RichTextBox rtb)
		{
			if (string.IsNullOrWhiteSpace(rtb.Text)) return;
			const string LfSymbol = "*";
			const string CrLf = "\n";
			int curSel = rtb.SelectionStart;
			bool corrected = rtb.BackColor == Color.LightYellow ? correctedAcross : correctedDown;
			rtb.Text = rtb.Text.Replace(CrLf, LfSymbol + CrLf);
			int startIndex = 0;
			int lineFeedErr = 0;
			while ((startIndex = rtb.Find(LfSymbol, startIndex, RichTextBoxFinds.None)) != -1)
			{
				rtb.SelectionStart = startIndex;
				char charBefore = rtb.Text.Take(startIndex)
									.Reverse()
									.SkipWhile(c => c == ' ')
									.FirstOrDefault();
				char charAfter = rtb.Text.Skip(startIndex + 2)
									.SkipWhile(c => c == ' ')
									.FirstOrDefault();
				if ((charBefore != ')' || Char.IsDigit(charAfter) == false) && corrected != true)
				{
					lineFeedErr++;
				}
				startIndex += 1;
			}
			LblLFErrorA.Text = lineFeedErr.ToString();
			LblLFErrorA.BackColor = LblLFErrorA.Text != "0" ? Color.LightPink : Color.White;
			rtb.Text = rtb.Text.Replace(LfSymbol, "");
			rtb.Select(curSel, 0);
		}
		private void ToggleLFs(RichTextBox rtb)
		{
			const string LfSymbol = "*";
			const string CrLf = "\n";
			int curSel = rtb.SelectionStart;
			bool corrected = rtb.BackColor == Color.LightYellow ? correctedAcross : correctedDown;
			if (BtnShowLF.Text == "Show LF")
			{
				rtb.Text = rtb.Text.Replace(CrLf, LfSymbol + CrLf);
				int startIndex = 0;
				int lineFeedErr = 0;
				while ((startIndex = rtb.Find("*", startIndex, RichTextBoxFinds.None)) != -1)
				{
					rtb.SelectionStart = startIndex;
					char charBefore = rtb.Text.Take(startIndex)
									  .Reverse()
									  .SkipWhile(c => c == ' ')
									  .FirstOrDefault();
					char charAfter = rtb.Text.Skip(startIndex + 2)
									 .SkipWhile(c => c == ' ')
									 .FirstOrDefault();
					rtb.SelectionLength = 1;
					if ((charBefore != ')' || Char.IsDigit(charAfter) == false) && corrected != true)
					{
						rtb.SelectionBackColor = System.Drawing.Color.Red;
						lineFeedErr++;
					}
					else
					{
						rtb.SelectionBackColor = System.Drawing.Color.LightGreen;
					}
					startIndex += 1;
				}
				LblLFErrorA.Text = lineFeedErr.ToString();
				BtnShowLF.Text = "Hide LF";
				BtnNumbers.Text = "Show Nos";
			}
			else
			{
				rtb.Text = rtb.Text.Replace(LfSymbol, "");
				BtnShowLF.Text = "Show LF";
				CheckSpelling(rtb);
			}
			LblLFErrorA.BackColor = LblLFErrorA.Text != "0" ? Color.LightPink : Color.White;
			rtb.Select(curSel, 0);
		}

		private void BtnShowLF_Click(object sender, EventArgs e)
		{
			RichTextBox rtb = TbRichTextAcrossClues.BackColor == rtbSelected ? TbRichTextAcrossClues : TbRichTextDownClues;
			if (!string.IsNullOrWhiteSpace(rtb.Text))
			{
				ToggleLFs(rtb);
			}
		}
		private void BtnRemoveLF_Click(object sender, EventArgs e)
		{
			RichTextBox rtb = TbRichTextAcrossClues.BackColor == rtbSelected ? TbRichTextAcrossClues : TbRichTextDownClues;
			if (BtnShowLF.Text == "Hide LF" && rtb.SelectedText.Length >= 0 && rtb.SelectedText.Contains("*"))
			{
				int selLen = rtb.SelectedText.Length;
				int selStart = rtb.SelectionStart;
				for (int i = 0; i < selLen; i++)
				{
					rtb.Select(selStart + i, 1);
					if (rtb.SelectionBackColor == Color.Red)
					{
						rtb.Select(selStart + i, 2);
						rtb.SelectedText = rtb.SelectedText.Replace("*\n", " ");
						break;
					}
				}
				BtnShowLF.PerformClick(); //Hide
				BtnShowLF.PerformClick(); //Show again
			}
		}
		private void BtnNumbers_Click(object sender, EventArgs e)
		{
			RichTextBox rtb = TbRichTextAcrossClues.BackColor == rtbSelected ? TbRichTextAcrossClues : TbRichTextDownClues;
			if (!string.IsNullOrWhiteSpace(rtb.Text))
			{
				if (BtnShowLF.Text == "Hide LF")
				{
					ToggleLFs(rtb);
				}
				HighlightNumbers(rtb);
			}
		}
		private void HighlightNumbers(RichTextBox rtb)
		{
			int curSel = rtb.SelectionStart;
			rtb.SelectAll();
			//rtb.SelectionColor = System.Drawing.Color.Black;
			rtb.SelectionBackColor = rtb.BackColor;
			if (BtnNumbers.Text == "Show Nos")
			{
				string pattern = @"(\(\s*\d+(\s*[,]\s*\d+)*\s*\)|\b\d+\b)";
				foreach (Match match in Regex.Matches(rtb.Text, pattern))
				{
					rtb.Select(match.Index, match.Length);
					//rtb.SelectionColor = System.Drawing.Color.Red;
					rtb.SelectionBackColor = System.Drawing.Color.Cyan;
				}
				rtb.SelectionColor = System.Drawing.Color.Black;
				BtnNumbers.Text = "Hide Nos";
			}
			else
			{
				BtnNumbers.Text = "Show Nos";
				CheckSpelling(rtb);
			}
			rtb.Select(curSel, 0);
		}

		// Add Word Lengths
		private void BtnWordLengths_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("Only if clues do not have word lengths. " +
				"Need to first ensure all clue numbers are clearly " +
				"identified. So just before it is ready for final format. " +
				"Continue?", "Add Word Lengths", MessageBoxButtons.YesNo);
			if (result == DialogResult.Yes)
			{
				RichTextBox rtb = TbRichTextAcrossClues.BackColor == rtbSelected ? TbRichTextAcrossClues : TbRichTextDownClues;
				string direction = TbRichTextAcrossClues.BackColor == rtbSelected ? "A" : "D";
				rtb.Text = FindMissingLengths(rtb.Text, direction);
			}
		}
		private string FindMissingLengths(string cluesList, string direction)
		{
			string[] cluesRebuild = new string[100];
			string[] listOfClues;
			listOfClues = Regex.Split(cluesList, @"(\n)");
			string wordlen = "";
			int j = 0;
			for (int i = 0; i < listOfClues.Length - 1; i += 2)
			{
				string clueNum = new string(listOfClues[i].SkipWhile(c => c == '\u200B')
								.TakeWhile(char.IsDigit)
								.ToArray());
				if (clueNum != "")
				{
					if (mainForm.clueData.ContainsKey(clueNum + direction))
					{
						wordlen = mainForm.clueData[clueNum + direction].WordLength.ToString();
						cluesRebuild[j] = listOfClues[i] + " (" + wordlen.Trim() + ")";
					}
					else
					{
						cluesRebuild[j] = listOfClues[i] + " (?)";
					}
					j++;
				}
			}
			cluesRebuild = cluesRebuild.Where(s => s != null).ToArray();
			cluesList = string.Join("\n", cluesRebuild);
			return cluesList;
		}

		// OCR Select
		private void BtnScreenAcross_Click(object sender, EventArgs e)
		{
			AcrossDown = "Across";
			Color colStart = TbRichTextAcrossClues.BackColor;
			TbRichTextAcrossClues.BackColor = rtbSelected;
			if (colStart != rtbSelected && TbRichTextDownClues.Text != "")
			{
				lastProcessedD = activeProcessingForm;
				StoreImageSettings("D" + activeProcessingForm);
			}
			OCRScreen(AcrossDown);
			if (colStart != rtbSelected)
			{
				SwitchDirection(TbRichTextDownClues);
			}
			newAcross = true;
			correctedAcross = false;
		}
		private void BtnScreenDown_Click(object sender, EventArgs e)
		{
			AcrossDown = "Down";
			Color colStart = TbRichTextDownClues.BackColor;
			TbRichTextDownClues.BackColor = rtbSelected;
			if (colStart != rtbSelected && TbRichTextAcrossClues.Text != "")
			{
				lastProcessedA = activeProcessingForm;
				StoreImageSettings("A" + activeProcessingForm);
			}
			OCRScreen(AcrossDown);
			if (colStart != rtbSelected)
			{
				SwitchDirection(TbRichTextAcrossClues);
				CheckSpelling(TbRichTextAcrossClues);
			}
			newDown = true;
			correctedDown = false;
		}
		private void TbRichTextClues_Click(object sender, EventArgs e)
		{
			AcrossDown = "Across";
			RichTextBox rtb = TbRichTextAcrossClues;
			if (rtb.BackColor != rtbSelected)
			{
				int curPos = rtb.SelectionStart;
				rtb.BackColor = rtbSelected;
				rtb.SelectAll();
				rtb.SelectionBackColor = rtbSelected;
				rtb.Select(curPos, 0);
				lastProcessedD = activeProcessingForm;
				if (rtb.Text != "")
				{
					StoreImageSettings("D" + activeProcessingForm);
				}
				SwitchDirection(TbRichTextDownClues);
				SetImageSettings("A" + activeProcessingForm);
			}
		}
		private void TbRichTextDownClues_Click(object sender, EventArgs e)
		{
			AcrossDown = "Down";
			RichTextBox rtb = TbRichTextDownClues;
			if (rtb.BackColor != rtbSelected)
			{
				int curPos = rtb.SelectionStart;
				rtb.BackColor = rtbSelected;
				rtb.SelectAll();
				rtb.SelectionBackColor = rtbSelected;
				rtb.Select(curPos, 0);
				lastProcessedA = activeProcessingForm;
				if (rtb.Text != "")
				{
					StoreImageSettings("A" + activeProcessingForm);
				}
				SwitchDirection(TbRichTextAcrossClues);
				SetImageSettings("D" + activeProcessingForm);
			}
		}
		private void SwitchDirection(RichTextBox rtb)
		{
			if (!string.IsNullOrEmpty(rtb.Text) && BtnShowLF.Text == "Hide LF")
			{
				ToggleLFs(rtb);
			}
			if (BtnNumbers.Text == "Hide Nos")
			{
				HighlightNumbers(rtb);
			}
			rtb.BackColor = System.Drawing.Color.White;
			rtb.SelectAll();
			rtb.SelectionBackColor = System.Drawing.Color.White;
			string directionOff = TbRichTextAcrossClues.BackColor == rtbSelected ? "Down" : "Across";
			string direction = directionOff == "Down" ? "Across" : "Down";
			activeProcessingForm = directionOff == "Across" ? lastProcessedD : lastProcessedA;
			foreach (Form form in System.Windows.Forms.Application.OpenForms)
			{
				if (form.Text.Contains("Down") || form.Text.Contains("Across"))
				{
					form.Visible = form.Text.Contains(directionOff) == true ? false : true;
				}
			}
			Application.OpenForms.OfType<Form>().FirstOrDefault(f => f.Text.Substring(f.Text.Length - 2) == "#" + activeProcessingForm)?.Activate();
			//Application.OpenForms.OfType<Form>().FirstOrDefault(f => activeProcessingForm == "0" && f.Text.Contains("Original"))?.Activate();
		}

		// OCR
		private async void OCRScreen(string AcrossDown)
		{
			{
				this.Opacity = 0.0;
				Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
				Bitmap screenCapture = new Bitmap(screenBounds.Width, screenBounds.Height);

				//Rectangle screenBounds = this.Bounds;
				//Bitmap screenCapture = new Bitmap(this.Width, this.Height);

				using (Graphics g = Graphics.FromImage(screenCapture))
				{
					g.CopyFromScreen(screenBounds.X, screenBounds.Y, 0, 0, screenBounds.Size, CopyPixelOperation.SourceCopy);
				}
				string extractedText = "";
				// Pass the screenshot to the snipping form.
				using (SnippingForm snipper = new SnippingForm(screenCapture))
				{
					if (snipper.ShowDialog() == DialogResult.OK)
					{
						capturedImage = snipper.CroppedImage;
						if (capturedImage != null)
						{
							// Explicitly convert the Image to a Bitmap.
							Bitmap croppedImage = new Bitmap(capturedImage);
							try
							{
								if (RadioWinOCR.Checked)
								{
									extractedText = await GetTextFromImageAsync(croppedImage);
								}
								else
								{
									extractedText = GetTextFromImage(croppedImage);
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show($"OCR failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							finally
							{
								capturedImageAcross = AcrossDown == "Across" ? capturedImage : capturedImageAcross;
								capturedImageDown = AcrossDown == "Down" ? capturedImage : capturedImageDown;
								croppedImage.Dispose();
							}
						}
					}
				}
				this.Opacity = 1.0;
				mainForm.Opacity = 1.0;
				imageForm.Opacity = 1.0;
				this.Location = new System.Drawing.Point(imageForm.Left - ScalePixelValue(350), imageForm.Top + ScalePixelValue(100));
				OCRTextParseNew(extractedText, AcrossDown);
				foreach (string key in processedImageData.Keys.Cast<string>().ToArray())
				{
					if (key.Contains(AcrossDown.Substring(0, 1)))
					{
						processedImageData.Remove(key);
					}
				}
				foreach (Form form in System.Windows.Forms.Application.OpenForms.Cast<Form>().ToArray())
				{
					if (form.Text.Contains(AcrossDown))
					{
						form.Close();
					}
				}
				activeProcessingForm = "0";
				StoreImageSettings(AcrossDown.Substring(0, 1) + activeProcessingForm);
			}
		}
		private string GetTextFromImage(Bitmap image) //Tesseract OCR Method
		{
			string tessDataPath = @".\tessdata";
			string language = "eng";
			string extractedText = string.Empty;
			using (var engine = new TesseractEngine(tessDataPath, language, EngineMode.Default))
			{
				if (RadioPSMAuto.Checked)
				{
					engine.DefaultPageSegMode = PageSegMode.Auto;
				}
				else if (RadioPSMBlock.Checked)
				{
					engine.DefaultPageSegMode = PageSegMode.SingleBlock;
				}
				else
				{
					engine.DefaultPageSegMode = PageSegMode.SingleColumn;
				}
				engine.SetVariable("tessedit_char_whitelist", @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+;#$?'"".,-() ");

				//MessageBox.Show("Using " + engine.DefaultPageSegMode);
				//	OsdOnly = 0,
				//	AutoOsd = 1,
				//	AutoOnly = 2,
				//	Auto = 3,
				//	SingleColumn = 4,
				//	SingleBlockVertText = 5,
				//	SingleBlock = 6,
				//	SingleLine = 7,
				//	SingleWord = 8,
				//	CircleWord = 9,
				//	SingleChar = 10,
				//	SparseText = 11,
				//	SparseTextOsd = 12,
				//	RawLine = 13
				using (var pix = PixConverter.ToPix(image))
				{
					using (var page = engine.Process(pix))
					{
						extractedText = page.GetText();
					}
				}
			}
			return extractedText;
		}
		private async Task<string> GetTextFromImageAsync(Bitmap image) // WinOCR Method
		{
			// 1. Convert the System.Drawing.Bitmap to a Windows.Graphics.Imaging.SoftwareBitmap
			SoftwareBitmap softwareBitmap = ConvertBitmapToSoftwareBitmap(image);

			// 2. Create the OcrEngine instance.
			// It automatically uses the system's current language settings and defaults.
			OcrEngine ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();

			if (ocrEngine != null)
			{
				// 3. Perform OCR recognition asynchronously
				OcrResult ocrResult = await ocrEngine.RecognizeAsync(softwareBitmap);

				// 4. Return the extracted text
				string whitelist = @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#;:%!""'.,-() ";
				string filteredText = new string(ocrResult.Text.Where(c => whitelist.Contains(c)).ToArray());
				filteredText = filteredText.Replace(") ", ")\n");
				filteredText = filteredText.Replace(")", ")\n");
				filteredText = filteredText.Replace("\r", "");
				return filteredText;
			}
			else
			{
				// Handle the case where the engine couldn't be created (e.g., missing language pack)
				throw new InvalidOperationException("Windows OCR engine could not be initialized. Check installed language packs.");
			}
		}
		private SoftwareBitmap ConvertBitmapToSoftwareBitmap(Bitmap bitmap)
		{
			// This conversion requires careful handling of formats.
			// The Windows API prefers specific pixel formats (like Bgra8).
			// This is a common pattern to convert a standard Bitmap to a SoftwareBitmap.

			// This helper uses System.IO.MemoryStream and Windows.Storage.Streams
			using (MemoryStream stream = new MemoryStream())
			{
				bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp); // Save to BMP format in memory
				stream.Position = 0;

				var ras = stream.AsRandomAccessStream();
				BitmapDecoder decoder = BitmapDecoder.CreateAsync(ras).GetAwaiter().GetResult();

				// Create a copy of the SoftwareBitmap in a format that the OcrEngine accepts (Bgra8, premultiplied alpha)
				return decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied).GetAwaiter().GetResult();
			}
		}
		private void OCRTextParseNew(string OCRText, string AcrossDown)
		{
			BtnNumbers.Text = "Show Nos";
			OCRText = OCRTextPrepare(OCRText);
			RichTextBox rtb = AcrossDown == "Across" ? TbRichTextAcrossClues : TbRichTextDownClues;
			PictureBox pb = AcrossDown == "Across" ? PictureBoxAcross : PictureBoxDown;
			pb.Invalidate();
			rtb.Text = OCRText;
			ClueNosMissing(rtb.Text, AcrossDown);
			CheckLFs(rtb);
			CheckSpelling(rtb);
		}
		public string OCRTextPrepare(string OCRText)
		{
			OCRText = OCRText.Replace("\r\n", "\r");
			OCRText = OCRText.Replace("\n\n", "\r");
			if (RadioWinOCR.Checked == false)
			{
				OCRText = OCRText.Replace("\n", " ");
				if (CbBracket.Checked)
				{
					OCRText = OCRText.Replace(")", ")\n");
					OCRText = OCRText.Replace("\r", "");
				}
				else
				{
					OCRText = OCRText.Replace("\r", "\n");
				}
			}
			OCRText = Regex.Replace(OCRText, " +", " ");
			//OCRText = Regex.Replace(OCRText, ",+", ",");
			//OCRText = Regex.Replace(OCRText, ".+", ".");
			return OCRText;
		}

		// OCR Options & ReDo OCR
		private void RadioPSMBlock_Click(object sender, EventArgs e)
		{
			if (newDown == false && newAcross == false) return;
			if (TbRichTextAcrossClues.BackColor == rtbSelected && newAcross == false) return;
			if (TbRichTextDownClues.BackColor == rtbSelected && newDown == false) return;
			ReDoOCR("Single Block");
			StoreImageSettings(AcrossDown.Substring(0, 1) + activeProcessingForm);
		}
		private void RadioPSMColumn_Click(object sender, EventArgs e)
		{
			if (newDown == false && newAcross == false) return;
			if (TbRichTextAcrossClues.BackColor == rtbSelected && newAcross == false) return;
			if (TbRichTextDownClues.BackColor == rtbSelected && newDown == false) return;
			ReDoOCR("Single Column");
			StoreImageSettings(AcrossDown.Substring(0, 1) + activeProcessingForm);
		}
		private void RadioPSMAuto_Click(object sender, EventArgs e)
		{
			if (newDown == false && newAcross == false) return;
			if (TbRichTextAcrossClues.BackColor == rtbSelected && newAcross == false) return;
			if (TbRichTextDownClues.BackColor == rtbSelected && newDown == false) return;
			ReDoOCR("Auto");
			StoreImageSettings(AcrossDown.Substring(0, 1) + activeProcessingForm);
		}
		private void RadioWinOCR_Click(object sender, EventArgs e)
		{
			//CbBracket.Checked = RadioWinOCR.Checked ? true : false;
			if (newDown == false && newAcross == false) return;
			if (TbRichTextAcrossClues.BackColor == rtbSelected && newAcross == false) return;
			if (TbRichTextDownClues.BackColor == rtbSelected && newDown == false) return;
			ReDoOCR("WinOCR");
			StoreImageSettings(AcrossDown.Substring(0, 1) + activeProcessingForm);
		}
		private void RadioWinOCR_CheckedChanged(object sender, EventArgs e)
		{
			CbBracket.Checked = RadioWinOCR.Checked;
		}
		private Form CreateWaitForm(string message)
		{
			int formWidth = ScalePixelValue(150);
			int formHeight = ScalePixelValue(70);
			int x = this.Location.X + (this.Width - formWidth) / 2;
			int y = this.Location.Y + (this.Height - formHeight) / 2;
			var waitForm = new Form
			{
				Size = new System.Drawing.Size(formWidth, formHeight),
				Text = message,
				MinimizeBox = false,
				MaximizeBox = false,
				FormBorderStyle = FormBorderStyle.FixedDialog,
				StartPosition = FormStartPosition.Manual,
				Location = new System.Drawing.Point(x, y),
				ControlBox = false // Prevents the user from closing the form
			};
			var waitLabel = new System.Windows.Forms.Label
			{
				Text = "Please wait...",
				AutoSize = true,
				Location = new System.Drawing.Point(ScalePixelValue(30), ScalePixelValue(10)) // Position the label
			};
			float baseFontSize = 8.25f; // Or whatever your base font size is
			waitLabel.Font = new Font(waitLabel.Font.FontFamily, baseFontSize * scaleFactor, waitLabel.Font.Style);
			waitForm.Controls.Add(waitLabel);
			return waitForm;
		}
		private async void ReDoOCR(string message)
		{
			if (capturedImage != null)
			{
				Color rtbBC = TbRichTextAcrossClues.BackColor;
				string direction = rtbBC == rtbSelected ? "Across" : "Down";
				bool formatted = rtbBC == rtbSelected ? correctedAcross : correctedDown;
				if (formatted == true)
				{
					DialogResult result = MessageBox.Show("Already formatted. Re-do OCR and Pre-Processing?", "Warning", MessageBoxButtons.YesNo);
					if (result == DialogResult.No)
					{
						Application.OpenForms.OfType<Form>().FirstOrDefault(f => f.Name.Contains(direction))?.Close();
						return;
					}
					else
					{
						correctedAcross = direction == "Across" ? false : correctedAcross;
						correctedDown = direction == "Down" ? false : correctedDown;
					}
				}
				BtnNumbers.Text = "Show Nos";
				RichTextBox rtb = rtbBC == rtbSelected ? TbRichTextAcrossClues : TbRichTextDownClues;
				System.Drawing.Image img = rtbBC == rtbSelected ? capturedImageAcross : capturedImageDown;
				Bitmap croppedImage = new Bitmap(img);
				message = "Original";
				foreach (Form form in System.Windows.Forms.Application.OpenForms)
				{
					if (form.Text.Contains("Processed") && form.Text.Contains(direction) &&
						form.Visible && form.Text.Last().ToString() == activeProcessingForm)
					{
						PictureBox pb = form.Controls.OfType<PictureBox>().FirstOrDefault();
						croppedImage = new Bitmap(pb.Image);
						message = "Processed #" + activeProcessingForm;
						break;
					}
				}
				var waitForm = CreateWaitForm(message);
				waitForm.Show(this);
				await Task.Delay(1);
				if (RadioWinOCR.Checked)
				{
					extractedText = await GetTextFromImageAsync(croppedImage);
				}
				else
				{
					extractedText = GetTextFromImage(croppedImage);
				}

				//string extractedText = GetTextFromImage(croppedImage);
				OCRTextParseNew(extractedText, direction);
				waitForm.Close();
			}
		}

		// Final Format and Add
		private void BtnFinalFormat_Click(object sender, EventArgs e) // final format button
		{
			if (TbRichTextAcrossClues.BackColor == rtbSelected)
			{
				if (LblSpellChkA.Text != "0" || LblMissingQtyA.Text != "0" || LblLFErrorA.Text != "0" || LblMissingWdLenA.Text != "0")
				{
					DialogResult result = MessageBox.Show("There are still corrections to be made. Continue?", "Error Status", MessageBoxButtons.YesNo);
					if (result == DialogResult.No)
					{
						return;
					}
				}
				FinalFormat(TbRichTextAcrossClues);
				correctedAcross = true;
				newAcross = false;
			}
			else
			{
				if (LblSpellChkA.Text != "0" || LblMissingQtyA.Text != "0" || LblLFErrorA.Text != "0" || LblMissingWdLenA.Text != "0")
				{
					DialogResult result = MessageBox.Show("There are still corrections to be made. Continue?", "Error Status", MessageBoxButtons.YesNo);
					if (result == DialogResult.No)
					{
						return;
					}
				}
				FinalFormat(TbRichTextDownClues);
				correctedDown = true;
				newDown = false;
			}
		}
		private void FinalFormat(RichTextBox rtb)
		{
			if (BtnNumbers.Text == "Hide Nos")
			{
				ToggleLFs(rtb);
			}
			string OCRText = rtb.Text;
			if (OCRText != "" && OCRText.Contains("\u200B") == false)
			{
				OCRText = OCRText.Replace("\n", "\r\n\u200B");
				System.Drawing.Font defaultFont = rtb.Font;
				int maxWidth = rtb.Width - LoadSVG.ScaleDPIPixel(20);
				// Font font = new Font("Arial", 10, System.Drawing.FontStyle.Regular);
				//int maxWidth = ScalePixelValue(195);
				rtb.Text = OCRWordWrapListSpacing(OCRText, maxWidth, defaultFont);
			}
			else
			{
				MessageBox.Show("No scan or already formatted");
			}
		}
		private string OCRWordWrapListSpacing(string OCRText, int maxWidth, Font font)
		{
			// OCRText = OCRTextPrepare(OCRText);
			Control dummyControl = new Control();
			int listIndent = 7;
			string indent = new string(' ', listIndent);
			StringBuilder wrappedText = new StringBuilder();
			var paragraphs = Regex.Split(OCRText, @"(\r\n\u200B)");
			for (int p = 0; p < paragraphs.Length; p++)
			{
				string paragraph = paragraphs[p].Trim();
				if (paragraph == "\r\n​" || paragraph == "\u200B" || string.IsNullOrEmpty(paragraph))
				{
					continue;
				}
				if (paragraph.Length < 10)
				{
					continue;
				}
				int spcPos = paragraph.IndexOf(" ");
				if (spcPos == -1 || spcPos > 4)
				{
					spcPos = 2;
				}
				string firstLineIndent = new string(' ', listIndent - spcPos - 2);
				indent = new string(' ', listIndent + spcPos - 1);
				paragraph = paragraph.Substring(0, spcPos) + firstLineIndent + paragraph.Substring(spcPos + 1);

				string[] words = paragraph.Split(' ');
				if (!words.Any()) continue;

				StringBuilder currentLine = new StringBuilder();
				currentLine.Append(words[0]);

				for (int i = 1; i < words.Length; i++)
				{
					string word = words[i];
					string testLine = currentLine.ToString() + " " + word;

					if (MeasureTextWidth(testLine, font) > maxWidth)
					{
						wrappedText.AppendLine(currentLine.ToString());
						currentLine.Clear();
						currentLine.Append(indent).Append(word);
					}
					else
					{
						currentLine.Append(" ").Append(word);
					}
				}
				// Append the last line of the paragraph.
				wrappedText.Append(currentLine.ToString());
				wrappedText.Append("\r\n\u200B");
			}
			return wrappedText.ToString();
		}
		private int MeasureTextWidth(string text, Font font)
		{
			// A dummy control is needed to create a graphics object.
			using (Control dummyControl = new Control())
			using (Graphics g = dummyControl.CreateGraphics())
			{
				return TextRenderer.MeasureText(g, text, font).Width;
			}
		}
		private void BtnPictureBox_Click(object sender, EventArgs e) // Adde 
		{
			BtnCopyToPicture();
		}
		private void BtnCopyToPicture() // Add to mainForm and close
		{
			if (correctedAcross == false || correctedDown == false)
			{
				return;
			}
			if (LblSpellChkA.Text != "0" || LblMissingQtyA.Text != "0" ||
				LblLFErrorA.Text != "0" || LblMissingWdLenA.Text != "0")
			{
				DialogResult result = MessageBox.Show("There are still corrections to be made. Continue?", "Error Status", MessageBoxButtons.YesNo);
				if (result == DialogResult.No)
				{
					return;
				}
			}
			mainForm.PictureBoxClues.Image = null;
			imageForm.ClearPaintedClues();
			mainForm.PictureBoxClues.Visible = false;
			mainForm.TbRichTextAcrossClues.Text = TbRichTextAcrossClues.Text;
			mainForm.TbRichTextDownClues.Text = TbRichTextDownClues.Text;
			mainForm.TbRichTextAcrossClues.Visible = true;
			mainForm.TbRichTextDownClues.Visible = true;
			imageForm.newCluesLoaded = true;
			this.Left = mainForm.Right - this.Width - 420;
			Screen currentScreen = Screen.FromControl(this);
			if (this.Left < currentScreen.WorkingArea.Left)
			{
				this.Left = currentScreen.WorkingArea.Left;
			}
			imageForm.Left = mainForm.Right - this.Width - 600 * this.DeviceDpi / 96;
			// this.Close();
		}

		// Raw Image Picturebox
		private void PictureBoxAcross_Paint(object sender, PaintEventArgs e)
		{
			if (capturedImageAcross == null) return;
			System.Drawing.Image img = capturedImageAcross;
			Rectangle clientRect = PictureBoxAcross.ClientRectangle;
			// Calculate scaled dimensions while maintaining aspect ratio (similar to Zoom)
			float imageRatio = (float)img.Width / img.Height;
			float containerRatio = (float)clientRect.Width / clientRect.Height;

			int drawWidth, drawHeight;
			if (imageRatio > containerRatio) // Image is wider relative to container
			{
				drawWidth = clientRect.Width;
				drawHeight = (int)(clientRect.Width / imageRatio);
			}
			else // Image is taller relative to container
			{
				drawHeight = clientRect.Height;
				drawWidth = (int)(clientRect.Height * imageRatio);
			}

			// Calculate position for top alignment
			int drawX = (clientRect.Width - drawWidth) / 2; // Center horizontally
			int drawY = 0; // Align to the top vertically

			// Draw the image
			e.Graphics.DrawImage(img, drawX, drawY, drawWidth, drawHeight);
			// capturedImage.Dispose();
		}
		private void PictureBoxDown_Paint(object sender, PaintEventArgs e)
		{
			if (capturedImageDown == null) return;
			System.Drawing.Image img = capturedImageDown;
			Rectangle clientRect = PictureBoxDown.ClientRectangle;
			// Calculate scaled dimensions while maintaining aspect ratio (similar to Zoom)
			float imageRatio = (float)img.Width / img.Height;
			float containerRatio = (float)clientRect.Width / clientRect.Height;

			int drawWidth, drawHeight;
			if (imageRatio > containerRatio) // Image is wider relative to container
			{
				drawWidth = clientRect.Width;
				drawHeight = (int)(clientRect.Width / imageRatio);
			}
			else // Image is taller relative to container
			{
				drawHeight = clientRect.Height;
				drawWidth = (int)(clientRect.Height * imageRatio);
			}

			// Calculate position for top alignment
			int drawX = (clientRect.Width - drawWidth) / 2; // Center horizontally
			int drawY = 0; // Align to the top vertically

			// Draw the image
			e.Graphics.DrawImage(img, drawX, drawY, drawWidth, drawHeight);
			// capturedImage.Dispose();
		}

		// Image Data
		private void BtnImageData_Click(object sender, EventArgs e)
		{
			Form infoForm = null;
			Image img = TbRichTextAcrossClues.BackColor == rtbSelected ? capturedImageAcross : capturedImageDown;
			infoForm = DisplayImageData(img);
			infoForm.Show(this);
		}
		private Form DisplayImageData(System.Drawing.Image snippedImage)
		{
			int width = snippedImage.Width;
			int height = snippedImage.Height;
			int horizontalRes = (int)snippedImage.HorizontalResolution;
			int verticalRes = (int)snippedImage.VerticalResolution;
			System.Drawing.Imaging.PixelFormat pixelFormat = snippedImage.PixelFormat;
			int formWidth = 200;
			int formHeight = 100;
			int x = this.Location.X + (this.Width - formWidth) / 2;
			int y = this.Location.Y + (this.Height - formHeight) / 2;
			string message = "Width = " + width + Environment.NewLine +
							"Height = " + height + Environment.NewLine +
							"DPI = " + horizontalRes + Environment.NewLine +
							"PixelFormat = " + pixelFormat.ToString().Replace("Format", "");
			var infoForm = new Form
			{
				Size = new System.Drawing.Size(formWidth, formHeight),
				Text = "Image Data",
				FormBorderStyle = FormBorderStyle.FixedDialog,
				MinimizeBox = false,
				MaximizeBox = false,
				StartPosition = FormStartPosition.Manual,
				Location = new System.Drawing.Point(x, y),
			};
			var waitLabel = new System.Windows.Forms.Label
			{
				Text = message,
				AutoSize = true,
				Location = new System.Drawing.Point(30, 10) // Position the label
			};
			infoForm.Controls.Add(waitLabel);
			infoForm.Height = waitLabel.Top + waitLabel.Height + 50;
			return infoForm;
		}

		// Info
		private void BtnInfo1_Click(object sender, EventArgs e)
		{
			string message = "Fully check OCR against the raw. Repeat as necessary. " +
							"Use OCR PSM options to reduce errors. Then try Pre-Processing. " +
							"Start with Scaling and Sharpening. " +
							"Once largely optimised proceed to manual correction. " +
							"Add Word lengths if scan has none. Only add after all the corrections. " +
							"When all is corect proceed to Format for final layout";
			InfoForms(message, "Overview");
		}
		private void BtnInfo2_Click(object sender, EventArgs e)
		{
			string message = "Word errors will show joined up words. Re-check after edits. " +
							"Clue errors will show Clue Numbers not detected by the OCR. " +
							"Manually edit. Add number, newline etc. It will then show Bracket Errors. " +
							"LF Error will show incorrect LF detection and may mean Bracket Errors. " +
							"Can remove LF with Show/Remove. Re-check and re-edit. " +
							"Click Format for the final layout.";
			InfoForms(message, "Manual editing");
		}
		private void BtnInfo4_Click(object sender, EventArgs e)
		{
			string message = "Start with PSM Options and also ' ( '  for poor paragraph detection. " +
						"Then try the Pre-processing options. Can have up to 3 separate profiles. " +
						"Can update each to fine-tune. Can switch between each. " +
						"Original will show no pre-processing. Can still use PSM Mode. " +
						"Once largely optimised proceed to manual correction. ";
			InfoForms(message, "OCR and Pre-processing");
		}
		private void BtnInfo5_Click(object sender, EventArgs e)
		{
			string message = "* Scale setable - always try first with x2." + Environment.NewLine +
			"* Sharpen settable. Higher creates halos. " + Environment.NewLine +
			"* CLAHE (clipLimit: 2.0, tileGridSize: 8) - " + Environment.NewLine +
			"* Denoise set to 8. Typically 5 to 15. Higher smooths more but blurs text. " + Environment.NewLine +
			"* Median set to 3. Typically 3 or 5. Smaller less bluring and for fine isolated noise. " + Environment.NewLine +
			"* LMeans set to 4. Typically 3 to 6. Reduced number of colours to k value. " + Environment.NewLine +
			"* Adaptive Filtering: BlockSize is pixel size for calculating the threshold." +
			"* Smaller means more sensitive. " + Environment.NewLine +
			"* Adaptive C subtracts from the adpative threshold. " +
			"* A positive C is higher threshold and more pixels black. Neg more pixels white";
			InfoForms(message, "Pre-processing Overview");
		}
		private void InfoForms(string message, string title)
		{
			int formWidth = LoadSVG.ScaleDPIPixel(200);
			int formHeight = LoadSVG.ScaleDPIPixel(220);
			int x = this.Location.X + (this.Width - formWidth) / 2;
			int y = this.Location.Y + (this.Height - formHeight) / 2;
			var infoForm = new Form
			{
				Size = new System.Drawing.Size(formWidth, formHeight),
				Text = title,
				FormBorderStyle = FormBorderStyle.FixedDialog,
				MinimizeBox = false,
				MaximizeBox = false,
				AutoScaleMode = AutoScaleMode.Dpi,
				StartPosition = FormStartPosition.Manual,
				Location = new System.Drawing.Point(x, y),
			};
			var waitLabel = new System.Windows.Forms.Label
			{
				Text = message,
				//AutoSize = false,
				//Height = 200,
				//Width = LoadSVG.ScaleDPIPixel(250),
				//Location = new System.Drawing.Point(LoadSVG.ScaleDPIPixel(8), LoadSVG.ScaleDPIPixel(8)) // Position the label

				Dock = DockStyle.Fill, // Label fills the entire form area
				Margin = new Padding(30), // Add some padding around the text
				TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
				AutoSize = true,
				MaximumSize = new System.Drawing.Size(250, 0),
			};
			// LoadSVG.LblHeightTextForDPI(waitLabel);
			infoForm.Controls.Add(waitLabel);

			infoForm.AutoSize = true;
			infoForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;

			//infoForm.Height = waitLabel.Top + waitLabel.Height + ScalePixelValue(40);
			//infoForm.Width = waitLabel.Width + ScalePixelValue(30);
			infoForm.Show(this);
		}

		// Clear and Close
		private void BtnClear_Click(object sender, EventArgs e)
		{
			TbRichTextAcrossClues.Clear();
			TbRichTextDownClues.Clear();
			if (capturedImageAcross != null)
			{
				capturedImageAcross.Dispose();
				capturedImageAcross = null;
			}
			if (capturedImageDown != null)
			{
				capturedImageDown.Dispose();
				capturedImageDown = null;
			}
			PictureBoxAcross.Invalidate();
			PictureBoxDown.Invalidate();
			newAcross = false;
			correctedAcross = false;
			newDown = false;
			correctedDown = false;
			activeProcessingForm = "0";
			lastProcessedA = "0";
			lastProcessedD = "0";
			BtnReset.PerformClick();
			foreach (Label label in this.Controls.OfType<Label>())
			{
				if (label.Name.Last().ToString() == "A")
				{
					label.BackColor = Color.White;
					label.Text = "0";
				}
			}
			LabMissingA.Text = "No clues scanned";
			processedImageData.Clear();
			//remove all forms
			foreach (Form form in System.Windows.Forms.Application.OpenForms.Cast<Form>().ToArray())
			{
				if (form.Text.Contains("Processed") || form.Text.Contains("Original"))
				{
					form.Close();
				}
			}

			//Application.OpenForms
			//			.OfType<Form>()
			//			.Where(form => form.Name.Contains("xx"))
			//			.ToList()
			//			.ForEach(form => form.Close());
			//Form form2 = this.OwnedForms.OfType<Form>().FirstOrDefault(f => f.Name == "Form2");
			//Form form = Application.OpenForms.OfType<Form>().FirstOrDefault(f => f.Name == "Form2");
			//Form form = Application.OpenForms.OfType<Form>().SingleOrDefault(f => f.Name == "Form2");
			//Form2 form = Application.OpenForms.OfType<Form2>().FirstOrDefault();
			//Form2 form2 = new Form2();
			//form2.Show(this); // form2 is now a child form of the current form (Form1)
			//foreach (Form form in this.OwnedForms)
			//{
			// Do something with the child form
			//}
			//Application.OpenForms.OfType<Form>().FirstOrDefault(f => f.Name.Contains(direction))?.Close();
			//Application.OpenForms
			//		.OfType<Form>()
			//		.Where(form => form.Name.Contains(directionOff))
			//		.ToList()
			//		.ForEach(form => form.Visible = false);
			//Application.OpenForms
			//		.OfType<Form>()
			//		.Where(form => form.Name.Contains(direction))
			//		.ToList()
			//		.ForEach(form => form.Visible = true);
			StoreImageSettings("Default");
		}
		private void BtnClose_Click(object sender, EventArgs e) // Cancel
		{
			if (correctedAcross != false || correctedDown != false)
			{
				DialogResult result = MessageBox.Show("Will lose the OCR to date. Continue?", "Cancel", MessageBoxButtons.YesNo);
				if (result == DialogResult.No)
				{
					return;
				}
			}
			this.Close();
		}
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			hunspell.Dispose();
			base.OnFormClosing(e);
		}

		private void label3_Click(object sender, EventArgs e)
		{

		}



		// Discontinued

	}
}
