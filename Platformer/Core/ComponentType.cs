using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    /// <summary>
    /// This is purely to decide where in the execution order the component sits
    /// i.e. physics components executed first, graphics components last
    /// </summary>
    public enum ComponentType
    {
        Physics = 0,
        Input = 1,
        StateManagement = 2,
        Graphics = 3
    }
}
