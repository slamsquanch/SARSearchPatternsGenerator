using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        public PatternController()
        {
            display = new PatternDisplay();
        }

        public void updateSettings()
        {
            display.resetInputGroup();
            display.addInputGroupItem("Datum:", new InputDecimalDegrees());
            display.addInputGroupItem("First Leg Distance:", new InputUnits());
            InputUnits orientation = new InputUnits();
            orientation.changeUnitText("°T");
            display.addInputGroupItem("Orientation:", orientation);
        }

        public override UserControl getDisplay()
        {
            return display;
        }
    }
}
