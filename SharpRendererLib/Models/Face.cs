using SharpGL.SceneGraph;

namespace SharpRendererLib
{
    public class Face
    {
        public Vertex Vertex3 { get; }
        public Vertex Vertex2 { get; }
        public Vertex Vertex1 { get; }
        
        public Face(Vertex vertex1, Vertex vertex2, Vertex vertex3)
        {
            (Vertex1, Vertex2, Vertex3) = (vertex1, vertex2, vertex3);
        }
    }
}