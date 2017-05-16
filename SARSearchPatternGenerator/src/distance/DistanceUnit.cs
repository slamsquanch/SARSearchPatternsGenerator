using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARSearchPatternGenerator
{
    public abstract class DistanceUnit
    {
        public abstract double convertTo(double dist);
    }
}
