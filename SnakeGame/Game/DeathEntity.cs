using System.Numerics;

namespace SnakeGame.Game
{
    public class DeathEntity
    {
        public Vector2 Position { get; set; }
        public Vector2 TruncatedPosition
        {
            get
            {
                return new Vector2((int)Position.X, (int)Position.Y);
            }
        }
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public char Sprite { get; set; } = '#';
    }
}
