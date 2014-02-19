using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Arrow : Item
    {
        bool steel, wooden, collected;

        public override bool Dead
        {
            get
            {
                return collected || base.Dead;
            }
        }

        public Arrow(float scaleFactor, bool steel, string tag, int value, Vector2 startPosition)
            : base(Game1.GameContent.Load<Texture2D>("Items/WoodenArrow"), scaleFactor, startPosition, tag, true, false, true, value)
        {
            collected = false;
            this.steel = steel;
            wooden = !steel;

            if (steel)
            {
                Image = Game1.GameContent.Load<Texture2D>("Items/SteelArrow");
            }
            else
            {
                Image = Game1.GameContent.Load<Texture2D>("Items/WoodenArrow");
            }
        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (this.isColliding(data.Player.CollisionRec))
            {
                if (!data.Player.PlayerInvent.Full)
                {
                    data.Player.addItem(this); //change to add arrow for arrow count
                    data.Player.addMessage(new Message("Wooden Arrow", new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                    collected = true;
                }
                else
                {
                    data.Player.addMessage(new Message("Inventory Full",
                        new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White)); //change to "Quiver Full"
                }
            }

            base.Update(data, gameTime);
        }
    }
}
