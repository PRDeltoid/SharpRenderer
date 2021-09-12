using System.Drawing;

namespace SharpRendererLib.Models
{
    public class Texture
    {
        private readonly Bitmap _texture;

        public int Width => _texture.Width;
        public int Height => _texture.Height;

        public Texture(Bitmap texture)
        {
            _texture = texture;
        }

        public Color GetPointColor(int x, int y)
        {
            return _texture.GetPixel(x, y);
        }
    }
}