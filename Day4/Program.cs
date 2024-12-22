namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("./input/input.txt");
            var letterMatrix = new LetterMatrix(input);

            Console.WriteLine($"The sum of the XMAS matches is {letterMatrix.XMASMatchSum}");
            Console.WriteLine($"The sum of the X_MAS matches is {letterMatrix.X_MASMatchSum}");
        }
    }
}
