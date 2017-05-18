using SARSearchPatternGenerator.coords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    public abstract class PatternInput : InputGroup
    {
        protected List<InputDistance> distanceInputs;
        protected List<InputCoordinate> coordinateInputs;
        public abstract List<Coordinate> getPattern();
        public abstract List<Coordinate> getFlatPattern();
        public PatternInput(): base()
        {
            distanceInputs = new List<InputDistance>();
            coordinateInputs = new List<InputCoordinate>();
        }
        public void changeUnits(string newName, DistanceUnit unit)
        {
            foreach(InputDistance i in distanceInputs)
            {
                double tmp = i.unit.convertFrom(i.value);
                i.changeUnitText(newName);
                i.unit = unit;
                EventHandler tmpEvents = i.changed;
                i.changed = null;
                i.setValue(i.unit.convertTo(tmp));
                i.changed = tmpEvents;
            }

            foreach(InputCoordinate i in coordinateInputs)
            {
                i.changeUnits(newName, unit);
            }
        }
        public void changeCoordinateSystem(CoordSystem system)
        {
            SuspendLayout();
            switch (system)
            {
                case CoordSystem.DecDeg:
                    foreach(InputCoordinate ic in coordinateInputs) {
                        int row = this.GetRow(ic);
                        int col = this.GetColumn(ic);
                        this.Controls.Remove(ic);

                        double lat = ic.getValue().getLat();
                        double lng = ic.getValue().getLng();
                        DecDeg newVal = new DecDeg(lat, lng);
                        InputCoordinate nic = new InputDecimalDegrees();
                        nic.Size = ic.Size;
                        nic.Location = ic.Location;
                        EventHandler tmpEvents = nic.changed;
                        nic.changed = null;
                        nic.setValue(newVal);
                        nic.changed = tmpEvents;
                        nic.changed += this.onValueChange;

                        this.Controls.Add(nic, col, row);
                    }
                    break;
                case CoordSystem.DegDecMin:
                    break;
                case CoordSystem.DegMinSec:
                    break;
                case CoordSystem.UTMCoord:
                    break;
            }
            ResumeLayout(false);
        }

        public void addInputGroupItem(string label, Control c)
        {
            SuspendLayout();
            int curRow = this.RowCount;
            if (curRow == 0)
            {
                this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
                this.RowCount++;
                curRow = this.RowCount;
            }

            this.RowStyles[curRow - 1] = (new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, c.Size.Height));
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            Label l = new Label();
            l.Anchor = System.Windows.Forms.AnchorStyles.Left;
            l.AutoSize = true;
            l.Text = label;
            l.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            c.Anchor = System.Windows.Forms.AnchorStyles.Left;

            this.Controls.Add(l, 0, curRow - 1);
            this.Controls.Add(c, 1, curRow - 1);

            this.RowCount++;
            ResumeLayout(false);
        }
    }
}
