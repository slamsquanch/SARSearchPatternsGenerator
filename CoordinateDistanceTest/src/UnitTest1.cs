using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SARSearchPatternGenerator;

namespace CoordinateDistanceTest
{
    [TestClass]
    public class UnitTest1
    {
        Coordinate coords1, coords2;

        [TestMethod]
        public void TestMethod1()
        {
            double expected = 1769;

            coords1 = new DegDecMin(73, 4, 55, 55); //=73.0667, 55.9167
            coords2 = new DegMinSec(57, 19, 12, 49, 59, 33); //=57.3200, 49.9925

            double actual = coords1.distance(coords2);
            Assert.AreEqual(expected, actual, 10, "You dun goofed");
        }

        [TestMethod]
        public void TestMethod2()
        {
            double expected = 8138;

            coords1 = new UTMCoord(10, 'U', 481375, 5466221); //=49.3484, -123.2564
            coords2 = new DegMinSec(57, 19, 12, 49, 59, 33); //=57.3200, 49.9925

            double actual = coords1.distance(coords2);
            Assert.AreEqual(expected, actual, 10, "You dun goofed");
        }

        //checking to see if the problem was conversion from UTM to latlong
        //(it's not apparently)
        [TestMethod]
        public void TestMethod3()
        {
            double expected = 8138;

            coords1 = new DecDeg(49.3484, -123.2564);
            coords2 = new DegMinSec(57, 19, 12, 49, 59, 33); //=57.3200, 49.9925

            double actual = coords1.distance(coords2);
            Assert.AreEqual(expected, actual, 10, "You dun goofed");
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfBoundsCoordinateException), "You dun goofed")]
        public void TestMethod4()
        {
            coords1 = new DegDecMin(73, 800000, 55, 55);
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfBoundsCoordinateException), "You dun goofed")]
        public void TestMethod5()
        {
            coords2 = new DegMinSec(57, 19, 33, 49, 59, 99999);
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfBoundsCoordinateException), "You dun goofed")]
        public void TestMethod6()
        {
            coords1 = new DecDeg(49.3484, -200000000);
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfBoundsCoordinateException), "You dun goofed")]
        public void TestMethod7()
        {
            coords2 = new UTMCoord(10, 'Z', 481375, 5466221);
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfBoundsCoordinateException), "You dun goofed")]
        public void TestMethod8()
        {
            coords1 = new UTMCoord(10, 'U', 481375, -1000);
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfBoundsCoordinateException), "You dun goofed")]
        public void TestMethod9()
        {
            coords2 = new UTMCoord(-1, 'U', 481375, 5466221);
        }

        [TestMethod]
        public void TestMethod10()
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

            actual.generatePattern(datum, numLegs, orientation, firstLegDistance, turnRight);

            for(int i = 0; i <= numLegs; i++)
            {
                if (!expected[i].Equals(actual.getPoint(i)))
                {
                    throw new Exception("Point " + i + " was not correct \nActual - " + actual.getPoint(i).ToString() + "\nExpected - " + expected[i].ToString());
                }
            }
        }
    }
}
