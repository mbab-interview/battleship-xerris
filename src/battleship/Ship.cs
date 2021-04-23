using System;
using System.Collections.Generic;
using System.Linq;

namespace battleship
{
    public record Ship
    {
        private readonly IReadOnlyCollection<Coordinate> coordinates;

        public IReadOnlyCollection<Coordinate> Coordinates {
            get => coordinates;
            init {
                ValidateCoordinates(value);
                coordinates = value;
            }
        }

        /// <summary>
        /// This accepts a string with the coordinates in a single string and creates the proper coordinates
        /// i.e A1A2A3 will create a ship with coordinates A1, A2 and A3
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Ship Parse(string input)
        {
            var coordinates = input.Trim().SplitInParts(2).Select(Coordinate.Parse);
            return new Ship
            {
                Coordinates = coordinates.ToList()
            };
        }

        private void ValidateCoordinates(IEnumerable<Coordinate> coordinates)
        {
            if (coordinates is null)
                throw new ArgumentNullException();

            // At the moment, only a boat of size 3 is allowed
            if (coordinates.Count() != 3)
                throw new ArgumentOutOfRangeException("Boats must have a length of 3");

            if (!coordinates.AllDifferent())
                throw new ArgumentException("All coordinates of a boat must be different.");

            var xCoords = coordinates.Select(c => c.X);
            var yCoords = coordinates.Select(c => c.Y);

            // Check if boat is vertical
            if (xCoords.AllIdentical())
            {
                if (!yCoords.AreConsecutive())
                    throw new ArgumentException("Vertical boats Y coordinates must be adjacent.");
            }
            // Check if boat is horizontal
            else if (yCoords.AllIdentical())
            {
                if (!xCoords.AreConsecutive())
                    throw new ArgumentException("Horizontal boats X coordinates must be adjacent.");
            }
            else
            {
                throw new ArgumentException("Invalid boat positionning");
            }
        }
    }
}
