using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace LeighzerConsoleGameEngine.CoreEngine
{
    public abstract class Engine
    { 
        private Stopwatch TickStopWatch { get; set; } // local timer for within tick
        private Stopwatch DeltaStopWatch { get; set; } // timer for time elapsed between game updates
        private double TickTimeElapsed { get; set; }     
        private int TickRate { get; set; } // ticks per second
        private double TickTime { get; set; } // seconds per tick
        private TimeSpan TickTimeSpan { get; set; } // seconds per tick as TimeSpan
        protected bool IsPlaying { get; set; }
        protected InputGatherer InputGatherer { get; set; }
        protected Renderer Renderer { get; set; }
        protected GameState GameState { get; set; }

        public Engine(int tickRate)
        {
            this.TickStopWatch = new Stopwatch();
            this.DeltaStopWatch = new Stopwatch();
            this.TickTimeElapsed = 0;
            this.TickRate = tickRate;
            this.TickTime = 1d / tickRate;
            this.TickTimeSpan = TimeSpan.FromSeconds(this.TickTime);
            this.IsPlaying = true;
            InputGatherer = new InputGatherer();
        }

        public void Init()
        {
            Console.CursorVisible = false;
        }

        public void Loop()
        {   
            while (IsPlaying)
            {
                TickStopWatch.Restart();
                TickTimeElapsed = 0;

                // gather input
                InputGatherer.ClearInput();
                InputGatherer.TryGatherInput();
                // update game state from previous state + new input
                GameState.Tick(InputGatherer.KeyPressBuffer, DeltaStopWatch.Elapsed.TotalSeconds);
                DeltaStopWatch.Restart();

                Renderer.Clear();
                // update frame buffer in renderer using gamestate
                Renderer.UpdateBuffer();
                Renderer.WriteBuffer();

                // wait if over with update                
                TickTimeElapsed = TickStopWatch.Elapsed.TotalSeconds;                
                while (TickTimeElapsed < TickTime)
                {
                    TickTimeElapsed = TickStopWatch.Elapsed.TotalSeconds;
                }
            }
        }

        //public abstract void Unload();
        public void Start()
        {
            Init();
            Loop();
            //Unload();
        }
    }
}
