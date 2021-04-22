using NSubstitute;
using Xunit;

namespace battleship.tests
{
    public class GameLoopTests
    {
        private readonly IYesNoInput yesNoInput = Substitute.For<IYesNoInput>();
        private readonly IBattleshipGameRunner gameRunner = Substitute.For<IBattleshipGameRunner>();
        private GameLoop gameLoop;

        public GameLoopTests()
        {
            gameLoop = new GameLoop(gameRunner, yesNoInput);
        }

        [Fact]
        public void KeepsPromptingUntilNo()
        {
            // Given
            var callCount = 0;
            yesNoInput.GetYesOrNo().Returns(x =>
            {
                callCount++;
                if (callCount < 50)
                    return true;
                return false;
            });

            // When
            gameLoop.Run();

            // Then
            Assert.True(true); // We are not stuck in infinite loop
            gameRunner.Received(50).RunGame(); // Game was run 50 times
        }
    }
}
