using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class BombObj : BaseSprite
    {
        int delay, delayTimer, nextImageTime, nextImageTimer, index;
        Texture2D[] bombImage;

        public bool BlowUp
        {
            get { return delayTimer >= delay; }
        }

        public BombObj(Texture2D texture, float scaleFactor, Vector2 start, int delay)
            : base(texture, scaleFactor, Game1.DisplayWidth, 0, start)
        {
            bombImage = new Texture2D[4];

            bombImage[0] = texture;
            bombImage[1] = Game1.GameContent.Load<Texture2D>("Weapons/BombAnime/Bomb2");
            bombImage[2] = Game1.GameContent.Load<Texture2D>("Weapons/BombAnime/Bomb3");
            bombImage[3] = Game1.GameContent.Load<Texture2D>("Weapons/BombAnime/Bomb4");

            this.delay = delay;
            delayTimer = 0;

            nextImageTime = delay / bombImage.Length;
            index = 0;
        }

        public override void Update(Map data, GameTime gameTime)
        {
            this.delayTimer += gameTime.ElapsedGameTime.Milliseconds;
            this.nextImageTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (nextImageTimer > nextImageTime)
            {
                index++;
                nextImageTimer = 0;
                if (index < bombImage.Length)
                {
                    setImage(bombImage[index]);
                }
            }

            base.Update(data, gameTime);
        }

        public void rushDelay()
        {
            delayTimer = delay;
        }
    }
}
