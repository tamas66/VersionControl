﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week6._2.Abstractions
{
    abstract class Toy
    {
        public Toy()
        {
            AutoSize = false;
            Width = 50;
            Height = 50;
            Paint += Ball_Paint;
        }

        private void Ball_Paint(object sender, PaintEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void DrawImage(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }

        public void MoveBall()
        {
            Left += 1;
        }
    }
}