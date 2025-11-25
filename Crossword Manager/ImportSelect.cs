using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Crossword_Filler.Form1;
using Image = System.Drawing.Image;

namespace Crossword_Filler
{
	public partial class ImportSelect : Form
	{
		public PictureBox MyPictureBoxClues => PictureBoxClues;
		private DataManager dataManager;
		private Form1 mainForm;
		//private CrosswordForm crosswordForm;
		public CrosswordPuzzle puzzle;
		private string filePathName;
		public bool mouseClk = false;
		public bool ticksDraw;
		private string filePathFromJSON;
		private string filePathToJSON;
		private string totCW;
		private int itemsAdded = 0;
		private bool blankCWNoData;
		private bool isSavedOrBlank;
		public enum BorderSide
		{
			None = 0,
			Bottom = 1,
			Right = 2
		}
		private static int colCnt = Form1.colCnt;
		private static int rowCnt = Form1.rowCnt;

		// Define Arrays

		// Holds the clue numbers for each cell
		private string[,] clueNo = new string[rowCnt, colCnt];
		// Dictionary list of values, border sides and cell fill for each cell.
		private Dictionary<Tuple<int, int>, List<BorderSide>> cellsWithBorders = new Dictionary<Tuple<int, int>, List<BorderSide>>();
		// JSON Export
		private List<CellDataRecord> items;
		// Define ticks, array, image and image scaling
		private Dictionary<string, List<Line>> _linesDictionary = new Dictionary<string, List<Line>>();
		private float _scaleX;
		private float _scaleY;

		public ImportSelect(Form1 form1, string jsonFn)
		{
			InitializeComponent();
			this.AutoScaleMode = AutoScaleMode.Dpi;
			mainForm = form1;
			totCW = mainForm.tbTotalCW.Text;
			filePathToJSON = jsonFn;
			dataManager = new DataManager();
			//this.crosswordForm = new CrosswordForm(mainForm, this);
			this.FormClosing += Form1_FormClosing;
			SetDpiAwareButtonIcons();

			//Wire/subscribe the trigger events += or -=
			DataGridView dg = dataGridView1;
			dg.EditingControlShowing += dataGridView1_EditingControlShowing;
			dg.CellMouseDown += dataGridView1_CellMouseDown;
			dg.CellPainting += DataGridView1_CellPainting;
			dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
			dataGridView1.KeyPress += dataGridView1_KeyPress;
			PictureBoxClues.Paint += PictureBoxClues_Paint;
			PictureBoxClues.BorderStyle = BorderStyle.None;
		}
		private void ImportSelect_Load(object sender, EventArgs e)
		{
			// CrosswordGridSetup();
		}

		public void CrosswordGridSetup(int rowCnt, int colCnt)
		{
			DataGridView dg = dataGridView1;
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
			int colWid = 36 * this.DeviceDpi / 96;
			int rowHt = 27 * this.DeviceDpi / 96;
			if (rowCnt < 15)
			{
				colWid = colWid + 5;
				rowHt = rowHt + 3;
			}
			if (rowCnt > 15)
			{
				colWid = colWid - 5;
				rowHt = rowHt - 3;
			}
			for (int i = 0; i < colCnt; i++)
			{
				// var newColumn = new DataGridViewRichTextColumn
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
					dg.Rows[i].Cells[j].Style.Font = new Font("Arial", 12, FontStyle.Bold); // Set font size and bold
				}
			}
			dg.Size = new Size(colCnt * colWid + 3, rowCnt * rowHt + 3);
			int startPos = referenceTextBox.Left + referenceTextBox.Width;
			if (rowCnt < 15)
			{
				dg.Left = (startPos + PictureBoxClues.Left - dg.Width) / 2;
			}
			else
			{
				dg.Left = startPos + 10 * this.DeviceDpi / 96;
			}
		}
		private void SetDpiAwareButtonIcons()
		{
			foreach (Button button in this.Controls.OfType<Button>())
			{
				string resourceName = LoadSVG.GetButtonResourceName(button.Name);
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


		private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (mouseClk == true)
			{
				mouseClk = false;
				dataGridView1.EndEdit();
			}
			else
			{
				e.Control.Enabled = false;
			}
		}
		private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			// Cancel the editing to prevent any control from showing
			e.Cancel = true;
		}
		private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			mouseClk = true;
		}
		private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}
		private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && !dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
			{
				var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
				var cellKey = new Tuple<int, int>(e.RowIndex, e.ColumnIndex);

				// Paint background: black cells stay black; otherwise selection highlight if selected, else white
				if (cell.Style.BackColor == Color.Black)
				{
					using (var brush = new SolidBrush(Color.Black))
						e.Graphics.FillRectangle(brush, e.CellBounds);
				}
				else if (cell.Selected)
				{
					using (var brush = new SolidBrush(Color.Yellow))
						e.Graphics.FillRectangle(brush, e.CellBounds);
				}
				else
				{
					using (var brush = new SolidBrush(Color.White))
						e.Graphics.FillRectangle(brush, e.CellBounds);
				}

				// Draw the cell value
				// Get the text to draw
				string text = e.FormattedValue.ToString();
				Font font1 = e.CellStyle.Font;

				// Measure the text size
				SizeF textSize = e.Graphics.MeasureString(text, font1);

				// Calculate the position to center the text
				float xx = e.CellBounds.X + (e.CellBounds.Width - textSize.Width) / 2;
				float yy = e.CellBounds.Y + (e.CellBounds.Height - textSize.Height) / 2;

				// Draw the text
				Brush textBrush;
				if (e.CellStyle.ForeColor == Color.LightGray)
				{
					textBrush = new SolidBrush(Color.LightGray);
				}
				else
				{
					textBrush = new SolidBrush(Color.Black);
				}
				// textBrush = new SolidBrush(Color.Black);
				using (textBrush)
				{
					e.Graphics.DrawString(text, font1, textBrush, xx, yy);
				}

				// Paint the clue number
				string clueNumber = clueNo[e.RowIndex, e.ColumnIndex]; // Implement this method
				if (!string.IsNullOrEmpty(clueNumber))
				{
					clueNumber = clueNumber.Substring(0, 2).Trim();
					using (Font font = new Font("Arial", 8, FontStyle.Bold))
					using (Brush brush = new SolidBrush(Color.Red))
					{
						var location = new Point(e.CellBounds.Left - 1, e.CellBounds.Top - 1);
						e.Graphics.DrawString(clueNumber, font, brush, location);
					}
				}

				// Paint all borders

				Color gridCol = dataGridView1.GridColor;
				if (tbCWNo.Text == "")
				{
					gridCol = Color.White;
				}
				using (Pen thinPen = new Pen(gridCol, 1))
				{
					int topAdj = (e.RowIndex == 0) ? 0 : 1;
					int leftAdj = (e.ColumnIndex == 0) ? 0 : 1;
					e.Graphics.DrawLine(thinPen, e.CellBounds.Left, e.CellBounds.Top - topAdj, e.CellBounds.Right, e.CellBounds.Top - topAdj);
					e.Graphics.DrawLine(thinPen, e.CellBounds.Left - leftAdj, e.CellBounds.Top, e.CellBounds.Left - leftAdj, e.CellBounds.Bottom);
					e.Graphics.DrawLine(thinPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
					e.Graphics.DrawLine(thinPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
				}

				// Paint thick borders

				if (cellsWithBorders.ContainsKey(cellKey))
				{
					int penWidth = 7;
					using (Pen thickPen = new Pen(Color.Gray, penWidth))
					{
						foreach (var side in cellsWithBorders[cellKey])
						{
							if (side == BorderSide.Bottom)
							{
								penWidth = 2;
								int x1 = e.CellBounds.Left;
								int y = e.CellBounds.Bottom - penWidth / 2 - 1;
								int x2 = e.CellBounds.Right - 1;
								e.Graphics.DrawLine(thickPen, x1, y, x2, y);
							}
							else if (side == BorderSide.Right)
							{
								penWidth = 7;
								int x = e.CellBounds.Right - penWidth / 2 - 1;
								int y1 = e.CellBounds.Top;
								int y2 = e.CellBounds.Bottom - 1;
								e.Graphics.DrawLine(thickPen, x, y1, x, y2);
							}
						}
					}
				}
				dataGridView1.ClearSelection();
				e.Handled = true;
				//dataGridView1.EndEdit();
			}
			else
			{
				// For cells in edit mode, let the default painting occur.
				e.Handled = false;
			}
		}

		// Button Click Events

		private async void BtnLoad_Click(object sender, EventArgs e)
		{
			ticksDraw = true;
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Crossword Files (cjz,puz,ipuz)|*.cjz;*.puz;*.ipuz|All Files (*.*)|*.*";
				openFileDialog.Title = "Select a Crossword file to load";
				openFileDialog.InitialDirectory = mainForm.downloadsPath;
				openFileDialog.RestoreDirectory = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						filePathFromJSON = openFileDialog.FileName;
						if (filePathToJSON == filePathFromJSON)
						{
							MessageBox.Show("Cannot import from currently loaded file");
							return;
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				else
				{
					MessageBox.Show("File selection cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
			}
			if (filePathFromJSON.Contains("puz"))
			{
				{
					IPuzzleParser parser;
					if (filePathFromJSON.Contains(".puz"))
					{
						parser = new PuzFileParser();
					}
					else
					{
						parser = new IpuzFileParser();
					}
					puzzle = await parser.Parse(filePathFromJSON);
				}
				PuzToJSON(filePathFromJSON);
				ChangeCrossword();
			}
			else
			{
				LoadData(filePathFromJSON);
				ChangeCrossword();
			}
			this.Text = "Import Options - " + Path.GetFileName(filePathFromJSON);
			NoCrossword.Visible = false;
			NoClues.Visible = false;
		}
		private void BtnNext_Click(object sender, EventArgs e)
		{
			if (tbCWNo.Text != "" && tbCWNo.Text != "0" && int.Parse(tbCWNo.Text) != int.Parse(tbTotalCW.Text))
			{

				dataManager.LoadNext();
				ChangeCrossword();
			}
		}
		private void BtnPrev_Click(object sender, EventArgs e)
		{
			if (tbCWNo.Text != "" && tbCWNo.Text != "1" && tbCWNo.Text != "0")
			{
				dataManager.LoadPrev();
				ChangeCrossword();
			}
		}
		private void BtnReplace_Click(object sender, EventArgs e)
		{
			ReplaceCurrentCW();
		}
		private void BtnImportCurrent_Click(object sender, EventArgs e)
		{
			MergeCurrentJSON();
		}
		private void BtnImportAll_Click(object sender, EventArgs e)
		{
			MergeJsonFiles();
		}
		private void CbSolns_Click(object sender, EventArgs e)
		{
			CbSolns.Checked = !CbSolns.Checked;
		}
		private void CbHints_Click(object sender, EventArgs e)
		{
			CbHints.Checked = !CbHints.Checked;
		}

		private void ChangeCrossword() // Load
		{
			cellsWithBorders.Clear();
			_linesDictionary.Clear();
			LoadCurrentRecord();
			AddClueNos(rowCnt, colCnt);
			if (TbRichTextAcrossClues.Visible == false)
			{
				LoadCluesNew(filePathFromJSON);
			}
			else if (filePathFromJSON.Contains("puz"))
			{
				AddMissingWordLength();
			}
		}

		// puz Clues list missing word length

		private void AddMissingWordLength()
		{
			var record = dataManager.GetCurrentRecord();
			int countEOL = record.CluesAcross.Count(c => c == ')');
			if (countEOL < 10)
			{
				Dictionary<int, string> dataByNumber = new Dictionary<int, string>();
				ClueNoToDictionary(clueNo, dataByNumber);
				record.CluesAcross = FindMissingLengths(record.CluesAcross, dataByNumber);
				TextParser.OCRTextParseNew(record.CluesAcross, TbRichTextAcrossClues, "Across");
				record.CluesDown = FindMissingLengths(record.CluesDown, dataByNumber);
				TextParser.OCRTextParseNew(record.CluesDown, TbRichTextDownClues, "Down");
			}
		}
		private string FindMissingLengths(string cluesList, Dictionary<int, string> dataByNumber)
		{
			string[] cluesRebuild = new string[100];
			string cluesRecord = cluesList.Replace("\u200B", "");
			string[] listOfClues;
			listOfClues = Regex.Split(cluesRecord, @"(\r\n)");
			string wordlen = "";
			int j = 0;
			for (int i = 0; i < listOfClues.Length - 1; i += 2)
			{
				string clueNum = new string(listOfClues[i].SkipWhile(c => c == '\u200B')
								.TakeWhile(char.IsDigit)
								.ToArray());
				if (clueNum != "")
				{
					int clueNo = int.Parse(clueNum);
					if (dataByNumber[clueNo].Contains(","))
					{
						var num = dataByNumber[clueNo].TakeWhile(c => c != ',');
						wordlen = new string(num.ToArray());
					}
					else
					{
						wordlen = dataByNumber[clueNo].ToString();
					}
					cluesRebuild[j] = listOfClues[i] + " (" + wordlen.Trim() + ")";
					j = j + 1;
				}
			}
			cluesRebuild = cluesRebuild.Where(s => s != null).ToArray();
			cluesList = string.Join("\r\n\u200B", cluesRebuild);
			return cluesList;
		}
		public void ClueNoToDictionary(string[,] clueNo, Dictionary<int, string> dataByNumber)
		{
			for (int row = 0; row < rowCnt; row++)
			{
				for (int col = 0; col < colCnt; col++)
				{
					string cellData = clueNo[row, col];
					if (!string.IsNullOrEmpty(cellData))
					{
						int firstSpaceIndex = cellData.IndexOf(' ');
						if (firstSpaceIndex > 0)
						{
							string numberStr = cellData.Substring(0, firstSpaceIndex);
							string stringData = cellData.Substring(firstSpaceIndex + 1);
							if (int.TryParse(numberStr, out int numberKey))
							{
								dataByNumber.Add(numberKey, stringData.Trim());
							}
						}
					}
				}
			}
		}

		//puz files

		public List<CellDataRecord> CreateBlankCellDataRecords(int count, string referencePrefix = "CW")
		{
			var result = new List<CellDataRecord>(count);
			for (int i = 0; i < count; i++)
			{
				var record = new CellDataRecord(rowCnt, colCnt)
				{
					Index = i + 1,
					Reference = string.Empty,
					GridSize = rowCnt,
					ScratchPad = string.Empty,
					Ticks = string.Empty,
					CluesAcross = string.Empty,
					CluesDown = string.Empty,
					HintsAcross = string.Empty,
					HintsDown = string.Empty
				};
				// CellData array is already created in the CellDataRecord constructor.
				// If you want explicit defaults for each cell, set them here:
				for (int r = 0; r < rowCnt; r++)
				{
					for (int c = 0; c < colCnt; c++)
					{
						// record.CellData[r, c] is already a new CellState(), but you can override:
						record.CellData[r, c].Value = "";
						record.CellData[r, c].WordSeparator = 0;
						record.CellData[r, c].Solution = "";
						record.CellData[r, c].Notes = "";
					}
				}
				result.Add(record);
			}
			return result;
		}
		private void PuzToJSON(string fn)
		{
			int cwCols = puzzle.Dimensions.Width;
			int cwRows = puzzle.Dimensions.Height;
			rowCnt = cwRows;
			colCnt = cwCols;
			List<CellDataRecord> newCW = CreateBlankCellDataRecords(1);
			newCW[0].Index = 1;
			newCW[0].Reference = puzzle.Title + ";" +
								puzzle.Version + ";" +
								puzzle.Author + ";" +
								puzzle.Copyright;
			newCW[0].GridSize = cwRows;
			for (int r = 0; r < cwRows; r++)
			{
				for (int c = 0; c < cwCols; c++)
				{
					if (fn.Contains(".ipuz"))
					{
						newCW[0].CellData[r, c].Solution = puzzle.Solution[r][c].ToString();
						if (puzzle.UserAmswers != null)
						{
							newCW[0].CellData[r, c].Value = puzzle.UserAmswers[r][c].ToString();
						}
						else
						{
							if (newCW[0].CellData[r, c].Solution == "#")
							{
								newCW[0].CellData[r, c].Value = "#";
							}
							else
							{
								newCW[0].CellData[r, c].Value = "";
							}
						}
					}
					else
					{
						string userSoln = puzzle.PuzUserGrid[r, c].ToString();
						if (userSoln == "-")
						{
							userSoln = "";
						}
						if (userSoln == ".")
						{
							userSoln = "#";
						}
						newCW[0].CellData[r, c].Value = userSoln;
						newCW[0].CellData[r, c].Solution = puzzle.PuzSolutionGrid[r, c].ToString();
					}
				}
			}
			if (puzzle.Clues.Across != null && puzzle.Clues.Across.Count != 0)
			{

				// AddClueNos missing lengths here? No Clues data?
				newCW[0].CluesAcross = PuzzleClueExtract(puzzle.Clues.Across);
				newCW[0].CluesDown = PuzzleClueExtract(puzzle.Clues.Down);
			}
			if (puzzle.Notes != null && puzzle.Notes != "" && puzzle.Notes.Length > 200)
			{
				string hints = puzzle.Notes.ToString();
				newCW[0].HintsAcross = hints.Substring(0, hints.IndexOf("Down"));
				newCW[0].HintsDown = hints.Substring(hints.IndexOf("Down"));
				CbHints.Checked = true;
			}
			else
			{
				newCW[0].HintsAcross = "";
				newCW[0].HintsDown = "";
				CbHints.Checked = false;
			}

			dataManager.Records = newCW;
			dataManager.CurrentIndex = 0;
		}
		private string PuzzleClueExtract(List<Clue> clues)
		{
			var sb = new StringBuilder();
			foreach (var clue in clues)
			{
				sb.AppendLine($"{clue.Number} {clue.Text}\u200B");
			}
			return sb.ToString();
		}

		// Close

		private void bClose_Click(object sender, EventArgs e)
		{

			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		// JSON Import

		private void ChkBlank()
		{
			if (mainForm.tbTotalCW.Text == "" || mainForm.tbTotalCW.Text == "0")
			{
				MessageBox.Show("Need to SaveAs before continuing");
				blankCWNoData = true;
				mainForm.SaveAsJSONZIP();
				if (mainForm.fileCWPathNameCurrent.Contains("blank") == false)
				{
					isSavedOrBlank = true;
					filePathToJSON = mainForm.fileCWPathName;
					mainForm.CreateBlankJsonFile(filePathToJSON);
					mainForm.NoCrossword.Visible = false;
				}
			}
			else
			{
				mainForm.NewLoadClose();
				if (mainForm.fileCWPathNameCurrent.Contains("blank") == false)
				{
					filePathToJSON = mainForm.fileCWPathName;
					isSavedOrBlank = true;
				}
			}
		}
		private void ReplaceCurrentCW()
		{
			if (tbCWNo.Text == "")
			{
				MessageBox.Show("No crossword loaded");
				return;
			}
			ChkBlank();
			if (isSavedOrBlank == false)
			{
				MessageBox.Show("Need to SaveAs the blank file first");
				return;
			}
			var items1 = LoadJsonFromFile(filePathToJSON); //to = destination or imported into
			List<CellDataRecord> items2 = new List<CellDataRecord>();
			if (filePathFromJSON.Contains("puz"))
			{
				items2 = dataManager.Records;
			}
			else
			{
				items2 = LoadJsonFromFile(filePathFromJSON);
			}
			int curItem = int.Parse(tbCWNo.Text) - 1;
			int destCurItem = int.Parse(mainForm.tbCWNo.Text);
			var itemsDict = items1.ToDictionary(item => item.Index);
			itemsDict[destCurItem] = items2[curItem];
			if (destCurItem == 0)
			{
				destCurItem = 1;
			}
			string fileToAdd = (curItem + 1).ToString() + " clues.png";
			string fileToRemove = destCurItem.ToString() + " clues.png";
			if (TbRichTextAcrossClues.Visible == false)
			{
				ReplaceZipEntry(filePathToJSON, filePathFromJSON, fileToRemove, fileToAdd); // png to move other to be deleted if no png in source it will just ignore
			}
			else if (TbRichTextAcrossClues.Visible == true && mainForm.TbRichTextAcrossClues.Visible == false)
			{
				mainForm.RemoveZipEntry(filePathToJSON, destCurItem.ToString());
			}
			List<CellDataRecord> mergedItems = itemsDict.Values.ToList();
			for (int i = 0; i < mergedItems.Count; i++)
			{
				mergedItems[i].Index = i + 1; // Reassign indices starting from 1
			}
			string mergedJson = JsonConvert.SerializeObject(mergedItems, Formatting.Indented);
			mainForm.UpdateJsonEntry(filePathToJSON, mergedJson);
			MessageBox.Show("Overwritten");
			if (blankCWNoData == true)
			{
				mainForm.tbCWNo.Text = "1";
			}
			blankCWNoData = false;
			mainForm.dataAdded = true;
			mainForm.LoadData(filePathToJSON);
			mainForm.ChangeCrossword();
		}
		private void MergeCurrentJSON()
		{
			if (tbCWNo.Text == "")
			{
				MessageBox.Show("No crossword loaded");
				return;
			}
			ChkBlank();
			if (isSavedOrBlank == false)
			{
				MessageBox.Show("Need to SaveAs the blank file first");
				return;
			}
			var items1 = LoadJsonFromFile(filePathToJSON);
			List<CellDataRecord> items2 = new List<CellDataRecord>();
			if (filePathFromJSON.Contains("puz"))
			{
				items2 = dataManager.Records;
			}
			else
			{
				items2 = LoadJsonFromFile(filePathFromJSON);
			}
			int curItem = int.Parse(tbCWNo.Text) - 1;
			var itemsDict = items1.ToDictionary(item => item.Index);
			totCW = mainForm.tbTotalCW.Text;
			if (totCW == "")
			{
				totCW = "0";
			}
			int nextRecord = 1 + int.Parse(totCW);
			itemsDict[nextRecord] = items2[curItem];
			string fn1;
			string fn2;

			if (TbRichTextAcrossClues.Visible == false)
			{
				fn1 = tbCWNo.Text + " clues.png";
				fn2 = nextRecord.ToString() + " clues.png";
				ReplaceZipEntry(filePathToJSON, filePathFromJSON, fn2, fn1);
			}

			List<CellDataRecord> mergedItems = itemsDict.Values.ToList();
			for (int i = 0; i < mergedItems.Count; i++)
			{
				mergedItems[i].Index = i + 1; // Reassign indices starting from 1
			}
			string mergedJson = JsonConvert.SerializeObject(mergedItems, Formatting.Indented);
			mainForm.UpdateJsonEntry(filePathToJSON, mergedJson);
			MessageBox.Show("Appended");
			mainForm.dataAdded = true;
			if (blankCWNoData == true)
			{
				mainForm.tbCWNo.Text = "1";
			}
			else
			{
				mainForm.tbCWNo.Text = nextRecord.ToString();
			}
			blankCWNoData = false;
			mainForm.LoadData(filePathToJSON);
			mainForm.ChangeCrossword();
			totCW = mainForm.tbTotalCW.Text;
		}
		private void MergeJsonFiles()
		{
			if (tbCWNo.Text == "")
			{
				MessageBox.Show("No crossword loaded");
				return;
			}
			if (tbCWNo.Text == "1")
			{
				MessageBox.Show("Only 1 crossword. Click Append Current");
				return;
			}
			ChkBlank();
			if (isSavedOrBlank == false)
			{
				MessageBox.Show("Need to SaveAs the blank file first");
				return;
			}
			var items1 = LoadJsonFromFile(filePathToJSON);
			var items2 = LoadJsonFromFile(filePathFromJSON);
			itemsAdded = 0;
			// Create a dictionary for quick access to existing indices
			totCW = mainForm.tbTotalCW.Text;
			int nextKey = int.Parse(totCW) + 1;
			var itemsDict = items1.ToDictionary(item => item.Index);
			string fn1;
			string fn2;
			foreach (var item in items2)
			{
				JumpToRecord(item.Reference);
				itemsDict[nextKey] = item;
				itemsAdded = itemsAdded + 1;
				int nextRecord = itemsAdded + int.Parse(totCW);
				if (TbRichTextAcrossClues.Visible == false)
				{
					fn1 = item.Index.ToString() + " clues.png";
					fn2 = nextRecord.ToString() + " clues.png";
					ReplaceZipEntry(filePathToJSON, filePathFromJSON, fn2, fn1);
				}
				nextKey++;
			}
			// Now create a new list from the dictionary
			if (itemsAdded != 0)
			{
				List<CellDataRecord> mergedItems = itemsDict.Values.ToList();
				for (int i = 0; i < mergedItems.Count; i++)
				{
					mergedItems[i].Index = i + 1; // Reassign indices starting from 1
				}
				string mergedJson = JsonConvert.SerializeObject(mergedItems, Formatting.Indented);
				mainForm.UpdateJsonEntry(filePathToJSON, mergedJson);
				MessageBox.Show("Appended " + itemsAdded.ToString());
				if (blankCWNoData == true)
				{
					mainForm.tbCWNo.Text = "1";
				}
				else
				{
					mainForm.tbCWNo.Text = (int.Parse(mainForm.tbCWNo.Text) + itemsAdded).ToString();
				}
				mainForm.dataAdded = true;
				mainForm.LoadData(filePathToJSON);
				mainForm.ChangeCrossword();
				totCW = mainForm.tbTotalCW.Text;
			}
			else
			{
				MessageBox.Show("No records affected");
			}
			MessageBox.Show(itemsAdded.ToString() + " Appended");

		}
		private List<CellDataRecord> LoadJsonFromFile(string filePathName)
		{
			string json = ReadJsonFromZip(filePathName);
			return JsonConvert.DeserializeObject<List<CellDataRecord>>(json);
		}
		private void JumpToRecord(string refText)
		{
			var record = dataManager.Records.FirstOrDefault(r => r.Reference == refText);
			if (record != null)
			{
				dataManager.CurrentIndex = dataManager.Records.IndexOf(record);
				ChangeCrossword();
			}
		}

		// Clues Zip update

		private void ReplaceZipEntry(string zipFilePath, string sourceZip, string fileToRemove, string fileToAdd)
		{
			// fileToRemove or fn2 is the png in the main crossword
			// if fileToAdd or fn1 is "" then just delete fileToRemove
			string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Directory.CreateDirectory(tempDir);
			string fileNoRemove = fileToRemove.Substring(0, fileToRemove.IndexOf(" "));
			string tempExtractedFilePath = Path.Combine(tempDir, fileToAdd); //unless ""
			using (ZipArchive zip1 = ZipFile.Open(sourceZip, ZipArchiveMode.Read))
			{
				var entry = zip1.GetEntry(fileToAdd);
				if (entry != null)
				{
					entry.ExtractToFile(tempExtractedFilePath, true); // Overwrite if it exists
				}
			}
			string newFileName = fileNoRemove + fileToAdd.Substring(fileToAdd.IndexOf(" "));
			tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Directory.CreateDirectory(tempDir);
			string newTempFilePath = Path.Combine(tempDir, newFileName);
			File.Copy(tempExtractedFilePath, newTempFilePath, true); // Overwrite if it exists
			using (ZipArchive zip2 = ZipFile.Open(zipFilePath, ZipArchiveMode.Update))
			{
				var entryToDelete = zip2.Entries.FirstOrDefault(e => e.FullName.StartsWith(fileNoRemove));
				if (entryToDelete != null)
				{
					entryToDelete.Delete();
				}
				zip2.CreateEntryFromFile(newTempFilePath, newFileName, CompressionLevel.Optimal);
			}
			if (Directory.Exists(newTempFilePath))
			{
				Directory.Delete(newTempFilePath, true);
			}
			if (Directory.Exists(tempExtractedFilePath))
			{
				Directory.Delete(tempExtractedFilePath, true);
			}
		}

		// Copied from Form1

		public void AddClueNos(int rowCnt, int colCnt) // To use main need to change dgv
		{
			clueNo = new string[rowCnt, colCnt];
			// buttonRemoveLabels()
			int num = 1;
			string sNum = "";
			int clueCount = 0;
			DataGridViewCell startpos = null;
			int solved = 0;
			int k = 0;
			int acrossLength = 0;
			int downLength = 0;
			for (int j = 0; j < rowCnt; j++)
			{
				for (int i = 0; i < colCnt; i++)
				{
					DataGridViewCell cel = dataGridView1.Rows[j].Cells[i];
					if (dataGridView1.Rows[j].Cells[i].Style.BackColor != Color.Black)
					{
						if ((i == 0 || (i > 0 && dataGridView1.Rows[j].Cells[i - 1].Style.BackColor == Color.Black)) && i != (rowCnt - 1) && (i < (rowCnt - 1) && dataGridView1.Rows[j].Cells[i + 1].Style.BackColor != Color.Black))
						{
							sNum = "A";
							bool isSolved = true;
							k = 0;
							while (i + k < colCnt && dataGridView1.Rows[j].Cells[i + k].Style.BackColor != Color.Black)
							{
								object chk = dataGridView1.Rows[j].Cells[i + k].Value;
								if (chk == null || string.IsNullOrEmpty(chk.ToString()))
								{
									isSolved = false;
								}
								k++;
							}
							acrossLength = k;
							if (isSolved == true)
							{
								solved = solved + 1;
							}
						}
						if ((j == 0 || (j > 0 && dataGridView1.Rows[j - 1].Cells[i].Style.BackColor == Color.Black)) && j != (colCnt - 1) && (j < (colCnt - 1) && dataGridView1.Rows[j + 1].Cells[i].Style.BackColor != Color.Black))
						{
							sNum = sNum + "D";
							bool isSolved = true;
							k = 0;
							while (j + k < rowCnt && dataGridView1.Rows[j + k].Cells[i].Style.BackColor != Color.Black)
							{
								object chk = dataGridView1.Rows[j + k].Cells[i].Value;
								if (chk == null || string.IsNullOrEmpty(chk.ToString()))
								{
									isSolved = false;
								}
								k++;
							}
							downLength = k;
							if (isSolved == true)
							{
								solved = solved + 1;
							}
						}
						if (sNum == "AD")
						{
							sNum = "B";
							clueCount = clueCount + 1;
						}
						if (sNum != "")
						{
							if (sNum == "A")
							{
								sNum = num.ToString() + "  " + acrossLength.ToString();
							}
							else if (sNum == "D")
							{
								sNum = num.ToString() + "  " + downLength.ToString();
							}
							else
							{
								sNum = num.ToString() + "  " + acrossLength.ToString() + "," + downLength.ToString();
							}
							clueNo[j, i] = sNum;
							num = num + 1;
							clueCount = clueCount + 1;
							sNum = "";
						}
						if (num == 2 && startpos == null)
						{
							startpos = cel;
						}
					}
				}
			}
			dataGridView1.ClearSelection();
			// dataGridView1.CurrentCell = startpos;
			this.tbSolnStatus.Text = solved.ToString() + "/" + clueCount.ToString();
			if (solved == clueCount)
			{
				tbSolnStatus.BackColor = Color.LightGreen;
			}
			else
			{
				tbSolnStatus.BackColor = Color.White;
			}
			//dataGridView1.Invalidate();
		}
		private class Line
		{
			private Point _midPoint;
			private int _length = 15;
			private int _thickness = 3;
			private Color _color = Color.Red;
			private string _cwNo;  // CWNo identifier

			public Line(Point midPoint, string cwNo)
			{
				_midPoint = midPoint;
				_cwNo = cwNo;
			}

			// Public properties for accessing private fields
			public Point MidPoint => _midPoint;
			public string CWNo => _cwNo;
			// Getter for CWNo

			public void Draw(Graphics g, float scaleX, float scaleY)
			{
				using (var pen = new Pen(_color, _thickness))
				{
					// Calculate the start and end points based on the midpoint
					var start = new Point((int)((MidPoint.X - _length / 2) * scaleX), (int)((MidPoint.Y + _length / 2) * scaleY));
					var end = new Point((int)((MidPoint.X + _length / 2) * scaleX), (int)((MidPoint.Y - _length / 2) * scaleY));
					g.DrawLine(pen, start, end);
				}
			}

			public bool Contains(Point p)
			{
				// Calculate the start and end points based on the midpoint
				var start = new Point(MidPoint.X - _length / 2, MidPoint.Y + _length / 2);
				var end = new Point(MidPoint.X + _length / 2, MidPoint.Y - _length / 2);

				// Create a rectangle that approximates the line area
				var lineRectangle = new Rectangle(
					Math.Min(start.X, end.X) - _thickness,
					Math.Min(start.Y, end.Y) - _thickness,
					Math.Abs(start.X - end.X) + _thickness * 2,
					Math.Abs(start.Y - end.Y) + _thickness * 2
				);

				// Check if the point is within the bounding rectangle
				return lineRectangle.Contains(p);
			}
		}
		private void PictureBoxClues_Paint(object sender, PaintEventArgs e)
		{
			// Draw the original image stretched to fit the PictureBox
			if (PictureBoxClues.Image != null)
			{
				e.Graphics.DrawImage(PictureBoxClues.Image, 0, 0, PictureBoxClues.ClientSize.Width, PictureBoxClues.ClientSize.Height);
			}
			ticksDraw = false; //no ticks in Import Mode
			if (ticksDraw == true)
			{
				foreach (var kvp in _linesDictionary)
				{
					foreach (var line in kvp.Value)
					{
						line.Draw(e.Graphics, _scaleX, _scaleY);
					}
				}
			}
		}
		private void LoadCluesNew(string fn)
		{
			string fileName = tbCWNo.Text + " clues.png";
			string zipPath = fn;
			try
			{
				using (ZipArchive archive = ZipFile.OpenRead(zipPath))
				{
					ZipArchiveEntry entry = archive.GetEntry(fileName);
					if (entry != null)
					{
						using (Stream stream = entry.Open())
						{
							using (Image image = Image.FromStream(stream))
							{
								// _scaleX = (float)PictureBoxClues.ClientSize.Width / image.Width;
								// _scaleY = (float)PictureBoxClues.ClientSize.Height / image.Height;
								// Set the PictureBox image
								PictureBoxClues.Image = new Bitmap(image); // Create a new Bitmap to display in PictureBox
							}
						}
					}
				}
				PictureBoxClues.Invalidate();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		public void LoadCurrentRecord()
		{
			var record = dataManager.GetCurrentRecord();
			if (record != null)
			{
				rowCnt = record.GridSize;
				colCnt = record.GridSize;
				CrosswordGridSetup(rowCnt, colCnt);
				tbCWNo.Text = record.Index.ToString();
				string CWNo = tbCWNo.Text.Trim();
				string[] info = record.Reference.ToString().Split(';');
				string[] paddedData = info.Take(4)
					 .Concat(Enumerable.Repeat(string.Empty, 4))
					 .Take(4)
					 .ToArray();
				string title = paddedData[0];
				int index = title.IndexOf(" No");
				string reference = "";
				if (index != -1)
				{
					reference = title.Substring(index + 4);
					title = title.Substring(0, index);
				}
				referenceTextBox.Text = reference;
				TbTitle.Text = title;
				TbAuthor.Text = paddedData[1];
				tbTotalCW.Text = dataManager.TotalCount.ToString();
				string notes = "";
				if (record.ScratchPad != null)
				{
					notes = record.ScratchPad.ToString();
				}
				_linesDictionary.Clear();
				var linesForCWNo = new List<Line>();
				if (record.Ticks != null)
				{
					string tickData = record.Ticks.ToString();
					string[] midPt = tickData.Split(';');
					for (int i = 0; i < midPt.Length; i++)
					{
						if (midPt[i] != "")
						{
							string[] coords = midPt[i].Split(',');
							Point midPoint = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
							linesForCWNo.Add(new Line(
								midPoint,
								CWNo
							));
						}
					}
				}
				if (record.CluesAcross != "")
				{
					// add missing length?
					//crosswordForm.OCRTextParseNew(record.CluesAcross, TbRichTextAcrossClues, "Across");
					//crosswordForm.OCRTextParseNew(record.CluesDown, TbRichTextDownClues, "Down");

					TextParser.OCRTextParseNew(record.CluesAcross, TbRichTextAcrossClues, "Across");
					TextParser.OCRTextParseNew(record.CluesDown, TbRichTextDownClues, "Down");

					TbRichTextAcrossClues.Visible = true;
					TbRichTextDownClues.Visible = true;
					PictureBoxClues.Visible = false;
				}
				else
				{
					TbRichTextAcrossClues.Text = "";
					TbRichTextDownClues.Text = "";
					TbRichTextAcrossClues.Visible = false;
					TbRichTextDownClues.Visible = false;
					PictureBoxClues.Visible = true;
				}
				CbHints.Checked = false;
				if (record.HintsAcross != "")
				{
					CbHints.Checked = true;
				}
				_linesDictionary[CWNo] = linesForCWNo ?? new List<Line>();
				PictureBoxClues.Invalidate(); // Request a redraw
				LoadCellDataIntoGrid(record.CellData);
			}
		}

		private void LoadCellDataIntoGrid(CellState[,] cellStates)
		{
			bool isSolution = true;
			CbSolns.Checked = false;
			for (int row = 0; row < rowCnt; row++)
			{
				for (int col = 0; col < colCnt; col++)
				{
					CellState cellState = cellStates[row, col];
					DataGridViewCell cel = dataGridView1.Rows[row].Cells[col];
					if (cellState.Value == "#")
					{
						cel.Style.BackColor = Color.Black;
					}
					else
					{
						cel.Style.BackColor = Color.White;
					}
					cel.Value = cellState.Value;
					if (cellState.Solution == "" || cellState.Solution == "-")
					{
						isSolution = false;
					}
					if (cellState.WordSeparator == 1)
					{
						ToggleCellBorder(cel, BorderSide.Bottom);
						var cellKey = new Tuple<int, int>(row, col);
						var borderList = cellsWithBorders[cellKey];
						borderList.Add(BorderSide.Bottom);
					}
					if (cellState.WordSeparator == 2)
					{
						ToggleCellBorder(cel, BorderSide.Right);
						var cellKey = new Tuple<int, int>(row, col);
						var borderList = cellsWithBorders[cellKey];
						borderList.Add(BorderSide.Right);
					}
				}
			}
			if (isSolution == true)
			{
				CbSolns.Checked = true;
			}
		}
		private void ToggleCellBorder(DataGridViewCell cell, BorderSide side)
		{
			if (cell == null) return;
			var cellKey = new Tuple<int, int>(cell.RowIndex, cell.ColumnIndex);
			if (!cellsWithBorders.ContainsKey(cellKey))
			{
				cellsWithBorders.Add(cellKey, new List<BorderSide>());
			}
			var borderList = cellsWithBorders[cellKey];
			if (borderList.Contains(side))
			{
				borderList.Remove(side);
				if (borderList.Count == 0)
				{
					cellsWithBorders.Remove(cellKey);
				}
			}
			else
			{
				borderList.Add(side);
			}
			dataGridView1.Invalidate();
		}
		private void LoadData(string filePathName)
		{
			string json = ReadJsonFromZip(filePathName);
			dataManager.Records = JsonConvert.DeserializeObject<List<CellDataRecord>>(json);
			int startCWNo = 0;
			dataManager.CurrentIndex = dataManager.Records.Count > 0 ? startCWNo : -1;
		}
	}
}
