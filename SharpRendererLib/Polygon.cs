using System.Collections.Generic;
using System.Linq;
using SharpGL.SceneGraph;

namespace SharpRendererLib
{
    public class Polygon
    {
        private readonly SharpGL.SceneGraph.Primitives.Polygon _polygon;

        public Polygon(SharpGL.SceneGraph.Primitives.Polygon polygon)
        {
            _polygon = polygon;
            Faces = polygon.Faces.Select(face => new Face(this, face)).ToList();
        }

        public List<Vertex> Vertices => _polygon.Vertices;
        public List<Face> Faces { get; }
        public List<Vertex> VertexNormals => _polygon.Normals;
    }
}