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
        private ComboBox turnDir;
        private ComboBox datumType;
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
            InputDecimalDegrees datum = new InputDecimalDegrees();
            datum = new InputDecimalDegrees();
            datum.setLabel("Datum Location ●");
            datum.makeBold();
            datum.setColor(System.Drawing.Color.Red);
            datum.changed += this.onValueChange;
            addCoordinateInputItem(datum);
            coordinateInputs.Add(datum);

            datumType = new ComboBox();
            datumType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            datumType.Items.AddRange(new object[]
            {
                "Parallel Track",
                "Creeping Line",
                "Baseline"
            });
            datumType.SelectedIndex = 0;
            datumType.SelectedIndexChanged += this.onValueChange;
            addInputGroupItem("Search Type: ", datumType);

            turnDir = new ComboBox();
            turnDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            turnDir.Items.AddRange(new object[] {
            "Right",
            "Left"});
            turnDir.SelectedIndex = 0;
            turnDir.SelectedIndexChanged += this.onValueChange;
            addInputGroupItem("1st Turn Direction:", turnDir);

            orientation = new InputUnits();
            orientation.changeUnitText("°T");
            orientation.changed += onValueChange;
            addInputGroupItem("1st Leg Orientation:", orientation);

            flg = new InputDistance();
            flg.changed += this.onValueChange;
            distanceInputs.Add(flg);
            addInputGroupItem("Leg Distance:", flg);

            trk = new InputDistance();
            trk.changed += this.onValueChange;
            distanceInputs.Add(trk);
            addInputGroupItem("Track Spacing:", trk);

            legNum = new NumericUpDown();
            legNum.Maximum = 999;
            legNum.ValueChanged += onValueChange;
            addInputGroupItem("Number of Legs:", legNum);
        }
        public override Pattern getPattern()
        {
            InputCoordinate datum = coordinateInputs[0];
            ParallelTrackPattern ptrn = new ParallelTrackPattern();
            switch (datumType.Text) {
                case "Parallel Track":
                     ptrn.generateFromParallelTrackDatum(datum.getValue(), (int)legNum.Value, orientation.value, flg.value, trk.value, turnDir.SelectedIndex == 0, flg.unit);
                    break;
                case "Creeping Line":
                    ptrn.generateFromCreepingLine(datum.getValue(), (int)legNum.Value, orientation.value, flg.value, trk.value, turnDir.SelectedIndex == 0, flg.unit);
                    break;
                case "Baseline":
                    ptrn.generateFromBaseline(datum.getValue(), (int)legNum.Value, orientation.value, flg.value, trk.value, turnDir.SelectedIndex == 0, flg.unit);
                    break;
                default:
                    ptrn.generatePattern(datum.getValue(), (int)legNum.Value, orientation.value, flg.value, trk.value, turnDir.SelectedIndex == 0, flg.unit);
                    break;
            }
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
