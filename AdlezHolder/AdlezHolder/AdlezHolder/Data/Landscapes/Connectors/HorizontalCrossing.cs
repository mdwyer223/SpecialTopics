using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class HorizontalCrossing : MapDataHolder
    {
        Vector2 topStart, botStart;
        int topZHeight, topZWidth, botZHeight, botZWidth;

        protected string lastPlace = "";
        protected bool changePos = false;

        public HorizontalCrossing(string id)
            : base()
        {
            lastPlace = id;

            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/Grass");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            topStart = new Vector2(-10, 0);
            topZHeight = 175;
            topZWidth = backgroundRec.Width;
            botStart = new Vector2(-10, (backgroundRec.Height /2) + 50);
            botZHeight = 175;
            botZWidth = backgroundRec.Width;

            placeForest(topStart, 70, topZHeight, topZWidth);
            placeForest(botStart, 70, botZHeight, botZWidth);
        }

        public HorizontalCrossing()
            : base()
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/Grass");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            topStart = new Vector2(-10, 0);
            topZHeight = 175;
            topZWidth = backgroundRec.Width;
            botStart = new Vector2(-10, (backgroundRec.Height / 2) + 50);
            botZHeight = 175;
            botZWidth = backgroundRec.Width;

            placeForest(topStart, 70, topZHeight, topZWidth);
            placeForest(botStart, 70, botZHeight, botZWidth);
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
