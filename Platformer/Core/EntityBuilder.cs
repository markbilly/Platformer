using Platformer.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public class EntityBuilder
    {
        private readonly ComponentContainer _container;
        private readonly HashSet<Type> _componentTypes;

        public EntityBuilder()
        {
            _container = new ComponentContainer();
            _componentTypes = new HashSet<Type>();
        }

        public EntityBuilder WithComponent(Type componentType)
        {
            if (!componentType.GetInterfaces().Contains(typeof(IComponent)))
            {
                throw new ArgumentException(nameof(componentType), "Type provided does not implement " + nameof(IComponent));
            }

            _componentTypes.Add(componentType);

            return this;
        }

        public Entity Build()
        {
            var components = new List<IComponent>();

            foreach (var type in _componentTypes)
            {
                components.Add((IComponent)_container.Resolve(type));
            }

#if DEBUG
            components.Add((IComponent)_container.Resolve(typeof(DebugGraphicsComponent)));
#endif

            return new Entity(components);
        }
    }
}
