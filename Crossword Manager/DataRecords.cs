using System.Collections.Generic;

namespace Crossword_Filler
{
	public class CellDataRecord
	{
		public int Index { get; set; }
		public string Reference { get; set; } // ; separated text for Author, Copyright, Title and Reference 
		public int GridSize { get; set; } // rows and columns are the same
		public string ScratchPad { get; set; } = ""; // ; separated list for listbox
		public string Ticks { get; set; } = ""; // , separated x/y screen coords
		public string CluesAcross { get; set; } = "";
		public string CluesDown { get; set; } = "";
		public string HintsAcross { get; set; } = ""; // or Notes
		public string HintsDown { get; set; } = "";


		public CellState[,] CellData { get; set; } // grid of CellState objects

		public CellDataRecord(int rowCnt, int colCnt)
		{
			// Initialize the grid of CellState
			CellData = new CellState[rowCnt, colCnt];
			for (int row = 0; row < rowCnt; row++)
			{
				for (int col = 0; col < colCnt; col++)
				{
					CellData[row, col] = new CellState(); // Initialize each cell
				}
			}
		}
	}
	public class CellState
	{
		public string Value { get; set; } // letter - active soln, letter+"*" if text is grey, "#" for black, " " for blank
		public string Solution { get; set; } // Change to string Solution answers, "#" for black
		public int WordSeparator { get; set; } // Change to Int WordSeparator 0 - none, 1 - bottom, 2 - right?
		public string Notes { get; set; } // Spare per cell

		public CellState()
		{
			Value = ""; // Default value
			Solution = ""; // Default value
			WordSeparator = 0; // No line
			Notes = ""; // Default value
		}
	}
	public class DataManager
	{
		public List<CellDataRecord> Records { get; set; }
		public int CurrentIndex { get; set; }
		public DataManager()
		{
			Records = new List<CellDataRecord>();
			CurrentIndex = -1;
		}
		public void AddRecord(CellDataRecord record)
		{
			Records.Add(record);
			CurrentIndex = Records.Count - 1; // Move to the new record
		}
		public void UpdateRecord(CellDataRecord updatedRecord)
		{
			if (CurrentIndex >= 0 && CurrentIndex < Records.Count)
			{
				Records[CurrentIndex] = updatedRecord;
			}
		}
		public void LoadNext()
		{
			if (CurrentIndex < Records.Count - 1)
			{
				CurrentIndex++;
			}

		}
		public void LoadPrev()
		{
			if (CurrentIndex > 0)
			{
				CurrentIndex--;
			}
		}
		public void LoadSame()
		{
			if (CurrentIndex > 0)
			{
				CurrentIndex = CurrentIndex;
			}
		}
		public CellDataRecord GetCurrentRecord()
		{
			if (CurrentIndex >= 0 && CurrentIndex < Records.Count)
			{
				return Records[CurrentIndex];
			}
			return null;
		}
		public int TotalCount => Records.Count;
	}
}
