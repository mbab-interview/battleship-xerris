namespace battleship
{
    public interface IYesNoInput
    {
        public bool GetYesOrNo();
    }

    public class YesNoInput : IYesNoInput
    {
        private readonly ConsoleWrapper console;

        public YesNoInput(ConsoleWrapper console)
        {
            this.console = console;
        }

        public bool GetYesOrNo()
        {
            do
            {
                string input = console.ReadLine();
                if (IsYes(input))
                    return true;
                if (IsNo(input))
                    return false;
            }
            while (true);
        }

        public static bool IsYes(string input)
        {
            var low = input.ToLower();
            return low == "y" || low == "yes";
        }

        public static bool IsNo(string input)
        {
            var low = input.ToLower();
            return low == "n" || low == "no";
        }
    }
}
