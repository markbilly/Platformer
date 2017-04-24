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
        private Point _extent;
        private Texture2D _texture;
        private string _spritesheet;

        private PositionComponent _positionComponent;
        
        public SpriteComponent(PositionComponent positionComponent, Point extent, string spritesheet)
        {
            _positionComponent = positionComponent;
            _extent = extent;
            _spritesheet = spritesheet;
        }

        public SpriteBatch SpriteBatch { get; set; }

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
        }

        public void Update()
        {
            return;
        }

        public void Draw()
        {
            if (_texture == null)
            {
                throw new InvalidOperationException("Texture not yet loaded");
            }

            var width = _extent.X;
            var height = _extent.Y;
            var sourceRect = new Rectangle(width * Frame, height * SpritesheetRow, width, height);

            var scaledLocation = (_positionComponent.Position * Constants.GameScale).ToPoint();
            var scaledWidth = width * Constants.GameScale;
            var scaledHeight = height * Constants.GameScale;
            var destinationRect = new Rectangle(scaledLocation.X, scaledLocation.Y, scaledWidth, scaledHeight);

            SpriteBatch.Draw(_texture, destinationRect, sourceRect, Color.White);
        }
    }
}
