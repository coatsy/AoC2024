namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("./input/input.txt");
            var diskMap = new DiskMap(input);
            Console.WriteLine($"Checksum: {diskMap.CheckSum}");
        }
    }
}
