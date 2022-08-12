using LeighzerConsoleGameEngine.CoreEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SnakeGame.Game
{
    public class SnakeGameState : GameState
    {
        public SnakeEntity Snake { get; set; }
        public List<DeathEntity> DeathEntities { get; set; }
        public List<FoodEntity> Foods { get; set; }
        public List<StringUIElement> MainMenuUI { get; set; }
        public List<StringUIElement> GameOverMenuUI { get; set; }
        public SnakeGameStatus GameStatus { get; set; }
        public Random Random { get; set; } = new Random();

        public SnakeGameState()
        {
            GameStatus = SnakeGameStatus.MainMenu;
            DeathEntities = new List<DeathEntity>();
            for (int i = 0; i < SnakeGameEngine.ScreenX; i++)
            {
                for (int j = 0; j < SnakeGameEngine.ScreenY; j++)
                {
                    if (i == 0 || i == SnakeGameEngine.ScreenX - 1 || j == 0 || j == SnakeGameEngine.ScreenY - 1)
                    {
                        DeathEntities.Add(new DeathEntity()
                        {
                            Position = new Vector2(i, j),
                            Sprite = '#'
                        });
                    }
                }
            }
            const string welcomeToSnakeGameByLeighzer = "Welcome to the Snake Game by Leighzer";
            const string pressTheSpacebarToPlay = "Press the spacebar to play";
            MainMenuUI = new List<StringUIElement>()
            {
                new StringUIElement() // welcome
                {
                    PositionX = SnakeGameEngine.ScreenX / 2 - welcomeToSnakeGameByLeighzer.Length / 2,
                    PositionY = SnakeGameEngine.ScreenY / 2 - 3,
                    IsVisible = true,
                    Layer = 0,
                    Sprites = welcomeToSnakeGameByLeighzer
                },
                new StringUIElement() // press btn to play
                {
                    PositionX = SnakeGameEngine.ScreenX / 2 - pressTheSpacebarToPlay.Length / 2,
                    PositionY = SnakeGameEngine.ScreenY / 2 - 1,
                    IsVisible = true,
                    Layer = 0,
                    Sprites = pressTheSpacebarToPlay
                },
            };
            const string gameOver = "Game Over";
            const string pressTheSpacebarToPlayAgain = "Press the spacebar to play again";
            GameOverMenuUI = new List<StringUIElement>()
            {
                new StringUIElement() // game over
                {
                    PositionX = SnakeGameEngine.ScreenX / 2 - gameOver.Length / 2,
                    PositionY = SnakeGameEngine.ScreenY / 2 - 3,
                    IsVisible = true,
                    Layer = 0,
                    Sprites = gameOver
                },
                new StringUIElement() // press btn to play
                {
                    PositionX = SnakeGameEngine.ScreenX / 2 - pressTheSpacebarToPlayAgain.Length / 2,
                    PositionY = SnakeGameEngine.ScreenY / 2 - 1,
                    IsVisible = true,
                    Layer = 0,
                    Sprites = pressTheSpacebarToPlayAgain
                },
            };
        }

        public override void Tick(ConsoleKeyInfo? keyPressBuffer, double deltaTime)
        {
            ProcessInput(keyPressBuffer);

            if (GameStatus == SnakeGameStatus.Playing)
            {
                Move();

                Collide();
            }
        }

        private void ProcessInput(ConsoleKeyInfo? keyPressBuffer)
        {
            if (keyPressBuffer.HasValue)
            {
                var key = keyPressBuffer.Value.Key;

                switch (GameStatus)
                {
                    case SnakeGameStatus.MainMenu:
                        if (key == ConsoleKey.Spacebar)
                        {
                            SetPlaying();
                        }
                        break;
                    case SnakeGameStatus.Playing:
                        if (key == ConsoleKey.UpArrow)
                        {
                            if (Snake.Direction != SnakeDirection.DOWN && Snake.Direction != SnakeDirection.UP && Snake.AllowVelocityChange)
                            {
                                Snake.Direction = SnakeDirection.UP;
                                Snake.Velocity = new Vector2(0, -SnakeEntity.Speed);
                                Snake.AllowVelocityChange = false;
                            }
                        }
                        else if (key == ConsoleKey.RightArrow)
                        {
                            if (Snake.Direction != SnakeDirection.LEFT && Snake.Direction != SnakeDirection.RIGHT && Snake.AllowVelocityChange)
                            {
                                Snake.Direction = SnakeDirection.RIGHT;
                                Snake.Velocity = new Vector2(SnakeEntity.Speed, 0);
                                Snake.AllowVelocityChange = false;
                            }
                        }
                        else if (key == ConsoleKey.LeftArrow)
                        {
                            if (Snake.Direction != SnakeDirection.RIGHT && Snake.Direction != SnakeDirection.RIGHT && Snake.AllowVelocityChange)
                            {
                                Snake.Direction = SnakeDirection.LEFT;
                                Snake.Velocity = new Vector2(-SnakeEntity.Speed, 0);
                                Snake.AllowVelocityChange = false;
                            }
                        }
                        else if (key == ConsoleKey.DownArrow)
                        {
                            if (Snake.Direction != SnakeDirection.UP && Snake.Direction != SnakeDirection.DOWN && Snake.AllowVelocityChange)
                            {
                                Snake.Direction = SnakeDirection.DOWN;
                                Snake.Velocity = new Vector2(0, SnakeEntity.Speed);
                                Snake.AllowVelocityChange = false;
                            }
                        }
                        //else if (key == ConsoleKey.Spacebar)
                        //{
                        //    Snake.SuspendTailLoss = true;
                        //    AddRandomFood();
                        //}
                        else if (key == ConsoleKey.P)
                        {
                            Pause();
                        }
                        else if (key == ConsoleKey.Escape)
                        {
                            SetMainMenu();
                        }
                        break;
                    case SnakeGameStatus.Paused:
                        if (key == ConsoleKey.P)
                        {
                            Unpause();
                        }
                        break;
                    case SnakeGameStatus.GameOver:
                        if (key == ConsoleKey.Spacebar)
                        {
                            SetPlaying();
                        }
                        break;
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
                Snake.AllowVelocityChange = true;
            }

            Foods.ForEach(x => x.Position += x.Velocity);
        }

        private void Collide()
        {
            for (int i = 0; i < Snake.Body.Count; i++)
            {
                if (Snake.TruncatedPosition == Snake.Body[i])
                {
                    SetGameOver();
                }
            }

            for (int i = 0; i < DeathEntities.Count; i++)
            {
                if (Snake.TruncatedPosition == DeathEntities[i].TruncatedPosition)
                {
                    SetGameOver();
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
                int x = Random.Next(1, SnakeGameEngine.ScreenX - 1);
                int y = Random.Next(1, SnakeGameEngine.ScreenY - 1);

                position = new Vector2(x, y);
                bool isCollision = false;
                if (position == Snake.TruncatedPosition)
                {
                    isCollision = true;
                }
                foreach (var bodySegment in Snake.Body)
                {
                    if (position == bodySegment)
                    {
                        isCollision = true;
                        break;
                    }
                }

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

        private void SetMainMenu()
        {
            GameStatus = SnakeGameStatus.MainMenu;
        }

        private void SetPlaying()
        {
            GameStatus = SnakeGameStatus.Playing;
            Snake = new SnakeEntity()
            {
                Position = new Vector2(SnakeGameEngine.ScreenX / 2, SnakeGameEngine.ScreenY / 2),
                Velocity = new Vector2(SnakeEntity.Speed, 0),
                Direction = SnakeDirection.RIGHT
            };
            Foods = new List<FoodEntity>();
            AddRandomFood();
        }

        private void Pause()
        {
            GameStatus = SnakeGameStatus.Paused;
        }

        private void Unpause()
        {
            GameStatus = SnakeGameStatus.Playing;
        }

        private void SetGameOver()
        {
            GameStatus = SnakeGameStatus.GameOver;
        }
    }
}
