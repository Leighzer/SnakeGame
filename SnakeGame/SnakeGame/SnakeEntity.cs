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
        public Vector2 Position { get; set; }
        public Vector2 TruncatedPosition
        {
            get
            {
                return new Vector2((int)Position.X, (int)Position.Y);
            }
        }
        public Vector2 Velocity { get; set; }
        public char Sprite { get; set; } = '@';

        public List<Vector2> Body = new List<Vector2>();
        public bool SuspendTailLoss = false;
    }
}
