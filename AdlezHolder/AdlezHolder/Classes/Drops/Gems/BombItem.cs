using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class BombItem : Item
    {
        bool collected = false;

        public override bool Dead
        {
            get
            {
                return collected || base.Dead;
            }
        }

        //orange
        public BombItem(float scaleFactor, Vector2 startPosition, int numberOf)
            :base(Game1.GameContent.Load<Texture2D>("Weapons/Bomb"), scaleFactor, startPosition, "Bomb", true, false, true, 100, numberOf)
        {
            
        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (this.isColliding(data.Player.CollisionRec))
            {
                if (!data.Player.BombBagFull)
                {
                    data.Player.addBomb(); 
                    data.Player.addMessage(new Message(tag, new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                    collected = true;
                }
                else
                {
                    data.Player.addMessage(new Message("Bomb Bag Full",
                        new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                }
            }

            base.Update(gameTime);
        }
    }
}
