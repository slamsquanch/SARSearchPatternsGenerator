using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    public class ExpandingSquarePattern : Pattern
    {
        public ExpandingSquarePattern() :base()
        {
            
        }

        public List<Coordinate> generatePattern(Coordinate datum, int numLegs, double orientation, double firstLegDistance, bool turnRight)
        {
            double turnDegrees, legDistance = firstLegDistance;
            bool secondLeg = false;
            addPoint(datum);

            if(turnRight)
            {
                turnDegrees = 90;
            }
            else
            {
                turnDegrees = -90;
            }

            //leg is just a move and a turn

            for (int i = 0; i < numLegs; i++)
            {
                //Add a point that is the legDistance away from the datum in the
                //direction of the orientation.
                addPoint(points.ElementAt(i).travel(legDistance, orientation));

                //Turn orientation for next leg
                orientation += turnDegrees;

                if (secondLeg)
                {
                    //Increase legDistance
                    legDistance += firstLegDistance;
                }

                secondLeg = !secondLeg;
            }

            return points;
        }
    }
}
