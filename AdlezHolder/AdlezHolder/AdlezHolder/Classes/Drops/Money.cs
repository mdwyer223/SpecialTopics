using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Money : Item
    {
        bool collected;

        public override bool Dead
        {
            get
            {
                return collected || base.Dead;
            }
        }

        public Money(float scaleFactor, Vector2 startPosition, string tag, int value)
            : base(Game1.GameContent.Load<Texture2D>("Items/1Coin"), scaleFactor, startPosition, tag, false, true, false, value)
        {
            collected = false;

            if (value >= 5)
                this.Image = Game1.GameContent.Load<Texture2D>("Items/5Coin");
            if (value >= 10)
                this.Image = Game1.GameContent.Load<Texture2D>("Items/10Coin");
        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (this.CollisionRec.Intersects(data.Player.CollisionRec))
            {
                data.Player.addFunds(this.value);
                data.Player.addMessage(new Message(value + " Coins", Color.White));
                collected = true;
            }

            base.Update(gameTime);
        }
    }
}
