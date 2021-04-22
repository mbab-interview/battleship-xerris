using System;

namespace battleship
{
    public class GameLoop
    {
        private readonly IYesNoInput yesNoInput;

        public GameLoop (IYesNoInput yesNoInput)
        {
            this.yesNoInput = yesNoInput;
        }

        public void Run()
        {
            do
            {
                Console.WriteLine("Starting a new game... ");
                Console.WriteLine("Play again ? (y/n)");
            }
            while (yesNoInput.GetYesOrNo());
        }
    }
}
