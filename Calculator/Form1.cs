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
        private enum Turns { firstNumber = 1 , SecondNumber=2,Digit = 3 , Result=4}
        Turns _enTurn = Turns.firstNumber;


        private enum Op {None = 0, Add = 1 , Sub = 2, Mul = 3 , Div = 4,Mod= 5};
        private Op _op = Op.None;
        private string _OpChar = " +-x/%";

        private void PressingOp(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            char.TryParse(btn.Tag.ToString(), out char op); 
            
            switch(op)
            {
                case '+':
                    _op = Op.Add;
                    break;
                case '-':
                    _op = Op.Sub;
                    break;
                case '*':
                    _op = Op.Mul;
                    break;
                case '/':
                    _op = Op.Div;
                    break;
                case '%':
                    _op = Op.Mod;
                    break;
                default:
                    _op = Op.None;
                    break;
            }

            UpdateTextLable();

            //Update Turn

            //so it won't cause deleting 2nd num in the lable
            if (_enTurn == Turns.firstNumber)
            {
            _enTurn = Turns.Digit;
            }

        }

        private void UpdateTextLable()
        {
            if (_enTurn == Turns.firstNumber)
            {
                textBox1.Text = _1stNumber.ToString();
                if (_op != Op.None)
                {
                    textBox1.Text += _OpChar[(int)_op];
                }
            }
            else if (_enTurn == Turns.Digit)
            {

                textBox1.Text = _1stNumber.ToString() + _OpChar[(int)_op];
            }
            else if (_enTurn == Turns.SecondNumber)
            {

                textBox1.Text = _1stNumber.ToString() + _OpChar[(int)_op] + _2ndNumber.ToString();
            }
        }

        private void PressingDigits(object sender , EventArgs e)
        {
            Button btn = (Button)sender;
            double.TryParse(btn.Text, out double temp);

            if (_enTurn == Turns.firstNumber)
            {
                //to shift the 1stnumber to left
                _1stNumber *= 10;
                _1stNumber += temp;

            }
            else if(_enTurn == Turns.Digit || _enTurn == Turns.SecondNumber)
            {

                //to shift the 2ndnumber to left
                _2ndNumber *= 10;
                _2ndNumber += temp;

                if (_enTurn == Turns.Digit)
                {
                    _enTurn = Turns.SecondNumber;
                }

            }

            UpdateTextLable();
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

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (_enTurn == Turns.firstNumber)
            {
            _1stNumber = Math.Floor(_1stNumber / 10);
            }
            else if(_enTurn == Turns.Digit)
            {
                _op = Op.None;

                //set turn to 1st num
                _enTurn = Turns.firstNumber;

            }
            else
            {
            _2ndNumber = Math.Floor(_2ndNumber / 10);
                //set turn to digit if 2nd num became 0
                if (_2ndNumber == 0)
                {
                _enTurn = Turns.Digit;
                }

            }

            UpdateTextLable();
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
