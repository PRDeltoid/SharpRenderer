using System.Numerics;

namespace SharpRendererLib.Models
{
    public class Camera
    {
        private readonly Vector3 _vector;

        public Camera(float x, float y, float z)
        {
            _vector = new Vector3(x, y, z);
        }

        public float X => _vector.X;
        public float Y => _vector.Y;
        public float Z => _vector.Z;
    }
}