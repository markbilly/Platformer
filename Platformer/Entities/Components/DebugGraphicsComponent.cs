using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer.Entities.Components
{
    public class DebugGraphicsComponent : GraphicsComponentBase
    {
        private Texture2D _debugPixel;
        private SpriteFont _debugFont;

        public override void Load(ContentManager contentManager)
        {
            _debugFont = contentManager.Load<SpriteFont>("fonts/debug");
            _debugPixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _debugPixel.SetData(new[] { Color.White });
        }

        public override void Update(Entity entity)
        {
            var width = entity.Size.X;
            var height = entity.Size.Y;

            var scaledLocation = (entity.Position * Constants.Game.Scale).ToPoint();
            var scaledWidth = width * Constants.Game.Scale;
            var scaledHeight = height * Constants.Game.Scale;
            var destinationRect = new Rectangle(scaledLocation.X, scaledLocation.Y, scaledWidth, scaledHeight);

            DrawDebugBorder(destinationRect);
            DrawDebugPosition(entity.Position);
        }

        private void DrawDebugPosition(Vector2 position)
        {
            var scaledPosition = position * Constants.Game.Scale;
            SpriteBatch.DrawString(_debugFont, "{x:" + position.X + ",y:" + position.Y + "}", scaledPosition, Color.Red);
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
