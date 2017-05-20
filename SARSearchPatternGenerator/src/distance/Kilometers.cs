using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// Holds functions to convert between kilometers and the base unit
    /// (kilometers).
    /// </summary>
    public class Kilometers : DistanceUnit
    {
        private static Kilometers unit;

        private Kilometers() {}

        /*
         * Creates a new Kilometers object if there isn't one already.
         */
        public static Kilometers create()
        {
            if (unit == null)
                unit = new Kilometers();
            return unit;
        }

        /*
         * Converts from the base unit to kilometers.
         */
        public override double convertTo(double dist)
        {
            return dist;
        }

        /*
         * Converts from kilometers to the base unit.
         */
        public override double convertFrom(double dist)
        {
            return dist;
        }
    }
}
