using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SARSearchPatternGenerator
{
    public class DegDecMin : Coordinate
    {
        protected double latDegrees;
        protected double lngDegrees;
        protected double latMinutes;
        protected double lngMinutes;

        public DegDecMin(double latDegrees, double latMinutes, double lngDegrees, double lngMinutes)
        {
            if (latDegrees > 90 || latDegrees < -90
                || ((latDegrees == 90 || latDegrees == -90) && latMinutes != 0)
                || latMinutes > 60 || latMinutes < 0)
                throw new OutOfBoundsCoordinateException("Latitude (" + latDegrees
                    + ", " + latMinutes + ") is invalid");
            if (lngDegrees > 180 || lngDegrees < -180
                || ((lngDegrees == 180 || lngDegrees == -180) && lngMinutes != 0)
                || lngMinutes > 60 || lngMinutes < 0)
                throw new OutOfBoundsCoordinateException("Longitude (" + lngDegrees
                    + ", " + lngMinutes + ") is invalid");

            this.latDegrees = latDegrees;
            this.latMinutes = latMinutes;
            this.lngDegrees = lngDegrees;
            this.lngMinutes = lngMinutes;

            toBase();
        }

        public DegDecMin(double latitude, double longitude)
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
            return new DegDecMin(lat, lng);
        }

        public double getLatDeg()
        {
            return latDegrees;
        }

        public void setLatDeg(double latDeg)
        {
            latDegrees = latDeg;

            if (latDeg > 90 || latDeg < -90
                || ((latDeg == 90 || latDeg == -90) && latMinutes != 0)
                || latMinutes > 60 || latMinutes < 0)
                throw new OutOfBoundsCoordinateException("Latitude (" + latDeg
                    + ", " + latMinutes + ") is invalid");

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
                || ((lngDeg == 180 || lngDeg == -180) && lngMinutes != 0)
                || lngMinutes > 60 || lngMinutes < 0)
                throw new OutOfBoundsCoordinateException("Longitude (" + lngDeg
                    + ", " + lngMinutes + ") is invalid");

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
                || ((latDegrees == 90 || latDegrees == -90) && latMin != 0)
                || latMin > 60 || latMin < 0)
                throw new OutOfBoundsCoordinateException("Latitude (" + latDegrees
                    + ", " + latMin + ") is invalid");

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
                || ((lngDegrees == 180 || lngDegrees == -180) && lngMin != 0)
                || lngMin > 60 || lngMin < 0)
                throw new OutOfBoundsCoordinateException("Longitude (" + lngDegrees
                    + ", " + lngMin + ") is invalid");

            toBase();
        }

        public override void fromBase()
        {
            latDegrees = (int)latitude;
            latMinutes = (latitude - latDegrees) * 60;

            lngDegrees = (int)longitude;
            lngMinutes = (longitude - lngDegrees) * 60;
        }

        public override void toBase()
        {
            latitude = latMinutes / 60 + latDegrees;

            longitude = lngMinutes / 60 + lngDegrees;
        }
    }
}
