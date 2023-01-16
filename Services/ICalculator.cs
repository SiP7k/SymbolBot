using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolBot.Services
{
    public interface ICalculator
    {
        public int CalculateSymbols(string text);
        public int Sum(string text);
    }
}
