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
    public class DebugGraphicsComponent : IGraphicsComponent
    {
        private readonly CollisionComponent _collisionComponent;
        private readonly VelocityComponent _velocityComponent;
        private readonly PositionComponent _positionComponent;
        private readonly SizeComponent _sizeComponent;
        private SpriteBatch _spriteBatch;
        private Texture2D _debugPixel;
        private SpriteFont _debugFont;

        public DebugGraphicsComponent(SizeComponent sizeComponent, PositionComponent positionComponent, VelocityComponent velocityComponent, CollisionComponent collisionComponent)
        {
            _velocityComponent = velocityComponent;
            _collisionComponent = collisionComponent;
            _positionComponent = positionComponent;
            _sizeComponent = sizeComponent;
        }

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _debugFont = contentManager.Load<SpriteFont>("fonts/debug");
            _debugPixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _debugPixel.SetData(new[] { Color.White });
        }

        public void Update()
        {
            var width = _sizeComponent.Size.X;
            var height = _sizeComponent.Size.Y;

            var scaledLocation = (_positionComponent.Position * Constants.Game.Scale).ToPoint();
            var scaledWidth = width * Constants.Game.Scale;
            var scaledHeight = height * Constants.Game.Scale;
            var destinationRect = new Rectangle(scaledLocation.X, scaledLocation.Y, scaledWidth, scaledHeight);

            //DrawDebugBorder(destinationRect);
            DrawDebugPosition();
            DrawDebugVelocity();
            //DrawDebugCollision();
        }

        private void DrawDebugCollision()
        {
            var collisions = _collisionComponent.GetCollisions();
            if (collisions.Any())
            {
                var scaledPosition = new Vector2(_positionComponent.Position.X + _sizeComponent.Size.X, (_positionComponent.Position.Y - (_sizeComponent.Size.Y * 0.5f))) * Constants.Game.Scale;

                foreach (var collision in collisions)
                {
                    scaledPosition = new Vector2(scaledPosition.X, scaledPosition.Y + 30);
                    _spriteBatch.DrawString(_debugFont, "{x:" + _positionComponent.Position.X + ",y:" + _positionComponent.Position.Y + "}", scaledPosition, Color.Green);
                }
            }
        }

        private void DrawDebugPosition()
        {
            var scaledPosition = _positionComponent.Position * Constants.Game.Scale;
            _spriteBatch.DrawString(_debugFont, "{x:" + _positionComponent.Position.X + ",y:" + _positionComponent.Position.Y + "}", scaledPosition, Color.Red);
        }

        private void DrawDebugVelocity()
        {
            var scaledPosition = new Vector2(_positionComponent.Position.X, _positionComponent.Position.Y + 8) * Constants.Game.Scale;
            _spriteBatch.DrawString(_debugFont, "{x:" + _velocityComponent.Velocity.X + ",y:" + _velocityComponent.Velocity.Y + "}", scaledPosition, Color.Blue);
        }

        private void DrawDebugBorder(Rectangle rectangleToDraw)
        {
            var thicknessOfBorder = 1;
            var borderColor = Color.Red;

            // Draw top line
            _spriteBatch.Draw(_debugPixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            _spriteBatch.Draw(_debugPixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            _spriteBatch.Draw(_debugPixel, new Rectangle(
                (rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                rectangleToDraw.Y,
                thicknessOfBorder,
                rectangleToDraw.Height), borderColor);

            // Draw bottom line
            _spriteBatch.Draw(_debugPixel, new Rectangle(
                rectangleToDraw.X,
                rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                rectangleToDraw.Width,
                thicknessOfBorder), borderColor);
        }
    }
}
