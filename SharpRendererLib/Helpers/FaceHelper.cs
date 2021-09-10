using SharpGL.SceneGraph;

namespace SharpRendererLib.Helpers
{
    public static class FaceHelper
    {

        public static (Vertex, Vertex, Vertex) GetFaceVertexes(Polygon polygon, Face face)
        {
            Vertex vert1 = polygon.Vertices[face.Indices[0].Vertex];
            Vertex vert2 = polygon.Vertices[face.Indices[1].Vertex];
            Vertex vert3 = polygon.Vertices[face.Indices[2].Vertex];
            return (vert1, vert2, vert3);
        }
    }
}