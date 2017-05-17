using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    class Feet : DistanceUnit
    {
        private static Feet unit;

        private Feet()
        {

        }

        public static Feet create()
        {
            if (unit == null)
                unit = new Feet();
            return unit;
        }

        public override double convertTo(double dist)
        {
            return dist * 3280.8399;
        }

        public override double convertFrom(double dist)
        {
            return dist / 3280.8399;
        }
    }
}
