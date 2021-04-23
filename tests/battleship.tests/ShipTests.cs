using System;
using System.Linq;
using Xunit;

namespace battleship.tests
{
    public record ShipTests
    {
        [Theory]
        [InlineData("A1", "A3", "A2")]
        [InlineData("A1", "b1", "c1")]
        [InlineData("B2", "b3", "b4")]
        [InlineData("H6", "H7", "H8")]
        [InlineData("F8", "G8", "h8")]
        public void ValidBoatsCoordinatesTests(params string[] inputs)
        {
            var coords = inputs.Select(Coordinate.Parse).ToList();

            var ship = new Ship { Coordinates = coords };
        }

        [Fact]
        public void ParseTests()
        {
            var ship = Ship.Parse(" A2a3A4 ");
            Assert.Equal(3, ship.Coordinates.Count);
        }

        [Theory]
        [InlineData("A0", "A1", "A2")]
        [InlineData("A1", "B1", "D1")]
        [InlineData("A4", "A6", "A7")]
        [InlineData("B4", "H6", "D8")]
        [InlineData("A4", "B5", "C6")]
        [InlineData("I1", "I2", "I3")]
        [InlineData("H7", "H8", "H9")]
        [InlineData("H7", "H8", "H8")]
        [InlineData("F9", "G9", "h9")]
        [InlineData("a1", "a2", "a3", "a4")]
        [InlineData("a1", "a2")]
        [InlineData("a2")]
        [InlineData()]
        public void InvalidBoatsCoordinatesTests(params string[] inputs)
        {
            var coords = inputs.Select(Coordinate.Parse);

            Assert.ThrowsAny<ArgumentException>( () => new Ship { Coordinates = coords.ToList() });
        }

        [Fact]
        public void NullCoordinates_ShouldFail()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => new Ship { Coordinates = null });
        }
    }
}
