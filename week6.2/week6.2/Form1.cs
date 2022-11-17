using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week6._2.Abstractions;
using week6._2.Entities;

namespace week6._2
{
    public partial class Form1 : Form
    {
        List<Toy> _toys = new List<Toy>();
        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreatNew();
            _toys.Add(toy);
            panel1.Controls.Add(toy);
            toy.Left = -toy.Width;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var posi = 0; 
            foreach (var toy in _toys)
            {
                toy.MoveToy();
                if (toy.Left > posi)
                    posi = toy.Left;
            }

            if(posi >= 1000)
            {
                var first = _toys[0];
                panel1.Controls.Remove(first);
                _toys.Remove(first);
            }
        }
    }
}
