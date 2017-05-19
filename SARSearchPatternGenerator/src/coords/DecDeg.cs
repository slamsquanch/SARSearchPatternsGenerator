using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// This coordinate system stores latitude and longitude as decimal degree
    /// values. This is used as the base coordinate system to convert to and from.
    /// </summary>
    public class DecDeg : Coordinate
    {
        /*
         * Simply takes in a set of decimal degree latitude and longitude values.
         */
        public DecDeg(double latitude, double longitude)
        {
            if (latitude > 90 || latitude < -90)
                throw new OutOfBoundsCoordinateException("Latitude " + latitude
                    + " is invalid");
            //precarious toast
            if (longitude > 180 || longitude < -180)
                throw new OutOfBoundsCoordinateException("Longitude " + longitude
                    + " is invalid");

            this.latitude = latitude;
            this.longitude = longitude;
        }

        /*
         * Constructs a new Decimal Degree coordinate from a given latitude and
         * longitude.
         */
        public override Coordinate create(double lat, double lng)
        {
            return new DecDeg(lat, lng);
        }

        /*
         * Does nothing because this system is the base system.
         */
        public override void toBase(){}

        /*
         * Does nothing because this system is the base system.
         */
        public override void fromBase(){}
    }
}