using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class PoisonStone : Gem
    {

        public override bool Dead
        {
            get
            {
                return collected || base.Dead;
            }
        }

        public PoisonStone(GemStruct gemData)
            : base(gemData)
        {
        }

        public PoisonStone(float scaleFactor, Vector2 startPosition, int tier)
            : base(Game1.GameContent.Load<Texture2D>("Items/MagentaStone"), scaleFactor, startPosition, "Poison Stone", 0)
        {
            color = Color.Purple;
            this.tier = tier;
            if (tier == 1)
            {
                this.Chance = .10f;
                this.Duration = 10f;
                this.Damage = 10;
            }
            else if (tier == 2)
            {
                this.Chance = .125f;
                this.Duration = 20f;
                this.Damage = 20;
            }
            else if (tier == 3)
            {
                this.Chance = .15f;
                this.Duration = 15f;
                this.Damage = 40;
            }
            else if (tier == 4)
            {
                this.Chance = .20f;
                this.Duration = 13f;
                this.Damage = 50;
            }
            else if (tier == 5)
            {
                this.Chance = .25f;
                this.Duration = 15f;
                this.Damage = 60;
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
                    data.Player.addMessage(new Message("Poison Stone", new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
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
