using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    enum CharMap : ushort
    {
        Blank = '.',
        Obstacle = '#',
        Up = '^',
        Down = 'v',
        Left = '<',
        Right = '>',
        Visited = 'X'
    }

    enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    [Flags]
    public enum  DirectionFlags
    {
        None = 0,
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8
    }
}
