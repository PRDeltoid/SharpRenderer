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
            const int width = 90;
            const int height = 90;
            PixelBuffer pixelBuff = new PixelBuffer(width, height);
            LineDrawer lineDrawer = new LineDrawer(new BresenhamLineDrawStrategy());
            Line line1 = new(new Point(13,20), new Point(80,40));
            Line line2 = new(new Point(13,20), new Point(80,40));
            Line line3 = new(new Point(13,20), new Point(80,40));
            
            lineDrawer.DrawLine(pixelBuff, line1, Color.White);
            lineDrawer.DrawLine(pixelBuff, line2, Color.Magenta);
            lineDrawer.DrawLine(pixelBuff, line3, Color.Aqua);

            Bitmap bitmap = Draw(pixelBuff);
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