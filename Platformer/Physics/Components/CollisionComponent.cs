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
        private readonly IList<Collision> _collisions;
        private IList<CollisionComponent> _nearbyComponents;

        public CollisionComponent(SizeComponent sizeComponent, PositionComponent positionComponent)
        {
            PositionComponent = positionComponent;
            SizeComponent = sizeComponent;

            _collisions = new List<Collision>();
        }

        public PositionComponent PositionComponent { get; }
        public SizeComponent SizeComponent { get; }
        public Type EntityType { get; set; }

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
                    var thisEntityBounds = new RectangleF(PositionComponent.Position, SizeComponent.Size);
                    var nearbyEntityBounds = new RectangleF(nearbyComponent.PositionComponent.Position, nearbyComponent.SizeComponent.Size);

                    if (RectangleF.Intersects(thisEntityBounds, nearbyEntityBounds))
                    {
                        var collisionVector = GetCollisionVector(thisEntityBounds, nearbyEntityBounds);
                        _collisions.Add(new Collision(nearbyComponent.EntityType, collisionVector));
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
