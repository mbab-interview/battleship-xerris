using System;

namespace battleship
{
    public interface IBattleshipGameRunner
    {
        void RunGame();
    }

    public class BattleshipGameRunner : IBattleshipGameRunner
    {
        private readonly IGameBoardInitializer gameBoardInitializer;
        private readonly ICoordinateInput coordinateInput;

        public BattleshipGameRunner(IGameBoardInitializer gameBoardInitializer, ICoordinateInput coordinateInput)
        {
            this.gameBoardInitializer = gameBoardInitializer;
            this.coordinateInput = coordinateInput;
        }

        public void RunGame()
        {
            var gameBoard = gameBoardInitializer.PrepareGame();

            while (gameBoard.Winner is null)
            {
                try
                {
                    gameBoard = gameBoard.PlayTurn((p) =>
                    {
                        Console.WriteLine($"It is {p.Name}'s turn");
                        // Todo: print additionnal contextual data.
                        return coordinateInput.GetCoordinate();
                    });
                }
                // Todo: use specific exceptions.
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine($"Game over. {gameBoard.Winner.Name} won !!!");
        }
    }
}
