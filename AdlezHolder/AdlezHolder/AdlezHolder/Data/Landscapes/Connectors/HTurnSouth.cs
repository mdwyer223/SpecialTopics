using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class HTurnSouth : MapDataHolder
    {
        Vector2 rBotStart, lBotStart, topStart;
        int rZWidth, rZHeight, lZWidth, lZHeight, topZHeight, topZWidth;

        protected string lastPlace = "";
        protected bool changePos = false;

        public HTurnSouth(string id)
            : base()
        {
            lastPlace = id;

            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/Grass");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            rBotStart = new Vector2((backgroundRec.Width / 2) + 50, (backgroundRec.Height / 2) + 50);
            rZHeight = 160;
            rZWidth = 325;

            lBotStart = new Vector2(-10, (backgroundRec.Height / 2) + 50);
            lZHeight = 160;
            lZWidth = 250;

            topStart = new Vector2(-10, 0);
            topZHeight = 175;
            topZWidth = backgroundRec.Width;

            placeForest(rBotStart, 50, rZHeight, rZWidth);
            placeForest(lBotStart, 50, lZHeight, lZWidth);
            placeForest(topStart, 100, topZHeight, topZWidth);
        }

        public HTurnSouth()
            : base()
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/Grass");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            rBotStart = new Vector2((backgroundRec.Width / 2) + 50, (backgroundRec.Height / 2) + 50);
            rZHeight = 160;
            rZWidth = 325;

            lBotStart = new Vector2(-10, (backgroundRec.Height / 2) + 50);
            lZHeight = 160;
            lZWidth = 250;

            topStart = new Vector2(-10, 0);
            topZHeight = 175;
            topZWidth = backgroundRec.Width;

            placeForest(rBotStart, 50, rZHeight, rZWidth);
            placeForest(lBotStart, 50, lZHeight, lZWidth);
            placeForest(topStart, 100, topZHeight, topZWidth);
        }

        public override void Update(Map map, GameTime gameTime)
        {
            
            base.Update(map, gameTime);
        }

        protected void placeForest(Vector2 start, int count, int height, int width)
        {
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                ImmovableObject tree = new ImmovableObject(Game1.GameContent.Load<Texture2D>("NatureObjects/Tree" + rand.Next(1, 4)), .08f, 0,
                    new Vector2(rand.Next((int)start.X, (int)(start.X + width)), rand.Next((int)start.Y, (int)(start.Y + height))));
                addImmovable(tree);
            }
        }
    }
}
