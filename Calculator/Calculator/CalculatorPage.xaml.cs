using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Model;
using Xamarin.Forms;

namespace Calculator
{
    public partial class CalculatorPage : ContentPage
    {
        private string stringPercent = "";
        private double totalImpairment = 100.0;
        char delimiterChars = ',';
        public Calc calc = new Calc();

        public CalculatorPage()
        {
            BindingContext = calc;
            InitializeComponent();
        }

        void AddOne(object sender, EventArgs e)
        {
            Display("1");
        }

        void AddTwo(object sender, EventArgs e)
        {
            Display("2");
        }

        void AddThree(object sender, EventArgs e)
        {
            Display("3");
        }

        void AddFour(object sender, EventArgs e)
        {
            Display("4");
        }

        void AddFive(object sender, EventArgs e)
        {
            Display("5");
        }

        void AddSix(object sender, EventArgs e)
        {
            Display("6");
        }

        void AddSeven(object sender, EventArgs e)
        {
            Display("7");
        }

        void AddEight(object sender, EventArgs e)
        {
            Display("8");
        }

        void AddNine(object sender, EventArgs e)
        {
            Display("9");
        }

        void AddZero(object sender, EventArgs e)
        {
            Display("0");
        }

        void Display(string percent)
        {
            if (!String.IsNullOrEmpty(stringPercent))
            {
                if (stringPercent.Length > 1)
                    return;
            }
            stringPercent = stringPercent + percent;
            calc.calcInput = stringPercent;       
        }

        void Delete(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(stringPercent))            
                return;
         
            stringPercent = stringPercent.TrimEnd(stringPercent[stringPercent.Length - 1]);           
            calc.calcInput = stringPercent;
        }

        void AddParameter(object sender, EventArgs e)
        {
            CreateWPIString();            
        }

        void getWPI(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(calc.fullWPIInput) && String.IsNullOrEmpty(calc.calcInput))
                return;

            String[] values;
            int[] fixedValues = new int[15];
            int i = 0;
            double highestValue = 0;

            double combinedValue = 0.0;
            int roundedFinalValue = 0;


            if (!String.IsNullOrEmpty(calc.fullWPIInput))
            {
                values = calc.fullWPIInput.Split(delimiterChars);
                foreach (var v in values)
                {
                    fixedValues[i] = Int32.Parse(v.Trim());
                    i++;
                }
            }
            if (!String.IsNullOrEmpty(calc.calcInput))
            {
                fixedValues[i] = Int32.Parse(calc.calcInput);
            }
            highestValue = fixedValues.Max();
            if (i > 0)
            {


                int numIndex = Array.IndexOf(fixedValues, (int) highestValue);
                fixedValues = fixedValues.Where((val, idx) => idx != numIndex).ToArray();
                for (int c = 0; c < i; c++)
                {
                    double nextValue = fixedValues.Max();
                    combinedValue = (highestValue / 100) + ((nextValue / 100) * (1 - (highestValue / 100)));
                    double comparison = (combinedValue * 100) - Math.Floor((combinedValue * 100));
                    if (comparison >= 0.5)
                    {
                        roundedFinalValue = (int) Math.Ceiling(combinedValue * 100);
                    }
                    else
                    {
                        roundedFinalValue = (int) Math.Floor(combinedValue * 100);
                    }
                    highestValue = roundedFinalValue;
                    numIndex = Array.IndexOf(fixedValues, (int) nextValue);
                    fixedValues = fixedValues.Where((val, idx) => idx != numIndex).ToArray();
                }
                calc.WPI = roundedFinalValue;
            }
            else
            {
                calc.WPI = highestValue;
            }
            CreateWPIString();

        }

        void CreateWPIString()
        {
            if (!String.IsNullOrEmpty(calc.calcInput))
            {
                if (Int32.Parse(calc.calcInput) > 0)
                {
                
                    if (!String.IsNullOrEmpty(calc.fullWPIInput))
                    {
                        calc.fullWPIInput = calc.fullWPIInput + ", " + calc.calcInput;
                    }
                    else
                    {
                        calc.fullWPIInput = calc.calcInput;
                    }
                }
            }
            
            calc.calcInput = "";
            stringPercent = "";
        }
        void reset(object sender, EventArgs e)
        {
            totalImpairment = 100.0;
            calc.calcInput = "";
            calc.WPI = 0;
            calc.fullWPIInput = "";
            stringPercent = "";
        }
    }
}
