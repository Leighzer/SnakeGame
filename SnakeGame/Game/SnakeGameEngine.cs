using LeighzerConsoleGameEngine.CoreEngine;

namespace SnakeGame.Game
{
    public class SnakeGameEngine : Engine
    {
        public const int ScreenX = Constants.ScreenWidths.ClassicScreenX;
        public const int ScreenY = Constants.ScreenWidths.ClassicScreenY;
        public const int TickRate = Constants.DefaultTickRate;

        protected new SnakeGameState GameState { get { return base.GameState as SnakeGameState; } }
        protected new SnakeGameRenderer Renderer { get { return base.Renderer as SnakeGameRenderer; } }

        public SnakeGameEngine() : base(tickRate: TickRate)
        {
            base.GameState = new SnakeGameState();
            base.Renderer = new SnakeGameRenderer(ScreenX, ScreenY, GameState);
        }
    }
}
