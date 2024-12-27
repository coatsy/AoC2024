namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in the maze
            var maze = new Maze("./input/input.txt");

            Console.WriteLine($"Positions visited by the guard: {maze.VisitedCount}");
            Console.WriteLine($"Possible Loop obstacle count: {maze.LoopObstacleCount}");
        }
    }
}
