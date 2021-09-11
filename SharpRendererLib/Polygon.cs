using System.Collections.Generic;
using System.Linq;
using SharpGL.SceneGraph;
using SharpRendererLib.Helpers;

namespace SharpRendererLib
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
                return new Face(vert1, vert2, vert3);
            }).ToList();
        }

        public List<Vertex> Vertices => _polygon.Vertices;
        public List<Face> Faces { get; }
        public List<Vertex> VertexNormals => _polygon.Normals;
        
        
        private static (Vertex, Vertex, Vertex) GetFaceVertexes(Polygon polygon, SharpGL.SceneGraph.Face face)
        {
            Vertex vert1 = polygon.Vertices[face.Indices[0].Vertex];
            Vertex vert2 = polygon.Vertices[face.Indices[1].Vertex];
            Vertex vert3 = polygon.Vertices[face.Indices[2].Vertex];
            return (vert1, vert2, vert3);
        }
    }
}