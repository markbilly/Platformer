using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class AnimationParameters
    {
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        public int SpritesheetRow { get; set; }
        public int FramesPerSecond { get; set; }
    }

    public class AnimateComponent : IComponent
    {
        private int _ticks;
        private bool _isRunning;
        private int _remainingLoops = -1;

        private int _fps;
        private int _startFrame;
        private int _frames;
        private SpriteComponent _spriteComponent;

        public AnimateComponent(SpriteComponent spriteComponent, AnimationParameters parameters)
        {
            _fps = parameters.FramesPerSecond;
            _startFrame = parameters.StartFrame;
            _frames = parameters.EndFrame - parameters.StartFrame;

            _spriteComponent = spriteComponent;
            _spriteComponent.Frame = parameters.StartFrame;
            _spriteComponent.SpritesheetRow = parameters.SpritesheetRow;
        }

        public void Pause() => _isRunning = false;
        public void Stop()
        {
            _isRunning = false;
            _spriteComponent.Frame = _startFrame;
        }

        /// <summary>
        /// </summary>
        /// <param name="loopsBeforeStop">
        /// How many loops of the animation to execute before stopping.
        /// Use -1 to play animation indefinitely.
        /// </param>
        public void Play(int loopsBeforeStop = -1)
        {
            _isRunning = true;
        }

        public void Update()
        {
            if (_remainingLoops == 0 || !_isRunning)
            {
                return;
            }

            _ticks++;
            if (_ticks == (60 / _fps))
            {
                _spriteComponent.Frame++;

                // new loop
                if (_spriteComponent.Frame == _frames)
                {
                    _spriteComponent.Frame = _startFrame;
                    if (_remainingLoops > 0 && !IsLoopingIndefinitely())
                    {
                        _remainingLoops--;
                    }
                }

                _ticks = 0;
            }
        }

        private bool IsLoopingIndefinitely()
        {
            return _remainingLoops == -1;
        }
    }
}
