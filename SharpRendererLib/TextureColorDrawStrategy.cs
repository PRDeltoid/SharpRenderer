using System.Drawing;
using System.Numerics;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class TextureColorDrawStrategy : IColorDrawStrategy
    {
        private readonly Texture _texture;
        private readonly int _width;
        private readonly int _height;

        public bool ColorPerFace { get; }
        
        public TextureColorDrawStrategy(Texture texture)
        {
            _texture = texture;
            _width = texture.Width;
            _height = texture.Height;
            // Texture is always colored per-pixel, not per-face
            ColorPerFace = false;
        }

        private static (float, float) GetTextureCoordinateForPoint(Face face, Vector3 barycentric)
        {
            float x = 0;
            
            // Interpolate Z value of the current point we are drawing using its barycentric coordinates
            x += face.TextureVertex1.U * barycentric.X;
            x += face.TextureVertex2.U * barycentric.Y;
            x += face.TextureVertex3.U * barycentric.Z;
            
            float y = 0;
            
            // Interpolate Z value of the current point we are drawing using its barycentric coordinates
            y += face.TextureVertex1.V * barycentric.X;
            y += face.TextureVertex2.V * barycentric.Y;
            y += face.TextureVertex3.V * barycentric.Z;
            return (x, y);
        }

        /// <summary>
        /// Gets the color for the provided barycentric coordinate in the face
        /// </summary>
        public Color GetColor(Face face, Vector3 barycentricCoord)
        {
            (float texX, float texY) = GetTextureCoordinateForPoint(face, barycentricCoord);
            return _texture.GetPointColor((int)(texX * _width), (int)(texY * _height));
        }
    }
}