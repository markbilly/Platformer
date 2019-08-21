using Platformer.Core;
using Platformer.Graphics.Components;
using Platformer.Graphics.GraphicsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Graphics.Components
{
    public class AnimateComponent : Component
    {
        const int DEFAULT_START_FRAME = 0;
        const int DEFAULT_END_FRAME = 8;
        const int DEFAULT_FPS = 4;

        private readonly SpriteGraphicsComponent _spriteGraphicsComponent;

        private readonly int _fps = DEFAULT_FPS;
        private readonly int _startFrame = DEFAULT_START_FRAME;
        private readonly int _frames = DEFAULT_END_FRAME - DEFAULT_START_FRAME;

        private int _ticks;

        public AnimateComponent(SpriteGraphicsComponent spriteGraphicsComponent) 
            : base(ComponentType.Graphics)
        {
            _spriteGraphicsComponent = spriteGraphicsComponent;
            _spriteGraphicsComponent.SpritesheetFrame = _startFrame;
        }

        public void SetAnimation(Animations animation)
        {
            _spriteGraphicsComponent.SpritesheetRow = (int)animation;
        }

        public override void Update(Entity entity)
        {
            _ticks++;
            if (_ticks == (60 / _fps))
            {
                _spriteGraphicsComponent.SpritesheetFrame++;

                if (_spriteGraphicsComponent.SpritesheetFrame == _frames)
                {
                    _spriteGraphicsComponent.SpritesheetFrame = _startFrame;
                }

                _ticks = 0;
            }
        }
    }

    public enum Animations
    {
        IdleRight = 0,
        IdleLeft = 1,
        MoveRight = 2,
        MoveLeft = 3
    }
}
