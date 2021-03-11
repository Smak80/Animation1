using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L2k_2021_02_25.Animation1
{
    public partial class Form1 : Form
    {
        private Animator a;
        public Form1()
        {
            InitializeComponent();
            a = new Animator(panel1.CreateGraphics());
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            a.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            a.Stop();
        }
    }
}
