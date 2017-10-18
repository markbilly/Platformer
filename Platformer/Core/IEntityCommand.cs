using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public interface IEntityCommand
    {
        void Execute(Entity entity);
    }
}
