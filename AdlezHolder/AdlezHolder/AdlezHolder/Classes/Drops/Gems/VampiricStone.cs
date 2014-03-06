using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class VampiricStone : Gem
    {
        public override bool Dead
        {
            get
            {
                return collected || base.Dead;
            }
        }

        public VampiricStone(GemStruct gemData)
            : base(gemData)
        {
        }

        public VampiricStone(float scaleFactor, Vector2 startPosition, int tier, int numberOf)
            :base(Game1.GameContent.Load<Texture2D>("Items/MagentaStone"), scaleFactor, startPosition, "Vampiric Stone", 50, numberOf)
        {
            this.tier = tier;

            if (tier == 1)
            {
                this.Chance = .1f;
            }
            else if (tier == 2)
            {
                this.Chance = .15f;
            }
            else if (tier == 3)
            {
                this.Chance = .175f;
            }
            else if (tier == 4)
            {
                this.Chance = .2f;
            }
            else if (tier == 5)
            {
                this.Chance = .25f;
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
                    data.Player.addMessage(new Message("Vampiric Stone", new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
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
