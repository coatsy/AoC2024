using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    internal class LetterMatrix
    {
        private readonly char[,] letters;
        private int[,] matches;
        private readonly Direction[] directions =
        {
            Direction.Up,
            Direction.Up | Direction.Right,
            Direction.Right,
            Direction.Right | Direction.Down,
            Direction.Down,
            Direction.Down| Direction.Left,
            Direction.Left,
            Direction.Left | Direction.Up
        };

        public LetterMatrix(string[] input)
        {
            letters = new char[input.Length, input[0].Length];
            matches = new int[input.Length, input[0].Length];
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    letters[i, j] = input[i][j];
                    matches[i, j] = 0;
                }
            }

            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    matches[i, j] = checkLetter(i, j);

                }
            }
        }

        public int MatchSum => matches.Cast<int>().Sum();

        private char GetLetter(int row, int col)
        {
            return letters[row, col];
        }
        private int RowCount => letters.GetLength(0);

        private int ColCount => letters.GetLength(1);

        private int checkLetter(int row, int col)
        {
            // check that it's an X, otherwise return 0
            if (GetLetter(row, col) != 'X') return 0;

            int matches = 0;

            foreach (var direction in directions)
            {
                matches += checkLetterInDirection(row, col, direction);
            }

            return matches;

        }

        private static char[] theLetters = { 'X', 'M', 'A', 'S' };
        private int checkLetterInDirection(int row, int col, Direction direction)
        {
            int rowDirection = 0, colDirection = 0;

            // check that it's not too close to the edge
            if ((direction & Direction.Up) == Direction.Up)
            {
                if (row <= 2)
                    return 0;
                rowDirection = -1;
            }
            if ((direction & Direction.Down) == Direction.Down)
            {
                if (row >= RowCount - 3)
                    return 0;
                rowDirection = 1;
            }
            if ((direction & Direction.Left) == Direction.Left)
            {
                if (col <= 2)
                    return 0;
                colDirection = -1;
            }
            if ((direction & Direction.Right) == Direction.Right)
            {
                if (col >= ColCount - 3)
                    return 0;
                colDirection = 1;
            }

            // deliberately starting at 1 because we're already on the X
            for (int i = 1; i < 4; i++)
            {
                if (GetLetter(row + i* rowDirection, col + i* colDirection) != theLetters[i])
                    return 0;
            }

            return 1;

        }
    }

    [Flags]
    internal enum Direction
    {
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8
    }
}
