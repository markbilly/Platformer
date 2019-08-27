using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public interface ISystem<TComponent> where TComponent : IComponent
    {
        void Update();
        void RegisterComponent(TComponent component);
    }
}
