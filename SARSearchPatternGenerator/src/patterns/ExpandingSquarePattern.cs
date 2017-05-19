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

        public List<Coordinate> generatePattern(Coordinate datum, int numLegs, double orientation, double firstLegDistance, bool turnRight, DistanceUnit dI)
        {
            double turnDegrees, legDistance;
            bool secondLeg = false;

            legDistance = firstLegDistance;
            this.legDistance = firstLegDistance;
            this.numLegs = numLegs;
            this.turnRight = turnRight;
            totalTrackLength = 0;
            searchedArea = 1;

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
                addPoint(points.ElementAt(i).travel(orientation, legDistance, dI));

                //Turn orientation for next leg
                orientation += turnDegrees;

                if (secondLeg)
                {
                    //Increase legDistance
                    legDistance += firstLegDistance;
                }

                secondLeg = !secondLeg;
                totalTrackLength += legDistance;

                if(i >= numLegs - 2)
                {
                    searchedArea *= legDistance;
                }
            }

            return points;
        }

        public override void calculatePatternInfo(double searchSpeed, double sweepWidth)
        {
            searchTime = totalTrackLength / searchSpeed;
            areaEffectivelySwept = totalTrackLength / sweepWidth;
            areaCoverage = areaEffectivelySwept / searchedArea;
            probabilityOfDetection = (1 - Math.Exp(-areaCoverage)) * 100;
        }
    }
}
