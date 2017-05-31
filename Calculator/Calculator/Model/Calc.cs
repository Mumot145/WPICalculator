using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace Calculator.Model
{
    [ImplementPropertyChanged]
    public class Calc
    {
        public double WPI { get; set; }
        public string calcInput { get; set; }
        public string fullWPIInput { get; set; }
    }
}
