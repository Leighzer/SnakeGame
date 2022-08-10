using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Snake.SnakeGame
{
    public class SnakeEntity
    {
        public const float Speed = 0.25f;
        public Vector2 Position { get; set; }
        public Vector2 TruncatedPosition
        {
            get
            {
                return new Vector2((int)Position.X, (int)Position.Y);
            }
        }
        public bool AllowVelocityChange { get; set; }
        public Vector2 Velocity { get; set; }
        public char Sprite { get; set; } = '@';

        public List<Vector2> Body { get; set; }  = new List<Vector2>();
        public bool SuspendTailLoss { get; set; }
        public SnakeDirection Direction { get; set; }
    }
}
