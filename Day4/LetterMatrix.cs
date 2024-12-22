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
        private int[,] XMASmatches;
        private int[,] X_MASmatches;
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
            XMASmatches = new int[input.Length, input[0].Length];
            X_MASmatches = new int[input.Length, input[0].Length];
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    letters[i, j] = input[i][j];
                    XMASmatches[i, j] = 0;
                    X_MASmatches[i, j] = 0;
                }
            }

            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    XMASmatches[i, j] = checkXMASLetter(i, j);
                    X_MASmatches[i, j] = checkX_MASLetter(i, j);
                }
            }
        }

        public int XMASMatchSum => XMASmatches.Cast<int>().Sum();
        public int X_MASMatchSum => X_MASmatches.Cast<int>().Sum();

        private static string[] X_MASPatterns =
        {
            "MMSS",
            "MSSM",
            "SSMM",
            "SMMS"
        };
        private int checkX_MASLetter(int row, int col)
        {
            // check thatit's an A, otherwise return 0
            if (GetLetter(row, col) != 'A') return 0;

            // check that it's not in the first or last row or column
            if (row == 0 || row == RowCount - 1 || col == 0 || col == ColCount - 1) return 0;

            // get the letters in surrounding diagonal cells (Note that the order matters)
            var letters = new char[4]
            {
                GetLetter(row - 1, col - 1), 
                GetLetter(row - 1, col + 1), 
                GetLetter(row + 1, col + 1), 
                GetLetter(row + 1, col - 1) 
            };
            if(!X_MASPatterns.Contains(new string(letters))) return 0;

            return 1;
        }

        private char GetLetter(int row, int col)
        {
            return letters[row, col];
        }
        private int RowCount => letters.GetLength(0);

        private int ColCount => letters.GetLength(1);

        private int checkXMASLetter(int row, int col)
        {
            // check that it's an X, otherwise return 0
            if (GetLetter(row, col) != 'X') return 0;

            int matches = 0;

            foreach (var direction in directions)
            {
                matches += checkXMASLetterInDirection(row, col, direction);
            }

            return matches;

        }

        private static char[] theLetters = { 'X', 'M', 'A', 'S' };
        private int checkXMASLetterInDirection(int row, int col, Direction direction)
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
