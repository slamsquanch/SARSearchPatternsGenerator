﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SARSearchPatternGenerator
{
    public abstract class DistanceUnit
    {
        public abstract double convertTo(double dist);
    }
}
