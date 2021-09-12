using System;

namespace SharpRendererLib.Models
{
    public class ZBuffer
    {
        private readonly float[,] _zBuffer;
        private readonly int _width;
        private readonly int _height;

        public ZBuffer(int width, int height)
        {
            _width = width;
            _height = height;
            _zBuffer = new float[width, height];
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _zBuffer[x, y] = float.NegativeInfinity;
                }
            }
        }

        public float GetZ(int x, int y)
        {
            // Attempt to get a Z value out-of-bounds should just be met with an value that any point would be in front
            // of (ie. it does not effect the z buffer at all)
            if (x < 0 || y < 0 || x >= _width || y >= _height) return float.NegativeInfinity;
            
            return _zBuffer[x, y];
        }

        public void SetZ(int x, int y, float z)
        {
            if (x < 0 || y < 0 || x >= _width || y >= _height) throw new IndexOutOfRangeException($"({x},{y}) is out of range 0->{_width-1}W, 0->{_height-1}H");
            
            _zBuffer[x, y] = z; 
        }

        public bool TrySetZ(int x, int y, float z)
        {
            // If we've already drawn something with a higher Z value, skip updating this point's Z value and return false
            if (GetZ(x, y) > z) return false;
                    
            // Otherwise, update zbuffer and return true to indicate success in setting Z value of buffer
            SetZ(x, y, z);
            return true;
        }
    }
}