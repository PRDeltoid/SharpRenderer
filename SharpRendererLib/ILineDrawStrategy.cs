using System;

namespace SharpRendererLib
{
    public interface ILineDrawStrategy
    {
        void CallOnAllDrawPoints(Line line, Action<int, int> drawFunc);
    }
}