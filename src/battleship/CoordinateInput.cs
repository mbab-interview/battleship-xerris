using System;

namespace battleship
{
    public interface ICoordinateInput
    {
        Coordinate GetCoordinate();
    }

    public class CoordinateInput : ICoordinateInput
    {
        private readonly ConsoleWrapper consoleWrapper;

        public CoordinateInput(ConsoleWrapper consoleWrapper)
        {
            this.consoleWrapper = consoleWrapper;
        }

        public Coordinate GetCoordinate()
        {
            while (true)
            {
                Console.WriteLine("Please enter coordinates i.e. A2");
                try
                {
                    return Coordinate.Parse(consoleWrapper.ReadLine());
                }
                // Todo: use custom exceptions to have cleaner messages.
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
