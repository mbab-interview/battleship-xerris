using System;
using System.Collections.Generic;
using System.Linq;

namespace battleship
{
    public record Player
    {
        private IReadOnlyCollection<Ship> ships;

        public IReadOnlyCollection<Ship> Ships{
            get => ships;
            init {
                ValidateShips(value);
                ships = value;
            }
        }

        public IReadOnlySet<Coordinate> Hits { get; private init; } = new HashSet<Coordinate>();

        public IReadOnlyDictionary<Coordinate, string> Shots { get; private init; } = new Dictionary<Coordinate, string>();

        public Player Shoot(Coordinate coordinate, Player enemy)
        {
            if (Shots.ContainsKey(coordinate))
                // Should we let the user do that and simply waste its turn ?
                throw new ArgumentOutOfRangeException("Coordinate already shot.");

            var hitShip = enemy.Ships.FirstOrDefault(s => s.Coordinates.Contains(coordinate));

            var newHits = new HashSet<Coordinate>(Hits);
            var newShotList = new Dictionary<Coordinate, string>(Shots);

            if (hitShip is null)
            {
                newShotList.Add(coordinate, "Miss");
                // Todo: make this in a more functionnal way.
                Console.WriteLine("Miss");
            } 
            else
            {
                newHits.Add(coordinate);
                newShotList.Add(coordinate, "Hit");
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

            return this with { Hits = newHits, Shots = newShotList };
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
