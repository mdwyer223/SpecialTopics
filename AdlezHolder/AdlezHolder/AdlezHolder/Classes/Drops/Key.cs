using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public enum KeyType { GOLD, SILVER, BRONZE };

    public class Key : Item
    {
        KeyType type;

        bool collected;

        public override bool Dead
        {
            get
            {
                return collected || base.Dead;
            }
        }

        public KeyType Type
        {
            get { return type; }
        }

        public Key(Vector2 startPosition, KeyType type, int numberOf)
            : base(Game1.GameContent.Load<Texture2D>("Items/Key"), .05f, 
                startPosition, "", true, false, true, 0, numberOf)
        {
            this.type = type;

            if (type == KeyType.GOLD)
            {
                color = Color.Gold;
                value = 150;
                tag = "Gold Key";
            }
            else if (type == KeyType.SILVER)
            {
                color = Color.Silver;
                value = 100;
                tag = "Silver Key";
            }
            else if (type == KeyType.BRONZE)
            {
                color = Color.SandyBrown;
                value = 50;
                tag = "Bronze Key";
            }

        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (this.isColliding(data.Player.CollisionRec))
            {
                    data.Player.addKey(type);
                    data.Player.addMessage(new Message(tag, new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                    collected = true;
            }

            base.Update(data, gameTime);
        }
    }
}
