using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Graphics.GraphicsComponents
{
    public class SpriteGraphicsComponent : GraphicsComponent
    {
        private Texture2D _texture;

        // TODO: Have this set by inheritance
        public string Spritesheet { get; set; }

        /// <summary>
        /// Which frame on the spritesheet to display (zero-indexed)
        /// </summary>
        public int SpritesheetFrame { get; set; }

        /// <summary>
        /// Which row to use to display the frame (zero-indexed)
        /// An animation can be stored on each row, for example
        /// </summary>
        public int SpritesheetRow { get; set; }

        public override void Load(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>(Spritesheet);
        }

        public override void Update(Entity entity)
        {
            if (_texture == null)
            {
                throw new InvalidOperationException("Texture not yet loaded");
            }

            var width = entity.Size.X;
            var height = entity.Size.Y;

            var pixelPerfectEntityPosition = new Vector2((float)Math.Round(entity.Position.X), (float)Math.Round(entity.Position.Y));

            var sourceRect = new Rectangle(width * SpritesheetFrame, height * SpritesheetRow, width, height);
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
