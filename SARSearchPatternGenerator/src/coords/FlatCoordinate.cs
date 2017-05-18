using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator.coords
{
    public class FlatCoordinate : Coordinate
    {
        public FlatCoordinate(double lat, double lng)
        {
            latitude = lat;
            longitude = lng;
        }
        public override Coordinate create(double lat, double lng)
        {
            return new FlatCoordinate(lat, lng);
        }
        new public double distance(Coordinate coord)
        {
            double y = coord.getLat() - latitude;
            double x = coord.getLng() - longitude;
            return Math.Sqrt(x * x + y * y);
        }
        new public Coordinate travel(double bearingDegrees, double distance)
        {
            double y = latitude + Math.Cos(toRadians(bearingDegrees)) * distance;
            double x = longitude + Math.Sin(toRadians(bearingDegrees)) * distance;
            return new FlatCoordinate(y, x);
        }
        public override void toBase()
        {
        }
        public override void fromBase()
        {
        }
    }
}
