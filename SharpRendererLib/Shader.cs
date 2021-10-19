using System.Drawing;
using System.Numerics;
using SharpRendererLib.Extensions;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class Shader : IShader
    {
        private readonly IColorDrawStrategy _colorDrawStrategy;
        private readonly IShadingStrategy _shadingStrategy;

        public Shader(IColorDrawStrategy colorDrawStrategy, IShadingStrategy shadingStrategy)
        {
            _colorDrawStrategy = colorDrawStrategy;
            _shadingStrategy = shadingStrategy;
        }

        public Color CalculatePointColor(Face face, Light light, Vector3 bc)
        {
            float intensity = _shadingStrategy.GetIntensity(face, bc, light);
            Color pointColor = _colorDrawStrategy.GetColor(face, bc);
            
            pointColor.SetIntensity(intensity);
            return pointColor;
        }
    }
}