using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Message
    {
        int timer, timeLength;
        float floatingValue;
        string message;

        Vector2 position;
        Color color, drawColor;
        int a;

        public bool TimeUp
        {
            get { return timer >= timeLength; }
        }

        public string Text
        {
            get { return message; }
        }

        public Message(string message, Color color)
        {
            this.message = message;
            this.position = new Vector2(Game1.DisplayWidth, Game1.DisplayHeight);

            a = 255;
            timeLength = 40;
            this.color = drawColor = color;
        }

        public Message(string message, Vector2 position, Color color)
        {
            this.message = message;
            this.position = position;

            a = 255;
            timeLength = 40;
            this.color = drawColor = color;
        }

        public void Update(GameTime gameTime, Vector2 playerPos, int width)
        {
            floatingValue += .25f;
            a -= 4;
            drawColor = new Color(color.R, color.G, color.B) * ((float)a / (float)255);
            SpriteFont font = Game1.GameContent.Load<SpriteFont>("SpriteFont1");
            float messageLength =  font.MeasureString(message).Length();
            position = new Vector2((width - (messageLength) / 2) + playerPos.X, playerPos.Y - floatingValue);

            timer++;
            if (timer > timeLength)
                timer = timeLength;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont1"), 
                message, position, drawColor);
        }
    }
}
