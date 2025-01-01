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
        private readonly List<string> validCombinations = [];
        public Equation(string input)
        {
            var sides = input.Split(':');
            if (!Int64.TryParse(sides[0], out targetResult))
            {
                throw new ArgumentException("Invalid input");
            }
            if (sides[0] != targetResult.ToString())
            {
                throw new ArgumentException("Invalid parsing");
            }

            values = sides[1].Trim().Split(' ').Select(v => Int64.Parse(v)).ToArray();

            checkCombinations();
        }

        public bool IsValid => validCombinations.Count > 0;
        public Int64 TargetResult => targetResult;

        private void checkCombinations()
        {

            foreach (var combination in GenerateCombinations(values.Length - 1))
            {
                Int64 acc = values[0];
                for (int i = 1; i < values.Length; i++)
                {
                    if (combination[i - 1] == '+') acc += values[i];
                    else if (combination[i - 1] == '*') acc *= values[i];
                }
                if (acc == targetResult)
                {
                    validCombinations.Add(combination);
                }
            }
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
