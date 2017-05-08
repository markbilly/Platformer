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
        private IList<Entity> _entities;
        private IList<Entity> _rigidBodyEntites;

        public Scene()
        {
            _entities = new List<Entity>();
            _rigidBodyEntites = new List<Entity>();
        }

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            foreach (var entity in _entities)
            {
                entity.Load(contentManager, spriteBatch);
            }
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);

            if (entity.GetComponent<RigidBodyComponent>() != null)
            {
                _rigidBodyEntites.Add(entity);
            }
        }

        public void Update()
        {
            foreach (var entity in _entities)
            {
                // give entity relevant other entities for collision
                var rigidBodyComponent = entity.GetComponent<RigidBodyComponent>();
                if (rigidBodyComponent != null)
                {
                    // TODO: only pass rigid bodies near to the entity
                    rigidBodyComponent.NearbyEntities = _rigidBodyEntites;
                }

                // update entity
                entity.Update();
            }
        }

        public void Draw()
        {
            foreach (var entity in _entities)
            {
                entity.Draw();
            }
        }
    }
}
