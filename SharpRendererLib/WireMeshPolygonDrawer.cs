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
        
        public void Draw(PixelBuffer pixelBuffer, Polygon polygon, Color color)
        {
            int halfHeight = (int)(pixelBuffer.Height / 2.0);
            int halfWidth = (int)(pixelBuffer.Width / 2.0);
            foreach (Face face in polygon.Faces)
            {
                DrawFace(pixelBuffer, polygon, face, color, halfWidth, halfHeight);
            }
        }

        private void DrawFace(PixelBuffer pixelBuffer, Polygon polygon, Face face, Color color, int halfWidth, int halfHeight)
        {
            Vertex vert1 = polygon.Vertices[face.Indices[0].Vertex];
            Vertex vert2 = polygon.Vertices[face.Indices[1].Vertex];
            Vertex vert3 = polygon.Vertices[face.Indices[2].Vertex];
            DrawVertexLine(pixelBuffer, vert1, vert2, color, halfWidth, halfHeight);
            DrawVertexLine(pixelBuffer,vert2, vert3, color, halfWidth, halfHeight);
            DrawVertexLine(pixelBuffer,vert3, vert1, color, halfWidth, halfHeight);
        }

        private void DrawVertexLine(PixelBuffer pixelBuffer, Vertex vert1, Vertex vert2, Color color, int halfWidth, int halfHeight)
        {
            int x1 = (int)((vert1.X + 1) * (halfWidth-1));
            int y1 = (int)((vert1.Y + 1) * (halfHeight-1));
            int x2 = (int)((vert2.X + 1) * (halfWidth-1));
            int y2 = (int)((vert2.Y + 1) * (halfHeight-1));
            Point vertPoint1 = new Point(x1, y1);
            Point vertPoint2 = new Point(x2, y2);
            Line line = new Line(vertPoint1, vertPoint2);
                
            _lineDrawer.DrawLine(pixelBuffer, line, color);  
        }
    }
}