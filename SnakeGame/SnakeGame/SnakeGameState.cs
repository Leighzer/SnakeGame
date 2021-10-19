using LeighzerConsoleGameEngine.CoreEngine;
using System;
using System.Collections.Generic;
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
            };
            Foods.Add(new FoodEntity()
            {

            });
            GameStatus = SnakeGameStatus.Playing;
        }

        public override void Tick(ConsoleKeyInfo? keyPressBuffer, double deltaTime)
        {
            Move();

            Collide();

            if (keyPressBuffer.HasValue)
            {
                //var key = keyPressBuffer.Value.Key;
                //if (key == ConsoleKey.UpArrow)
                //{
                //    if (CursorY > 0)
                //    {
                //        CursorY -= 1;
                //    }
                //}
                //else if (key == ConsoleKey.RightArrow)
                //{
                //    if (CursorX < SnakeGame.ScreenX - 1)
                //    {
                //        CursorX += 1;
                //    }
                //}
                //else if (key == ConsoleKey.LeftArrow)
                //{
                //    if (CursorX > 0)
                //    {
                //        CursorX -= 1;
                //    }
                //}
                //else if (key == ConsoleKey.DownArrow)
                //{
                //    if (CursorY < SnakeGame.ScreenY - 1)
                //    {
                //        CursorY += 1;
                //    }
                //}
            }
        }

        private void Move()
        {
            Snake.Position += Snake.Velocity;

            Foods.ForEach(x => x.Position += x.Velocity);
        }

        private void Collide()
        {
            for (int i = 0; i < Snake.Body.Count; i++)
            {
                if (Snake.Position == Snake.Body[i])
                {
                    GameStatus = SnakeGameStatus.GameOver;
                }
            }

            for (int i = Foods.Count - 1; i >= 0; i--)
            {
                var food = Foods[i];
                if (Snake.Position == food.Position)
                {   
                    Snake.SuspendTailLoss = true;
                    Foods.RemoveAt(i);
                    AddRandomFood();
                }
            }
        }

        private void AddRandomFood()
        {
            
        }
    }
}
