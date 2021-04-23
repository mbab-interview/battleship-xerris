using System;
using System.Collections.Generic;
using System.Linq;

namespace battleship
{
    public record Player
    {
        private IReadOnlyCollection<Ship> ships;

        public IReadOnlyCollection<Ship> Ships{
            private get => ships;
            init {
                ValidateShips(value);
                ships = value;
            }
        }

        private string name = "player";

        public string Name
        {
            get => name;
            init {
                var trimmedValue = value.Trim();
                if (string.IsNullOrWhiteSpace(trimmedValue))
                    throw new ArgumentException("Player name cannot be empty");
                name = trimmedValue;
            }
        }

        public IReadOnlySet<Coordinate> Hits { get; private init; } = new HashSet<Coordinate>();

        public IReadOnlyDictionary<Coordinate, string> ReceivedBombs { get; private init; } = new Dictionary<Coordinate, string>();

        public bool AllShipsSunk => Ships.All(s => s.Coordinates.All(Hits.Contains));

        public bool HasReceivedBombs => ReceivedBombs.Count > 0;

        /// <summary>
        /// Sends a bomb on this player.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns>A copy of the player with the updated Hits and Misses</returns>
        public Player SendBomb(Coordinate coordinate)
        {
            if (ReceivedBombs.ContainsKey(coordinate))
                // Should we let the user do that and simply waste its turn ?
                // Requirement says "a single shot"
                // Todo: update this and the test to meet requirements
                throw new ArgumentOutOfRangeException("Coordinate already shot.");

            var hitShip = Ships.FirstOrDefault(s => s.Coordinates.Contains(coordinate));

            var newHits = new HashSet<Coordinate>(Hits);
            var newShotList = new Dictionary<Coordinate, string>(ReceivedBombs);

            if (hitShip is null)
            {
                newShotList.Add(coordinate, "M");
                // Todo: make this in a more functionnal way.
                Console.WriteLine("Miss");
            }
            else
            {
                newHits.Add(coordinate);
                newShotList.Add(coordinate, "H");
                if (hitShip.Coordinates.All(newHits.Contains))
                {
                    // Todo: make this in a more functionnal way.
                    Console.WriteLine("You sunk my battleship");
                }
                else
                {
                    // Todo: make this in a more functionnal way.
                    Console.WriteLine("Hit");
                }
            }

            return this with { Hits = newHits, ReceivedBombs = newShotList };
        }

        private void ValidateShips(IReadOnlyCollection<Ship> ships)
        {
            if (ships == null)
                throw new ArgumentNullException();

            // For now there is only one ship
            if (ships.Count != 1)
                throw new ArgumentOutOfRangeException("Only one ship is allowed");

            // Todo: Implement multiple logic to allow multiple ships.
        }
    }
}
