using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class Nwot : MapDataHolder
    {
        string lastPlace = "";
        bool changePos = false;

        Npc blacksmith, gStoreOwn;

        Building1 bL;
        Building2 bR;
        Building3 tL;
        Building4 tR;

        public Nwot()
            : base()
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/TownSquare");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            ImmovableObject fountain = new ImmovableObject(Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/FountainINTOWNSQUARE"),
                .2f, 0, new Vector2(323, 208));
            addImmovable(fountain);

            BuildingObject building = new BuildingObject(Game1.GameContent.Load<Texture2D>("Buildings/BottomLeftHouse"), .2f, new Vector2(5, 255));
            addImmovable(building);
            TripWire bottomLeft = new TripWire(.02f, new Rectangle(165, 327, 6, 53));
            addTripWire(bottomLeft);

            building = new BuildingObject(Game1.GameContent.Load<Texture2D>("Buildings/TopRightHouse"), .2f, new Vector2(640, 70));
            addImmovable(building);
            TripWire topRight = new TripWire(.02f, new Rectangle(627, 141, 6, 55));
            addTripWire(topRight);

            building = new BuildingObject(Game1.GameContent.Load<Texture2D>("Buildings/BottomRightHouse"), .2f, new Vector2(640, 255));
            addImmovable(building);
            TripWire botRight = new TripWire(.02f, new Rectangle(627, 324, 6, 58));
            addTripWire(botRight);

            building = new BuildingObject(Game1.GameContent.Load<Texture2D>("Buildings/TopLeftHouse"), .2f, new Vector2(5, 70));
            addImmovable(building);
            TripWire topLeft = new TripWire(.02f, new Rectangle(165, 137, 6, 55));
            addTripWire(topLeft);

            TripWire d1 = new TripWire(.02f, new Rectangle(0, backgroundRec.Height - 10, backgroundRec.Width, 10));
            addTripWire(d1);

            TripWire d2 = new TripWire(.02f, new Rectangle(0, 0, backgroundRec.Width, 10));
            addTripWire(d2);

            music = Game1.GameContent.Load<Song>("Music/TravelingSong");
        }

        public Nwot(string id)
            : base()
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/TownSquare");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            ImmovableObject fountain = new ImmovableObject(Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/FountainINTOWNSQUARE"),
                .2f, 0, new Vector2(323, 208));
            addImmovable(fountain);

            BuildingObject building = new BuildingObject(Game1.GameContent.Load<Texture2D>("Buildings/BottomLeftHouse"), .2f, new Vector2(5, 255));
            addImmovable(building);
            TripWire bottomLeft = new TripWire(.02f, new Rectangle(165, 327, 6, 53));
            addTripWire(bottomLeft);

            building = new BuildingObject(Game1.GameContent.Load<Texture2D>("Buildings/TopRightHouse"), .2f, new Vector2(640, 70));
            addImmovable(building);
            TripWire topRight = new TripWire(.02f, new Rectangle(627, 141, 6, 55));
            addTripWire(topRight);

            building = new BuildingObject(Game1.GameContent.Load<Texture2D>("Buildings/BottomRightHouse"), .2f, new Vector2(640, 255));
            addImmovable(building);
            TripWire botRight = new TripWire(.02f, new Rectangle(627, 324, 6, 58));
            addTripWire(botRight);

            building = new BuildingObject(Game1.GameContent.Load<Texture2D>("Buildings/TopLeftHouse"), .2f, new Vector2(5, 70));
            addImmovable(building);
            TripWire topLeft = new TripWire(.02f, new Rectangle(165, 137, 6, 55));
            addTripWire(topLeft);

            TripWire d1 = new TripWire(.02f, new Rectangle(0, backgroundRec.Height - 10, backgroundRec.Width, 10));
            addTripWire(d1);

            TripWire d2 = new TripWire(.02f, new Rectangle(0, 0, backgroundRec.Width, 10));
            addTripWire(d2);

            music = Game1.GameContent.Load<Song>("Music/TravelingSong");

            lastPlace = id;
        }

        public override void Update(Map map, GameTime gameTime)
        {
            bool changeBL = false, changeTL = false,
                changeBR = false, changeTR = false,
                changeD1 = false, changeD2 = false;

            if (!changePos)
            {
                if (lastPlace.Equals("d1first"))
                {
                    map.Player.Position = new Vector2((backgroundRec.Width / 2) - map.Player.DrawnRec.Width, backgroundRec.Height - map.Player.DrawnRec.Height - 5);
                }
                else if (lastPlace.Equals("d2first"))
                {
                    map.Player.Position = new Vector2((backgroundRec.Width / 2) - map.Player.DrawnRec.Width, map.Player.DrawnRec.Height + 5);
                }
                lastPlace = "";
                changePos = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Game1.ParticleState = ParticleState.RAIN;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                Game1.ParticleState = ParticleState.OFF;
            }

            for (int i = 0; i < tripWires.Count; i++)
            {
                tripWires[i].Update(map.Player.CollisionRec);

                if (tripWires[i].IfTripped && i == 0 && 
                    map.Player.Direction == Orientation.LEFT)
                {
                    bL = new Building1(map.Player);
                    changeBL = true;
                }
                if (tripWires[i].IfTripped && i == 1 && 
                    map.Player.Direction == Orientation.RIGHT)
                {
                    tR = new Building4(map.Player);
                    changeTR = true;
                }
                if (tripWires[i].IfTripped && i == 2 &&
                    map.Player.Direction == Orientation.RIGHT)
                {
                    bR = new Building2(map.Player);
                    changeBR = true;
                }
                if (tripWires[i].IfTripped && i == 3 &&
                    map.Player.Direction == Orientation.LEFT)
                {
                    tL = new Building3(map.Player);
                    changeTL = true;
                }
                if (tripWires[i].IfTripped && i == 4 &&
                    map.Player.Direction == Orientation.DOWN)
                {
                    changeD1 = true;
                }
                if (tripWires[i].IfTripped && i == 5 &&
                    map.Player.Direction == Orientation.UP)
                {
                    changeD2 = true;
                }
            }

            if (changeBL)
            {
                map.changeMap(bL);
            }
            else if (changeBR)
            {
                map.changeMap(bR);
            }
            else if (changeTL)
            {
                map.changeMap(tL);
            }
            else if (changeTR)
            {
                map.changeMap(tR);
            }
            else if (changeD1)
            {
                map.changeMap(new D1First("nwot"));
            }
            else if (changeD2)
            {
                map.changeMap(new D2First("nwot"));
            }
            else
            {
                base.Update(map, gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
