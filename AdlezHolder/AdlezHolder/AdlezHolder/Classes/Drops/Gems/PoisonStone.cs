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

        public PoisonStone(float scaleFactor, Vector2 startPosition, int tier, int numberOf)
            : base(Game1.GameContent.Load<Texture2D>("Items/MagentaStone"), scaleFactor, startPosition, "Poison Stone", 0, numberOf)
        {
            color = Color.Purple;
            this.tier = tier;
            if (tier == 1)
            {
                this.Chance = .10f;
                this.Duration = 10f;
                this.Damage = 10;
                this.value = 50;
            }
            else if (tier == 2)
            {
                this.Chance = .125f;
                this.Duration = 20f;
                this.Damage = 20;
                this.value = 100;
            }
            else if (tier == 3)
            {
                this.Chance = .15f;
                this.Duration = 15f;
                this.Damage = 40;
                this.value = 150;
            }
            else if (tier == 4)
            {
                this.Chance = .20f;
                this.Duration = 13f;
                this.Damage = 50;
                this.value = 250;
            }
            else if (tier == 5)
            {
                this.Chance = .25f;
                this.Duration = 15f;
                this.Damage = 60;
                this.value = 350;
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
        public override string getEffectsString()
        {
            return "You Have Sold a Poison Stone";
        }
        public override string getName()
        {
            if (tier == 1)
            {
                return "Lev 1 Poison Stone";
            }
            else if (tier == 2)
            {
                return "Lev 2 Poison Stone";
            }
            else if (tier == 3)
            {
                return "Lev 3 Poison Stone";
            }
            else if (tier == 4)
            {
                return "Lev 4 Poison Stone";
            }
            else
            {
                return "Lev 5 Poison Stone";
            }
        }

        public override string getChangesString()
        {
            return "You Now Have a Poison Stone!";
        }
    }
}
