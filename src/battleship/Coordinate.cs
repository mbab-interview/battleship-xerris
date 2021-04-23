using System;

namespace battleship
{
    public record Coordinate
    {
        private int y;

        public int Y
        {
            get => y;
            init
            {
                if (!IsValidY(value))
                    throw new ArgumentOutOfRangeException("Y coordinate must be between 1 and 8");
                y = value;
            }
        }

        private char x;

        public char X
        {
            get => x;
            init {
                if (!IsValidX(value))
                    throw new ArgumentOutOfRangeException("X coordinate must be a char between A and H");
                x = char.ToLower(value);
            }
        }

        public bool IsValidY(int y)
        {
            return y >= 1 && y <= 8;
        }

        public static bool IsValidX(char x)
        {
            return x >= 'A' && x <= 'H' || x >= 'a' && x <= 'h';
        }

        public static Coordinate Parse(string input)
        {
            var trimmedInput = input.Trim();
            if (trimmedInput.Length != 2)
                throw new ArgumentOutOfRangeException("a coordinate is composed of a letter followed by a digit");

            char x = trimmedInput[0];

            if (!int.TryParse(trimmedInput.Substring(1, 1), out int y))
                throw new ArgumentOutOfRangeException("a coordinate is composed of a letter followed by a digit");

            return new Coordinate
            {
                X = x,
                Y = y
            };
        }
    }
}
