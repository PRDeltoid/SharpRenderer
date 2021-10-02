using System.Numerics;

namespace SharpRendererLib.Models
{
    public class ModelView
    {
        public ModelView(Camera camera, Vector3 center)
        {
            Camera = camera;
            Center = center;
        }

        public Camera Camera { get; set; }
        public Vector3 Center { get; set; }

        public Matrix GetModelViewMatrix()
        {
            Vector3 up = new Vector3(0, 1, 0);
            Vector3 z = Vector3.Normalize(Camera - Center);
            Vector3 x = Vector3.Normalize(Vector3.Cross(up, z));
            Vector3 y = Vector3.Normalize(Vector3.Cross(z, x));

            Matrix Minv = Matrix.Identity(4);
            Matrix Tr = Matrix.Identity(4);
            Minv[0, 0] = x.X;
            Minv[0, 1] = x.Y;
            Minv[0, 2] = x.Z;

            Minv[1, 0] = y.X;
            Minv[1, 1] = y.Y;
            Minv[1, 2] = y.Z;

            Minv[2, 0] = z.X;
            Minv[2, 1] = z.Y;
            Minv[2, 2] = z.Z;

            Tr[0, 3] = -Center.X;
            Tr[1, 3] = -Center.Y;
            Tr[2, 3] = -Center.Z;

            return Minv * Tr;
        }
    }
}