using SharpGL.SceneGraph;

namespace SharpRendererLib.Models
{
    public class Face
    {
        public UV TextureVertex1 { get; set; }
        public UV TextureVertex2 { get; set; }
        public UV TextureVertex3 { get; set; }
        public Vertex Vertex3 { get; set; }
        public Vertex Vertex2 { get; set; }
        public Vertex Vertex1 { get; set; }
        public Vertex Vertex1Norm { get; set; }
        public Vertex Vertex2Norm { get; set; }
        public Vertex Vertex3Norm { get; set; }
        public Vertex[] Vertexes { get; }
        public UV[] TextureVertexes { get; }
        public Vertex[] Normals { get; }
        
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
        
        public Face(Vertex vertex1, Vertex vertex2, Vertex vertex3, UV textureVertex1, UV textureVertex2, UV textureVertex3, Vertex vert1Norm, Vertex vert2Norm, Vertex vert3Norm)
        {
            (Vertex1, Vertex2, Vertex3) = (vertex1, vertex2, vertex3);
            (TextureVertex1, TextureVertex2, TextureVertex3) = (textureVertex1,textureVertex2,textureVertex3);
            (Vertex1Norm, Vertex2Norm, Vertex3Norm) = (vert1Norm, vert2Norm, vert3Norm);
            
            Vertexes = new Vertex[] { Vertex1, Vertex2, Vertex3 };
            TextureVertexes = new UV[] { TextureVertex1, TextureVertex2, TextureVertex3 };
            Normals = new Vertex[] { Vertex1Norm, Vertex2Norm, Vertex3Norm };
        }
    }
}