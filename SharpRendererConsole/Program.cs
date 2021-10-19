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
            string normalMapPath = Path.Combine("../../../res", "african_head_normalmap.tga");
            
            Polygon polygon = ObjFileParser.ParseFile(path);
            Texture texture = new Texture(TgaFileParser.ParseFile(texturePath));
            NormalMap normalMap = new NormalMap(TgaFileParser.ParseFile(normalMapPath));
            ViewPort viewPort = new ViewPort((int)(width * 0.75f), (int)(height * 0.75f), 255, width / 8, height / 8);
            Camera camera = new Camera(1, 1, 3);
            Vector3 center = new Vector3(0, 0, 0);
            ModelView modelView = new ModelView(camera, center);
            Light lightVec = new Vector3(1, 1, 1);

            // Render the polygon
            // Wiremesh
            // PolygonDrawer drawer = new(new WireMeshFaceDrawStrategy(new BresenhamLineDrawStrategy(), new RandomColorDrawStrategy(), camera, viewPort, modelView));
            
            // Shader-based
            // Shader shader = new(new TextureColorDrawStrategy(texture), new FlatShadingStrategy());
            // Shader shader = new(new FlatColorDrawStrategy(Color.White), new GouraudShading(normalMap));
            Shader shader = new(new TextureColorDrawStrategy(texture), new GouraudShading(normalMap));
            PolygonDrawer drawer = new(new FaceDrawStrategy(shader, camera, viewPort, modelView));
            
            drawer.Draw(pixelBuff, polygon, lightVec, zBuffer);

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