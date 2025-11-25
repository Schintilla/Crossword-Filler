using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crossword_Filler
{
	public partial class CrosswordForm : Form
	{
		public CrosswordPuzzle puzzle;
		private string filePath;
		private int cwRows;
		private int cwCols;
		private string extractedText;
		private Form1 mainForm;
		private ImageDisplayForm imageForm;
		private ImportSelect importForm;
		private readonly JsonSerializerOptions jsonOptions;

		public CrosswordForm(Form1 form1)
		{
			InitializeComponent();
			mainForm = form1;
			jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				PropertyNameCaseInsensitive = true,
				Converters = { new CellOrIntListConverter(), new ClueConverter(), new CharArray2DConverter() }
			};
			RichTextBox rtb = hexViewerRichTextBox;
			rtb.Font = new System.Drawing.Font("Consolas", 8); // Use a fixed-width font
		}
		public CrosswordForm(Form1 form1, ImportSelect importSelect)
		{
			InitializeComponent();
			mainForm = form1;
			importForm = importSelect;
			jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				PropertyNameCaseInsensitive = true,
				Converters = { new CellOrIntListConverter(), new ClueConverter(), new CharArray2DConverter() }
			};
			RichTextBox rtb = hexViewerRichTextBox;
			rtb.Font = new System.Drawing.Font("Consolas", 8); // Use a fixed-width font
		}
		private void CrosswordForm_Load(object sender, EventArgs e)
		{
			CrosswordGridSetup(15, 15, DGVSolution1);
			CrosswordGridSetup(15, 15, DGVUser1);
			CrosswordGridSetup(15, 15, DGVSolution2);
			CrosswordGridSetup(15, 15, DGVUser2);
		}

		private void CrosswordGridSetup(int rowCnt, int colCnt, DataGridView dg)
		{
			dg.Columns.Clear();
			dg.AllowUserToAddRows = false;
			dg.AllowUserToDeleteRows = false;
			dg.RowHeadersVisible = false;
			dg.ColumnHeadersVisible = false;
			dg.AllowUserToResizeColumns = false;
			dg.AllowUserToResizeRows = false;
			dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			dg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
			dg.RowsDefaultCellStyle.BackColor = Color.White;
			dg.RowsDefaultCellStyle.ForeColor = Color.Black;
			//185,150
			int colWid = dg.Width / colCnt;
			int rowHt = dg.Height / rowCnt;
			for (int i = 0; i < colCnt; i++)
			{
				var newColumn = new DataGridViewTextBoxColumn
				{
					Width = colWid,
				};
				dg.Columns.Add(newColumn);
			}
			dg.RowCount = rowCnt;
			foreach (DataGridViewRow row in dg.Rows)
			{
				row.Height = rowHt;
			}
			for (int i = 0; i < rowCnt; i++)
			{
				for (int j = 0; j < colCnt; j++)
				{
					dg.Rows[i].Cells[j].Value = ""; // Start with empty cells
					dg.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center text
					dg.Rows[i].Cells[j].Style.Font = new System.Drawing.Font("Arial", 6, FontStyle.Regular); // Set font size and bold
				}
			}
			// dg.Size = new Size(colCnt * colWid + 3, rowCnt * rowHt + 3);
		}
		private void ClearDataGridBackColor(int rowCnt, int colCnt, DataGridView dg)
		{
			for (int i = 0; i < rowCnt; i++)
			{
				for (int j = 0; j < colCnt; j++)
				{
					dg.Rows[i].Cells[j].Style.BackColor = Color.White;
					dg.Rows[i].Cells[j].Value = "";
				}
			}
		}
		private void FillGridPUZ(int rowCnt, int colCnt, DataGridView dg, char[,] grid)
		{
			for (int r = 0; r < rowCnt; r++)
			{
				for (int c = 0; c < colCnt; c++)
				{
					dg.Rows[r].Cells[c].Style.BackColor = Color.White;
					if (grid[r, c] == '.')
					{
						dg.Rows[r].Cells[c].Style.BackColor = Color.Black;
					}
					else if (grid[r, c] == '-')
					{
						dg.Rows[r].Cells[c].Value = "";
					}
					else
					{
						dg.Rows[r].Cells[c].Value = grid[r, c];
					}
				}
			}
		}
		private void FillGridIPUZ(int rowCnt, int colCnt, DataGridView dg, List<List<string>> grid)
		{
			for (int r = 0; r < rowCnt; r++)
			{
				for (int c = 0; c < colCnt; c++)
				{
					dg.Rows[r].Cells[c].Style.BackColor = Color.White;
					if (grid[r][c] == "#")
					{
						dg.Rows[r].Cells[c].Style.BackColor = Color.Black;
					}
					else if (grid[r][c] == " ")
					{
						dg.Rows[r].Cells[c].Value = "";
					}
					else
					{
						dg.Rows[r].Cells[c].Value = grid[r][c];
					}
				}
			}
		}

		// Load Puzzle

		private async void btnLoad_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = "Crossword Puzzles (*.puz,*.ipuz)|*.puz;*.ipuz";
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				filePath = openFileDialog1.FileName;
				if (filePath.Contains("puz"))
				{
					await PuzLoad(filePath);

				}
				else
				{
					MessageBox.Show("Unsupported file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private async void btnLoad2_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = "Crossword Puzzles (*.puz,*.ipuz)|*.puz;*.ipuz";
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				filePath = openFileDialog1.FileName;
				if (filePath.Contains("puz"))
				{
					await PuzLoad2(filePath);
				}
				else
				{
					MessageBox.Show("Unsupported file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private async Task PuzLoad(string fn)
		{
			IPuzzleParser parser;
			if (fn.Contains(".puz"))
			{
				parser = new PuzFileParser();
			}
			else
			{
				parser = new IpuzFileParser();
			}
			puzzle = await parser.Parse(fn);
			txtJsonOutput.Text = JsonSerializer.Serialize(puzzle, jsonOptions);
			label1.Text = Path.GetFileName(fn);
			label1.ForeColor = Color.Red;
			label4.ForeColor = Color.Black;
			label12.Text = Path.GetFileName(fn);
			hexViewerRichTextBox.Text = "";
			if (fn.Contains(".puz"))
			{
				CrosswordGridSetup(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVSolution1);
				CrosswordGridSetup(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVUser1);
				FillGridPUZ(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVSolution1, puzzle.PuzSolutionGrid);
				FillGridPUZ(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVUser1, puzzle.PuzUserGrid);
				LblSolGrid1.Text = "PuzSolutionGrid";
				LblUserGrid1.Text = "PuzUserGrid";
				DisplayFileHexWithHighlight(fn, hexViewerRichTextBox);
				if (puzzle.Notes.Length > 0)
				{
					hexViewerRichTextBox.Text = puzzle.Notes;
				}

			}
			else
			{
				CrosswordGridSetup(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVSolution1);
				FillGridIPUZ(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVSolution1, puzzle.Solution);
				LblSolGrid1.Text = "Solution";
				CrosswordGridSetup(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVUser1);
				if (puzzle.UserAmswers == null)
				{
					ClearDataGridBackColor(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVUser1);
					LblUserGrid1.Text = "User - not defined";
				}
				else
				{
					FillGridIPUZ(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVUser1, puzzle.UserAmswers);
					LblUserGrid1.Text = "UserAnswers";
				}
			}
			rtbAcrossClues.Rtf = "";
			rtbDownClues.Rtf = "";
			extractedText = PuzzleClueExtract("ACROSS", puzzle.Clues.Across);
			// rtbAcrossClues.Text = extractedText;
			TextParser.OCRTextParseNew(extractedText, rtbAcrossClues, "Across");
			//OCRTextParseNew(extractedText, rtbAcrossClues, "Across");
			extractedText = PuzzleClueExtract("DOWN", puzzle.Clues.Down);
			TextParser.OCRTextParseNew(extractedText, rtbDownClues, "Down");
			//OCRTextParseNew(extractedText, rtbDownClues, "Down");
		}
		private async Task PuzLoad2(string fn)
		{
			IPuzzleParser parser;
			if (fn.Contains(".puz"))
			{
				parser = new PuzFileParser();
			}
			else
			{
				parser = new IpuzFileParser();
			}
			puzzle = await parser.Parse(fn);
			txtJsonOutput2.Text = JsonSerializer.Serialize(puzzle, jsonOptions);
			label4.Text = Path.GetFileName(fn);
			label4.ForeColor = Color.Red;
			label1.ForeColor = Color.Black;
			label12.Text = Path.GetFileName(fn);
			hexViewerRichTextBox2.Text = "";
			if (fn.Contains(".puz"))
			{
				CrosswordGridSetup(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVSolution2);
				CrosswordGridSetup(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVUser2);
				FillGridPUZ(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVSolution2, puzzle.PuzSolutionGrid);
				FillGridPUZ(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVUser2, puzzle.PuzUserGrid);
				LblSolGrid2.Text = "PuzSolutionGrid";
				LblUserGrid2.Text = "PuzUserGrid";
				DisplayFileHexWithHighlight(fn, hexViewerRichTextBox2);
				if (puzzle.Notes.Length > 0)
				{
					hexViewerRichTextBox2.Text = puzzle.Notes;
				}
			}
			else
			{
				CrosswordGridSetup(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVSolution2);
				FillGridIPUZ(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVSolution2, puzzle.Solution);

				LblSolGrid2.Text = "Solution";
				CrosswordGridSetup(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVUser2);
				if (puzzle.UserAmswers == null)
				{
					ClearDataGridBackColor(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVUser2);
					LblUserGrid2.Text = "User - not defined";
				}
				else
				{
					FillGridIPUZ(puzzle.Dimensions.Height, puzzle.Dimensions.Width, DGVUser2, puzzle.UserAmswers);
					LblUserGrid2.Text = "UserAnswers";
				}
			}

			rtbAcrossClues2.Rtf = "";
			rtbDownClues2.Rtf = "";
			extractedText = PuzzleClueExtract("ACROSS", puzzle.Clues.Across);
			TextParser.OCRTextParseNew(extractedText, rtbAcrossClues2, "Across");
			// OCRTextParseNew(extractedText, rtbAcrossClues2, "Across");
			extractedText = PuzzleClueExtract("DOWN", puzzle.Clues.Down);
			TextParser.OCRTextParseNew(extractedText, rtbDownClues2, "Down");
			//OCRTextParseNew(extractedText, rtbDownClues2, "Down");
		}

		private string PuzzleClueExtract(string title, List<Clue> clues)
		{
			var sb = new StringBuilder();
			foreach (var clue in clues)
			{
				sb.AppendLine($"{clue.Number} {clue.Text}\u200B");
				//need to check missing word length ( )
				// will contain \r\n and \u200B
			}
			return sb.ToString();
		}

		private void btnSavePuz_Click(object sender, EventArgs e)
		{
			if (puzzle == null)
			{
				MessageBox.Show("Please load or create a puzzle first.", "No Puzzle", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			string fileName = label12.Text;
			SaveIPUZ_PUZ(puzzle, fileName);
		}
		private async void SaveIPUZ_PUZ(CrosswordPuzzle puzzle, string filename)
		{
			if (puzzle == null)
			{
				MessageBox.Show("Please load or create a puzzle first.", "No Puzzle", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			saveFileDialog1.Filter = filePath.Contains(".ipuz") ? "ipuz files (*.ipuz)|*.ipuz" :
																"Across Lite Puzzles (*.puz)|*.puz";
			saveFileDialog1.FileName = filename;
			saveFileDialog1.InitialDirectory = mainForm.downloadsPath;
			saveFileDialog1.RestoreDirectory = true;
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if (filePath.Contains(".ipuz"))
				{
					var saver = new IpuzFileSaver();
					// Use the shared jsonOptions object
					await saver.Save(puzzle, saveFileDialog1.FileName, jsonOptions);
				}
				else
				{
					var saver = new PuzFileSaver();
					await saver.Save(puzzle, saveFileDialog1.FileName);
				}
				MessageBox.Show("Puzzle saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		// Display Hex

		private void DisplayFileHexWithHighlight(string filePath, RichTextBox rtb)
		{
			if (!File.Exists(filePath))
			{
				MessageBox.Show("File not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var hexOutput = new StringBuilder();
			byte[] buffer;
			try
			{
				using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
				using (BinaryReader reader = new BinaryReader(fileStream))
				{
					const int bytesToRead = 128;
					buffer = reader.ReadBytes(bytesToRead);

					for (int i = 0; i < buffer.Length; i++)
					{
						hexOutput.AppendFormat("{0:X2} ", buffer[i]);
						if ((i + 1) % 16 == 0)
						{
							hexOutput.AppendLine();
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Populate the RichTextBox
			// rtb.Text = hexOutput.ToString();
			// rtb.Font = new System.Drawing.Font("Consolas", 10); // Use a fixed-width font

			// Highlight the specified byte positions
			HighlightByteRange(0x2C, 0x2F, rtb); // Highlight bytes from 0x2C to 0x2F
			HighlightByteRange(0x36, 0x36, rtb); // Highlight byte at 0x36
		}
		private void HighlightByteRange(int startIndex, int endIndex, RichTextBox rtb)
		{
			for (int i = startIndex; i <= endIndex; i++)
			{
				// Each byte is represented by two hex characters plus one space.
				// A newline is added every 16 bytes.
				int row = i / 16;
				int col = i % 16;
				// Calculate the starting character index in the RichTextBox's text
				int startCharIndex = (row * (16 * 3 + Environment.NewLine.Length)) + (col * 3);
				int length = 2; // Each hex value is two characters long
								// Check to make sure the index is valid within the text length
				if (startCharIndex >= 0 && startCharIndex + length <= hexViewerRichTextBox.Text.Length)
				{
					// Select the text and apply the background color
					rtb.Select(startCharIndex, length);
					rtb.SelectionBackColor = Color.Yellow;
				}
			}
		}

		private void BtnShowClueHex_Click(object sender, EventArgs e)
		{
			HexAsciiDisplay.DisplayBytesAfterCluesWithAscii(filePath, hexViewerRichTextBox, rtbAcrossClues.Text.Substring(5, 5));
		}
		private void BtnParseHex_Click(object sender, EventArgs e)
		{

			string input = rtbDownClues.Text; // or any string you want to inspect

			StringHexAsciiDisplay.DisplayStringAsHexAscii(input, hexViewerRichTextBox);
		}
		private void BtnParseHexAcross_Click(object sender, EventArgs e)
		{
			string input = rtbAcrossClues.Text; // or any string you want to inspect
			StringHexAsciiDisplay.DisplayStringAsHexAscii(input, hexViewerRichTextBox);
		}

		private void BtnRawRTF_Click(object sender, EventArgs e)
		{
			rtbDownClues.Text = extractedText;
		}
		private void BtnRawRTF2_Click(object sender, EventArgs e)
		{
			rtbDownClues2.Text = extractedText;
		}

	}
}

