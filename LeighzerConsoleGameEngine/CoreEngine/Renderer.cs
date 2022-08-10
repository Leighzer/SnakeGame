using System;
using System.Collections.Generic;
using System.Text;

namespace LeighzerConsoleGameEngine.CoreEngine
{
    public abstract class Renderer
    {
        protected int ScreenX { get; set; }
        protected int ScreenY { get; set; }
        protected char[][] FrameBuffer { get; set; }
        protected GameState GameState { get; set; }

        public Renderer(int screenX, int screenY, GameState gameState)
        {
            this.ScreenX = screenX;
            this.ScreenY = screenY;
            this.FrameBuffer = new char[screenY][];
            for (int i = 0; i < FrameBuffer.Length; i++)
            {
                FrameBuffer[i] = new char[screenX];
            }
            this.GameState = gameState;
        }

        // clear by drawing spaces
        // Console.Clear() is too slow to do 60 times per second
        public void Clear()
        {
            for (int i = 0; i < FrameBuffer.Length; i++)
            {
                for (int j = 0; j < FrameBuffer[i].Length; j++)
                {
                    FrameBuffer[i][j] = ' ';
                }
            }
        }

        public void Draw(char c, int x, int y)
        {
            FrameBuffer[y][x] = c;
        }

        public void DrawText(string s, int startX, int startY)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var characters = s.ToCharArray();
                int x = startX;
                int y = startY;
                for (int i = 0; i < characters.Length; i++)
                {
                    if (IsOnScreen(x, y))
                    {
                        Draw(characters[i], x, y);
                        x += 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public abstract void UpdateBuffer();

        public void WriteBuffer()
        {
            Console.SetCursorPosition(0, 0);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < FrameBuffer.Length; i++)
            {   
                sb.Append(FrameBuffer[i]);
                sb.Append(Environment.NewLine);
            }
            Console.Write(sb.ToString()); // single Console.Write call

            // alt render method that could support changing colors between chars - TOO SLOW THOUGH :(
            //for (int i = 0; i < FrameBuffer.Length; i++)
            //{
            //    for (int j = 0; j < FrameBuffer[i].Length; j++)
            //    {
            //        if ((i + j) % 2 == 0)
            //        {
            //            Console.ForegroundColor = ConsoleColor.White;
            //        }
            //        else
            //        {
            //            Console.ForegroundColor = ConsoleColor.Red;
            //        }
            //        Console.Write(FrameBuffer[i][j]);
            //        Console.SetCursorPosition(j + 1, i);
            //    }
            //    Console.SetCursorPosition(0, i + 1);
            //}

        }

        private bool IsOnScreen(int x, int y)
        {
            return x >= 0 && x < ScreenX && y >= 0 && y < ScreenY;
        }
    }
}
