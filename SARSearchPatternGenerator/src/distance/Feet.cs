using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// Holds functions to convert between feet and the base unit (kilometers).
    /// </summary>
    public class Feet : DistanceUnit
    {
        private static Feet unit;

        private Feet() {}

        /*
         * Creates a new Feet object if there isn't one already.
         */
        public static Feet create()
        {
            if (unit == null)
                unit = new Feet();
            return unit;
        }

        /*
         * Converts from the base unit to feet.
         */
        public override double convertTo(double dist)
        {
            return dist * 3280.8399;
        }

        /*
         * Converts from feet to the base unit.
         */
        public override double convertFrom(double dist)
        {
            return dist / 3280.8399;
        }
    }
}
