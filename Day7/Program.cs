namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var equations = File.ReadAllLines("./input/input.txt").Select(l => new Equation(l)).ToList();

            var validEquations = equations.Where(e => e.IsValid).ToList();

            Console.WriteLine($"Valid equations: {validEquations.Count}");
        }
    }
}
