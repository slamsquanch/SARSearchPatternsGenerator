using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class FloatInput : TextBox
    {
        private double value = 0;
        public FloatInput() : base() {
            this.TextChanged += new System.EventHandler(this.restrict);
        }

        public void setValue(double v)
        {
            value = v;
            Text = v.ToString();
        }

        public double getValue()
        {
            return value;
        }

        private void restrict(object sender, EventArgs e)
        {
            double parsedValue;

            if (!double.TryParse(Text, out parsedValue))
            {
                if (Text.Equals(""))
                {
                    value = 0;
                    Text = "";
                }
                else
                {
                    Text = value.ToString();
                }
            }
            else
            {
                value = parsedValue;
            }
        }
    }
}
