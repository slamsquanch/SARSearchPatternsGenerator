using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// The base class for all types of search patterns. Stores a list of points
    /// that guide the pattern.
    /// </summary>
    [DataContract]
    [KnownType(typeof(DecDeg))]
    [KnownType(typeof(DegDecMin))]
    [KnownType(typeof(DegMinSec))]
    [KnownType(typeof(UTMCoord))]
    public class Pattern
    {
        protected List<Coordinate> points;
        protected double orientation, legDistance, totalTrackLength, areaEffectivelySwept, areaCoverage, searchedArea, searchTime, probabilityOfDetection;
        protected Coordinate datum;
        protected int numLegs;
        protected bool turnRight;


        public Pattern()
        {
            points = new List<Coordinate>();
        }

        /*
         *  Returns a Color class array of 6 different colours that the KML and GPX classes can iterate through
         *   to alternate the track leg colours of the Point-to-Point track pattern.  This method is "virtual" so
         *   that its child classes can override it to best suit their pattern's needs.
         */
        public virtual Color[] getColours()
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
         * Adds a coordinate to the search pattern.
         */
        public void addPoint(Coordinate point)
        {
            points.Add(point);
        }

        /*
         * Returns a particular point in the search pattern.
         */
        public Coordinate getPoint(int index)
        {
            return points[index];
        }

        /*
         * Returns the list of coordinates in the pattern.
         */
        public List<Coordinate> getPattern()
        {
            return points;
        }

        /*
         * Removes a coordinate from the pattern.
         */
        public void removePoint(Coordinate point)
        {
            points.Remove(point);
        }

        /*
         * Clears all coordinates from the pattern.
         */
        public void clearPoints()
        {
            points = new List<Coordinate>();
        }

        /*
         * Finds and returns the max latitude value of a pattern.
         */
        public double maxLat()
        {
            double max = points[0].getLat();
            for (int i = 1; i < points.Count; i++)
            {
                max = Math.Max(max, points[i].getLat());
            }
            return max;
        }

        /*
         * Finds and returns the max longitude value of a pattern.
         */
        public double maxLong()
        {
            double max = points[0].getLng();
            for (int i = 1; i < points.Count; i++)
            {
                max = Math.Max(max, points[i].getLng());
            }
            return max;
        }

        /*
         * Finds and returns the minimum latitude value of a pattern.
         */
        public double minLat()
        {
            double min = points[0].getLat();
            for (int i = 1; i < points.Count; i++)
            {
                min = Math.Min(min, points[i].getLat());
            }
            return min;
        }

        /*
         * Finds and returns the minimum longitude value of a pattern.
         */
        public double minLong()
        {
            double min = points[0].getLng();
            for (int i = 1; i < points.Count; i++)
            {
                min = Math.Min(min, points[i].getLng());
            }
            return min;
        }

        /*
         *  Sets the datum point of a pattern.
         */
        public void setDatum(Coordinate d)
        {
            this.datum = d;
        }


        /*
         *  Gets the datum point of a pattern.
         */
        public Coordinate getDatum()
        {
            if (this.datum == null)
            {
                if (this.points.Count > 0)
                {
                    return this.points[0];
                }
                else
                {
                    return null;
                }
            }
            return this.datum;
        }

        public virtual void calculatePatternInfo(double searchSpeed, double sweepWidth) {}

        public double getSearchTime()
        {
            return searchTime;
        }

        public double getAreaEffectivelySwept()
        {
            return areaEffectivelySwept;
        }

        public double getAreaCoverage()
        {
            return areaCoverage;
        }

        public double getProbabilityOfDetection()
        {
            return probabilityOfDetection;
        }

        public bool turnsRight()
        {
            return turnRight;
        }

        public double getOrientation()
        {
            return orientation;
        }

        public double getLegDistance()
        {
            return legDistance;
        }

        public int getNumLegs()
        {
            return numLegs;
        }

        public int getNumPoints()
        {
            return points.Count;
        }
    }
}
