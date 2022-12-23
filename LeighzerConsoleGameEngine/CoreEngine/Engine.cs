using System;
using System.Diagnostics;

namespace LeighzerConsoleGameEngine.CoreEngine
{
    public abstract class Engine
    {
        private Stopwatch _tickStopWatch { get; set; } // local timer for within tick
        private Stopwatch _deltaStopWatch { get; set; } // timer for time elapsed between game updates
        private double _tickTimeElapsed { get; set; }
        private int _tickRate { get; set; } // ticks per second
        private double _tickTime { get; set; } // seconds per tick
        private TimeSpan _tickTimeSpan { get; set; } // seconds per tick as TimeSpan
        protected bool IsPlaying { get; set; }
        protected InputGatherer InputGatherer { get; set; }
        protected Renderer Renderer { get; set; }
        protected GameState GameState { get; set; }

        public Engine(int tickRate)
        {
            this._tickStopWatch = new Stopwatch();
            this._deltaStopWatch = new Stopwatch();
            this._tickTimeElapsed = 0;
            this._tickRate = tickRate;
            this._tickTime = 1d / tickRate;
            this._tickTimeSpan = TimeSpan.FromSeconds(this._tickTime);
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
                _tickStopWatch.Restart();
                _tickTimeElapsed = 0;

                // gather input
                InputGatherer.ClearInput();
                InputGatherer.TryGatherInput();
                // update game state from previous state + new input
                GameState.Tick(InputGatherer.KeyPressBuffer, _deltaStopWatch.Elapsed.TotalSeconds);
                _deltaStopWatch.Restart();

                Renderer.Clear();
                // update frame buffer in renderer using gamestate
                Renderer.UpdateBuffer();
                Renderer.WriteBuffer();

                // wait if over with update                
                _tickTimeElapsed = _tickStopWatch.Elapsed.TotalSeconds;
                while (_tickTimeElapsed < _tickTime)
                {
                    _tickTimeElapsed = _tickStopWatch.Elapsed.TotalSeconds;
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
