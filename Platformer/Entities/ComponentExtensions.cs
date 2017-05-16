using Platformer.AI.Components;
using Platformer.Entities;
using Platformer.Entities.Components;
using Platformer.Graphics.Components;
using Platformer.Input.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities
{
    public static class ComponentExtensions
    {
        public static int GetExecutionOrder(this IComponent component)
        {
            var type = component.GetType();

            if (type == typeof(AnimateComponent))
                return 0;
            if (type == typeof(ApplyForceComponent))
                return 10;
            if (type == typeof(CollisionComponent))
                return 20;
            if (type == typeof(RigidBodyComponent))
                return 30;
            if (type == typeof(PatrolAIComponent))
                return 40;
            if (type == typeof(PlayerInputComponent))
                return 40;
            if (type == typeof(HumanoidStateComponent))
                return 50;
            if (type == typeof(HumanoidAnimationComponent))
                return 60;
            if (type == typeof(GraphicsComponentBase))
                return 70;

            throw new Exception("Execution order not defined for type.");
        }
    }
}
