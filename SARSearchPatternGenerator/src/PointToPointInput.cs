using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    class PointToPointInput : PatternInput
    {
        public PointToPointInput() : base()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            InputDecimalDegrees datum = new InputDecimalDegrees();
            datum.changed += this.onValueChange;
            addInputGroupItem("Datum:", datum);
            coordinateInputs.Add(datum);

            Button add = new Button();
            add.Text = "Add";
            addInputGroupItem("", add);
        }
        public override List<Coordinate> getPattern()
        {
            Pattern ptrn = new Pattern();
            foreach(InputCoordinate c in coordinateInputs) {
                ptrn.addPoint(c.getValue());
            }
            return ptrn.getPattern();
        }
        public override List<Coordinate> getFlatPattern()
        {
            /*
             * meant to be temporary;
             * real implementation would involve getting distance and angle
             * between each connected point and making a new list of points
             */
            return getPattern();
        }
    }
}
