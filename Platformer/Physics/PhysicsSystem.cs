using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Physics
{
    public class PhysicsSystem : ISystem<IPhysicsComponent>
    {
        private static readonly Vector2 GRAVITY = new Vector2(0, 10f);

        private readonly IList<CollisionComponent> _collisionComponents;        
        private readonly IList<IPhysicsComponent> _components;

        public PhysicsSystem()
        {
            _collisionComponents = new List<CollisionComponent>();
            _components = new List<IPhysicsComponent>();
        }

        public void RegisterComponent(IPhysicsComponent component)
        {
            if (component is RigidBodyComponent rigidBodyComponent)
            {
                _collisionComponents.Add(rigidBodyComponent.CollisionComponent);
            }

            if (component is ApplyForceComponent forceComponent)
            {
                forceComponent.ApplyConstantForce("gravity", GRAVITY);
            }

            _components.Add(component);
        }

        public void Update()
        {
            foreach (var component in _components)
            {   
                // give entity relevant other entities for collision
                if (component is CollisionComponent collisionComponent)
                {
                    // TODO: only pass rigid bodies near to the entity
                    // TODO: just generally do this stuff more efficiently
                    collisionComponent.SetNearbyComponents(_collisionComponents);
                }

                component.Update();
            }
        }
    }
}
