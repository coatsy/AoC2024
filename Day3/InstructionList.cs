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
        private List<Instruction> allInstructions = new List<Instruction>();
        private List<Instruction> enabledInstructions = new List<Instruction>();

        public InstructionList(string input) {

            allInstructions = ParseInstruction(input);

            var inputSplitAtDont = input.Split(@"don't()");
            enabledInstructions.AddRange(ParseInstruction(inputSplitAtDont[0]));

            foreach (var instruction in inputSplitAtDont.Skip(1))
            {
                if (instruction.IndexOf("do()") >= 0)
                {
                    enabledInstructions.AddRange(ParseInstruction(instruction.Substring(instruction.IndexOf("do()") + "do()".Length)));
                }
            }
        }

        private List<Instruction> ParseInstruction(string instructionString)
        {
            var instrList = new List<Instruction>();
            string pattern = @"mul\(\d{1,3},\d{1,3}\)";
            var matches = Regex.Matches(instructionString, pattern);

            foreach (Match match in matches)
            {
                var instruction = new Instruction();
                var values = match.Value.Replace("mul(", "").Replace(")", "").Split(',');
                instruction.m1 = int.Parse(values[0]);
                instruction.m2 = int.Parse(values[1]);
                instrList.Add(instruction);
            }

            return instrList;
        }
        public int ProductSum => allInstructions.Sum(i => i.Product);
        public int EnabledProductSum => enabledInstructions.Sum(i => i.Product);
    }

    internal class Instruction
    {
        public int m1, m2;
        public int Product => m1 * m2;


    }
}
