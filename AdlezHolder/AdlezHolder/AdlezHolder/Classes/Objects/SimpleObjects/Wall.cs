using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class Wall : ImmovableObject
    {
        public override Rectangle CollisionRec
        {
            get
            {
                return collisionRec;
            }
            protected set
            {
                base.CollisionRec = value;
            }
        }

        public Wall(Rectangle objRec, Texture2D noTexture, Vector2 start)
            : base(noTexture, 0.1f, 0, start)
        {
            collisionRec = objRec;
            this.position.X = collisionRec.X;
            this.position.Y = collisionRec.Y;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            collisionRec.X = (int)position.X;
            collisionRec.Y = (int)position.Y;
        }
    }
}
