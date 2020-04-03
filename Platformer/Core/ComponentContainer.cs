using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public class ComponentContainer
    {
        private readonly Dictionary<Type, IComponent> _instancesByType;
        
        public ComponentContainer()
        {
            _instancesByType = new Dictionary<Type, IComponent>();
        }

        public object Resolve(Type instanceType)
        {
            return GetOrCreateComponentInstance(instanceType);
        }

        public TInstance Resolve<TInstance>() where TInstance : IComponent
        {
            return (TInstance)GetOrCreateComponentInstance(typeof(TInstance));
        }

        private object GetOrCreateComponentInstance(Type instanceType)
        {
            if (_instancesByType.TryGetValue(instanceType, out IComponent instance))
            {
                return instance;
            }

            // TODO: Throw specific exceptions when 1) there is > 1 constructor and 2) any parameters are not sub class of type Component

            var ctor = instanceType.GetConstructors().Single();
            var parameterTypes = ctor.GetParameters().Select(p => p.ParameterType).Where(pt => pt.GetInterfaces().Contains(typeof(IComponent)));
            var dependencies = parameterTypes.Select(pt => GetOrCreateComponentInstance(pt)).ToArray();

            instance = (IComponent)Activator.CreateInstance(instanceType, dependencies);

            _instancesByType.Add(instanceType, instance);

            return instance;
        }
    }
}
