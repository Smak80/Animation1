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
        public bool IsAlive
        {
            get => (t != null && t.IsAlive);
        }

        private Graphics mainG;

        private BufferedGraphics bg;

        public Graphics MainGraphics
        {
            get => mainG;
            set
            {
                mainG = value;
                bg = BufferedGraphicsManager.Current.Allocate(
                    MainGraphics, Rectangle.Round(mainG.VisibleClipBounds));
                Stop(true);
                while (IsAlive) ;
                bg.Graphics.Clear(Color.White);
                Start(true);
                Circle.ContainerSize = mainG.VisibleClipBounds.Size.ToSize();
            }
        }

        public Animator(Graphics g)
        {
            MainGraphics = g;
        }

        private List<Circle> circ = new List<Circle>();
        private Thread t;

        public void Start(bool continue_thread = false)
        {
            stop = false;
            if (!continue_thread) AddNewCircle();
            if (t == null || !t.IsAlive)
            {
                t = new Thread(new ThreadStart(Animate));
                t.Start();
            }
        }

        private void AddNewCircle()
        {
            var c = new Circle();
            circ.Add(c);
            c.Start();
        }

        private void Animate()
        {
            while (!stop)
            {
                bg.Graphics.Clear(Color.White);
                circ = circ.FindAll(it => it.IsAlive);
                var cnt = circ.Count;
                for (int i = 0; i<cnt; i++)
                {
                    circ[i].Paint(bg.Graphics);
                }

                try
                {
                    bg.Render(MainGraphics);
                } catch (Exception e) { }

                Thread.Sleep(33);
            }
        }

        public void Stop(bool just_pause = false)
        {
            stop = true;
            if (!just_pause) Circle.StopAll = true;
        }
    }
}
