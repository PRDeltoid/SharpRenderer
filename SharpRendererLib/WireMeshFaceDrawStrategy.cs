using System.Drawing;
using System.Numerics;
using SharpGL.SceneGraph;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;
using Face = SharpRendererLib.Models.Face;
using Matrix = SharpRendererLib.Models.Matrix;

namespace SharpRendererLib
{
    public class WireMeshFaceDrawStrategy : IFaceDrawStrategy
    {
        private readonly LineDrawer _lineDrawer;
        private readonly IColorDrawStrategy _colorDrawStrategy;
        private readonly Camera _camera;
        private readonly ViewPort _viewPort;
        private readonly Matrix _modelView;
        private readonly Matrix _projection;

        public WireMeshFaceDrawStrategy(ILineDrawStrategy lineDrawStrategy, IColorDrawStrategy colorDrawStrategy, Camera camera, ViewPort viewPort, Matrix modelView)
        {
            _lineDrawer = new LineDrawer(lineDrawStrategy);
            _colorDrawStrategy = colorDrawStrategy;
            _camera = camera;
            _projection = Matrix.Identity(4);
            _projection[3, 2] = -1f / camera.Z;
            _viewPort = viewPort;
            _modelView = modelView;
        }

        public void DrawFace(PixelBuffer pixelBuffer, Face face, Light _light, ZBuffer _zBuffer)
        {
            // NOTE: The Barycenter Coordinates below are basically "random" and serve no purpose. 
            // If using a color drawer that requires them, a new DrawStrategy that calculates barycentric coordinates
            // should be created instead.
            
            // Line 1 -> 2
            DrawVertexLine(pixelBuffer, face.Vertex1, face.Vertex2, _colorDrawStrategy.GetColor(face, new Vector3(1,0,0)), _viewPort, _projection, _modelView);
            // Line 2 -> 3
            DrawVertexLine(pixelBuffer,face.Vertex2, face.Vertex3, _colorDrawStrategy.GetColor(face, new Vector3(1,0,0)), _viewPort, _projection, _modelView);
            // Line 3 -> 1
            DrawVertexLine(pixelBuffer,face.Vertex3, face.Vertex1, _colorDrawStrategy.GetColor(face, new Vector3(1,0,0)), _viewPort, _projection, _modelView);
        }

        private void DrawVertexLine(PixelBuffer pixelBuffer, Vertex vert1, Vertex vert2, Color color, ViewPort viewPort,
            Matrix projection, Matrix modelView)
        {
            Point p1 = VertexHelper.VertexToPoint(viewPort, projection, modelView, vert1);
            Point p2 = VertexHelper.VertexToPoint(viewPort, projection, modelView, vert2);

            Line line = new Line(p1, p2);
            _lineDrawer.DrawLine(pixelBuffer, line, color);
        }
    }
}