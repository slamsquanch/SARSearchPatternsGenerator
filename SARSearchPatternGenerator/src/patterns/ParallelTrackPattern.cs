using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    public class ParallelTrackPattern : Pattern
    {
        private double crossingDistance, parallelTrackSize;
        private int numCrossings;

        public ParallelTrackPattern() : base()
        {

        }

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

            CSP = datum.travel(orientation - turnDegrees, (numLegs - 1) * trackSpacing / 2, dI);
            CSP = CSP.travel(orientation + 180, legDistance / 2, dI);

            generatePattern(CSP, numLegs, orientation, legDistance, trackSpacing, firstTurnRight, dI);

            return points;
        }

        public List<Coordinate> generateFromBaseline(Coordinate datum, int numLegs, double orientation, double legDistance, double trackSpacing, bool firstTurnRight, DistanceUnit dI)
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

            CSP = datum.travel(orientation - turnDegrees, (numLegs - 1) * trackSpacing / 2, dI);

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

            this.legDistance = legDistance;
            this.numLegs = numLegs;
            this.turnRight = firstTurnRight;
            crossingDistance = trackSpacing;
            numCrossings = numLegs - 1;

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

        public override void calculatePatternInfo(double searchSpeed, double sweepWidth)
        {
            totalTrackLength = crossingDistance * numCrossings + legDistance * numLegs;
            parallelTrackSize = legDistance + crossingDistance * numCrossings;
            searchedArea = (legDistance + crossingDistance / 2 + crossingDistance / 2) * (numCrossings + crossingDistance / 2 + crossingDistance / 2);
            searchTime = totalTrackLength / searchSpeed;
            areaEffectivelySwept = totalTrackLength / sweepWidth;
            areaCoverage = areaEffectivelySwept / searchedArea;
            probabilityOfDetection = (1 - Math.Exp(-areaCoverage)) * 100;
        }
    }
}
