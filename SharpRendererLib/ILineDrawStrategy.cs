using System.Drawing;

namespace SharpRendererLib
{
    public interface ILineDrawStrategy
    {
        int DetermineY(int x, Point point0, Point point2);
    }
}