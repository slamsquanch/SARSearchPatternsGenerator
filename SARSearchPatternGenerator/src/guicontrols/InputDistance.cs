using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// An input class that determines what unit the program uses for measuring
    /// distance.
    /// </summary>
    public class InputDistance : InputUnits
    {
        private DistanceUnit _unit = NauticalMiles.create();
        public DistanceUnit unit {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
            }
        }
    }
}
