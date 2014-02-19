using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class Chest : ImmovableObject
    {
        Texture2D openTexture;
        bool open;

        public bool Open
        {
            get { return open; }
            set { open = value; }
        }

        public Chest(float scaleFactor, Vector2 start)
            : base(Game1.GameContent.Load<Texture2D>("Chests/Chest"), scaleFactor, 
                0, start)
        {
            openTexture = Game1.GameContent.Load<Texture2D>("Chests/EmptyChest");
        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (!open && data.Player.Sword.CollisionRec.Intersects(this.CollisionRec) 
                && !data.Player.Sword.isDead)
            {
                open = true;
                data.Player.addMessage(new Message("You got a key", 
                    new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
            }
            if (open)
            {
                this.Image = openTexture;
            }
        }
    }
}
