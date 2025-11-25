using System;
using System.Drawing;
using System.Windows.Forms;

namespace Crossword_Filler
{
	public partial class SnippingForm : Form
	{
		// These will store the drawn rectangle and the cropped image.
		private Rectangle selectionRect;
		private Image fullScreenImage;
		private Point startPoint;

		// A property to expose the final cropped image to the main form.
		public Image CroppedImage { get; private set; }

		// ... rest of the code
		public SnippingForm(Image screenshot)
		{
			InitializeComponent();
			this.fullScreenImage = screenshot;
			this.DoubleBuffered = true; // Prevents flickering.
			this.Cursor = Cursors.Cross; // Change cursor to crosshair.
										 // this.BackColor = Color.DimGray;
		}

		// Draw the selection rectangle on the form.
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (selectionRect.Width > 0 && selectionRect.Height > 0)
			{
				using (Pen pen = new Pen(Color.Red, 2))
				{
					e.Graphics.DrawRectangle(pen, selectionRect);
				}
			}
		}

		// Start drawing the rectangle.
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				startPoint = e.Location;
				selectionRect = new Rectangle(startPoint, new Size(0, 0));
			}
		}

		// Resize the rectangle while dragging.
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				int x = Math.Min(startPoint.X, e.X);
				int y = Math.Min(startPoint.Y, e.Y);
				int width = Math.Abs(startPoint.X - e.X);
				int height = Math.Abs(startPoint.Y - e.Y);
				selectionRect = new Rectangle(x, y, width, height);
				this.Invalidate(); // Repaint the form to show the new rectangle.
			}
		}

		// Finalize the crop and close the form.
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (selectionRect.Width > 0 && selectionRect.Height > 0)
				{
					// Create a new Bitmap with the size of the selection.
					Bitmap croppedBitmap = new Bitmap(selectionRect.Width, selectionRect.Height);
					using (Graphics g = Graphics.FromImage(croppedBitmap))
					{
						// Copy the selected area from the full-screen image.
						g.DrawImage(fullScreenImage, new Rectangle(0, 0, croppedBitmap.Width, croppedBitmap.Height),
									selectionRect, GraphicsUnit.Pixel);
					}
					this.CroppedImage = croppedBitmap;
					this.DialogResult = DialogResult.OK; // Indicate success.
				}
				this.Close();
			}
		}

	}
}
