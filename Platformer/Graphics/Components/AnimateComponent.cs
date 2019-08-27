using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Core;
using Platformer.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Graphics.Components
{
    public class AnimateComponent : IGraphicsComponent
    {
        const int DEFAULT_START_FRAME = 0;
        const int DEFAULT_END_FRAME = 8;
        const int DEFAULT_FPS = 4;

        private readonly SpriteComponent _spriteComponent;

        private readonly int _fps = DEFAULT_FPS;
        private readonly int _startFrame = DEFAULT_START_FRAME;
        private readonly int _frames = DEFAULT_END_FRAME - DEFAULT_START_FRAME;

        private int _ticks;

        public AnimateComponent(SpriteComponent spriteComponent)
        {
            _spriteComponent = spriteComponent;
            _spriteComponent.FrameX = _startFrame;
        }

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) { }

        public void SetAnimation(Animations animation)
        {
            _spriteComponent.FrameY = (int)animation;
        }

        public void Update()
        {
            _ticks++;
            if (_ticks == (60 / _fps))
            {
                _spriteComponent.FrameX++;

                if (_spriteComponent.FrameX == _frames)
                {
                    _spriteComponent.FrameX = _startFrame;
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
