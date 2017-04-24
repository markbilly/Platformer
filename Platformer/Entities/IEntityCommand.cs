using Platformer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities
{
    public interface IEntityCommand
    {
        void Execute(Entity entity);
    }
}
