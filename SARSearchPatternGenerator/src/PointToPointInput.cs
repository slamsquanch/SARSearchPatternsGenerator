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
            InputDecimalDegrees datum = new InputDecimalDegrees();
            datum.changed += this.onValueChange;
            addInputGroupItem("Datum:", datum);
            coordinateInputs.Add(datum);

            add = new Button();
            add.Text = "Add";
            add.Click += addPoint;
            addInputGroupItem("", add);
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
            Controls.Remove(add);
            InputDecimalDegrees c = new InputDecimalDegrees();
            c.changed += this.onValueChange;
            SuspendLayout();
            c.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            int curRow = this.RowCount - 1;
            if (curRow == 0)
            {
                this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
                this.RowCount++;
                curRow = this.RowCount - 1;
            }

            this.RowStyles[curRow - 1] = (new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, c.Size.Height));
            this.RowStyles[curRow] = (new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            Label l = new Label();
            l.Anchor = System.Windows.Forms.AnchorStyles.Left;
            l.AutoSize = true;

            l.Text = "";
            l.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            coordinateInputs.Add(c);

            this.Controls.Add(l, 0, curRow - 1);
            this.Controls.Add(c, 1, curRow - 1);

            this.RowCount++;

            addInputGroupItem("", add);
            ResumeLayout(false);
        }
    }
}
