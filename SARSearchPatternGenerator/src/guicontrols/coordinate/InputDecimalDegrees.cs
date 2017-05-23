using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// An input class that handles the input for a Decimal Degree coordinate.
    /// </summary>
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class InputDecimalDegrees : InputCoordinate
    {
        private FloatInput floatInput1;
        private FloatInput floatInput2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ButtonToggle buttonToggle2;
        private ButtonToggle buttonToggle1;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;

        public InputDecimalDegrees()
        {
            InitializeComponent();
            modifyComponent();
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonToggle2 = new SARSearchPatternGenerator.ButtonToggle();
            this.label4 = new System.Windows.Forms.Label();
            this.floatInput1 = new SARSearchPatternGenerator.FloatInput();
            this.floatInput2 = new SARSearchPatternGenerator.FloatInput();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonToggle1 = new SARSearchPatternGenerator.ButtonToggle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.81818F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.18182F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.Controls.Add(this.buttonToggle2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.floatInput1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.floatInput2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonToggle1, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(349, 60);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // buttonToggle2
            // 
            this.buttonToggle2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonToggle2.Location = new System.Drawing.Point(325, 33);
            this.buttonToggle2.Name = "buttonToggle2";
            this.buttonToggle2.offText = "E";
            this.buttonToggle2.onText = "W";
            this.buttonToggle2.Size = new System.Drawing.Size(20, 24);
            this.buttonToggle2.TabIndex = 6;
            this.buttonToggle2.Text = "E";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(308, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "°";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // floatInput1
            // 
            this.floatInput1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.floatInput1.Location = new System.Drawing.Point(100, 3);
            this.floatInput1.Name = "floatInput1";
            this.floatInput1.precision = 8;
            this.floatInput1.Size = new System.Drawing.Size(202, 22);
            this.floatInput1.TabIndex = 0;
            this.floatInput1.Text = "0";
            // 
            // floatInput2
            // 
            this.floatInput2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.floatInput2.Location = new System.Drawing.Point(100, 33);
            this.floatInput2.Name = "floatInput2";
            this.floatInput2.precision = 8;
            this.floatInput2.Size = new System.Drawing.Size(202, 22);
            this.floatInput2.TabIndex = 0;
            this.floatInput2.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Latitude:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Longitude:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "°";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonToggle1
            // 
            this.buttonToggle1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonToggle1.Location = new System.Drawing.Point(325, 3);
            this.buttonToggle1.Name = "buttonToggle1";
            this.buttonToggle1.offText = "N";
            this.buttonToggle1.onText = "S";
            this.buttonToggle1.Size = new System.Drawing.Size(20, 24);
            this.buttonToggle1.TabIndex = 5;
            this.buttonToggle1.Text = "N";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 87);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // InputDecimalDegrees
            // 
            this.Controls.Add(this.groupBox1);
            this.Name = "InputDecimalDegrees";
            this.Size = new System.Drawing.Size(368, 90);
            this.Load += new System.EventHandler(this.InputDecimalDegrees_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void modifyComponent()
        {
            this.updateValue();
            this.floatInput1.setValue(0);
            this.floatInput2.setValue(0);
            this.floatInput1.TextChanged += onChange;
            this.floatInput2.TextChanged += onChange;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {}

        /*
         * Changes the fields to match the values.
         */
        protected override void repopulateFields()
        {
            double lat = this.value.getLat();
            double lng = this.value.getLng();
            buttonToggle1.setState(lat < 0);
            buttonToggle2.setState(lng < 0);

            this.floatInput1.TextChanged -= onChange;
            this.floatInput2.TextChanged -= onChange;
            floatInput1.setValue(Math.Abs(((DecDeg)this.value).getLat()));
            floatInput2.setValue(Math.Abs(((DecDeg)this.value).getLng()));
            this.floatInput1.TextChanged += onChange;
            this.floatInput2.TextChanged += onChange;
        }

        /*
         * Changes the coordinate value to match the new set of inputs.
         */
        protected override void updateValue()
        {
            try
            {
                this.value = new DecDeg(
                        floatInput1.getValue() * (buttonToggle1.isEnabled() ? -1 : 1),
                        floatInput2.getValue() * (buttonToggle2.isEnabled() ? -1 : 1)
                );
            }
            catch (OutOfBoundsCoordinateException)
            {
                // keep value the same as it was before
            }
        }

        private void InputDecimalDegrees_Load(object sender, EventArgs e)
        {

        }

        public override void setLabel(string txt)
        {
            groupBox1.Text = txt;
        }

        public override void setColor(Color c)
        {
            groupBox1.ForeColor = c;
        }

        public override string getLabel()
        {
            return groupBox1.Text;
        }

        public override Color getColor()
        {
            return groupBox1.ForeColor;
        }
    }
}
