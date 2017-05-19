using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SARSearchPatternGenerator
{
    public class DecDeg : Coordinate
    {
        public DecDeg(double latitude, double longitude)
        {
            if (latitude > 90 || latitude < -90)
                throw new OutOfBoundsCoordinateException("Latitude " + latitude
                    + " is invalid");
            if(longitude > 180 || longitude < -180)
                throw new OutOfBoundsCoordinateException("Longitude " + longitude
                    + " is invalid");

            this.latitude = latitude;
            this.longitude = longitude;
        }

        public override Coordinate create(double lat, double lng)
        {
            return new DecDeg(lat, lng);
        }

        public override void toBase(){}

        public override void fromBase(){}
    }
}