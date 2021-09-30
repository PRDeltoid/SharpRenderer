using SharpGL.SceneGraph;
using Matrix = SharpRendererLib.Models.Matrix;

namespace SharpRendererLib.Helpers
{
    public class MatrixHelper
    {
        public static Vertex MatrixToVertex(Matrix matrix)
        {
            Vertex vertex = new Vertex();
            float div = (float)matrix[3,0];
            vertex.X = (float)matrix[0, 0] / div;
            vertex.Y = (float)matrix[1, 0] / div;
            vertex.Z = (float)matrix[2, 0] / div;
            return vertex;
        }
    }
}