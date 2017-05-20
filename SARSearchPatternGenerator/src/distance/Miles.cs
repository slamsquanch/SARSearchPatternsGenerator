using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SARSearchPatternGenerator
{
    /// <summary>
    /// Holds functions to convert between miles and the base unit (kilometers).
    /// </summary>
    public class Miles : DistanceUnit
    {
        private static Miles unit;

        private Miles() {}

        /*
         * Creates a new Miles object if there isn't one already.
         */
        public static Miles create()
        {
            if (unit == null)
                unit = new Miles();
            return unit;
        }

        /*
         * Converts from the base unit to miles.
         */
        public override double convertTo(double dist)
        {
            return dist * 0.621371;
        }

        /*
         * Converts from miles to the base unit.
         */
        public override double convertFrom(double dist)
        {
            return dist / 0.621371;
        }
    }
}
