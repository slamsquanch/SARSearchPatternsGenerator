using SARSearchPatternGenerator.coords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// An input class that handles all of the input classes and other fields
    /// required for a particular pattern.
    /// </summary>
    public abstract class PatternInput : InputGroup
    {
        protected List<InputDistance> distanceInputs;
        protected List<InputCoordinate> coordinateInputs;
        public abstract Pattern getPattern();
        public abstract Pattern getFlatPattern();
        public PatternInput(): base()
        {
            distanceInputs = new List<InputDistance>();
            coordinateInputs = new List<InputCoordinate>();
        }

        /*
         * Converts the units used to the distance unit selected by the user.
         */
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

            foreach(InputCoordinate i in coordinateInputs.ToList())
            {
                i.changeUnits(newName, unit);
            }
        }

        /*
         * Changes the form to fit the layout that the user has selected.
         */
        public void changeCoordinateSystem(CoordSystem system)
        {
            SuspendLayout();
            foreach (InputCoordinate ic in coordinateInputs.ToList())
            {
                int row = this.GetRow(ic);
                int col = this.GetColumn(ic);

                double lat = ic.getValue().getLat();
                double lng = ic.getValue().getLng();

                Coordinate newVal = null;
                InputCoordinate nic = null;
                switch (system)
                {
                    case CoordSystem.DecDeg:
                        newVal = new DecDeg(lat, lng);
                        nic = new InputDecimalDegrees();
                        break;
                    case CoordSystem.DegDecMin:
                        newVal = new DegDecMin(lat, lng);
                        nic = new InputDegreeDecimalMinutes();
                        break;
                    case CoordSystem.DegMinSec:
                        newVal = new DegMinSec(lat, lng);
                        nic = new InputDegreeMinutesSeconds();
                        break;
                    case CoordSystem.UTMCoord:
                        newVal = new UTMCoord(lat, lng);
                        nic = new InputUTMZoneCoord();
                        break;
                }
                if (newVal != null && nic != null)
                {
                    this.RowStyles[row] = (new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, nic.Height));
                    nic.Width = ic.Width;
                    nic.Location = ic.Location;
                    nic.Anchor = ic.Anchor;
                    EventHandler tmpEvents = nic.changed;
                    nic.changed = null;
                    nic.setValue(newVal);
                    nic.changed = tmpEvents;
                    nic.changed += this.onValueChange;

                    this.Controls.Remove(ic);
                    this.coordinateInputs.Remove(ic);
                    this.Controls.Add(nic, col, row);
                    this.coordinateInputs.Add(nic);
                }
            }
            ResumeLayout(true);
        }

        public void addInputGroupItem(string label, Control c)
        {
            SuspendLayout();
            c.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
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

            this.Controls.Add(l, 0, curRow - 1);
            this.Controls.Add(c, 1, curRow - 1);

            this.RowCount++;
            ResumeLayout(false);
        }
    }
}
