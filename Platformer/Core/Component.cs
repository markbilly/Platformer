using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public abstract class Component
    {
        protected Component(ComponentType type)
        {
            Type = type;
        }

        public ComponentType Type { get; }

        public abstract void Update(Entity entity);
    }
}
