using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
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
        }

        public void setLng(double lng)
        {
            longitude = lng;
        }

        public double distance(Coordinate coord)
        {
            double er = Kilometers.create().convertTo(6366.707);

            double latFrom = toRadians(getLat());
            double latTo = toRadians(coord.getLat());
            double lngFrom = toRadians(getLng());
            double lngTo = toRadians(coord.getLng());

            double d = Math.Acos(Math.Sin(latFrom) * Math.Sin(latTo)
                + Math.Cos(latFrom) * Math.Cos(latTo) * Math.Cos(lngTo - lngFrom))
                * er;

            return d;
        }

        public Coordinate travel(double bearingDegrees, double distance)
        {
            double er = Kilometers.create().convertTo(6366.707);

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

        public bool Equals(Coordinate other)
        {
            return other.latitude == latitude && other.longitude == longitude;
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