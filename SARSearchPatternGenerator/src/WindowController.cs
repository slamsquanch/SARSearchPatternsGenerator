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
            testPattern();

            //This is what STARTS the main window.
            mainWindow = new Window();
            mainWindow.setController(this);
            writeSystemText("Program loaded");

            Application.Run(mainWindow);
        }

        private void testPattern()
        {
            ParallelTrackPattern testPattern = new ParallelTrackPattern();
            testPattern.generateFromParallelTrackDatum(new DecDeg(0, 0), 6, 0, 10, 2, true, Kilometers.create());
            Console.WriteLine(testPattern);
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
            mainWindow.unitChange();
            mainWindow.coordSystemChange();
            writeSystemText("New pattern created");
        }
    }
}
