using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class ApplyForceComponent : IComponent
    {
        private class AppliedForce
        {
            public AppliedForce(Vector2 force, int duration)
            {
                Force = force;
                Duration = duration;
            }

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

        public void ApplyConstantForce(Vector2 force)
        {
            _forces.Add(new AppliedForce(force, -1));
        }

        public void ApplyForce(Vector2 force, int duration)
        {
            _forces.Add(new AppliedForce(force, duration));
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
