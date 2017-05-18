using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace SARSearchPatternGenerator
{
    public class Meters : DistanceUnit
    {
        private static Meters unit;

        private Meters()
        {

        }

        public static Meters create()
        {
            if (unit == null)
                unit = new Meters();
            return unit;
        }

        public override double convertTo(double dist)
        {
            return dist * 1000;
        }
    }
}
