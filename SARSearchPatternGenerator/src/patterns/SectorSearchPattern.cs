using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    public class SectorSearchPattern : Pattern
    {
        private double crossingDistance, radius;
        private int numCrossings;

        public SectorSearchPattern() :base()
        {

        }

        public List<Coordinate> generatePattern(Coordinate datum, int numLegs, double orientation, double legDistance, bool turnRight, DistanceUnit dI)
        {
            double radius, theta, alpha, crossingDistance, turnDegrees;

            radius = legDistance / 2;
            theta = 360 / numLegs;
            alpha = (180 - theta) / 2;
            turnDegrees = 180 - alpha;
            crossingDistance = 2 * (radius * Math.Sin(theta * Math.PI / 180 / 2));

            this.legDistance = legDistance;
            this.numLegs = numLegs;
            this.turnRight = turnRight;
            this.crossingDistance = crossingDistance;
            this.radius = radius;
            numCrossings = numLegs - 1;

            if(!turnRight)
            {
                turnDegrees = -turnDegrees;
            }

            Coordinate CSP = datum.travel(orientation - 180, legDistance / 2, dI);
            addPoint(CSP);

            for (int i = 0; i < numLegs; i++)
            {
                addPoint(points.ElementAt(i).travel(orientation, legDistance, dI));

                orientation += turnDegrees;

                addPoint(points.ElementAt(i).travel(orientation, crossingDistance, dI));

                orientation += turnDegrees;
                
            }

            return points;
        }

        public override void calculatePatternInfo(double searchSpeed, double sweepWidth)
        {
            totalTrackLength = crossingDistance * numCrossings + legDistance * numLegs;
            searchedArea = Math.PI * radius * radius;
            searchTime = totalTrackLength / searchSpeed;
            areaEffectivelySwept = totalTrackLength / sweepWidth;
            areaCoverage = areaEffectivelySwept / searchedArea;
            probabilityOfDetection = (1 - Math.Exp(-areaCoverage)) * 100;
        }
    }
}
