using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public abstract class Entity
    {
        private readonly Dictionary<Type, Component> _allComponentsByType;

        private IEnumerable<Component> _nonGraphicsComponents;
        private IEnumerable<GraphicsComponent> _graphicsComponents;
        
        protected Entity(Point size)
        {
            Size = size;

            _allComponentsByType = new Dictionary<Type, Component>();

            _nonGraphicsComponents = new List<Component>();
            _graphicsComponents = new List<GraphicsComponent>();

#if DEBUG
            AddComponent<DebugGraphicsComponent>();
#endif
        }

        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public Point Size { get; set; }

        public T GetComponent<T>() where T : Component
        {
            return (T)_nonGraphicsComponents.SingleOrDefault(x => x.GetType() == typeof(T));
        }

        public T GetGraphicsComponent<T>() where T : GraphicsComponent
        {
            return (T)_graphicsComponents.SingleOrDefault(x => x.GetType() == typeof(T));
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
            Position += Velocity;

            foreach (var component in _nonGraphicsComponents)
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

        protected void AddComponent<TComponent>() where TComponent : Component
        {
            var componentType = typeof(TComponent);

            _allComponentsByType.Add(componentType, (TComponent)GetOrCreateComponentInstance(componentType));

            _nonGraphicsComponents = _allComponentsByType
                .Where(x => !x.Key.IsSubclassOf(typeof(GraphicsComponent)))
                .Select(x => x.Value)
                .OrderBy(c => (int)c.Type);

            _graphicsComponents = _allComponentsByType
                .Where(x => x.Key.IsSubclassOf(typeof(GraphicsComponent)))
                .Select(x => x.Value)
#if DEBUG
                .OrderBy(c => c.GetType() == typeof(DebugGraphicsComponent))
#endif
                .Cast<GraphicsComponent>();
        }

        private object GetOrCreateComponentInstance(Type componentType)
        {
            if (_allComponentsByType.TryGetValue(componentType, out Component component))
            {
                return component;
            }

            // TODO: Throw specific exceptions when 1) there is > 1 constructor and 2) any parameters are not sub class of type Component

            var ctor = componentType.GetConstructors().Single();
            var parameterTypes = ctor.GetParameters().Select(p => p.ParameterType).Where(pt => pt.IsSubclassOf(typeof(Component)));
            var dependencies = parameterTypes.Select(pt => GetOrCreateComponentInstance(pt)).ToArray();

            return Activator.CreateInstance(componentType, dependencies);
        }
    }
}
