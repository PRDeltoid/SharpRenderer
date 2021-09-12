using SharpGL.SceneGraph;

namespace SharpRendererLib.Models
{
    public class Face
    {
        public UV TextureVertex1 { get; }
        public UV TextureVertex2 { get; }
        public UV TextureVertex3 { get; }
        public Vertex Vertex3 { get; }
        public Vertex Vertex2 { get; }
        public Vertex Vertex1 { get; }
        public Vertex[] Vertexes { get; }
        public UV[] TextureVertexes { get; }
        
        public Face(Vertex vertex1, Vertex vertex2, Vertex vertex3)
        {
            (Vertex1, Vertex2, Vertex3) = (vertex1, vertex2, vertex3);
            Vertexes = new Vertex[] { Vertex1, Vertex2, Vertex3 };
        }
        
        public Face(Vertex vertex1, Vertex vertex2, Vertex vertex3, UV textureVertex1, UV textureVertex2, UV textureVertex3)
        {
            (Vertex1, Vertex2, Vertex3) = (vertex1, vertex2, vertex3);
            (TextureVertex1, TextureVertex2, TextureVertex3) = (textureVertex1,textureVertex2,textureVertex3);
            
            Vertexes = new Vertex[] { Vertex1, Vertex2, Vertex3 };
            TextureVertexes = new UV[] { TextureVertex1, TextureVertex2, TextureVertex3 };
        }
    }
}