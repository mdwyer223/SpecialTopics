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
        public LightningStone(GemStruct gemData)
            : base(gemData)
        {
        }

        public LightningStone(float scaleFactor, Vector2 startPosition, int tier, int numberOf)
            : base(Game1.GameContent.Load<Texture2D>("Items/YellowStone"), scaleFactor, startPosition, "Lightning Stone", 0, numberOf)
        {
            this.tier = tier;
            if (tier == 1)
            {
                this.Chance = .10f;
                this.Duration = .25f;
                this.value = 60;
            }
            else if (tier == 2)
            {
                this.Chance = .125f;
                this.Duration = .5f;
                this.value = 100;
            }
            else if (tier == 3)
            {
                this.Chance = .15f;
                this.Duration = .6f;
                this.value = 140;
            }
            else if (tier == 4)
            {
                this.Chance = .20f;
                this.Duration = .75f;
                this.value = 210;
            }
            else if (tier == 5)
            {
                this.Chance = .25f;
                this.Duration = 1f;
                this.value = 325;
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
        public override string getEffectsString()
        {
            return "You Have Sold a Lightning Stone";
        }
        public override string getName()
        {
            if (tier == 1)
            {
                return "Lev 1 Lightning Stone";
            }
            else if (tier == 2)
            {
                return "Lev 2 Lightning Stone";
            }
            else if (tier == 3)
            {
                return "Lev 3 Lightning Stone";
            }
            else if (tier == 4)
            {
                return "Lev 4 Lightning Stone";
            }
            else
            {
                return "Lev 5 Lightning Stone";
            }
        }

        public override string getChangesString()
        {
            return "You Now Have a Lightning Stone!";
        }
    }
}
