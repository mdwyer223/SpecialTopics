using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class BaseSpriteVars
    {
        public Color imageColor;
        public bool isDead, isVisible;
        public Vector2 position;
        public Vector2 center;

        public Rectangle drawnRec;
        public Rectangle collisionRec;

        protected BaseSpriteVars()
        {
        }

        public BaseSpriteVars(BaseSprite inBaseSprite)
        {
            imageColor = inBaseSprite.ImageColor;
            isDead = inBaseSprite.IsDead;
            isVisible = inBaseSprite.IsVisible;
            position = inBaseSprite.Position;
            center = inBaseSprite.Center;
            drawnRec = inBaseSprite.DrawnRec;
            collisionRec = inBaseSprite.CollisionRec;
        }


    }
}
