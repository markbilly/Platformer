using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Graphics
{
    public interface IGraphicsComponent : IComponent
    {
        void Load(ContentManager contentManager, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
    }
}
