using System;
using System.Drawing;

namespace SharpRendererLib
{
    public class BresenhamLineDrawStrategy : ILineDrawStrategy
    {
        public void CallOnAllDrawPoints(Line line, Action<int, int> drawFunc)
        {
            bool isSteep = line.Slope > 0.5;

            Point p1 = line.Point1;
            Point p2 = line.Point2;

            if (isSteep)
            {
                // This algorithm works better with a shallower line
                // We can transpose a steep line to make it shallow, run the algo, and then swap it back at draw-time
                PointHelper.TransposeXY(ref p1);
                PointHelper.TransposeXY(ref p2);
            }

            for (int drawX = p1.X; drawX < p2.X; drawX++)
            {
                // How far we've "traveled" down the line
                double tick = (drawX - line.Point1.X) / (double)PointHelper.DistanceX(line.Point1, line.Point2);
                int drawY = (int)(p1.Y * (1.0 - tick) + p2.Y * tick);

                if (isSteep)
                {
                    // De-transpose X and Y
                    drawFunc.Invoke(drawY, drawX);
                }
                else
                {
                    drawFunc.Invoke(drawX, drawY);
                }
            }
        }
    }
}