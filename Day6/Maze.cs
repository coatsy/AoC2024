using System.Drawing;

namespace Day6
{
    internal class Maze
    {
        private string[] maze;
        private char[,] route;

        private Point currentLocation;
        private Direction currentDirection;

        private static readonly List<CharMap> DirectionChars = new List<CharMap>
        {
            CharMap.Up,
            CharMap.Right,
            CharMap.Down,
            CharMap.Left
        };

        public Maze(string mazeFile)
        {
            maze = File.ReadAllLines(mazeFile);
            route = new char[maze.Length, maze[0].Length];
            for (var i = 0; i < maze.Length; i++)
            {
                for (var j = 0; j < maze[i].Length; j++)
                {
                    route[i, j] = (char)CharMap.Blank;
                }
            }


            // find the starting point
            for (var y = 0; y < maze.Length; y++)
            {
                for (var x = 0; x < maze[y].Length; x++)
                {
                    if (DirectionChars.Select(d => (char)d).Contains(maze[y][x]))
                    {
                        currentLocation = new Point(x, y);
                        currentDirection = (Direction)DirectionChars.IndexOf((CharMap)maze[y][x]);
                        route[y, x] = (char)CharMap.Visited;
                    }
                }
            }

            // process the maze
            while (true)
            {
                var nextLocation = GetNextLocation();
                if (nextLocation == Point.Empty)
                {
                    break;
                }
                currentLocation = nextLocation;
                route[currentLocation.Y, currentLocation.X] = (char)CharMap.Visited;
            }
        }

        public int VisitedCount => route.Cast<char>().Count(c => c == (char)CharMap.Visited);

        private Point GetNextLocation()
        {
            // the next location is the current location plus the current direction
            var nextLocation = new Point(currentLocation.X, currentLocation.Y);
            switch (currentDirection)
            {
                case Direction.Up:
                    nextLocation.Y--;
                    break;
                case Direction.Right:
                    nextLocation.X++;
                    break;
                case Direction.Down:
                    nextLocation.Y++;
                    break;
                case Direction.Left:
                    nextLocation.X--;
                    break;
            }
            // if the nex move will take us outside the maze, return an empty point - we're done
            if (nextLocation.Y < 0 || nextLocation.Y >= maze.Length || nextLocation.X < 0 || nextLocation.X >= maze[0].Length)
            {
                return Point.Empty;
            }
            // if the next move is an obstacle, turn right and return this location
            if (maze[nextLocation.Y][nextLocation.X] == (char)CharMap.Obstacle)
            {
                currentDirection = (Direction)((int)(currentDirection + 1) % 4);
                return currentLocation;
            }

            return nextLocation;
        }
    }
}