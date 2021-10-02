using System.Drawing;
using SharpGL.SceneGraph;
using SharpRendererLib.Models;
using Matrix = SharpRendererLib.Models.Matrix;

namespace SharpRendererLib.Helpers
{
    public static class VertexHelper
    {
        public static Matrix VertexToMatrix(Vertex vert)
        {
            Matrix matrix = new Matrix(4, 1)
            {
                [0, 0] = vert.X,
                [1, 0] = vert.Y,
                [2, 0] = vert.Z,
                [3, 0] = 1
            };
            return matrix;
        } 
        
        public static Point VertexToPoint(ViewPort viewPort, Matrix projection, Matrix modelView, Vertex vertex)
        {
            Matrix matrixVert = VertexToMatrix(vertex);
            Vertex screenVertex = MatrixHelper.MatrixToVertex(viewPort * projection * modelView * matrixVert);
            int x = (int)screenVertex.X;
            int y = (int)screenVertex.Y;
            return new Point(x, y);
        }
    }
}