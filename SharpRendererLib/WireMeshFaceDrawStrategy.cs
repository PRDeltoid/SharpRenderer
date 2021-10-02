using System.Drawing;
using System.Numerics;
using SharpGL.SceneGraph;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;
using Face = SharpRendererLib.Models.Face;
using Matrix = SharpRendererLib.Models.Matrix;

namespace SharpRendererLib
{
    public class WireMeshFaceDrawStrategy : FaceDrawStrategyBase, IFaceDrawStrategy
    {
        private readonly LineDrawer _lineDrawer;
        private readonly IColorDrawStrategy _colorDrawStrategy;

        public WireMeshFaceDrawStrategy(ILineDrawStrategy lineDrawStrategy, IColorDrawStrategy colorDrawStrategy, Camera camera, ViewPort viewPort, ModelView modelView)
            : base(camera, viewPort, modelView)
        {
            _lineDrawer = new LineDrawer(lineDrawStrategy);
            _colorDrawStrategy = colorDrawStrategy;
        }

        public void DrawFace(PixelBuffer pixelBuffer, Face face, Light _light, ZBuffer _zBuffer)
        {
            // NOTE: The Barycenter Coordinates below are basically "random" and serve no purpose. 
            // If using a color drawer that requires them, a new DrawStrategy that calculates barycentric coordinates
            // should be created instead.
            
            // Line 1 -> 2
            DrawVertexLine(pixelBuffer, face.Vertex1, face.Vertex2, _colorDrawStrategy.GetColor(face, new Vector3(1,0,0)), TransformationMatrix);
            // Line 2 -> 3
            DrawVertexLine(pixelBuffer,face.Vertex2, face.Vertex3, _colorDrawStrategy.GetColor(face, new Vector3(1,0,0)), TransformationMatrix);
            // Line 3 -> 1
            DrawVertexLine(pixelBuffer,face.Vertex3, face.Vertex1, _colorDrawStrategy.GetColor(face, new Vector3(1,0,0)), TransformationMatrix);
        }

        private void DrawVertexLine(PixelBuffer pixelBuffer, Vertex vert1, Vertex vert2, Color color, Matrix transformationMatrix)
        {
            Point p1 = VertexHelper.VertexToPoint(vert1, transformationMatrix);
            Point p2 = VertexHelper.VertexToPoint(vert2, transformationMatrix);

            Line line = new Line(p1, p2);
            _lineDrawer.DrawLine(pixelBuffer, line, color);
        }
    }
}