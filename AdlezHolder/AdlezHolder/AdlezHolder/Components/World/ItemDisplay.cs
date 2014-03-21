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
        Rectangle selectedRectangle, screenRectangle, keyRec, bombRec, arrowRec, moneyRec,
            bKeyRec, sKeyRec, gKeyRec;
        int moneyCount, bombCount, arrowCount, bronzeCount, silverCount, goldCount;

        SpriteFont font;

        int inDisplayWidth, inDisplayHeight;

        public ItemDisplay()
        {
            font = Game1.GameContent.Load<SpriteFont>("SpriteFont1");

            inDisplayWidth = Game1.DisplayWidth;
            inDisplayHeight = Game1.DisplayHeight;
            screenRectangle = new Rectangle((int)(inDisplayWidth * .1), (int)(inDisplayHeight * .1),
                (int)(inDisplayWidth * .97), (int)(inDisplayHeight * .97));

            bombImage = Game1.GameContent.Load<Texture2D>("Weapons/Bomb");
            arrowImage = Game1.GameContent.Load<Texture2D>("Items/WoodenArrow");
            keyImage = Game1.GameContent.Load<Texture2D>("Items/Key");
            moneyImage = Game1.GameContent.Load<Texture2D>("Items/5Coin");

            bombRec = new Rectangle((int)(screenRectangle.X), (int)(screenRectangle.Height - bombImage.Height), bombImage.Width, bombImage.Height);
            arrowRec = new Rectangle((int)(screenRectangle.X), (int)(screenRectangle.Height - arrowImage.Height), arrowImage.Width, arrowImage.Height);
            keyRec = new Rectangle((int)(screenRectangle.X), (int)(screenRectangle.Height - keyImage.Height), keyImage.Width, keyImage.Height);
            moneyRec = new Rectangle((int)(screenRectangle.X), (int)(screenRectangle.Height - moneyImage.Height), moneyImage.Width, moneyImage.Height);

            bombRec = scale(.02f, bombRec, bombImage);
            arrowRec = scale(.02f, arrowRec, arrowImage);
            keyRec = scale(.05f, keyRec, keyImage);
            moneyRec = scale(.02f, moneyRec, moneyImage);

            keyRec.Y += 15;

            bKeyRec = new Rectangle(keyRec.X, keyRec.Y, keyRec.Width, keyRec.Height);
            sKeyRec = new Rectangle(keyRec.X, keyRec.Y, keyRec.Width, keyRec.Height);
            gKeyRec = new Rectangle(keyRec.X, keyRec.Y, keyRec.Width, keyRec.Height);

            moneyRec.X = (int)(Game1.DisplayWidth - font.MeasureString("000000").X - moneyRec.Width); //740
            arrowRec.X = (int)(moneyRec.X - font.MeasureString("00").X - arrowRec.Width - 5);
            bombRec.X = (int)(arrowRec.X - font.MeasureString("00").X - bombRec.Width - 5);
            bKeyRec.X = (int)(bombRec.X - font.MeasureString("00").X - bKeyRec.Width - 5);
            sKeyRec.X = (int)(bKeyRec.X - font.MeasureString("00").X - sKeyRec.Width - 5);
            gKeyRec.X = (int)(sKeyRec.X - font.MeasureString("00").X - gKeyRec.Width - 5);
        }

        public Rectangle scale(float scaleFactor, Rectangle rectangle, Texture2D image)
        {
            rectangle.Width = (int)((inDisplayWidth * scaleFactor) + 0.5f);
            float aspectRatio = (float)image.Width / image.Height;
            rectangle.Height = (int)((rectangle.Width / aspectRatio) + 0.5f);
            rectangle.Y = screenRectangle.Height - rectangle.Height;

            return rectangle;
        }

        public void Update(Character player)
        {
            moneyCount = player.Money;
            arrowCount = player.Quiver;
            bombCount = player.BombBag;

            bronzeCount = player.Bronzekeys;
            silverCount = player.SilverKeys;
            goldCount = player.GoldKeys;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.MainGameState != GameState.GAMEOVER)
            {
                spriteBatch.Draw(moneyImage, moneyRec, Color.White);
                spriteBatch.DrawString(font, "" + moneyCount, new Vector2((int)moneyRec.X + moneyRec.Width, (int)moneyRec.Y - 5), Color.White);
                spriteBatch.Draw(arrowImage, arrowRec, Color.White);
                spriteBatch.DrawString(font, "" + arrowCount, new Vector2((int)arrowRec.X + arrowRec.Width, (int)moneyRec.Y - 5), Color.White);
                spriteBatch.Draw(bombImage, bombRec, Color.White);
                spriteBatch.DrawString(font, "" + bombCount, new Vector2((int)bombRec.X + bombRec.Width, (int)moneyRec.Y - 5), Color.White);
                spriteBatch.Draw(keyImage, bKeyRec, Color.SandyBrown);
                spriteBatch.DrawString(font, "" + bronzeCount, new Vector2((int)bKeyRec.X + bKeyRec.Width, (int)moneyRec.Y - 5), Color.SandyBrown);
                spriteBatch.Draw(keyImage, sKeyRec, Color.Silver);
                spriteBatch.DrawString(font, "" + silverCount, new Vector2((int)sKeyRec.X + sKeyRec.Width, (int)moneyRec.Y - 5), Color.Silver);
                spriteBatch.Draw(keyImage, gKeyRec, Color.Gold);
                spriteBatch.DrawString(font, "" + goldCount, new Vector2((int)gKeyRec.X + gKeyRec.Width, (int)moneyRec.Y - 5), Color.Gold);
            }
        }
    }
}
