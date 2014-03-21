using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class VTurnWest : MapDataHolder
    {
        Vector2 rightStart, lTopStart, lBotStart;
        int rZWidth, rZHeight, lZHeight, lZWidth;

        protected string lastPlace = "";
        protected bool changePos = false;

        public VTurnWest(string id)
            : base()
        {
            lastPlace = id;

            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/Grass");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            lTopStart = new Vector2(-10, -10);
            lBotStart = new Vector2(-10, (backgroundRec.Height / 2) + 50);
            lZHeight = 160;
            lZWidth = 250;

            rightStart = new Vector2((backgroundRec.Width / 2) + 50, 0);
            rZHeight = backgroundRec.Height;
            rZWidth = (backgroundRec.Width / 2) - 50;

            placeForest(lTopStart, 50, lZHeight, lZWidth);
            placeForest(lBotStart, 50, lZHeight, lZWidth);

            placeForest(rightStart, 75, rZHeight, rZWidth);
        }

        public VTurnWest()
            : base()
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/Grass");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            lTopStart = new Vector2(-10, -10);
            lBotStart = new Vector2(-10, (backgroundRec.Height / 2) + 50);
            lZHeight = 160;
            lZWidth = 250;

            rightStart = new Vector2((backgroundRec.Width / 2) + 50, 0);
            rZHeight = backgroundRec.Height;
            rZWidth = (backgroundRec.Width / 2) - 50;

            placeForest(lTopStart, 50, lZHeight, lZWidth);
            placeForest(lBotStart, 50, lZHeight, lZWidth);

            placeForest(rightStart, 100, rZHeight, rZWidth);
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
