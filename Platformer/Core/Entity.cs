using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Graphics;
using Platformer.Graphics.Components;
using Platformer.Graphics.EntityRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public abstract class Entity
    {
        private readonly Dictionary<Type, Component> _componentsByType;

        private IList<Component> _components;
        private IList<EntityRenderer> _entityRenderers;
        
        protected Entity(Point size)
        {
            Size = size;

            _componentsByType = new Dictionary<Type, Component>();

            _components = new List<Component>();
            _entityRenderers = new List<EntityRenderer>();

#if DEBUG
            _entityRenderers.Add(new DebugEntityRenderer());
#endif
        }

        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public Point Size { get; set; }

        public T GetComponent<T>() where T : Component
        {
            return (T)_components.SingleOrDefault(x => x.GetType() == typeof(T));
        }

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            foreach (var entityRenderer in _entityRenderers)
            {
                entityRenderer.SpriteBatch = spriteBatch;
                entityRenderer.GraphicsDevice = graphicsDevice;

                entityRenderer.Load(contentManager);
            }
        }

        public void Update()
        {
            Position += Velocity;

            foreach (var component in _components)
            {
                component.Update(this);
            }
        }

        public void Draw()
        {
            foreach (var entityRenderer in _entityRenderers)
            {
                entityRenderer.Render(this);
            }
        }

        protected void AddRenderer(EntityRenderer entityRenderer)
        {
            _entityRenderers.Add(entityRenderer);

#if DEBUG
            _entityRenderers = _entityRenderers.OrderBy(er => er is DebugEntityRenderer).ToList();
#endif
        }

        protected void AddComponent<TComponent>() where TComponent : Component
        {
            GetOrCreateComponentInstance(typeof(TComponent));

            _components = _componentsByType.Values.OrderBy(c => (int)c.Type).ToList();
        }

        private object GetOrCreateComponentInstance(Type componentType)
        {
            if (_componentsByType.TryGetValue(componentType, out Component component))
            {
                return component;
            }

            // TODO: Throw specific exceptions when 1) there is > 1 constructor and 2) any parameters are not sub class of type Component

            var ctor = componentType.GetConstructors().Single();
            var parameterTypes = ctor.GetParameters().Select(p => p.ParameterType).Where(pt => pt.IsSubclassOf(typeof(Component)));
            var dependencies = parameterTypes.Select(pt => GetOrCreateComponentInstance(pt)).ToArray();
            var instance = Activator.CreateInstance(componentType, dependencies);

            _componentsByType.Add(componentType, (Component)instance);

            return instance;
        }
    }
}
