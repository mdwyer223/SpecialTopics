using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class BuildingObject : ImmovableObject
    {
        public BuildingObject(Texture2D texture, float scaleFactor, Vector2 start)
            : base(texture, scaleFactor, 0, start)
        {
            
        }


        public override void Update(GameTime gameTime)
        {
            //tripwire stuff
            base.Update(gameTime);
        }
    }
}
