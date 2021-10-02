using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class FaceDrawStrategyBase
    {
        protected readonly Matrix TransformationMatrix;

        protected FaceDrawStrategyBase(Camera camera, ViewPort viewPort, ModelView modelView)
        {
            Matrix projection = Matrix.Identity(4);
            projection[3, 2] = -1f / camera.Z;
            TransformationMatrix = BuildVertexTransformMatrix(viewPort, modelView, projection);
        }

        private static Matrix BuildVertexTransformMatrix(ViewPort viewPort, ModelView modelView, Matrix projection)
        {
            return viewPort * projection * modelView.GetModelViewMatrix();
        }
    }
}