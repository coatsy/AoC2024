namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var equations = File.ReadAllLines("./input/input.txt").Select(l => new Equation(l));

            Console.WriteLine($"Total calibration result with 2 operators: {equations.Where(e => e.IsValid2Operators).Sum(v=>v.TargetResult)}");
            Console.WriteLine($"Total calibration result with 3 operators: {equations.Where(e => e.IsValid3Operators).Sum(v => v.TargetResult)}");
        }
    }
}
