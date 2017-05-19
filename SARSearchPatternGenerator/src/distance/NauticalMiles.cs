using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// Holds functions to convert between nautical miles and the base unit
    /// (kilometers).
    /// </summary>
    public class NauticalMiles : DistanceUnit
    {
        private static NauticalMiles unit;

        private NauticalMiles() {}

        /*
         * Creates a new NauticalMiles object if there isn't one already.
         */
        public static NauticalMiles create()
        {
            if (unit == null)
                unit = new NauticalMiles();
            return unit;
        }

        /*
         * Converts from the base unit to nautical miles.
         */
        public override double convertTo(double dist)
        {
            return dist * 0.539957;
        }

        /*
         * Converts from nautical miles to the base unit.
         */
        public override double convertFrom(double dist)
        {
            return dist / 0.539957;
        }
    }
}
