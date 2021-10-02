using System.Collections.Generic;
using System.Linq;
using SharpGL.SceneGraph;

namespace SharpRendererLib.Models
{
    public class Polygon
    {
        private readonly SharpGL.SceneGraph.Primitives.Polygon _polygon;

        public Polygon(SharpGL.SceneGraph.Primitives.Polygon polygon)
        {
            _polygon = polygon;
            Faces = polygon.Faces.Select(face =>
            {
                // Always parse vertexes, as without them, no polygon would exist
                (Vertex vert1, Vertex vert2, Vertex vert3) = GetFaceVertexes(this, face);
                Face returnFace = new Face(vert1, vert2, vert3);

                // Only parse texture coordinates if they exist
                if (polygon.UVs.Count > 0)
                {
                    (returnFace.TextureVertex1, returnFace.TextureVertex2, returnFace.TextureVertex3) = GetFaceTextureUVs(this, face);
                }

                // Only parse normal vertexes if they exist
                if (polygon.Normals.Count > 0)
                {
                    (returnFace.Vertex1Norm, returnFace.Vertex2Norm, returnFace.Vertex3Norm) = GetVertexNormals(this, face);
                }

                return returnFace;
            }).ToList();
        }

        public List<Face> Faces { get; }
        public List<Vertex> Vertices => _polygon.Vertices;
        public List<UV> UVs => _polygon.UVs;
        public List<Vertex> VertexNormals => _polygon.Normals;
        
        private static (UV, UV, UV) GetFaceTextureUVs(Polygon polygon, SharpGL.SceneGraph.Face face)
        {
            UV vert1 = polygon.UVs[face.Indices[0].UV];
            UV vert2 = polygon.UVs[face.Indices[1].UV];
            UV vert3 = polygon.UVs[face.Indices[2].UV];
            return (vert1, vert2, vert3);
        }
        
        private static (Vertex, Vertex, Vertex) GetFaceVertexes(Polygon polygon, SharpGL.SceneGraph.Face face)
        {
            Vertex vert1 = polygon.Vertices[face.Indices[0].Vertex];
            Vertex vert2 = polygon.Vertices[face.Indices[1].Vertex];
            Vertex vert3 = polygon.Vertices[face.Indices[2].Vertex];
            return (vert1, vert2, vert3);
        }
        
        private static (Vertex, Vertex, Vertex) GetVertexNormals(Polygon polygon, SharpGL.SceneGraph.Face face)
        {
            Vertex vert1 = polygon.Vertices[face.Indices[0].Normal];
            Vertex vert2 = polygon.Vertices[face.Indices[1].Normal];
            Vertex vert3 = polygon.Vertices[face.Indices[2].Normal];
            return (vert1, vert2, vert3);
        }
    }
}