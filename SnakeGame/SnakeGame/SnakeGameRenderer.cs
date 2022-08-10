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
            switch (GameState.GameStatus)
            {
                case SnakeGameStatus.MainMenu:
                    for (int i = 0; i < GameState.MainMenuUI.Count; i++)
                    {
                        var mainMenuUIElement = GameState.MainMenuUI[i];
                        DrawText(mainMenuUIElement.Sprites, mainMenuUIElement.PositionX, mainMenuUIElement.PositionY);
                    }
                    break;
                case SnakeGameStatus.Playing:
                case SnakeGameStatus.Paused:
                    for (int i = 0; i < GameState.DeathEntities.Count; i++)
                    {
                        var deathEntity = GameState.DeathEntities[i];
                        Draw(deathEntity.Sprite, (int)deathEntity.TruncatedPosition.X, (int)deathEntity.TruncatedPosition.Y);
                    }

                    Draw(GameState.Snake.Sprite, (int)GameState.Snake.TruncatedPosition.X, (int)GameState.Snake.TruncatedPosition.Y);

                    for (int i = 0; i < GameState.Snake.Body.Count; i++)
                    {
                        var body = GameState.Snake.Body[i];
                        Draw(GameState.Snake.Sprite, (int)body.X, (int)body.Y);
                    }

                    for (int i = 0; i < GameState.Foods.Count; i++)
                    {
                        Draw(GameState.Foods[i].Sprite, (int)GameState.Foods[i].TruncatedPosition.X, (int)GameState.Foods[i].TruncatedPosition.Y);
                    }
                    break;
                case SnakeGameStatus.GameOver:
                    for (int i = 0; i < GameState.GameOverMenuUI.Count; i++)
                    {
                        var gameOverUIElement = GameState.GameOverMenuUI[i];
                        DrawText(gameOverUIElement.Sprites, gameOverUIElement.PositionX, gameOverUIElement.PositionY);
                    }
                    break;
            }
        }
    }
}
