using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword_Filler
{
	public class PuzFileSaver
	{
		public async Task Save(CrosswordPuzzle puzzle, string filePath)
		{
			await Task.Run(() =>
			{
				if (puzzle.PuzSolutionGrid == null || puzzle.PuzUserGrid == null)
				{
					throw new InvalidOperationException("Puz-specific grid data is missing.");
				}

				// --- Declare variables in a higher scope ---
				int gridSize = puzzle.Dimensions.Width * puzzle.Dimensions.Height;
				var solutionBytes = new byte[gridSize];
				var userBytes = new byte[gridSize];

				//string[] allClues = puzzle.Clues.Across.Select(c => c.Text)
				//					   .Concat(puzzle.Clues.Down.Select(c => c.Text))
				//					   .ToArray();

				var allClues = puzzle.Clues.Across.Select(c => new { Clue = c, Direction = "Across" })
					.Concat(puzzle.Clues.Down.Select(c => new { Clue = c, Direction = "Down" }))
					.OrderBy(c => int.Parse(c.Clue.Number)) // Sort numerically by clue number
					.ThenBy(c => c.Direction) // Sort Across before Down
					.ToList();


				//string[] allClues = puzzle.Clues.Across
				//					.Select(c => $"{c.Number}. {c.Text}") // Format for Across clues
				//					.Concat(puzzle.Clues.Down
				//					.Select(c => $"{c.Number}. {c.Text}")) // Format for Down clues
				//					.ToArray();

				// Extract only the text, as the .puz format doesn't store the numbers in the clue strings
				string[] sortedClueTexts = allClues.Select(c => c.Clue.Text).ToArray();

				// --- 1. Collect all content data in a MemoryStream ---
				var contentStream = new MemoryStream();
				using (var contentWriter = new BinaryWriter(contentStream, Encoding.GetEncoding("ISO-8859-1")))
				{
					// Write the grid data
					for (int r = 0; r < puzzle.Dimensions.Height; r++)
					{
						for (int c = 0; c < puzzle.Dimensions.Width; c++)
						{
							solutionBytes[r * puzzle.Dimensions.Width + c] = (byte)puzzle.PuzSolutionGrid[r, c];
							userBytes[r * puzzle.Dimensions.Width + c] = (byte)puzzle.PuzUserGrid[r, c];
						}
					}
					contentWriter.Write(solutionBytes);
					contentWriter.Write(userBytes);

					// Write the string sections
					WriteNullTerminatedString(contentWriter, puzzle.Title);
					WriteNullTerminatedString(contentWriter, puzzle.Author);
					WriteNullTerminatedString(contentWriter, puzzle.Copyright);

					foreach (var clueText in sortedClueTexts)
					{
						WriteNullTerminatedString(contentWriter, clueText);
						// Console.WriteLine(clueText);
					}
					WriteNullTerminatedString(contentWriter, puzzle.Notes);
				}
				byte[] contentBytes = contentStream.ToArray();

				// --- 2. Calculate checksums ---
				ushort cibChecksum = ChecksumRegion(new byte[] {
					(byte)puzzle.Dimensions.Width,
					(byte)puzzle.Dimensions.Height,
					(byte)((sortedClueTexts.Length) & 0xFF),
					(byte)(((sortedClueTexts.Length) >> 8) & 0xFF)
				}, 4, 0);

				ushort solutionChecksum = ChecksumRegion(solutionBytes, gridSize, 0);
				ushort gridChecksum = ChecksumRegion(userBytes, gridSize, 0);
				ushort partialChecksum = CalculatePartialChecksum(puzzle);

				// --- 3. Build the header and final file ---
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				using (var writer = new BinaryWriter(fileStream, Encoding.GetEncoding("ISO-8859-1")))
				{
					// Write header fields at correct offsets
					writer.Write((ushort)0); // Placeholder for Global checksum
					writer.Write(Encoding.GetEncoding("ISO-8859-1").GetBytes("ACROSS&DOWN\0"));
					writer.Write(new byte[] {
						(byte)(0x49 ^ (cibChecksum & 0xFF)),
						(byte)(0x43 ^ (solutionChecksum & 0xFF)),
						(byte)(0x48 ^ (gridChecksum & 0xFF)),
						(byte)(0x45 ^ (partialChecksum & 0xFF)),
						(byte)(0x41 ^ ((cibChecksum & 0xFF00) >> 8)),
						(byte)(0x54 ^ ((solutionChecksum & 0xFF00) >> 8)),
						(byte)(0x45 ^ ((gridChecksum & 0xFF00) >> 8)),
						(byte)(0x44 ^ ((partialChecksum & 0xFF00) >> 8))
					});
					writer.Write(Encoding.GetEncoding("ISO-8859-1").GetBytes("1.2\0"));
					writer.Write(new byte[2]); // Reserved bytes
					writer.Write((ushort)0); // Scrambled checksum
					writer.Write(new byte[8]); // Reserved bytes

					// Add additional reserved bytes to align with offset 0x2C
					writer.Write(new byte[6]); // Two additional reserved bytes

					// Write width and height (now at 0x2C)
					writer.Write((byte)puzzle.Dimensions.Width); // Width
					writer.Write((byte)puzzle.Dimensions.Height); // Height
					writer.Write((ushort)sortedClueTexts.Length); // Clue count as ushort

					writer.Write(new byte[4]); // before grid starts
											   // Write the prepared content
					writer.Write(contentBytes); // Write the grid and clues content

					// --- 4. Final checksum ---
					fileStream.Seek(0, SeekOrigin.Begin);
					using (var reader = new BinaryReader(fileStream))
					{
						byte[] finalFileContent = reader.ReadBytes((int)fileStream.Length);
						ushort finalGlobalChecksum = ChecksumRegion(finalFileContent, (int)fileStream.Length, 0);
						writer.Seek(0, SeekOrigin.Begin);
						writer.Write(finalGlobalChecksum);
					}
				}
			});
		}

		private static ushort ChecksumRegion(byte[] data, int length, ushort initialChecksum)
		{
			ushort cksum = initialChecksum;
			for (int i = 0; i < length; i++)
			{
				if ((cksum & 0x0001) > 0)
				{
					cksum = (ushort)((cksum >> 1) + 0x8000);
				}
				else
				{
					cksum = (ushort)(cksum >> 1);
				}
				cksum += data[i];
			}
			return cksum;
		}

		private static ushort CalculatePartialChecksum(CrosswordPuzzle puzzle)
		{
			ushort partialChecksum = 0x0000;
			byte[] titleBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(puzzle.Title + "\0");
			partialChecksum = ChecksumRegion(titleBytes, titleBytes.Length, partialChecksum);
			byte[] authorBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(puzzle.Author + "\0");
			partialChecksum = ChecksumRegion(authorBytes, authorBytes.Length, partialChecksum);
			byte[] copyrightBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(puzzle.Copyright + "\0");
			partialChecksum = ChecksumRegion(copyrightBytes, copyrightBytes.Length, partialChecksum);
			foreach (var clueText in puzzle.Clues.Across.Select(c => c.Text).Concat(puzzle.Clues.Down.Select(c => c.Text)))
			{
				byte[] clueBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(clueText + "\0");
				partialChecksum = ChecksumRegion(clueBytes, clueBytes.Length, partialChecksum);
			}
			if (!string.IsNullOrEmpty(puzzle.Notes))
			{
				byte[] notesBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(puzzle.Notes + "\0");
				partialChecksum = ChecksumRegion(notesBytes, notesBytes.Length, partialChecksum);
			}
			return partialChecksum;
		}

		private static void WriteNullTerminatedString(BinaryWriter writer, string value)
		{
			if (value != null)
			{
				writer.Write(Encoding.GetEncoding("ISO-8859-1").GetBytes(value));
			}
			writer.Write((byte)0x00);
		}


	}
}


