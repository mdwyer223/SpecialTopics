using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Potion : Item
    {
        int healingPower;
        bool collected;
        int tempTier;
        public override bool Dead
        {
            get
            {
                return collected || base.Dead;
            }
        }

        public Potion(Vector2 start, float scaleFactor, int tier, int numberOf)
            : base(Game1.GameContent.Load<Texture2D>("Items/1RedPotion"), scaleFactor, start,
                "Potion", true, false, true, 50, numberOf)
        {
            options.Add("Use");
            tempTier = tier;
            if (tier == 1)
            {
                healingPower = 50;
            }
            else if (tier == 2)
            {
                healingPower = 100;
            }
            else if (tier == 3)
            {
                healingPower = 150;
            }
        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (this.isColliding(data.Player.CollisionRec))
            {
                if (!data.Player.PlayerInvent.Full)
                {
                    data.Player.addItem(this); //change to add arrow for arrow count
                    data.Player.addMessage(new Message("Potion", new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                    playerTemp = data.Player;
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

        public override void chooseOption(string s, List<Item> items)
        {
            //compare the string to the options chosen
            switch (s)
            {
                case "Use":
                    {
                        if (playerTemp != null)
                        {
                            playerTemp.heal(healingPower);
                            if (count <= 0)
                            {
                                playerTemp = null;
                            }
                            this.drop(items);
                        }
                        break;
                    }
            }

            base.chooseOption(s, items);
        }
        public string getEffectsString()
        {
            if (tempTier == 1)
            {
                return "You Have Sold a Lev 1 Potion";
            }
            else if (tempTier == 2)
            {
                return "You Have Sold a Lev 2 Potion";
            }
            else
            {
                return "You Have Sold a Lev 3 Potion";
            }
        }
        public override string getName()
        {
            if (tempTier == 1)
            {
                return "Lev 1 Potion";
            }
            else if (tempTier == 2)
            {
                return "Lev 2 Potion";
            }
            else
            {
                return "Lev 3 Potion";
            }

        }

        public override string getChangesString()
        {
            if (tempTier == 1)
            {
                return "You Now Have a Lev 1 Potion";
            }
            else if (tempTier == 2)
            {
                return "You Now Have a Lev 2 Potion";
            }
            else
            {
                return "You Now Have a Lev 3 Potion";
            }
        }
    }
}
