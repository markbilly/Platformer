using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.GameLogic.Components;
using Platformer.Graphics.Components;
using Platformer.Input.Components;
using Platformer.Physics;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities
{
    public class EntityFactory
    {
        public Entity CreateBox(Vector2 startPosition)
        {
            var entity = new EntityBuilder()
                .WithComponent(typeof(SizeComponent))
                .WithComponent(typeof(PositionComponent))
                .WithComponent(typeof(VelocityComponent))
                .WithComponent(typeof(ApplyForceComponent))
                .WithComponent(typeof(CollisionComponent))
                .WithComponent(typeof(RigidBodyComponent))
                .WithComponent(typeof(SpriteComponent))
                .Build();

            entity.GetComponent<SpriteComponent>().Spritesheet = "test/box";
            entity.GetComponent<SizeComponent>().Size = new Point(16, 16);
            entity.GetComponent<CollisionComponent>().CollisionProfile = CollisionProfiles.StaticSceneElement;
            entity.GetComponent<PositionComponent>().Position = startPosition;

            return entity;
        }

        public Entity CreateGuard(Vector2 startPosition)
        {
            var entity = new EntityBuilder()
                .WithComponent(typeof(SizeComponent))
                .WithComponent(typeof(PositionComponent))
                .WithComponent(typeof(VelocityComponent))
                .WithComponent(typeof(ApplyForceComponent))
                .WithComponent(typeof(CollisionComponent))
                .WithComponent(typeof(RigidBodyComponent))
                .WithComponent(typeof(SpriteComponent))
                .WithComponent(typeof(AnimateComponent))
                .WithComponent(typeof(PatrolBehaviourComponent))
                .WithComponent(typeof(HumanoidBehaviourComponent))
                .Build();

            entity.GetComponent<SpriteComponent>().Spritesheet = "test/walk";
            entity.GetComponent<SizeComponent>().Size = new Point(16, 32);
            entity.GetComponent<CollisionComponent>().CollisionProfile = CollisionProfiles.NonPlayerCharacter;
            entity.GetComponent<PositionComponent>().Position = startPosition;

            return entity;
        }

        public Entity CreatePlayer(Vector2 startPosition)
        {
            var entity = new EntityBuilder()
                .WithComponent(typeof(SizeComponent))
                .WithComponent(typeof(PositionComponent))
                .WithComponent(typeof(VelocityComponent))
                .WithComponent(typeof(ApplyForceComponent))
                .WithComponent(typeof(CollisionComponent))
                .WithComponent(typeof(RigidBodyComponent))
                .WithComponent(typeof(SpriteComponent))
                .WithComponent(typeof(AnimateComponent))
                .WithComponent(typeof(HumanoidBehaviourComponent))
                .WithComponent(typeof(PlayerInputComponent))
                .Build();

            entity.GetComponent<SizeComponent>().Size = new Point(16, 32);
            entity.GetComponent<SpriteComponent>().Spritesheet = "test/walk";
            entity.GetComponent<CollisionComponent>().CollisionProfile = CollisionProfiles.PlayerCharacter;
            entity.GetComponent<PositionComponent>().Position = startPosition;

            return entity;
        }

        public Entity CreateTile(Vector2 position)
        {
            var entity = new EntityBuilder()
                .WithComponent(typeof(SizeComponent))
                .WithComponent(typeof(PositionComponent))
                .WithComponent(typeof(VelocityComponent))
                .WithComponent(typeof(CollisionComponent))
                .WithComponent(typeof(RigidBodyComponent))
                .WithComponent(typeof(SpriteComponent))
                .Build();

            entity.GetComponent<SpriteComponent>().Spritesheet = "test/grass";
            entity.GetComponent<SizeComponent>().Size = new Point(16, 16);
            entity.GetComponent<CollisionComponent>().CollisionProfile = CollisionProfiles.StaticSceneElement;
            entity.GetComponent<PositionComponent>().Position = position;

            return entity;
        }
    }
}
