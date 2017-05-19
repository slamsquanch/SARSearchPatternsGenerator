using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// A base unit class to serve as a template for all other types of distance
    /// unit systems. It uses kilometers as the base system used by convertTo()
    /// and convertFrom().
    /// </summary>
    public abstract class DistanceUnit
    {
        /*
         * Converts dist to the extended class' unit from the base unit.
         */
        public abstract double convertTo(double dist);

        /*
         * Converts dist from the extended class' unit to kilometers.
         */
        public abstract double convertFrom(double dist);
    }
}
