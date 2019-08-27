using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.GameLogic
{
    public class GameLogicSystem : ISystem<IGameLogicComponent>
    {
        private readonly IList<IGameLogicComponent> _components;

        public GameLogicSystem()
        {
            _components = new List<IGameLogicComponent>();
        }

        public void RegisterComponent(IGameLogicComponent component)
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
