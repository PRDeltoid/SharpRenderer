using System.Drawing;

namespace SharpRendererLib
{
    public interface ILineDrawStrategy
    {
        int DetermineY(int x, Line line);
    }
}