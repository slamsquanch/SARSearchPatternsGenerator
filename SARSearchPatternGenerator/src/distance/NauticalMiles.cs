using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARSearchPatternGenerator
{
    class NauticalMiles : DistanceUnit
    {
        private static NauticalMiles unit;

        private NauticalMiles()
        {

        }

        public static NauticalMiles create()
        {
            if (unit == null)
                unit = new NauticalMiles();
            return unit;
        }

        public override double convertTo(double dist)
        {
            return dist * 0.539957;
        }
    }
}
