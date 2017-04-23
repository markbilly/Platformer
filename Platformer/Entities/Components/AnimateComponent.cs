using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class AnimateComponent : IComponent
    {
        private int _ticks;
        private bool _isRunning;
        private int _remainingLoops = -1;

        private int _fps;
        private int _startFrame;
        private int _frames;
        private SpriteComponent _spriteComponent;

        public AnimateComponent(SpriteComponent spriteComponent, int startFrame, int endFrame, int fps)
        {
            _fps = fps;
            _startFrame = startFrame;
            _frames = endFrame - startFrame;
            _spriteComponent = spriteComponent;

            _spriteComponent.Frame = _startFrame;
        }

        public void Pause() => _isRunning = false;
        public void Stop()
        {
            _isRunning = false;
            _spriteComponent.Frame = _startFrame;
        }

        public void Play(int loops = -1)
        {
            _remainingLoops = loops; // -1 loops is indefinite
            _isRunning = true;
        }

        public void Draw()
        {
            // Depend on sprite component for draw
            return;
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
