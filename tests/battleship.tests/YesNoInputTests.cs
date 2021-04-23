using NSubstitute;
using Xunit;

namespace battleship.tests
{
    public class YesNoInputTests
    {
        private readonly ConsoleWrapper console = Substitute.For<ConsoleWrapper>();
        private readonly YesNoInput yesNoInput;

        public YesNoInputTests()
        {
            yesNoInput = new YesNoInput(console);
        }

        [Fact]
        public void KeepsPromptingUntilValidInput()
        {
            // Given
            var callCount = 0;
            console.ReadLine().Returns(x =>
            {
                callCount++;
                if (callCount < 50)
                    return "junk";
                return "y";
            });

            // When
            bool result = yesNoInput.GetYesOrNo();

            // Then
            Assert.True(result);
        }

        [Theory()]
        [InlineData("y", true)]
        [InlineData("yes", true)]
        [InlineData("yEs", true)]
        [InlineData("n", false)]
        [InlineData("no", false)]
        [InlineData("NO", false)]
        public void Test_ExpectedReturns(string input, bool expected)
        {
            // Given
            console.ReadLine().Returns(input);

            // When
            bool result = yesNoInput.GetYesOrNo();

            // Then
            Assert.Equal(expected, result);
        }
    }
}
