using System.Collections.Generic;
using Xunit;

namespace battleship.tests
{
    public class CollectionExtensionsTests
    {
        [Fact]
        public void OrderedList_Tests()
        {
            Assert.True(new List<int> { 2, 1, 3, 4 }.AreConsecutive());
            Assert.True(new List<int> { 1, 2, 3, 4 }.AreConsecutive());
            Assert.True(new List<int> { 2 }.AreConsecutive());
            Assert.True(new List<int> { }.AreConsecutive());

            // Duplicates are not allowed
            Assert.False(new List<int> { 2, 2, 3, 4 }.AreConsecutive());
            Assert.False(new List<int> { 2, 1, 3, 7 }.AreConsecutive());
        }

        [Fact]
        public void OrderedCharList_Tests()
        {
            Assert.True(new List<char> { 'a', 'b', 'c', 'd' }.AreConsecutive());
            Assert.True(new List<char> { 'd', 'b', 'a', 'c' }.AreConsecutive());
            Assert.True(new List<char> { 'b' }.AreConsecutive());
            Assert.True(new List<char> { }.AreConsecutive());

            // Duplicates are not allowed
            Assert.False(new List<char> { 'b','b', 'a', 'c' }.AreConsecutive());
            Assert.False(new List<char> { 'a', 'b', 'c', 'h' }.AreConsecutive());
        }
    }
}
