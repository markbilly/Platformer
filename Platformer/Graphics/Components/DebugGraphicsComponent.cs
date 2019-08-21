using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.Physics.Components;
using Platformer.Entities;

namespace Platformer.Graphics.Components
{
    public class DebugGraphicsComponent : GraphicsComponent
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
            //DrawDebugPosition(entity.Position);
            //DrawDebugVelocity(entity);
            DrawDebugCollision(entity);
        }

        private void DrawDebugCollision(Entity entity)
        {
            var collisions = entity.GetComponent<CollisionComponent>().GetCollisions();
            if (collisions.Any() && entity.GetType() == typeof(PlayerEntity))
            {
                var scaledPosition = new Vector2(entity.Position.X + entity.Size.X, (entity.Position.Y - (entity.Size.Y * 0.5f))) * Constants.Game.Scale;

                foreach (var collision in collisions)
                {
                    scaledPosition = new Vector2(scaledPosition.X, scaledPosition.Y + 30);
                    SpriteBatch.DrawString(_debugFont, "{x:" + entity.Position.X + ",y:" + entity.Position.Y + "}", scaledPosition, Color.Green);
                }
            }
        }

        private void DrawDebugPosition(Vector2 position)
        {
            var scaledPosition = position * Constants.Game.Scale;
            SpriteBatch.DrawString(_debugFont, "{x:" + position.X + ",y:" + position.Y + "}", scaledPosition, Color.Red);
        }

        private void DrawDebugVelocity(Entity entity)
        {
            var scaledPosition = new Vector2(entity.Position.X, entity.Position.Y + 8) * Constants.Game.Scale;
            SpriteBatch.DrawString(_debugFont, "{x:" + entity.Velocity.X + ",y:" + entity.Velocity.Y + "}", scaledPosition, Color.Blue);
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
