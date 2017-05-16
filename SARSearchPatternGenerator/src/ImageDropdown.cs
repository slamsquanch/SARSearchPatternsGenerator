using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class ImageDropdown : ComboBox
    {
        public ImageDropdown() : base()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            //DrawMode = DrawMode.OwnerDrawFixed;
        }
        public List<Image> images;
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            string str = Items[e.Index].ToString();
            Image img = images[e.Index];
            e.DrawFocusRectangle();
            if (img != null) {
                e.Graphics.DrawImage(img, e.Bounds.Left, e.Bounds.Top);
            }
            e.Graphics.DrawString(Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + img.Width, e.Bounds.Top);
            base.OnDrawItem(e);
        }
    }
}
