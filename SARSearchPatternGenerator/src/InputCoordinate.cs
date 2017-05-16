using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace SARSearchPatternGenerator
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class InputCoordinate : UserControl
    {
        public InputCoordinate()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {

        }
        // value for coordinate

        public EventHandler changed;

        protected void onChange(object sender, EventArgs args)
        {
            if (changed != null)
            {
                changed.Invoke(sender, args);
            }
        }

    }
}
