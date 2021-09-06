using System.Diagnostics;
using System.Drawing;
using System.IO;
using SharpRendererLib;

namespace SharpRendererConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            // Setup the draw space
            const int width = 1000;
            const int height = 1000;
            PixelBuffer pixelBuff = new PixelBuffer(width, height);

            LineDrawer lineDrawer = new LineDrawer(new BresenhamLineDrawStrategy());
            
            // Load the polygon to render
            ObjFileParser parser = new();
            string path = Path.Combine("../../../res", "african_head.obj");
            Polygon parsedFile = parser.ParseFile(path); 
            
            // Render the polygon as a wiremesh
            WireMeshPolygonDrawer polygonDrawer = new WireMeshPolygonDrawer(new BresenhamLineDrawStrategy());
            polygonDrawer.Draw(pixelBuff, parsedFile, 600, 600, new Point(200,200), Color.White);

            // Output to a bitmap
            Bitmap bitmap = Draw(pixelBuff);
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
            string outPath = Path.Combine(Path.GetTempPath(), "test.bmp");
            bitmap.Save(outPath);
            OpenImage(outPath);
        }

        private static void OpenImage(string imagePath)
        {
            ProcessStartInfo openImageProc = new(imagePath) { UseShellExecute = true };
            Process.Start(openImageProc);
        }

        private static Bitmap Draw(PixelBuffer pixelBuffer)
        {
            Bitmap bitmap = new(pixelBuffer.Width, pixelBuffer.Height);

            for (int x = 0; x < pixelBuffer.Width; x++)
            {
                for (int y = 0; y < pixelBuffer.Height; y++)
                {
                    bitmap.SetPixel(x, y, pixelBuffer.GetPixel(x,y));
                }
            }

            return bitmap;
        }
    }
}