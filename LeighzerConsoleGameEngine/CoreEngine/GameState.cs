using System;

namespace LeighzerConsoleGameEngine.CoreEngine
{
    public abstract class GameState
    {
        public abstract void Tick(ConsoleKeyInfo? keyPressBuffer, double detlaTime);
    }
}
