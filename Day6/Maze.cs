using System.Drawing;

namespace Day6
{
    internal class Maze
    {
        private string[] maze;
        private char[,] route;
        private DirectionFlags[,] directionFlags;

        private Point currentLocation;
        private Direction currentDirection;

        private int loopObstacleCount = 0;

        public int LoopObstacleCount => loopObstacleCount;

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
            directionFlags = new DirectionFlags[maze.Length, maze[0].Length];
            for (var i = 0; i < maze.Length; i++)
            {
                for (var j = 0; j < maze[i].Length; j++)
                {
                    route[i, j] = maze[i][j];
                }
            }


            // find the starting point
            Point startingPoint = Point.Empty;
            Direction startingDirection = Direction.Up;
            for (var y = 0; y < maze.Length; y++)
            {
                for (var x = 0; x < maze[y].Length; x++)
                {
                    if (DirectionChars.Select(d => (char)d).Contains(maze[y][x]))
                    {
                        currentLocation = new Point(x, y);
                        startingPoint = currentLocation;
                        currentDirection = (Direction)DirectionChars.IndexOf((CharMap)maze[y][x]);
                        startingDirection = currentDirection;
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

            // Count the possible Obstacle positions for a loop
            for (var y = 0; y < maze.Length; y++)
            {
                for (var x = 0; x < maze[y].Length; x++)
                {
                    // if this is not a blank, can't put put an obstacle there, so skip
                    if (maze[y][x] != (char)CharMap.Blank)
                    {
                        continue;
                    }

                    // put an obstacle here
                    maze[y] = maze[y].Substring(0, x) + (char)CharMap.Obstacle + maze[y].Substring(x + 1);

                    currentDirection = startingDirection;
                    currentLocation = startingPoint;
                    for (var i = 0; i < maze.Length; i++)
                    {
                        for (var j = 0; j < maze[i].Length; j++)
                        {
                            directionFlags[i, j] = DirectionFlags.None;
                        }
                    }

                    bool foundLoop = false;
                    while (!foundLoop)
                    {
                        switch (currentDirection)
                        {
                            case Direction.Up:
                                if ((directionFlags[currentLocation.Y, currentLocation.X] & DirectionFlags.Up) == DirectionFlags.Up)
                                {
                                    foundLoop = true;
                                }
                                else
                                {
                                    directionFlags[currentLocation.Y, currentLocation.X] |= DirectionFlags.Up;
                                }
                                break;
                            case Direction.Right:
                                if ((directionFlags[currentLocation.Y, currentLocation.X] & DirectionFlags.Right) == DirectionFlags.Right)
                                {
                                    foundLoop = true;
                                }
                                else
                                {
                                    directionFlags[currentLocation.Y, currentLocation.X] |= DirectionFlags.Right;
                                }
                                break;
                            case Direction.Down:
                                if ((directionFlags[currentLocation.Y, currentLocation.X] & DirectionFlags.Down) == DirectionFlags.Down)
                                {
                                    foundLoop = true;
                                }
                                else
                                {
                                    directionFlags[currentLocation.Y, currentLocation.X] |= DirectionFlags.Down;
                                }
                                break;
                            case Direction.Left:
                                if ((directionFlags[currentLocation.Y, currentLocation.X] & DirectionFlags.Left) == DirectionFlags.Left)
                                {
                                    foundLoop = true;
                                }
                                else
                                {
                                    directionFlags[currentLocation.Y, currentLocation.X] |= DirectionFlags.Left;
                                }
                                break;
                        }

                        if (foundLoop)
                        {
                            loopObstacleCount++;
                            break;
                        }

                        var nextLocation = GetNextLocation();

                        if (nextLocation == Point.Empty)
                        {
                            break;
                        }
                        currentLocation = nextLocation;
                    }

                    // take the temporary obstacle away again
                    maze[y] = maze[y].Substring(0, x) + (char)CharMap.Blank + maze[y].Substring(x + 1);
                }
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
            // if the next move will take us outside the maze, return an empty point - we're done
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