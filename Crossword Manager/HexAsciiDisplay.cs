using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

public static class HexAsciiDisplay
{
	/// <summary>
	/// Finds "clues" in file, reads up to 128 bytes after it, and renders hex+ASCII into the supplied RichTextBox.
	/// Highlights CR (0x0D) and LF (0x0A) and the UTF-8 ZERO WIDTH SPACE sequence E2 80 8B.
	/// </summary>
	public static void DisplayBytesAfterCluesWithAscii(string filePath, RichTextBox rtb, string searchString)
	{
		//const string searchString = "clues";
		byte[] searchBytes = Encoding.UTF8.GetBytes(searchString);
		byte[] fileBytes;

		try
		{
			fileBytes = File.ReadAllBytes(filePath);
		}
		catch (Exception ex)
		{
			rtb.Text = "Error reading file: " + ex.Message;
			return;
		}

		int found = FindPattern(fileBytes, searchBytes);
		if (found < 0)
		{
			rtb.Text = searchString + " not found in file ." + filePath;
			return;
		}

		int start = found + searchBytes.Length;
		int take = Math.Min(128, Math.Max(0, fileBytes.Length - start));
		byte[] bytes = new byte[take];
		Array.Copy(fileBytes, start, bytes, 0, take);

		// Build text: 16 bytes per line
		const int bytesPerLine = 16;
		var sb = new StringBuilder();
		// mapping arrays: for each byte store hex start index and ascii start index in the final string
		int[] hexStart = new int[bytes.Length];
		int[] hexLen = new int[bytes.Length];
		int[] asciiStart = new int[bytes.Length];
		int[] asciiLen = new int[bytes.Length];

		for (int i = 0; i < bytes.Length; i++)
		{
			if (i % bytesPerLine == 0)
			{
				// If not first line, newline already added; nothing special required
				if (i != 0) sb.AppendLine();
			}

			// record hex start
			hexStart[i] = sb.Length;
			string hex = bytes[i].ToString("X2");
			sb.Append(hex);
			hexLen[i] = 2;

			// append space between hex bytes except maybe at end-of-line to keep layout readable
			if ((i % bytesPerLine) != bytesPerLine - 1 && i != bytes.Length - 1)
			{
				sb.Append(' ');
				hexLen[i] += 1; // account for trailing space in selection length
			}
			else if ((i % bytesPerLine) == bytesPerLine - 1)
			{
				// end of hex group for this line - add two spaces before ASCII column
				sb.Append("  ");
				hexLen[i] += 2;
			}
			else if (i == bytes.Length - 1)
			{
				// last byte of entire dump: add two spaces to separate ASCII column (consistent)
				sb.Append("  ");
				hexLen[i] += 2;
			}

			// If we've finished the line (or last byte), build the ASCII column for this line.
			if ((i % bytesPerLine) == bytesPerLine - 1 || i == bytes.Length - 1)
			{
				// Determine start index of ASCII column for the first byte on this line.
				// Find the first byte index of this line
				int lineFirstIndex = i - (i % bytesPerLine);
				int asciiColumnStart = sb.Length;

				// For each byte on this line, append a single ASCII character representation
				int lineLastIndex = i;
				for (int j = lineFirstIndex; j <= lineLastIndex; j++)
				{
					asciiStart[j] = sb.Length;
					char ch = ToPrintableAscii(bytes[j]);
					if (j == lineFirstIndex)
					{
						sb.AppendLine();
					}
					sb.Append(ch + "  ");
					asciiLen[j] = 1;
				}

				// If the line is not full (less than bytesPerLine), pad ASCII column so selections align if needed
				int missing = bytesPerLine - (lineLastIndex - lineFirstIndex + 1);
				if (missing > 0)
				{
					// pad with spaces to keep columns visually aligned (optional)
					sb.Append(new string(' ', missing));
				}
			}
		}

		// Put text into RichTextBox
		rtb.Clear();
		//Font newFont = new Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size - 4, rtb.SelectionFont.Style);
		//rtb.SelectionFont = newFont;
		rtb.Text = sb.ToString().ToUpper();

		// Reset formatting
		rtb.SelectAll();
		rtb.SelectionColor = rtb.ForeColor;
		rtb.SelectionBackColor = rtb.BackColor;
		rtb.DeselectAll();
		int originalSelectionStart = rtb.SelectionStart;
		int originalSelectionLength = rtb.SelectionLength;
		for (int i = 0; i < rtb.Lines.Length; i++)
		{
			int lineStartIndex = rtb.GetFirstCharIndexFromLine(i);
			int lineLength;
			if (i < rtb.Lines.Length - 1)
			{
				lineLength = rtb.GetFirstCharIndexFromLine(i + 1) - lineStartIndex;
			}
			else
			{
				lineLength = rtb.Text.Length - lineStartIndex;
			}
			rtb.Select(lineStartIndex, lineLength);
			if (i % 2 == 0)
			{
				rtb.SelectionBackColor = Color.White;
			}
			else
			{
				rtb.SelectionBackColor = Color.LawnGreen;
			}
		}
		rtb.Select(originalSelectionStart, originalSelectionLength);
		rtb.ResumeLayout();

		// Highlight CR (0x0D) and LF (0x0A) in both hex and ASCII
		for (int i = 0; i < bytes.Length; i++)
		{
			if (bytes[i] == 0x0D || bytes[i] == 0x0A || bytes[i] == 0x00)
			{
				// hex highlight
				rtb.Select(hexStart[i], hexLen[i]);
				//rtb.SelectionBackColor = Color.Yellow;
				rtb.SelectionColor = Color.Black;

				// ascii highlight (if asciiStart was assigned; it will be assigned)
				rtb.Select(asciiStart[i], asciiLen[i]);
				//rtb.SelectionBackColor = Color.Yellow;
				rtb.SelectionColor = Color.Black;
			}
		}

		for (int i = 0; i < rtb.Text.Length - 2; i++)
		{
			if (rtb.Text.Substring(i, 2) == "00")
			{
				// hex highlight
				//rtb.Select(hexStart[i], hexLen[i]);
				rtb.Select(i, 2);
				rtb.SelectionBackColor = Color.Yellow;
				rtb.SelectionColor = Color.Black;

				// ascii highlight (if asciiStart was assigned; it will be assigned)
				//rtb.Select(asciiStart[i], asciiLen[i]);
				//rtb.SelectionBackColor = Color.Yellow;
				//rtb.SelectionColor = Color.Black;
			}
		}

		// Highlight UTF-8 ZERO WIDTH SPACE (E2 80 8B) sequence as a grouped highlight
		for (int i = 0; i + 2 < bytes.Length; i++)
		{
			if (bytes[i] == 0xE2 && bytes[i + 1] == 0x80 && bytes[i + 2] == 0x8B)
			{
				// Hex select from start of byte i to end of byte i+2
				int hexSelStart = hexStart[i];
				int hexSelLen = hexLen[i] + hexLen[i + 1] + hexLen[i + 2];
				rtb.Select(hexSelStart, hexSelLen);
				//rtb.SelectionBackColor = Color.LightBlue;
				rtb.SelectionColor = Color.Black;

				// ASCII select: three ASCII positions (they'll be non-printable so show as '.')
				int asciiSelStart = asciiStart[i];
				int asciiSelLen = asciiLen[i] + asciiLen[i + 1] + asciiLen[i + 2];
				rtb.Select(asciiSelStart, asciiSelLen);
				//rtb.SelectionBackColor = Color.LightGreen;
				rtb.SelectionColor = Color.Black;

				// skip ahead to avoid overlapping re-highlighting
				i += 2;
			}
		}

		// ensure caret at start
		rtb.SelectionStart = 0;
		rtb.ScrollToCaret();
		rtb.DeselectAll();
	}

	// Convert a byte to printable ASCII or '.' for unprintable bytes
	private static char ToPrintableAscii(byte b)
	{
		if (b >= 32 && b <= 126) // printable ASCII range
			return (char)b;
		// show a visible placeholder for control / non-ASCII bytes
		return '.';
	}

	// simple byte pattern search
	private static int FindPattern(byte[] haystack, byte[] needle)
	{
		if (needle.Length == 0) return 0;
		for (int i = 0; i + needle.Length <= haystack.Length; i++)
		{
			bool ok = true;
			for (int j = 0; j < needle.Length; j++)
			{
				if (haystack[i + j] != needle[j]) { ok = false; break; }
			}
			if (ok) return i;
		}
		return -1;
	}
}

