using LeighzerConsoleGameEngine.CoreEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.SnakeGame
{
    public class SnakeGameRenderer : Renderer
    {
        protected new SnakeGameState GameState { get { return base.GameState as SnakeGameState; } }

        public SnakeGameRenderer(int x, int y, SnakeGameState gameState) : base(x, y, gameState)
        {

        }

        public override void UpdateBuffer()
        {
            for (int i = 0; i < GameState.UI.Count; i++)
            {
                var uiChar = GameState.UI[i];
                Draw(uiChar.Sprite, uiChar.PositionX, uiChar.PositionY);
            }

            //Draw('@', GameState.CursorX, GameState.CursorY);
        }
    }
}
