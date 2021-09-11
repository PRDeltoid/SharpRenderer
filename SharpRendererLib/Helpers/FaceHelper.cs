using System.Numerics;
using SharpRendererLib.Models;

namespace SharpRendererLib.Helpers
{
    public static class FaceHelper
    {
        public static Vector3 GetFaceNormal(Face face)
        {
            // Create vectors that lie along two edges of the face
            Vector3 vec1 = new Vector3(face.Vertex3.X - face.Vertex1.X, face.Vertex3.Y - face.Vertex1.Y,
                face.Vertex3.Z - face.Vertex1.Z);
            Vector3 vec2 = new Vector3(face.Vertex2.X - face.Vertex1.X, face.Vertex2.Y - face.Vertex1.Y,
                face.Vertex2.Z - face.Vertex1.Z);
            
            // The cross of these two vectors produces a normal vector from the two vectors (and therefor the face)
            Vector3 cross = Vector3.Cross(vec1, vec2);
            return Vector3.Normalize(cross);
        }
    }
}