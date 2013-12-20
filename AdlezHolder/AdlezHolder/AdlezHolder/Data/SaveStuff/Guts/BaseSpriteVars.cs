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
        public Texture2D image;
        public Color imageColor;
        public bool isDead, isVisible;
        public Vector2 position;
        public Vector2 center;

        public Rectangle drawnRec;
        public Rectangle collisionRec;

        // TODO: if each class had a property with only the guts of the class
        // Need to save data to re-create the player: all the stuff in the constructor
        protected BaseSpriteVars()
        {
        }

        public BaseSpriteVars(BaseSprite inBaseSprite)
        {            
            //image = inBaseSprite.Image;
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
