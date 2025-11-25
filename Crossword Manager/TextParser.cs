using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Crossword_Filler
{
	public class TextParser
	{
		public static void OCRTextParseNew(string OCRText, RichTextBox rtb, string direction)
		{
			// Create an instance to call non-static methods
			TextParser parser = new TextParser();
			//float currentFontSize = rtb.SelectionFont.Size;
			System.Drawing.Font defaultFont = rtb.Font;
			//Font font = new Font("Arial", 10, FontStyle.Regular);
			int maxWidth = rtb.Width-LoadSVG.ScaleDPIPixel(16);
			string wrappedText = parser.WordWrapTextPreservingLineEndings(OCRText, maxWidth, defaultFont);
			rtb.Text = wrappedText;
		}
		private int MeasureTextWidth(string text, Font font)
		{
			using (Control dummyControl = new Control())
			using (Graphics g = dummyControl.CreateGraphics())
			{
				return TextRenderer.MeasureText(g, text, font).Width;
			}
		}
		private string WordWrapTextPreservingLineEndings(string OCRText, int maxWidth, Font font)
		{
			Control dummyControl = new Control();
			int listIndent = 7;
			string indent = new string(' ', listIndent);
			string firstlnIndentA = new string(' ', 4); // 4 spaces - one digit number
			string firstlnIndentB = new string(' ', 3); // 3 spaces - two digit number
			StringBuilder wrappedText = new StringBuilder();
			OCRText = Regex.Replace(OCRText, " +", " ");
			var paragraphs = Regex.Split(OCRText, @"(?:\u200B|\r\n)");
			for (int p = 0; p < paragraphs.Length; p += 2)
			{
				string paragraph = paragraphs[p];
				int spcPos = paragraph.IndexOf(" ");
				if (spcPos == 1 || paragraph.IndexOf(".") == 1) // "1. " or "1 " 2 or 1
				{
					paragraph = paragraph.Substring(0, 1) + firstlnIndentA + paragraph.Substring(2);
				}
				else if (spcPos == 2 || paragraph.IndexOf(".") == 2) // "12. " or "12 " 3 or 2 
				{
					paragraph = paragraph.Substring(0, 2) + firstlnIndentB + paragraph.Substring(3);
				}
				indent = new string(' ', listIndent + spcPos - 1);
				if (string.IsNullOrEmpty(paragraph) || paragraph == "\u200B")
				{
					continue;
				}

				string[] words = paragraph.Split(' ');
				if (!words.Any()) continue;

				StringBuilder currentLine = new StringBuilder();
				currentLine.Append(words[0]);

				for (int i = 1; i < words.Length; i++)
				{
					string word = words[i];
					string testLine = currentLine.ToString() + " " + word;
					if (word.Contains("chipmunk"))
					{
						continue;
					}
					if (MeasureTextWidth(testLine, font) > maxWidth)
					{
						wrappedText.AppendLine(currentLine.ToString());
						currentLine.Clear();
						currentLine.Append(indent).Append(word);
					}
					else
					{
						currentLine.Append(" ").Append(word);
					}
				}
				// Append the last line of the paragraph.
				wrappedText.Append(currentLine.ToString());
				wrappedText.Append("\r\n\u200B");
			}
			return wrappedText.ToString();
		}
	}
}
