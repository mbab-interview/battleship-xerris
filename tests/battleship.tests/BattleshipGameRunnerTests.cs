using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace battleship.tests
{
    public class BattleshipGameRunnerTests
    {
        private readonly IGameBoardInitializer boardInitializer = Substitute.For<IGameBoardInitializer>();
        private readonly ICoordinateInput coordinateInput = Substitute.For<ICoordinateInput>();
        private BattleshipGameRunner gameRunner;

        public BattleshipGameRunnerTests()
        {
            gameRunner = new BattleshipGameRunner(boardInitializer, coordinateInput);
        }

        [Fact]
        public void RunGameTest()
        {
            // Given
            boardInitializer.PrepareGame().Returns(
                GameBoard.Create(
                    new Player
                    {
                        Name = "player1",
                        Ships = new List<Ship>
                        {
                            Ship.Parse("A1B1C1")
                        }
                    },
                    new Player
                    {
                        Name = "player2",
                        Ships = new List<Ship>
                        {
                            Ship.Parse("A1B1C1")
                        }
                    }
                ));
            coordinateInput.GetCoordinate()
                // This is the sequence of values that will be returned
                .Returns(
                    Coordinate.Parse("a1"), 
                    Coordinate.Parse("f1"), 
                    Coordinate.Parse("b1"), 
                    Coordinate.Parse("f2"),
                    Coordinate.Parse("c1")
                );

            // When
            gameRunner.RunGame();

            coordinateInput.Received(5).GetCoordinate();
        }
    }
}
