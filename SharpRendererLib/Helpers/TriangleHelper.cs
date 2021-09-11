using System;
using System.Drawing;
using System.Numerics;
using SharpRendererLib.Models;

namespace SharpRendererLib.Helpers
{
    public static class TriangleHelper
    {
        public static Triangle TriangleFromFace(Face face, int width, int height)
        {
            int halfWidth = width / 2;
            int halfHeight = height / 2;
            
            Point p1 = VertexHelper.VertexToPoint(face.Vertex1, halfWidth, halfHeight);
            Point p2 = VertexHelper.VertexToPoint(face.Vertex2, halfWidth, halfHeight);
            Point p3 = VertexHelper.VertexToPoint(face.Vertex3, halfWidth, halfHeight);
            return new Triangle(p1, p2, p3);
        }
        
        public static bool PointInTriangle(Triangle triangle, Point point)
        {
            Vector3 baryCoords  = Barycentric(triangle, point);
            if (baryCoords.X < 0 || baryCoords.Y < 0 || baryCoords.Z < 0)
            {
                return false;
            }

            return true;
        }

        private static Vector3 Barycentric(Triangle triangle, Point point)
        {
           Vector3 vec1 = new Vector3(triangle.Point3.X - triangle.Point1.X, triangle.Point2.X - triangle.Point1.X,
               triangle.Point1.X - point.X);
           Vector3 vec2 = new Vector3(triangle.Point3.Y - triangle.Point1.Y, triangle.Point2.Y - triangle.Point1.Y,
               triangle.Point1.Y - point.Y);
           Vector3 u = Vector3.Cross(vec1, vec2);
           
           // Degenerative case: return negative vector to indicate it is not within triangle's bounds
           if(Math.Abs(u.Z) < 1) return new Vector3(-1, 1, 1);
           
           return new Vector3(1.0f - (u.X + u.Y) / u.Z, u.Y / u.Z, u.X / u.Z);
        }
    }
}