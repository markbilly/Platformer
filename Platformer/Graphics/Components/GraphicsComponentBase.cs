using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Graphics.Components
{
    public abstract class GraphicsComponentBase : IComponent
    {
        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }

        public abstract void Load(ContentManager contentManager);

        public abstract void Update(Entity entity);
    }
}
