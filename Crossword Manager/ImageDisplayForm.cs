using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;


namespace Crossword_Filler
{
	public class ImageDisplayForm : Form
	{
		private Form1 mainForm; // Reference to the main form
		private System.Windows.Forms.Button BtnFillGrid;
		private System.Windows.Forms.Button BtnCopyClues;
		private System.Windows.Forms.Button BtnClose;
		private System.Windows.Forms.Button BtnNew;
		private System.Windows.Forms.Button BtnResetSelection;
		private System.Windows.Forms.TextBox TbTopLeftCoord;
		private System.Windows.Forms.TextBox TbBottomRightCoord;
		public System.Windows.Forms.TextBox TbBlackThreshold;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button BtnAddCW;
		private System.Windows.Forms.Button BtnPaste;
		private System.Windows.Forms.Button BtnScreen;
		private System.Windows.Forms.Button BtnOCRScreen;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;
		private Panel panel1;
		private Panel panel3;
		public NumericUpDown GridSizeSelect;
		public Bitmap capturedCluesImage;
		private Bitmap canvas;
		public Bitmap originalCluesImage;
		private Bitmap whiteBackground;
		public Image snippedImage;
		public PictureBox pictureBoxNewCW;
		private System.Drawing.Point topLeftCorner;
		private System.Drawing.Point bottomRightCorner;
		private bool isSelectingCorners = true;
		private string filePathName;
		private int blacklevel;
		public bool newGridLoaded;
		public bool newCluesLoaded;
		private bool blankSaveAs = false;
		private float _scaleX;
		private Label label10;
		private Button BtnImageData;
		private float _scaleY;
		private float scaleFactor;

		public ImageDisplayForm(Form1 form1)
		{
			InitializeComponent();
			this.AutoScaleMode = AutoScaleMode.Dpi;
			mainForm = form1; // Assign the reference to the main form
			pictureBoxNewCW.SizeMode = PictureBoxSizeMode.AutoSize;
			pictureBoxNewCW.MouseDown += PictureBox_MouseDown; // Subscribe to MouseDown event
			this.Text = "Crossword Image Extract";
			newGridLoaded = false;
			newCluesLoaded = false;
			InitializeTrackBar();
		}
		private void ImageDisplayForm_Load(object sender, EventArgs e)
		{
			// canvas = new Bitmap(PictureBoxOCRClues.Width, PictureBoxOCRClues.Height);
			// PictureBoxOCRClues.Image = canvas;
			scaleFactor = (float)this.DeviceDpi / 96f;

			this.StartPosition = FormStartPosition.Manual; // Center this form relative to the main form
			this.Left = mainForm.Right - ScalePixelValue(400);
			this.Top = mainForm.Top + ScalePixelValue(20);
			PositionControls();
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			pictureBoxNewCW = new PictureBox();
			BtnClose = new Button();
			BtnNew = new Button();
			BtnFillGrid = new Button();
			BtnCopyClues = new Button();
			BtnResetSelection = new Button();
			TbTopLeftCoord = new TextBox();
			TbBottomRightCoord = new TextBox();
			label1 = new Label();
			panel1 = new Panel();
			trackBar1 = new TrackBar();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			label5 = new Label();
			BtnAddCW = new Button();
			BtnPaste = new Button();
			BtnScreen = new Button();
			label7 = new Label();
			label8 = new Label();
			label6 = new Label();
			panel3 = new Panel();
			BtnImageData = new Button();
			BtnOCRScreen = new Button();
			label9 = new Label();
			GridSizeSelect = new NumericUpDown();
			label10 = new Label();
			toolTip1 = new ToolTip(components);
			((System.ComponentModel.ISupportInitialize)pictureBoxNewCW).BeginInit();
			((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
			panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)GridSizeSelect).BeginInit();
			SuspendLayout();
			// 
			// pictureBoxNewCW
			// 
			pictureBoxNewCW.BorderStyle = BorderStyle.FixedSingle;
			pictureBoxNewCW.Location = new Point(10, 2);
			pictureBoxNewCW.Name = "pictureBoxNewCW";
			pictureBoxNewCW.Size = new Size(692, 424);
			pictureBoxNewCW.TabIndex = 0;
			pictureBoxNewCW.TabStop = false;
			// 
			// BtnClose
			// 
			BtnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtnClose.Location = new Point(637, 56);
			BtnClose.Name = "BtnClose";
			BtnClose.Size = new Size(63, 23);
			BtnClose.TabIndex = 1;
			BtnClose.Text = "Close";
			toolTip1.SetToolTip(BtnClose, "If area is selected but not added will request confirmation\r\nIf not confirm will remove the scanned data ");
			BtnClose.UseVisualStyleBackColor = true;
			BtnClose.Click += BtnClose_Click;
			// 
			// BtnNew
			// 
			BtnNew.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtnNew.Location = new Point(183, 56);
			BtnNew.Name = "BtnNew";
			BtnNew.Size = new Size(63, 23);
			BtnNew.TabIndex = 2;
			BtnNew.Text = "File";
			toolTip1.SetToolTip(BtnNew, "Load an image of the crossword\r\nCan repeat for multiple images");
			BtnNew.UseVisualStyleBackColor = true;
			BtnNew.Click += BtnNew_Click;
			// 
			// BtnFillGrid
			// 
			BtnFillGrid.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtnFillGrid.Location = new Point(255, 2);
			BtnFillGrid.Name = "BtnFillGrid";
			BtnFillGrid.Size = new Size(63, 23);
			BtnFillGrid.TabIndex = 2;
			BtnFillGrid.Text = "Fill Grid";
			toolTip1.SetToolTip(BtnFillGrid, "Select once the crossword grid has been selected\r\nWill need to save first if not saved already\r\nRepeat with new black level as necessary");
			BtnFillGrid.UseVisualStyleBackColor = true;
			BtnFillGrid.Click += BtnFillGrid_Click;
			// 
			// BtnCopyClues
			// 
			BtnCopyClues.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtnCopyClues.Location = new Point(450, 2);
			BtnCopyClues.Name = "BtnCopyClues";
			BtnCopyClues.Size = new Size(73, 23);
			BtnCopyClues.TabIndex = 2;
			BtnCopyClues.Text = "Snip Clues";
			toolTip1.SetToolTip(BtnCopyClues, "Select once the Clues List has been selected\r\nWill need to save first if not saved already");
			BtnCopyClues.UseVisualStyleBackColor = true;
			BtnCopyClues.Click += BtnCopyCluesNew_Click;
			// 
			// BtnResetSelection
			// 
			BtnResetSelection.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtnResetSelection.Location = new Point(383, 56);
			BtnResetSelection.Name = "BtnResetSelection";
			BtnResetSelection.Size = new Size(63, 23);
			BtnResetSelection.TabIndex = 2;
			BtnResetSelection.Text = "Reset Area";
			toolTip1.SetToolTip(BtnResetSelection, "Reset the selected area to try again or for a new image");
			BtnResetSelection.UseVisualStyleBackColor = true;
			BtnResetSelection.Click += BtnResetSelection_Click;
			// 
			// TbTopLeftCoord
			// 
			TbTopLeftCoord.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			TbTopLeftCoord.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			TbTopLeftCoord.Location = new Point(337, 5);
			TbTopLeftCoord.Name = "TbTopLeftCoord";
			TbTopLeftCoord.Size = new Size(52, 22);
			TbTopLeftCoord.TabIndex = 3;
			TbTopLeftCoord.TextAlign = HorizontalAlignment.Center;
			TbTopLeftCoord.WordWrap = false;
			// 
			// TbBottomRightCoord
			// 
			TbBottomRightCoord.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			TbBottomRightCoord.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			TbBottomRightCoord.Location = new Point(394, 5);
			TbBottomRightCoord.Name = "TbBottomRightCoord";
			TbBottomRightCoord.Size = new Size(52, 22);
			TbBottomRightCoord.TabIndex = 4;
			TbBottomRightCoord.TextAlign = HorizontalAlignment.Center;
			TbBottomRightCoord.WordWrap = false;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			label1.AutoSize = true;
			label1.Location = new Point(124, 38);
			label1.Name = "label1";
			label1.Size = new Size(35, 15);
			label1.TabIndex = 8;
			label1.Text = "Black";
			// 
			// panel1
			// 
			panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			panel1.BorderStyle = BorderStyle.FixedSingle;
			panel1.Location = new Point(124, 13);
			panel1.Name = "panel1";
			panel1.Size = new Size(39, 22);
			panel1.TabIndex = 7;
			// 
			// trackBar1
			// 
			trackBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			trackBar1.BackColor = SystemColors.ActiveCaption;
			trackBar1.Location = new Point(8, -5);
			trackBar1.Maximum = 255;
			trackBar1.Name = "trackBar1";
			trackBar1.Size = new Size(111, 45);
			trackBar1.TabIndex = 6;
			toolTip1.SetToolTip(trackBar1, "Adjus the threshold for non-white. May need to change if it does map black and white squares due to the contrast in the image");
			trackBar1.Scroll += trackBar1_Scroll;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			label2.AutoSize = true;
			label2.Location = new Point(348, 29);
			label2.Name = "label2";
			label2.Size = new Size(36, 15);
			label2.TabIndex = 9;
			label2.Text = "TopL:";
			label2.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			label3.AutoSize = true;
			label3.Location = new Point(391, 29);
			label3.Name = "label3";
			label3.Size = new Size(57, 15);
			label3.TabIndex = 10;
			label3.Text = "BottomR:";
			label3.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			label4.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
			label4.Location = new Point(166, 24);
			label4.Name = "label4";
			label4.Size = new Size(176, 35);
			label4.TabIndex = 11;
			label4.Text = "Click Grid centre of Top Left and BottomRight squares";
			label4.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			label5.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
			label5.Location = new Point(461, 24);
			label5.Name = "label5";
			label5.Size = new Size(180, 32);
			label5.TabIndex = 12;
			label5.Text = "Click Clues TopLeft and BottomRight area or click OCR Clues";
			label5.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// BtnAddCW
			// 
			BtnAddCW.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtnAddCW.BackgroundImage = Properties.Resources.add;
			BtnAddCW.BackgroundImageLayout = ImageLayout.Stretch;
			BtnAddCW.Location = new Point(640, 3);
			BtnAddCW.Name = "BtnAddCW";
			BtnAddCW.Size = new Size(63, 46);
			BtnAddCW.TabIndex = 13;
			toolTip1.SetToolTip(BtnAddCW, "Click once the grid and the clues have been copied across.  \r\nNeed to add to confirm the scan\r\nCan repeat the process for multiple scans");
			BtnAddCW.UseVisualStyleBackColor = true;
			BtnAddCW.Click += BtnAddCW_Click;
			// 
			// BtnPaste
			// 
			BtnPaste.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtnPaste.Location = new Point(248, 56);
			BtnPaste.Name = "BtnPaste";
			BtnPaste.Size = new Size(69, 23);
			BtnPaste.TabIndex = 15;
			BtnPaste.Text = "Clipboard";
			toolTip1.SetToolTip(BtnPaste, "Paste from the clipboard\r\nCan repeat for multiple images");
			BtnPaste.UseVisualStyleBackColor = true;
			BtnPaste.Click += BtnPaste_Click;
			// 
			// BtnScreen
			// 
			BtnScreen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtnScreen.Location = new Point(317, 56);
			BtnScreen.Name = "BtnScreen";
			BtnScreen.Size = new Size(63, 23);
			BtnScreen.TabIndex = 16;
			BtnScreen.Text = "Screen";
			toolTip1.SetToolTip(BtnScreen, "Open to select screen area to grab. Ensure the crossword is open behind\r\n");
			BtnScreen.UseVisualStyleBackColor = true;
			BtnScreen.Click += BtnScreen_Click;
			// 
			// label7
			// 
			label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			label7.AutoSize = true;
			label7.BackColor = SystemColors.ActiveCaption;
			label7.Font = new Font("Segoe UI", 9F);
			label7.Location = new Point(96, 26);
			label7.Name = "label7";
			label7.Size = new Size(18, 15);
			label7.TabIndex = 18;
			label7.Text = "W";
			// 
			// label8
			// 
			label8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			label8.AutoSize = true;
			label8.BackColor = SystemColors.ActiveCaption;
			label8.FlatStyle = FlatStyle.Flat;
			label8.Font = new Font("Segoe UI", 9F);
			label8.Location = new Point(15, 25);
			label8.Name = "label8";
			label8.Size = new Size(14, 15);
			label8.TabIndex = 19;
			label8.Text = "B";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.BackColor = Color.Transparent;
			label6.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label6.ForeColor = SystemColors.AppWorkspace;
			label6.Location = new Point(195, 185);
			label6.Name = "label6";
			label6.Size = new Size(262, 45);
			label6.TabIndex = 20;
			label6.Text = "Load Crossword";
			// 
			// panel3
			// 
			panel3.Anchor = AnchorStyles.Bottom;
			panel3.Controls.Add(label8);
			panel3.Controls.Add(label7);
			panel3.Controls.Add(BtnImageData);
			panel3.Controls.Add(BtnOCRScreen);
			panel3.Controls.Add(label9);
			panel3.Controls.Add(GridSizeSelect);
			panel3.Controls.Add(TbTopLeftCoord);
			panel3.Controls.Add(BtnClose);
			panel3.Controls.Add(BtnScreen);
			panel3.Controls.Add(BtnNew);
			panel3.Controls.Add(BtnPaste);
			panel3.Controls.Add(BtnFillGrid);
			panel3.Controls.Add(BtnAddCW);
			panel3.Controls.Add(BtnCopyClues);
			panel3.Controls.Add(BtnResetSelection);
			panel3.Controls.Add(panel1);
			panel3.Controls.Add(trackBar1);
			panel3.Controls.Add(label1);
			panel3.Controls.Add(TbBottomRightCoord);
			panel3.Controls.Add(label4);
			panel3.Controls.Add(label3);
			panel3.Controls.Add(label2);
			panel3.Controls.Add(label5);
			panel3.Controls.Add(label10);
			panel3.Location = new Point(-1, 439);
			panel3.Name = "panel3";
			panel3.Size = new Size(711, 83);
			panel3.TabIndex = 21;
			// 
			// BtnImageData
			// 
			BtnImageData.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtnImageData.Location = new Point(527, 56);
			BtnImageData.Name = "BtnImageData";
			BtnImageData.Size = new Size(63, 23);
			BtnImageData.TabIndex = 25;
			BtnImageData.Text = "Image";
			toolTip1.SetToolTip(BtnImageData, "Click once the grid and the clues have been copied across.  \r\nNeed to add to confirm the scan\r\nCan repeat the process for multiple scans");
			BtnImageData.UseVisualStyleBackColor = true;
			BtnImageData.Click += BtnImageData_Click;
			// 
			// BtnOCRScreen
			// 
			BtnOCRScreen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtnOCRScreen.Location = new Point(565, 2);
			BtnOCRScreen.Name = "BtnOCRScreen";
			BtnOCRScreen.Size = new Size(73, 23);
			BtnOCRScreen.TabIndex = 23;
			BtnOCRScreen.Text = "OCR Clues";
			toolTip1.SetToolTip(BtnOCRScreen, "Select if OCR is to be attempted rather than just snipping the Clue List");
			BtnOCRScreen.UseVisualStyleBackColor = true;
			BtnOCRScreen.Click += BtnOCRScreen_Click;
			// 
			// label9
			// 
			label9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			label9.AutoSize = true;
			label9.Location = new Point(172, 8);
			label9.Name = "label9";
			label9.Size = new Size(30, 15);
			label9.TabIndex = 21;
			label9.Text = "Size:";
			label9.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// GridSizeSelect
			// 
			GridSizeSelect.Location = new Point(206, 3);
			GridSizeSelect.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
			GridSizeSelect.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
			GridSizeSelect.Name = "GridSizeSelect";
			GridSizeSelect.ReadOnly = true;
			GridSizeSelect.Size = new Size(37, 23);
			GridSizeSelect.TabIndex = 20;
			toolTip1.SetToolTip(GridSizeSelect, "Select crossword grid size before clicking filling grid");
			GridSizeSelect.Value = new decimal(new int[] { 15, 0, 0, 0 });
			// 
			// label10
			// 
			label10.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			label10.AutoSize = true;
			label10.Font = new Font("Segoe UI", 9F);
			label10.Location = new Point(524, 4);
			label10.Name = "label10";
			label10.Size = new Size(39, 15);
			label10.TabIndex = 24;
			label10.Text = "- OR -";
			label10.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// ImageDisplayForm
			// 
			AutoScaleDimensions = new SizeF(96F, 96F);
			AutoScaleMode = AutoScaleMode.Dpi;
			ClientSize = new Size(710, 523);
			Controls.Add(label6);
			Controls.Add(pictureBoxNewCW);
			Controls.Add(panel3);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			Name = "ImageDisplayForm";
			SizeGripStyle = SizeGripStyle.Hide;
			Text = "Import and Define Crossword";
			Load += ImageDisplayForm_Load;
			((System.ComponentModel.ISupportInitialize)pictureBoxNewCW).EndInit();
			((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)GridSizeSelect).EndInit();
			ResumeLayout(false);
			PerformLayout();

		}
		private void InitializeTrackBar()
		{
			int blacklvl = 53;
			trackBar1.Value = blacklvl;
			trackBar1_Scroll(null, EventArgs.Empty);
		}
		private void PositionControls()
		{
			//int ScaleValue(int value) => (int)(value * scaleFactor);
			int formWidth = pictureBoxNewCW.Width + ScalePixelValue(40);
			int formWidthMin= ScalePixelValue(750);
			formWidth = formWidth < formWidthMin ? formWidthMin : formWidth;
			// formWidth = 1100;
			//int width = Convert.ToInt32(processedImage.Width * scaleFactor);
			int fmDPIHeight = ScalePixelValue(125);
			this.Size = new System.Drawing.Size(formWidth, pictureBoxNewCW.Height + fmDPIHeight); // Set desired size
			pictureBoxNewCW.Left = (this.Width - pictureBoxNewCW.Width) / 2;
			Screen currentScreen = Screen.FromControl(this);
			if (this.Right > currentScreen.WorkingArea.Right)
			{
				this.Left = currentScreen.WorkingArea.Right - this.Width;
			}
			if (this.Bottom > currentScreen.WorkingArea.Bottom)
			{
				this.Top = currentScreen.WorkingArea.Bottom - this.Height;
			}
			if (this.Top < currentScreen.WorkingArea.Top)
			{
				this.Top = currentScreen.WorkingArea.Top;
				this.SizeGripStyle = SizeGripStyle.Show;
			}
			if (this.Bottom > currentScreen.WorkingArea.Bottom)
			{
				this.Top = currentScreen.WorkingArea.Bottom - this.Height;
			}

			// pictureBoxNewCW.BringToFront();
			pictureBoxNewCW.SendToBack();
			label6.SendToBack();
			this.BackColor = Color.LightGray;
			panel3.Left = (this.Width - panel3.Width) / 2;
			//panel3.Top = panel3.Top
		}
		private int ScalePixelValue(int value)
		{
			return this.LogicalToDeviceUnits(new Size(value, 0)).Width;
		}

		private void BtnNew_Click(object sender, EventArgs e)  // File import
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "PNG Files (*.png)|*.png|All Files (*.*)|*.*"; // Set filter for PNG files
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					// mainForm.ReLoad();
					filePathName = openFileDialog.FileName; // Store the selected file path
					Bitmap image = new Bitmap(filePathName); // Load the image
					pictureBoxNewCW.Image = image;
					pictureBoxNewCW.SizeMode = PictureBoxSizeMode.AutoSize;
					this.Location = new System.Drawing.Point(mainForm.Right - 400, mainForm.Top + 20);
					snippedImage = image;
					PositionControls();
					ResetSelectedBox();
					newGridLoaded = false;
					newCluesLoaded = false;
				}
			}
		}
		private void BtnPaste_Click(object sender, EventArgs e)
		{
			// Check if the clipboard contains an image
			if (Clipboard.ContainsImage())
			{
				// mainForm.ReLoad();
				// Retrieve the image from the clipboard
				System.Drawing.Image clipboardImage = Clipboard.GetImage();

				// Assign the image to the PictureBox
				pictureBoxNewCW.Image = clipboardImage;
				pictureBoxNewCW.SizeMode = PictureBoxSizeMode.AutoSize;
				this.Location = new System.Drawing.Point(mainForm.Right - ScalePixelValue(400), mainForm.Top + ScalePixelValue(20));
				PositionControls();
				ResetSelectedBox();
				newGridLoaded = false;
				newCluesLoaded = false;
			}
			else
			{
				MessageBox.Show("The clipboard does not contain an image.", "No Image Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		private void BtnScreen_Click(object sender, EventArgs e)
		{
			// mainForm.ReLoad();
			this.Opacity = 0.0;
			mainForm.Opacity = 0.0;
			System.Windows.Forms.Application.DoEvents();
			// Create a full-screen screenshot.
			Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
			Bitmap screenCapture = new Bitmap(screenBounds.Width, screenBounds.Height);
			using (Graphics g = Graphics.FromImage(screenCapture))
			{
				g.CopyFromScreen(screenBounds.X, screenBounds.Y, 0, 0, screenBounds.Size, CopyPixelOperation.SourceCopy);
			}
			// Pass the screenshot to the snipping form.
			using (SnippingForm snipper = new SnippingForm(screenCapture))
			{
				if (snipper.ShowDialog() == DialogResult.OK)
				{
					// Get the cropped image from the snipping tool.
					// Bitmap croppedImage = snipper.CroppedImage;
					pictureBoxNewCW.SizeMode = PictureBoxSizeMode.AutoSize;
					pictureBoxNewCW.Image = snipper.CroppedImage;
					snippedImage = snipper.CroppedImage;
				}
			}
			this.Opacity = 1.0;
			mainForm.Opacity = 1.0;
			this.Location = new System.Drawing.Point(mainForm.Right - ScalePixelValue(400), mainForm.Top + ScalePixelValue(20));
			PositionControls();
			ResetSelectedBox();
			newGridLoaded = false;
			newCluesLoaded = false;
		}

		private void BtnImageData_Click(object sender, EventArgs e)
		{
			var waitForm = DisplayImageData(snippedImage);
			waitForm.Show(this);
		}
		private Form DisplayImageData(Image snippedImage)
		{
			int width = snippedImage.Width;
			int height = snippedImage.Height;
			int horizontalRes = (int)snippedImage.HorizontalResolution;
			int verticalRes = (int)snippedImage.VerticalResolution;
			System.Drawing.Imaging.PixelFormat pixelFormat = snippedImage.PixelFormat;
			int formWidth = ScalePixelValue(200);
			int formHeight = ScalePixelValue(100);
			int x = this.Location.X + (this.Width - formWidth) / 2;
			int y = this.Location.Y + (this.Height - formHeight) / 2;
			string message = "Width = " + width + Environment.NewLine +
							"Height = " + height + Environment.NewLine +
							"DPI = " + horizontalRes + Environment.NewLine +
							"PixelFormat = " + pixelFormat.ToString().Replace("Format", "");
			var waitForm = new Form
			{
				Size = new System.Drawing.Size(formWidth, formHeight),
				Text = "Image Data",
				FormBorderStyle = FormBorderStyle.FixedDialog,
				MinimizeBox = false,
				MaximizeBox = false,
				StartPosition = FormStartPosition.Manual,
				Location = new System.Drawing.Point(x, y),
			};
			var waitLabel = new Label
			{
				Text = message,
				AutoSize = true,
				Location = new System.Drawing.Point(ScalePixelValue(30), ScalePixelValue(10)) // Position the label
			};
			waitForm.Controls.Add(waitLabel);
			waitForm.Height = waitLabel.Top + waitLabel.Height + ScalePixelValue(50);
			return waitForm;
		}

		private void ChkBlank()
		{
			if (mainForm.fileCWPathName.Contains("blank"))
			{
				MessageBox.Show("Need to SaveAs before continuing");
				mainForm.SaveAsJSONZIP();
				if (mainForm.fileCWPathNameCurrent.Contains("blank") == false)
				{
					mainForm.NoCrossword.SendToBack();
					blankSaveAs = true;
				}
			}
			else
			{
				blankSaveAs = true;
			}
		}
		private void PictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (isSelectingCorners == false)
			{
				ResetSelectedBox();
			}
			if (isSelectingCorners)
			{
				if (topLeftCorner.IsEmpty)
				{
					topLeftCorner = e.Location; // Capture top-left corner
					TbTopLeftCoord.Text = topLeftCorner.X.ToString() + ", " + topLeftCorner.Y.ToString();
					TbTopLeftCoord.BackColor = Color.LightGreen;
				}
				else
				{
					bottomRightCorner = e.Location; // Capture bottom-right corner
					TbBottomRightCoord.Text = bottomRightCorner.X.ToString() + ", " + bottomRightCorner.Y.ToString();
					TbBottomRightCoord.BackColor = Color.LightGreen;
					isSelectingCorners = false; // Stop selecting
					using (Graphics g = pictureBoxNewCW.CreateGraphics())
					{
						g.DrawRectangle(Pens.Red, new Rectangle(topLeftCorner, new System.Drawing.Size(bottomRightCorner.X - topLeftCorner.X, bottomRightCorner.Y - topLeftCorner.Y)));
					}
				}
			}
		}
		public (System.Drawing.Point topLeft, System.Drawing.Point bottomRight) GetSelectedCorners()
		{
			return (topLeftCorner, bottomRightCorner);
		}
		private void BtnResetSelection_Click(object sender, EventArgs e)
		{
			ResetSelectedBox();
		}
		private void ResetSelectedBox()
		{
			// Reset selection for new corner selection
			topLeftCorner = System.Drawing.Point.Empty;
			bottomRightCorner = System.Drawing.Point.Empty;
			isSelectingCorners = true;
			TbBottomRightCoord.Text = "";
			TbTopLeftCoord.Text = "";
			TbTopLeftCoord.BackColor = Color.White;
			TbBottomRightCoord.BackColor = Color.White;
			// Optionally clear any highlights
			pictureBoxNewCW.Invalidate(); // Redraw the PictureBox to clear highlights
		}
		private void BtnClose_Click(object sender, EventArgs e)
		{
			if (newCluesLoaded == true || newGridLoaded == true)
			{
				DialogResult result = MessageBox.Show("Imported Crossword not added. Cancel?", "Close Crossword Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{
					// if previously a blank form then nothing to reload
					ClearPaintedClues();
					mainForm.ReLoad();
					this.Close();
				}
			}
			else
			{
				this.Close();
			}
		}
		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			int grayValue = trackBar1.Value;
			Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
			panel1.BackColor = grayColor;
			label1.Text = $"Black: {grayValue}";
		}

		// Fill grid

		private void BtnFillGrid_Click(object sender, EventArgs e)
		{
			if (blankSaveAs == false)
			{
				ChkBlank();
				if (blankSaveAs == false)
				{
					MessageBox.Show("Need to SaveAs first");
					return;
				}
			}
			var (topLeft, bottomRight) = GetSelectedCorners();
			if (!topLeft.IsEmpty && !bottomRight.IsEmpty)
			{
				// Cast the image to Bitmap
				Bitmap bitmapImage = pictureBoxNewCW.Image as Bitmap;

				if (bitmapImage != null)
				{
					// Create a new Bitmap for the selected area
					int width = bottomRight.X - topLeft.X;
					int height = bottomRight.Y - topLeft.Y;

					if (width > 0 && height > 0)
					{
						Bitmap selectedArea = new Bitmap(width, height);
						using (Graphics g = Graphics.FromImage(selectedArea))
						{
							// Draw the selected area from the original image to the new Bitmap
							g.DrawImage(bitmapImage, new Rectangle(0, 0, width, height), new Rectangle(topLeft.X, topLeft.Y, width, height), GraphicsUnit.Pixel);
						}
					}
					else
					{
						MessageBox.Show("Invalid selection area.");
					}
					newGridLoaded = true;
				}

				if (bitmapImage != null)
				{
					Form1.rowCnt = (int)GridSizeSelect.Value;
					Form1.colCnt = (int)GridSizeSelect.Value;
					blacklevel = trackBar1.Value;
					mainForm.LoadImageAndFillCells(topLeft, bottomRight, bitmapImage, blacklevel);
					this.Location = new System.Drawing.Point(mainForm.Right - ScalePixelValue(400), mainForm.Top + ScalePixelValue(20));
					PositionControls();
					ResetSelectedBox();
				}
				else
				{
					MessageBox.Show("The image is not a valid Bitmap.");
				}
			}
			else
			{
				MessageBox.Show("Please select both corners before processing the fill.");
			}
		}

		// Copy Clues

		private void BtnCopyCluesNew_Click(object sender, EventArgs e)
		{
			if (!blankSaveAs)
			{
				ChkBlank();
				if (!blankSaveAs)
				{
					MessageBox.Show("Need to SaveAs first");
					return;
				}
			}
			var (topLeft, bottomRight) = GetSelectedCorners();
			if (!topLeft.IsEmpty && !bottomRight.IsEmpty)

			{
				ClearPaintedClues();
				if (mainForm.PictureBoxClues != null)
				{
					mainForm.PictureBoxClues.Image = null;
					mainForm.PictureBoxClues.BackColor = Color.White;
					mainForm.PictureBoxClues.SizeMode = PictureBoxSizeMode.Normal;
				}
				Bitmap bitmapImage = pictureBoxNewCW.Image as Bitmap;
				if (bitmapImage != null)
				{
					this.Left = mainForm.Right - this.Width - 380;
					int width = bottomRight.X - topLeft.X;
					int height = bottomRight.Y - topLeft.Y;

					if (width > 0 && height > 0)
					{
						// Capture the selected area at its original resolution.
						Bitmap tempImage = new Bitmap(width, height);
						using (Graphics g = Graphics.FromImage(tempImage))
						{
							g.DrawImage(bitmapImage, new Rectangle(0, 0, width, height),
										new Rectangle(topLeft.X, topLeft.Y, width, height),
										GraphicsUnit.Pixel);
						}

						// Dispose of the old image if it exists to prevent memory leaks.
						if (capturedCluesImage != null)
						{
							capturedCluesImage.Dispose();
						}
						capturedCluesImage = tempImage;
						originalCluesImage = tempImage;
						mainForm.capturedCluesImage = capturedCluesImage;

						// Trigger the PictureBox to repaint itself.
						if (mainForm.PictureBoxClues != null)
						{
							mainForm.PictureBoxClues.Image = null; // Clear any existing image
																   // Set SizeMode to normal so our custom paint logic can take over.
							mainForm.PictureBoxClues.SizeMode = PictureBoxSizeMode.Normal;
							// mainForm.PictureBoxClues.Invalidate();
							mainForm.RefreshPictureBoxClues();
							mainForm.PictureBoxClues.BringToFront();
							mainForm.PictureBoxClues.Visible = true;
							mainForm.TbRichTextAcrossClues.Visible = false;
							mainForm.TbRichTextDownClues.Visible = false;
						}

						newCluesLoaded = true;
					}
					else
					{
						MessageBox.Show("Invalid selection area.");
					}
				}
			}
		}
		public void ClearPaintedClues()
		{
			if (capturedCluesImage != null)
			{
				capturedCluesImage.Dispose();
				capturedCluesImage = null; // Set the reference to null.
										   // whiteBackground.Dispose();
										   // whiteBackground = null;
			}
			if (mainForm.PictureBoxClues != null)
			{
				// mainForm.PictureBoxClues.Invalidate();
				//mainForm.RefreshPictureBoxClues();
			}
		}
		private void DrawClues(Graphics g, Rectangle destRect)
		{
			g.Clear(Color.White);
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

		// Add crossword

		private void BtnAddCW_Click(object sender, EventArgs e)
		{
			if (newCluesLoaded == true && newGridLoaded == true)
			{
				newCluesLoaded = false;
				newGridLoaded = false;
				mainForm.tbCWNo.Text = (int.Parse(mainForm.tbTotalCW.Text) + 1).ToString();
				// mainForm.TbRichTextAcrossClues.Parent.Controls.GetChildIndex(mainForm.TbRichTextAcrossClues)
				if (mainForm.TbRichTextAcrossClues.Visible == false)
				{
					SaveCluesAsPNG(mainForm.fileCWPathName);
				}
				ClearPaintedClues();
				// mainForm.LoadClues(mainForm.fileCWPathName);
				// LoadCluesNew(mainForm.fileCWPathName);
				mainForm.BtnAdd();
				this.Left = mainForm.Right - ScalePixelValue(400);
				this.Top = mainForm.Top + ScalePixelValue(20);
				Screen currentScreen = Screen.FromControl(this);
				if (this.Right > currentScreen.WorkingArea.Right)
				{
					this.Left = currentScreen.WorkingArea.Right - this.Width;
				}
				if (this.Bottom > currentScreen.WorkingArea.Bottom)
				{
					this.Top = currentScreen.WorkingArea.Bottom - this.Height;
				}
			}
			else
			{
				MessageBox.Show("Either crossword grid or clues or both not loaded");
			}
		}
		public void SaveCluesAsPNG(string fn)
		{
			if (capturedCluesImage == null)
			{
				MessageBox.Show("There is no image to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				string fileName = mainForm.tbCWNo.Text + " clues.png";
				string zipPath = fn;
				// Create a new Bitmap with the original image dimensions.
				using (Bitmap fullSizeBmp = new Bitmap(originalCluesImage.Width, originalCluesImage.Height))
				{
					// Get a Graphics object for the new bitmap.
					using (Graphics g = Graphics.FromImage(fullSizeBmp))
					{
						// Draw onto the new bitmap at its full size (no scaling needed for the drawing rectangle).
						DrawClues(g, new Rectangle(0, 0, fullSizeBmp.Width, fullSizeBmp.Height));
					}

					// Continue with your ZIP file logic.
					using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
					{
						ZipArchiveEntry existingEntry = archive.GetEntry(fileName);
						if (existingEntry != null)
						{
							existingEntry.Delete();
						}

						ZipArchiveEntry newEntry = archive.CreateEntry(fileName);

						using (Stream stream = newEntry.Open())
						{
							// Save the full-size bitmap to the stream.
							fullSizeBmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error saving image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//OCR using Tesseract

		private void BtnOCRScreen_Click(object sender, EventArgs e)
		{
			OCRClues OCRForm = new OCRClues(mainForm, this);
			OCRForm.StartPosition = FormStartPosition.Manual;
			OCRForm.Left = this.Left - ScalePixelValue(350);
			OCRForm.Top = this.Top + ScalePixelValue(100);
			OCRForm.ShowDialog();
		}

		// Superseded by BtnCopyCluesNew_Click

		private void BtnCopyClues_Click(object sender, EventArgs e)
		{
			if (blankSaveAs == false)
			{
				ChkBlank();
				if (blankSaveAs == false)
				{
					MessageBox.Show("Need to SaveAs first");
					return;
				}
			}
			var (topLeft, bottomRight) = GetSelectedCorners();
			if (!topLeft.IsEmpty && !bottomRight.IsEmpty)
			{
				// Cast the image to Bitmap
				Bitmap bitmapImage = pictureBoxNewCW.Image as Bitmap;
				// mainForm.PictureBoxClues.SizeMode = PictureBoxSizeMode.Zoom;
				if (bitmapImage != null)
				{
					int newWidth, newHeight;
					this.Left = mainForm.Right - this.Width - ScalePixelValue(380);
					// Create a new Bitmap for the selected area
					int width = bottomRight.X - topLeft.X;
					int height = bottomRight.Y - topLeft.Y;
					// Get the original image's DPI
					float originalDpiX = bitmapImage.HorizontalResolution;
					float originalDpiY = bitmapImage.VerticalResolution;
					double originalAspectRatio = (double)width / height;
					double targetAspectRatio = (double)mainForm.PictureBoxClues.Width / mainForm.PictureBoxClues.Height;
					if (originalAspectRatio < targetAspectRatio)
					{
						// The original image is too "tall" for the target aspect ratio.
						// Expand the width to match the new ratio.
						newHeight = height;
						newWidth = (int)Math.Ceiling(newHeight * targetAspectRatio);
					}
					else if (originalAspectRatio > targetAspectRatio)
					{
						// The original image is too "wide" for the target aspect ratio.
						// Expand the height to match the new ratio.
						newWidth = width;
						newHeight = (int)Math.Ceiling(newWidth / targetAspectRatio);
					}
					else
					{
						newHeight = height;
						newWidth = width;
					}

					if (width > 0 && height > 0)
					{
						Bitmap selectedArea = new Bitmap(newWidth, newHeight);
						selectedArea.SetResolution(originalDpiX, originalDpiY);
						using (Graphics g = Graphics.FromImage(selectedArea))
						{
							g.Clear(Color.White);
							// Draw the selected area from the original image to the new Bitmap
							// bmp, destination, source
							// g.DrawImage(bitmapImage, new Rectangle(0, 0, width, height), new Rectangle(topLeft.X, topLeft.Y, width, height), GraphicsUnit.Pixel);
							g.DrawImage(bitmapImage, new Rectangle(0, 0, width, height), new Rectangle(topLeft.X, topLeft.Y, newWidth, newHeight), GraphicsUnit.Pixel);
						}
						if (mainForm.PictureBoxClues != null)
						{
							mainForm.PictureBoxClues.Image = selectedArea;
							mainForm.PictureBoxClues.BringToFront();
						}
						newCluesLoaded = true;
					}
					else
					{
						MessageBox.Show("Invalid selection area.");
					}
				}
			}
		}


	}
}
