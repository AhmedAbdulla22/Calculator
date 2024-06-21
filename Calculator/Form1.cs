using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double _number = 0;

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button20_MouseEnter(object sender, EventArgs e)
        {
            button20.BackColor = Color.Red;
            button20.ForeColor = Color.Black;
        }

        private void button20_MouseLeave(object sender, EventArgs e)
        {
            button20.BackColor = Color.Transparent;
            button20.ForeColor = Color.White;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
