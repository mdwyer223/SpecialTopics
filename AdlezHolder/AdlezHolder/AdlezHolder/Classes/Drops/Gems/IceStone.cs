using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class IceStone : Gem
    {
        float critDamagePercent;
        public float CritDamage
        {
            get { return critDamagePercent; }
            protected set { critDamagePercent = value; }
        }

        public override bool Dead
        {
            get
            {
                return collected || base.Dead;
            }
        }

        //cyan
        public IceStone(GemStruct gemData)
            : base(gemData)
        {
        }

        public IceStone(float scaleFactor, Vector2 startPosition, int tier, int numberOf)
            : base(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scaleFactor, startPosition, "Ice Stone", 0, numberOf)
        {
            this.tier = tier;
            if (tier == 1)
            {
                //load texture, value, and stats
                this.Chance = .05f;
                this.CritDamage = 1f;
                this.Duration = 1f;
                this.value = 75;
            }
            else if (tier == 2)
            {
                this.Chance = .075f;
                this.CritDamage = 1.25f;
                this.Duration = 3f;
                this.value = 90;
            }
            else if (tier == 3)
            {
                this.Chance = .1f;
                this.CritDamage = 1.5f;
                this.Duration = 2f;
                this.value = 115;
            }
            else if (tier == 4)
            {
                this.Chance = .125f;
                this.CritDamage = 1.75f;
                this.Duration = 2.5f;
                this.value = 225;
            }
            else if (tier == 5)
            {
                this.Chance = .15f;
                this.CritDamage = 2f;
                this.Duration = 3f;
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
                    data.Player.addMessage(new Message("Ice Stone", new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
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
            return "You Have Sold a Ice Stone";
        }
        public override string getName()
        {
            if (tier == 1)
            {
                return "Lev 1 Ice Stone";
            }
            else if (tier == 2)
            {
                return "Lev 2 Ice Stone";
            }
            else if (tier == 3)
            {
                return "Lev 3 Ice Stone";
            }
            else if (tier == 4)
            {
                return "Lev 4 Ice Stone";
            }
            else
            {
                return "Lev 5 Ice Stone";
            }
        }

        public override string getChangesString()
        {
            return "You Now Have a Ice Stone!";
        }
    }
}
