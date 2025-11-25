using ExCSS;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Application = System.Windows.Forms.Application;
using Image = System.Drawing.Image;

namespace Crossword_Filler
{
	public partial class Form1 : Form
	{
		private const int PFM_LINESPACING = 0x00000100;
		private const int EM_SETPARAFORMAT = 1095;
		private const int SCF_SELECTION = 1;

		// Use explicit CharSet for consistency
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1, Size = 72)]
		private struct PARAFORMAT2
		{
			public int cbSize;
			public uint dwMask;
			public short wNumbering;
			public ushort wReserved;
			public int dxStartIndent;
			public int dyStartIndent;
			public int dxRightIndent;
			public int dyRightIndent;
			public int dyOutdent;
			public short wAlignment;
			public short wShadingWeight;
			public short wShadingStyle;
			public short wBorderSpace;
			public short wBorderWidth;
			public short wBorders;
			public short wCustomFlags;
			public int dyLineSpacing;
			public short sStyle;
			public byte bLineSpacingRule;
			public byte bOutlineLevel;
			public short wHeadingStyle;
			public short wReserved2;
			public int dxOffset;
			public short wEffects;
			public short wWidth;
		}
		private const int PFM_STARTINDENT = 0x00000001; // Add this constant
		public PictureBox MyPictureBoxClues => PictureBoxClues;
		private DataManager dataManager;
		public static int rowCnt;
		public static int colCnt;
		public int selectedRow;
		public int selectedCol;
		private int padHeight;
		private string filePathName;
		public string fileCWPathName;
		public string fileCWPathNameCurrent;
		public string appPath;
		public string downloadsPath;
		public bool dataAdded = false;
		public bool mouseClk = false;
		public bool ticksDraw; //do not draw when pasting new clues
		public float _scaleX;
		public float _scaleY;
		public Bitmap capturedCluesImage;
		private string appNameVersion;
		private int initialDpi;
		private int fmWidth;
		private float scaleFactor;
		public enum BorderSide
		{
			None = 0,
			Bottom = 1,
			Right = 2
		}
		private readonly JsonSerializerOptions jsonOptions;

		private List<string> recentFiles;
		private UserSettings userSettings;

		// Define Arrays

		//private List<string> recentFiles = new List<string>();
		// Holds the clue numbers for each cell
		public string[,] clueNo;
		// Dictionary list of values, border sides and cell fill for each cell.
		private Dictionary<Tuple<int, int>, List<BorderSide>> cellsWithBorders = new Dictionary<Tuple<int, int>, List<BorderSide>>();
		// JSON Export
		private List<CellDataRecord> items;
		// Define ticks, array and image
		private Dictionary<string, List<Line>> _linesDictionary = new Dictionary<string, List<Line>>();
		// Array for clue
		public Dictionary<string, ClueData> clueData = new Dictionary<string, ClueData>();

		public Form1(string[] args)
		{
			InitializeComponent();
			userSettings = SettingsManager.LoadSettings();

			this.AutoScaleMode = AutoScaleMode.Dpi;
			this.KeyPreview = true;
			appNameVersion = "Crossword Manager v" + Application.ProductVersion;
			if (args.Length > 0)
			{
				string filePathName = args[0];
				this.Text = appNameVersion + " - " + Path.GetFileName(filePathName);
			}
			clueNo = new string[rowCnt, colCnt];
			dataManager = new DataManager();
			jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				PropertyNameCaseInsensitive = true,
				Converters = { new CellOrIntListConverter(), new ClueConverter(), new CharArray2DConverter() }
			};

			// Wire/subscribe the trigger events += or -=
			this.FormClosing += Form1_FormClosing; // Ensure this line is present

			DataGridView dg = dataGridView1;
			dg.EditingControlShowing += dataGridView1_EditingControlShowing;
			dg.CellMouseDown += dataGridView1_CellMouseDown;
			dg.CellPainting += DataGridView1_CellPainting;
			dg.CellBeginEdit += dataGridView1_CellBeginEdit;
			dg.KeyPress += dataGridView1_KeyPress;

			PictureBoxClues.MouseClick += PictureBoxClues_MouseClick;
			PictureBoxClues.Paint += PictureBoxClues_PaintClues;
			PictureBoxClues.Paint += PictureBoxClues_PaintTicks;
		}
		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			initialDpi = this.DeviceDpi;
			scaleFactor = (float)this.DeviceDpi / 96f;
			MessageBox.Show($"Loaded Width: {this.Width}, Initial DPI: {initialDpi}");
			fmWidth = this.Width;
			//Form1_DpiChanged(this, new DpiChangedEventArgs(new Rectangle(), initialDpi, initialDpi));
			SetDpiAwareMenuIcons();
			SetDpiAwareButtonIcons();
			ScratchpadSetup();
			SideMenuPanelOn();
			// folders

			appPath = Application.StartupPath;
			string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			downloadsPath = Path.Combine(userFolder, "Downloads");
			// Temp run
			//string fileJSON = Path.Combine(downloadsPath, "CWData1-5.json");
			//JSONFieldChange(fileJSON);
			//return;

			// Startup methods
			// TbRichTextAcrossClues.SelectionCharOffset = -2;
			ticksDraw = true;
			fileCWPathName = "";
			tbCWNo.Text = userSettings.LastCrossword.ToString();
			if (string.IsNullOrWhiteSpace(filePathName))
			{
				fileCWPathName = userSettings.LastJSON.ToString();
			}
			else
			{
				fileCWPathName = filePathName; // commandline
			}
			LoadRecentFiles();
			UpdateRecentFilesMenu();
			//bool success = SetRichTextBoxLineSpacing(TbRichTextAcrossClues, 200);
			if (fileCWPathName == "" || fileCWPathName.Contains("blank") || File.Exists(fileCWPathName) == false) //check for file exist
			{
				fileCWPathName = "blank";
				fileCWPathNameCurrent = "blank";
				importToolStripMenuItem.PerformClick(); // actually newTool
			}
			else
			{
				NoCrossword.Visible = false;
				newJSONCluesFnAppText(fileCWPathName);
				LoadData(fileCWPathName);
				ChangeCrossword();
			}
		}


		// Select Icon size
		private void SetDpiAwareButtonIcons()
		{
			//DpiChangedBeforeParent 
			//DpiChangedAfterParent 
			//DpiChanged 
			int height;
			int width;
			string svgString = "";
			foreach (Control control in panel1.Controls)
			{
				if (control is Button button)
				{
					string resourceName = LoadSVG.GetButtonResourceName(button.Name);
					//Debug.Print(resourceName);
					if (!string.IsNullOrEmpty(resourceName))
					{
						svgString = LoadSVG.GetSvgStringFromResource(resourceName);
						if (svgString != null)
						{
							button.Image = null;
							height = (int)(button.Height - (button.Height * 0.20));
							width = (int)(button.Width - (button.Width * 0.25));
							button.Image = LoadSVG.GetSvgImage(svgString, width, height);
							button.ImageAlign = ContentAlignment.MiddleCenter;
						}
					}
				}
			}
			svgString = LoadSVG.GetSvgStringFromResource("btnscratchpad");
			height = (int)(BtnScratchPad.Height - (BtnScratchPad.Height * 0.20));
			width = (int)(BtnScratchPad.Width);
			BtnScratchPad.Image = LoadSVG.GetSvgImage(svgString, width, height);
			BtnScratchPad.ImageAlign = ContentAlignment.MiddleCenter;

		}
		private void SetDpiAwareMenuIcons()
		{
			// Get the system's DPI scale factor
			float dpiScale = (float)this.DeviceDpi / 96.0f;
			int targetSize;
			if (dpiScale > 1.5)
			{
				targetSize = 32; // For 150% or higher scaling
			}
			else
			{
				targetSize = 16; // For 100% or standard scaling
			}
			// --- Loop through all top-level menu items ---
			// SetMenuItemsImage2(menuStrip1.Items, targetSize);

			// --- Loop through all top-level and sub menu items ---
			SetMenuItemsImage(menuStrip1.Items, targetSize);

		}
		private void SetMenuItemsImage(ToolStripItemCollection items, int size)
		{
			foreach (ToolStripItem item in items)
			{
				if (item is ToolStripMenuItem menuItem)
				{
					// Assign the image only if the menu item is a command
					string resourceName = LoadSVG.GetResourceName(menuItem.Name);
					Debug.Print(resourceName);
					if (!string.IsNullOrEmpty(resourceName))
					{
						// Look up the resource and convert the byte array to a string
						string svgString = LoadSVG.GetSvgStringFromResource(resourceName);
						if (svgString != null)
						{
							menuItem.Image = LoadSVG.GetSvgImage(svgString, size, size);
						}
					}

					// Recursively call the method for any sub-menu items
					if (menuItem.HasDropDownItems)
					{
						SetMenuItemsImage(menuItem.DropDownItems, size);
					}
				}
			}
		}
		private int ScalePixelValue(int value)
		{
			// LogicalToDeviceUnits requires a Size or Point object.
			// We create a Size with the value in the width, use the method, and return the scaled Width.
			return this.LogicalToDeviceUnits(new System.Drawing.Size(value, 0)).Width;
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, ref PARAFORMAT2 lParam);

		// --- Extension Method to apply line spacing ---

		/// <summary>
		/// Sets the line spacing for the RichTextBox.
		/// </summary>
		/// <param name="rtb">The RichTextBox control.</param>
		/// <param name="lineSpacingTwips">The exact spacing in twips (1/20 of a point). 
		/// To reduce spacing below default single, use a smaller value.</param>
		private bool SetRichTextBoxLineSpacing(RichTextBox rtb, int lineSpacingTwips)
		{
			int structSize = Marshal.SizeOf(typeof(PARAFORMAT2));
			PARAFORMAT2 fmt = new PARAFORMAT2();
			//fmt.cbSize = structSize;
			fmt.cbSize = Marshal.SizeOf(fmt);
			fmt.dwMask = (uint)(PFM_LINESPACING | PFM_STARTINDENT);
			//fmt.dwMask = (uint)PFM_LINESPACING;
			fmt.dyLineSpacing = lineSpacingTwips;
			fmt.bLineSpacingRule = 4; // Rule 4 means "exact spacing"
			rtb.SelectAll();
			IntPtr result = SendMessage(rtb.Handle, EM_SETPARAFORMAT, SCF_SELECTION, ref fmt);
			System.Diagnostics.Debug.WriteLine($"SendMessage result: {result}");
			return result != IntPtr.Zero; // Returns true if successful
		}
		private void LoadTextAndApplyRtfSpacing(RichTextBox rtb)
		{
			string plainTextContent = rtb.Text;
			int lineSpacingTwips = 120;
			// Use RTF control words to define the spacing.
			// \sl200 is 200 twips spacing (10pt)
			// \slmult0 means use exactly that height, not a multiple of 1.
			//string rtfPrefix = $@"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033{{\fonttbl{{\f0\fnil\fcharset0 Segoe UI;}}}}\pard\f0\fs18\sl{lineSpacingTwips}\slmult0 ";
			string rtfPrefix = String.Format(@"{{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033{{\fonttbl{{\f0\fnil\fcharset0 Segoe UI;}}}}\pard\f0\fs18\sl{0}\slmult0 ", lineSpacingTwips);

			string rtfSuffix = @"}";

			// Replace newlines with the RTF paragraph break
			string rtfContent = plainTextContent.Replace(Environment.NewLine, @"\par ");

			// Combine everything and assign it
			rtb.Rtf = rtfPrefix + rtfContent + rtfSuffix;

			// Note: This method bypasses the need for the P/Invoke code entirely.
		}
		private void LoadTextAndApplySpacing(RichTextBox rtb,int newSpacing)
		{
			string newTextContent = rtb.Text;
			// 1. Ensure the control is in RTF mode before manipulating formats
			if (string.IsNullOrEmpty(rtb.Rtf))
			{
				rtb.Rtf = "{\\rtf1}";
			}

			// 2. Clear the existing content safely (preserves mode better than setting .Text = "")
			//rtb.Clear();
			rtb.Rtf = "";

			// 3. Add the new plain text using AppendText (avoids reverting to plain text mode defaults)
			rtb.AppendText(newTextContent);

			// 4. Select all the newly added text
			rtb.SelectAll();

			// 5. Apply the specific line spacing (e.g., 200 twips for 9pt Segoe UI)
			bool success = SetRichTextBoxLineSpacing(rtb, newSpacing);

			if (!success)
			{
				System.Diagnostics.Debug.WriteLine("Warning: Failed to set line spacing.");
			}

			// 6. Deselect the text for user interaction
			rtb.SelectionLength = 0;
		}

		// Setup	
		private void CrosswordGridSetup(int rowCnt, int colCnt)
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
			dg.RowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
			dg.RowsDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
			int colWid = ScalePixelValue(40);
			int rowHt = ScalePixelValue(30);
			// max size width/height 15x40=600, 15x30=450
			// min size width/height 11x45=495, 11x33=363
			if (rowCnt < 15)
			{
				colWid = colWid + 5;
				rowHt = rowHt + 3;
			}
			if (rowCnt > 15)
			{
				colWid = colWid - 3;
				rowHt = rowHt - 2;
			}
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
					DataGridViewCell cel = dg.Rows[i].Cells[j];
					cel.Value = "";
					cel.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
					cel.Style.Font = new Font("Arial", 12, System.Drawing.FontStyle.Bold);
				}
			}
			dg.Size = new System.Drawing.Size(colCnt * colWid + 3, rowCnt * rowHt + 3);
			if (panel1.Visible)
			{
				dg.Left = (panel1.Width - 10 + PictureBoxClues.Left - dg.Width) / 2;
				this.Text = appNameVersion + " - " + Path.GetFileName(fileCWPathName);
			}
			else
			{
				dg.Left = (10 + PictureBoxClues.Left - dg.Width) / 2;
				this.Text = appNameVersion + " - " + Path.GetFileName(fileCWPathName) + " " + tbCWNo.Text + "/" + tbTotalCW.Text;

			}
		}
		private void ScratchpadSetup()
		{
			DataGridView dgvPad = DataGridScratchPad;
			padHeight = 280;
			dgvPad.RowCount = 16;
			foreach (DataGridViewRow row in dgvPad.Rows)
			{
				row.Height = 20;
			}
			for (int i = 0; i < 1; i++)
			{
				dgvPad.Rows[i].Cells[0].Value = "";
				dgvPad.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dgvPad.Rows[i].Cells[0].Style.Font = new Font("Arial", 8, System.Drawing.FontStyle.Regular);
			}
			dgvPad.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
			dgvPad.RowsDefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
			dgvPad.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightYellow;
			dgvPad.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
			dgvPad.Top = BtnScratchPad.Top + BtnScratchPad.Height;
			dgvPad.Height = 0;
		}
		private void SideMenuPanelOff()
		{
			this.Width = ScalePixelValue(1020); //Original 1185
												//this.Width = fmWidth;
			dataGridView1.Left = (ScalePixelValue(10) + PictureBoxClues.Left - dataGridView1.Width) / 2;
			PictureBoxClues.Left = this.Width - PictureBoxClues.Width - ScalePixelValue(30);
			panel1.Visible = false;
			TbRichTextAcrossClues.Left = PictureBoxClues.Left;
			TbRichTextDownClues.Left = TbRichTextAcrossClues.Left + TbRichTextAcrossClues.Width + 5;
			label14.Left = TbRichTextAcrossClues.Left;
			label13.Left = TbRichTextDownClues.Left;
			NoCrossword.Left = dataGridView1.Left + (dataGridView1.Width - NoCrossword.Width) / 2;
			this.Text = appNameVersion + " - " + Path.GetFileName(fileCWPathName) + " " + tbCWNo.Text + "/" + tbTotalCW.Text;
		}
		private void SideMenuPanelOn()
		{
			//this.Width = 1185*initialDpi/96; //Original 1185
			this.Width = fmWidth;
			panel1.Visible = true;
			dataGridView1.Left = (panel1.Width - ScalePixelValue(10) + PictureBoxClues.Left - dataGridView1.Width) / 2;
			RichTextBox rtb = TbRichTextAcrossClues;
			PictureBoxClues.Left = this.Width - PictureBoxClues.Width - ScalePixelValue(30);
			rtb.Left = PictureBoxClues.Left;
			TbRichTextDownClues.Left = rtb.Left + rtb.Width + ScalePixelValue(5);
			label14.Left = rtb.Left;
			label13.Left = TbRichTextDownClues.Left;
			NoCrossword.Left = dataGridView1.Left + (dataGridView1.Width - NoCrossword.Width) / 2;
			this.Text = appNameVersion + " - " + Path.GetFileName(fileCWPathName);

		}

		// Form/Datagridview trigger methods

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			// Check for the Ctrl modifier and the 'A' key
			if (e.Control && e.KeyCode == Keys.Left)
			{
				bPrev_Click_1(BtnPrev, EventArgs.Empty);
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.Right)
			{
				bNext_Click_1(BtnNext, EventArgs.Empty);
				e.Handled = true; // Mark the event as handled to stop further processing
			}
			else if (e.Control && e.KeyCode == Keys.M)
			{
				if (panel1.Visible == false)
				{
					SideMenuPanelOn();
				}
				else
				{
					SideMenuPanelOff();
				}
				ReLoad();
			}
			else if (e.Control && e.KeyCode == Keys.I)
			{
				PuzUtility();
			}
			else if (e.Control && e.KeyCode == Keys.G)
			{
				//gray text
			}
			else if (e.Control && e.KeyCode == Keys.A)
			{
				//across separator
			}
			else if (e.Control && e.KeyCode == Keys.D)
			{
				//down separator
			}
			else if (e.Control && e.KeyCode == Keys.U)
			{
				//Update clue solved
			}
		}
		private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (mouseClk == true)
			{
				mouseClk = false;
				dataGridView1.EndEdit();
				return;
			}
			e.Control.Enabled = false;
		}
		private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			// Cancel the editing to prevent any control from showing
			e.Cancel = true;
		}
		private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
		{
			DataGridViewCell currentCell = dataGridView1.CurrentCell;
			selectedRow = currentCell.RowIndex;
			selectedCol = currentCell.ColumnIndex;
			if (e.KeyChar == (char)13)
			{
				e.Handled = true;
				return; // Exit the method
			}
			if (currentCell != null)
			{
				char upperChar = char.ToUpper(e.KeyChar);
				// Check if the input is a letter or a space
				if (char.IsLetter(upperChar))
				{
					// Set the cell value to the uppercase letter, overwriting the old value
					currentCell.Value = upperChar.ToString();
					if (RadioPencil.Checked == true)
					{
						currentCell.Style.ForeColor = System.Drawing.Color.Gray;
					}
					else
					{
						currentCell.Style.ForeColor = System.Drawing.Color.Black;
					}
					e.Handled = true; // Prevent further processing of the key press
				}
				else if (char.IsWhiteSpace(upperChar))
				{
					// Convert space to an empty string
					currentCell.Value = string.Empty;
					e.Handled = true; // Prevent further processing of the key press
				}
				else
				{
					// Prevent non-letter input
					// e.SuppressKeyPress = true
					e.Handled = true;
				}
			}
		}
		private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			mouseClk = true;
			DataGridView dgv = dataGridView1;
			var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
			dgv.CurrentCell = cell;
			selectedRow = cell.RowIndex;
			selectedCol = cell.ColumnIndex;
			int rowIndex = e.RowIndex;
			int colIndex = e.ColumnIndex;
			if (cell.Style.BackColor != System.Drawing.Color.Black)
			{
				cell.Style.BackColor = System.Drawing.Color.Yellow;
			}
			TbWordLookUp.SelectedText = "";
			string direction = "";
			string cNum = "";
			if (clueNo[rowIndex, colIndex] != null && clueNo[rowIndex, colIndex] != "")
			{
				string ansDown = "*";
				string ansAcross = "*";
				cNum = clueNo[rowIndex, colIndex];
				string cNoExtract = new string(cNum.TakeWhile(char.IsDigit).ToArray());
				direction = cNum.Substring(cNum.Length - 1);
				string dataDirection = "";
				if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					if (direction == "B")
					{
						ansAcross = ReadCrossword("A", rowIndex, colIndex, dgv);
						ansDown = ReadCrossword("D", rowIndex, colIndex, dgv);
						if (ansAcross.Contains("_") && ansDown.Contains("_") == false)
						{
							dataDirection = "A";
							direction = "A";
						}
						else if (ansDown.Contains("_") && ansAcross.Contains("_") == false)
						{
							dataDirection = "D";
							direction = "D";
						}
						else if (ansDown.Contains("_") && ansAcross.Contains("_"))
						{
							dataDirection = "A";
							direction = "B"; ;
						}
						else
						{
							return;
						}
					}
					else if (direction == "A")
					{
						ansAcross = ReadCrossword("A", rowIndex, colIndex, dgv);
						if (ansAcross.Contains("_") == false)
						{
							return;
						}
						dataDirection = direction;
					}
					else if (direction == "D")
					{
						ansDown = ReadCrossword("D", rowIndex, colIndex, dgv);
						if (ansDown.Contains("_") == false)
						{
							return;
						}
						dataDirection = direction;
					}
					string ans = ReadCrossword(dataDirection + "*", rowIndex, colIndex, dgv);
					HighlightClueText(cNoExtract, dataDirection);
					EnterSolution enterSolutionForm = new EnterSolution(this);
					enterSolutionForm.TbRowNo.Text = rowIndex.ToString();
					enterSolutionForm.TbColNo.Text = colIndex.ToString();
					enterSolutionForm.StartEnterSolution(direction, cNum, ans);
					dataGridView1.Focus();
				}
				else
				{
					ClueExplorer clueExplorerForm = Application.OpenForms.OfType<ClueExplorer>().FirstOrDefault();
					if (clueExplorerForm != null)
					{
						ClearDataGridBackColor();
						string hightlightDirection = direction;
						if (direction == "B")
						{
							hightlightDirection = "A";
						}
						string ans = ReadCrossword(hightlightDirection + "*", rowIndex, colIndex, dgv);
						HighlightClueText(cNoExtract, hightlightDirection);
						clueExplorerForm.TbRowNo.Text = rowIndex.ToString();
						clueExplorerForm.TbColNo.Text = colIndex.ToString();
						clueExplorerForm.AddClueSelection(direction, cNum, ans);
						//clueExplorerForm.Activate();
					}
				}
			}
		}
		private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && !dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
			{
				var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
				var cellKey = new Tuple<int, int>(e.RowIndex, e.ColumnIndex);

				// Paint background: black cells stay black; otherwise selection highlight if selected, else white
				if (cell.Style.BackColor == System.Drawing.Color.Black)
				{
					using (var brush = new SolidBrush(System.Drawing.Color.Black))
						e.Graphics.FillRectangle(brush, e.CellBounds);
				}
				else if (cell.Selected)
				{
					using (var brush = new SolidBrush(System.Drawing.Color.Yellow))
						e.Graphics.FillRectangle(brush, e.CellBounds);
				}
				else if (cell.Style.BackColor == System.Drawing.Color.LightGreen)
				{
					using (var brush = new SolidBrush(System.Drawing.Color.LightGreen))
						e.Graphics.FillRectangle(brush, e.CellBounds);
				}
				else
				{
					using (var brush = new SolidBrush(System.Drawing.Color.White))
						e.Graphics.FillRectangle(brush, e.CellBounds);
				}

				// Get the text to draw

				string text = e.FormattedValue.ToString();
				Font font1 = e.CellStyle.Font;

				// Measure the text size
				//SizeF textSize = e.Graphics.MeasureString(text, font1);

				// Calculate the position to center the text
				//float xx = e.CellBounds.X + (e.CellBounds.Width - textSize.Width) / 2;
				//float yy = e.CellBounds.Y + (e.CellBounds.Height - textSize.Height) / 2;

				// Draw the text
				//Brush textBrush;
				//if (e.CellStyle.ForeColor != System.Drawing.Color.Black)
				//{
				//	textBrush = new SolidBrush(System.Drawing.Color.Gray);
				//}
				//else
				//{
				//	textBrush = new SolidBrush(System.Drawing.Color.Black);
				//}
				//using (textBrush)
				//{
				//	e.Graphics.DrawString(text, font1, textBrush, xx, yy);
				//}


				// Determine the text color
				System.Drawing.Color textColor;
				if (e.CellStyle.ForeColor != System.Drawing.Color.Black)
				{
					textColor = System.Drawing.Color.Gray;
				}
				else
				{
					textColor = System.Drawing.Color.Black;
				}

				// Draw the text using TextRenderer
				// The TextFormatFlags.HorizontalCenter and TextFormatFlags.VerticalCenter flags handle centering automatically
				TextRenderer.DrawText(
					e.Graphics,
					text,
					font1,
					e.CellBounds,
					textColor,
					TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.WordEllipsis
				);

				// Paint the clue number
				string clueNumber = clueNo[e.RowIndex, e.ColumnIndex]; // Implement this method
				if (!string.IsNullOrEmpty(clueNumber))
				{
					clueNumber = clueNumber.Substring(0, 2).Trim();
					using (Font font = new Font("Arial", 8, System.Drawing.FontStyle.Bold))
					using (Brush brush = new SolidBrush(System.Drawing.Color.Red))
					{
						var location = new System.Drawing.Point(e.CellBounds.Left - 1, e.CellBounds.Top - 1);
						e.Graphics.DrawString(clueNumber, font, brush, location);
					}
				}

				// Paint all borders
				using (Pen thinPen = new Pen(dataGridView1.GridColor, 1))
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
					using (Pen thickPen = new Pen(System.Drawing.Color.Gray, penWidth))
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
				e.Handled = true;
				//dataGridView1.EndEdit();
			}
			else
			{
				// For cells in edit mode, let the default painting occur.
				e.Handled = false;
			}
		}

		// Menu - File

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Crossword JSON Files (*.cjz)|*.cjz|All Files (*.*)|*.*";
				openFileDialog.Title = "Select a Crossword file to load";
				openFileDialog.InitialDirectory = downloadsPath;
				openFileDialog.RestoreDirectory = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						filePathName = openFileDialog.FileName;
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				else
				{
					// MessageBox.Show("File selection cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
			}
			if (fileCWPathNameCurrent == filePathName)
			{
				MessageBox.Show("File already loaded", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			OpenRecentFile(filePathName);
		}
		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewLoadClose();
			fileCWPathName = Path.Combine(appPath, "CWData blank.cjz");
			newJSONCluesFnAppText(fileCWPathName);
			CreateBlankJsonFile(fileCWPathName);
			ticksDraw = true;
			tbCWNo.Text = "1"; // set to "0"?
			dataAdded = false;
			NoCrossword.Visible = true;
			LoadData(fileCWPathName); // loads blank
			_linesDictionary.Clear();
			TbRichTextAcrossClues.Text = "";
			TbRichTextDownClues.Text = "";
			capturedCluesImage = null;
			ChangeCrossword(); // just returns due to null
		}
		private void exportSolutionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (fileCWPathNameCurrent.Contains("blank") && dataAdded == false)
			{
				MessageBox.Show("No data to export", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			NewLoadClose();
			ExportJSON();
			// MessageBox.Show("Exported","Export file");
		}
		private void importSolutionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Application.OpenForms["ImportSelect"] == null)
			{
				// RecordUpdate();
				Form impSelect = new ImportSelect(this, fileCWPathName);
				impSelect.Top = this.Top + 150;
				impSelect.Left = this.Left + 300;
				impSelect.Show();
			}
			else
			{
				ImportSelect impForm = Application.OpenForms.OfType<ImportSelect>().FirstOrDefault();
				impForm.Close();
				importSolutionToolStripMenuItem.PerformClick();
			}
		}
		private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RecordUpdate();
			SaveData(fileCWPathNameCurrent);
			string sourceZip = fileCWPathNameCurrent;
			SaveAsJSONZIP();
			if (filePathName != "")
			{
				File.Copy(sourceZip, filePathName, true);
				string jsonOld = Path.GetFileName(sourceZip).Replace(".cjz", ".json");
				string jsonNew = Path.GetFileName(filePathName).Replace(".cjz", ".json");
				RenameFileFromZip(filePathName, jsonOld, jsonNew);
				SaveData(fileCWPathNameCurrent); // new file as per SaveAs
			}
		}
		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewLoadClose();
		}
		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewLoadClose();
			DeleteCrossword();
		}
		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bClose_Click(bClose, EventArgs.Empty);
			//bClose.PerformClick();
		}

		// Menu - Tools

		private void addToolStripMenuItem_Click(object sender, EventArgs e) // Scan new crosswords - BtnImport
		{
			BtnImport_Click(BtnImport, EventArgs.Empty);
		}
		private void cypticExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BtnLoadExplorer_Click(BtnLoadExplorer, EventArgs.Empty);
		}
		private void hintToolStripMenuItem_Click(object sender, EventArgs e)
		{
			HintDisplay();
		}
		private void solutionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SolutionDisplay();
		}
		private void cheatToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (TbRichTextAcrossClues.Visible == false)
			{
				CheatView cheat = new CheatView();
				using (OpenFileDialog openFileDialog = new OpenFileDialog())
				{
					openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
					openFileDialog.Title = "Select Solution File";
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						if (openFileDialog.FileName != "")
						{
							cheat.PictureAnswers.Image = new System.Drawing.Bitmap(openFileDialog.FileName);
							cheat.Width = cheat.PictureAnswers.Width + ScalePixelValue(20);
							cheat.Height = cheat.PictureAnswers.Height + ScalePixelValue(80);
							cheat.button1.Left = (cheat.Width - cheat.button1.Width) / 2;
							cheat.ShowDialog();
						}
					}
				}
			}
			else
			{
				DisplaySolutionGrid();
				DialogResult result = DialogResult.None;
				if (CbSolns.Checked == false)
				{
					result = MessageBox.Show("Do you want to copy answers to the Solution grid?", "Copy to Solution", MessageBoxButtons.YesNo);
				}
				if (result == DialogResult.Yes)
				{
					var record = dataManager.GetCurrentRecord();
					DataGridView dgv = dataGridView1;
					for (int r = 0; r < rowCnt; r++)
					{
						for (int c = 0; c < colCnt; c++)
						{
							CellState cell = record.CellData[r, c];
							cell.Solution = cell.Value;
						}
					}
					DisplaySolutionGrid();
				}
			}
		}
		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
		private void clueDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CluesReference clueRefenceForm = new CluesReference();
			clueRefenceForm.Top = this.Top + ScalePixelValue(100);
			clueRefenceForm.Left = this.Left + ScalePixelValue(50);
			clueRefenceForm.Show();
		}

		// Menu - Help

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (About about = new About(this))
			{

				PictureBox pb = about.PBLogo;
				RichTextBox rtb = about.RtbFeatures;
				Panel p = about.panel1;
				p.BackgroundImage = null;
				p.BackColor = System.Drawing.Color.LightGray;
				pb.Visible = true;
				string svgString = LoadSVG.GetSvgStringFromResource("pblogo");
				int height = (int)(pb.Height - (pb.Height * 0.20));
				int width = (int)(pb.Width);
				pb.Image = LoadSVG.GetSvgImage(svgString, width, height);
				rtb.Text = "Crossword Manager v1.0" + Environment.NewLine +
							"by Scintilla" + Environment.NewLine + Environment.NewLine +
							"Freeware 2025" + Environment.NewLine +
							"Windows 10/11" + Environment.NewLine +
							"Desktop App" + Environment.NewLine +
							"C#, Winforms and .NET 8.0" + Environment.NewLine;

				rtb.SelectAll();
				rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
				rtb.SelectionColor = System.Drawing.Color.Black;
				rtb.SelectionFont = new Font(rtb.SelectionFont, rtb.SelectionFont.Style | System.Drawing.FontStyle.Bold);
				// int ScaleValue(int value) => (int)(value * scaleFactor);
				rtb.Left = about.PBLogo.Width + ScalePixelValue(20);
				rtb.Width = about.Width - about.PBLogo.Width - ScalePixelValue(40);
				rtb.Height = ScalePixelValue(125);
				about.Height = ScalePixelValue(190);
				p.Width = about.Width - ScalePixelValue(5);
				p.Height = about.Height - ScalePixelValue(5);
				about.BtnClose.Top = about.Height - ScalePixelValue(40);
				about.BtnDonate.Top = about.BtnClose.Top;
				about.ShowDialog();
			}
		}
		private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("CROSSWORD KEYS:" + Environment.NewLine +
				"* Keys A to Z. Will convert to uppercase" + Environment.NewLine +
				"* ARROW KEYS to move" + Environment.NewLine +
				"* SPACE to delete letter" + Environment.NewLine +
				"* SHIFT Click Clue for easy solution entry" + Environment.NewLine +
				"* SHIFT Click to add/delete tick to Clues image" + Environment.NewLine + Environment.NewLine +
				"For specific help hover over any control and wait for ToolTip to appear", "Help");
		}
		private void featureToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (About about = new About(this))
			{
				PictureBox pb = about.PBLogo;
				RichTextBox rtb = about.RtbFeatures;
				Panel p = about.panel1;
				Panel panelRTB = new Panel();
				about.Controls.Add(panelRTB);
				pb.Visible = false;
				p.BackColor = System.Drawing.Color.LightGray;
				p.SendToBack();
				panelRTB.BackColor = System.Drawing.Color.White;

				rtb.Dock = System.Windows.Forms.DockStyle.Fill;
				rtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
				rtb.BackColor = System.Drawing.Color.White;
				string message = "Electronically fill Crosswords only available as hardcopies or not readily available for completion on-line. \r\n\r\n" +
					"Only need a Crossword image plus its clues. Clues are saved as png unless OCR'd\r\n\r\n" +
					"Use OCR directly from the screen or paste from cliboard or from the loaded image file\r\n\r\n" +
					"Image scan will depend on quality. Pre-processing may help \r\n\r\n" +
					"Use on-the-fly pre-processing to scale, sharpen, threshold, denoise etc\r\n\r\n" +
					"A place to also store all your Crosswords still to be completed. " +
					"You can browse and revisit for completion at any time.\r\n\r\n" +
					"Also has import and export options to share Crosswords. " +
					"Can read and write puz and ipuz which is better than scanning and OCR, and may also include the solution and hints\r\n \r\n" +
					"Finally, various other features to assist the stubborn and obscure clues, particularly useful for Cryptic Crosswords. \r\n\r\n" +
					"A Clues Explorer to aid anagram, clue disecting, matching letters, process of elimination etc. \r\n\r\n" +
					"A scratchpad for your ruminations and thinking out aloud. \r\n\r\n" +
					"An ability to compile a database of hints and common clue techniques for future reminder.\r\n\r\n" +
					"Online dictionaries for word and phrase searches \r\n\r\n" +
					"Last but not least an AI lookup!\r\n\r\n" +
					"Enjoy and..... Donate!\r\n";
				rtb.Text = message;
				rtb.ForeColor = System.Drawing.Color.Black;
				panelRTB.Controls.Add(rtb);
				//int ScaleValue(int value) => (int)(value * scaleFactor);
				panelRTB.Padding = new Padding(5, 0, 5, 0);
				p.Width = about.Width - 5;
				p.Height = about.Height - 5;
				panelRTB.Location = new System.Drawing.Point(5, 5);
				panelRTB.Size = new System.Drawing.Size(p.Width - 5, p.Height - 50);
				about.ShowDialog();
			}
		}
		private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}


		private void newJSONCluesFnAppText(string jsonPathFileName)
		{
			string jsonFileName = Path.GetFileName(jsonPathFileName);
			this.Text = appNameVersion + " - " + jsonFileName;
			fileCWPathName = jsonPathFileName;
			fileCWPathNameCurrent = jsonPathFileName;
		}
		public void CreateBlankJsonFile(string zipFile)
		{
			string jsonEntryName = Path.GetFileName(zipFile).Replace(".cjz", ".json");
			string jsonFile = zipFile.Replace(".cjz", ".json");
			string content = "[]";
			File.WriteAllText(jsonFile, content, Encoding.UTF8);
			string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			try
			{
				Directory.CreateDirectory(tempDirectory);
				if (File.Exists(zipFile))
				{
					File.Delete(zipFile);
				}
				ZipFile.CreateFromDirectory(tempDirectory, zipFile);
			}
			finally
			{
				if (Directory.Exists(tempDirectory))
				{
					Directory.Delete(tempDirectory, true);
				}
			}
			using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Update))
			{
				archive.CreateEntryFromFile(jsonFile, jsonEntryName);
			}
			File.Delete(jsonFile);
		}
		public void NewLoadClose()
		{
			if (fileCWPathNameCurrent.Contains("blank") && dataManager.Records.Count != 0)
			{
				SaveAsJSONZIP();
			}
			if (fileCWPathNameCurrent.Contains("blank") == false)
			{
				NoCrossword.Visible = false;
				RecordUpdate();
				SaveData(fileCWPathNameCurrent);
			}
		}
		public void SaveAsJSONZIP()
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Filter = "Crossword files (*.cjz)|*.cjz|All files (*.*)|*.*"; // File type filter
				saveFileDialog.Title = "Save Crossword As";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					filePathName = saveFileDialog.FileName;
					if (fileCWPathNameCurrent == filePathName)
					{
						MessageBox.Show("Already saved as this file. Select Save");
						return;
					}
					newJSONCluesFnAppText(filePathName);
				}
				else
				{
					filePathName = "";
					MessageBox.Show("Save cancelled", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		// Recent files methods

		private void OpenRecentFile(string filePathName)
		{
			if (filePathName.Contains("blank"))
			{
				MessageBox.Show("File is blank.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if (File.Exists(filePathName) == false)
			{
				MessageBox.Show("File no longer available");
				recentFiles.Remove(filePathName);
				UpdateRecentFilesMenu();
				return;
			}
			if (filePathName == fileCWPathNameCurrent)
			{
				MessageBox.Show("File already loaded");
				return;
			}
			var json = ReadJsonFromZip(filePathName);
			var chkJSON = JsonConvert.DeserializeObject<List<CellDataRecord>>(json);
			if (chkJSON.Count == 0) //check for Null
			{
				MessageBox.Show("File has no data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			NewLoadClose();
			ticksDraw = true;
			tbCWNo.Text = "1";
			dataAdded = true;
			NoCrossword.Visible = false;
			newJSONCluesFnAppText(filePathName);
			LoadData(fileCWPathName);
			ChangeCrossword();
			AddToRecentFiles(fileCWPathName);
			UpdateRecentFilesMenu();
		}
		private void LoadRecentFiles()
		{
			recentFiles = userSettings.RecentFiles;
		}
		private void AddToRecentFiles(string filePathName)
		{
			if (string.IsNullOrWhiteSpace(filePathName)) return; // Prevent adding empty paths

			if (!recentFiles.Contains(filePathName))
			{
				recentFiles.Add(filePathName);
			}
			else
			{
				recentFiles.Remove(filePathName);
				recentFiles.Add(filePathName);  // Move to most recent
			}

			if (recentFiles.Count > 6) // Limit to 10 items
			{
				recentFiles.RemoveAt(0);
			}
			userSettings.RecentFiles = recentFiles;
			SettingsManager.SaveSettings(userSettings); // Serialize and save the file
		}
		private void UpdateRecentFilesMenu()
		{
			recentFilesToolStripMenuItem.DropDownItems.Clear();
			if (string.IsNullOrEmpty(recentFiles.ToString()))
			{
				return;
			}
			foreach (var file in recentFiles)
			{
				var item = new ToolStripMenuItem(file);
				if (!file.ToString().Contains("\"\""))
				{
					item.Click += (s, e) => OpenRecentFile(file);
					recentFilesToolStripMenuItem.DropDownItems.Add(item);
				}
			}
		}

		// Crossword Navigation and Update

		private void BtnCWInfo_Click(object sender, EventArgs e)
		{
			var currentRecord = dataManager.GetCurrentRecord();
			string[] refData = currentRecord.Reference.ToString().Split(';');
			string[] paddedData = refData.Take(4)
							 .Concat(Enumerable.Repeat(string.Empty, 4))
							 .Take(4)
							 .ToArray();
			using (CrossWordInfo crossInfoForm = new CrossWordInfo())
			{
				string title = paddedData[0];
				int index = title.IndexOf(" No");
				string reference = "";
				if (index != -1)
				{
					reference = title.Substring(index + 4);
					title = title.Substring(0, index);
				}
				crossInfoForm.TbTitle.Text = title;
				crossInfoForm.TbVersion.Text = paddedData[1];
				crossInfoForm.TbAuthor.Text = paddedData[2];
				crossInfoForm.TbCopyright.Text = paddedData[3];
				crossInfoForm.TbReference.Text = reference;
				crossInfoForm.ShowDialog();
				if (crossInfoForm.DialogResult == DialogResult.OK)
				{
					referenceTextBox.Text = crossInfoForm.TbReference.Text;
					currentRecord.Reference = crossInfoForm.TbTitle.Text + " No. " + crossInfoForm.TbReference.Text + ";" +
												crossInfoForm.TbVersion.Text + ";" +
												crossInfoForm.TbAuthor.Text + ";" +
												crossInfoForm.TbCopyright.Text + ";";
				}
			}
		}
		private void BtnCWKeys_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Use Cursor Keys to move" + Environment.NewLine +
							"SPACE to delete letter" + Environment.NewLine +
							"Will convert to uppercase" + Environment.NewLine +
							"SHIFT Click Clue for easy entry" + Environment.NewLine +
							"SHIFT Click to add/delete tick to Clues image", "Crossword Entry Keys");
		}
		private void bNext_Click_1(object sender, EventArgs e)
		{
			if (tbCWNo.Text != "" && tbCWNo.Text != "0" && int.Parse(tbCWNo.Text) != int.Parse(tbTotalCW.Text))
			{
				RecordUpdate();
				dataManager.LoadNext();
				ChangeCrossword();
				//ticksDraw = true;
			}
		}
		private void bPrev_Click_1(object sender, EventArgs e)
		{
			if (tbCWNo.Text != "" && tbCWNo.Text != "1" && tbCWNo.Text != "0")
			{
				RecordUpdate();
				dataManager.LoadPrev();
				ChangeCrossword();
			}
		}
		private void BtnJump_Click_1(object sender, EventArgs e)
		{
			if (tbTotalCW.Text == "0" || tbTotalCW.Text == "")
			{
				return;
			}
			string referenceToJump = referenceTextBox.Text;
			var currentRecord = dataManager.GetCurrentRecord();
			string[] refData = currentRecord.Reference.ToString().Split(';');
			string[] paddedData = refData.Take(4)
				 .Concat(Enumerable.Repeat(string.Empty, 4))
				 .Take(4)
				 .ToArray();
			string title = paddedData[0];
			int index = title.IndexOf(" No");
			string reference = "";
			if (index != -1)
			{
				reference = title.Substring(index + 4);
			}
			referenceTextBox.Text = reference;
			RecordUpdate();
			if (referenceToJump != "")
			{
				var record = dataManager.Records.FirstOrDefault(r => r.Reference == referenceToJump);
				if (record != null)
				{
					dataManager.CurrentIndex = dataManager.Records.IndexOf(record);
					ChangeCrossword();
				}
				else
				{
					MessageBox.Show("Reference not found.");
				}
			}
			dataGridView1.Focus();
		}
		private void BtnUpdate_Click_1(object sender, EventArgs e) //Save
		{
			NewLoadClose();
		}
		public void ReLoad()
		{
			dataManager.LoadSame();
			ChangeCrossword();
		}

		private void RecordUpdate() // Save current status before closure
		{
			var currentRecord = dataManager.GetCurrentRecord();
			DataGridView dgv = DataGridScratchPad;
			if (currentRecord != null)
			{
				//currentRecord.Reference is always saved if there are any changes
				string scratchPadData = "";
				string field = "";
				for (int i = 0; i < dgv.RowCount; i++)
				{
					object txt = dgv.Rows[i].Cells[0].Value;
					if (txt != null)
					{
						field = txt.ToString();
						field = field.Replace(",", "");
					}
					//if (field.ToString().Contains(",") || field.ToString().Contains("\""))
					//{
					//	 Escape internal double quotes by doubling them
					//	field = field.Replace("\"", "\"\"");
					//	field = $"\"{field}\"";
					//}
					scratchPadData = scratchPadData + field + ",";
				}
				if (scratchPadData.Length > 0)
				{
					scratchPadData = scratchPadData.Substring(0, scratchPadData.Length - 1);
				}
				currentRecord.ScratchPad = scratchPadData;
				string ticksData = "";
				if (_linesDictionary.Count != 0)
				{
					foreach (var line in _linesDictionary[tbCWNo.Text.Trim()])
					{
						ticksData = ticksData + line.MidPoint.X + "," + line.MidPoint.Y + ";";
					}
					if (ticksData.Length > 0)
					{
						ticksData = ticksData.Substring(0, ticksData.Length - 1);
					}
					currentRecord.Ticks = ticksData;
				}
				if (string.IsNullOrEmpty(currentRecord.CluesAcross) == false)
				{
					// aleady contains the latest - will not change.
				}
				if (string.IsNullOrEmpty(currentRecord.HintsAcross) == false)
				{
					// aleady contains the latest - will not change.
				}
				PopulateCellData(currentRecord);
				dataManager.UpdateRecord(currentRecord);
			}
			dataGridView1.Focus();
		}
		public void ChangeCrossword() // Load
		{
			cellsWithBorders.Clear();
			LoadCurrentRecord();
			var record = dataManager.GetCurrentRecord();
			if (record != null) //for blank file
			{
				if (TbRichTextAcrossClues.Visible == false)
				{
					LoadCluesNew(fileCWPathName);
				}
				AddClueNos(rowCnt, colCnt);
				ClueLines(TbRichTextAcrossClues);
				ClueLines(TbRichTextDownClues);
				UpdateClueStatus();
				//TbRichTextAcrossClues.SelectAll();
				// TbRichTextAcrossClues.SelectionCharOffset = 0;
				//SetRichTextBoxLineSpacing(TbRichTextAcrossClues, 200);
				//LoadTextAndApplySpacing(TbRichTextAcrossClues,200);
				//LoadTextAndApplyRtfSpacing(TbRichTextAcrossClues);
				//bool success = SetRichTextBoxLineSpacing(TbRichTextAcrossClues, 180);
				RadioPen.Checked = true;
				dataGridView1.Focus();
			}
		}
		public void LoadCurrentRecord() // load into rowCnt, tbCWNo, Reference, Scratchpad and ticks. Then grid letters etc
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
				}
				referenceTextBox.Text = reference;
				tbTotalCW.Text = dataManager.TotalCount.ToString();
				string notes = "";
				if (record.ScratchPad != null)
				{
					notes = record.ScratchPad.ToString();
				}
				string[] textRows = notes.Split(',');
				DataGridView dgv = DataGridScratchPad;
				for (int i = 0; i < textRows.Length; i++)
				{
					dgv.Rows[i].Cells[0].Value = textRows[i];
				}
				for (int i = textRows.Length; i < dgv.RowCount; i++)
				{
					dgv.Rows[i].Cells[0].Value = "";
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
							System.Drawing.Point midPoint = new System.Drawing.Point(int.Parse(coords[0]), int.Parse(coords[1]));
							linesForCWNo.Add(new Line(
								midPoint,
								CWNo
							));
						}
					}
				}
				// CrosswordForm crosswordForm = new CrosswordForm(this);
				if (string.IsNullOrEmpty(record.CluesAcross) == false)
				{
					// crosswordForm.OCRTextParseNew(record.CluesAcross, TbRichTextAcrossClues, "Across");
					TextParser.OCRTextParseNew(record.CluesAcross, TbRichTextAcrossClues, "Across");
					// crosswordForm.OCRTextParseNew(record.CluesDown, TbRichTextDownClues, "Down");
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
				PictureBoxClues.Invalidate(); // Request a redraw of the ticks
				LoadCellDataIntoGrid(record.CellData);
			}
			else
			{
				tbCWNo.Text = "0";
				tbTotalCW.Text = "0";
				referenceTextBox.Text = "";
				tbSolnStatus.Text = "";
				_linesDictionary.Clear();
				PictureBoxClues.Image = null;
				DataGridView dgv = dataGridView1;
				for (int i = 0; i < rowCnt; i++)
				{
					for (int j = 0; j < colCnt; j++)
					{
						dgv.Rows[i].Cells[j].Value = "";
						dgv.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
					}
				}
				clueNo = new string[rowCnt, colCnt];
				CbHints.Checked = false;
				dgv.ClearSelection();
				// dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
				NoCrossword.Visible = true;
				dgv.Invalidate();
			}
		}
		private void LoadCellDataIntoGrid(CellState[,] cellStates) //Update
		{
			bool isSolution = true;
			CbSolns.Checked = false;
			for (int row = 0; row < rowCnt; row++)
			{
				for (int col = 0; col < colCnt; col++)
				{
					string letterOnly;
					CellState cellState = cellStates[row, col];
					DataGridViewCell cel = dataGridView1.Rows[row].Cells[col];
					if (cellState.Value == "#")
					{
						cel.Style.BackColor = System.Drawing.Color.Black;
					}
					else
					{
						cel.Style.BackColor = System.Drawing.Color.White;
					}
					if (cellState.Value.Contains("*"))
					{
						letterOnly = cellState.Value.Substring(0, 1);
						cel.Style.ForeColor = System.Drawing.Color.Gray;
					}
					else
					{
						letterOnly = cellState.Value;
						cel.Style.ForeColor = System.Drawing.Color.Black;
					}
					cel.Value = letterOnly;
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

		// New crossword scan and fill grid

		private void BtnImport_Click(object sender, EventArgs e) // scan new crossword
		{
			if (Application.OpenForms["ImageDisplayForm"] == null)
			{
				RecordUpdate();
				ticksDraw = false;
				ImageDisplayForm imageDisplayForm = new ImageDisplayForm(this);
				imageDisplayForm.ShowDialog();
				ticksDraw = true;
			}
			else
			{
				ImageDisplayForm imageDisplayForm = Application.OpenForms.OfType<ImageDisplayForm>().FirstOrDefault();
				imageDisplayForm.Close();
			}
		}
		public void BtnAdd() // Add new scan
		{
			CrossWordInfo crosswordInfoForm = new CrossWordInfo();
			Random random = new Random();
			int randomNumber = random.Next(0, 1000000);
			crosswordInfoForm.TbTitle.Text = "Crossword";
			crosswordInfoForm.TbVersion.Text = "1.0";
			crosswordInfoForm.TbCopyright.Text = DateTime.Now.Year.ToString();
			crosswordInfoForm.TbReference.Text = randomNumber.ToString("D6");
			crosswordInfoForm.ShowDialog();
			string refTitle = crosswordInfoForm.TbTitle.Text.Replace(";", "") + " No. " + crosswordInfoForm.TbReference.Text;
			string info = refTitle + ";" +
						crosswordInfoForm.TbAuthor.Text.Replace(";", "") + ";" +
						crosswordInfoForm.TbVersion.Text.Replace(";", "") + ";" +
						crosswordInfoForm.TbCopyright.Text.Replace(";", "");
			string acrossClues = "";
			string downClues = "";
			if (TbRichTextAcrossClues.Visible == true)
			{
				acrossClues = TbRichTextAcrossClues.Text;
				acrossClues = Regex.Replace(acrossClues, " +", " ");
				acrossClues = acrossClues.Replace("\n", "");
				acrossClues = acrossClues.Replace("\u200B​​\u200B", "\u200B");
				acrossClues = acrossClues.Replace("\u200B", "\r\n\u200B");
				downClues = TbRichTextDownClues.Text;
				downClues = Regex.Replace(downClues, " +", " ");
				downClues = downClues.Replace("\n", "");
				downClues = downClues.Replace("\u200B\u200B", "\u200B");
				downClues = downClues.Replace("\u200B", "\r\n\u200B");
				// Console.WriteLine(string.Join(" ", acrossClues.Select(c => ((int)c).ToString("X4"))));
			}

			var newRecord = new CellDataRecord(rowCnt, colCnt)
			{
				Index = dataManager.TotalCount + 1,
				Reference = info,
				GridSize = rowCnt,
				CluesAcross = acrossClues,
				CluesDown = downClues,
				HintsAcross = "",
				HintsDown = "",
			};
			// Call recordUpdate instead?
			PopulateCellData(newRecord); //new so no data
			dataManager.AddRecord(newRecord);
			dataAdded = true;
			cellsWithBorders.Clear();
			LoadCurrentRecord(); //re-load new
			if (TbRichTextAcrossClues.Visible == false)
			{
				LoadCluesNew(fileCWPathName);
			}
			//NewLoadClose(); // Saves JSON
		}
		public void LoadImageAndFillCells(System.Drawing.Point topLeft, System.Drawing.Point bottomRight, Bitmap bitmap, int rgbValue)
		{
			CrosswordGridSetup(rowCnt, colCnt);
			// Check if corners are valid
			if (topLeft.IsEmpty || bottomRight.IsEmpty)
			{
				MessageBox.Show("Please select the corners of the grid.");
				return;
			}
			// Calculate the width and height of the selected area
			int gridWidth = bottomRight.X - topLeft.X;
			int gridHeight = bottomRight.Y - topLeft.Y;
			// Calculate the dimensions of each cell in the grid
			int cellWidth = gridWidth / (colCnt - 1);
			int cellHeight = gridHeight / (rowCnt - 1);
			// int centerXX = topLeft.X + cellWidth / 2;
			// int centerYY = topLeft.Y + cellHeight / 2;
			int centerXX = topLeft.X;
			int centerYY = topLeft.Y;
			//int rgbValueTemp = 5263440;
			System.Drawing.Color grayColor = System.Drawing.Color.FromArgb(rgbValue, rgbValue, rgbValue);
			byte red = grayColor.R;
			byte green = grayColor.G;
			byte blue = grayColor.B;
			for (int i = 0; i < rowCnt; i++)
			{
				for (int j = 0; j < colCnt; j++)
				{
					// Calculate the center pixel position for the cell
					int centerX = centerXX + j * cellWidth;
					int centerY = centerYY + i * cellHeight;
					// Get the pixel color at the center
					if (centerX < bitmap.Width && centerY < bitmap.Height)
					{
						System.Drawing.Color pixelColor = bitmap.GetPixel(centerX, centerY);
						// Check if the pixel is black (adjust threshold for your needs)
						if (pixelColor.R < red && pixelColor.G < green && pixelColor.B < blue)
						{
							dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Black;
						}
						else
						{
							dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White; // Reset to white if not black
						}
						dataGridView1.Rows[i].Cells[j].Value = "";
					}
				}
			}
			ClearAllBorders();
			AddClueNos(rowCnt, colCnt);

		}

		// Puzzle file diagnostic

		private void PuzUtility()
		{
			//ImportSelect importSelect = new ImportSelect(this, fileCWPathName);
			CrosswordForm puzForm = new CrosswordForm(this);
			puzForm.Show();
		}

		//Delete crossword

		private void DeleteCrossword()
		{
			if (tbCWNo.Text == "" || tbTotalCW.Text == "1")
			{
				MessageBox.Show("No crossword or only one");
				return;
			}
			string json = ReadJsonFromZip(fileCWPathNameCurrent);
			var items1 = JsonConvert.DeserializeObject<List<CellDataRecord>>(json);
			int destCurItem = int.Parse(tbCWNo.Text);
			var itemsDict = items1.ToDictionary(item => item.Index);
			itemsDict.Remove(destCurItem);
			List<CellDataRecord> mergedItems = itemsDict.Values.ToList();
			for (int i = 0; i < mergedItems.Count; i++)
			{
				mergedItems[i].Index = i + 1; // Reassign indices starting from 1
			}
			string mergedJson = JsonConvert.SerializeObject(mergedItems, Formatting.Indented);
			UpdateJsonEntry(fileCWPathNameCurrent, mergedJson);
			RemoveZipEntry(fileCWPathNameCurrent, tbCWNo.Text);
			// MessageBox.Show("Deleted");
			if (destCurItem == 1)
			{
				tbCWNo.Text = "1";
			}
			else
			{
				tbCWNo.Text = (destCurItem - 1).ToString();
			}
			LoadData(fileCWPathNameCurrent);
			ChangeCrossword();
		}
		public void RemoveZipEntry(string zipFilePath, string fileToRemove)
		{
			string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			string tempNewZipPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".cjz");
			try
			{
				Directory.CreateDirectory(tempDir);
				ZipFile.ExtractToDirectory(zipFilePath, tempDir);
				// Create a new zip file.
				using (var newZipStream = new FileStream(tempNewZipPath, FileMode.Create))
				using (var newArchive = new ZipArchive(newZipStream, ZipArchiveMode.Create))
				{
					bool jsonAdded = false;
					var filesInTempDir = Directory.GetFiles(tempDir);
					foreach (var fileInfo in filesInTempDir)
					{
						string fn = Path.GetFileName(fileInfo);
						if (fn.Contains("json") == false)
						{
							int spc = fn.IndexOf(" ");
							int fileIdx = int.Parse(fn.Substring(0, spc).Trim(' '));
							if (fileIdx < int.Parse(fileToRemove))
							{
								newArchive.CreateEntryFromFile(fileInfo, fn, CompressionLevel.Optimal);
							}
							else if (fileIdx > int.Parse(fileToRemove))
							{
								string newfile = (fileIdx - 1).ToString() + fn.Substring(spc, 10);
								newArchive.CreateEntryFromFile(fileInfo, newfile, CompressionLevel.Optimal);
							}
						}
						else if (jsonAdded == false)
						{
							newArchive.CreateEntryFromFile(fileInfo, fn);
							jsonAdded = true;
						}
					}
				}
				File.Delete(zipFilePath);
				File.Move(tempNewZipPath, zipFilePath);
			}
			finally
			{
				// Clean up temporary files and directories.
				if (Directory.Exists(tempDir))
				{
					Directory.Delete(tempDir, true);
				}
				if (File.Exists(tempNewZipPath))
				{
					File.Delete(tempNewZipPath);
				}
			}
		}

		// Application Close

		private void bClose_Click(object sender, EventArgs e)
		{
			this.Close();
			//Application.Exit();
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			NewLoadClose();
			userSettings.LastCrossword = tbCWNo.Text;
			userSettings.LastJSON = fileCWPathName;
			SettingsManager.SaveSettings(userSettings);
		}

		// Solution and Hints

		private void CbSolns_Click(object sender, EventArgs e)
		{
			CbSolns.Checked = !CbSolns.Checked;
		}
		private void CbHints_Click(object sender, EventArgs e)
		{
			CbHints.Checked = !CbHints.Checked;
		}
		public void SolutionDisplay()
		{
			if (CbSolns.Checked == false)
			{
				MessageBox.Show("No solutions available");
				return;
			}
			if (clueNo[selectedRow, selectedCol] != null && clueNo[selectedRow, selectedCol] != "")
			{
				string cNum = clueNo[selectedRow, selectedCol];
				string direction = cNum.Substring(cNum.Length - 1);
				string cNoExtract = new string(cNum.TakeWhile(char.IsDigit).ToArray());
				if (direction == "B")
				{
					ClueExplorer clueExplorerForm = Application.OpenForms.OfType<ClueExplorer>().FirstOrDefault();
					if (clueExplorerForm != null)
					{
						if (clueExplorerForm.RadioAcross.Checked)
						{
							direction = "A";
						}
						else
						{
							direction = "D";
						}
					}
				}
				HighlightClueText(cNoExtract, direction);
				ReadCrossword(direction + "*", selectedRow, selectedCol, dataGridView1);
				SolutionLookUp(direction, selectedRow, selectedCol);
				RemoveCellHighlight(cNum, direction);
				ClearClueTextColour();
			}
		}
		private void SolutionLookUp(string clue, int rowIndex, int colIndex)
		{
			var record = dataManager.GetCurrentRecord();
			string soln = "";
			int i = 0;
			int j = 0;
			for (int k = 0; k < colCnt; k++)
			{
				if (clue.Substring(0, 1) == "D")
				{
					i = 0;
					j = k;
				}
				else
				{
					i = k;
					j = 0;
				}
				// . or # for black, - or "" for blank
				if ((colIndex + i) < colCnt && (rowIndex + j) < colCnt)
				{
					string letter = record.CellData[rowIndex + j, colIndex + i].Solution;
					if (letter == "" || letter == "-")
					{
						soln = "";
						MessageBox.Show("No solution available");
						return;
					}
					if (letter != "#" && letter != ".")
					{
						soln = soln + record.CellData[rowIndex + j, colIndex + i].Solution;
						if (clue.Substring(0, 1) != "D")
						{
							if (GetCellBorder(rowIndex + j, colIndex + i) == 2)
							{
								soln = soln + " ";
							}
						}
						else
						{
							if (GetCellBorder(rowIndex + j, colIndex + i) == 1)
							{
								soln = soln + " ";
							}
						}
					}
					else
					{
						break;
					}
				}
				else
				{
					break;
				}
			}
			MessageBox.Show("Solution: " + soln, "Solution");
		}
		public void HintDisplay()
		{
			if (CbHints.Checked == false)
			{
				MessageBox.Show("No hints available");
				return;
			}
			if (clueNo[selectedRow, selectedCol] != null && clueNo[selectedRow, selectedCol] != "")
			{
				string cNum = clueNo[selectedRow, selectedCol];
				string direction = cNum.Substring(cNum.Length - 1);
				string cNoExtract = new string(cNum.TakeWhile(char.IsDigit).ToArray());
				if (direction == "B")
				{
					ClueExplorer clueExplorerForm = Application.OpenForms.OfType<ClueExplorer>().FirstOrDefault();
					if (clueExplorerForm != null)
					{
						if (clueExplorerForm.RadioAcross.Checked)
						{
							direction = "A";
						}
						else
						{
							direction = "D";
						}
					}
				}
				HighlightClueText(cNoExtract, direction);
				ReadCrossword(direction + "*", selectedRow, selectedCol, dataGridView1);
				HintLookUp(cNum, direction);
				RemoveCellHighlight(cNum, direction);
				//ClearDataGridBackColor();
				ClearClueTextColour();
				//HighlightClueText("", direction);
			}
		}
		private void HintLookUp(string clue, string direction)
		{
			var currentRecord = dataManager.GetCurrentRecord();
			string[] hLines;
			if (direction == "A")
			{
				string hintsA = currentRecord.HintsAcross.ToString();
				hLines = hintsA.Split(new[] { "\r\n" }, StringSplitOptions.None);
			}
			else
			{
				string hintsD = currentRecord.HintsDown.ToString();
				hLines = hintsD.Split(new[] { "\r\n" }, StringSplitOptions.None);
			}
			clue = new string(clue.TakeWhile(char.IsDigit).ToArray());
			clue = clue.PadLeft(2, '0');
			string curLine = "";
			for (int i = 0; i < hLines.Length; i++)
			{
				curLine = hLines[i];
				if (curLine.StartsWith(clue, StringComparison.Ordinal) == true)
				{
					break;
				}
			}
			if (curLine != "")
			{
				MessageBox.Show(curLine, "Hint-Answer");
			}
			else
			{
				MessageBox.Show("No hints available", "Hint-Answer");
			}
		}
		private void DisplaySolutionGrid()
		{
			using (var gridForm = new Form())
			{
				gridForm.Text = "Crossword Solution";
				gridForm.Size = new System.Drawing.Size(ScalePixelValue(360), ScalePixelValue(320));
				gridForm.StartPosition = FormStartPosition.Manual;
				gridForm.SizeGripStyle = SizeGripStyle.Hide;
				gridForm.MinimizeBox = false;
				gridForm.MaximizeBox = false;
				gridForm.AutoScaleMode = AutoScaleMode.Dpi;
				gridForm.Left = this.Left + ScalePixelValue(10);
				gridForm.Top = this.Top + ScalePixelValue(100);
				int colWid = 5 + gridForm.Width / colCnt;
				int rowHt = 5 + gridForm.Height / rowCnt;
				int gridWidth = (colWid * colCnt);
				int gridHeight = (rowHt * rowCnt);
				int borderOffset = 30; // A small padding for form borders
				var record = dataManager.GetCurrentRecord();
				//Font boldFont = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
				Font boldFont = new Font("Arial", 10, System.Drawing.FontStyle.Bold);
				for (int i = 0; i < rowCnt; i++)
				{
					for (int j = 0; j < rowCnt; j++)
					{
						System.Windows.Forms.RichTextBox rtb = new System.Windows.Forms.RichTextBox();
						System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
						panel.Location = new System.Drawing.Point(15 + (j * colWid) - 1, 10 + (i * rowHt) - 1);
						panel.Size = new System.Drawing.Size(colWid + 2, rowHt + 2);
						panel.BorderStyle = BorderStyle.FixedSingle;
						rtb.Dock = DockStyle.Fill;
						rtb.Location = new System.Drawing.Point(15 + (j * colWid), 10 + (i * rowHt));
						rtb.Size = new System.Drawing.Size(colWid, rowHt);
						rtb.Font = boldFont;
						rtb.Multiline = false;
						rtb.WordWrap = false;
						//rtb.ScrollBars = ScrollBars.None;
						rtb.BorderStyle = BorderStyle.None;
						rtb.Click += (sender, e) => gridForm.Close();
						rtb.BringToFront();
						CellState cell = record.CellData[i, j];
						if (cell.Value == "#")
						{
							rtb.BackColor = System.Drawing.Color.Black;
						}
						else if (cell.Solution == " " || cell.Solution == "-")
						{

							rtb.Text = "";
						}
						else
						{
							if (clueNo[i, j] != null)
							{
								rtb.Text = clueNo[i, j].Substring(0, 2).Trim().PadRight(3) + cell.Solution;
								rtb.Select(0, 3);
								rtb.SelectionColor = System.Drawing.Color.Red;
								rtb.SelectionFont = new Font("Arial", 8, System.Drawing.FontStyle.Bold);
								rtb.SelectionCharOffset = 10;                                                          //rtb.SelectionFont.Size = 10;
								rtb.Select(4, 1);
								rtb.SelectionCharOffset = -10;
								//rtb.SelectionAlignment = HorizontalAlignment.Center;
								rtb.SelectionColor = System.Drawing.Color.Black;
								rtb.SelectionFont = new Font("Arial", 10, System.Drawing.FontStyle.Bold);
							}
							else
							{
								rtb.Text = cell.Solution;
								rtb.SelectAll();
								rtb.SelectionCharOffset = -8;
								rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
							}
						}
						rtb.DeselectAll();
						panel.Controls.Add(rtb);
						gridForm.Controls.Add(panel);
					}
				}
				gridForm.ClientSize = new System.Drawing.Size(gridWidth + borderOffset, gridHeight + borderOffset);
				gridForm.ShowDialog();
			}
		}

		// Clues List PNG Save and Load

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
							// Image.FromStream requires a seekable stream.
							// Copy the stream to a MemoryStream to make it seekable.
							MemoryStream memoryStream = new MemoryStream();
							stream.CopyTo(memoryStream);
							memoryStream.Position = 0;
							if (capturedCluesImage != null)
							{
								capturedCluesImage.Dispose();
							}
							capturedCluesImage = (Bitmap)Image.FromStream(memoryStream);
							//_scaleX = (float)PictureBoxClues.ClientSize.Width / capturedCluesImage.Width;
							//_scaleY = (float)PictureBoxClues.ClientSize.Height / capturedCluesImage.Height;
							//PictureBoxClues.Image = new Bitmap(capturedCluesImage);
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
		private void PictureBoxClues_PaintClues(object sender, PaintEventArgs e)
		{
			if (capturedCluesImage == null) return;
			if (capturedCluesImage.Width == 0 || capturedCluesImage.Height == 0) return;
			// Calculate the aspect ratio and destination rectangle to fit the image.
			float imageAspect = (float)capturedCluesImage.Width / capturedCluesImage.Height;
			PictureBox pb = (PictureBox)sender;
			float boxAspect = (float)pb.Width / pb.Height;
			Rectangle destRect;

			if (boxAspect > imageAspect)
			{
				// PictureBox is wider, so fit by height.
				int newWidth = (int)(pb.Height * imageAspect);
				int xOffset = (pb.Width - newWidth) / 2;
				destRect = new Rectangle(xOffset, 0, newWidth, pb.Height);
			}
			else
			{
				// PictureBox is taller or has the same aspect ratio, so fit by width.
				int newHeight = (int)(pb.Width / imageAspect);
				int yOffset = 0;
				destRect = new Rectangle(0, yOffset, pb.Width, newHeight);
			}
			_scaleX = (float)pb.Width / capturedCluesImage.Width;
			_scaleY = (float)pb.Height / capturedCluesImage.Height;

			DrawClues(e.Graphics, destRect);
		}
		private void DrawClues(Graphics g, Rectangle destRect)
		{
			// Use high-quality interpolation to prevent blurriness during scaling.
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
			g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

			// Draw the image onto the canvas with the high-quality settings.
			g.DrawImage(capturedCluesImage, destRect);

			// Any other custom drawing (e.g., text, circles) goes here.
			// NOTE: You must also scale these drawing coordinates if necessary.
			// For example: g.DrawRectangle(Pens.Red, scaledX, scaledY, scaledWidth, scaledHeight);
		}
		public void RefreshPictureBoxClues()
		{
			PictureBoxClues.Invalidate();
		}
		public void DeleteFileFromZip(string zipFilePath, string fileNameToDelete)
		{
			using (FileStream zipToOpen = new FileStream(zipFilePath, FileMode.Open))
			{
				using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
				{
					ZipArchiveEntry entryToDelete = archive.GetEntry(fileNameToDelete);

					if (entryToDelete != null)
					{
						entryToDelete.Delete();
					}
					else
					{
						// Console.WriteLine($"File '{fileNameToDelete}' not found in '{zipFilePath}'.");
					}
				}
			}
		}
		public void RenameFileFromZip(string zipFilePath, string oldFileName, string newFileName)
		{
			using (ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Update))
			{
				ZipArchiveEntry originalEntry = archive.GetEntry(oldFileName);
				ZipArchiveEntry newEntry = archive.CreateEntry(newFileName);
				using (Stream originalStream = originalEntry.Open())
				using (Stream newStream = newEntry.Open())
				{
					originalStream.CopyTo(newStream);
				}
				originalEntry.Delete();
			}
		}

		// Adding Ticks

		private void PictureBoxClues_MouseClick(object sender, MouseEventArgs e)
		{
			if (ModifierKeys == Keys.Shift)
			{
				// Convert mouse coordinates to original image coordinates
				System.Drawing.Point originalPoint = GetOriginalImagePoint(e.Location);

				// Check if a line was clicked in any of the lists in the dictionary
				foreach (var kvp in _linesDictionary)
				{
					List<Line> lines = kvp.Value;
					for (int i = lines.Count - 1; i >= 0; i--)
					{
						if (lines[i].Contains(originalPoint))
						{
							lines.RemoveAt(i); // Remove the line if clicked
											   // SaveLinesToJson(); // Save the updated state to JSON
							PictureBoxClues.Invalidate();
							return; // Exit the method
						}
					}
				}
				// If no line was clicked, add a new line with midpoint at the original click location
				// For example, if you want to add it to a specific CWNo, specify it here
				string currentCWNo = tbCWNo.Text;
				if (!_linesDictionary.ContainsKey(currentCWNo))
				{
					_linesDictionary[currentCWNo] = new List<Line>();
				}
				_linesDictionary[currentCWNo].Add(new Line(originalPoint, currentCWNo));
				// SaveLinesToJson(); // Save the new line to JSON
				PictureBoxClues.Invalidate();
			}
		}
		private void PictureBoxClues_PaintTicks(object sender, PaintEventArgs e)
		{
			// Draw the original image stretched to fit the PictureBox
			//if (PictureBoxClues.Image != null)
			//{
			//	e.Graphics.DrawImage(PictureBoxClues.Image, 0, 0, PictureBoxClues.ClientSize.Width, PictureBoxClues.ClientSize.Height);
			//}
			// _scaleX = (float)1.08902073;
			// _scaleY = (float)1.027088;
			_scaleX = (float)1.0;
			_scaleY = (float)1.0;
			if (ticksDraw == true)
			{
				// Draw the lines on top of the stretched image
				foreach (var kvp in _linesDictionary)
				{
					foreach (var line in kvp.Value)
					{
						line.Draw(e.Graphics, _scaleX, _scaleY);
					}
				}
			}
		}
		private class Line
		{
			private System.Drawing.Point _midPoint;
			private int _length = 12;
			private int _thickness = 3;
			private System.Drawing.Color _color = System.Drawing.Color.Red;
			private string _cwNo;  // CWNo identifier

			public Line(System.Drawing.Point midPoint, string cwNo)
			{
				_midPoint = midPoint;
				_cwNo = cwNo;
			}

			// Public properties for accessing private fields
			public System.Drawing.Point MidPoint => _midPoint;
			public string CWNo => _cwNo;
			// Getter for CWNo

			public void Draw(Graphics g, float scaleX, float scaleY)
			{
				using (var pen = new Pen(_color, _thickness))
				{
					// Calculate the start and end points based on the midpoint
					var start = new System.Drawing.Point((int)((MidPoint.X - _length / 2) * scaleX), (int)((MidPoint.Y + _length / 2) * scaleY));
					var end = new System.Drawing.Point((int)((MidPoint.X + _length / 2) * scaleX), (int)((MidPoint.Y - _length / 2) * scaleY));
					g.DrawLine(pen, start, end);
				}
			}
			public bool Contains(System.Drawing.Point p)
			{
				// Calculate the start and end points based on the midpoint
				var start = new System.Drawing.Point(MidPoint.X - _length / 2, MidPoint.Y + _length / 2);
				var end = new System.Drawing.Point(MidPoint.X + _length / 2, MidPoint.Y - _length / 2);

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
		private System.Drawing.Point GetOriginalImagePoint(System.Drawing.Point p)
		{
			// Convert mouse coordinates to original image coordinates
			int originalX = (int)(p.X / _scaleX);
			int originalY = (int)(p.Y / _scaleY);
			return new System.Drawing.Point(originalX, originalY);
		}

		// JSON and Clues Export

		private void ExportJSON()
		{
			{
				ExportCrossword newForm = new ExportCrossword();
				//Location originalSize = new Location(this.Width, this.Height);
				//Size scaledSize = this.LogicalToDeviceUnits(originalSize);
				newForm.Top = this.Top + 20 * this.DeviceDpi / 96;
				newForm.Left = this.Left + 200 * this.DeviceDpi / 96;
				newForm.TbCurCW1.Text = tbCWNo.Text;
				newForm.TBCurCW2.Text = tbCWNo.Text;
				newForm.TbEnd1.Text = tbTotalCW.Text;
				newForm.TBEnd2.Text = tbTotalCW.Text;
				newForm.AutoScaleMode = AutoScaleMode.Dpi;
				newForm.ShowDialog();
				if (newForm.DialogResult == DialogResult.Cancel)
				{
					return;
				}
				int startIndex = 1;
				int endIndex = 1;
				if (newForm.RadioCurrent.Checked == true)
				{
					startIndex = int.Parse(newForm.TbCurCW1.Text);
					endIndex = int.Parse(newForm.TbCurCW1.Text);
				}
				else if (newForm.RadioAll.Checked == true)
				{
					startIndex = 1;
					endIndex = int.Parse(newForm.TbEnd1.Text);
				}
				else if (newForm.RadioFromCurrent.Checked == true)
				{
					startIndex = int.Parse(newForm.TbCurCW1.Text);
					endIndex = int.Parse(newForm.TBEnd2.Text);
				}
				else if (newForm.RadioCustom.Checked == true)
				{
					startIndex = int.Parse(newForm.TBCustomStart.Text);
					endIndex = int.Parse(newForm.TBCustomEnd.Text);
				}
				if (newForm.CheckBoxIPUZ.Checked == true || newForm.CheckBoxPUZ.Checked == true)
				{
					if (TbRichTextAcrossClues.Visible == false)
					{
						MessageBox.Show("Cannot Export to puz or ipuz if clues list is text");
					}
					else
					{
						CrosswordPuzzle puzzle = new CrosswordPuzzle();
						JSONToPuzzle(puzzle, newForm.CheckBoxPUZ.Checked);
						ImportSelect impSelect = new ImportSelect(this, fileCWPathName);
						//CrosswordForm crosswordForm = new CrosswordForm(this, impSelect);
						if (newForm.CheckBoxPUZ.Checked != true)
						{

							SaveIPUZ_PUZ(puzzle, "Crossword.ipuz");
						}
						else
						{
							SaveIPUZ_PUZ(puzzle, "Crossword.puz");
						}
						return;
					}
				}
				string json = ReadJsonFromZip(fileCWPathName);
				List<CellDataRecord> records = JsonConvert.DeserializeObject<List<CellDataRecord>>(json);
				var subset = records.Where(record => record.Index >= startIndex && record.Index <= endIndex).ToList();
				for (int i = 0; i < subset.Count; i++)
				{
					subset[i].Index = i + 1;
				}
				string newJ = JsonConvert.SerializeObject(subset, Formatting.Indented);
				JSONZIPSave(newJ, startIndex, endIndex);
			}
		}
		public async void SaveIPUZ_PUZ(CrosswordPuzzle puzzle, string filename)
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Filter = filename.Contains(".ipuz") ? "ipuz files (*.ipuz)|*.ipuz" :
																"Across Lite Puzzles (*.puz)|*.puz";
				saveFileDialog.FileName = filename;
				saveFileDialog.InitialDirectory = downloadsPath;
				saveFileDialog.RestoreDirectory = true;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					if (filename.Contains(".ipuz"))
					{
						var saver = new IpuzFileSaver();
						// Use the shared jsonOptions object
						await saver.Save(puzzle, saveFileDialog.FileName, jsonOptions);
					}
					else
					{
						var saver = new PuzFileSaver();
						await saver.Save(puzzle, saveFileDialog.FileName);
					}
					MessageBox.Show("Puzzle saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}
		public void JSONToPuzzle(CrosswordPuzzle puzzle, bool puz)
		{
			// dataManager.CurrentIndex = int.Parse(tbCWNo.Text);
			var record = dataManager.GetCurrentRecord();
			rowCnt = record.GridSize;
			colCnt = rowCnt;
			puzzle.Kind = new List<string> { "http://ipuz.org/crossword" };
			string[] info = record.Reference.ToString().Split(';');
			string[] paddedData = info.Take(4)
				 .Concat(Enumerable.Repeat(string.Empty, 4))
				 .Take(4)
				 .ToArray();
			puzzle.Title = paddedData[0]; //Reference
			puzzle.Version = paddedData[1];
			puzzle.Author = paddedData[2];
			puzzle.Copyright = paddedData[3];
			puzzle.Puzzle = null;
			puzzle.Dimensions = new Dimensions();
			puzzle.Dimensions.Height = rowCnt;
			puzzle.Dimensions.Width = rowCnt;
			//if CbHints is true
			puzzle.Notes = record.HintsAcross + Environment.NewLine + record.HintsDown;
			List<Clue> cluesAcross = new List<Clue>();
			List<Clue> cluesDown = new List<Clue>();
			cluesAcross = ReadCluesList(TbRichTextAcrossClues, record.CluesAcross);
			cluesDown = ReadCluesList(TbRichTextDownClues, record.CluesDown);
			puzzle.Clues = new Clues();
			puzzle.Clues.Across = cluesAcross;
			puzzle.Clues.Down = cluesDown;
			if (puz == false) //i.e. ipuz
			{
				if (puzzle.Solution == null)
				{
					puzzle.Solution = new List<List<string>>(puzzle.Dimensions.Height);
					puzzle.UserAmswers = new List<List<string>>(puzzle.Dimensions.Height);
				}
				for (int r = 0; r < rowCnt; r++)
				{
					List<string> rowList = new List<string>(puzzle.Dimensions.Width);
					List<string> rowListUser = new List<string>(puzzle.Dimensions.Width);
					for (int c = 0; c < colCnt; c++)
					{
						// if CbSolution is true
						string cellValue = record.CellData[r, c].Solution;
						// if CbAnswers is true
						string cellValueUser = record.CellData[r, c].Value;
						if (record.CellData[r, c].Value == "#")
						{
							cellValue = "#";
						}
						rowList.Add(cellValue);
						rowListUser.Add(cellValueUser);
					}
					puzzle.Solution.Add(rowList);
					puzzle.UserAmswers.Add(rowListUser);
				}
			}
			else
			{
				puzzle.Solution = null;
				puzzle.UserAmswers = null;
				puzzle.PuzSolutionGrid = new char[rowCnt, colCnt];
				puzzle.PuzUserGrid = new char[rowCnt, colCnt];
				for (int r = 0; r < rowCnt; r++)
				{
					for (int c = 0; c < colCnt; c++)
					{
						if (record.CellData[r, c].Value == "#")
						{
							puzzle.PuzUserGrid[r, c] = '.';
							puzzle.PuzSolutionGrid[r, c] = '.';
						}
						else
						{
							string ans = record.CellData[r, c].Value;
							string soln = record.CellData[r, c].Solution;
							if (string.IsNullOrEmpty(soln))
							{
								puzzle.PuzSolutionGrid[r, c] = '-';
							}
							else
							{
								// if CbSolution is true otherwise '-'
								puzzle.PuzSolutionGrid[r, c] = soln[0];
							}
							if (string.IsNullOrEmpty(ans))
							{
								puzzle.PuzUserGrid[r, c] = '-';
							}
							else
							{
								// if CbAnswer is true otherwise '-'
								puzzle.PuzUserGrid[r, c] = ans[0];
							}
						}
					}
				}
			}
		}
		public List<Clue> ReadCluesList(RichTextBox rtb, string lines)
		{
			// string[] items = lines.Split("\r\n");
			string[] items = lines.Split(new[] { "\r\n" }, StringSplitOptions.None);
			// string[] items = Regex.Split(lines, @"\r\n"); \\ ignore empty entries (common when separators may appear consecutively
			List<Clue> clueList = new List<Clue>();
			string clueC = "";
			int lnNum;
			bool found = false;
			string lnStr = "";
			for (int i = 0; i < items.Length; i++)
			{
				string lineText = items[i];
				lineText = lineText.Replace("\u200B", "");
				lineText = Regex.Replace(lineText, " +", " ");
				lnStr = new string(lineText.TakeWhile(char.IsDigit).ToArray());
				if (int.TryParse(lnStr, out lnNum))
				{
					if (found == false)
					{
						clueC = lineText;
						found = true;
					}
					else
					{
						lnStr = new string(clueC.TakeWhile(char.IsDigit).ToArray());
						clueC = clueC.Substring(clueC.IndexOf(" ") + 1);
						clueList.Add(new Clue { Number = lnStr, Text = clueC });
						clueC = lineText;
					}
				}
				else
				{
					clueC = clueC + lineText;
				}
				if (i == items.Length - 1)
				{
					lnStr = new string(clueC.TakeWhile(char.IsDigit).ToArray());
					clueC = clueC.Substring(clueC.IndexOf(" ") + 1);
					clueList.Add(new Clue { Number = lnStr, Text = clueC });
				}
			}
			return clueList;
		}
		public void JSONZIPSave(string json, int start, int end)
		{
			string fn = "CWData" + start.ToString() + "-" + end.ToString();
			string defaultfnJSON = fn + ".cjz";
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Filter = "Crossword files (*.cjz)|*.cjz|All files (*.*)|*.*"; // File type filter
				saveFileDialog.Title = "Save Crossword As";
				saveFileDialog.FileName = defaultfnJSON;
				saveFileDialog.InitialDirectory = downloadsPath;
				saveFileDialog.RestoreDirectory = true;

				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					string filePathName = saveFileDialog.FileName;
					try
					{
						if (fileCWPathNameCurrent == filePathName)
						{
							MessageBox.Show("Cannot export to current file");
							return;
						}
						string jsonFile = Path.GetFileName(filePathName).Replace(".cjz", ".json");
						CreateNewFileWithJson(filePathName, jsonFile, json);
						ExportClues(start, end, fileCWPathName, filePathName);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"An error occurred: {ex.Message}");
					}
				}
				else
				{
					// cancelled
				}
			}
		}
		private void CreateNewFileWithJson(string filePathName, string jsonEntryName, string serializedJson)
		{
			using (var fileStream = new FileStream(filePathName, FileMode.Create))
			using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
			{
				// var serializedJson = JsonConvert.SerializeObject(data, Formatting.Indented);
				var jsonEntry = archive.CreateEntry(jsonEntryName);

				using (var entryStream = jsonEntry.Open())
				using (var streamWriter = new StreamWriter(entryStream, Encoding.UTF8))
				{
					streamWriter.Write(serializedJson);
				}
			}
			// Console.WriteLine($"Created new file '{filePathName}' with JSON entry '{jsonEntryName}'.");
		}
		private void ExportClues(int first, int last, string sourceZipPath, string destinationZipPath)
		{
			string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
			Directory.CreateDirectory(tempDir);
			try
			{
				// Extract the existing ZIP file
				ZipFile.ExtractToDirectory(sourceZipPath, tempDir);
				using (FileStream zipToOpen = new FileStream(destinationZipPath, FileMode.OpenOrCreate))
				using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
				{
					var files = Directory.GetFiles(tempDir);
					for (int k = first; k <= last; k++)
					{
						string fnPNG = k.ToString() + " clues.png";
						foreach (var file in files)
						{
							string fName = Path.GetFileName(file);
							if (fName == fnPNG)
							{
								int m = k - first + 1;
								string newFn = m.ToString() + " clues.png";
								archive.CreateEntryFromFile(file, newFn);
								break;
							}
						}
					}
				}
			}
			finally
			{
				Directory.Delete(tempDir, true);
			}
		}

		// Clue Explorer

		private void BtnLoadExplorer_Click(object sender, EventArgs e)
		{
			if (Application.OpenForms["ClueExplorer"] == null)
			{
				Screen currentScreen = Screen.FromControl(this);
				ClueExplorer clueExplorerForm = new ClueExplorer(this);
				clueExplorerForm.Top = this.Top + this.Height;
				clueExplorerForm.Left = this.Left + 300 * this.DeviceDpi / 96;
				if (clueExplorerForm.Bottom > currentScreen.WorkingArea.Bottom)
				{
					clueExplorerForm.Top = currentScreen.WorkingArea.Bottom - clueExplorerForm.Height;
				}
				//float scaleX = (float)this.DeviceDpi / 96;
				//float scaleY = (float)this.DeviceDpi / 96;
				//clueExplorerForm.Scale(new SizeF(scaleX, scaleY));

				clueExplorerForm.Show();
			}
			else
			{
				ClueExplorer clueExplorerForm = Application.OpenForms.OfType<ClueExplorer>().FirstOrDefault();
				clueExplorerForm.Close();
				BtnLoadExplorer_Click(BtnLoadExplorer, EventArgs.Empty);
				//BtnLoadExplorer.PerformClick();
			}
		}
		public string ReadCrossword(string direction, int rowIndex, int colIndex, DataGridView dgv)
		{
			string ans = "";
			string clueNoStr = clueNo[rowIndex, colIndex].Replace(" ", "").Replace("*", "");
			clueNoStr = clueNoStr.Substring(0, clueNoStr.Length - 1);
			int noOfLetters = clueData[clueNoStr + direction.Replace("*", "")].WordLength;
			bool down = true;
			bool across = true;
			down = direction.Contains("A") ? false : true;
			across = direction.Contains("A") ? true : false;
			for (int k = 0; k < noOfLetters; k++)
			{
				DataGridViewCell cel = dgv.Rows[rowIndex + k * Convert.ToInt32(down)].Cells[colIndex + k * Convert.ToInt32(across)];
				if (cel.Value == null || cel.Value.ToString() == "")
				{
					ans = ans + "_";
				}
				else
				{
					ans = ans + cel.Value.ToString();
				}
				if (GetCellBorder(rowIndex + k * Convert.ToInt32(down), colIndex + k * Convert.ToInt32(across)) == 1 + Convert.ToInt32(across))
				{
					ans = ans + "/";
				}
				if (direction.Substring(direction.Length - 1) == "*")
				{
					cel.Style.BackColor = System.Drawing.Color.LightGreen;
				}
				else
				{
					cel.Style.BackColor = System.Drawing.Color.White;
				}
			}
			return ans;
		}

		// Clues List and Clue Numbers

		public void AddClueNos(int rowCnt, int colCnt)
		{
			clueNo = new string[rowCnt, colCnt];
			int num = 1;
			string sNum = "";
			DataGridViewCell startpos = null;
			int k = 0;
			clueData.Clear();
			for (int j = 0; j < rowCnt; j++)
			{
				for (int i = 0; i < colCnt; i++)
				{
					DataGridViewCell cel = dataGridView1.Rows[j].Cells[i];
					if (cel.Style.BackColor != System.Drawing.Color.Black)
					{
						if ((i == 0 || (i > 0 && dataGridView1.Rows[j].Cells[i - 1].Style.BackColor == System.Drawing.Color.Black)) && i != (rowCnt - 1) && (i < (rowCnt - 1) && dataGridView1.Rows[j].Cells[i + 1].Style.BackColor != System.Drawing.Color.Black))
						{
							sNum = "A";
							bool isSolved = true;
							clueData.Add(num.ToString() + "A", new ClueData { Solved = isSolved });
							k = 0;
							while (i + k < colCnt && dataGridView1.Rows[j].Cells[i + k].Style.BackColor != System.Drawing.Color.Black)
							{
								object chk = dataGridView1.Rows[j].Cells[i + k].Value;
								if (chk == null || string.IsNullOrEmpty(chk.ToString()))
								{
									isSolved = false;
									clueData[num.ToString() + "A"].Solved = isSolved;
								}
								k++;
							}
							clueData[num.ToString() + "A"].WordLength = k;
						}
						if ((j == 0 || (j > 0 && dataGridView1.Rows[j - 1].Cells[i].Style.BackColor == System.Drawing.Color.Black)) && j != (colCnt - 1) && (j < (colCnt - 1) && dataGridView1.Rows[j + 1].Cells[i].Style.BackColor != System.Drawing.Color.Black))
						{
							bool isSolved = true;
							clueData.Add(num.ToString() + "D", new ClueData { Solved = isSolved });
							sNum = sNum + "D";
							k = 0;
							while (j + k < rowCnt && dataGridView1.Rows[j + k].Cells[i].Style.BackColor != System.Drawing.Color.Black)
							{
								object chk = dataGridView1.Rows[j + k].Cells[i].Value;
								if (chk == null || string.IsNullOrEmpty(chk.ToString()))
								{
									isSolved = false;
									clueData[num.ToString() + "D"].Solved = isSolved;
								}
								k++;
							}
							clueData[num.ToString() + "D"].WordLength = k;
						}
						if (sNum == "AD")
						{
							sNum = "B";
						}
						if (sNum != "")
						{
							sNum = num.ToString() + "  " + sNum;
							clueNo[j, i] = sNum;
							num = num + 1;
							sNum = "";
						}
						if (num == 2 && startpos == null)
						{
							startpos = cel;
						}
					}
				}
			}
			// ClueLines(TbRichTextAcrossClues);
			// ClueLines(TbRichTextDownClues);
			// UpdateClueStatus();
			dataGridView1.Focus();
			//dataGridView1.ClearSelection();
			dataGridView1.CurrentCell = startpos;
			//dataGridView1.Invalidate();
		}
		public void tbSolnStatus_MouseClick(object sender, MouseEventArgs e)
		{
			updateSolnStatus();
		}
		public void updateSolnStatus()
		{
			int k = 0;
			string cNum;
			for (int j = 0; j < rowCnt; j++)
			{
				for (int i = 0; i < colCnt; i++)
				{
					if (clueNo[j, i] != null && clueNo[j, i] != "")
					{
						cNum = clueNo[j, i];
						if (cNum.Contains("A") || cNum.Contains("B"))
						{
							string sNum = cNum.Replace("B", "A");
							bool isSolved = true;
							k = 0;
							while (i + k < colCnt && dataGridView1.Rows[j].Cells[i + k].Style.BackColor != System.Drawing.Color.Black)
							{
								object chk = dataGridView1.Rows[j].Cells[i + k].Value;
								if (chk == null || string.IsNullOrEmpty(chk.ToString()))
								{
									isSolved = false;
									break;
								}
								k++;
							}
							clueData[sNum.Replace(" ", "").Replace("*", "")].Solved = isSolved;
						}
						if (cNum.Contains("D") || cNum.Contains("B"))
						{
							string sNum = cNum.Replace("B", "D");
							bool isSolved = true;
							k = 0;
							while (j + k < rowCnt && dataGridView1.Rows[j + k].Cells[i].Style.BackColor != System.Drawing.Color.Black)
							{
								object chk = dataGridView1.Rows[j + k].Cells[i].Value;
								if (chk == null || string.IsNullOrEmpty(chk.ToString()))
								{
									isSolved = false;
									break;
								}
								k++;
							}
							clueData[sNum.Replace(" ", "").Replace("*", "")].Solved = isSolved;
						}
					}
				}
			}
			UpdateClueStatus();
		}
		public void UpdateClueStatus()
		{
			int acrossSolved = clueData.Count(kvp => kvp.Key.Contains("A") && kvp.Value.Solved == true);
			int downSolved = clueData.Count(kvp => kvp.Key.Contains("D") && kvp.Value.Solved == true);
			if (acrossSolved > 0)
			{
				HighlightSolvedClue(TbRichTextAcrossClues);
			}
			if (downSolved > 0)
			{
				HighlightSolvedClue(TbRichTextDownClues);
			}
			int solved = acrossSolved + downSolved;
			int clueCount = clueData.Count;
			this.tbSolnStatus.Text = solved.ToString() + "/" + clueCount.ToString();
			if (solved == clueCount)
			{
				tbSolnStatus.BackColor = System.Drawing.Color.LightGreen;
			}
			else
			{
				tbSolnStatus.BackColor = System.Drawing.Color.White;
			}
			dataGridView1.Focus();
		}
		public string HighlightClueText(string clueNo, string direction)
		{
			if (TbRichTextAcrossClues.Visible == false)
			{
				return "";
			}
			ClearClueTextColour();
			if (string.IsNullOrEmpty(clueNo))
			{
				return "";
			}
			RichTextBox rtb = TbRichTextAcrossClues;
			if (direction == "D")
			{
				rtb = TbRichTextDownClues;
			}
			int start = clueData[clueNo.Trim() + direction].ClueStart;
			int end = clueData[clueNo.Trim() + direction].ClueLength;
			rtb.Select(start + 2, end - start);
			rtb.SelectionColor = System.Drawing.Color.Red;
			rtb.ScrollToCaret();
			string ln = rtb.Text.Substring(start, end - start);
			ln = Regex.Replace(ln, " +", " ");
			return ln;
		}
		public void ClueLines(RichTextBox rtb)
		{
			string[] items = rtb.Text.Split(new[] { "\u200B" }, StringSplitOptions.None);
			string direction = "A";
			if (rtb.Name.Contains("Down"))
			{
				direction = "D";
			}
			int stPos = 0;
			string lnStr = "";
			int lineLen = 0;
			for (int i = 0; i < items.Length; i++)
			{
				string lineText = items[i];
				if (lineText == "")
				{
					continue;
				}
				lineLen = lineText.Length;
				lnStr = new string(lineText.SkipWhile(c => c == '\u200B')
							   .TakeWhile(char.IsDigit)
							   .ToArray());
				if (int.TryParse(lnStr, out int lnNum))
				{
					if (clueData.ContainsKey(lnStr + direction))
					{
						clueData[lnStr + direction].ClueStart = stPos;
						clueData[lnStr + direction].ClueLength = stPos + lineLen;
					}
					else
					{
						clueData.Add(lnStr + direction, new ClueData
						{
							ClueStart = stPos,
							ClueLength = stPos + lineLen,
						});
					}
				}
				stPos = stPos + lineLen + 1;
			}
		}
		public void HighlightSolvedClue(RichTextBox rtb)
		{
			string direction = rtb.Name.Contains("Down") ? "D" : "A";
			rtb.SelectAll();
			rtb.SelectionBackColor = System.Drawing.Color.White;
			foreach (var key in clueData.Keys)
			{
				int start = clueData[key].ClueStart;
				int end = clueData[key].ClueLength;
				rtb.Select(start, end - start);
				if (key.Contains(direction) == true && clueData[key].Solved == true)
				{
					rtb.SelectionBackColor = System.Drawing.Color.LightGreen;
				}
			}
			rtb.SelectionLength = 0;
		}
		public class ClueData
		{
			public bool Solved { get; set; } = false;
			public int ClueStart { get; set; } = 0;
			public int ClueLength { get; set; } = 0;
			public int WordLength { get; set; } = 0;
		}
		private void RemoveCellHighlight(string cNum, string direction)
		{
			int letters = clueData[cNum.ToString().Substring(0, 2).Trim() + direction].WordLength;
			if (direction == "A")
			{
				for (int i = 0; i < letters; i++)
				{
					DataGridViewCell cel = dataGridView1.Rows[selectedRow].Cells[selectedCol + i];
					cel.Style.BackColor = System.Drawing.Color.White;
				}
			}
			else
			{
				for (int i = 0; i < letters; i++)
				{
					DataGridViewCell cel = dataGridView1.Rows[selectedRow + i].Cells[selectedCol];
					cel.Style.BackColor = System.Drawing.Color.White;
				}
			}
		}
		public void ClearDataGridBackColor()
		{
			DataGridView dg = dataGridView1;
			for (int i = 0; i < rowCnt; i++)
			{
				for (int j = 0; j < colCnt; j++)
				{
					DataGridViewCell cel = dg.Rows[i].Cells[j];
					if (cel.Style.BackColor == System.Drawing.Color.LightGreen)
					{
						cel.Style.BackColor = System.Drawing.Color.White;
					}
				}
			}
		}
		public void ClearClueTextColour()
		{
			if (TbRichTextAcrossClues.Visible != false)
			{
				TbRichTextAcrossClues.SelectAll();
				TbRichTextAcrossClues.SelectionColor = System.Drawing.Color.Black;
				TbRichTextDownClues.SelectAll();
				TbRichTextDownClues.SelectionColor = System.Drawing.Color.Black;
			}
		}

		// Add Word Separators

		private void AcrossSeparator_Click(object sender, EventArgs e)
		{
			ToggleCellBorder(dataGridView1.CurrentCell, BorderSide.Right);
		}
		private void DownSeparator_Click(object sender, EventArgs e)
		{
			ToggleCellBorder(dataGridView1.CurrentCell, BorderSide.Bottom);
		}
		private void ClearAllBorders()
		{
			cellsWithBorders.Clear();
			dataGridView1.Invalidate();
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
			// Toggle the border side.
			if (borderList.Contains(side))
			{
				borderList.Remove(side);
				// If no borders are left, remove the key entirely.
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
			dataGridView1.Focus();
		}

		// Grey Text

		private void RadioPen_CheckedChanged(object sender, EventArgs e)
		{
			dataGridView1.Focus();
		}
		private void RadioPencil_CheckedChanged(object sender, EventArgs e)
		{
			dataGridView1.Focus();
		}

		// Word Lookup & Scratchpad

		private void BtnGoogle_Click(object sender, EventArgs e)
		{

			string searchURL = "https://www.google.com/search?q=" + TbWordLookUp.Text.Replace("?", "");
			//Process.Start("chrome.exe", searchURL);
			dataGridView1.Focus();
		}
		private void BtnScratchPad_Click(object sender, EventArgs e)
		{
			int i = 0;
			if (DataGridScratchPad.Height == 0)
			{
				for (i = 0; i < padHeight; i++)
				{
					DataGridScratchPad.Height = i;
				}
				DataGridScratchPad.BringToFront();
				BtnScratchPad.Text = "Hide Scratchpad";
			}
			else
			{
				for (i = padHeight; i >= 0; i--)
				{
					DataGridScratchPad.Height = i;
				}
				BtnScratchPad.Text = "Show Scratchpad";
			}
		}

		// Grid load into JSON

		public void PopulateCellData(CellDataRecord record) //Update
		{
			for (int row = 0; row < rowCnt; row++)
			{
				for (int col = 0; col < colCnt; col++)
				{
					CellState cell = record.CellData[row, col];
					cell.Value = GetCellValue(row, col);
					cell.WordSeparator = GetCellBorder(row, col);
					cell.Notes = "";
				}
			}
		}
		private string GetCellValue(int row, int col)
		{
			var cell = dataGridView1.Rows[row].Cells[col];

			// Check if the cell has a value and convert to string if necessary
			if (cell.Style.BackColor == System.Drawing.Color.Black)
			{
				return "#";
			}
			if (cell.Value is string stringValue)
			{
				if (cell.Style.ForeColor == System.Drawing.Color.Gray)
				{
					return stringValue + "*"; // Return empty string for grey text
				}
				else
				{
					return stringValue; // Return the string directly
				}
			}

			else if (cell.Value != null)
			{
				return cell.Value.ToString(); // Convert to string if it's not null
			}
			return string.Empty; // Return an empty string if no value
		}
		private int GetCellBorder(int row, int col)
		{
			var cellKey = new Tuple<int, int>(row, col);
			if (cellsWithBorders.TryGetValue(cellKey, out List<BorderSide> borders))
			{
				if (borders.Contains(BorderSide.Bottom) == true)
				{
					return 1;
				}
				else
				{
					return 2;
				}
			}
			else
			{
				return 0;
			}
		}
		private string GetCellSolution(int row, int col)
		{
			var cell = dataGridView1.Rows[row].Cells[col];
			// Check if the cell has a value and convert to string if necessary
			if (cell.Value is string stringValue)
			{
				return stringValue; // Return the string directly
			}
			else if (cell.Value != null)
			{
				return cell.Value.ToString(); // Convert to string if it's not null
			}
			return string.Empty; // Return an empty string if no value
		}

		//JSON I/O

		public static string ReadJsonFromZip(string filePathName)
		{
			string jsonEntryName = Path.GetFileName(filePathName).Replace(".cjz", ".json");
			using (var fileStream = new FileStream(filePathName, FileMode.Open))
			using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
			{
				ZipArchiveEntry jsonEntry = archive.GetEntry(jsonEntryName);
				if (jsonEntry == null)
				{
					throw new FileNotFoundException($"JSON entry '{jsonEntryName}' not found in the zip file.");
				}
				using (var jsonStream = jsonEntry.Open())
				using (var streamReader = new StreamReader(jsonStream, Encoding.UTF8))
				{
					return streamReader.ReadToEnd();
				}
			}
		}
		public void UpdateJsonEntry(string filePathName, string updatedJson)
		{
			string jsonEntryName = Path.GetFileName(filePathName).Replace(".cjz", ".json");
			// Create a temporary file path
			string tempFilePath = Path.GetTempFileName();
			try
			{
				// 1. Copy the original cjz file to a temporary file
				File.Copy(filePathName, tempFilePath, true);
				// 2. Open the temporary file for updating
				using (var archive = ZipFile.Open(tempFilePath, ZipArchiveMode.Update))
				{
					// Find and delete the existing JSON entry
					var jsonEntry = archive.GetEntry(jsonEntryName);
					if (jsonEntry != null)
					{
						jsonEntry.Delete();
					}
					// Serialize the new data
					// var updatedJson = JsonConvert.SerializeObject(dataManager.Records, Formatting.Indented);
					// Create a new entry with the correct filename and write the updated content
					var newJsonEntry = archive.CreateEntry(jsonEntryName);
					using (var streamWriter = new StreamWriter(newJsonEntry.Open(), Encoding.UTF8))
					{
						streamWriter.Write(updatedJson);
					}
				} // The ZipArchive is properly closed here, saving all changes to the temp file.
				  // 3. Replace the original file with the updated temporary file
				File.Delete(filePathName);
				File.Move(tempFilePath, filePathName);
				// Console.WriteLine($"Successfully updated JSON entry '{jsonEntryName}' in '{filePathName}'.");
			}
			catch (Exception ex)
			{
				// Console.WriteLine($"An error occurred while updating the zip file: {ex.Message}");
			}
			finally
			{
				// 4. Clean up the temporary file if it still exists
				if (File.Exists(tempFilePath))
				{
					File.Delete(tempFilePath);
				}
			}
		}
		public void LoadData(string filePathName)
		{
			string json = ReadJsonFromZip(filePathName);
			try
			{
				dataManager.Records = JsonConvert.DeserializeObject<List<CellDataRecord>>(json);
				int startCWNo = 0;
				if (int.Parse(tbCWNo.Text) > 1)
				{
					startCWNo = int.Parse(tbCWNo.Text) - 1;
				}
				dataManager.CurrentIndex = dataManager.Records.Count > 0 ? startCWNo : -1;
			}
			catch (Newtonsoft.Json.JsonException jsonEx)
			{
				// Handle JSON deserialization errors
				//Console.WriteLine($"JSON Error: {jsonEx.Message}");
				MessageBox.Show("JSON error. Incorrect or corrupt file");
			}
			catch (Exception ex)
			{
				MessageBox.Show("JSON Exception error. Incorrect or corrupt file");
				// Handle any other exceptions
				// Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}
		private void SaveData(string filePathName)
		{
			var json = JsonConvert.SerializeObject(dataManager.Records, Formatting.Indented);
			UpdateJsonEntry(filePathName, json);
		}

		// Common Functions and Methods

		public string CalcWordSplit(string soln)
		{
			int wSplit = 0;
			int[] wordLen = new int[5];
			string clueWords = "";
			for (int i = 0; i < soln.Length; i++)
			{
				if (soln.Substring(i, 1) == "/")
				{
					wSplit = wSplit + 1;
				}
				else
				{
					wordLen[wSplit] = wordLen[wSplit] + 1;
				}
			}
			for (int i = 0; i <= wSplit; i++)
			{
				clueWords = clueWords + wordLen[i] + ",";
			}
			return clueWords;
		}

		private void label13_Click(object sender, EventArgs e)
		{

		}
	}
}
