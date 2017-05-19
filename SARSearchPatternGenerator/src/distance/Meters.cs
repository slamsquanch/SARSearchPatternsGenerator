using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// Holds functions to convert between meters and the base unit (kilometers).
    /// </summary>
    public class Meters : DistanceUnit
    {
        private static Meters unit;

        private Meters() {}

        /*
         * Creates a new Meters object if there isn't one already.
         */
        public static Meters create()
        {
            if (unit == null)
                unit = new Meters();
            return unit;
        }

        /*
         * Converts from the base unit to meters.
         */
        public override double convertTo(double dist)
        {
            return dist * 1000;
        }

        /*
         * Converts from meters to the base unit.
         */
        public override double convertFrom(double dist)
        {
            return dist / 1000;
        }
    }
}
