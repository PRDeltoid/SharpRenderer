using System;
using System.Drawing;
using SharpRendererLib.Helpers;
using SharpRendererLib.Models;

namespace SharpRendererLib
{
    public class BresenhamFastLineDrawStrategy : ILineDrawStrategy
    {
        public void CallOnAllDrawPoints(Line line, Action<int, int> drawFunc)
        {
            Point p1 = line.Point1;
            Point p2 = line.Point2;
            
            bool isSteep = line.Slope > 0.5;
            int error = 0;
            // Line direction of 1 indicates line moves up
            // Line direction of -1 indicates line moves down
            // Our Y value is modified by this value to move our Y value up or down
            int lineDirection = line.Point2.Y > line.Point1.Y ? 1 : -1;
            int xDist = PointHelper.DistanceX(line.Point1, line.Point2);
            int errorDelta = Math.Abs(PointHelper.DistanceY(line.Point1, line.Point2))*2;
            // The amount to reduce our error value by each time the delta becomes too high
            int errorReduction = xDist*2;

            // This algorithm works better with a shallower line
            // We can transpose a steep line to make it shallow, run the algo, and then swap it back at draw-time
            if (isSteep)
            {
                PointHelper.TransposeXY(ref p1);
                PointHelper.TransposeXY(ref p2);
            }

            int drawY = p1.Y;
            for (int drawX = p1.X; drawX < p2.X; drawX++)
            {
                
                error += errorDelta; 
            
                // Our error value is getting too high and needs to be corrected
                if (error > xDist) { 
                    // Move our Y in the correct direction (up or down)
                    drawY += lineDirection; 
                    // Since we've corrected the error, reduce the error value to "reset" error calculations for the next point
                    error -= errorReduction; 
                }

                if (isSteep)
                {
                    // De-transpose
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