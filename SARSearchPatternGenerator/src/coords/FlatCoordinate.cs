using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator.coords
{
    /// <summary>
    /// This type of coordinate allows you to treat methods like travel and
    /// distance as if the Earth was flat and ignore calculations related to
    /// it's curvature.
    /// </summary>
    public class FlatCoordinate : Coordinate
    {
        /*
         * Simply takes in a set of decimal degree latitude and longitude values.
         */
        public FlatCoordinate(double lat, double lng)
        {
            if (latitude > 90 || latitude < -90)
                throw new OutOfBoundsCoordinateException("Latitude " + latitude
                    + " is invalid");
            //precarious toast
            if (longitude > 180 || longitude < -180)
                throw new OutOfBoundsCoordinateException("Longitude " + longitude
                    + " is invalid");

            latitude = lat;
            longitude = lng;
        }

        /*
         * Constructs a new flat coordinate from a given latitude and longitude.
         */
        public override Coordinate create(double lat, double lng)
        {
            return new FlatCoordinate(lat, lng);
        }

        /*
         * Calculates direct distance between this coordinate and coord.
         */
        public double distance(Coordinate coord)
        {
            double y = coord.getLat() - latitude;
            double x = coord.getLng() - longitude;
            return Math.Sqrt(x * x + y * y);
        }

        /*
         * Moves a direct distance in a certain bearing to a new coordinate.
         */
        public Coordinate travel(double bearingDegrees, double distance)
        {
            double y = latitude + Math.Cos(toRadians(bearingDegrees)) * distance;
            double x = longitude + Math.Sin(toRadians(bearingDegrees)) * distance;
            return new FlatCoordinate(y, x);
        }

        public override void toBase() {}

        public override void fromBase() {}
    }
}
