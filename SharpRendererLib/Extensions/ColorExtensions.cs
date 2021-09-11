using System.Drawing;

namespace SharpRendererLib.Extensions
{
    public static class ColorExtensions
    {
        public static void SetIntensity(this ref Color color, float intensity)
        {
            color = Color.FromArgb(color.A, (int)(color.R * intensity), (int)(color.G * intensity), (int)(color.B * intensity));  
        }
    }
}