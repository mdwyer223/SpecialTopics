using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class Map
    {
        MapDataHolder currentMapData;
        Character player;

        public MapStruct SaveData
        {
            get
            {
                MapStruct saveData = new MapStruct();
                saveData.mapData = CurrentData.SaveData;
                saveData.playerData = Player.SaveData;
                return saveData;
            }
        }

        public MapDataHolder CurrentData
        {
            get { return currentMapData; }
            protected set { currentMapData = value; }
        }

        public Character Player
        {
            get { return player; }
            protected set { player = value; }
        }

        //ARRAY OF DOORS/TRIPWIRES/RECTANGLES to link to next map
        //each map will have an index to align each rectangle with a certain map, will go in order from the town hub 
        //to each individual dungeon map. Set up folders to differentiate which mapdatas are held where. The index won't 
        //be some complicated algorithm.

        public void load(MapStruct mapStruct)
        {
            //TODO: create the proper MapDataHoler
            this.player.load(mapStruct);
            currentMapData.changePlayer(player);

            switch (mapStruct.mapData.mapId)
            {
                case "bHall":
                    this.currentMapData = new BossHall(player);
                    currentMapData.load(mapStruct);
                    break;
                case "bRoom1":
                    this.currentMapData = new BossRoom(player);
                    currentMapData.load(mapStruct);
                    break;
                case "bLCorner":
                    this.currentMapData = new BottomLeftCorner(player);
                    currentMapData.load(mapStruct);
                    break;
                case "bRCorner":
                    this.currentMapData = new BottomRightCorner(player);
                    currentMapData.load(mapStruct);
                    break;
                case "lHallway":
                    this.currentMapData = new LeftHallway(player);
                    currentMapData.load(mapStruct);
                    break;
                case "mainRoom1":
                    this.currentMapData = new MainRoom(player);
                    currentMapData.load(mapStruct);
                    break;
                case "rHallway":
                    this.currentMapData = new RightHallway(player);
                    currentMapData.load(mapStruct);
                    break;
                case "tLCorner":
                    this.currentMapData = new TopLeftCorner(player);
                    currentMapData.load(mapStruct);
                    break;
                case "tRCorner":
                    this.currentMapData = new TopRightCorner(player);
                    currentMapData.load(mapStruct);
                    break;
                case "lPassage"://this will probably be changed to a better name later
                    this.currentMapData = new LeftPassage();
                    currentMapData.load(mapStruct);
                    break;
                case "nwot":
                    this.currentMapData = new Nwot();
                    currentMapData.load(mapStruct);
                    break;
                case "d1first":
                    this.currentMapData = new D1First();
                    currentMapData.load(mapStruct);
                    break;
                case "d1second":
                    this.currentMapData = new D1Second("");
                    currentMapData.load(mapStruct);
                    break;
                case "d1third":
                    this.currentMapData = new D1Third("");
                    currentMapData.load(mapStruct);
                    break;
                case "d2first":
                    this.currentMapData = new D2First("");
                    currentMapData.load(mapStruct);
                    break;
                case "d2second":
                    this.currentMapData = new D2Second("");
                    currentMapData.load(mapStruct);
                    break;
                case "d2third":
                    this.currentMapData = new D2Third("");
                    currentMapData.load(mapStruct);
                    break;                    
            }

            changeMap(this.CurrentData);
        }

        public Map()
        {
            player = new Character(Game1.GameContent.Load<Texture2D>("Alistar/F"),
                .04f, 800, 5, Vector2.Zero);
            player.Position = new Vector2((Game1.DisplayWidth / 2) - (player.CollisionRec.Width / 2),
                    Game1.DisplayHeight - player.CollisionRec.Height - 5);
        }

        public Map(ContentManager Content)
        {
            //Content.load...;
            //either arrays of stuff passed in, or other stuff
        }

        public Map(ContentManager Content, MapDataHolder mapData, Viewport screen)
        {
            currentMapData = mapData; //adjust to have each map be initialized
        }

        public void changeMap(MapDataHolder map)
        {
            currentMapData = map;
            currentMapData.changePlayer(player);
            Game1.ParticleState = ParticleState.OFF;
        }

        public void Update(GameTime gameTime)
        {
            if (Game1.MainGameState == GameState.PLAYING || Game1.MainGameState == GameState.CUTSCENE || Game1.MainGameState == GameState.TALKING)
            {
                currentMapData.Update(this, gameTime);
            }
            if (Game1.MainGameState == GameState.PLAYING)
            {
                player.Update(this, gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentMapData.Draw(spriteBatch);
            //player.Draw(spriteBatch);
        }
    }
}
