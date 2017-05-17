using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SARSearchPatternGenerator
{
    class Ellipsoid
    {
        private double semiMajorAxis;
        private double semiMinorAxis;
        private double eccentricitySquared;
        private double flattening;

        public Ellipsoid()
        {
            semiMajorAxis = 6378137;
            semiMinorAxis = 6356752.3142;
            double semiMajorAxisSquared = semiMajorAxis * semiMajorAxis;
            double semiMinorAxisSquared = semiMinorAxis * semiMinorAxis;
            flattening = (semiMajorAxis - semiMinorAxis) / semiMajorAxis;
            eccentricitySquared = (semiMajorAxisSquared - semiMinorAxisSquared)
                / semiMajorAxisSquared;
        }

        public double getSemiMajorAxis()
        {
            return semiMajorAxis;
        }

        public void setSemiMajorAxis(double d)
        {
            semiMajorAxis = d;
        }

        public double getSemiMinorAxis()
        {
            return semiMinorAxis;
        }

        public void setSemiMinorAxis(double d)
        {
            semiMinorAxis = d;
        }

        public double getEccentricitySquared()
        {
            return eccentricitySquared;
        }

        public void setEccentricitySquared(double e)
        {
            eccentricitySquared = e;
        }

        public double getFlattening()
        {
            return flattening;
        }

        public void setFlattening(double f)
        {
            flattening = f;
        }
    }
}
