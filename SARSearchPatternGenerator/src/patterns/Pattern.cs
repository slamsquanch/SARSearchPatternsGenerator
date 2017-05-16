using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
