using System.Drawing;
using SharpGL.SceneGraph;

namespace SharpRendererLib
{
    public class WireMeshPolygonDrawer : IPolygonDrawer
    {
        private readonly LineDrawer _lineDrawer;

        public WireMeshPolygonDrawer(ILineDrawStrategy lineDrawStrat)
        {
            _lineDrawer = new LineDrawer(lineDrawStrat);
        }
        
        public void Draw(PixelBuffer pixelBuffer, Polygon polygon, int width, int height, Point startPoint, Color color)
        {
            int halfWidth = (int)(width / 2.0);
            int halfHeight = (int)(height / 2.0);
            foreach (Face face in polygon.Faces)
            {
                DrawFace(pixelBuffer, polygon, face, color, halfWidth, halfHeight, startPoint);
            }
        }

        private void DrawFace(PixelBuffer pixelBuffer, Polygon polygon, Face face, Color color, int halfWidth, int halfHeight, Point startPoint)
        {
            (Vertex vert1, Vertex vert2, Vertex vert3) = FaceHelper.GetFaceVertexes(polygon, face);
            
            // Line 1 -> 2
            DrawVertexLine(pixelBuffer, vert1, vert2, color, halfWidth, halfHeight, startPoint);
            // Line 2 -> 3
            DrawVertexLine(pixelBuffer,vert2, vert3, color, halfWidth, halfHeight, startPoint);
            // Line 3 -> 1
            DrawVertexLine(pixelBuffer,vert3, vert1, color, halfWidth, halfHeight, startPoint);
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
            int x = (int)((vert.X + 1) * (halfWidth-1));
            int y = (int)((vert.Y + 1) * (halfHeight-1));
            return (x + startPoint.X, y + startPoint.Y);
        }
    }
}