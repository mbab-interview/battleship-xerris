using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace battleship.tests
{
    public class CoordinateTests
    {
        [Theory]
        [InlineData("A1", 'a', 1)]
        [InlineData("a1", 'a', 1)]
        [InlineData("a8", 'a', 8)]
        [InlineData("c5", 'c', 5)]
        [InlineData("e6", 'e', 6)]
        [InlineData("h8", 'h', 8)]
        [InlineData("H8", 'h', 8)]
        [InlineData("  H8   ", 'h', 8)]
        public void Parse_ValidValues(string input, char expectedX, int expectedY)
        {
            var coordinate = Coordinate.Parse(input);

            Assert.Equal(expectedX, coordinate.X);
            Assert.Equal(expectedY, coordinate.Y);
        }

        [Theory]
        [InlineData("A9")]
        [InlineData("a0")]
        [InlineData("A23")]
        [InlineData("")]
        [InlineData("11")]
        [InlineData("HH")]
        [InlineData("H9")]
        [InlineData("I1")]
        public void Parse_InvalidValues(string input)
        {
            Assert.ThrowsAny<Exception>(() => Coordinate.Parse(input));
        }
    }
}
