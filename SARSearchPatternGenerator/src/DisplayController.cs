using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    /*
     * The DisplayController class controls the Display view, constructing a 
     * base DisplayController will create a default blank Display. This class
     * is meant to be extended by different controllers for different displays.
     */
    public abstract class DisplayController
    {
        /*
         * @return - Returns the Display this object
         *           controls. 
         */
        public abstract UserControl getDisplay();
        
    }
}
