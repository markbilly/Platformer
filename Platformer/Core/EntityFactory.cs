using Platformer.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public class EntityFactory
    {
        private readonly Container<IComponent> _container;
        private readonly Type[] _componentTypes;

        public EntityFactory(Type[] componentTypes)
        {
            _container = new Container<IComponent>();
            _componentTypes = componentTypes;
        }

        public virtual Entity Build()
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
