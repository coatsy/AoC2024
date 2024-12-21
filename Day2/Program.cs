namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("./input/input.txt");
            var reportList = new ReportList(input);
            Console.WriteLine($"Safe reports: {reportList.SafeCount}");
        }
    }
}
