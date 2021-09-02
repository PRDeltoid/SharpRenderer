using System.Collections.Generic;
using SharpGL.SceneGraph;

namespace SharpRendererLib
{
    public class Polygon
    {
        private readonly SharpGL.SceneGraph.Primitives.Polygon _polygon;

        public Polygon(SharpGL.SceneGraph.Primitives.Polygon polygon)
        {
            _polygon = polygon;
        }

        public List<Vertex> Vertices => _polygon.Vertices;
        public List<Face> Faces => _polygon.Faces;
        public List<Vertex> VertexNormals => _polygon.Normals;
    }
}