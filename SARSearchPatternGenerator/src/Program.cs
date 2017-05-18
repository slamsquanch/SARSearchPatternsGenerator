using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    static class Program
    {
        //GPX test data
       static double[] lat = new double[] 
        {
            49.3658750, 49.3662420, 49.3665020, 49.3667150, 49.3669600, 49.3670980,
            49.3672960, 49.3674640, 49.3677400, 49.3677850, 49.3679230, 49.3680760,
            49.3682590, 49.3684420, 49.3684570, 49.3688850
        };

        //GPX test data
        static double[] lon = new double[]
        {
            -123.0882700, -123.0881300, -123.0879200, -123.0879200, -123.0878270,
            -123.0878270, -123.0876170, -123.0877110, -123.0875480, -123.0876650,
            -123.0878520, -123.0878290, -123.0879230, -123.0878300, -123.0881570,
            -123.0880650

        };


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new WindowController();
            //Test GPX export.
            Pattern p = new Pattern();
            for (int i = 0; i < 16; i++) {
                p.addPoint(new DecDeg(lat[i], lon[i]));
            }
            GPX gpx = new GPX(p);
            gpx.writeFile("BCMC");
        }
    }
}