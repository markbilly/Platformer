using Microsoft.Xna.Framework.Graphics;
using Platformer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities
{
    public class Entity : List<IComponent>
    {
        public T GetComponent<T>() where T : IComponent
        {
            return (T)this.SingleOrDefault(x => x.GetType() == typeof(T));
        }

        public void Update()
        {
            foreach (var component in this)
            {
                if (component.GetType() != typeof(SpriteComponent))
                {
                    component.Update();
                }
            }
        }
    }
}
