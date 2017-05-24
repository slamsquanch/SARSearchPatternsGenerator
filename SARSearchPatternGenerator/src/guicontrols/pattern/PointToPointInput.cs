using SARSearchPatternGenerator.coords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// An input class that handles all of the coordinate inputs in the
    /// point-to-point pattern.
    /// </summary>
    class PointToPointInput : PatternInput
    {
        private Button add;
        public PointToPointInput() : base()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            /*InputDecimalDegrees datum = new InputDecimalDegrees();
            datum.changed += this.onValueChange;
            addCoordinateInputItem(datum);
            coordinateInputs.Add(datum);*/

            add = new Button();
            add.Text = "Add";
            add.Click += addPoint;

            this.Controls.Add(add, 0, 0);
            this.Controls.Add(new Label());
            this.RowCount++;
        }
        public override Pattern getPattern()
        {
            Pattern ptrn = new Pattern();
            foreach(InputCoordinate c in coordinateInputs) {
                ptrn.addPoint(c.getValue());
            }
            return ptrn;
        }
        public override Pattern getFlatPattern()
        {
            /*
             * meant to be temporary;
             * real implementation would involve getting distance and angle
             * between each connected point and making a new list of points
             */
            return getPattern();
        }
        private void addPoint(object sender, EventArgs e)
        {
            InputCoordinate c = new InputDecimalDegrees();
            Coordinate newVal = new DecDeg(0, 0);
            double lat = 0;
            double lng = 0;
            if (this.coordinateInputs.Count > 0)
            {
                lat = this.coordinateInputs.Last().getValue().getLat();
                lng = this.coordinateInputs.Last().getValue().getLng();
            }
            switch (this.coordinateSystem)
            {
                case CoordSystem.DecDeg:
                    newVal = new DecDeg(lat, lng);
                    c = new InputDecimalDegrees();
                    break;
                case CoordSystem.DegDecMin:
                    newVal = new DegDecMin(lat, lng);
                    c = new InputDegreeDecimalMinutes();
                    break;
                case CoordSystem.DegMinSec:
                    newVal = new DegMinSec(lat, lng);
                    c = new InputDegreeMinutesSeconds();
                    break;
                case CoordSystem.UTMCoord:
                    newVal = new UTMCoord(lat, lng);
                    c = new InputUTMZoneCoord();
                    break;
            }
            c.setValue(newVal);
            c.changed += this.onValueChange;
            SuspendLayout();
            int h = c.Height;
            add.Anchor = System.Windows.Forms.AnchorStyles.None;
            c.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            int curRow = this.RowCount;
            if (curRow <= 1)
            {
                this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                this.RowCount++;
                curRow = this.RowCount;
            }
            c.setLabel("Point " + (curRow - 1));

            Controls.Remove(add);

            this.RowStyles[curRow - 2] = (new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, h + 10));
            this.RowStyles[curRow - 1] = (new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, add.Height + 10));
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.Controls.Add(c, 0, curRow - 2);
            this.coordinateInputs.Add(c);

            this.Controls.Add(add, 0, curRow - 1);
            this.Controls.Add(new Label());
            this.RowCount++;
            
            ResumeLayout(true);
        }
    }
}
