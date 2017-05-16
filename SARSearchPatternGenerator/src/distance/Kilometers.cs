using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARSearchPatternGenerator
{
    class Kilometers : DistanceUnit
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
    }
}
