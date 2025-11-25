namespace Crossword_Filler
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			bClose = new System.Windows.Forms.Button();
			BtnImport = new System.Windows.Forms.Button();
			tbCWNo = new System.Windows.Forms.TextBox();
			tbTotalCW = new System.Windows.Forms.TextBox();
			PictureBoxClues = new System.Windows.Forms.PictureBox();
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			importSolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			exportSolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			cypticExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			cheatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			clueDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			featureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			BtnPrev = new System.Windows.Forms.Button();
			BtnNext = new System.Windows.Forms.Button();
			tbSolnStatus = new System.Windows.Forms.TextBox();
			AcrossSeparator = new System.Windows.Forms.Button();
			DownSeparator = new System.Windows.Forms.Button();
			BtnUpdate = new System.Windows.Forms.Button();
			BtnJump = new System.Windows.Forms.Button();
			referenceTextBox = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			TbWordLookUp = new System.Windows.Forms.TextBox();
			BtnGoogle = new System.Windows.Forms.Button();
			BtnLoadExplorer = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			BtnScratchPad = new System.Windows.Forms.Button();
			DataGridScratchPad = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label8 = new System.Windows.Forms.Label();
			NoCrossword = new System.Windows.Forms.Label();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			RadioPen = new System.Windows.Forms.RadioButton();
			RadioPencil = new System.Windows.Forms.RadioButton();
			TbRichTextDownClues = new System.Windows.Forms.RichTextBox();
			TbRichTextAcrossClues = new System.Windows.Forms.RichTextBox();
			label13 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			BtnCWInfo = new System.Windows.Forms.Button();
			BtnCWKeys = new System.Windows.Forms.Button();
			CbHints = new System.Windows.Forms.CheckBox();
			CbSolns = new System.Windows.Forms.CheckBox();
			panel1 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			imageList1 = new System.Windows.Forms.ImageList(components);
			((System.ComponentModel.ISupportInitialize)PictureBoxClues).BeginInit();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)DataGridScratchPad).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// bClose
			// 
			bClose.Location = new System.Drawing.Point(90, 427);
			bClose.Name = "bClose";
			bClose.Size = new System.Drawing.Size(69, 33);
			bClose.TabIndex = 4;
			bClose.Text = "Close";
			bClose.UseVisualStyleBackColor = true;
			bClose.Click += bClose_Click;
			// 
			// BtnImport
			// 
			BtnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnImport.Location = new System.Drawing.Point(51, 50);
			BtnImport.Name = "BtnImport";
			BtnImport.Size = new System.Drawing.Size(65, 39);
			BtnImport.TabIndex = 5;
			BtnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			toolTip1.SetToolTip(BtnImport, "To scan and add new crosswords ");
			BtnImport.UseVisualStyleBackColor = true;
			BtnImport.Click += BtnImport_Click;
			// 
			// tbCWNo
			// 
			tbCWNo.BackColor = System.Drawing.Color.WhiteSmoke;
			tbCWNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
			tbCWNo.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			tbCWNo.Location = new System.Drawing.Point(66, 368);
			tbCWNo.Name = "tbCWNo";
			tbCWNo.Size = new System.Drawing.Size(45, 22);
			tbCWNo.TabIndex = 9;
			tbCWNo.TabStop = false;
			tbCWNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tbTotalCW
			// 
			tbTotalCW.BackColor = System.Drawing.SystemColors.InactiveBorder;
			tbTotalCW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			tbTotalCW.Location = new System.Drawing.Point(66, 397);
			tbTotalCW.Name = "tbTotalCW";
			tbTotalCW.Size = new System.Drawing.Size(45, 22);
			tbTotalCW.TabIndex = 10;
			tbTotalCW.TabStop = false;
			tbTotalCW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// PictureBoxClues
			// 
			PictureBoxClues.Anchor = System.Windows.Forms.AnchorStyles.Top;
			PictureBoxClues.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			PictureBoxClues.Location = new System.Drawing.Point(797, 44);
			PictureBoxClues.Margin = new System.Windows.Forms.Padding(0);
			PictureBoxClues.Name = "PictureBoxClues";
			PictureBoxClues.Size = new System.Drawing.Size(358, 432);
			PictureBoxClues.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			PictureBoxClues.TabIndex = 11;
			PictureBoxClues.TabStop = false;
			toolTip1.SetToolTip(PictureBoxClues, "SHIFT Click over clue number to add or remove tick mark");
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.ShowItemToolTips = true;
			menuStrip1.Size = new System.Drawing.Size(1166, 24);
			menuStrip1.TabIndex = 12;
			menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { loadToolStripMenuItem, importToolStripMenuItem, toolStripSeparator3, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator5, deleteToolStripMenuItem, toolStripSeparator4, importSolutionToolStripMenuItem, exportSolutionToolStripMenuItem, toolStripSeparator1, recentFilesToolStripMenuItem, toolStripSeparator2, closeToolStripMenuItem });
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			fileToolStripMenuItem.Text = "File";
			// 
			// loadToolStripMenuItem
			// 
			loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			loadToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
			loadToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			loadToolStripMenuItem.Text = "Open";
			loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
			// 
			// importToolStripMenuItem
			// 
			importToolStripMenuItem.Name = "importToolStripMenuItem";
			importToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
			importToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			importToolStripMenuItem.Text = "New";
			importToolStripMenuItem.Click += newToolStripMenuItem_Click;
			// 
			// toolStripSeparator3
			// 
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(166, 6);
			// 
			// saveToolStripMenuItem
			// 
			saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			saveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
			saveToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			saveToolStripMenuItem.Text = "Save";
			saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
			// 
			// saveAsToolStripMenuItem
			// 
			saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			saveAsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			saveAsToolStripMenuItem.Text = "Save As";
			saveAsToolStripMenuItem.Click += saveasToolStripMenuItem_Click;
			// 
			// toolStripSeparator5
			// 
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(166, 6);
			// 
			// deleteToolStripMenuItem
			// 
			deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D;
			deleteToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			deleteToolStripMenuItem.Text = "Delete";
			deleteToolStripMenuItem.ToolTipText = "Delete current crossword";
			deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
			// 
			// toolStripSeparator4
			// 
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(166, 6);
			// 
			// importSolutionToolStripMenuItem
			// 
			importSolutionToolStripMenuItem.Name = "importSolutionToolStripMenuItem";
			importSolutionToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			importSolutionToolStripMenuItem.Text = "Import Crossword";
			importSolutionToolStripMenuItem.Click += importSolutionToolStripMenuItem_Click;
			// 
			// exportSolutionToolStripMenuItem
			// 
			exportSolutionToolStripMenuItem.Name = "exportSolutionToolStripMenuItem";
			exportSolutionToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			exportSolutionToolStripMenuItem.Text = "Export Crossword";
			exportSolutionToolStripMenuItem.Click += exportSolutionToolStripMenuItem_Click;
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
			// 
			// recentFilesToolStripMenuItem
			// 
			recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
			recentFilesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			recentFilesToolStripMenuItem.Text = "Recent Files";
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
			// 
			// closeToolStripMenuItem
			// 
			closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			closeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q;
			closeToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			closeToolStripMenuItem.Text = "Close";
			closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
			// 
			// toolsToolStripMenuItem
			// 
			toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { addToolStripMenuItem, cypticExplorerToolStripMenuItem, cheatToolStripMenuItem, clueDatabaseToolStripMenuItem, toolStripSeparator6, settingsToolStripMenuItem });
			toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			toolsToolStripMenuItem.Text = "Tools";
			// 
			// addToolStripMenuItem
			// 
			addToolStripMenuItem.Name = "addToolStripMenuItem";
			addToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			addToolStripMenuItem.Text = "New Scan";
			addToolStripMenuItem.Click += addToolStripMenuItem_Click;
			// 
			// cypticExplorerToolStripMenuItem
			// 
			cypticExplorerToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("cypticExplorerToolStripMenuItem.Image");
			cypticExplorerToolStripMenuItem.Name = "cypticExplorerToolStripMenuItem";
			cypticExplorerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
			cypticExplorerToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			cypticExplorerToolStripMenuItem.Text = "Cryptic Explorer";
			cypticExplorerToolStripMenuItem.Click += cypticExplorerToolStripMenuItem_Click;
			// 
			// cheatToolStripMenuItem
			// 
			cheatToolStripMenuItem.Name = "cheatToolStripMenuItem";
			cheatToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			cheatToolStripMenuItem.Text = "All Solution";
			cheatToolStripMenuItem.ToolTipText = "Select image file of solutions, if any";
			cheatToolStripMenuItem.Click += cheatToolStripMenuItem_Click;
			// 
			// clueDatabaseToolStripMenuItem
			// 
			clueDatabaseToolStripMenuItem.Name = "clueDatabaseToolStripMenuItem";
			clueDatabaseToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			clueDatabaseToolStripMenuItem.Text = "Clue Database";
			clueDatabaseToolStripMenuItem.Click += clueDatabaseToolStripMenuItem_Click;
			// 
			// toolStripSeparator6
			// 
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(194, 6);
			// 
			// settingsToolStripMenuItem
			// 
			settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			settingsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			settingsToolStripMenuItem.Text = "Settings";
			settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
			// 
			// helpToolStripMenuItem
			// 
			helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { featureToolStripMenuItem, helpToolStripMenuItem1, toolStripSeparator7, aboutToolStripMenuItem });
			helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			helpToolStripMenuItem.Text = "Help";
			// 
			// featureToolStripMenuItem
			// 
			featureToolStripMenuItem.Name = "featureToolStripMenuItem";
			featureToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			featureToolStripMenuItem.Text = "Features";
			featureToolStripMenuItem.Click += featureToolStripMenuItem_Click;
			// 
			// helpToolStripMenuItem1
			// 
			helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
			helpToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
			helpToolStripMenuItem1.Text = "Help";
			helpToolStripMenuItem1.Click += helpToolStripMenuItem1_Click;
			// 
			// toolStripSeparator7
			// 
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(115, 6);
			// 
			// aboutToolStripMenuItem
			// 
			aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			aboutToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			aboutToolStripMenuItem.Text = "About";
			aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
			// 
			// dataGridView1
			// 
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dataGridView1.ColumnHeadersVisible = false;
			dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
			dataGridView1.Location = new System.Drawing.Point(181, 27);
			dataGridView1.MultiSelect = false;
			dataGridView1.Name = "dataGridView1";
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
			dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			dataGridView1.ShowCellErrors = false;
			dataGridView1.ShowCellToolTips = false;
			dataGridView1.ShowEditingIcon = false;
			dataGridView1.ShowRowErrors = false;
			dataGridView1.Size = new System.Drawing.Size(605, 455);
			dataGridView1.TabIndex = 3;
			// 
			// BtnPrev
			// 
			BtnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnPrev.Location = new System.Drawing.Point(10, 366);
			BtnPrev.Name = "BtnPrev";
			BtnPrev.Size = new System.Drawing.Size(50, 54);
			BtnPrev.TabIndex = 14;
			BtnPrev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			BtnPrev.UseVisualStyleBackColor = true;
			BtnPrev.Click += bPrev_Click_1;
			// 
			// BtnNext
			// 
			BtnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnNext.Location = new System.Drawing.Point(119, 366);
			BtnNext.Name = "BtnNext";
			BtnNext.RightToLeft = System.Windows.Forms.RightToLeft.No;
			BtnNext.Size = new System.Drawing.Size(50, 54);
			BtnNext.TabIndex = 15;
			BtnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			BtnNext.UseVisualStyleBackColor = true;
			BtnNext.Click += bNext_Click_1;
			// 
			// tbSolnStatus
			// 
			tbSolnStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			tbSolnStatus.Location = new System.Drawing.Point(105, 338);
			tbSolnStatus.Name = "tbSolnStatus";
			tbSolnStatus.Size = new System.Drawing.Size(60, 22);
			tbSolnStatus.TabIndex = 16;
			tbSolnStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			tbSolnStatus.MouseClick += tbSolnStatus_MouseClick;
			// 
			// AcrossSeparator
			// 
			AcrossSeparator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			AcrossSeparator.Location = new System.Drawing.Point(87, 162);
			AcrossSeparator.Name = "AcrossSeparator";
			AcrossSeparator.Size = new System.Drawing.Size(71, 33);
			AcrossSeparator.TabIndex = 17;
			toolTip1.SetToolTip(AcrossSeparator, "Add/remove multi-word DOWN separator");
			AcrossSeparator.UseVisualStyleBackColor = true;
			AcrossSeparator.Click += AcrossSeparator_Click;
			// 
			// DownSeparator
			// 
			DownSeparator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			DownSeparator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			DownSeparator.Location = new System.Drawing.Point(11, 162);
			DownSeparator.Name = "DownSeparator";
			DownSeparator.Size = new System.Drawing.Size(75, 33);
			DownSeparator.TabIndex = 18;
			toolTip1.SetToolTip(DownSeparator, "Add/remove multi-word ACROSS separator");
			DownSeparator.UseVisualStyleBackColor = true;
			DownSeparator.Click += DownSeparator_Click;
			// 
			// BtnUpdate
			// 
			BtnUpdate.BackColor = System.Drawing.SystemColors.Control;
			BtnUpdate.Location = new System.Drawing.Point(13, 425);
			BtnUpdate.Name = "BtnUpdate";
			BtnUpdate.Size = new System.Drawing.Size(71, 34);
			BtnUpdate.TabIndex = 19;
			BtnUpdate.Text = "Save";
			BtnUpdate.UseVisualStyleBackColor = false;
			BtnUpdate.Click += BtnUpdate_Click_1;
			// 
			// BtnJump
			// 
			BtnJump.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnJump.Location = new System.Drawing.Point(82, 315);
			BtnJump.Margin = new System.Windows.Forms.Padding(0);
			BtnJump.Name = "BtnJump";
			BtnJump.Size = new System.Drawing.Size(41, 20);
			BtnJump.TabIndex = 21;
			BtnJump.UseVisualStyleBackColor = true;
			BtnJump.Click += BtnJump_Click_1;
			// 
			// referenceTextBox
			// 
			referenceTextBox.Location = new System.Drawing.Point(17, 292);
			referenceTextBox.Name = "referenceTextBox";
			referenceTextBox.Size = new System.Drawing.Size(145, 23);
			referenceTextBox.TabIndex = 22;
			toolTip1.SetToolTip(referenceTextBox, "Reference to unique Crossword No or a date");
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.White;
			label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(10, 256);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(68, 15);
			label1.TabIndex = 23;
			label1.Text = "Crossword:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.BackColor = System.Drawing.Color.White;
			label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(9, 338);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(86, 15);
			label2.TabIndex = 24;
			label2.Text = "Solved Status:";
			// 
			// TbWordLookUp
			// 
			TbWordLookUp.Location = new System.Drawing.Point(17, 233);
			TbWordLookUp.Name = "TbWordLookUp";
			TbWordLookUp.Size = new System.Drawing.Size(99, 23);
			TbWordLookUp.TabIndex = 28;
			toolTip1.SetToolTip(TbWordLookUp, "Enter word or phrase to Google");
			// 
			// BtnGoogle
			// 
			BtnGoogle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnGoogle.Location = new System.Drawing.Point(119, 233);
			BtnGoogle.Name = "BtnGoogle";
			BtnGoogle.Size = new System.Drawing.Size(43, 23);
			BtnGoogle.TabIndex = 29;
			BtnGoogle.UseVisualStyleBackColor = true;
			BtnGoogle.Click += BtnGoogle_Click;
			// 
			// BtnLoadExplorer
			// 
			BtnLoadExplorer.Location = new System.Drawing.Point(47, 107);
			BtnLoadExplorer.Name = "BtnLoadExplorer";
			BtnLoadExplorer.Size = new System.Drawing.Size(67, 36);
			BtnLoadExplorer.TabIndex = 32;
			toolTip1.SetToolTip(BtnLoadExplorer, "To load clue explorer to assist in matching clue to the likely solution with various online lookups\r\nAlso link to LLM AI and database of clue meanings\r\n ");
			BtnLoadExplorer.UseVisualStyleBackColor = true;
			BtnLoadExplorer.Click += BtnLoadExplorer_Click;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.BackColor = System.Drawing.Color.White;
			label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(9, 34);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(97, 15);
			label3.TabIndex = 33;
			label3.Text = "Scan Crossword:";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.BackColor = System.Drawing.Color.White;
			label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(9, 90);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(84, 15);
			label4.TabIndex = 34;
			label4.Text = "Clue Explorer:";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.BackColor = System.Drawing.Color.White;
			label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(9, 146);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(88, 15);
			label5.TabIndex = 35;
			label5.Text = "Solution Entry:";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.BackColor = System.Drawing.Color.White;
			label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(9, 217);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(140, 15);
			label6.TabIndex = 36;
			label6.Text = "Word or Phrase Lookup:";
			// 
			// BtnScratchPad
			// 
			BtnScratchPad.BackColor = System.Drawing.Color.Transparent;
			BtnScratchPad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			BtnScratchPad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			BtnScratchPad.ForeColor = System.Drawing.Color.Black;
			BtnScratchPad.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			BtnScratchPad.Location = new System.Drawing.Point(5, 5);
			BtnScratchPad.Name = "BtnScratchPad";
			BtnScratchPad.Size = new System.Drawing.Size(165, 25);
			BtnScratchPad.TabIndex = 39;
			BtnScratchPad.Text = "Show Scratchpad";
			toolTip1.SetToolTip(BtnScratchPad, "Will Open/Close the Scratchpad");
			BtnScratchPad.UseVisualStyleBackColor = false;
			BtnScratchPad.Click += BtnScratchPad_Click;
			// 
			// DataGridScratchPad
			// 
			DataGridScratchPad.AllowUserToAddRows = false;
			DataGridScratchPad.AllowUserToResizeColumns = false;
			DataGridScratchPad.AllowUserToResizeRows = false;
			DataGridScratchPad.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			DataGridScratchPad.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			DataGridScratchPad.BackgroundColor = System.Drawing.Color.FromArgb(255, 255, 192);
			DataGridScratchPad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			DataGridScratchPad.ColumnHeadersVisible = false;
			DataGridScratchPad.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Column1 });
			DataGridScratchPad.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			DataGridScratchPad.EnableHeadersVisualStyles = false;
			DataGridScratchPad.GridColor = System.Drawing.SystemColors.ActiveBorder;
			DataGridScratchPad.Location = new System.Drawing.Point(8, 30);
			DataGridScratchPad.MultiSelect = false;
			DataGridScratchPad.Name = "DataGridScratchPad";
			DataGridScratchPad.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			DataGridScratchPad.RowHeadersVisible = false;
			DataGridScratchPad.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			DataGridScratchPad.ScrollBars = System.Windows.Forms.ScrollBars.None;
			DataGridScratchPad.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			DataGridScratchPad.ShowCellErrors = false;
			DataGridScratchPad.ShowCellToolTips = false;
			DataGridScratchPad.ShowRowErrors = false;
			DataGridScratchPad.Size = new System.Drawing.Size(160, 295);
			DataGridScratchPad.TabIndex = 40;
			// 
			// Column1
			// 
			Column1.HeaderText = "Column1";
			Column1.Name = "Column1";
			Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.BackColor = System.Drawing.Color.White;
			label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(9, 350);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(85, 13);
			label8.TabIndex = 41;
			label8.Text = "(Click to update)";
			// 
			// NoCrossword
			// 
			NoCrossword.AutoSize = true;
			NoCrossword.BackColor = System.Drawing.Color.White;
			NoCrossword.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			NoCrossword.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			NoCrossword.Location = new System.Drawing.Point(285, 224);
			NoCrossword.Name = "NoCrossword";
			NoCrossword.Size = new System.Drawing.Size(376, 40);
			NoCrossword.TabIndex = 58;
			NoCrossword.Text = "Scan or Import Crossword";
			// 
			// RadioPen
			// 
			RadioPen.AutoSize = true;
			RadioPen.Checked = true;
			RadioPen.Location = new System.Drawing.Point(37, 196);
			RadioPen.Name = "RadioPen";
			RadioPen.Size = new System.Drawing.Size(45, 19);
			RadioPen.TabIndex = 59;
			RadioPen.TabStop = true;
			RadioPen.Text = "Pen";
			RadioPen.UseVisualStyleBackColor = true;
			RadioPen.CheckedChanged += RadioPen_CheckedChanged;
			// 
			// RadioPencil
			// 
			RadioPencil.AutoSize = true;
			RadioPencil.Location = new System.Drawing.Point(87, 196);
			RadioPencil.Name = "RadioPencil";
			RadioPencil.Size = new System.Drawing.Size(57, 19);
			RadioPencil.TabIndex = 60;
			RadioPencil.TabStop = true;
			RadioPencil.Text = "Pencil";
			RadioPencil.UseVisualStyleBackColor = true;
			RadioPencil.CheckedChanged += RadioPencil_CheckedChanged;
			// 
			// TbRichTextDownClues
			// 
			TbRichTextDownClues.BorderStyle = System.Windows.Forms.BorderStyle.None;
			TbRichTextDownClues.Location = new System.Drawing.Point(979, 44);
			TbRichTextDownClues.Margin = new System.Windows.Forms.Padding(0);
			TbRichTextDownClues.Name = "TbRichTextDownClues";
			TbRichTextDownClues.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			TbRichTextDownClues.Size = new System.Drawing.Size(183, 437);
			TbRichTextDownClues.TabIndex = 63;
			TbRichTextDownClues.Text = "";
			// 
			// TbRichTextAcrossClues
			// 
			TbRichTextAcrossClues.BorderStyle = System.Windows.Forms.BorderStyle.None;
			TbRichTextAcrossClues.Location = new System.Drawing.Point(794, 44);
			TbRichTextAcrossClues.Margin = new System.Windows.Forms.Padding(0);
			TbRichTextAcrossClues.Name = "TbRichTextAcrossClues";
			TbRichTextAcrossClues.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			TbRichTextAcrossClues.Size = new System.Drawing.Size(183, 437);
			TbRichTextAcrossClues.TabIndex = 62;
			TbRichTextAcrossClues.Text = "";
			// 
			// label13
			// 
			label13.BackColor = System.Drawing.Color.Transparent;
			label13.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
			label13.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			label13.Location = new System.Drawing.Point(977, 27);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(180, 16);
			label13.TabIndex = 65;
			label13.Text = "DOWN";
			label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			label13.Click += label13_Click;
			// 
			// label14
			// 
			label14.BackColor = System.Drawing.Color.Transparent;
			label14.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
			label14.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			label14.Location = new System.Drawing.Point(792, 27);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(180, 16);
			label14.TabIndex = 65;
			label14.Text = "ACROSS";
			label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// BtnCWInfo
			// 
			BtnCWInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnCWInfo.Font = new System.Drawing.Font("Snap ITC", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			BtnCWInfo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			BtnCWInfo.Location = new System.Drawing.Point(46, 315);
			BtnCWInfo.Margin = new System.Windows.Forms.Padding(0);
			BtnCWInfo.Name = "BtnCWInfo";
			BtnCWInfo.Size = new System.Drawing.Size(31, 20);
			BtnCWInfo.TabIndex = 67;
			BtnCWInfo.UseVisualStyleBackColor = true;
			BtnCWInfo.Click += BtnCWInfo_Click;
			// 
			// BtnCWKeys
			// 
			BtnCWKeys.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			BtnCWKeys.Font = new System.Drawing.Font("Script MT Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			BtnCWKeys.Location = new System.Drawing.Point(130, 142);
			BtnCWKeys.Margin = new System.Windows.Forms.Padding(0);
			BtnCWKeys.Name = "BtnCWKeys";
			BtnCWKeys.Size = new System.Drawing.Size(24, 20);
			BtnCWKeys.TabIndex = 68;
			BtnCWKeys.UseVisualStyleBackColor = true;
			BtnCWKeys.Click += BtnCWKeys_Click;
			// 
			// CbHints
			// 
			CbHints.AutoSize = true;
			CbHints.Location = new System.Drawing.Point(98, 274);
			CbHints.Name = "CbHints";
			CbHints.Size = new System.Drawing.Size(54, 19);
			CbHints.TabIndex = 73;
			CbHints.Text = "Hints";
			CbHints.UseVisualStyleBackColor = true;
			CbHints.Click += CbHints_Click;
			// 
			// CbSolns
			// 
			CbSolns.AutoSize = true;
			CbSolns.Location = new System.Drawing.Point(18, 274);
			CbSolns.Name = "CbSolns";
			CbSolns.Size = new System.Drawing.Size(70, 19);
			CbSolns.TabIndex = 72;
			CbSolns.Text = "Solution";
			CbSolns.UseVisualStyleBackColor = true;
			CbSolns.Click += CbSolns_Click;
			// 
			// panel1
			// 
			panel1.Controls.Add(label2);
			panel1.Controls.Add(BtnScratchPad);
			panel1.Controls.Add(DataGridScratchPad);
			panel1.Controls.Add(CbSolns);
			panel1.Controls.Add(CbHints);
			panel1.Controls.Add(bClose);
			panel1.Controls.Add(BtnImport);
			panel1.Controls.Add(BtnCWKeys);
			panel1.Controls.Add(tbCWNo);
			panel1.Controls.Add(BtnCWInfo);
			panel1.Controls.Add(tbTotalCW);
			panel1.Controls.Add(BtnPrev);
			panel1.Controls.Add(BtnNext);
			panel1.Controls.Add(tbSolnStatus);
			panel1.Controls.Add(AcrossSeparator);
			panel1.Controls.Add(RadioPencil);
			panel1.Controls.Add(DownSeparator);
			panel1.Controls.Add(BtnUpdate);
			panel1.Controls.Add(RadioPen);
			panel1.Controls.Add(BtnJump);
			panel1.Controls.Add(referenceTextBox);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(label6);
			panel1.Controls.Add(TbWordLookUp);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(BtnGoogle);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(BtnLoadExplorer);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(label7);
			panel1.Location = new System.Drawing.Point(-1, 27);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(181, 464);
			panel1.TabIndex = 74;
			// 
			// label7
			// 
			label7.BackColor = System.Drawing.Color.White;
			label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			label7.Location = new System.Drawing.Point(3, 2);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(172, 460);
			label7.TabIndex = 61;
			// 
			// imageList1
			// 
			imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			imageList1.ImageSize = new System.Drawing.Size(16, 16);
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// Form1
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			ClientSize = new System.Drawing.Size(1166, 494);
			Controls.Add(menuStrip1);
			Controls.Add(panel1);
			Controls.Add(TbRichTextDownClues);
			Controls.Add(TbRichTextAcrossClues);
			Controls.Add(label13);
			Controls.Add(label14);
			Controls.Add(NoCrossword);
			Controls.Add(dataGridView1);
			Controls.Add(PictureBoxClues);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MainMenuStrip = menuStrip1;
			MaximizeBox = false;
			Name = "Form1";
			SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Crossword Manager";
			Load += Form1_Load;
			KeyDown += Form1_KeyDown;
			((System.ComponentModel.ISupportInitialize)PictureBoxClues).EndInit();
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)DataGridScratchPad).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button BtnImport;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.Button AcrossSeparator;
        private System.Windows.Forms.Button DownSeparator;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox TbWordLookUp;
		private System.Windows.Forms.Button BtnGoogle;
		private System.Windows.Forms.Button BtnLoadExplorer;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
		private System.Windows.Forms.Button BtnScratchPad;
		private System.Windows.Forms.DataGridView DataGridScratchPad;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.ToolStripMenuItem exportSolutionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importSolutionToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.Label label8;
		public System.Windows.Forms.TextBox referenceTextBox;
		public System.Windows.Forms.TextBox tbTotalCW;
		public System.Windows.Forms.PictureBox PictureBoxClues;
		public System.Windows.Forms.TextBox tbCWNo;
		public System.Windows.Forms.Button BtnUpdate;
		public System.Windows.Forms.Button BtnJump;
		public System.Windows.Forms.Label NoCrossword;
		private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.RadioButton RadioPen;
		private System.Windows.Forms.RadioButton RadioPencil;
		public System.Windows.Forms.RichTextBox TbRichTextDownClues;
		public System.Windows.Forms.RichTextBox TbRichTextAcrossClues;
		private System.Windows.Forms.ToolStripMenuItem featureToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		public System.Windows.Forms.TextBox tbSolnStatus;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		public System.Windows.Forms.Button BtnCWInfo;
		public System.Windows.Forms.Button button1;
		public System.Windows.Forms.Button BtnCWKeys;
		private System.Windows.Forms.CheckBox CbHints;
		private System.Windows.Forms.CheckBox CbSolns;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cypticExplorerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cheatToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clueDatabaseToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ImageList imageList1;
	}
}

