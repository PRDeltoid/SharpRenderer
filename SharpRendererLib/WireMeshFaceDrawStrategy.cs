﻿using System.Drawing;
using SharpGL.SceneGraph;
using SharpRendererLib.Helpers;

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
            
            // Line 1 -> 2
            DrawVertexLine(pixelBuffer, face.Vertex1, face.Vertex2, _meshColor, halfWidth, halfHeight, startPoint);
            // Line 2 -> 3
            DrawVertexLine(pixelBuffer,face.Vertex2, face.Vertex3, _meshColor, halfWidth, halfHeight, startPoint);
            // Line 3 -> 1
            DrawVertexLine(pixelBuffer,face.Vertex3, face.Vertex1, _meshColor, halfWidth, halfHeight, startPoint);
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