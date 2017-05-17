using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    /*
     *  This class has the sole purpose of being a Controller of the main Window. 
     */
    public class WindowController
    {
        Window mainWindow;
        /*
         * Constructor for this WindowController class.  It starts running the main Window. 
         */
        public WindowController() {
            patternTest();

            //This is what STARTS the main window.
            mainWindow = new Window();
            mainWindow.setController(this);
            writeSystemText("Program loaded");

            Application.Run(mainWindow);
        }

        private void patternTest()
        {
            List<Coordinate> expected = new List<Coordinate>();

            expected.Add(new DegMinSec(49, 12, 36, 122, 54, 15));
            expected.Add(new DegMinSec(49, 12, 36, 122, 54, 13));
            expected.Add(new DegMinSec(49, 12, 34, 122, 54, 13));
            expected.Add(new DegMinSec(49, 12, 34, 122, 54, 18));

            Coordinate datum = expected[0];
            int numLegs = 3;
            double orientation = 180;
            double firstLegDistance = 0.05;
            bool turnRight = true;

            Coordinate test = datum.travel(orientation, firstLegDistance);

            ExpandingSquarePattern actual = new ExpandingSquarePattern();

            actual.generatePattern(datum, numLegs, orientation, firstLegDistance, turnRight);


        }

        private void writeSystemText(string txt)
        {
            mainWindow.setSystemLabel(txt);
        }

        public void onFileNew()
        {
            PatternController pc = new PatternController();
            mainWindow.setDisplay(pc);
            pc.updateSettings();
            writeSystemText("New pattern created");
        }
    }
}
