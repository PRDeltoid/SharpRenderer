using System.Drawing;

namespace SharpRendererLib
{
    public class FlatColorDrawStrategy : IFlatColorDrawStrategy
    {
        private readonly Color _color;

        public FlatColorDrawStrategy(Color color)
        {
            _color = color;
        }

        public Color GetColor()
        {
            return _color;
        }
    }
}