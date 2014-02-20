using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class LightningStone : Gem
    {
        public override bool Dead
        {
            get
            {
                return collected || base.Dead;
            }
        }

        //white/yellow
        public LightningStone(float scaleFactor, Vector2 startPosition, int tier)
            : base(Game1.GameContent.Load<Texture2D>("Items/YellowStone"), scaleFactor, startPosition, "Lightning Stone", 0)
        {
            this.tier = tier;
            if (tier == 1)
            {
                this.Chance = .10f;
                this.Duration = .25f;
            }
            else if (tier == 2)
            {
                this.Chance = .125f;
                this.Duration = .5f;
            }
            else if (tier == 3)
            {
                this.Chance = .15f;
                this.Duration = .6f;
            }
            else if (tier == 4)
            {
                this.Chance = .20f;
                this.Duration = .75f;
            }
            else if (tier == 5)
            {
                this.Chance = .25f;
                this.Duration = 1f;
            }
            else
            {
            }
        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (this.isColliding(data.Player.CollisionRec))
            {
                if (!data.Player.PlayerInvent.Full)
                {
                    data.Player.addItem(this); //change to add arrow for arrow count
                    data.Player.addMessage(new Message("Lightning Stone", new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                    collected = true;
                }
                else
                {
                    data.Player.addMessage(new Message("Inventory Full",
                        new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White)); //change to "Quiver Full"
                }
            }

            base.Update(gameTime);
        }
    }
}
