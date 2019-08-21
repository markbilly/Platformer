﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Platformer.Core;

namespace Platformer.Entities.EntityCommands
{
    public class MoveLeftEntityCommand : IEntityCommand
    {
        public void Execute(Entity entity)
        {
            entity.Velocity = new Vector2(-1, entity.Velocity.Y);
        }
    }
}