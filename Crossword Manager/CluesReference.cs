using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Crossword_Filler
{
	public partial class CluesReference : Form
	{
		private string csvFilePath = Path.Combine(Application.StartupPath, "clue reference.csv");
		private Dictionary<string, List<string>> dataRecords = new Dictionary<string, List<string>>();

		public CluesReference()
		{
			InitializeComponent();
			this.AutoScaleMode = AutoScaleMode.Dpi;
			LbClueSelect.SelectedIndexChanged += LbClueSelect_SelectedIndexChanged;
			LoadCsvData();
			PopulateComboBox();
		}

		private void LoadCsvData()
		{
			if (!System.IO.File.Exists(csvFilePath))
			{
				MessageBox.Show("clue reference.csv file not found in Application Folder. Creating an empty one");
				System.IO.File.WriteAllText(csvFilePath, "Index,Value" + Environment.NewLine);
				return;
			}
			var lines = System.IO.File.ReadAllLines(csvFilePath).Skip(1); // Skip header
			foreach (var line in lines)
			{
				var fields = line.Split(',');
				var index = fields[0].Trim();
				var value = fields[1].Trim();

				if (!dataRecords.ContainsKey(index))
				{
					dataRecords[index] = new List<string>();
				}
				dataRecords[index].Add(value);
			}
		}
		private void PopulateComboBox()
		{
			var uniqueIndices = dataRecords.Keys.OrderBy(i => i).ToList();
			LbClueSelect.DataSource = uniqueIndices;
		}
		private void btnAdd_Click(object sender, EventArgs e)
		{
			var newIndex = TbClueText.Text.Trim();
			var newValue = TbClueDefinition.Text.Trim();

			if (dataRecords.ContainsKey(newIndex))
			{
				LbClueSelect.SelectedItem = newIndex;
				var existingValues = string.Join(",", dataRecords[newIndex]);
				var confirmResult = MessageBox.Show(
					$"Already exists. Do you want to still add?",
					"Confirm Add",
					MessageBoxButtons.YesNo);

				if (confirmResult == DialogResult.No)
					return;
			}

			// Add the new record
			if (!dataRecords.ContainsKey(newIndex))
			{
				dataRecords[newIndex] = new List<string>();
			}
			dataRecords[newIndex].Add(newValue);

			// Append to CSV file
			using (var writer = new StreamWriter(csvFilePath, true))
			{
				writer.WriteLine($"{newIndex},{newValue}");
			}

			// Refresh ComboBox
			PopulateComboBox();
			//var selectedIndex = newIndex;
			//if (dataRecords.ContainsKey(selectedIndex))
			//{
			//	TbClueInfo.Text = string.Join(Environment.NewLine, dataRecords[selectedIndex]);
			//}
			TbClueDefinition.Text = "";
			LbClueSelect.SelectedItem = newIndex;
			// TbClueDefinition.Clear();
		}
		private void BtnDelete_Click(object sender, EventArgs e)
		{
			string newIdx;
			int selectionStart = TbClueLookUp.SelectionStart;
			int selectedLineIndex = TbClueLookUp.GetLineFromCharIndex(selectionStart);
			string selectedLine;
			if (TbClueLookUp.Lines.Count() == 0)
			{
				selectedLine = "";
			}
			else
			{
				selectedLine = TbClueLookUp.Lines[selectedLineIndex];
			}
			var selectedIndex = LbClueSelect.SelectedItem.ToString();
			newIdx = "";
			if (selectedLine != "")
			{
				var existingValues = string.Join(",", dataRecords[selectedIndex]);
				string newValues = existingValues.Replace(selectedLine, "").Replace(",,", ",").Trim(' ');
				if (newValues == "")
				{
					dataRecords.Remove(selectedIndex);
				}
				else
				{
					dataRecords[selectedIndex] = newValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
									  .Select(v => v.Trim())
									  .Where(v => !string.IsNullOrWhiteSpace(v))
									  .ToList();
					// dataRecords[selectedIndex] = newValues.Split(',').ToList();
					newIdx = selectedIndex;
				}
			}
			else
			{
				dataRecords.Remove(selectedIndex);
			}
			PopulateComboBox();
			if (newIdx != "")
			{
				LbClueSelect.SelectedItem = newIdx;
			}
			writeToCSV();
		}
		private void TbLookup_TextChanged(object sender, EventArgs e)
		{
			string wd = TbLookup.Text;
			if (dataRecords.ContainsKey(wd))
			{
				LbClueSelect.Text = wd;
			}
		}
		private void LbClueSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selectedIndex = LbClueSelect.SelectedItem.ToString();
			if (dataRecords.ContainsKey(selectedIndex))
			{
				TbClueLookUp.Text = string.Join(Environment.NewLine, dataRecords[selectedIndex]);
			}
		}
		private void writeToCSV()
		{
			string header = "Key,Value";
			var csvLines = new List<string> { header };
			foreach (var kvp in dataRecords)
			{
				for (int i = 0; i < kvp.Value.Count; i++)
				{
					string keyAndValues = kvp.Key + "," + kvp.Value[i];
					csvLines.Add(keyAndValues);
				}
			}
			System.IO.File.WriteAllLines(csvFilePath, csvLines);
		}
		private void BtnClose_Click(object sender, EventArgs e)
		{
			Close();
		}


	}
}
