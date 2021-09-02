using SharpGL.SceneGraph;
using SharpGL.Serialization.Wavefront;

namespace SharpRendererLib
{
    public class ObjFileParser
    {
        private static ObjFileFormat _objFileLoader = new ObjFileFormat();
        
        public ObjContainer ParseFile(string path)
        {
            Scene scene = _objFileLoader.LoadData(path);
            return new ObjContainer(scene);
        }
    }
}