using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    record BoatCoordinates
    {
        private readonly IReadOnlyCollection<Coordinate> coordinates;

        public IReadOnlyCollection<Coordinate> Coordinates {
            get => coordinates;
            init
            {
                if (coordinates is null)
                    throw new ArgumentNullException();
            }
        }

        public static bool AreValidBoatCordinates(IReadOnlyCollection<Coordinate> coordinates)
        {
            if (coordinates is null)
                return false;

            // At the moment, only a boat of size 3 is allowed
            if (coordinates.Count != 3)
                return false;

            var xCoords = coordinates.Select(c => c.X);
            var yCoords = coordinates.Select(c => c.Y);

            // Check if boat is vertical
            if (xCoords.AllIdentical() && yCoords.AllDifferent())
            {
                // Validate that
            }
            return false;
        }
    }
}
