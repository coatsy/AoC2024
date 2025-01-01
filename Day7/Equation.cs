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
        private readonly List<string> validCombinations2Operators = [];
        private readonly List<string> validCombinations3Operators = [];
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

        public bool IsValid2Operators => validCombinations2Operators.Count > 0;
        public bool IsValid3Operators => validCombinations3Operators.Count > 0;
        public Int64 TargetResult => targetResult;
        public Int64[] Values => values;

        char[] symbols2Operators = new char[] { '+', '*' };
        char[] symbols3Operators = new char[] { '+', '*', '|' };

        private void checkCombinations()
        {
            Int64 acc;
            foreach (var combination in GenerateCombinations(values.Length - 1, symbols2Operators))
            {
                acc = values[0];
                for (int i = 1; i < values.Length; i++)
                {
                    if (combination[i - 1] == '+') acc += values[i];
                    else if (combination[i - 1] == '*') acc *= values[i];
                    else throw new ArgumentException("Invalid combination");
                }
                if (acc == targetResult)
                {
                    validCombinations2Operators.Add(combination);
                }
            }

            foreach (var combination in GenerateCombinations(values.Length - 1, symbols3Operators))
            {
                acc = values[0];
                for (int i = 1; i < values.Length; i++)
                {
                    if (combination[i - 1] == '+') acc += values[i];
                    else if (combination[i - 1] == '*') acc *= values[i];
                    else if (combination[i - 1] == '|')acc = Int64.Parse($"{acc}{values[i]}"); 
                    else throw new ArgumentException("Invalid combination");
                }
                if (acc == targetResult)
                {
                    validCombinations3Operators.Add(combination);
                }

            }
        }


        private IEnumerable<string> GenerateCombinations(int Length, char[] symbols)
        {
            if (Length == 0)
            {
                return new List<string> { "" };
            }

            return from symbol in symbols
                   from combination in GenerateCombinations(Length - 1, symbols)
                   select symbol + combination;
        }
    }
}
