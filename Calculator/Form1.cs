﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private double _1stNumber = 0,_2ndNumber = 0 , _Result = 0;
        private enum Turns { firstNumber = 1 , SecondNumber=2,Operator = 3 , Result=4}
        Turns _enTurn = Turns.firstNumber;


        private enum Op {None = 0, Add = 1 , Sub = 2, Mul = 3 , Div = 4,Mod= 5,X2 = 6 , Square = 7, Dot = 8};
        private Op _op = Op.None;
        private char[] _OpChar = { '*', '+', '-', 'x', '/', '%' };

        private enum Result { none = 0,invalid = 1,}
        Result _enResult = Result.none;

        private bool _1stIsFloat = false,_2ndIsFloat = false;

        private void PressingOp(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            char.TryParse(btn.Tag.ToString(), out char op);

            if (char.IsDigit(textBox1.Text[textBox1.TextLength -1]))
            {
            textBox1.Text += op;
            }

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



        }

        private void UpdateResult()
        {
            if (_enResult == Result.invalid)
            {
                textBox1.ForeColor = Color.Red;
                textBox1.Text = "Invalid! 0 is undivisable...";
                
                //reset it
                _enResult = Result.none;

            }
            else
            {
                textBox1.Text = _Result.ToString();

            }
                if (_op != Op.X2 && _op != Op.Square)
                {
                    
                Reset();
                }

        }

        private void Reset()
        {
            //reset textbox
            textBox1.Text = "0";

            _1stNumber = _2ndNumber = _Result = 0;
            _enTurn = Turns.firstNumber;
            _op = Op.None;
            _enResult = Result.none;
        }

        private void UpdateTextLable()
        {
            if (textBox1.ForeColor == Color.Red)
            {
                textBox1.ForeColor = Color.Black;
            }

            if (_enTurn != Turns.Operator)
                {
                //textBox1.Text = ((float)_1stNumber).ToString();
                //if (_op != Op.None && _op != Op.X2 && _op != Op.Square)
                //{
                //    textBox1.Text += _OpChar[(int)_op];
                //}
                
                }
                else if (_enTurn == Turns.Operator)
                {

                  //  textBox1.Text = _1stNumber.ToString() + _OpChar[(int)_op];
                }
                //else if (_enTurn == Turns.SecondNumber)
                //{

                //    textBox1.Text = _1stNumber.ToString() + _OpChar[(int)_op] + _2ndNumber.ToString();
                //}

            
        }

        private void PressingDigits(object sender , EventArgs e)
        {
            Button btn = (Button)sender;

            if (textBox1.Text[0] == '0' && btn.Text[0] == '0' && textBox1.TextLength == 1)
            {
                return;
            }
            else if(textBox1.Text[0] == '0' && textBox1.TextLength == 1)
            {
                textBox1.Text = btn.Text;
            }
            else
            {
                textBox1.Text += btn.Text;

            }
        }

        private void button20_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
            btnClose.ForeColor = Color.Black;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Reset();

            UpdateTextLable();
            
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength >= 1)
            {
            textBox1.Text = textBox1.Text.Substring(0,textBox1.TextLength - 1);
                if (textBox1.TextLength == 0)
                {
                    textBox1.Text = "0";
                }
            }
            else
            {
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 363;
        }

        private void CalculateResult()
        {
            var txt = textBox1.Text;
            string[] numsTxt = txt.Split(_OpChar);
            char[] opChars = txt.Where(c => _OpChar.Contains(c)).ToArray();

            for (int i = 0; i < numsTxt.Length - 1; i++)
            {
                double.TryParse(numsTxt[i], out double firstNum);
                double.TryParse(numsTxt[i+1], out double SecondNum);
                switch(opChars[i])
                {
                    case '+':
                        _Result = firstNum + SecondNum;
                        break;
                    case '-':
                        _Result = firstNum - SecondNum;
                        break;
                    case '*':
                        _Result = firstNum * SecondNum;
                        break;
                    case '/':
                        _Result = firstNum / SecondNum;
                        break;
                }

                numsTxt[i + 1] = _Result.ToString();
            }
                
            textBox1.Text = _Result.ToString();


        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            
                CalculateResult();
                                         
        }

        private void btnX2_Click(object sender, EventArgs e)
        {
            if (_enTurn == Turns.firstNumber)
            {
                _op = Op.X2;
            }

            btnEqual_Click(sender , e);
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            if (_enTurn == Turns.firstNumber)
            {
                _op = Op.Square;
            }

            btnEqual_Click(sender, e);
        }

        private void btnSigns_Click(object sender, EventArgs e)
        {
            if (_enTurn == Turns.firstNumber)
            {
                _1stNumber *= -1;               
            }
            else if(_enTurn == Turns.SecondNumber)
            {
                 _2ndNumber *= -1;
            }

            UpdateTextLable();
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // Find the last index of the dot
            int lastIndexOfDot = textBox1.Text.LastIndexOf('.');
            
            // Find the last index of any operator
            int lastIndexOfOperator = textBox1.Text.LastIndexOfAny(_OpChar);

            // Check if there is a dot and if it appears after the last operator
            bool hasDot = lastIndexOfDot != -1 && lastIndexOfOperator < lastIndexOfDot;

            if (hasDot || !char.IsDigit(textBox1.Text[textBox1.TextLength - 1]))
            {
                return;
            }
            else
            {
                textBox1.Text += button.Text;
            }
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
