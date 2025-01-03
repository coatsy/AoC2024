namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("./input/input.txt");
            var diskMap = new DiskMap(input);
            Console.WriteLine($"Checksum 1: {diskMap.Defrag1CheckSum}");
            Console.WriteLine($"Checksum 2: {diskMap.Defrag2CheckSum}");
        }
    }
}
