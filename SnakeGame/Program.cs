using Snake.SnakeGame;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Hasher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SnakeGame snakeGame = new SnakeGame();

            snakeGame.Start();
        }
    }
}
