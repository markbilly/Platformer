using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public class Container<T>
    {
        private readonly Dictionary<Type, T> _instancesByType;
        
        public Container()
        {
            _instancesByType = new Dictionary<Type, T>();
        }

        public object Resolve(Type instanceType)
        {
            return GetOrCreateComponentInstance(instanceType);
        }

        public TInstance Resolve<TInstance>() where TInstance : T
        {
            return (TInstance)GetOrCreateComponentInstance(typeof(TInstance));
        }

        private object GetOrCreateComponentInstance(Type instanceType)
        {
            T instance = default;

            if (_instancesByType.TryGetValue(instanceType, out instance))
            {
                return instance;
            }

            // TODO: Throw specific exceptions when 1) there is > 1 constructor and 2) any parameters are not sub class of type Component

            var ctor = instanceType.GetConstructors().Single();
            var parameterTypes = ctor.GetParameters().Select(p => p.ParameterType).Where(pt => pt.GetInterfaces().Contains(typeof(T)));
            var dependencies = parameterTypes.Select(pt => GetOrCreateComponentInstance(pt)).ToArray();

            instance = (T)Activator.CreateInstance(instanceType, dependencies);

            _instancesByType.Add(instanceType, instance);

            return instance;
        }
    }
}
