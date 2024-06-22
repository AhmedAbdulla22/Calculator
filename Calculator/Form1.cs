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

        private double _1stNumber = 0,_2ndNumber = 0 , _Result = 0;
        private enum Turns { firstNumber = 1 , SecondNumber=2 , Result=3}
        Turns _enTurn = Turns.firstNumber;

        private void PressingDigits(object sender , EventArgs e)
        {
            Button btn = (Button)sender;
            double.TryParse(btn.Text, out double temp);

            //to shift the 1stnumber to left
            _1stNumber *= 10;
            _1stNumber += temp;

            textBox1.Text = _1stNumber.ToString();
        }

        private void button20_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
            btnClose.ForeColor = Color.Black;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _1stNumber = _2ndNumber = 0;
            textBox1.Text = "0";
        }

        private void button20_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
            btnClose.ForeColor = Color.White;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
