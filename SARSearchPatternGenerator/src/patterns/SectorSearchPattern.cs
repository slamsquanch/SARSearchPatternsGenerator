using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SARSearchPatternsGenerator.src;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// Contains methods that generate a sector search pattern and stores
    /// the coordinates in the points variable.
    /// </summary>
    public class SectorSearchPattern : Pattern
    {
        private double crossingDistance, radius;
        private int numCrossings;

        public SectorSearchPattern() :base()
        {
            comment = (string)DefaultComments.ResourceManager.GetObject("SectorSearchComment");
        }


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
         * Generates a sector search starting from the datum with the given
         * number of legs of the given size. The first leg will go in the
         * bearing specified by orientation, and then you will turn right or
         * left depending on the value of turnRight for the first turn.
         */
        public List<Coordinate> generatePattern(Coordinate datum, int numLegs, double orientation, double legDistance, bool turnRight, DistanceUnit dI)
        {
            double radius, theta, alpha, crossingDistance, turnDegrees;

            radius = legDistance / 2;
            theta = 360.0 / numLegs / 2.0;
            alpha = (180 - theta) / 2;
            turnDegrees = 180 - alpha;
            crossingDistance = 2 * (radius * Math.Sin(theta * Math.PI / 180 / 2));

            this.legDistance = legDistance;
            this.numLegs = numLegs;
            this.turnRight = turnRight;
            this.crossingDistance = crossingDistance;
            this.radius = radius;
            this.orientation = orientation;
            numCrossings = numLegs - 1;

            if(!turnRight)
            {
                turnDegrees = -turnDegrees;
            }

            Coordinate CSP = datum.travel(orientation - 180, legDistance / 2, dI);
            addPoint(CSP);

            for (int i = 0; i < numLegs * 2; i += 2)
            {
                addPoint(points.ElementAt(i).travel(orientation, legDistance, dI));

                orientation += turnDegrees;

                addPoint(points.ElementAt(i + 1).travel(orientation, crossingDistance, dI));

                orientation += turnDegrees;
                
            }

            removePoint(points[numLegs * 2]);

            return points;
        }

        public override void calculatePatternInfo(double searchSpeed, double sweepWidth)
        {
            totalTrackLength = crossingDistance * numCrossings + legDistance * numLegs;
            searchedArea = Math.PI * radius * radius;
            searchTime = totalTrackLength / searchSpeed;
            areaEffectivelySwept = totalTrackLength * sweepWidth;
            areaCoverage = areaEffectivelySwept / searchedArea;
            probabilityOfDetection = (1 - Math.Exp(-areaCoverage)) * 100;
        }
    }
}
