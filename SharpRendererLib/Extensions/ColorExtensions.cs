using System.Drawing;

namespace SharpRendererLib.Extensions
{
    public static class ColorExtensions
    {
        public static void SetIntensity(this ref Color color, float intensity)
        {
            if (intensity > 1.0f)
            {
                intensity = 1.0f;
            }
            color = Color.FromArgb(color.A, (int)(color.R * intensity), (int)(color.G * intensity), (int)(color.B * intensity));  
        }
    }
}