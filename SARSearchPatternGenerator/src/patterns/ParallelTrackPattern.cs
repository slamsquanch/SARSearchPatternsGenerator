using System;
using System.Drawing;
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
        private double crossingDistance, parallelTrackSize;
        private int numCrossings;

        public ParallelTrackPattern() : base() {}


        /*
         *  Returns a Color class array of 6 different colours (each doubled) that the KML and GPX classes
         *   can iterate through to alternate the track leg colours of the Expanding Square pattern.  This 
         *   array contains pairs of each colour because the track "crossings" should be the same colour as 
         *   their corresponding track "leg".
         *   Overrides parent's method (Pattern.cs).
         */
        public override Color[] getColours()
        {
            //size 12
            Color[] legColours = new Color[]
            {
            Color.Red, Color.Red,
            Color.Blue, Color.Blue,
            Color.Yellow, Color.Yellow,
            Color.Purple, Color.Purple,
            Color.Green, Color.Green,
            Color.Cyan, Color.Cyan
            };
            return legColours;
        }


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
            this.orientation = orientation;
            crossingDistance = trackSpacing;
            numCrossings = numLegs - 1;

            for (int i = 0; i < numLegs * 2; i += 2)
            {
                //Add a point that is the legDistance away from the current point in the
                //direction of the orientation. (This is the leg)
                addPoint(points.ElementAt(i).travel( orientation, legDistance, dI));

                //Turn orientation for crossing
                orientation += turnDegrees;

                //Add a point that is the trackspacing away from the current point in the
                //direction of the orientation. (This is a crossing)
                addPoint(points.ElementAt(i + 1).travel(orientation, trackSpacing, dI));

                //Turn orientation for next leg
                orientation += turnDegrees;

                turnDegrees = -turnDegrees;
            }

            removePoint(points[numLegs * 2]);

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

        public double getCrossingDistance()
        {
            return crossingDistance;
        }
    }
}
