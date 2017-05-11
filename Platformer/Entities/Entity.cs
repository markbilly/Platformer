using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities
{
    public class Entity
    {
        private IList<IComponent> _components;
        private SpriteComponent _spriteComponent;

        public Entity()
        {
            _components = new List<IComponent>();
        }

        public Entity(Point size, params IComponent[] components)
        {
            Size = size;

            _components = new List<IComponent>();
            foreach (var component in components)
            {
                AddComponent(component);
            }
        }

        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public Point Size { get; set; }

        public void AddComponent(IComponent component)
        {
            if (component is SpriteComponent)
            {
                _spriteComponent = (SpriteComponent)component;
                return;
            }

            _components.Add(component);
        }

        public T GetComponent<T>() where T : IComponent
        {
            return (T)_components.SingleOrDefault(x => x.GetType() == typeof(T));
        }

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (_spriteComponent != null)
            {
                _spriteComponent.SpriteBatch = spriteBatch;
                _spriteComponent.GraphicsDevice = graphicsDevice;

                _spriteComponent.Load(contentManager);
            }
        }

        public void Update()
        {
            Position += Velocity; // TODO: move velocity into component?

            foreach (var component in _components)
            {
                component.Update(this);
            }
        }

        public void Draw()
        {
            if (_spriteComponent != null)
            {
                _spriteComponent.Update(this);
            }
        }
    }
}
