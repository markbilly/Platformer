using Microsoft.Xna.Framework;
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

        public void HandleCollisions()
        {
            foreach (var entity in Entities)
            {
                var positionComponent = entity.GetComponent<PositionComponent>();
                var rigidBodyComponent = entity.GetComponent<RigidBodyComponent>();

                if (rigidBodyComponent != null && positionComponent != null)
                {
                    // Just check game boundaries for now
                    if (positionComponent.Position.X > Constants.Game.Width)
                    {
                        rigidBodyComponent.Collision = new Vector2(1, 0);
                    }
                    else if (positionComponent.Position.X < 0)
                    {
                        rigidBodyComponent.Collision = new Vector2(-1, 0);
                    }
                    else
                    {
                        rigidBodyComponent.Collision = Vector2.Zero;
                    }
                }
            }
        }

        public void UpdateEntites()
        {
            foreach (var entity in Entities)
            {
                entity.Update();
            }
        }
    }
}
