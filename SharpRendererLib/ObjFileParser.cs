using System.Linq;
using SharpGL.SceneGraph;
using SharpGL.Serialization.Wavefront;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class ObjFileParser
    {
        private static readonly ObjFileFormat _objFileLoader = new ObjFileFormat();
        
        public Polygon ParseFile(string path)
        {
            Scene scene = _objFileLoader.LoadData(path);
            // an Obj file only contains one model, and ObjFileFormat makes it the first child of the SceneContainer
            SharpGL.SceneGraph.Primitives.Polygon polygon = (SharpGL.SceneGraph.Primitives.Polygon)scene.SceneContainer.Children.First();
            return new Polygon(polygon);
        }
    }
}