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
            ViewPort viewPort = new ViewPort((int)(width * 0.75f), (int)(height * 0.75f), 255, width / 8, height / 8);
            Camera camera = new Camera(1, 1, 3);
            Vector3 center = new Vector3(0, 0, 0);
            Light lightVec = new Vector3(0, 0, 1);

            Matrix modelView = GetModelView(camera, center);
            
            // Render the polygon
            // PolygonDrawer drawer = new(new WireMeshFaceDrawStrategy(new BresenhamLineDrawStrategy(), new RandomColorDrawStrategy()));
            // PolygonDrawer drawer = new(new FaceDrawStrategy(new FlatColorDrawStrategy(Color.White), camera, viewPort));
            PolygonDrawer drawer = new(new FaceDrawStrategy(new TextureColorDrawStrategy(texture), new FlatShadingStrategy(), camera, viewPort, modelView));
            
            drawer.Draw(pixelBuff, polygon, lightVec, zBuffer, 600, 600, new Point(0,0));

            // Output to a bitmap
            Bitmap bitmap = Draw(pixelBuff);
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
            string outPath = Path.Combine(Path.GetTempPath(), "test.bmp");
            bitmap.Save(outPath);
            OpenImage(outPath);
        }

        private static Matrix GetModelView(Camera camera, Vector3 center)
        {
            Vector3 up = new Vector3(0, 1, 0);
            Vector3 z = Vector3.Normalize(camera - center);
            Vector3 x = Vector3.Normalize(Vector3.Cross(up, z));
            Vector3 y = Vector3.Normalize(Vector3.Cross(z, x));
            
            Matrix Minv = Matrix.Identity(4);
            Matrix Tr = Matrix.Identity(4);
            Minv[0, 0] = x.X;
            Minv[0, 1] = x.Y;
            Minv[0, 2] = x.Z;
            
            Minv[1, 0] = y.X;
            Minv[1, 1] = y.Y;
            Minv[1, 2] = y.Z;
            
            Minv[2, 0] = z.X;
            Minv[2, 1] = z.Y;
            Minv[2, 2] = z.Z;
            
            Tr[0, 3] = -center.X;
            Tr[1, 3] = -center.Y;
            Tr[2, 3] = -center.Z;

            return Minv * Tr;
            //Vec3f eye(1,1,3);
            // Vec3f center(0,0,0);`
            // void lookat(Vec3f eye, Vec3f center, Vec3f up) {
            //     Vec3f z = (eye-center).normalize();
            //     Vec3f x = cross(up,z).normalize();
            //     Vec3f y = cross(z,x).normalize();
            //     Matrix Minv = Matrix::identity();
            //     Matrix Tr   = Matrix::identity();
            //     for (int i=0; i<3; i++) {
            //         Minv[0][i] = x[i];
            //         Minv[1][i] = y[i];
            //         Minv[2][i] = z[i];
            //         Tr[i][3] = -center[i];
            //     }
            //     ModelView = Minv*Tr;
            // }
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