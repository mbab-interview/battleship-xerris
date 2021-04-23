using NSubstitute;
using Xunit;

namespace battleship.tests
{
    public class GameBoardInitializerTests
    {
        private readonly ConsoleWrapper console = Substitute.For<ConsoleWrapper>();



        [Fact]
        public void PrepareGame_PromptsUntilValidCoordinate()
        {
            var callCount = 0;
            console.ReadLine().Returns(x =>
            {
                callCount++;
                if (callCount < 50)
                    return "JUNK";
                return "B1B2B3";
            });

            var gameInitializer = new GameBoardInitializer(console)
;
            var gameBoard = gameInitializer.PrepareGame();

            Assert.Empty(gameBoard.CurrentPlayer.ReceivedBombs);
            Assert.Empty(gameBoard.CurrentPlayer.Hits);
            Assert.Empty(gameBoard.Opponent.ReceivedBombs);
            Assert.Empty(gameBoard.Opponent.Hits);
            Assert.Equal("player1", gameBoard.CurrentPlayer.Name);
            Assert.Equal("player2", gameBoard.Opponent.Name);

            // 50 to get the first ship right then one for the second ship
            console.Received(51).ReadLine();
        }
    }
}
