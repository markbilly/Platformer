using Microsoft.Xna.Framework.Graphics;
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
            ForEach(x => x.Update());
        }

        public void Draw()
        {
            ForEach(x => x.Draw());
        }
    }
}
