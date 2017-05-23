using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// Handles the drawing of the pattern that the user is working with.
    /// </summary>
    public class PatternVisualization : Control
    {
        List<Coordinate> pattern;

        public PatternVisualization(): base()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            pattern = new List<Coordinate>();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen p = new Pen(Color.Black);

            if (pattern.Count > 0) {

                double minx = pattern[0].getLng();
                double miny = pattern[0].getLat();
                double maxx = minx;
                double maxy = miny;

                for (int i = 0; i < pattern.Count; i++)
                {
                    double x = pattern[i].getLng();
                    double y = pattern[i].getLat();

                    if (x < minx)
                    {
                        minx = x;
                    }
                    else if (x > maxx)
                    {
                        maxx = x;
                    }
                    if (y < miny)
                    {
                        miny = y;
                    }
                    else if (y > maxy)
                    {
                        maxy = y;
                    }
                }

                double width = maxx - minx;
                double height = maxy - miny;

                for (int i = 1; i < pattern.Count; i++)
                {
                    double x1 = pattern[i - 1].getLng();
                    double y1 = pattern[i - 1].getLat();
                    double x2 = pattern[i].getLng();
                    double y2 = pattern[i].getLat();
                    e.Graphics.DrawLine(p, new Point(), new Point());
                }
            }
        }
    }
}
