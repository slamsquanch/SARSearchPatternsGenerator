using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// This coordinate system stores a latitude and a longitude zone that
    /// describe a particular rectangular zone of the Earth and a northing
    /// and easting value that describe how far north and east the coordinate
    /// is in the zone.
    /// </summary>
    public class UTMCoord : Coordinate
    {
        private int lngZone;
        private char latZone;
        private double UTMEasting, UTMNorthing;

        /*
         * Takes in a latitude zone between C and X (but not O or I), a longitude
         * zone between 1 and 60, an easting value, and a northing value.
         * It then converts to the Decimal Degree Latitude Longitude system.
         */
        public UTMCoord(int lngZone, char latZone, double UTMEasting, double UTMNorthing)
        {
            char[] invalidChars = { 'A', 'a', 'B', 'b', 'I', 'i', 'O', 'o', 'Y', 'y', 'Z', 'z' };
            if (Array.IndexOf(invalidChars, latZone) != -1)
                throw new OutOfBoundsCoordinateException("Latitude zone " + latZone
                    + " is invalid");
            if (lngZone > 60 || lngZone < 0)
                throw new OutOfBoundsCoordinateException("Longitude zone " + lngZone
                    + " is invalid");
            if(UTMNorthing > 10000000 || UTMNorthing < 0)
                throw new OutOfBoundsCoordinateException("Northing must be between 0 and 10000000");
            if (UTMEasting > 1000000 || UTMEasting < 0)
                throw new OutOfBoundsCoordinateException("Easting must be between 0 and 1000000");

            this.lngZone = lngZone;
            this.latZone = latZone;
            this.UTMEasting = UTMEasting;
            this.UTMNorthing = UTMNorthing;

            toBase();
        }

        public UTMCoord(double latitude, double longitude)
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
            return new UTMCoord(lat, lng);
        }

        public char getLatZone()
        {
            return latZone;
        }

        public void setLatZone(char lat)
        {
            latZone = lat;

            char[] invalidChars = { 'A', 'a', 'B', 'b', 'I', 'i', 'O', 'o', 'Y', 'y', 'Z', 'z' };
            if (Array.IndexOf(invalidChars, lat) != -1)
                throw new OutOfBoundsCoordinateException("Latitude zone " + lat
                    + " is invalid");

            toBase();
        }

        public int getLngZone()
        {
            return lngZone;
        }

        public void setLngZone(int lng)
        {
            lngZone = lng;

            if (lng > 60 || lng < 0)
                throw new OutOfBoundsCoordinateException("Longitude zone " + lng
                    + " is invalid");

            toBase();
        }

        public double getNorthing()
        {
            return UTMNorthing;
        }

        public void setNorthing(double north)
        {
            UTMNorthing = north;

            if (north > 10000000 || north < 0)
                throw new OutOfBoundsCoordinateException("Northing must be between 0 and 10000000");

            toBase();
        }

        public double getEasting()
        {
            return UTMEasting;
        }

        public void setEasting(double east)
        {
            UTMEasting = east;

            if (east > 1000000 || east < 0)
                throw new OutOfBoundsCoordinateException("Easting must be between 0 and 1000000");

            toBase();
        }

        public override void toBase()
        {
            double UTM_F0 = 0.9996;
            Ellipsoid e = new Ellipsoid();
            double a = e.getSemiMajorAxis();
            double eSquared = e.getEccentricitySquared();
            double ePrimeSquared = eSquared / (1.0 - eSquared);
            double e1 = (1 - Math.Sqrt(1 - eSquared)) / (1 + Math.Sqrt(1 - eSquared));
            double x = UTMEasting - 500000.0;

            double y = UTMNorthing;
            int zoneNumber = lngZone;
            char zoneLetter = latZone;

            double longitudeOrigin = (zoneNumber - 1.0) * 6.0 - 180.0 + 3.0;

            // Correct y for southern hemisphere
            if ((zoneLetter - 'N') < 0)
            {
                y -= 10000000.0;
            }

            double m = y / UTM_F0;
            double mu = m
                / (a * (1.0 - eSquared / 4.0 - 3.0 * eSquared * eSquared / 64.0 - 5.0 * Math
                    .Pow(eSquared, 3.0) / 256.0));

            double phi1Rad = mu + (3.0 * e1 / 2.0 - 27.0 * Math.Pow(e1, 3.0) / 32.0)
                * Math.Sin(2.0 * mu)
                + (21.0 * e1 * e1 / 16.0 - 55.0 * Math.Pow(e1, 4.0) / 32.0)
                * Math.Sin(4.0 * mu) + (151.0 * Math.Pow(e1, 3.0) / 96.0)
                * Math.Sin(6.0 * mu);

            double n = a
                / Math.Sqrt(1.0 - eSquared * Math.Sin(phi1Rad) * Math.Sin(phi1Rad));
            double t = Math.Tan(phi1Rad) * Math.Tan(phi1Rad);
            double c = ePrimeSquared * Math.Cos(phi1Rad) * Math.Cos(phi1Rad);
            //precarious toast
            double r = a * (1.0 - eSquared)
                / Math.Pow(1.0 - eSquared * Math.Sin(phi1Rad) * Math.Sin(phi1Rad), 1.5);
            double d = x / (n * UTM_F0);

            double latitude = (phi1Rad - (n * Math.Tan(phi1Rad) / r)
                * (d
                    * d
                    / 2.0
                    - (5.0 + (3.0 * t) + (10.0 * c) - (4.0 * c * c) - (9.0 * ePrimeSquared))
                    * Math.Pow(d, 4.0) / 24.0 + (61.0 + (90.0 * t) + (298.0 * c)
                    + (45.0 * t * t) - (252.0 * ePrimeSquared) - (3.0 * c * c))
                    * Math.Pow(d, 6.0) / 720.0))
                * (180.0 / Math.PI);

            double longitude = longitudeOrigin
                + ((d - (1.0 + 2.0 * t + c) * Math.Pow(d, 3.0) / 6.0 + (5.0 - (2.0 * c)
                    + (28.0 * t) - (3.0 * c * c) + (8.0 * ePrimeSquared) + (24.0 * t * t))
                    * Math.Pow(d, 5.0) / 120.0) / Math.Cos(phi1Rad))
                * (180.0 / Math.PI);

            this.latitude = latitude;
            this.longitude = longitude;
        }

        public override void fromBase()
        {
            if (getLat() < -80 || getLat() > 84)
            {
                throw new OutOfBoundsCoordinateException("Latitude (" + getLat()
                    + ") falls outside the UTM grid.");
            }

            if (this.longitude == 180.0)
            {
                this.longitude = -180.0;
            }

            Ellipsoid e = new Ellipsoid();

            double UTM_F0 = 0.9996;
            double a = e.getSemiMajorAxis();
            double eSquared = e.getEccentricitySquared();
            double longitude = this.longitude;
            double latitude = this.latitude;

            double latitudeRad = latitude * (Math.PI / 180.0);
            double longitudeRad = longitude * (Math.PI / 180.0);
            int longitudeZone = (int)Math.Floor((longitude + 180.0) / 6.0) + 1;

            // Special zone for Norway
            if (latitude >= 56.0 && latitude < 64.0 && longitude >= 3.0
                && longitude < 12.0)
            {
                longitudeZone = 32;
            }

            // Special zones for Svalbard
            if (latitude >= 72.0 && latitude < 84.0)
            {
                if (longitude >= 0.0 && longitude < 9.0)
                {
                    longitudeZone = 31;
                }
                else if (longitude >= 9.0 && longitude < 21.0)
                {
                    longitudeZone = 33;
                }
                else if (longitude >= 21.0 && longitude < 33.0)
                {
                    longitudeZone = 35;
                }
                else if (longitude >= 33.0 && longitude < 42.0)
                {
                    longitudeZone = 37;
                }
            }

            double longitudeOrigin = (longitudeZone - 1) * 6 - 180 + 3;
            double longitudeOriginRad = longitudeOrigin * (Math.PI / 180.0);

            char UTMZ = getUTMLatitudeZoneLetter(latitude);

            double ePrimeSquared = (eSquared) / (1 - eSquared);

            double n = a
                / Math.Sqrt(1 - eSquared * Math.Sin(latitudeRad)
                    * Math.Sin(latitudeRad));
            double t = Math.Tan(latitudeRad) * Math.Tan(latitudeRad);
            double c = ePrimeSquared * Math.Cos(latitudeRad) * Math.Cos(latitudeRad);
            double A = Math.Cos(latitudeRad) * (longitudeRad - longitudeOriginRad);

            double M = a
                * ((1 - eSquared / 4 - 3 * eSquared * eSquared / 64 - 5 * eSquared
                    * eSquared * eSquared / 256)
                    * latitudeRad
                    - (3 * eSquared / 8 + 3 * eSquared * eSquared / 32 + 45 * eSquared
                        * eSquared * eSquared / 1024)
                    * Math.Sin(2 * latitudeRad)
                    + (15 * eSquared * eSquared / 256 + 45 * eSquared * eSquared
                        * eSquared / 1024) * Math.Sin(4 * latitudeRad) - (35 * eSquared
                    * eSquared * eSquared / 3072)
                    * Math.Sin(6 * latitudeRad));

            double UTMEast = (UTM_F0 * n * (A + (1 - t + c)
                    * Math.Pow(A, 3.0) / 6 + (5 - 18 * t + t * t + 72
                    * c - 58 * ePrimeSquared)
                    * Math.Pow(A, 5.0) / 120) + 500000.0);

            double UTMNorth = (UTM_F0 * (M + n * Math.Tan(latitudeRad)
                * (A * A / 2 + (5 - t + (9 * c) + (4 * c * c)) * Math.Pow(A, 4.0) / 24 + (61
                    - (58 * t) + (t * t) + (600 * c) - (330 * ePrimeSquared))
                    * Math.Pow(A, 6.0) / 720)));

            // Adjust for the southern hemisphere
            if (latitude < 0)
            {
                UTMNorth += 10000000.0;
            }

            lngZone = longitudeZone;
            latZone = UTMZ;
            UTMEasting = UTMEast;
            UTMNorthing = UTMNorth;
        }

        public static char getUTMLatitudeZoneLetter(double lat)
        {
            if ((84 >= lat) && (lat >= 72))
                return 'X';
            else if ((72 > lat) && (lat >= 64))
                return 'W';
            else if ((64 > lat) && (lat >= 56))
                return 'V';
            else if ((56 > lat) && (lat >= 48))
                return 'U';
            else if ((48 > lat) && (lat >= 40))
                return 'T';
            else if ((40 > lat) && (lat >= 32))
                return 'S';
            else if ((32 > lat) && (lat >= 24))
                return 'R';
            else if ((24 > lat) && (lat >= 16))
                return 'Q';
            else if ((16 > lat) && (lat >= 8))
                return 'P';
            else if ((8 > lat) && (lat >= 0))
                return 'N';
            else if ((0 > lat) && (lat >= -8))
                return 'M';
            else if ((-8 > lat) && (lat >= -16))
                return 'L';
            else if ((-16 > lat) && (lat >= -24))
                return 'K';
            else if ((-24 > lat) && (lat >= -32))
                return 'J';
            else if ((-32 > lat) && (lat >= -40))
                return 'H';
            else if ((-40 > lat) && (lat >= -48))
                return 'G';
            else if ((-48 > lat) && (lat >= -56))
                return 'F';
            else if ((-56 > lat) && (lat >= -64))
                return 'E';
            else if ((-64 > lat) && (lat >= -72))
                return 'D';
            else if ((-72 > lat) && (lat >= -80))
                return 'C';
            else
                return 'Z';
        }
    }
}