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
            this.tier = tier;
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
                            playerTemp = null;
                            this.drop(items);
                        }
                        break;
                    }
            }

            base.chooseOption(s, items);
        }
    }
}
