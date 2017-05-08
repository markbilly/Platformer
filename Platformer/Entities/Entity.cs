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

        public Entity()
        {
            _components = new List<IComponent>();
        }

        public Entity(Vector2 position, Point size, params IComponent[] components)
        {
            Position = position;
            Size = size;
            _components = components;
        }
        
        public Vector2 Position { get; set; }
        public Point Size { get; set; }

        public void AddComponent(IComponent component)
        {
            _components.Add(component);
        }

        public T GetComponent<T>() where T : IComponent
        {
            return (T)_components.SingleOrDefault(x => x.GetType() == typeof(T));
        }

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
            {
                if (component.GetType() == typeof(SpriteComponent))
                {
                    ((SpriteComponent)component).SpriteBatch = spriteBatch;
                    ((SpriteComponent)component).Load(contentManager);
                    break;
                }
            }
        }

        public void Update()
        {
            foreach (var component in _components)
            {
                if (component.GetType() != typeof(SpriteComponent))
                {
                    component.Update(this);
                }
            }
        }

        public void Draw()
        {
            foreach (var component in _components)
            {
                if (component.GetType() == typeof(SpriteComponent))
                {
                    component.Update(this);
                    break;
                }
            }
        }
    }
}
