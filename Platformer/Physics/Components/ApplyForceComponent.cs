using Microsoft.Xna.Framework;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Physics.Components
{
    public class ApplyForceComponent : IComponent
    {
        private class AppliedForce
        {
            public AppliedForce(string id, Vector2 force, int duration)
            {
                Id = id;
                Force = force;
                Duration = duration;
            }

            public string Id { get; private set; }
            public Vector2 Force { get; private set; }
            public int Duration { get; set; }
        }

        private IList<AppliedForce> _forces;
        private int _mass;

        public ApplyForceComponent(int mass = 50)
        {
            _forces = new List<AppliedForce>();
            _mass = mass;
        }

        public bool IsForceApplied(string id)
        {
            return _forces.Select(x => x.Id).Contains(id);
        }

        public void ApplyConstantForce(string id, Vector2 force)
        {
            _forces.Add(new AppliedForce(id, force, -1));
        }

        public void ApplyForce(string id, Vector2 force, int duration)
        {
            _forces.Add(new AppliedForce(id, force, duration));
        }

        public void Update(Entity entity)
        {
            for (var i = 0; i < _forces.Count; i++)
            {
                var force = _forces[i];

                if (force.Duration == 0)
                {
                    _forces.RemoveAt(i);
                }

                if (!IsMaxYVelocityReachedInDirectionOfForce(entity, force.Force))
                {
                    entity.Velocity += force.Force / _mass;
                }

                if (force.Duration > -1)
                {
                    force.Duration--;
                }
            }
        }

        private static bool IsMaxYVelocityReachedInDirectionOfForce(Entity entity, Vector2 force)
        {
            return 
                (entity.Velocity.Y > 0 && entity.Velocity.Y > 5 && force.Y > 0) ||
                (entity.Velocity.Y < 0 && entity.Velocity.Y < 5 && force.Y < 0);
        }
    }
}
