using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// A base coordinate class to serve as a template for all other types of
    /// coordinate systems. It uses Decimal Degree as the base system referred
    /// to by toBase() and fromBase(). The latitude and longitude store the
    /// converted values of the coordinate in Decimal Degree.
    /// </summary>
    [DataContract]
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

        /*
         * Latitude must be between -90 and 90
         */
        public void setLat(double lat)
        {
            latitude = lat;

            if (lat > 90 || lat < -90)
                throw new OutOfBoundsCoordinateException("Latitude " + lat
                    + " is invalid");

            fromBase();
        }

        /*
         * Longitudes must be between -180 and 180
         */
        public void setLng(double lng)
        {
            longitude = lng;

            if (lng > 180 || lng < -180)
                throw new OutOfBoundsCoordinateException("Longitude " + lng
                    + " is invalid");

            fromBase();
        }

        /*
         * Calulates the distance from this coordinate to coord, in the distance
         * unit specified by dUnit.
         */
        public double distance(Coordinate coord, DistanceUnit dUnit)
        {
            double er = dUnit.convertTo(6366.707);

            double latFrom = toRadians(getLat());
            double latTo = toRadians(coord.getLat());
            double lngFrom = toRadians(getLng());
            double lngTo = toRadians(coord.getLng());

            double d = Math.Acos(Math.Sin(latFrom) * Math.Sin(latTo)
                + Math.Cos(latFrom) * Math.Cos(latTo) * Math.Cos(lngTo - lngFrom))
                * er;

            return d;
        }

        /*
         * Moves a specified distance at a certain bearing from this Coordinate
         * to find a new Coordinate. DUnit specifies the unit of distance that
         * you are inputting.
         */
        public Coordinate travel(double bearingDegrees, double distance, DistanceUnit dUnit)
        {
            double er = dUnit.convertTo(6366.707);

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

        /*
         * If the longitude is out of bounds, this will put it back in bounds.
         */
        public static double fixLong(double lng)
        {
            double adjusted = lng;

            while (adjusted > 180)
                adjusted -= 360;
            while (adjusted < -180)
                adjusted += 360;

            return adjusted;
        }

        /*
         * If the latitude and longitude are equal, then this coordinate is equal
         * to coord.
         */
        public bool equals(Coordinate coord)
        {
            if (getLat() == coord.getLat() && getLng() == coord.getLng())
                return true;
            return false;
        }

        /*
         * Prints the latitude and longitude of this coordinate.
         */
        public override string ToString()
        {
            return "Latitude: " + latitude + " Longitude: " + longitude;
        }

        /*
         * Creates a new coordinate of the extended coordinate system type.
         */
        public abstract Coordinate create(double lat, double lng);

        /*
         * Converts from the extended coordinate system to the base system,
         * which is Decimal Degree. The results should be stored in
         * the latitude and longitude variables.
         */
        public abstract void toBase();

        /*
         * Converts to the extended coordinate system from the base system,
         * which is Decimal Degree. The results should be stored in
         * the appropriate variables in the extended class.
         */
        public abstract void fromBase();
    }
}

/// <summary>
/// Thrown when an coordinate value is input that is out of bounds/invalid.
/// </summary>
public class OutOfBoundsCoordinateException : Exception
{
    public OutOfBoundsCoordinateException(string message) : base(message) {}
}