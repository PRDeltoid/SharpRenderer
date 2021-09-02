using System.IO;
using NUnit.Framework;
using SharpRendererLib;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class ObjFileParserTests
    {
        [Test]
        public void ObjFileParserTests_OpenFile_ReturnsObjContainer()
        {
            ObjFileParser parser = new();
            string path = Path.Combine("../../../res", "teapot.obj");
            ObjContainer parsedFile = parser.ParseFile(path);
        }
    }
}