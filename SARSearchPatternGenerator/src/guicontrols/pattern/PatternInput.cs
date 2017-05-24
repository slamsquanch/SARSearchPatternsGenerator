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
        protected string unitName = "nm";
        protected DistanceUnit unit = NauticalMiles.create();
        protected CoordSystem coordinateSystem = CoordSystem.DecDeg;
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
            this.unit = unit;
            this.unitName = newName;
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
                        try
                        {
                            newVal = new DecDeg(lat, lng);
                        }
                        catch (OutOfBoundsCoordinateException o)
                        {
                            newVal = new DecDeg(0, 0);
                        }
                        nic = new InputDecimalDegrees();
                        break;
                    case CoordSystem.DegDecMin:
                        try
                        {
                            newVal = new DegDecMin(lat, lng);
                        }
                        catch (OutOfBoundsCoordinateException o)
                        {
                            newVal = new DegDecMin(0, 0);
                        }
                        nic = new InputDegreeDecimalMinutes();
                        break;
                    case CoordSystem.DegMinSec:
                        try
                        {
                            newVal = new DegMinSec(lat, lng);
                        }
                        catch (OutOfBoundsCoordinateException o)
                        {
                            newVal = new DegMinSec(0, 0);
                        }
                        nic = new InputDegreeMinutesSeconds();
                        break;
                    case CoordSystem.UTMCoord:
                        try
                        {
                            newVal = new UTMCoord(lat, lng);
                        }
                        catch (OutOfBoundsCoordinateException o)
                        {
                            newVal = new UTMCoord(0, 0);
                        }
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
                    nic.setLabel(ic.getLabel());
                    nic.setColor(ic.getColor());
                    if (ic.Font.Bold)
                    {
                        nic.makeBold();
                    }

                    this.Controls.Remove(ic);
                    this.coordinateInputs.Remove(ic);
                    this.Controls.Add(nic, col, row);
                    this.coordinateInputs.Add(nic);
                }
            }
            this.coordinateSystem = system;
            ResumeLayout(true);
        }

        public void addInputGroupItem(string label, Control c)
        {
            SuspendLayout();
            int h = c.Height;
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
            Label l = new Label();
            l.Anchor = System.Windows.Forms.AnchorStyles.Left;
            l.AutoSize = true;
            l.Text = label;
            l.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            tlp.AutoSize = true;
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120));
            tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

            tlp.Controls.Add(l, 0, 0);
            tlp.Controls.Add(c, 1, 0);
            tlp.Height = h;

            this.RowStyles[curRow - 1] = (new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, h + 10));
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.Controls.Add(tlp, 0, curRow - 1);
            this.Controls.Add(new Label());

            this.RowCount++;
            ResumeLayout(true);
        }

        public void addCoordinateInputItem(Control c)
        {
            SuspendLayout();
            int h = c.Height;
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

            this.RowStyles[curRow - 1] = (new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, h + 10));
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.Controls.Add(c, 0, curRow - 1);
            this.Controls.Add(new Label());
            this.RowCount++;

            ResumeLayout(true);
        }
        public CoordSystem getCoordSystem()
        {
            return coordinateSystem;
        }
        public string getUnitName()
        {
            return unitName;
        }
        public DistanceUnit getUnit()
        {
            return unit;
        }
    }
}
