using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Graphics;
using Platformer.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public class Entity
    {
        private readonly IList<IComponent> _components;

        public Entity(IList<IComponent> components)
        {
            _components = components;
        }

        public IEnumerable<IComponent> Components => _components;

        public TComponent GetComponent<TComponent>() where TComponent : IComponent
        {
            return (TComponent)_components.SingleOrDefault(c => c.GetType() == typeof(TComponent));
        }
    }
}
