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
        public override BaseSpriteStruct SaveData
        {
            get
            {
                BaseSpriteStruct myData = base.SaveData;
                myData.saveId = "IWa";
                return myData;
            }
            set
            {
                base.SaveData = value;
            }
        }

        public Wall(Rectangle objRec, Texture2D noTexture, Vector2 start)
            : base(noTexture, 0.1f, 0, Vector2.Zero)
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
