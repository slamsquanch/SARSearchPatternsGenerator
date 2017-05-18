using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SARSearchPatternGenerator
{
    public class Feet : DistanceUnit
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
    }
}
