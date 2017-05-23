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
            InputDecimalDegrees c = new InputDecimalDegrees();
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
