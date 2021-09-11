using System.Drawing;
using SharpGL.SceneGraph;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;
using Face = SharpRendererLib.Models.Face;

namespace SharpRendererLib
{
    public class WireMeshFaceDrawStrategy : IFaceDrawStrategy
    {
        private readonly LineDrawer _lineDrawer;
        private readonly IFlatColorDrawStrategy _colorDrawStrategy;
        

        public WireMeshFaceDrawStrategy(ILineDrawStrategy lineDrawStrategy, IFlatColorDrawStrategy colorDrawStrategy)
        {
            _lineDrawer = new LineDrawer(lineDrawStrategy);
            _colorDrawStrategy = colorDrawStrategy;
        }
        public void DrawFace(PixelBuffer pixelBuffer, Polygon polygon, Face face, Light light, int width, int height, Point startPoint)
        {
            int halfWidth = width / 2;
            int halfHeight = height / 2;
            
            // Line 1 -> 2
            DrawVertexLine(pixelBuffer, face.Vertex1, face.Vertex2, _colorDrawStrategy.GetColor(), halfWidth, halfHeight, startPoint);
            // Line 2 -> 3
            DrawVertexLine(pixelBuffer,face.Vertex2, face.Vertex3, _colorDrawStrategy.GetColor(), halfWidth, halfHeight, startPoint);
            // Line 3 -> 1
            DrawVertexLine(pixelBuffer,face.Vertex3, face.Vertex1, _colorDrawStrategy.GetColor(), halfWidth, halfHeight, startPoint);
        }
        
        private void DrawVertexLine(PixelBuffer pixelBuffer, Vertex vert1, Vertex vert2, Color color, int halfWidth, int halfHeight, Point startPoint)
        {
            Point p1 = VertexHelper.VertexToPoint(vert1, halfWidth, halfHeight);
            Point p2 = VertexHelper.VertexToPoint(vert2, halfWidth, halfHeight);
            
            Point p1Adjusted = PointHelper.OffsetPoint(p1, startPoint);
            Point p2Adjusted = PointHelper.OffsetPoint(p2, startPoint);
            
            Line line = new Line(p1Adjusted, p2Adjusted);
            _lineDrawer.DrawLine(pixelBuffer, line, color);  
        }
    }
}