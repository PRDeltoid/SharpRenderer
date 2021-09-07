using System.Drawing;
using SharpGL.SceneGraph;

namespace SharpRendererLib
{
    public class WireMeshFaceDrawStrategy : IFaceDrawStrategy
    {
        private readonly LineDrawer _lineDrawer;
        private readonly Color _meshColor;

        public WireMeshFaceDrawStrategy(ILineDrawStrategy lineDrawStrategy, Color meshColor)
        {
            _lineDrawer = new LineDrawer(lineDrawStrategy);
            _meshColor = meshColor;
        }
        public void DrawFace(PixelBuffer pixelBuffer, Polygon polygon, Face face, int width, int height, Point startPoint)
        {
            int halfWidth = width / 2;
            int halfHeight = height / 2;
            (Vertex vert1, Vertex vert2, Vertex vert3) = FaceHelper.GetFaceVertexes(polygon, face);
            
            // Line 1 -> 2
            DrawVertexLine(pixelBuffer, vert1, vert2, _meshColor, halfWidth, halfHeight, startPoint);
            // Line 2 -> 3
            DrawVertexLine(pixelBuffer,vert2, vert3, _meshColor, halfWidth, halfHeight, startPoint);
            // Line 3 -> 1
            DrawVertexLine(pixelBuffer,vert3, vert1, _meshColor, halfWidth, halfHeight, startPoint);
        }
        
        private void DrawVertexLine(PixelBuffer pixelBuffer, Vertex vert1, Vertex vert2, Color color, int halfWidth, int halfHeight, Point startPoint)
        {
            (int x1, int y1) = VertexToPixel(vert1, halfWidth, halfHeight, startPoint);
            (int x2, int y2) = VertexToPixel(vert2, halfWidth, halfHeight, startPoint);
            Point vertPoint1 = new Point(x1, y1);
            Point vertPoint2 = new Point(x2, y2);
            Line line = new Line(vertPoint1, vertPoint2);
                
            _lineDrawer.DrawLine(pixelBuffer, line, color);  
        }
        
        private (int, int) VertexToPixel(Vertex vert, int halfWidth, int halfHeight, Point startPoint)
        {
            int x = (int)((vert.X) * (halfWidth));
            int y = (int)((vert.Y) * (halfHeight));
            return (x + startPoint.X, y + startPoint.Y);
        }
    }
}