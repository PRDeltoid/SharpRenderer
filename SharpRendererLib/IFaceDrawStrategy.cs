using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public interface IFaceDrawStrategy
    {
        void DrawFace(PixelBuffer pixelBuffer, Face face, Light light, ZBuffer zBuffer);
    }
}