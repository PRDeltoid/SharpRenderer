using System.Drawing;
using SharpGL.SceneGraph;

namespace SharpRendererLib.Helpers
{
    public static class VertexHelper
    {
        public static Point VertexToPoint(Vertex vert, int halfWidth, int halfHeight)
        {
            // Vert.X/Y + 1 makes all vertexes in the African_head.obj file positive (from 0->2 from -1 -> 1)
            // This will be removed when Viewport is used
            // Second addition and subtraction on halfWidth/Height and the result is to make sure our value
            // falls between 0 and width-1/height-1, not 0 and width/height
            // width = 600, halfwidth = 300, (2 [the max VertX/Y])(halfwidth-1))+1 = 599, (0 [the min VertX/Y])(halfwidth-1))+1 = 0
            int x = (int)((vert.X + 1) * (halfWidth - 1) + 1);
            int y = (int)((vert.Y + 1) * (halfHeight - 1) + 1);
            return new Point(x, y);
        }

    }
}