using SARSearchPatternGenerator.coords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    class SectorSearchInput : PatternInput
    {
        private ComboBox turnDir;
        private InputUnits orientation;
        private InputDistance flg;
        private NumericUpDown legNum;
        public SectorSearchInput() : base()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            InputDecimalDegrees datum = new InputDecimalDegrees();
            datum = new InputDecimalDegrees();
            datum.setLabel("Datum Location ●");
            datum.setColor(System.Drawing.Color.Red);
            datum.changed += this.onValueChange;
            addCoordinateInputItem(datum);
            coordinateInputs.Add(datum);

            turnDir = new ComboBox();
            turnDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            turnDir.Items.AddRange(new object[] {
            "Right",
            "Left"});
            turnDir.SelectedIndex = 0;
            turnDir.SelectedIndexChanged += this.onValueChange;
            addInputGroupItem("First Turn Direction:", turnDir);

            orientation = new InputUnits();
            orientation.changeUnitText("°T");
            orientation.changed += onValueChange;
            addInputGroupItem("Orientation:", orientation);

            flg = new InputDistance();
            flg.changed += this.onValueChange;
            distanceInputs.Add(flg);
            addInputGroupItem("Leg Distance:", flg);

            legNum = new NumericUpDown();
            legNum.Maximum = 999;
            legNum.ValueChanged += onValueChange;
            addInputGroupItem("Number of Legs:", legNum);
        }
        public override Pattern getPattern()
        {
            InputCoordinate datum = coordinateInputs[0];
            SectorSearchPattern ptrn = new SectorSearchPattern();
            ptrn.generatePattern(datum.getValue(), (int)legNum.Value, orientation.value, flg.value, turnDir.SelectedIndex == 0, flg.unit);
            ptrn.setDatum(datum.getValue());
            return ptrn;
        }
        public override Pattern getFlatPattern()
        {

            SectorSearchPattern ptrn = new SectorSearchPattern();
            ptrn.generatePattern(new FlatCoordinate(0, 0), (int)legNum.Value, orientation.value, flg.value, turnDir.SelectedIndex == 0, flg.unit);
            ptrn.setDatum(new FlatCoordinate(0, 0));
            return ptrn;
        }
    }
}
