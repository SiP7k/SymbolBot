using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SymbolBot.Services
{
    public class Calculator : ICalculator
    {
        public int Sum(string text)
        {
            string[] textNumbers = text.Split(" ");
            int result = 0;

            try
            {
                foreach (var number in textNumbers)
                {
                    result = result + Int32.Parse(number);
                }
            }
            catch
            {
                Console.WriteLine("Неправильный ввод чисел, введите числа через пробел, без лишних символов!");
            }
            return result;
        }
        public int CalculateSymbols(string text)
        {
            return text.Length;
        }
    }
}
