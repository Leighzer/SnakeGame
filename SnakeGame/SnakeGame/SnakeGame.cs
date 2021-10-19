using LeighzerConsoleGameEngine.CoreEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.SnakeGame
{
    public class SnakeGame : Engine
    {
        public const int ScreenX = Constants.ScreenWidths.ClassicScreenX;
        public const int ScreenY = Constants.ScreenWidths.ClassicScreenY;
        public const int TickRate = Constants.DefaultTickRate;

        protected new SnakeGameState GameState { get { return base.GameState as SnakeGameState; } }
        protected new SnakeGameRenderer Renderer { get { return base.Renderer as SnakeGameRenderer; } }

        public SnakeGame() : base(tickRate:TickRate)
        {
            base.GameState = new SnakeGameState();
            base.Renderer = new SnakeGameRenderer(ScreenX, ScreenY, GameState);
        }
    }
}
