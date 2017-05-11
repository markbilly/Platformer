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
        private IList<GraphicsComponentBase> _graphicsComponents;
        
        public Entity(Point size, IEnumerable<IComponent> components)
        {
            Size = size;

            _components = new List<IComponent>();
            _graphicsComponents = new List<GraphicsComponentBase>();

            foreach (var component in components)
            {
                AddComponent(component);
            }

#if DEBUG
            AddComponent(new DebugGraphicsComponent());
#endif

        }

        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public Point Size { get; set; }

        public void AddComponent(IComponent component)
        {
            if (component is GraphicsComponentBase)
            {
                _graphicsComponents.Add((GraphicsComponentBase)component);
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
            foreach (var graphicsComponent in _graphicsComponents)
            {
                graphicsComponent.SpriteBatch = spriteBatch;
                graphicsComponent.GraphicsDevice = graphicsDevice;

                graphicsComponent.Load(contentManager);
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
            foreach (var graphicsComponent in _graphicsComponents)
            {
                graphicsComponent.Update(this);
            }
        }
    }
}
