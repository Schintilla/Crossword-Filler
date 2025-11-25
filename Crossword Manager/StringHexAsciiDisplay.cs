using System.Drawing;
using System.Text;
using System.Windows.Forms;

public static class StringHexAsciiDisplay
{
	/// <summary>
	/// Encode the supplied string as UTF-8, then render hex + ASCII into the RichTextBox.
	/// Highlights CR (0x0D), LF (0x0A) and the UTF-8 sequence E2 80 8B (U+200B).
	/// </summary>
	public static void DisplayStringAsHexAscii(string input, RichTextBox rtb)
	{
		if (input == null) input = string.Empty;

		// UTF-8 bytes from the input string
		byte[] bytes = Encoding.UTF8.GetBytes(input);

		// Format layout
		const int bytesPerLine = 16;
		var sb = new StringBuilder();

		// mapping arrays for highlighting: hexStart/hexLen and asciiStart/asciiLen per byte
		int[] hexStart = new int[bytes.Length];
		int[] hexLen = new int[bytes.Length];
		int[] asciiStart = new int[bytes.Length];
		int[] asciiLen = new int[bytes.Length];

		for (int i = 0; i < bytes.Length; i++)
		{
			if (i % bytesPerLine == 0)
			{
				if (i != 0) sb.AppendLine();
			}

			// record hex start
			hexStart[i] = sb.Length;
			string hex = bytes[i].ToString("X2");
			sb.Append(hex);
			hexLen[i] = 2;

			// spacing rules:
			// - between bytes on the same line: single space
			// - after the last byte of the line (or last byte overall): two spaces to separate ASCII column
			if ((i % bytesPerLine) != bytesPerLine - 1 && i != bytes.Length - 1)
			{
				sb.Append(' ');
				hexLen[i] += 1; // include the trailing space in selectable length
			}
			else
			{
				// end-of-line or end-of-data -> add two spaces
				sb.Append("  ");
				hexLen[i] += 2;
			}

			// If this is the end of the line (or last byte), append ASCII column for the whole line
			if ((i % bytesPerLine) == bytesPerLine - 1 || i == bytes.Length - 1)
			{
				int lineFirstIndex = i - (i % bytesPerLine);
				int lineLastIndex = i;
				for (int j = lineFirstIndex; j <= lineLastIndex; j++)
				{
					asciiStart[j] = sb.Length;

					if (j == lineFirstIndex)
					{
						sb.AppendLine();
					}
					sb.Append(ToPrintableAscii(bytes[j]) + "  ");
					asciiLen[j] = 1;
				}

				// pad ASCII column if the line wasn't full to keep alignment neat
				int used = lineLastIndex - lineFirstIndex + 1;
				if (used < bytesPerLine)
				{
					sb.Append(new string(' ', bytesPerLine - used));
				}
			}
		}

		// Put text into RichTextBox
		rtb.Clear();
		int len = 2000;
		rtb.Text = sb.ToString().ToUpper().Substring(0, len);

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

		// Highlight CR and LF (both hex and ASCII columns)
		for (int i = 0; i < bytes.Length; i++)
		{
			if (bytes[i] == 0x0D || bytes[i] == 0x0A || bytes[i] == 0x00)
			{
				// hex highlight
				rtb.Select(hexStart[i], hexLen[i]);
				//rtb.SelectionBackColor = Color.Yellow;
				rtb.SelectionColor = Color.Black;

				// ascii highlight
				rtb.Select(asciiStart[i], asciiLen[i]);
				//rtb.SelectionBackColor = Color.Yellow;
				rtb.SelectionColor = Color.Black;
			}
		}
		for (int i = 0; i < len - 100; i++)
		{
			if (rtb.Text.Substring(i, 2) == "20" || rtb.Text.Substring(i, 2) == "29" || rtb.Text.Substring(i, 2) == "00" || rtb.Text.Substring(i, 2) == "0D" || rtb.Text.Substring(i, 2) == "0A")
			{
				// hex highlight
				//rtb.Select(hexStart[i], hexLen[i]);
				rtb.Select(i, 2);
				if (rtb.Text.Substring(i, 2) == "29")
				{
					rtb.SelectionBackColor = Color.Cyan;
				}
				else if (rtb.Text.Substring(i, 2) == "20")
				{
					rtb.SelectionBackColor = Color.LightGray;
				}
				else
				{
					rtb.SelectionBackColor = Color.Yellow;
				}
				rtb.SelectionColor = Color.Black;

				// ascii highlight (if asciiStart was assigned; it will be assigned)
				//rtb.Select(asciiStart[i], asciiLen[i]);
				//rtb.SelectionBackColor = Color.Yellow;
				//rtb.SelectionColor = Color.Black;
			}
		}

		for (int i = 0; i < len - 100; i++)
		{
			if (rtb.Text.Substring(i, 2) == "E2" || rtb.Text.Substring(i, 2) == "80" || rtb.Text.Substring(i, 2) == "8B")
			{
				// hex highlight
				//rtb.Select(hexStart[i], hexLen[i]);
				rtb.Select(i, 2);
				rtb.SelectionBackColor = Color.Magenta;
				rtb.SelectionColor = Color.Black;

				// ascii highlight (if asciiStart was assigned; it will be assigned)
				//rtb.Select(asciiStart[i], asciiLen[i]);
				//rtb.SelectionBackColor = Color.Yellow;
				//rtb.SelectionColor = Color.Black;
			}
		}


		// Highlight UTF-8 ZERO WIDTH SPACE (E2 80 8B)
		for (int i = 0; i + 2 < bytes.Length; i++)
		{
			if (bytes[i] == 0xE2 && bytes[i + 1] == 0x80 && bytes[i + 2] == 0x8B)
			{
				// hex selection spans three bytes
				int hexSelStart = hexStart[i];
				int hexSelLen = hexLen[i] + hexLen[i + 1] + hexLen[i + 2];
				rtb.Select(hexSelStart, hexSelLen);
				//rtb.SelectionBackColor = Color.LightBlue;
				rtb.SelectionColor = Color.Black;

				// ascii selection spans the three ascii slots (they're usually '.' for these bytes)
				int asciiSelStart = asciiStart[i];
				int asciiSelLen = asciiLen[i] + asciiLen[i + 1] + asciiLen[i + 2];
				rtb.Select(asciiSelStart, asciiSelLen);
				//rtb.SelectionBackColor = Color.Red;
				rtb.SelectionColor = Color.Black;

				i += 2; // skip ahead to avoid overlapping highlights
			}
		}

		// reset caret
		rtb.SelectionStart = 0;
		rtb.ScrollToCaret();
		rtb.DeselectAll();
	}

	// printable ASCII for bytes 32..126, '.' otherwise
	private static char ToPrintableAscii(byte b)
	{
		if (b >= 32 && b <= 126) return (char)b;
		return '.';
	}
}

