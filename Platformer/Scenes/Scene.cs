using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Core;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Scenes
{
    public class Scene
    {
        private static readonly Vector2 GRAVITY = new Vector2(0, 10f);

        private IList<Entity> _entities;
        private IList<Entity> _collisionEntites;

        public Scene()
        {
            _entities = new List<Entity>();
            _collisionEntites = new List<Entity>();
        }

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            foreach (var entity in _entities)
            {
                entity.Load(contentManager, spriteBatch, graphicsDevice);
            }
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);

            if (entity.GetComponent<RigidBodyComponent>() != null)
            {
                _collisionEntites.Add(entity);
            }

            var forceComponent = entity.GetComponent<ApplyForceComponent>();
            if (forceComponent != null)
            {
                forceComponent.ApplyConstantForce("gravity", GRAVITY);
            }
        }

        public void Update()
        {
            foreach (var entity in _entities)
            {
                // give entity relevant other entities for collision
                var collisionComponent = entity.GetComponent<CollisionComponent>();
                if (collisionComponent != null)
                {
                    // TODO: only pass rigid bodies near to the entity
                    // TODO: just generally do this stuff more efficiently
                    collisionComponent.SetNearbyEntities(_collisionEntites);
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
