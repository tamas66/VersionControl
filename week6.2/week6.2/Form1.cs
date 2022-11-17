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

        private Toy _nextToy;

        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set 
            { 
                _factory = value;
                DisplayNext();
            }
        }

        public Form1()
        {
            InitializeComponent();
            
        }


        private void DisplayNext()
        {
            if (_nextToy != null)
            {
                Controls.Remove( _nextToy );
            }

            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            panel1.Controls.Add(toy);
            
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


        private void button3_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var colorPicker = new ColorDialog();

            colorPicker.Color = button.BackColor;
            if (colorPicker.ShowDialog() != DialogResult.OK)
                return;
            button.BackColor = colorPicker.Color;
        }
    }
}
