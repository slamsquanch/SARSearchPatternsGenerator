using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// Contains methods that generate a parallel track pattern and stores
    /// the coordinates in the points variable.
    /// </summary>
    public class ParallelTrackPattern : Pattern
    {
        public ParallelTrackPattern() : base() {}

        /*
         * Generates a parallel track search starting at a point on the same line
         * as the datum with the given number of legs of the given size. The first
         * leg will go in the bearing specified by orientation, and then you will
         * turn right or left depending on the value of turnRight for the first turn.
         */
        public List<Coordinate> generateFromCreepingLine(Coordinate datum, int numLegs, double orientation, double legDistance, double trackSpacing, bool firstTurnRight, DistanceUnit dI)
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

            CSP = datum.travel(orientation + turnDegrees, trackSpacing / 2, dI);
            CSP = CSP.travel(orientation + 180, legDistance / 2, dI);

            generatePattern(CSP, numLegs, orientation, legDistance, trackSpacing, firstTurnRight, dI);

            return points;
        }

        /*
         * Generates a parallel track search with the datum at the middle with the
         * given number of legs of the given size. The first leg will go in the
         * bearing specified by orientation, and then you will turn right or left
         * depending on the value of turnRight for the first turn.
         */
        public List<Coordinate> generateFromParallelTrackDatum(Coordinate datum, int numLegs, double orientation, double legDistance, double trackSpacing, bool firstTurnRight, DistanceUnit dI)
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

            CSP = datum.travel(orientation - turnDegrees, 3 * trackSpacing / 2, dI);
            CSP = CSP.travel(orientation + 180, legDistance / 2, dI);

            generatePattern(CSP, numLegs, orientation, legDistance, trackSpacing, firstTurnRight, dI);

            return points;
        }

        public List<Coordinate> generatePattern(Coordinate CSP, int numLegs, double orientation, double legDistance, double trackSpacing, bool firstTurnRight, DistanceUnit dI)
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
                addPoint(points.ElementAt(i).travel( orientation, legDistance, dI));

                //Turn orientation for crossing
                orientation += turnDegrees;

                //Add a point that is the trackspacing away from the current point in the
                //direction of the orientation. (This is a crossing)
                addPoint(points.ElementAt(i).travel(orientation, trackSpacing, dI));

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
