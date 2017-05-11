using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public enum Animations
    {
        IdleRight = 0,
        IdleLeft = 1,
        WalkRight = 2,
        WalkLeft = 3
    }

    public class AnimationParameters
    {
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        public int FramesPerSecond { get; set; }
    }

    public class AnimateComponent : IComponent
    {
        private int _ticks;
        private int _fps;
        private int _startFrame;
        private int _frames;
        private SpriteGraphicsComponent _spriteComponent;

        public AnimateComponent(SpriteGraphicsComponent spriteComponent, AnimationParameters parameters)
        {
            _fps = parameters.FramesPerSecond;
            _startFrame = parameters.StartFrame;
            _frames = parameters.EndFrame - parameters.StartFrame;

            _spriteComponent = spriteComponent;
            _spriteComponent.SpritesheetFrame = parameters.StartFrame;
        }

        public void SetAnimation(Animations animation)
        {
            _spriteComponent.SpritesheetRow = (int)animation;
        }

        public void Update(Entity entity)
        {
            _ticks++;
            if (_ticks == (60 / _fps))
            {
                _spriteComponent.SpritesheetFrame++;

                if (_spriteComponent.SpritesheetFrame == _frames)
                {
                    _spriteComponent.SpritesheetFrame = _startFrame;
                }

                _ticks = 0;
            }
        }
    }
}
