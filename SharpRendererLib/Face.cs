using SharpGL.SceneGraph;
using SharpRendererLib.Helpers;

namespace SharpRendererLib
{
    public class Face
    {
        public Vertex Vertex3 { get; }
        public Vertex Vertex2 { get; }
        public Vertex Vertex1 { get; }
        
        public Face(Polygon polygon, SharpGL.SceneGraph.Face rawFace)
        {
            (Vertex1, Vertex2, Vertex3) = FaceHelper.GetFaceVertexes(polygon, rawFace);

        }

    }
}