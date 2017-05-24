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
    public class ButtonToggle : Button
    {
        private string _onText;
        private string _offText;
        public string onText
        {
            get {
                return _onText;
            }
            set {
                _onText = value;
            }
        }
        public string offText
        {
            get
            {
                return _offText;
            }
            set
            {
                _offText = value;
            }
        }
        private bool enabled = false;

        public ButtonToggle() : base()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.Text = onText;
            this.Click += toggleState;
        }
        public void setState(bool e)
        {
            enabled = e;
        }
        public bool isEnabled()
        {
            return enabled;
        }
        private void toggleState(object sender, EventArgs e)
        {
            enabled = !enabled;
            Text = enabled ? onText : offText;
        }
    }
}
