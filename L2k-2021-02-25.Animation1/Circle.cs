using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace L2k_2021_02_25.Animation1
{
    class Circle
    {
        private int size;
        private PointF pos;

        public float XPos
        {
            get => pos.X;
            set
            {
                if (value < 0 || value > containerSize.Width - size - 1)
                {
                    if (value < 0) value = 0;
                    if (value > containerSize.Width - size - 1)
                        value = containerSize.Width - size - 1;
                    dx = -dx;
                }
                pos.X = value;
            }
        }

        public float YPos
        {
            get => pos.Y;
            set
            {
                if (value < 0 || value > containerSize.Height - size - 1)
                {
                    if (value < 0) value = 0;
                    if (value > containerSize.Height - size - 1)
                        value = containerSize.Height - size - 1;
                    dy = -dy;
                }
                pos.Y = value;
            }
        }

        public int Size
        {
            get => size;
            set
            {
                if (value < 5) size = 5;
                else if (value > 100) size = 100;
                else size = value;
            }
        }

        private int dx;
        private int dy;

        public Color CircleColor
        {
            get; set;
        }

        private static Random r = new Random();

        private static Size containerSize = new Size(1, 1);

        public static Size ContainerSize
        {
            get => containerSize;
            set
            {
                containerSize = value;
            }
        }

        public Circle()
        {
            size = r.Next(5, 100);
            var red = r.Next(255);
            var grn = r.Next(255);
            var blu = r.Next(255);
            pos = new PointF(
                r.Next(0, containerSize.Width - size - 1), 
                r.Next(0, containerSize.Height - size - 1));
            do
            {
                dx = r.Next(-5, 5);
            } while (dx == 0);
            do
            {
                dy = r.Next(-5, 5);
            } while (dy == 0);
            CircleColor = Color.FromArgb(red, grn, blu);
        }

        public void Paint(Graphics g)
        {
            Color bc = Color.FromArgb(120, CircleColor);
            Brush b = new SolidBrush(bc);
            Pen p = new Pen(CircleColor);
            g.FillEllipse(b, XPos, YPos, Size, Size);
            g.DrawEllipse(p, XPos, YPos, Size, Size);
        }

        public void Move()
        {
            XPos += dx;
            YPos += dy;
        }

    }
}
