namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // var input = new string[] { "1 2", "3 4", "5 6" };
            var input = File.ReadLines("./input/input.txt").ToArray();
            var locationList = new LocationList();
            locationList.LoadLocations(input);
            Console.WriteLine(locationList.GetDifferenceSum());
        }
    }
}
