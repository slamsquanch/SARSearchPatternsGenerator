using SARSearchPatternGenerator.coords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    class ExpandingSquareInput : PatternInput
    {
        private InputDecimalDegrees datum;
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
            datum = new InputDecimalDegrees();
            datum.changed += this.onValueChange;
            addInputGroupItem("Datum:", datum);
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
        public override List<Coordinate> getPattern()
        {
            ExpandingSquarePattern ptrn = new ExpandingSquarePattern();
            return ptrn.generatePattern(datum.getValue(), (int)legNum.Value, orientation.value, flg.value, turnDir.SelectedIndex == 0, flg.unit);
        }
        public override List<Coordinate> getFlatPattern()
        {

            ExpandingSquarePattern ptrn = new ExpandingSquarePattern();
            return ptrn.generatePattern(new FlatCoordinate(0, 0), (int)legNum.Value, orientation.value, flg.value, turnDir.SelectedIndex == 0, flg.unit);
        }
    }
}
