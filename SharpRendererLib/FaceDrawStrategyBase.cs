using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class FaceDrawStrategyBase
    {
        protected readonly Matrix TransformationMatrix;
        protected readonly ModelView ModelView;
        protected readonly Matrix Projection;

        protected FaceDrawStrategyBase(Camera camera, ViewPort viewPort, ModelView modelView)
        {
            ModelView = modelView;
            Projection = Matrix.Identity(4);
            Projection[3, 2] = -1f / camera.Z;
            TransformationMatrix = BuildVertexTransformMatrix(viewPort, modelView, Projection);
        }

        private static Matrix BuildVertexTransformMatrix(ViewPort viewPort, ModelView modelView, Matrix projection)
        {
            return viewPort * projection * modelView.GetModelViewMatrix();
        }
    }
}