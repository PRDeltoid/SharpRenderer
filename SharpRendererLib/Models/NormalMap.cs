using System.Drawing;
using System.Numerics;

namespace SharpRendererLib.Models
{
    public class NormalMap
    {
        private readonly Bitmap _normalMap;
        public int Width => _normalMap.Width;
        public int Height => _normalMap.Height; 
        
        public NormalMap(Bitmap normalMapImage)
        {
            _normalMap = normalMapImage;
        }
        
        public Vector3 GetPointNormal(int x, int y)
        {
            // for (int i=0; i<3; i++)
            //     res[2-i] = c[i]/255.*2 - 1;
            Color color = _normalMap.GetPixel(x, y);
            // Interperet the pixel color as a normal vector, with each color component representing a vector component
            return Vector3.Normalize(new Vector3(color.B / 255.0f * 2 - 1, color.G / 255.0f * 2 - 1, color.R / 255.0f * 2 - 1));
        }
    }
}