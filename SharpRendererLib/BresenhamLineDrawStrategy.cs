using System;
using System.Drawing;

namespace SharpRendererLib
{
    public class BresenhamLineDrawStrategy : ILineDrawStrategy
    {
        public void CallOnAllDrawPoints(Line line, Action<int, int> drawFunc)
        {
            Point p1 = line.Point1;
            Point p2 = line.Point2;
            
            if (p1.X > p2.X)
            {
                throw new Exception($"Line is pointing the wrong direction. P1: ({p1.X},{p1.Y}),  P2:({p2.X},{p2.Y}).");
            }

            // A slope above 1 or below -1 has a greater rise over run. It is considered "steep"
            bool isSteep = line.Slope is > 1 or < -1;
            if (isSteep)
            {
                // This algorithm works better with a shallower line
                // We can transpose a steep line to make it shallow, run the algo, and then swap it back at draw-time
                PointHelper.TransposeXY(ref p1);
                PointHelper.TransposeXY(ref p2);
                
                // If we transposed the values, our points may now be out-of-order
                // This makes sure the line orientation is left-to-right
                if (line.Slope < -1)
                {
                    PointHelper.SwapPoints(ref p1, ref p2);
                }
            }

            // If our line is a single point, just draw the point and exit without looping
            if (p1 == p2)
            {
                drawFunc.Invoke(p1.X, p1.Y);
                return;
            }

            for (int drawX = p1.X; drawX <= p2.X; drawX++)
            {
                int dist = PointHelper.DistanceX(p1, p2);
                int deltaX = drawX - p1.X;
                // How far we've "traveled" down the line
                double time = deltaX / (double)dist;
                // If our line has no X movement (ie. dist = 0), it is a straight vertical line
                // time = 0 produces a straight vertical line
                if (double.IsNaN(time))
                {
                    time = 0.0;
                }
                int drawY = (int)(p1.Y * (1.0 - time) + p2.Y * time);

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