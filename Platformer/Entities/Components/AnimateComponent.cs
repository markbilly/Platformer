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

        public void Stop()
        {
            _isRunning = false;
            _spriteComponent.Frame = _startFrame;
        }

        public void Pause() => _isRunning = false;
        public void Play() => _isRunning = true;

        public void Draw()
        {
            // Depend on sprite component for draw
            return;
        }

        public void Update()
        {
            if (!_isRunning)
            {
                return;
            }

            _ticks++;
            if (_ticks == (60 / _fps))
            {
                _spriteComponent.Frame++;
                if (_spriteComponent.Frame == _frames)
                    _spriteComponent.Frame = 0;
                _ticks = 0;
            }
        }
    }
}
