using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using SharpRendererLib;
using SharpRendererLib.Models;

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
            ZBuffer zBuffer = new ZBuffer(width, height);

            // Load the polygon to render
            // string path = Path.Combine("../../../res", "teapot.obj");
            string path = Path.Combine("../../../res", "african_head.obj");
            string texturePath = Path.Combine("../../../res", "african_head_diffuse.tga");
            
            Polygon polygon = ObjFileParser.ParseFile(path);
            Texture texture = new Texture(TgaFileParser.ParseFile(texturePath));
            
            Light lightVec = new Vector3(0, 0, -1);
            
            // Render the polygon
            // PolygonDrawer drawer = new(new WireMeshFaceDrawStrategy(new BresenhamLineDrawStrategy(), new RandomColorDrawStrategy()));
            // PolygonDrawer drawer = new(new FaceDrawStrategy(new FlatColorDrawStrategy(Color.White)));
            PolygonDrawer drawer = new(new FaceDrawStrategy(new TextureColorDrawStrategy(texture)));
            
            drawer.Draw(pixelBuff, polygon, lightVec, zBuffer, 600, 600, new Point(100,100));

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