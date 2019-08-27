using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Core;
using Platformer.Physics.Components;

namespace Platformer.Graphics.Components
{
    public class SpriteComponent : IGraphicsComponent
    {
        private readonly PositionComponent _positionComponent;
        private readonly SizeComponent _sizeComponent;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;

        public SpriteComponent(SizeComponent sizeComponent, PositionComponent positionComponent)
        {
            _positionComponent = positionComponent;
            _sizeComponent = sizeComponent;
        }

        public string Spritesheet { get; set; }
        public int FrameX { get; set; }
        public int FrameY { get; set; }

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _texture = contentManager.Load<Texture2D>(Spritesheet);
        }

        public void Update()
        {
            if (_texture == null)
            {
                throw new InvalidOperationException("Texture not yet loaded");
            }

            var width = _sizeComponent.Size.X;
            var height = _sizeComponent.Size.Y;
            var pixelPerfectEntityPosition = new Vector2((float)Math.Round(_positionComponent.Position.X), (float)Math.Round(_positionComponent.Position.Y));

            var sourceRect = new Rectangle(width * FrameX, height * FrameY, width, height);
            var scaledPosition = pixelPerfectEntityPosition * Constants.Game.Scale;

            _spriteBatch.Draw(
                _texture,
                scaledPosition,
                sourceRect,
                Color.White,
                0f,
                new Vector2(0, 0),
                Constants.Game.Scale,
                SpriteEffects.None,
                0f);
        }
    }
}
