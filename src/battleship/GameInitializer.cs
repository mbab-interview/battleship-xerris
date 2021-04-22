using System;
using System.Collections.Generic;

namespace battleship
{
    public interface IGameBoardInitializer
    {
        GameBoard PrepareGame();
    }

    public class GameBoardInitializer : IGameBoardInitializer
    {
        private const string player1Name = "player1";
        private const string player2Name = "player2";
        private readonly ConsoleWrapper consoleWrapper;

        public GameBoardInitializer(ConsoleWrapper consoleWrapper)
        {
            this.consoleWrapper = consoleWrapper;
        }

        public GameBoard PrepareGame()
        {
            Ship player1Ship = GetShip(player1Name);
            Ship player2Ship = GetShip(player2Name);

            Player player1 = new Player
            {
                Ships = new List<Ship> { player1Ship },
                Name = player1Name
            };

            Player player2 = new Player
            {
                Ships = new List<Ship> { player2Ship },
                Name = player2Name
            };

            return GameBoard.Create(player1, player2);

            Ship GetShip(string playerName)
            {
                while (true)
                {
                    Console.WriteLine($"{playerName}: Please write your boat's coordinates without spaces. i.e A1A2A3");
                    try
                    {
                        return Ship.Parse(consoleWrapper.ReadLine());
                    }
                    // Todo: use custom exceptions
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
