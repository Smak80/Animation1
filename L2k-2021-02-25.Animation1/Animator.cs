using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace L2k_2021_02_25.Animation1
{
    class Animator
    {
        private bool stop = false;

        private Graphics mainG;

        private BufferedGraphics bg;

        public Graphics MainGraphics
        {
            get => mainG;
            set
            {
                mainG = value;
            }
        }

        public Animator(Graphics g)
        {
            MainGraphics = g;
            bg = BufferedGraphicsManager.Current.Allocate(
                MainGraphics, Rectangle.Round(g.VisibleClipBounds));
        }

        private List<Circle> circ = new List<Circle>();
        private Thread t;

        public void Start()
        {
            stop = false;
            AddNewCircle();
            if (t == null || !t.IsAlive)
            {
                ThreadStart ts = new ThreadStart(Animate);
                t = new Thread(ts);
                t.Start();
            }
        }

        private void AddNewCircle()
        {
            Circle.ContainerSize = MainGraphics.VisibleClipBounds.Size.ToSize();
            circ.Add(new Circle());
        }

        private void Animate()
        {
            for (int k = 0; k < 1000 && !stop; k++)
            {
                bg.Graphics.Clear(Color.White);
                foreach (var circle in circ)
                {
                    circle.Paint(bg.Graphics);
                    circle.Move();
                }

                try
                {
                    bg.Render(MainGraphics);
                } catch (Exception e) { }

                Thread.Sleep(15);
            }
        }

        public void Stop()
        {
            stop = true;
        }
    }
}
