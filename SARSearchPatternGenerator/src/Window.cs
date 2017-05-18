using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    /*
     *  This is only a part of our main Window class.   The rest of the class' code can be found in "Window.Designer.cs".
     */
    public partial class Window : Form
    {
        DisplayController currentDisplay;
        WindowController controller;
        /*
         * Window class' constructor.  
         */
        public Window()
        {
            InitializeComponent();
        }

        private void Window_Load(object sender, EventArgs e)
        {
        }

        public void setDisplay(DisplayController dc)
        {
            System.Drawing.Point loc = this.display.Location;
            System.Drawing.Size size = this.display.Size;
            currentDisplay = dc;
            UserControl uc = currentDisplay.getDisplay();
            this.SuspendLayout();
            if (this.display != null)
            {
                this.Controls.Remove(this.display);
            }
            this.display = uc;
            this.display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.display.Size = size;
            this.display.Location = loc;
            this.display.Name = "display";
            this.display.TabIndex = 4;
            this.Controls.Add(this.display);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void setController(WindowController wc)
        {
            controller = wc;
        }

        public void setSystemLabel(string txt)
        {
            systemLabel.Text = txt;
            Console.WriteLine(systemLabel.Text);
        }

        /*
         *  The Display panel contained inside of our main window.
         */
        private void displayPanel(object sender, PaintEventArgs e)
        {

        }


        /*
         *  The "File" dropdown menu from the toolbar. 
        */
        private void fileDropDown(object sender, EventArgs e)
        {

        }


        /*
         *  The "Open" button inside the "File" menu's dropdown. 
        */
        private void openFileButton(object sender, EventArgs e)
        {

        }


        /*
         *  The "Save" button inside the "File" menu's dropdown. 
        */
        private void saveFileButton(object sender, EventArgs e)
        {

        }


        /*
         *  The "Cut" button inside the Edit menu's dropdown. 
        */
        private void cutEditButton(object sender, EventArgs e)
        {

        }


        /*
         *  The "Copy" button inside the Edit menu's dropdown. 
        */
        private void copyEditButton(object sender, EventArgs e)
        {

        }


        /*
         *  The "Paste" button inside the Edit menu's dropdown. 
        */
        private void pasteEditButton(object sender, EventArgs e)
        {

        }

        /*
         *  The "Delete" button inside the Edit menu's dropdown. 
        */
        private void deleteEditButton(object sender, EventArgs e)
        {

        }


        /*
         *  The "Edit" drop down button from the toolbar menu.
        */
        private void editDropDown(object sender, EventArgs e)
        {

        }

 
        /*
         *  The "About" drop down from the toolbar menu.
         */
        private void aboutButton(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.onFileNew();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void changeUnitsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropdown3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
