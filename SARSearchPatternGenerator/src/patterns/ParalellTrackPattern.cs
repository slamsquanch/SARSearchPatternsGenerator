using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARSearchPatternGenerator
{
    public class ParalellTrackPattern : Pattern
    {
        public ParalellTrackPattern() : base()
        {

        }

        public List<Coordinate> generateFromCreepingLine(Coordinate datum, int numLegs, double orientation, double legDistance, double trackSpacing, bool firstTurnRight)
        {
            Coordinate CSP;
            double turnDegrees;

            if (firstTurnRight)
            {
                turnDegrees = 90;
            }
            else
            {
                turnDegrees = -90;
            }

            CSP = datum.travel(trackSpacing / 2, orientation + turnDegrees);
            CSP = CSP.travel(legDistance / 2, orientation + 180);

            generatePattern(CSP, numLegs, orientation, legDistance, trackSpacing, firstTurnRight);

            return points;
        }

        public List<Coordinate> generateFromParalellTrackDatum(Coordinate datum, int numLegs, double orientation, double legDistance, double trackSpacing, bool firstTurnRight)
        {
            Coordinate CSP;
            double turnDegrees;

            if (firstTurnRight)
            {
                turnDegrees = 90;
            }
            else
            {
                turnDegrees = -90;
            }

            CSP = datum.travel(3 * trackSpacing / 2, orientation - turnDegrees);
            CSP = CSP.travel(legDistance / 2, orientation + 180);

            generatePattern(CSP, numLegs, orientation, legDistance, trackSpacing, firstTurnRight);

            return points;
        }

        public List<Coordinate> generatePattern(Coordinate CSP, int numLegs, double orientation, double legDistance, double trackSpacing, bool firstTurnRight)
        {
            double turnDegrees;
            bool secondLeg = false;

            addPoint(CSP);

            if (firstTurnRight)
            {
                turnDegrees = 90;
            }
            else
            {
                turnDegrees = -90;
            }

            for (int i = 0; i < numLegs; i++)
            {
                //Add a point that is the legDistance away from the current point in the
                //direction of the orientation. (This is the leg)
                addPoint(points.ElementAt(i).travel(legDistance, orientation));

                //Turn orientation for crossing
                orientation += turnDegrees;

                //Add a point that is the trackspacing away from the current point in the
                //direction of the orientation. (This is a crossing)
                addPoint(points.ElementAt(i).travel(trackSpacing, orientation));

                //Turn orientation for next leg
                orientation += turnDegrees;

                if (secondLeg)
                {
                    //Flip turn direction
                    turnDegrees = -turnDegrees;
                }

                secondLeg = !secondLeg;
            }

            return points;
        }
    }
}
