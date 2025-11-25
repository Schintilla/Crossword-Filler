using Newtonsoft.Json.Linq;
using Svg;
using System;
using System.Drawing;
using System.IO;

namespace Crossword_Filler
{
	public class LoadSVG
	{
		public static string GetButtonResourceName(string buttonName)
		{
			string baseName = buttonName.Replace("button", "").ToLower();
			return $"{baseName}Icon";
		}
		public static string GetSvgStringFromResource(string resourceName)
		{
			try
			{
				// Get the resource as a byte array
				byte[] svgBytes = (byte[])Properties.Resources.ResourceManager.GetObject(resourceName);
				if (svgBytes == null)
				{
					return null;
				}

				// Convert the byte array to a UTF-8 string
				return System.Text.Encoding.UTF8.GetString(svgBytes);
			}
			catch
			{
				// Handle cases where the resource is not found or is not a byte array
				return null;
			}
		}
		public static Image GetSvgImage(string svgString, int width, int height)
		{
			// Use a memory stream to avoid writing files to disk
			using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(svgString)))
			{
				var svgDocument = SvgDocument.Open<SvgDocument>(stream);
				return svgDocument.Draw(width, height);
			}
		}
		public static string GetResourceName(string menuItemName)
		{
			if (menuItemName.IndexOf("ToolStripMenuItem") >= 0)
			{
				string baseName = menuItemName.Replace("ToolStripMenuItem", "");
				return $"{baseName}Icon";
			}
			return null;
		}
		private static Image GetIconImage(Icon sourceIcon, int size)
		{
			// Create a new Icon instance at the target size.
			// The framework will select the best-matching size from the source icon.
			Icon sizedIcon = new Icon(sourceIcon, new Size(size, size));
			return sizedIcon.ToBitmap();
		}
		public static int ScaleDPIPixel(int value)
		{
			// Simple DPI scaling using the current graphics DPI.
			using (var g = Graphics.FromHwnd(IntPtr.Zero))
			{
				float dpiScale = g.DpiX / 96f;
				return (int)(value * dpiScale);
			}
		}
		public static void LblHeightTextForDPI(System.Windows.Forms.Label label)
		{
			float scaleFactor;
			using (var g = Graphics.FromHwnd(IntPtr.Zero))
			{
				scaleFactor = g.DpiX / 96f;
			}
			float baseFontSize = label.Font.Size;
			label.Font = new Font(label.Font.FontFamily, baseFontSize * scaleFactor, label.Font.Style);

			// Ensure AutoSize is false for this to work correctly
			if (label.AutoSize == true) return;
			// Get the Graphics object from the control itself to get the correct scaling context
			using (Graphics g = label.CreateGraphics())
			{
				// Define constraints for measuring the string (use the label's *current, scaled* width)
				SizeF textSize = g.MeasureString(
					label.Text,
					label.Font,
					label.Width, // Use the label's current (already DPI-scaled) width
					StringFormat.GenericTypographic
				);
				// Add a small buffer for safety (padding)
				int requiredHeight = (int)Math.Ceiling(textSize.Height * scaleFactor) + 5;
				// Set the new height
				label.Height = requiredHeight;
			}
		}
		public static void BtnHeightTextForDPI(System.Windows.Forms.Button button)
		{
			// Calculate the DPI scale factor.
			float scaleFactor;
			using (var g = Graphics.FromHwnd(IntPtr.Zero))
			{
				scaleFactor = g.DpiX / 96f; // 96 is the standard DPI
			}

			// Adjust the font size according to the scale factor.
			float baseFontSize = button.Font.Size;
			button.Font = new Font(button.Font.FontFamily, baseFontSize * scaleFactor, button.Font.Style);

			// Ensure AutoSize is false for the button.
			if (button.AutoSize) return;

			// Measure the string for the button text with the new font size.
			using (Graphics g = button.CreateGraphics())
			{
				SizeF textSize = g.MeasureString(button.Text, button.Font, button.Width, StringFormat.GenericTypographic);

				// Add padding for height.
				int requiredHeight = (int)Math.Ceiling(textSize.Height * scaleFactor) + 10; // Extra padding

				// Set the new height, adjusting with the scale factor.
				button.Height = requiredHeight;

				// If needed, also adjust the width to maintain a minimum size.
				int requiredWidth = (int)Math.Ceiling(textSize.Width * scaleFactor) + 10; // Extra padding
				button.Width = Math.Max(button.Width, requiredWidth); // Ensure it fits the text
			}
		}
	}
}
