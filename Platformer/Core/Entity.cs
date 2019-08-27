using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Graphics;
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
        private readonly Dictionary<Type, IComponent> _componentsByType = new Dictionary<Type, IComponent>();

        protected Entity()
        {
#if DEBUG
            AddComponent<DebugGraphicsComponent>();
#endif
        }

        public IEnumerable<IComponent> Components => _componentsByType.Values;

        public TComponent GetComponent<TComponent>() where TComponent : IComponent
        {
            return _componentsByType.TryGetValue(typeof(TComponent), out IComponent component) ? (TComponent)component : default;
        }

        protected void AddComponent<TComponent>() where TComponent : IComponent
        {
            GetOrCreateComponentInstance(typeof(TComponent));
        }

        private object GetOrCreateComponentInstance(Type componentType)
        {
            if (_componentsByType.TryGetValue(componentType, out IComponent component))
            {
                return component;
            }

            // TODO: Throw specific exceptions when 1) there is > 1 constructor and 2) any parameters are not sub class of type Component

            var ctor = componentType.GetConstructors().Single();
            var parameterTypes = ctor.GetParameters().Select(p => p.ParameterType).Where(pt => pt.GetInterfaces().Contains(typeof(IComponent)));
            var dependencies = parameterTypes.Select(pt => GetOrCreateComponentInstance(pt)).ToArray();
            var instance = Activator.CreateInstance(componentType, dependencies);

            _componentsByType.Add(componentType, (IComponent)instance);

            return instance;
        }
    }
}
