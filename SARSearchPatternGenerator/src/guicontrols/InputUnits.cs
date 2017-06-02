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
    /// An input class that deals with the distance unit being used throughout
    /// the program.
    /// </summary>
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    [DefaultEvent("changed")]
    public class InputUnits : UserControl
    {
        private FloatInput textBox1;
        private ComboBox comboBox1;
        private Label label4;
        private TableLayoutPanel tableLayoutPanel1;

        public double value;

        public EventHandler changed;

        public InputUnits() : base()
        {
            InitializeComponent();
            modifyComponent();
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new SARSearchPatternGenerator.FloatInput();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(174, 30);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.precision = 3;
            this.textBox1.Size = new System.Drawing.Size(133, 22);
            this.textBox1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "nm";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 25);
            this.comboBox1.TabIndex = 0;
            // 
            // InputUnits
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "InputUnits";
            this.Size = new System.Drawing.Size(177, 30);
            this.Load += new System.EventHandler(this.PatternDisplay_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void modifyComponent()
        {
            textBox1.TextChanged += textBox1_TextChanged;
        }

        private void PatternDisplay_Load(object sender, EventArgs e)
        {

        }


        //Orientation textBox.
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            onChange(sender, e);
        }


        /*
         *  Takes in a string passed from the unit dropdown function and changes the units label based on that.
         */
        public void changeUnitText(String s)
        {
            this.label4.Text = s;
        }

        //Units label.
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            // Save the selected employee's name, because we will remove
            // the employee's name from the list.
            //string selectedUnitSystem = (string)comboBox1.SelectedItem;

            string selected = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            //MessageBox.Show(selected);
            changeUnitText(selected);

            //int count = 0;
            //int resultIndex = -1;

            // Call the FindStringExact method to find the first 
            // occurrence in the list.
            //resultIndex = comboBox1.FindStringExact(selectedUnitSystem);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void setValue(double val)
        {
            value = val;
            textBox1.setValue(val);
        }

        protected void onChange(object sender, EventArgs args)
        {
            value = textBox1.getValue();
            if (changed != null)
            {
                changed.Invoke(sender, args);
            }
        }

    }
}
