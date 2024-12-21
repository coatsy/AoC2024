namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("./input/input.txt");
            var reportList = new ReportList(input);
             Console.WriteLine($"Safe reports: {reportList.SafeCount}");
            Console.WriteLine($"Total safe and damped Safe reports: {reportList.DampedSafeCount} / {reportList.ReportCount}");
            Console.WriteLine($"Only damped safe reports: {reportList.OnlyDampedSafeCount}");
            //Console.WriteLine();
        }
    }
}
