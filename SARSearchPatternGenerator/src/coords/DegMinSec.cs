using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    public class DegMinSec : Coordinate
    {
        protected double latDegrees;
        protected double lngDegrees;
        protected double latMinutes;
        protected double lngMinutes;
        protected double latSeconds;
        protected double lngSeconds;

        public DegMinSec(double latDegrees, double latMinutes, double latSeconds,
            double lngDegrees, double lngMinutes, double lngSeconds)
        {
            if (latDegrees > 90 || latDegrees < -90
                || ((latDegrees == 90 || latDegrees == -90) && latMinutes + latSeconds != 0)
                || latMinutes > 60 || latMinutes < 0
                || latSeconds > 60 || latSeconds < 0)
                throw new OutOfBoundsCoordinateException("Latitude (" + latDegrees
                    + ", " + latMinutes + ", " + latSeconds + ") is invalid");
            if (lngDegrees > 180 || lngDegrees < -180
                || ((lngDegrees == 180 || lngDegrees == -180) && lngMinutes + lngSeconds != 0)
                || lngMinutes > 60 || lngMinutes < 0
                || lngSeconds > 60 || lngSeconds < 0)
                throw new OutOfBoundsCoordinateException("Longitude (" + lngDegrees
                    + ", " + lngMinutes + ", " + lngSeconds + ") is invalid");

            this.latDegrees = latDegrees;
            this.latMinutes = latMinutes;
            this.latSeconds = latSeconds;
            this.lngDegrees = lngDegrees;
            this.lngMinutes = lngMinutes;
            this.lngSeconds = lngSeconds;

            toBase();
        }

        public DegMinSec(double latitude, double longitude)
        {
            if (latitude > 90 || latitude < -90)
                throw new OutOfBoundsCoordinateException("Latitude " + latitude
                    + " is invalid");
            if (longitude > 180 || longitude < -180)
                throw new OutOfBoundsCoordinateException("Longitude " + longitude
                    + " is invalid");

            this.latitude = latitude;
            this.longitude = longitude;

            fromBase();
        }

        public override Coordinate create(double lat, double lng)
        {
            return new DegMinSec(lat, lng);
        }

        public double getLatDeg()
        {
            return latDegrees;
        }

        public void setLatDeg(double latDeg)
        {
            latDegrees = latDeg;

            if (latDeg > 90 || latDeg < -90
                || ((latDeg == 90 || latDeg == -90) && latMinutes + latSeconds != 0)
                || latMinutes > 60 || latMinutes < 0
                || latSeconds > 60 || latSeconds < 0)
                throw new OutOfBoundsCoordinateException("Latitude (" + latDeg
                    + ", " + latMinutes + ", " + latSeconds + ") is invalid");

            toBase();
        }

        public double getLngDeg()
        {
            return lngDegrees;
        }

        public void setLngDeg(double lngDeg)
        {
            lngDegrees = lngDeg;

            if (lngDeg > 180 || lngDeg < -180
                || ((lngDeg == 180 || lngDeg == -180) && lngMinutes + lngSeconds != 0)
                || lngMinutes > 60 || lngMinutes < 0
                || lngSeconds > 60 || lngSeconds < 0)
                throw new OutOfBoundsCoordinateException("Longitude (" + lngDeg
                    + ", " + lngMinutes + ", " + lngSeconds + ") is invalid");

            toBase();
        }

        public double getLatMin()
        {
            return latMinutes;
        }

        public void setLatMin(double latMin)
        {
            latMinutes = latMin;

            if (latDegrees > 90 || latDegrees < -90
                || ((latDegrees == 90 || latDegrees == -90) && latMin + latSeconds != 0)
                || latMin > 60 || latMin < 0
                || latSeconds > 60 || latSeconds < 0)
                throw new OutOfBoundsCoordinateException("Latitude (" + latDegrees
                    + ", " + latMin + ", " + latSeconds + ") is invalid");

            toBase();
        }

        public double getLngMin()
        {
            return lngMinutes;
        }

        public void setLngMin(double lngMin)
        {
            lngMinutes = lngMin;

            if (lngDegrees > 180 || lngDegrees < -180
                || ((lngDegrees == 180 || lngDegrees == -180) && lngMin + lngSeconds != 0)
                || lngMin > 60 || lngMin < 0
                || lngSeconds > 60 || lngSeconds < 0)
                throw new OutOfBoundsCoordinateException("Longitude (" + lngDegrees
                    + ", " + lngMin + ", " + lngSeconds + ") is invalid");

            toBase();
        }

        public double getLatSec()
        {
            return latSeconds;
        }

        public void setLatSec(double latSec)
        {
            latSeconds = latSec;

            if (latDegrees > 90 || latDegrees < -90
                || ((latDegrees == 90 || latDegrees == -90) && latMinutes + latSec != 0)
                || latMinutes > 60 || latMinutes < 0
                || latSec > 60 || latSec < 0)
                throw new OutOfBoundsCoordinateException("Latitude (" + latDegrees
                    + ", " + latMinutes + ", " + latSec + ") is invalid");

            toBase();
        }

        public double getLngSec()
        {
            return lngSeconds;
        }

        public void setLngSec(double lngSec)
        {
            lngSeconds = lngSec;

            if (lngDegrees > 180 || lngDegrees < -180
                || ((lngDegrees == 180 || lngDegrees == -180) && lngMinutes + lngSec != 0)
                || lngMinutes > 60 || lngMinutes < 0
                || lngSec > 60 || lngSec < 0)
                throw new OutOfBoundsCoordinateException("Longitude (" + lngDegrees
                    + ", " + lngMinutes + ", " + lngSec + ") is invalid");

            toBase();
        }

        public override void fromBase()
        {
            latDegrees = (int)latitude;
            latMinutes = (latitude - latDegrees) * 60;
            latSeconds = latMinutes;
            latMinutes = (int)latMinutes;
            latSeconds = (latSeconds - latMinutes) * 60;
            
            lngDegrees = (int)longitude;
            lngMinutes = (longitude - lngDegrees) * 60;
            lngSeconds = lngMinutes;
            lngMinutes = (int)lngMinutes;
            lngSeconds = (lngSeconds - lngMinutes) * 60;
        }

        public override void toBase()
        {
            latitude = (latSeconds / 60 + latMinutes) / 60 + latDegrees;

            longitude = (lngSeconds / 60 + lngMinutes) / 60 + lngDegrees;
        }
    }
}
