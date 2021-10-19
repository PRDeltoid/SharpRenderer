using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Win32;
using SharpRendererLib;
using SharpRendererLib.Models;
namespace SharpRendererWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Render()
        {
            const int width = 1000;
            const int height = 1000;
            PixelBuffer pixelBuff = new PixelBuffer(width, height);
            ZBuffer zBuffer = new ZBuffer(width, height);

            // Load the polygon to render
            string polyPath = ModelFilePath.Text;
            string texturePath = TextureFilePath.Text;
            string normalMapPath = NormalFilePath.Text;
            
            // Load user-entered polygon, texture and normal map
            Polygon polygon = ObjFileParser.ParseFile(polyPath);
            
            // Texture map is optional
            Texture texture = null;
            if (texturePath != "" && File.Exists(texturePath))
            {
                texture = new Texture(TgaFileParser.ParseFile(texturePath));
            }

            // Normal map is optional
            NormalMap normalMap = null;
            if (normalMapPath != "" && File.Exists(normalMapPath))
            {
                normalMap = new NormalMap(TgaFileParser.ParseFile(normalMapPath));
            }
            
            // Vectors used to compute transforms and shading
            Camera camera = new Camera(float.Parse(CameraX.Text), float.Parse(CameraY.Text), float.Parse(CameraZ.Text));
            Vector3 center = new Vector3(float.Parse(CenterX.Text), float.Parse(CenterY.Text), float.Parse(CenterZ.Text));
            Light lightVec = new Vector3(float.Parse(LightX.Text), float.Parse(LightY.Text), float.Parse(LightZ.Text));
            
            // Transformation matrixes
            ViewPort viewPort = new ViewPort((int)(width * 0.75f), (int)(height * 0.75f), 255, width / 8, height / 8);
            ModelView modelView = new ModelView(camera, center);
            
            IColorDrawStrategy colorStrat = ColorStrategyComboBox.Text switch
            {
                "Texture" => new TextureColorDrawStrategy(texture),
                "Greyscale" => new FlatColorDrawStrategy(System.Drawing.Color.White),
                "Random" => new RandomColorDrawStrategy(),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            IShadingStrategy shadeStrat = ShadingStrategyComboBox.Text switch
            {
                "Gouraud" => new GouraudShading(normalMap),
                "Flat" => new FlatShadingStrategy(),
                _ => throw new ArgumentOutOfRangeException()
            };

            IShader shader = new Shader(colorStrat, shadeStrat);

            IFaceDrawStrategy faceDrawStrat = FaceStrategyComboBox.Text switch
            {
                "Wiremesh" => new WireMeshFaceDrawStrategy(new BresenhamFastLineDrawStrategy(), colorStrat, camera, viewPort, modelView),
                "Filled" => new FaceDrawStrategy(shader, camera, viewPort, modelView),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            PolygonDrawer drawer = new(faceDrawStrat);
            
            drawer.Draw(pixelBuff, polygon, lightVec, zBuffer);

            
            // Output to a bitmap
            Bitmap bitmap = Draw(pixelBuff);
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
            
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapSource);

            RenderImage.Source = writeableBitmap;
            // string outPath = Path.Combine(Path.GetTempPath(), "test.bmp");
            // bitmap.Save(outPath);
            //
            // if (RenderImage.Source != null)
            // {
            //     RenderImage.Source = null;
            // }
            // RenderImage.Source = new BitmapImage(new Uri(outPath));
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
        
        private void OpenFileModelButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Object Files (*.obj)|*.obj"
            };
            
            if (openFileDialog.ShowDialog() != true) return;
            
            ModelFilePath.Text = openFileDialog.FileName;
        }

        private void OpenFileTextureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                Filter = "TGA Image Files (*.tga)|*.tga"
            };
            
            if (openFileDialog.ShowDialog() != true) return;
            
            TextureFilePath.Text = openFileDialog.FileName;
        }

        private void OpenFileNormalButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                Filter = "TGA Image Files (*.tga)|*.tga"
            };
            
            if (openFileDialog.ShowDialog() != true) return;
            
            NormalFilePath.Text = openFileDialog.FileName;
        }

        private void RenderButton_Click(object sender, RoutedEventArgs e)
        {
            Render();
        }

        private void CancelRenderButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}