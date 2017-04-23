using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Entities;
using Platformer.Entities.Components;
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

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            foreach (var entity in Entities)
            {
                var spriteComponent = entity.GetComponent<SpriteComponent>();

                if (spriteComponent != null)
                {
                    spriteComponent.SpriteBatch = spriteBatch;
                    spriteComponent.Load(contentManager);
                }
            }
        }
    }
}
