using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SARSearchPatternGenerator
{
    public class Pattern
    {
        protected List<Coordinate> points;


        public Pattern()
        {
            points = new List<Coordinate>();
        }

        public void addPoint(Coordinate point)
        {
            points.Add(point);
        }

        public Coordinate getPoint(int index)
        {
            return points[index];
        }

        public List<Coordinate> getPattern()
        {
            return points;
        }

        public void removePoint(Coordinate point)
        {
            points.Remove(point);
        }

        public void clearPoints()
        {
            points = new List<Coordinate>();
        }


        /*
         *  Finds and returns the max latitude value of a pattern.
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
         *  Finds and returns the max longitude value of a pattern.
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
         *  Finds and returns the minimum latitude value of a pattern.
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
         *  Finds and returns the minimum longitude value of a pattern.
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

    }
}
