﻿using Microsoft.Xna.Framework;
using Platformer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Factories
{
    public class TileEntityFactory : IEntityFactory
    {
        public Entity Build()
        {
            var testSpriteComponent = new SpriteComponent("test/grass");

            return new Entity(
                new Point(16, 16),
                testSpriteComponent,
                new RigidBodyComponent());
        }
    }
}