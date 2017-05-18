using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    public abstract class DistanceUnit
    {
        public abstract double convertTo(double dist);
        public abstract double convertFrom(double dist);
    }
}
