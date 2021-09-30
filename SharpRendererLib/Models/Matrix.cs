namespace SharpRendererLib.Models
{
    public class Matrix
    {
        private readonly SharpGL.SceneGraph.Matrix _matrix;

        public Matrix(int rows, int cols)
        {
            _matrix = new SharpGL.SceneGraph.Matrix(rows, cols);
        }

        private Matrix(SharpGL.SceneGraph.Matrix matrix)
        {
            _matrix = matrix;
        }
        
        public double this[int i, int j] {
            get => _matrix[i, j];
            set => _matrix[i, j] = value;
        }
        
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            return new Matrix(matrix1._matrix * matrix2._matrix);
        }
        
        public static Matrix Identity(int width)
        {
            Matrix matrix = new Matrix(width, width);
            // An identity matrix has '1's along the top-left to bottom-right diagonal
            // All other spaces are '0'
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1;
                    }
                }
            }

            return matrix;
        }
    }
}