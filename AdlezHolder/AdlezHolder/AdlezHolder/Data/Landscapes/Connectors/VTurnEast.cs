using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class VTurnEast : MapDataHolder
    {
        Vector2 rTopStart, rBotStart, leftStart;
        int rZWidth, rZHeight, lZWidth, lZHeight;

        protected string lastPlace = "";
        protected bool changePos = false;

        public VTurnEast(string id)
            : base()
        {
            lastPlace = id;

            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/Grass");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);


            rTopStart = new Vector2((backgroundRec.Width / 2) + 50, -10);
            rBotStart = new Vector2((backgroundRec.Width / 2) + 50, (backgroundRec.Height / 2) + 50);
            rZHeight = 160;
            rZWidth = 325;

            leftStart = new Vector2(-10, 0);
            lZHeight = backgroundRec.Height;
            lZWidth = (backgroundRec.Width / 2) - 75;

            placeForest(rTopStart, 50, rZHeight, rZWidth);
            placeForest(rBotStart, 50, rZHeight, rZWidth);

            placeForest(leftStart, 70, lZHeight, lZWidth);
        }

        public VTurnEast()
            : base()
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/Grass");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);


            rTopStart = new Vector2((backgroundRec.Width / 2) + 50, -10);
            rBotStart = new Vector2((backgroundRec.Width / 2) + 50, (backgroundRec.Height / 2) + 50);
            rZHeight = 160;
            rZWidth = 325;

            leftStart = new Vector2(-10, 0);
            lZHeight = backgroundRec.Height;
            lZWidth = (backgroundRec.Width / 2) - 125;

            placeForest(rTopStart, 50, rZHeight, rZWidth);
            placeForest(rBotStart, 50, rZHeight, rZWidth);

            placeForest(leftStart, 70, lZHeight, lZWidth);
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
