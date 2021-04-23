using System;

namespace battleship
{
    public class GameLoop
    {
        private readonly IBattleshipGameRunner gameRunner;
        private readonly IYesNoInput yesNoInput;

        public GameLoop(IBattleshipGameRunner gameRunner, IYesNoInput yesNoInput)
        {
            this.gameRunner = gameRunner;
            this.yesNoInput = yesNoInput;
        }

        public void Run()
        {
            do
            {
                Console.WriteLine("Starting a new game... ");
                gameRunner.RunGame();
                Console.WriteLine("Play again ? (y/n)");
            }
            while (yesNoInput.GetYesOrNo());
        }
    }
}
