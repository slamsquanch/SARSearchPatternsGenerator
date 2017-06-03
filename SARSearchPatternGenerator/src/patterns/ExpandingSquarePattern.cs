using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SARSearchPatternsGenerator.src;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// Contains methods that generate an expanding square pattern and stores
    /// the coordinates in the points variable.
    /// </summary>
    public class ExpandingSquarePattern : Pattern
    {

        public ExpandingSquarePattern() :base()
        {
            comment = (string)DefaultComments.ResourceManager.GetObject("ExpandingSquareComment");
        }

        /*
         *  Returns a Color class array of 6 different colours that the KML and GPX classes can iterate through
         *   to alternate the track leg colours of the Expanding Square pattern. 
         *   Overrides parent's method (Pattern.cs).
         */
        public override Color[] getColours()
        {
            //size 6
            Color[] legColours = new Color[]
            {
            Color.Red,
            Color.Blue,
            Color.Yellow,
            Color.Purple,
            Color.Green, 
            Color.Cyan
            };
            return legColours;
        }


        /*
         * Generates an expanding square search starting at the datum with the
         * given number of legs of the given size. The first leg will go in the
         * bearing specified by orientation, and then you will turn right or left
         * depending on the value of turnRight for the first turn.
         */
        public List<Coordinate> generatePattern(Coordinate datum, int numLegs, double orientation, double firstLegDistance, bool turnRight, DistanceUnit dUnit)
        {
            double turnDegrees, legDistance;
            bool secondLeg = false;

            legDistance = firstLegDistance;
            this.legDistance = firstLegDistance;
            this.numLegs = numLegs;
            this.turnRight = turnRight;
            this.orientation = orientation;
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
                addPoint(points.ElementAt(i).travel(orientation, legDistance, dUnit));

                //Turn orientation for next leg
                orientation += turnDegrees;

                totalTrackLength += legDistance;

                if (secondLeg)
                {
                    //Increase legDistance
                    legDistance += firstLegDistance;
                }

                secondLeg = !secondLeg;

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
            areaEffectivelySwept = totalTrackLength * sweepWidth;
            areaCoverage = areaEffectivelySwept / searchedArea;
            probabilityOfDetection = (1 - Math.Exp(-areaCoverage)) * 100;
        }
    }
}
