using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public abstract class Component
    {
        protected Component(ComponentType type, Entity entity = null)
        {
            Entity = entity;
            Type = type;
        }

        protected Entity Entity { get; }
        public ComponentType Type { get; }

        public abstract void Update(Entity entity);
    }
}
