using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
using System.Threading;
=======
>>>>>>> 8f1ee7e0db690875a8367b4b86fbcb06209faa6a

namespace SARSearchPatternGenerator
{
    public class SectorSearchPattern : Pattern
    {
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
    }
}
