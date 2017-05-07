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
        private IList<SpriteComponent> _spriteComponents;

        public Scene()
        {
            _spriteComponents = new List<SpriteComponent>();
            _entities = new List<Entity>();
        }

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            foreach (var spriteComponent in _spriteComponents)
            {
                spriteComponent.SpriteBatch = spriteBatch;
                spriteComponent.Load(contentManager);
            }
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);

            var spriteComponent = entity.GetComponent<SpriteComponent>();
            if (spriteComponent != null)
            {
                _spriteComponents.Add(spriteComponent);
            }
        }

        public void Update()
        {
            // handle collisions
            HandleCollisions();

            // update entities
            foreach (var entity in _entities)
            {
                entity.Update();
            }
        }

        public void Draw()
        {
            foreach (var spriteComponent in _spriteComponents)
            {
                spriteComponent.Update();
            }
        }

        private void HandleCollisions()
        {
            foreach (var entity1 in _entities)
            {
                var entity1Position = entity1.GetComponent<PositionComponent>();
                var entity1RigidBody = entity1.GetComponent<RigidBodyComponent>();

                if (entity1Position != null && entity1RigidBody != null)
                {
                    foreach (var entity2 in _entities)
                    {
                        if (entity1 != entity2)
                        {
                            var entity2Position = entity2.GetComponent<PositionComponent>();
                            var entity2RigidBody = entity2.GetComponent<RigidBodyComponent>();

                            if (entity2Position != null && entity2RigidBody != null)
                            {
                                var entity1Bounds = new Rectangle(entity1Position.Position.ToPoint(), entity1RigidBody.BoundingBoxSize);
                                var entity2Bounds = new Rectangle(entity2Position.Position.ToPoint(), entity2RigidBody.BoundingBoxSize);

                                if (entity1Bounds.Intersects(entity2Bounds))
                                {
                                    entity1RigidBody.Collision = new Vector2(1, 0);
                                    entity2RigidBody.Collision = new Vector2(1, 0);
                                }
                                else
                                {
                                    entity1RigidBody.Collision = Vector2.Zero;
                                    entity2RigidBody.Collision = Vector2.Zero;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
