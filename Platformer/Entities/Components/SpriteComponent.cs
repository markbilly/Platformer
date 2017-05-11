using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class SpriteComponent : IComponent
    {
        private Texture2D _texture;
        private Texture2D _debugPixel;
        private string _spritesheet;
        
        public SpriteComponent(string spritesheet)
        {
            _spritesheet = spritesheet;
        }

        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        /// Which frame on the spritesheet to display (zero-indexed)
        /// </summary>
        public int Frame { get; set; }

        /// <summary>
        /// Which row to use to display the frame (zero-indexed)
        /// An animation can be stored on each row, for example
        /// </summary>
        public int SpritesheetRow { get; set; }

        public void Load(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>(_spritesheet);

            _debugPixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _debugPixel.SetData(new[] { Color.White });
        }

        public void Update(Entity entity)
        {
            if (_texture == null)
            {
                throw new InvalidOperationException("Texture not yet loaded");
            }

            var width = entity.Size.X;
            var height = entity.Size.Y;
            var sourceRect = new Rectangle(width * Frame, height * SpritesheetRow, width, height);

            var scaledLocation = (entity.Position * Constants.Game.Scale).ToPoint();
            var scaledWidth = width * Constants.Game.Scale;
            var scaledHeight = height * Constants.Game.Scale;
            var destinationRect = new Rectangle(scaledLocation.X, scaledLocation.Y, scaledWidth, scaledHeight);

            SpriteBatch.Draw(_texture, destinationRect, sourceRect, Color.White);
            DrawDebugBorder(destinationRect);
        }

        private void DrawDebugBorder(Rectangle rectangleToDraw)
        {
            var thicknessOfBorder = 1;
            var borderColor = Color.Red;

            // Draw top line
            SpriteBatch.Draw(_debugPixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            SpriteBatch.Draw(_debugPixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            SpriteBatch.Draw(_debugPixel, new Rectangle(
                (rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                rectangleToDraw.Y,
                thicknessOfBorder,
                rectangleToDraw.Height), borderColor);

            // Draw bottom line
            SpriteBatch.Draw(_debugPixel, new Rectangle(
                rectangleToDraw.X,
                rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                rectangleToDraw.Width,
                thicknessOfBorder), borderColor);
        }
    }
}
