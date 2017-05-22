using SARSearchPatternGenerator.coords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// An input class that handles all the input fields for a parallel search
    /// pattern.
    /// </summary>
    class ParallelSearchInput : PatternInput
    {
        private InputDecimalDegrees datum;
        private ComboBox turnDir;
        private InputUnits orientation;
        private InputDistance flg;
        private InputDistance trk;
        private NumericUpDown legNum;
        public ParallelSearchInput() : base()
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
            addInputGroupItem("Leg Distance:", flg);

            trk = new InputDistance();
            trk.changed += this.onValueChange;
            distanceInputs.Add(trk);
            addInputGroupItem("Track Space:", trk);

            legNum = new NumericUpDown();
            legNum.Maximum = 999;
            legNum.ValueChanged += onValueChange;
            addInputGroupItem("Number of Legs:", legNum);
        }
        public override Pattern getPattern()
        {
            ParallelTrackPattern ptrn = new ParallelTrackPattern();
            ptrn.generatePattern(datum.getValue(), (int)legNum.Value, orientation.value, flg.value, trk.value, turnDir.SelectedIndex == 0, flg.unit);
            ptrn.setDatum(datum.getValue());
            return ptrn;
        }
        public override Pattern getFlatPattern()
        {
            ParallelTrackPattern ptrn = new ParallelTrackPattern();
            ptrn.generatePattern(new FlatCoordinate(0, 0), (int)legNum.Value, orientation.value, flg.value, trk.value, turnDir.SelectedIndex == 0, flg.unit);
            ptrn.setDatum(new FlatCoordinate(0, 0));
            return ptrn;
        }
    }
}
