using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class Equation
    {
        private readonly Int64 targetResult;
        private readonly Int64[] values;
        private string validCombination = string.Empty;
        public Equation(string input)
        {
            var sides = input.Split(':');
            if (!Int64.TryParse(sides[0], out targetResult))
            {
                throw new ArgumentException("Invalid input");
            }

            values = sides[1].Trim().Split(' ').Select(v => Int64.Parse(v)).ToArray();

        }

        public bool IsValid => isValid();

        private bool isValid()
        {
            /* 
            // check the sum and the product first
            if (values.Sum() == targetResult)
            {
                validCombination = new string('+', values.Length - 1);
                return true;
            }
            if (values.Sum() > targetResult)
            {
                return false;
            }
            if (values.Aggregate((acc, x) => acc * x) == targetResult)
            {
                validCombination = new string('*', values.Length - 1);
                return true;
            }
            if (values.Aggregate((acc, x) => acc * x) < targetResult)
            {
                return false;
            }
            */
            bool valid = false;

            foreach (var combination in GenerateCombinations(values.Length - 1))
            {
                Int64 acc = values[0];
                for (int i = 1; i < values.Length; i++)
                {
                    if (combination[i - 1] == '+') acc += values[i];
                    else if (combination[i - 1] == '*') acc *= values[i];
                    if (acc > targetResult) break;
                }
                if (acc == targetResult)
                {
                    valid = true;
                    validCombination = combination;
                    break;
                }
            }

            return valid;
        }

        char[] symbols = new char[] { '+', '*' };

        private IEnumerable<string> GenerateCombinations(int Length)
        {
            if (Length == 0)
            {
                return new List<string> { "" };
            }

            return from symbol in symbols
                   from combination in GenerateCombinations(Length - 1)
                   select symbol + combination;
        }
    }
}
