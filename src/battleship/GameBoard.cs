using System;

namespace battleship
{
    public record GameBoard
    {
        public Player CurrentPlayer { get; private init; }

        public Player Opponent { get; private init; }

        public Player? Winner { get; private init; }

        public static GameBoard Create(Player player1, Player player2)
        {
            if (player1.HasReceivedBombs || player2.HasReceivedBombs)
            {
                throw new ArgumentException("A game can only be created with players without any bombs received");
            }
            return new GameBoard
            {
                CurrentPlayer = player1,
                Opponent = player2
            };
        }

        public GameBoard PlayTurn(Func<Player, Coordinate> fetchCoordinates)
        {
            if (Winner != null)
                throw new InvalidOperationException("Game is over, cannot play additionnal turns.");

            var opponentAfterBombShot = Opponent.SendBomb(fetchCoordinates(CurrentPlayer));

            return this with {
                CurrentPlayer = opponentAfterBombShot,
                Opponent = CurrentPlayer,
                Winner = opponentAfterBombShot.AllShipsSunk ? CurrentPlayer : null
            };
        }
    }
}
