using System.Drawing;
using SharpGL.SceneGraph;

namespace SharpRendererLib.Helpers
{
    public static class VertexHelper
    {
        public static Point VertexToPoint(Vertex vert, int halfWidth, int halfHeight)
        {
            int x = (int)((vert.X) * (halfWidth));
            int y = (int)((vert.Y) * (halfHeight));
            return new Point(x, y);
        }

    }
}