using Microsoft.Xna.Framework;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Physics.Components
{
    public class CollisionComponent : IPhysicsComponent
    {
        private readonly PositionComponent _positionComponent;
        private readonly SizeComponent _sizeComponent;

        private readonly IList<Collision> _collisions;
        private IList<CollisionComponent> _nearbyComponents;

        public CollisionComponent(SizeComponent sizeComponent, PositionComponent positionComponent)
        {
            _positionComponent = positionComponent;
            _sizeComponent = sizeComponent;

            _collisions = new List<Collision>();
        }

        public Vector2 Position => _positionComponent.Position;
        public Point Size => _sizeComponent.Size;
        public CollisionProfiles CollisionProfile { get; set; }

        public Collision? GetCollision(Func<Collision, bool> predicate, bool remove = true)
        {
            for (var i = 0; i < _collisions.Count; i++)
            {
                if (predicate(_collisions[i]))
                {
                    return GetCollision(i, remove);
                }
            }

            return null;
        }

        public IEnumerable<Collision> GetCollisions()
        {
            for (var i = 0; i < _collisions.Count; i++)
            {
                yield return GetCollision(i, false);
            }
        }
        
        public void SetNearbyComponents(IList<CollisionComponent> components)
        {
            _nearbyComponents = components;
        }

        public void Update()
        {
            // clear collisions as they were all resolved last update
            _collisions.Clear();

            // detect and add new collisions for this update
            foreach (var nearbyComponent in _nearbyComponents)
            {
                if (this != nearbyComponent)
                {
                    var thisEntityBounds = new RectangleF(Position, Size);
                    var nearbyEntityBounds = new RectangleF(nearbyComponent.Position, nearbyComponent.Size);

                    if (RectangleF.Intersects(thisEntityBounds, nearbyEntityBounds))
                    {
                        var collisionVector = GetCollisionVector(thisEntityBounds, nearbyEntityBounds);
                        _collisions.Add(new Collision(nearbyComponent.CollisionProfile, collisionVector));
                    }
                }
            }
        }

        private Vector2 GetCollisionVector(RectangleF thisEntityBounds, RectangleF nearbyEntityBounds)
        {
            var entityWidthOffset = (thisEntityBounds.X > nearbyEntityBounds.X ? -1 : 1) * thisEntityBounds.Width;
            var entityHeightOffset = (thisEntityBounds.Y > nearbyEntityBounds.Y ? -1 : 1) * thisEntityBounds.Height;

            var collisionX = thisEntityBounds.X + entityWidthOffset - nearbyEntityBounds.X;
            var collisionY = thisEntityBounds.Y + entityHeightOffset - nearbyEntityBounds.Y;

            return new Vector2(collisionX, collisionY);
        }

        private Collision GetCollision(int index, bool remove = true)
        {
            var collision = _collisions[index];

            if (remove)
            {
                _collisions.RemoveAt(index);
            }

            return collision;
        }
    }
}
