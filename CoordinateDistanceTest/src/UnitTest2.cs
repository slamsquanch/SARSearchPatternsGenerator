using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SARSearchPatternGenerator;
using System.Collections.Generic;

namespace SARUnitTesting
{
    /// <summary>
    /// Tests for finding a point based on a starting coordinate and bearing.
    /// </summary>
    [TestClass]
    public class UnitTest2
    {
        DistanceUnit dUnit = Kilometers.create();

        [TestMethod]
        public void TestMethod10()
        {
            Coordinate expected = new DegMinSec(49, 20, 54, -123, 8, 45);

            Coordinate coord = new DecDeg(49.3484, -123.2564);

            Coordinate actual = coord.travel(90, 8, dUnit);
            Assert.AreEqual(expected.getLat(), actual.getLat(), 2, "You dun goofed");
            Assert.AreEqual(expected.getLng(), actual.getLng(), 2, "You dun goofed");
        }

        [TestMethod]
        public void TestMethod11()
        {
            Coordinate expected = new DegMinSec(41, 15, 16, -123, 15, 23);

            Coordinate coord = new DecDeg(49.3484, -123.2564);

            Coordinate actual = coord.travel(180, 900, dUnit);
            Assert.AreEqual(expected.getLat(), actual.getLat(), 2, "You dun goofed");
            Assert.AreEqual(expected.getLng(), actual.getLng(), 2, "You dun goofed");
        }

        [TestMethod]
        public void TestMethod12()
        {
            Coordinate expected = new DegMinSec(48, 56, 52, -113, 38, 43);

            Coordinate coord = new DecDeg(49.3484, -123.2564);

            Coordinate actual = coord.travel(-270, 700, dUnit);
            Assert.AreEqual(expected.getLat(), actual.getLat(), 2, "You dun goofed");
            Assert.AreEqual(expected.getLng(), actual.getLng(), 2, "You dun goofed");
        }

        [TestMethod]
        public void TestMethod13()
        {
            Coordinate expected = new DegMinSec(48, 52, 17, -122, 39, 43);

            Coordinate coord = new DecDeg(49.3484, -123.2564);

            Coordinate actual = coord.travel(500, 69, dUnit);
            Assert.AreEqual(expected.getLat(), actual.getLat(), 2, "You dun goofed");
            Assert.AreEqual(expected.getLng(), actual.getLng(), 2, "You dun goofed");
        }

        [TestMethod]
        public void TestMethod14()
        {
            UTMCoord expected = new UTMCoord(10, 'U', 525665.7, 5413217.9);

            Coordinate coord = new DecDeg(49.3484, -123.2564);

            Coordinate actual = coord.travel(500, 69, dUnit);

            UTMCoord actualUTM = new UTMCoord(actual.getLat(), actual.getLng());

            Assert.AreEqual(expected.getLatZone(), actualUTM.getLatZone(), "You dun goofed");
            Assert.AreEqual(expected.getLngZone(), actualUTM.getLngZone(), "You dun goofed");
            Assert.AreEqual(expected.getEasting(), actualUTM.getEasting(), 100, "You dun goofed");
            Assert.AreEqual(expected.getNorthing(), actualUTM.getNorthing(), 100, "You dun goofed");
        }

        [TestMethod]
        public void TestMethod15()
        {
            List<Coordinate> expected = new List<Coordinate>();

            expected.Add(new DegMinSec(49, 12, 36, 122, 54, 15));
            expected.Add(new DegMinSec(49, 12, 36, 122, 54, 13));
            expected.Add(new DegMinSec(49, 12, 34, 122, 54, 13));
            expected.Add(new DegMinSec(49, 12, 34, 122, 54, 18));

            Coordinate datum = expected[0];
            int numLegs = 3;
            double orientation = 90;
            double firstLegDistance = 0.05;
            bool turnRight = true;

            ExpandingSquarePattern actual = new ExpandingSquarePattern();

            actual.generatePattern(datum, numLegs, orientation, firstLegDistance, turnRight, dUnit);

            for (int i = 0; i <= numLegs; i++)
            {
                Assert.AreEqual(expected[i].getLat(), actual.getPoint(i).getLat(), 1,
                    "Point " + i + " was not correct \nActual - " + actual.getPoint(i).ToString() + "\nExpected - " + expected[i].ToString());
                Assert.AreEqual(expected[i].getLng(), actual.getPoint(i).getLng(), 1,
                    "Point " + i + " was not correct \nActual - " + actual.getPoint(i).ToString() + "\nExpected - " + expected[i].ToString());
            }
        }
    }
}
