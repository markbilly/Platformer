﻿using Microsoft.Xna.Framework;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Physics.Components
{
    public class ApplyForceComponent : IPhysicsComponent
    {
        const int DEFUALT_MASS = 50;

        private readonly VelocityComponent _velocityComponent;
        private readonly IList<AppliedForce> _forces;
        private readonly int _mass;

        public ApplyForceComponent(VelocityComponent velocityComponent)
        {
            _velocityComponent = velocityComponent;
            _forces = new List<AppliedForce>();
            _mass = DEFUALT_MASS;
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

        public void Update()
        {
            for (var i = 0; i < _forces.Count; i++)
            {
                var force = _forces[i];

                if (force.Duration == 0)
                {
                    _forces.RemoveAt(i);
                }

                if (!IsMaxYVelocityReachedInDirectionOfForce(force.Force))
                {
                    _velocityComponent.Velocity += force.Force / _mass;
                }

                if (force.Duration > -1)
                {
                    force.Duration--;
                }
            }
        }

        private bool IsMaxYVelocityReachedInDirectionOfForce(Vector2 force)
        {
            return 
                (_velocityComponent.Velocity.Y > 0 && _velocityComponent.Velocity.Y > 5 && force.Y > 0) ||
                (_velocityComponent.Velocity.Y < 0 && _velocityComponent.Velocity.Y < 5 && force.Y < 0);
        }

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
    }
}
