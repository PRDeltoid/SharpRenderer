using System.IO;
using NUnit.Framework;
using SharpRendererLib;
using SharpRendererLib.Models;

namespace SharpRendererLibTests
{
    [TestFixture]
    public class ObjFileParserTests
    {
        [Test]
        [TestCase("teapot.obj")]
        [TestCase("african_head.obj")]
        public void ObjFileParserTests_OpenFile_ReturnsObjContainer(string objFileName)
        {
            string path = Path.Combine("../../../res", objFileName);
            Polygon parsedFile = ObjFileParser.ParseFile(path);
        }
        
        [Test]
        [TestCase("teapot.obj", 3644)]
        [TestCase("african_head.obj", 1258)]
        public void ObjFileParserTests_OpenFile_PolygoneHasVertices(string objFileName, int expectedVertices)
        {
            string path = Path.Combine("../../../res", objFileName);
            Polygon parsedFile = ObjFileParser.ParseFile(path);
            
            Assert.AreEqual(expectedVertices,parsedFile.Vertices.Count);
        }
        
        [Test]
        [TestCase("teapot.obj", 6320)]
        [TestCase("african_head.obj", 2492)]
        public void ObjFileParserTests_OpenFile_PolygonHasFaces(string objFileName, int expectedFaces)
        {
            string path = Path.Combine("../../../res", objFileName);
            Polygon parsedFile = ObjFileParser.ParseFile(path);
            
            Assert.AreEqual(expectedFaces,parsedFile.Faces.Count);
        }
        
        [Test]
        [TestCase("teapot.obj", 0)]
        [TestCase("african_head.obj", 1258)]
        public void ObjFileParserTests_OpenTeapotFile_PolygonHasNormals(string objFileName, int expectedNormals)
        {
            string path = Path.Combine("../../../res", objFileName);
            Polygon parsedFile = ObjFileParser.ParseFile(path);
            
            Assert.AreEqual(expectedNormals,parsedFile.VertexNormals.Count);
        }
    }
}