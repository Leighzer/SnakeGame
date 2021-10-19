using System;
using System.Collections.Generic;
using System.Text;

namespace LeighzerConsoleGameEngine.CoreEngine
{
    public abstract class GameState
    {
        public abstract void Tick(ConsoleKeyInfo? keyPressBuffer, double detlaTime);
    }
}
