using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities
{
    public interface IComponent
    {
        int Order { get; }
        void Update(Entity entity);
    }
}
