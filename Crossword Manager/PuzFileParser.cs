using System.Collections.Generic;
using System.IO;
using System.Linq; // Add this using directive for the Select method
using System.Text;
using System.Threading.Tasks;

namespace Crossword_Filler
{
	public class PuzFileParser : IPuzzleParser
	{
		// These classes are now nested inside PuzFileParser,
		// so they can be declared as private.
		private class PuzCrossword
		{
			public int Width { get; set; }
			public int Height { get; set; }
			public string Title { get; set; }
			public string Author { get; set; }
			public string Copyright { get; set; }
			public string[] Clues { get; set; }
			public string Notes { get; set; }
			public char[,] Solution { get; set; }
			public char[,] UserGrid { get; set; }
			public List<PuzClue> AcrossClues { get; set; }
			public List<PuzClue> DownClues { get; set; }
		}

		// This is also a nested class now.
		private class PuzClue
		{
			public int Number { get; set; }
			public int StartRow { get; set; }
			public int StartCol { get; set; }
			public string Text { get; set; }
		}

		public async Task<CrosswordPuzzle> Parse(string filePath)
		{
			var puzPuzzle = await Task.Run(() =>
			{
				var tempPuzzle = new PuzCrossword();

				using (var reader = new BinaryReader(File.Open(filePath, FileMode.Open), Encoding.GetEncoding("ISO-8859-1")))
				{
					// --- Header section ---
					// Skip the first bytes to get to the grid dimensions
					// Seek to 0x2C, based on your hex dump
					reader.BaseStream.Seek(0x2C, SeekOrigin.Begin);

					// Read the width, height, and clue count from this new position
					tempPuzzle.Width = reader.ReadByte();
					tempPuzzle.Height = reader.ReadByte();
					int clueCount = reader.ReadInt16();

					// The grid data starts at byte 0x34 (52 bytes)
					reader.BaseStream.Seek(0x34, SeekOrigin.Begin);

					// Correctly reading the grid data
					int gridSize = tempPuzzle.Width * tempPuzzle.Height;
					byte[] solutionBytes = reader.ReadBytes(gridSize);
					byte[] userBytes = reader.ReadBytes(gridSize);

					tempPuzzle.Solution = new char[tempPuzzle.Height, tempPuzzle.Width];
					tempPuzzle.UserGrid = new char[tempPuzzle.Height, tempPuzzle.Width];

					// Convert the byte arrays to char arrays
					for (int r = 0; r < tempPuzzle.Height; r++)
					{
						for (int c = 0; c < tempPuzzle.Width; c++)
						{
							tempPuzzle.Solution[r, c] = (char)solutionBytes[r * tempPuzzle.Width + c];
							tempPuzzle.UserGrid[r, c] = (char)userBytes[r * tempPuzzle.Width + c];
						}
					}

					// --- String sections ---
					tempPuzzle.Title = ReadNullTerminatedString(reader);
					tempPuzzle.Author = ReadNullTerminatedString(reader);
					tempPuzzle.Copyright = ReadNullTerminatedString(reader);

					tempPuzzle.Clues = new string[clueCount];
					for (int i = 0; i < clueCount; i++)
					{
						tempPuzzle.Clues[i] = ReadNullTerminatedString(reader);
					}

					tempPuzzle.Notes = ReadNullTerminatedString(reader);
				}

				(tempPuzzle.AcrossClues, tempPuzzle.DownClues) = DeriveCluesFromGrid(tempPuzzle);
				return tempPuzzle;
			});

			// Map the data from PuzCrossword to the generic CrosswordPuzzle
			var genericPuzzle = new CrosswordPuzzle
			{
				Title = puzPuzzle.Title,
				Author = puzPuzzle.Author,
				Copyright = puzPuzzle.Copyright,
				Notes = puzPuzzle.Notes,
				Dimensions = new Dimensions
				{
					Width = puzPuzzle.Width,
					Height = puzPuzzle.Height
				},
				Clues = new Clues
				{
					Across = puzPuzzle.AcrossClues.Select(c => new Clue { Number = c.Number.ToString(), Text = c.Text }).ToList(),
					Down = puzPuzzle.DownClues.Select(c => new Clue { Number = c.Number.ToString(), Text = c.Text }).ToList()
				},
				PuzSolutionGrid = puzPuzzle.Solution,
				PuzUserGrid = puzPuzzle.UserGrid
			};

			return genericPuzzle;
		}

		private static string ReadNullTerminatedString(BinaryReader reader)
		{
			var stringBuilder = new StringBuilder();
			char c;
			while ((c = reader.ReadChar()) != 0x00)
			{
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		private static (List<PuzClue>, List<PuzClue>) DeriveCluesFromGrid(PuzCrossword puzzle)
		{
			var acrossClues = new List<PuzClue>();
			var downClues = new List<PuzClue>();
			var cellNumber = 1;
			var clueIndex = 0; // A single, sequential index for the raw clue array
							   // string cw="";
			for (int r = 0; r < puzzle.Height; r++)
			{
				for (int c = 0; c < puzzle.Width; c++)
				{
					// bool isAcrossStart = (c == 0 || puzzle.Solution[r, c - 1] == '.') && c < puzzle.Width - 1 && puzzle.Solution[r, c] != '.';
					// bool isDownStart = (r == 0 || puzzle.Solution[r - 1, c] == '.') && r < puzzle.Height - 1 && puzzle.Solution[r, c] != '.';
					bool isAcrossStart = (c == 0 || puzzle.Solution[r, c - 1] == '.') && c < puzzle.Width - 1 && puzzle.Solution[r, c] != '.' && puzzle.Solution[r, c + 1] != '.';
					bool isDownStart = (r == 0 || puzzle.Solution[r - 1, c] == '.') && r < puzzle.Height - 1 && puzzle.Solution[r, c] != '.' && puzzle.Solution[r + 1, c] != '.';
					// cw = cw + puzzle.Solution[r, c];
					if (isAcrossStart || isDownStart)
					{
						if (isAcrossStart)
						{
							if (clueIndex >= puzzle.Clues.Length)
							{
								// This indicates an issue with the file or parsing, but it prevents an out-of-range error.
								// throw new InvalidDataException("Unexpected end of clues array for Across clue.");
								break;
							}
							var acrossClueText = puzzle.Clues[clueIndex];
							acrossClues.Add(new PuzClue { Number = cellNumber, StartRow = r, StartCol = c, Text = acrossClueText });
							clueIndex++;
						}

						if (isDownStart)
						{
							if (clueIndex >= puzzle.Clues.Length)
							{
								// This indicates an issue with the file or parsing.
								// throw new InvalidDataException("Unexpected end of clues array for Down clue.");
								break;
							}
							var downClueText = puzzle.Clues[clueIndex];
							downClues.Add(new PuzClue { Number = cellNumber, StartRow = r, StartCol = c, Text = downClueText });
							clueIndex++;
						}

						cellNumber++;
					}
				}
				// cw = cw + Environment.NewLine;
			}
			// Console.WriteLine(cw);
			// It's a good practice to verify the number of clues read
			//if (clueIndex != puzzle.Clues.Length)
			//{
			//	throw new InvalidDataException("The number of clues in the file does not match the number of clues derived from the grid.");
			//}
			return (acrossClues, downClues);
		}



	}
}





