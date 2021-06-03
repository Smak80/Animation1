using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace L2k_2021_02_25.Animation1
{
    class Animator
    {
        class DblBuf
        {
            public BufferedGraphics bg;
        }
        private bool stop = false;
        public bool IsAlive
        {
            get => (t != null && t.IsAlive);
        }

        private Graphics mainG;
        private DblBuf db = new DblBuf();

        public Graphics MainGraphics
        {
            get => mainG;
            set
            {
                mainG = value;
                db.bg = BufferedGraphicsManager.Current.Allocate(
                    MainGraphics, Rectangle.Round(mainG.VisibleClipBounds));
                //Stop(true);
                //while (IsAlive) ;
                Monitor.Enter(db);
                db.bg.Graphics.Clear(Color.White);
                Monitor.Exit(db);
                //Start(true);
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
            if (!continue_thread) 
                AddNewCircle();
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
                circ = circ.FindAll(it => it.IsAlive);
                var cnt = circ.Count;
                Monitor.Enter(db);
                db.bg.Graphics.Clear(Color.White);
                for (int i = 0; i<cnt; i++)
                {
                    circ[i].Paint(db.bg.Graphics);
                }
                try
                {
                    db.bg.Render(MainGraphics);
                } catch (Exception e) { }
                Monitor.Exit(db);

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
