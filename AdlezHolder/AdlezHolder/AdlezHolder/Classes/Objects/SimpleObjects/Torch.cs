using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Torch : BaseSprite
    {
        Texture2D[] torchImages, lightImages;
        Color torchColor, lightColor;

        int frame, frameTimer, frameTimeLength, lightValue;
        const int higestSwitch = 12, lowestSwitch = 5;

        public override BaseSpriteStruct SaveData
        {
            get
            {
                BaseSpriteStruct myData = base.SaveData;
                myData.saveId = "BTo";
                return myData;
            }
            set
            {
                base.SaveData = value;
            }
        }

        public int LightingValue
        {
            get { return lightValue; }
        }

        public Torch(float scaleFactor, Vector2 startPosition)
            : base(Game1.GameContent.Load<Texture2D>("Torch/Torch1"), scaleFactor, Game1.DisplayWidth, 0, startPosition)
        {
            torchImages = new Texture2D[3];
            lightImages = new Texture2D[3];

            torchImages[0] = Game1.GameContent.Load<Texture2D>("Torch/Torch1");
            torchImages[1] = Game1.GameContent.Load<Texture2D>("Torch/Torch2");
            torchImages[2] = Game1.GameContent.Load<Texture2D>("Torch/Torch3");

            lightImages[0] = Game1.GameContent.Load<Texture2D>("Torch/Light1");
            lightImages[1] = Game1.GameContent.Load<Texture2D>("Torch/Light2");
            lightImages[2] = Game1.GameContent.Load<Texture2D>("Torch/Light3");

            lightValue = 20;
        }

        public override void Update(Map data, GameTime gameTime)
        {
            Random rand = new Random();
            if (frameTimer >= frameTimeLength)
            {
                frameTimeLength = rand.Next(lowestSwitch, higestSwitch);
                frameTimer = 0;
                frame++;
                if (frame > torchImages.Length - 1)
                {
                    frame = 0;
                }
            }
            else
            {
                frameTimer++;
            }

            base.Update(data, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(lightImages[frame], DrawnRec, new Color(255, 255, 255) * (100f / 255f));
            spriteBatch.Draw(torchImages[frame], DrawnRec, Color.White);
        }
    }
}
