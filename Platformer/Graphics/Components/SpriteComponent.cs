using Microsoft.Xna.Framework.Graphics;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Graphics.Components
{
    public class SpriteComponent : Component
    {
        public SpriteComponent() : base(ComponentType.Graphics) { }

        public string Spritesheet { get; set; }

        /// <summary>
        /// Which frame on the spritesheet to display (zero-indexed)
        /// </summary>
        public int SpritesheetFrame { get; set; }

        /// <summary>
        /// Which row to use to display the frame (zero-indexed)
        /// An animation can be stored on each row, for example
        /// </summary>
        public int SpritesheetRow { get; set; }

        public override void Update(Entity entity)
        {

        }
    }
}
