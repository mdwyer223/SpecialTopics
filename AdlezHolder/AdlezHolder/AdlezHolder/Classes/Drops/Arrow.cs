﻿using System;
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
        bool steel, collected;

        

        public override bool Dead
        {
            get
            {
                return collected || base.Dead;
            }
        }

        public bool Steel
        {
            get { return steel; }
        }

        public Arrow(ItemStruct itemStruct)
            : base(itemStruct)
        {
        }


        public Arrow(float scaleFactor, bool steel, string tag, int value, Vector2 startPosition, int numberOf)
            : base(Game1.GameContent.Load<Texture2D>("Items/WoodenArrow"), scaleFactor, startPosition, tag, true, false, true, value, numberOf)
        {
            collected = false;
            this.steel = steel;

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
                if (!data.Player.QuiverFull)
                {
                    data.Player.addArrow();
                    data.Player.addMessage(new Message("Arrow", new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                    collected = true;
                }
                else
                {
                    data.Player.addMessage(new Message("Quiver Full",
                        new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                }
            }

            base.Update(data, gameTime);
        }
    }
}