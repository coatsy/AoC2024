namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("./input/input.txt");
            var instructionList = new InstructionList(input);

            Console.WriteLine($"Sum of products: {instructionList.ProductSum}");
        }
    }
}
