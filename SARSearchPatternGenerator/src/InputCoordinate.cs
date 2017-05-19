using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// A form that takes in coordinate data.
    /// </summary>
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class InputCoordinate : UserControl
    {
        protected Coordinate value;
        public EventHandler changed;

        protected List<InputDistance> distanceInputs;

        public InputCoordinate()
        {
            distanceInputs = new List<InputDistance>();
            InitializeComponent();
        }
        
        private void InitializeComponent() {}

        public Coordinate getValue()
        {
            return value;
        }

        public void setValue(Coordinate c)
        {
            if (value == null)
            {
                return;
            }
            double lat = c.getLat();
            double lng = c.getLng();
            value.setLat(lat);
            value.setLng(lng);
            value.fromBase();
            repopulateFields();
        }

        protected void onChange(object sender, EventArgs args)
        {
            updateValue();
            if (changed != null)
            {
                changed.Invoke(sender, args);
            }
        }

        public void changeUnits(string newName, DistanceUnit unit)
        {
            foreach (InputDistance i in distanceInputs)
            {
                double tmp = i.unit.convertFrom(i.value);
                i.changeUnitText(newName);
                i.unit = unit;
                i.setValue(i.unit.convertTo(tmp));
            }
        }

        virtual protected void repopulateFields() {}

        virtual protected void updateValue() {}
    }
}
