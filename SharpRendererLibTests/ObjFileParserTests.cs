using System.IO;
using NUnit.Framework;
using SharpRendererLib;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class ObjFileParserTests
    {
        [Test]
        [TestCase("teapot.obj")]
        [TestCase("african_head.obj")]
        public void ObjFileParserTests_OpenTeapotFile_ReturnsObjContainer(string objFileName)
        {
            ObjFileParser parser = new();
            string path = Path.Combine("../../../res", "teapot.obj");
            Polygon parsedFile = parser.ParseFile(path);
        }
        
        [Test]
        [TestCase("teapot.obj", 3644)]
        [TestCase("african_head.obj", 1258)]
        public void ObjFileParserTests_OpenFile_PolygoneHasVertices(string objFileName, int expectedVertices)
        {
            ObjFileParser parser = new();
            string path = Path.Combine("../../../res", objFileName);
            Polygon parsedFile = parser.ParseFile(path);
            
            Assert.AreEqual(expectedVertices,parsedFile.Vertices.Count);
        }
        
        [Test]
        [TestCase("teapot.obj", 6320)]
        [TestCase("african_head.obj", 2492)]
        public void ObjFileParserTests_OpenFile_PolygonHasFaces(string objFileName, int expectedFaces)
        {
            ObjFileParser parser = new();
            string path = Path.Combine("../../../res", objFileName);
            Polygon parsedFile = parser.ParseFile(path);
            
            Assert.AreEqual(expectedFaces,parsedFile.Faces.Count);
        }
        
        [Test]
        [TestCase("teapot.obj", 0)]
        [TestCase("african_head.obj", 1258)]
        public void ObjFileParserTests_OpenTeapotFile_PolygonHasNormals(string objFileName, int expectedNormals)
        {
            ObjFileParser parser = new();
            string path = Path.Combine("../../../res", objFileName);
            Polygon parsedFile = parser.ParseFile(path);
            
            Assert.AreEqual(expectedNormals,parsedFile.VertexNormals.Count);
        }
    }
}