using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Core;
using Platformer.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Graphics
{
    public class GraphicsSystem : ISystem<IGraphicsComponent>
    {
        private IList<IGraphicsComponent> _components = new List<IGraphicsComponent>();

        public void RegisterComponent(IGraphicsComponent component)
        {
            _components.Add(component);

            _components = _components.OrderBy(x => x is DebugGraphicsComponent).ToList();
        }

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            foreach (var component in _components)
            {
                component.Load(contentManager, spriteBatch, graphicsDevice);
            }
        }

        public void Update()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }
    }
}
