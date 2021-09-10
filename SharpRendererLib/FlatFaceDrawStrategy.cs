using System.Drawing;
using SharpRendererLib.Helpers;

namespace SharpRendererLib
{
    public class FlatFaceDrawStrategy : IFaceDrawStrategy
    {
        private readonly IFlatColorDrawStrategy _colorDrawStrategy;

        public FlatFaceDrawStrategy(IFlatColorDrawStrategy colorDrawStrategy)
        {
            _colorDrawStrategy = colorDrawStrategy;
        }
        
        public void DrawFace(PixelBuffer pixelBuffer, Polygon polygon, Face face, int width, int height, Point start)
        {
            Color faceColor = _colorDrawStrategy.GetColor();
            
            Triangle faceTriangle = TriangleHelper.TriangleFromFace(face, width, height);
            BoundingBox bb = BoundingBoxHelper.GetBoundingBox(faceTriangle.Points);

            bb.PerformActionOverBoundingBox(((point) =>
            {
                if (TriangleHelper.PointInTriangle(faceTriangle, point))
                {
                    var drawPoint = PointHelper.OffsetPoint(point, start);
                    pixelBuffer.SetPixel(drawPoint.X, drawPoint.Y, faceColor);
                }
            }));
        }
    }
}