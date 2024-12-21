using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day3
{
    internal class InstructionList
    {
        private List<Instruction> instructions = new List<Instruction>();
        public InstructionList(string input) {
            string pattern = @"mul\(\d{1,3},\d{1,3}\)";
            var matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                var instruction = new Instruction();
                var values = match.Value.Replace("mul(", "").Replace(")", "").Split(',');
                instruction.m1 = int.Parse(values[0]);
                instruction.m2 = int.Parse(values[1]);
                instructions.Add(instruction);
            }
        }

        public int ProductSum => instructions.Sum(i => i.Product);
    }

    internal class Instruction
    {
        public int m1, m2;
        public int Product => m1 * m2;


    }
}
