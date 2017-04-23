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
        private string _assetName;

        private PositionComponent _positionComponent;
        
        public SpriteComponent(PositionComponent positionComponent, Point extent, string assetName)
        {
            _positionComponent = positionComponent;
            _extent = extent;
            _assetName = assetName;
        }

        public SpriteBatch SpriteBatch { get; set; }

        public void Load(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>(_assetName);
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

            var location = _positionComponent.Position.ToPoint();

            SpriteBatch.Draw(_texture, new Rectangle(location, _extent), Color.White);
        }
    }
}
