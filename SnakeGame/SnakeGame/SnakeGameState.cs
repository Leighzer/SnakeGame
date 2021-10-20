using LeighzerConsoleGameEngine.CoreEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Snake.SnakeGame
{
    public class SnakeGameState : GameState
    {
        public SnakeEntity Snake { get; set; }
        public List<FoodEntity> Foods { get; set; }
        public List<SimpleUIElement> UI { get; set; }
        public SnakeGameStatus GameStatus { get; set; }
        public Random Random { get; set; } = new Random();

        public SnakeGameState()
        {
            UI = new List<SimpleUIElement>();
            for (int i = 0; i < SnakeGame.ScreenX; i++)
            {
                for (int j = 0; j < SnakeGame.ScreenY; j++)
                {
                    if ((i == 0 || i == SnakeGame.ScreenX - 1) || (j == 0 || j == SnakeGame.ScreenY - 1))
                    {
                        UI.Add(new SimpleUIElement()
                        {
                            PositionX = i,
                            PositionY = j,
                            IsVisible = true,
                            Layer = 0,
                            Sprite = '#'
                        });
                    }
                }
            }
            Snake = new SnakeEntity()
            {
                Position = new Vector2(SnakeGame.ScreenX / 2, SnakeGame.ScreenY / 2),
                Velocity = Vector2.UnitX,
                Direction = SnakeDirection.RIGHT
            };
            Foods = new List<FoodEntity>();
            AddRandomFood();
            GameStatus = SnakeGameStatus.Playing;
        }

        public override void Tick(ConsoleKeyInfo? keyPressBuffer, double deltaTime)
        {
            ProcessInput(keyPressBuffer);

            Move();

            Collide();
        }

        private void ProcessInput(ConsoleKeyInfo? keyPressBuffer)
        {
            if (keyPressBuffer.HasValue)
            {
                var key = keyPressBuffer.Value.Key;
                if (key == ConsoleKey.UpArrow)
                {
                    if (Snake.Direction != SnakeDirection.DOWN)
                    {
                        Snake.Direction = SnakeDirection.UP;
                        Snake.Velocity = new Vector2(0, -SnakeEntity.Speed);
                    }
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    if (Snake.Direction != SnakeDirection.LEFT)
                    {
                        Snake.Direction = SnakeDirection.RIGHT;
                        Snake.Velocity = new Vector2(SnakeEntity.Speed, 0);
                    }
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    if (Snake.Direction != SnakeDirection.RIGHT)
                    {
                        Snake.Direction = SnakeDirection.LEFT;
                        Snake.Velocity = new Vector2(-SnakeEntity.Speed, 0);
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (Snake.Direction != SnakeDirection.UP)
                    {
                        Snake.Direction = SnakeDirection.DOWN;
                        Snake.Velocity = new Vector2(0, SnakeEntity.Speed);
                    }
                }
            }
        }

        private void Move()
        {
            Vector2 prevPos = Snake.TruncatedPosition;
            Snake.Position += Snake.Velocity;
            if (prevPos != Snake.TruncatedPosition)
            {
                if (Snake.Body.Any())
                {
                    if (!Snake.SuspendTailLoss)
                    {
                        Snake.Body.RemoveAt(Snake.Body.Count - 1);
                    }

                    Snake.Body.Insert(0, prevPos);
                }
                else if (Snake.SuspendTailLoss)
                {
                    Snake.Body.Insert(0, prevPos);
                }
                Snake.SuspendTailLoss = false;
            }

            Foods.ForEach(x => x.Position += x.Velocity);
        }

        private void Collide()
        {
            for (int i = 0; i < Snake.Body.Count; i++)
            {
                if (Snake.TruncatedPosition == Snake.Body[i])
                {
                    GameStatus = SnakeGameStatus.GameOver;
                }
            }

            for (int i = Foods.Count - 1; i >= 0; i--)
            {
                var food = Foods[i];
                if (Snake.TruncatedPosition == food.Position)
                {
                    Snake.SuspendTailLoss = true; 
                    Foods.RemoveAt(i);
                    AddRandomFood();
                }
            }
        }

        private void AddRandomFood()
        {
            bool isPositionFound = false;
            Vector2 position = new Vector2(-1, -1);
            while (!isPositionFound)
            {
                int x = Random.Next(1, SnakeGame.ScreenX - 1);
                int y = Random.Next(1, SnakeGame.ScreenY - 1);
                if (x <= 0 || y <= 0)
                {
                    throw new Exception("AddRandomFood pos fail");
                }

                position = new Vector2(x, y);
                bool isCollision = false;
                if (position == Snake.TruncatedPosition)
                {
                    isCollision = true;
                }
                Snake.Body.ForEach((x) =>
                {
                    if (position == x)
                    {
                        isCollision = true;
                    }
                });

                if (!isCollision)
                {
                    isPositionFound = true;
                }
            }

            Foods.Add(new FoodEntity()
            {
                Position = position
            });
        }
    }
}
