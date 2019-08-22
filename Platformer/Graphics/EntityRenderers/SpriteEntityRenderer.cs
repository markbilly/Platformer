using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Core;
using Platformer.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Graphics.EntityRenderers
{
    // TODO: Really, this depends on a "bounding box" and a "sprite" and does not need to know about the concept of an entity.
    //
    // Propose two properties on "Entity" - "BoundingBoxData" and "SpriteData", thus removing need for "SpriteComponent" as it is just a bag of data

    public class SpriteEntityRenderer : EntityRenderer
    {
        private readonly SpriteComponent _spriteComponent;

        private Texture2D _texture;

        public SpriteEntityRenderer(SpriteComponent spriteComponent)
        {
            _spriteComponent = spriteComponent;
        }

        public override void Load(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>(_spriteComponent.Spritesheet);
        }

        public override void Render(Entity entity)
        {
            if (_texture == null)
            {
                throw new InvalidOperationException("Texture not yet loaded");
            }

            var width = entity.Size.X;
            var height = entity.Size.Y;

            var pixelPerfectEntityPosition = new Vector2((float)Math.Round(entity.Position.X), (float)Math.Round(entity.Position.Y));

            var sourceRect = new Rectangle(width * _spriteComponent.SpritesheetFrame, height * _spriteComponent.SpritesheetRow, width, height);
            var scaledPosition = pixelPerfectEntityPosition * Constants.Game.Scale;

            SpriteBatch.Draw(
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
