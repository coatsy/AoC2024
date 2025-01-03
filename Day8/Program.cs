namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("./input/input.txt");
            var nodeMap = new NodeMap(input);
            Console.WriteLine($"Single Distance AntiNode Count: {nodeMap.SingleDistanceAntiNodeCount}");
            Console.WriteLine($"Any Distance AntiNode Count: {nodeMap.AnyDistanceAntiNodeCount}");
        }
    }
}
