using System;

namespace battleship
{
    /// <summary>
    /// This is a wrapper on the Console.ReadLine to be able to unit test the remainder of the code.
    /// </summary>
    public class ConsoleWrapper
    {
        public virtual string ReadLine() => Console.ReadLine();
    }
}
