using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public interface IComponent
    {
        void Update(Entity entity);
        ComponentType Type { get; }
    }
}
