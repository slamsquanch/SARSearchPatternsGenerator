using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SARSearchPatternGenerator.coords;

namespace SARSearchPatternGenerator
{
    /*
     * The PatternController class controls the display for viewing
     * a pattern. This controller handles inputs such as changing
     * values for the pattern and changing the coordinate system.
    */
    public class PatternController : DisplayController
    {
        private PatternDisplay display;
        private string unitName;
        private DistanceUnit unit;
        private WindowController winController;
        private String patternFileName = "expanding_";

        public PatternController()
        {
            display = new PatternDisplay();
            display.setController(this);
        }


        private String getTimestamp()
        {
            String dateTime = DateTime.Now.ToString("dd-MMM-yyyy-hh-mm");
            return dateTime;
        }

        public void updateSettings()
        {
            expandingSquareSetup();
        }

        private void expandingSquareSetup()
        {
            ExpandingSquareInput eei = new ExpandingSquareInput();
            display.setInputGroup(eei);
            display.setPatternImage("img\\ExpandingSquare_SearchPattern2.jpg");
            display.setPatternText("Expanding Square");
        }

        private void sectorSearchSetup()
        {
            SectorSearchInput ssi = new SectorSearchInput();
            display.setInputGroup(ssi);
            display.setPatternImage("img\\SectorSearchPattern_3LegAircraft2.jpg");
            display.setPatternText("Sector Search");
        }

        private void parallelSearchSetup()
        {
            ParallelSearchInput pss = new ParallelSearchInput();
            display.setInputGroup(pss);
            display.setPatternImage("img\\ParallelTrack_SearchPattern2.jpg");
            display.setPatternText("Parallel Search");
        }

        private void pointToPointSetup()
        {
            PointToPointInput ptpi = new PointToPointInput();
            display.setInputGroup(ptpi);
            display.setPatternImage("img\\TracklineSearch2.png");
            display.setPatternText("Point-to-Point");
        }

        public void changePattern(int index)
        {
            switch(index)
            {
                case 0:
                    expandingSquareSetup();
                    patternFileName = "expanding_";
                    break;
                case 1:
                    sectorSearchSetup();
                    patternFileName = "sector_";
                    break;
                case 2:
                    parallelSearchSetup();
                    patternFileName = "parallel_";
                    break;
                case 3:
                    pointToPointSetup();
                    patternFileName = "ptop_";
                    break;
            }
        }

        public override void onUnitChange(int index)
        {
            unitName = "nm";
            unit = NauticalMiles.create();
            switch(index)
            {
                case 1:
                    unitName = "mi";
                    unit = Miles.create();
                    break;
                case 2:
                    unitName = "ft";
                    unit = Feet.create();
                    break;
                case 3:
                    unitName = "km";
                    unit = Kilometers.create();
                    break;
                case 4:
                    unitName = "m";
                    unit = Meters.create();
                    break;
            }
            if (display != null)
            {
                display.changeUnitSystem(unitName, unit);
            }
        }

        public string getUnitName()
        {
            return unitName;
        }

        public DistanceUnit getUnit()
        {
            return unit;
        }

        public override void onCoordSystemChange(int index)
        {
            switch(index)
            {
                case 0:
                    display.changeCoordinateSystem(CoordSystem.DecDeg);
                    break;
                case 1:
                    display.changeCoordinateSystem(CoordSystem.DegDecMin);
                    break;
                case 2:
                    display.changeCoordinateSystem(CoordSystem.DegMinSec);
                    break;
                case 3:
                    display.changeCoordinateSystem(CoordSystem.UTMCoord);
                    break;
            }
        }

        public override UserControl getDisplay()
        {
            return display;
        }

        public void exportGPX(Pattern p)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "GPX files(*.gpx)|*.gpx";
            sf.FileName = patternFileName + getTimestamp();


            if (sf.ShowDialog() == DialogResult.OK)
            {
                GPX gpx = new GPX(p);
                gpx.writeFile(sf.FileName);
            }
        }
        public void exportKML(Pattern p)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "KML files(*.kml)|*.kml";
            sf.FileName = patternFileName + getTimestamp();

            if (sf.ShowDialog() == DialogResult.OK)
            {
                KML kml = new KML(p);
                kml.writeFile(sf.FileName);
            }
        }
    }
}
