using Platformer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Scenes
{
    public class Scene
    {
        public Scene()
        {
            Entities = new List<Entity>();
        }

        public IList<Entity> Entities { get; set; }
    }
}
