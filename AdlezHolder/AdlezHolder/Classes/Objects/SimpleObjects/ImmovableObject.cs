using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class ImmovableObject : BaseSprite
    {
        protected bool dead, visible;
        public Vector2[] nodes;

        public ImmovableObject(Texture2D texture, float scaleFactor, float secondsToCrossScreen, Vector2 start)
            : base(texture, scaleFactor, Game1.DisplayWidth, secondsToCrossScreen, start)
        {
            this.Image = texture;

            dead = false;
            visible = true;

            int halfHeight = CollisionRec.Height / 2;
            int halfWidth = CollisionRec.Width / 2;

            nodes = new Vector2[8];

            nodes[0] = new Vector2(Position.X, Position.Y);
            nodes[1] = new Vector2(Position.X + halfWidth, Position.Y);
            nodes[2] = new Vector2(Position.X + CollisionRec.Width, Position.Y);

            nodes[3] = new Vector2(Position.X + CollisionRec.Width, Position.Y + halfHeight);
            nodes[4] = new Vector2(Position.X + CollisionRec.Width, Position.Y + CollisionRec.Height);

            nodes[5] = new Vector2(Position.X + halfWidth, Position.Y + CollisionRec.Height);

            nodes[6] = new Vector2(Position.X, Position.Y + CollisionRec.Height);

            nodes[7] = new Vector2(Position.X, Position.Y + halfHeight);
        }

        public override void Update(Map data, GameTime gametime)
        {
            if (!dead)
            { }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(Image, DrawnRec, Color.White);
            }
        }
    }
}
