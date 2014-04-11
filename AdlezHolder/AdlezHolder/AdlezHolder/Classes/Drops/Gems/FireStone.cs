using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class FireStone : Gem
    {
        public override bool Dead
        {
            get
            {
                return collected || base.Dead;
            }
        }

        //orange
        public FireStone(GemStruct gemData)
            : base(gemData)
        {
        }

        public FireStone(float scaleFactor, Vector2 startPosition, int tier, int numberOf)
            : base(Game1.GameContent.Load<Texture2D>("Items/MagentaStone"), scaleFactor, startPosition, "Fire Stone", 0, numberOf)
        {
            color = Color.Orange;
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
                this.Duration = 8f;
                this.Damage = 15;
                this.value = 100;
            }
            else if (tier == 3)
            {
                this.Chance = .15f;
                this.Duration = 6f;
                this.Damage = 25;
                this.value = 150;
            }
            else if (tier == 4)
            {
                this.Chance = .20f;
                this.Duration = 4f;
                this.Damage = 35;
                this .value = 200;
            }
            else if (tier == 5)
            {
                this.Chance = .25f;
                this.Duration = 3f;
                this.Damage = 45;
                this.value = 500;
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
                    data.Player.addMessage(new Message("Fire Stone", new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
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
            return "You Have Sold a Fire Stone";
        }
        public override string getName()
        {
            if (tier == 1)
            {
              return "Lev 1 Fire Stone";
            }
            else if (tier == 2)
            {
                return "Lev 2 Fire Stone";
            }
            else if (tier == 3)
            {
                return "Lev 3 Fire Stone";
            }
            else if (tier == 4)
            {
                return "Lev 4 Fire Stone";
            }
            else
            {
                return "Lev 5 Fire Stone";
            }


        }

        public override string getChangesString()
        {
            return "You Now Have a Fire Stone!";
        }

    }
}
