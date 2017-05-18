using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// A base coordinate class to serve as a template for all other types of
    /// coordinate systems. It uses Decimal Degree as the base system referred
    /// to by toBase() and fromBase(). The latitude and longitude store the
    /// converted values of the coordinate in Decimal Degree.
    /// </summary>
    public abstract class Coordinate
    {
        protected double latitude;
        protected double longitude;

        protected double toRadians(double deg)
        {
            return Math.PI * deg / 180.0;
        }//precarious toast

        public double getLat()
        {
            return latitude;
        }

        public double getLng()
        {
            return longitude;
        }

        public void setLat(double lat)
        {
            latitude = lat;

            if (lat > 90 || lat < -90)
                throw new OutOfBoundsCoordinateException("Latitude " + lat
                    + " is invalid");

            fromBase();
        }

        public void setLng(double lng)
        {
            longitude = lng;

            if (lng > 180 || lng < -180)
                throw new OutOfBoundsCoordinateException("Longitude " + lng
                    + " is invalid");

            fromBase();
        }

        public double distance(Coordinate coord, DistanceUnit dI)
        {
            double er = dI.convertTo(6366.707);

            double latFrom = toRadians(getLat());
            double latTo = toRadians(coord.getLat());
            double lngFrom = toRadians(getLng());
            double lngTo = toRadians(coord.getLng());

            double d = Math.Acos(Math.Sin(latFrom) * Math.Sin(latTo)
                + Math.Cos(latFrom) * Math.Cos(latTo) * Math.Cos(lngTo - lngFrom))
                * er;

            return d;
        }

        public Coordinate travel(double bearingDegrees, double distance, DistanceUnit dI)
        {
            double er = dI.convertTo(6366.707);

            double distRatio = distance / er;
            double distRatioSine = Math.Sin(distRatio);
            double distRatioCosine = Math.Cos(distRatio);

            //get lat and long in radians
            double startLatRad = latitude * Math.PI / 180;
            double startLonRad = longitude * Math.PI / 180;

            double startLatCos = Math.Cos(startLatRad);
            double startLatSin = Math.Sin(startLatRad);

            double bearingRadians = bearingDegrees * Math.PI / 180;

            double endLatRads = Math.Asin((startLatSin * distRatioCosine)
                + (startLatCos * distRatioSine * Math.Cos(bearingRadians)));

            double endLonRads = startLonRad + Math.Atan2(Math.Sin(bearingRadians)
                * distRatioSine * startLatCos,
                    distRatioCosine - startLatSin * Math.Sin(endLatRads));

            return create(endLatRads / Math.PI * 180, endLonRads / Math.PI * 180);
        }

        public bool equals(Coordinate coord)
        {
            if (getLat() == coord.getLat() && getLng() == coord.getLng())
                return true;
            return false;
        }

        public override string ToString()
        {
            return "Latitude: " + latitude + " Longitude: " + longitude;
        }

        public abstract Coordinate create(double lat, double lng);

        public abstract void toBase();

        public abstract void fromBase();
    }
}

public class OutOfBoundsCoordinateException : Exception
{
    public OutOfBoundsCoordinateException(string message) : base(message)
    {
    }
}