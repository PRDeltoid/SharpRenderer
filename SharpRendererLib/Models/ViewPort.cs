namespace SharpRendererLib.Models
{
    public class ViewPort
    {
        private readonly Matrix _matrix;
        
        public int Width { get; }
        public int Height { get; }
        
        public ViewPort(int width, int height, int depth, int x, int y)
        {
            Width = width;
            Height = height;
            _matrix = ConstructViewport(x, y, width, height, depth);
        }

        private static Matrix ConstructViewport(int x, int y, int width, int height, int depth)
        {
            float halfWidth = width / 2f;
            float halfHeight = height / 2f;
            float halfDepth = depth / 2f;
            Matrix matrix = Matrix.Identity(4);
            matrix[0, 3] = x + halfWidth;
            matrix[1, 3] = y + halfHeight;
            matrix[2, 3] = halfDepth;

            matrix[0, 0] = halfWidth;
            matrix[1, 1] = halfHeight;
            matrix[2, 2] = halfDepth;
            return matrix;
        }

        public static Matrix operator *(ViewPort vp, Matrix matrix)
        {
            return vp._matrix* matrix;
        }
    }
}