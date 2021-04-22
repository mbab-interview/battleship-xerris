using System;
using System.Collections.Generic;
using Xunit;

namespace battleship.tests
{
    public class GameBoardTests
    {
        private readonly Player player1 = new Player
        {
            Name = "player1",
            Ships = new List<Ship>
                {
                    Ship.Parse("B2B3B4")
                }
        };
        private readonly Player player2 = new Player
        {
            Name = "player2",
            Ships = new List<Ship>
            {
                Ship.Parse("c2d2e2")
            }
        };
        private readonly GameBoard gameBoard;

        public GameBoardTests()
        {
            gameBoard = GameBoard.Create(player1, player2);
        }

        [Fact]
        public void CannotCreateGameWithPlayersThatHaveShots()
        {
            // Player that already received a bomb
            var bombardedPlayer = player1.SendBomb(Coordinate.Parse("A1"));
            Assert.Throws<ArgumentException>(() => GameBoard.Create(bombardedPlayer, player2));
            Assert.Throws<ArgumentException>(() => GameBoard.Create(player2, bombardedPlayer));
        }

        [Fact]
        public void AfterTurnCurrentPlayerAndOpponentAreSwapped()
        {
            Assert.Equal(player1.Name, gameBoard.CurrentPlayer.Name);
            Assert.Equal(player2.Name, gameBoard.Opponent.Name);

            var gameAfterFirstTurn = gameBoard.PlayTurn((p) => Coordinate.Parse("c2"));

            Assert.Equal(player1.Name, gameAfterFirstTurn.Opponent.Name);
            Assert.Equal(player2.Name, gameAfterFirstTurn.CurrentPlayer.Name);

            // player 2 attacks player 1 now.
            var gameAfterSecondTurn = gameAfterFirstTurn.PlayTurn((p) => Coordinate.Parse("c2"));

            Assert.Equal(player1.Name, gameAfterSecondTurn.CurrentPlayer.Name);
            Assert.Equal(player2.Name, gameAfterSecondTurn.Opponent.Name);

            Assert.Null(gameAfterSecondTurn.Winner);
        }

        [Fact]
        public void AttackingTwiceSameCoordinateShouldThrow()
        {
            Func<Player, Coordinate> getC2 = (p) => Coordinate.Parse("c2");

            // player 1 attacks player 2.
            var gameAfterFirstTurn = gameBoard.PlayTurn(getC2);

            // player 2 attacks player 1.
            var gameAfterSecondTurn = gameAfterFirstTurn.PlayTurn(getC2);

            // player 1 attacks player 2.
            Assert.Throws<ArgumentOutOfRangeException>(() => gameAfterSecondTurn.PlayTurn(getC2));
        }

        [Fact]
        public void WhenAllShipsAreSunkNoAdditionnalGamesCanBePlayed()
        {
            // player 1 hits player 2.
            var inProgressGame = gameBoard.PlayTurn((p) => Coordinate.Parse("c2"));
            Assert.Null(inProgressGame.Winner);

            // player 2 misses player 1.
            inProgressGame = inProgressGame.PlayTurn((p) => Coordinate.Parse("c2"));
            Assert.Null(inProgressGame.Winner);

            // player 1 hits player 2.
            inProgressGame = inProgressGame.PlayTurn((p) => Coordinate.Parse("d2"));
            Assert.Null(inProgressGame.Winner);

            // player 2 misses player 1.
            inProgressGame = inProgressGame.PlayTurn((p) => Coordinate.Parse("d2"));
            Assert.Null(inProgressGame.Winner);

            // player 1 hits player 2 and wins.
            inProgressGame = inProgressGame.PlayTurn((p) => Coordinate.Parse("e2"));
            Assert.Equal(player1.Name, inProgressGame.Winner.Name);

            // No more turns can be played when a game is over
            Assert.Throws<InvalidOperationException>(() => inProgressGame.PlayTurn((p) => Coordinate.Parse("e2")));
        }
    }
}
