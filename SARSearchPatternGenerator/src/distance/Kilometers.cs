using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    public class Kilometers : DistanceUnit
    {
        private static Kilometers unit;

        private Kilometers()
        {

        }

        public static Kilometers create()
        {
            if (unit == null)
                unit = new Kilometers();
            return unit;
        }

        public override double convertTo(double dist)
        {
            return dist;
        }

        public override double convertFrom(double dist)
        {
            return dist;
        }
    }
}
