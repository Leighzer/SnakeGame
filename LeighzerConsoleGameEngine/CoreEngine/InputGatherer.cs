using System;

namespace LeighzerConsoleGameEngine.CoreEngine
{
    public class InputGatherer
    {
        public ConsoleKeyInfo? KeyPressBuffer { get; set; }

        public InputGatherer()
        {
            this.KeyPressBuffer = null;
        }

        public void ClearInput()
        {
            KeyPressBuffer = null;
        }

        public void TryGatherInput()
        {
            if (Console.KeyAvailable)
            {
                KeyPressBuffer = Console.ReadKey(true);
            }
        }
    }
}
