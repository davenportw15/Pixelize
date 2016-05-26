using System;
using System.Drawing;

namespace Pixelize
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.Write ("Full path to image: ");
			string inputImagePath = Console.ReadLine ();
			Bitmap inputImage = null;
			try {
				inputImage = new Bitmap (inputImagePath);
			} catch {
				Console.WriteLine ("Image not found");
				Console.ReadKey ();
				Environment.Exit (1);
			}

			Console.Write ("Number of pixels (width): ");
			string widthInput = Console.ReadLine ();

			Console.Write ("Number of pixels (height): ");
			string heightInput = Console.ReadLine ();

			int numberOfPixelsInHeight = 0;
			int numberOfPixelsInWidth = 0;
			try {
				numberOfPixelsInWidth = Convert.ToInt32 (widthInput);
				numberOfPixelsInHeight = Convert.ToInt32 (heightInput);
			} catch {
				Console.WriteLine ("Invalid dimensions");
				Console.ReadKey ();
				Environment.Exit (1);
			}
		
			Console.Write ("Full path to save pixelized image: ");
			string outputImagePath = Console.ReadLine ();

			Console.WriteLine ("Pixelizing...");

			PixelizableImage image = new PixelizableImage (inputImage, numberOfPixelsInWidth, numberOfPixelsInHeight);
			Bitmap output = image.Pixelize ();
			try {
				output.Save (outputImagePath);
			} catch {
				Console.WriteLine ("Cannot save image -- Invalid path");
				Console.ReadKey ();
				Environment.Exit (1);
			}

			Console.WriteLine ("Done. Press any key to continue...");
			Console.ReadKey ();
			Environment.Exit (0);
		}
	}
}
