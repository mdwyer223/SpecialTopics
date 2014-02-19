using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class BombObj : BaseSprite
    {
        int delay, delayTimer;

        public bool BlowUp
        {
            get { return delayTimer >= delay; }
        }

        public BombObj(Texture2D texture, float scaleFactor, Vector2 start, int delay)
            : base(texture, scaleFactor, Game1.DisplayWidth, 0, start)
        {
            this.delay = delay;
            delayTimer = 0;
        }

        public override void Update(Map data, GameTime gameTime)
        {
            this.delayTimer += gameTime.ElapsedGameTime.Milliseconds;
            base.Update(data, gameTime);
        }
    }
}
