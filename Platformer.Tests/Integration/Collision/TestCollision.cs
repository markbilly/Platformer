using Microsoft.Xna.Framework;
using NUnit.Framework;
using Platformer.Entities;
using Platformer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Tests.Integration.Collision
{
    [TestFixture]
    public class TestCollision
    {
        [Test]
        public void Collision_GivenTwoRigidBodyEntitiesCollideVeritcally_StopEntitiesMovingVeritcally()
        {
            // Arrange
            var stationaryEntity = new Entity(
                new Vector2(1, 1),
                new Point(10, 10),
                new RigidBodyComponent());

            var movingEntity = new Entity(
                new Vector2(1, 2),
                new Point(10, 10),
                new RigidBodyComponent());

            movingEntity.Velocity = new Vector2(0, 1);

            var entities = new List<Entity> { stationaryEntity, movingEntity };

            stationaryEntity.GetComponent<RigidBodyComponent>().NearbyEntities = entities;
            movingEntity.GetComponent<RigidBodyComponent>().NearbyEntities = entities;

            // Action
            stationaryEntity.Update();
            movingEntity.Update();

            // Assert
            Assert.That(movingEntity.Velocity.Y == 0, "Vertical speed not zero as expected.");
        }
    }
}
