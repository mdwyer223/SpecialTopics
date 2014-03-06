using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AdlezHolder
{
    public class ItemDisplay
    {
        Texture2D bombImage, arrowImage, keyImage, moneyImage;
        Rectangle selectedRectangle, screenRectangle, keyRec, bombRec, arrowRec, moneyRec;
        Vector2 bombPos, arrowPos, keyPos, moneyPos;

        int inDisplayWidth, inDisplayHeight;

        public ItemDisplay()
        {
            inDisplayWidth = Game1.DisplayWidth;
            inDisplayHeight = Game1.DisplayHeight;
            screenRectangle = new Rectangle((int)(inDisplayWidth * .1), (int)(inDisplayHeight * .1),
                (int)(inDisplayWidth * .97), (int)(inDisplayHeight * .97));

            bombImage = Game1.GameContent.Load<Texture2D>("Weapons/Bomb");
            arrowImage = Game1.GameContent.Load<Texture2D>("Items/WoodenArrow");
            keyImage = Game1.GameContent.Load<Texture2D>("Items/Key");
            moneyImage = Game1.GameContent.Load<Texture2D>("Items/5Coin");

            bombRec = new Rectangle((int)(screenRectangle.X), (int)(screenRectangle.Height - bombImage.Height), bombImage.Width, bombImage.Height);


            bombRec = scale(.02f, bombRec, bombImage);
        }

        public Rectangle scale(float scaleFactor, Rectangle rectangle, Texture2D image)
        {
            rectangle.Width = (int)((inDisplayWidth * scaleFactor) + 0.5f);
            float aspectRatio = (float)image.Width / image.Height;
            rectangle.Height = (int)((rectangle.Width / aspectRatio) + 0.5f);
            rectangle.Y = screenRectangle.Height - rectangle.Height;

            return rectangle;
        }
    }
}
