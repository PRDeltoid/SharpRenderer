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
                (Vertex vert1, Vertex vert2, Vertex vert3) = GetFaceVertexes(this, face);
                (UV texVert1, UV texVert2, UV texVert3) = GetFaceTextureUVs(this, face);
                return new Face(vert1, vert2, vert3, texVert1, texVert2, texVert3);
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
    }
}