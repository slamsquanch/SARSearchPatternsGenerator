using SARSearchPatternGenerator.coords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// An input class that handles all the input for the expanding square
    /// pattern form.
    /// </summary>
    class ExpandingSquareInput : PatternInput
    {
        private ComboBox turnDir;
        private InputUnits orientation;
        private InputDistance flg;
        private NumericUpDown legNum;

        public ExpandingSquareInput(): base()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            InputDecimalDegrees datum = new InputDecimalDegrees();
            datum.changed += this.onValueChange;
            datum.setLabel("Datum ●");
            datum.setColor(System.Drawing.Color.Red);
            addCoordinateInputItem(datum);
            coordinateInputs.Add(datum);

            turnDir = new ComboBox();
            turnDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            turnDir.Items.AddRange(new object[] {
            "Right",
            "Left"});
            turnDir.SelectedIndex = 0;
            turnDir.SelectedIndexChanged += this.onValueChange;
            addInputGroupItem("First Turn:", turnDir);

            orientation = new InputUnits();
            orientation.changeUnitText("°T");
            orientation.changed += onValueChange;
            addInputGroupItem("Orientation:", orientation);

            flg = new InputDistance();
            flg.changed += this.onValueChange;
            distanceInputs.Add(flg);
            addInputGroupItem("First Leg Distance:", flg);

            legNum = new NumericUpDown();
            legNum.Maximum = 999;
            legNum.ValueChanged += onValueChange;
            addInputGroupItem("Number of Legs:", legNum);
        }

        /*
         * Gets the coordinates for a pattern based on the information in the form.
         */
        public override Pattern getPattern()
        {
            InputCoordinate datum = coordinateInputs[0];
            ExpandingSquarePattern ptrn = new ExpandingSquarePattern();
            ptrn.generatePattern(datum.getValue(), (int)legNum.Value, orientation.value, flg.value, turnDir.SelectedIndex == 0, flg.unit);
            ptrn.setDatum(datum.getValue());
            return ptrn;
        }
        /*
         * Gets the coordinates for a pattern based on the information in the form
         * that ignores the curvature of the earth, for display purposes.
         */
        public override Pattern getFlatPattern()
        {
            ExpandingSquarePattern ptrn = new ExpandingSquarePattern();
            ptrn.generatePattern(new FlatCoordinate(0, 0), (int)legNum.Value, orientation.value, flg.value, turnDir.SelectedIndex == 0, flg.unit);
            ptrn.setDatum(new FlatCoordinate(0, 0));
            return ptrn;
        }
    }
}
