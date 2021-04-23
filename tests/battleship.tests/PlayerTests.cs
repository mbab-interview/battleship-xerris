using System;
using System.Collections.Generic;
using Xunit;

namespace battleship.tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerNoBoatShouldFail()
        {
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() => new Player { Ships = new List<Ship>() });
        }

        [Fact]
        public void PlayerMoreThanOneBoatShouldFail()
        {
            var ships = new List<Ship>
            {
                Ship.Parse("A1A2A3"),
                Ship.Parse("B1B2B3")
            };
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() => new Player { Ships = ships });
        }

        [Fact]
        public void PlayerBoatsCannotBeNull()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => new Player { Ships = null });
        }

        [Fact]
        public void PlayerWithNoHitsShouldNotHaveAllShipSunk()
        {
            var ships = new List<Ship>
            {
                Ship.Parse("A1A2A3"),
            };
            var player = new Player { Ships = ships };

            Assert.False(player.AllShipsSunk);
        }

        [Fact]
        public void ObjectIsCopiedAfterEachBomb()
        {
            var ships = new List<Ship>
            {
                Ship.Parse("C3D3E3"),
            };
            var player = new Player { Ships = ships };

            Coordinate h4 = Coordinate.Parse("H4");

            var playerAfterAttack = player.SendBomb(h4);

            Assert.NotSame(player, playerAfterAttack);
            Assert.NotEqual(player, playerAfterAttack);
        }

        [Fact]
        public void MissesAreTracked()
        {
            var ships = new List<Ship>
            {
                Ship.Parse("C3D3E3"),
            };
            var player = new Player { Ships = ships };

            Coordinate h4 = Coordinate.Parse("H4");

            var playerAfterAttack = player.SendBomb(h4);

            Assert.Contains(h4, playerAfterAttack.ReceivedBombs);
        }

        [Fact]
        public void AfterMissesThereAreNotHits()
        {
            var ships = new List<Ship>
            {
                Ship.Parse("C3D3E3"),
            };
            var player = new Player { Ships = ships };

            Coordinate h4 = Coordinate.Parse("H4");
            Coordinate c4 = Coordinate.Parse("C4");
            Coordinate a1 = Coordinate.Parse("A1");

            var playerAfterAttacks = player
                .SendBomb(h4)
                .SendBomb(c4)
                .SendBomb(a1);

            Assert.Empty(playerAfterAttacks.Hits);
            Assert.Equal("M", playerAfterAttacks.ReceivedBombs[h4]);
            Assert.Equal("M", playerAfterAttacks.ReceivedBombs[c4]);
            Assert.Equal("M", playerAfterAttacks.ReceivedBombs[a1]);
        }

        [Fact]
        public void DuplicateBombCoordinateShouldThrow()
        {
            var ships = new List<Ship>
            {
                Ship.Parse("C3D3E3"),
            };

            Coordinate h4 = Coordinate.Parse("H4");
            var player = new Player { Ships = ships }.SendBomb(h4);

            Assert.Throws<ArgumentOutOfRangeException>(() => player.SendBomb(h4));
        }

        [Fact]
        public void HitsAreBeingCounted()
        {
            var ships = new List<Ship>
            {
                Ship.Parse("C3D3E3"),
            };
            var player = new Player { Ships = ships };

            Coordinate c3 = Coordinate.Parse("C3");
            Coordinate d3 = Coordinate.Parse("D3");
            Coordinate e3 = Coordinate.Parse("E3");

            var playerAfterAttacks = player
                .SendBomb(c3);

            Assert.False(playerAfterAttacks.AllShipsSunk);

            playerAfterAttacks = playerAfterAttacks
                .SendBomb(d3);

            Assert.False(playerAfterAttacks.AllShipsSunk);

            playerAfterAttacks = playerAfterAttacks
                .SendBomb(e3);

            Assert.True(playerAfterAttacks.AllShipsSunk);

            Assert.Contains(c3, playerAfterAttacks.Hits);
            Assert.Contains(d3, playerAfterAttacks.Hits);
            Assert.Contains(e3, playerAfterAttacks.Hits);
        }
    }
}
