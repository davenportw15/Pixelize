using System;
using System.Drawing;

namespace Pixelize
{
	public class PixelizableImage
	{
		// Bitmap of unmodified image
		private Bitmap bitmap;

		// Pixels in width
		private int numberOfPixelsInWidth;

		// Pixels in height
		private int numberOfPixelsInHeight;

		// Width of pixelized image
		private int pixelizedWidth;

		// Height of pixelized image
		private int pixelizedHeight;

		// Width of individual pixel in pixelized image
		private int pixelWidth;

		// Height of individual pixel in pixelized image
		private int pixelHeight;

		public PixelizableImage (Bitmap bitmap, int numberOfPixelsInWidth, int numberOfPixelsInHeight)
		{
			this.bitmap = bitmap;

			this.numberOfPixelsInWidth = numberOfPixelsInWidth;
			this.numberOfPixelsInHeight = numberOfPixelsInHeight;

			this.pixelWidth = bitmap.Width / this.numberOfPixelsInWidth;
			this.pixelHeight = bitmap.Height / this.numberOfPixelsInHeight;

			this.pixelizedWidth = this.numberOfPixelsInWidth * this.pixelWidth;
			this.pixelizedHeight = this.numberOfPixelsInHeight * this.pixelHeight;
		}

		// Returns location of top left corner of each pixelized pixel
		private Point[] pixelCorners ()
		{
			int totalNumberOfPixels = this.numberOfPixelsInWidth * this.numberOfPixelsInHeight;
			Point[] corners = new Point[totalNumberOfPixels];

			int index = 0;
			for (int x = 0; x < this.pixelizedWidth; x += this.pixelWidth) {
				for (int y = 0; y < this.pixelizedHeight; y += this.pixelHeight) {
					corners [index] = new Point (x, y);
					index++;
				}
			}

			return corners;
		}

		// Returns the average color in a pixelized pixel
		private Color averageColorInPixel (Point point)
		{
			int redSum = 0;
			int greenSum = 0;
			int blueSum = 0;
			int numberOfColors = 0;

			for (int x = 0; x < this.pixelWidth; x++) {
				for (int y = 0; y < this.pixelHeight; y++) {
					int xFinal = point.X + x;
					int yFinal = point.Y + y;
					Color colorAtLocation = bitmap.GetPixel (xFinal, yFinal);

					redSum += colorAtLocation.R;
					greenSum += colorAtLocation.G;
					blueSum += colorAtLocation.B;

					numberOfColors++;
				}
			}

			int redAverage = redSum / numberOfColors;
			int greenAverage = greenSum / numberOfColors;
			int blueAverage = blueSum / numberOfColors;

			return Color.FromArgb (redAverage, greenAverage, blueAverage);
		}

		// Creates a pixelized version of the image
		public Bitmap Pixelize ()
		{
			Bitmap pixelized = new Bitmap (this.pixelizedWidth, this.pixelizedHeight);
			foreach (Point corner in pixelCorners ()) {
				Color averageColor = averageColorInPixel (corner);
				for (int x = 0; x < this.pixelWidth; x++) {
					for (int y = 0; y < this.pixelHeight; y++) {
						int xFinal = corner.X + x;
						int yFinal = corner.Y + y;
						pixelized.SetPixel (xFinal, yFinal, averageColor);
					}
				}
			}

			return pixelized;
		}
	}
}

