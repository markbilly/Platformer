using Platformer.Core;
using Platformer.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Animation.Components
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

        public static AnimationParameters Default()
        {
            return new AnimationParameters
            {
                StartFrame = 0,
                EndFrame = 8,
                FramesPerSecond = 4,
            };
        }
    }

    public class AnimateComponent : IComponent
    {
        private SpriteGraphicsComponent _spriteComponent;

        private int _ticks;
        private int _fps;
        private int _startFrame;
        private int _frames;

        public AnimateComponent(AnimationParameters parameters)
        {
            // TODO: Do not pass these in on construction
            _fps = parameters.FramesPerSecond;
            _startFrame = parameters.StartFrame;
            _frames = parameters.EndFrame - parameters.StartFrame;
        }

        public void SetAnimation(Animations animation)
        {
            _spriteComponent.SpritesheetRow = (int)animation;
        }

        public void Update(Entity entity)
        {
            GatherDependencies(entity);

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

        private void GatherDependencies(Entity entity)
        {
            if (_spriteComponent == null)
            {
                _spriteComponent = entity.GetGraphicsComponent<SpriteGraphicsComponent>();
                _spriteComponent.SpritesheetFrame = _startFrame; // TODO: remove this
            }
        }
    }
}
