using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Input
{
    public class InputSystem : ISystem<IInputComponent>
    {
        private readonly IList<IInputComponent> _components;

        public InputSystem()
        {
            _components = new List<IInputComponent>();
        }

        public void RegisterComponent(IInputComponent component)
        {
            _components.Add(component);
        }

        public void Update()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }
    }
}
