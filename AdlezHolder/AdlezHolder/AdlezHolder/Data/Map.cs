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
        
        public void load(MapVars mapVar)
        {
            //this.currentMapData.load(mapVar.mapDataVar);
            this.player.load(mapVar.playerVar);
            currentMapData.changePlayer(player);
        }

        public Map()
        {
            player = new Character(Game1.GameContent.Load<Texture2D>("Alistar/F"),
                .04f, 800, 5, Vector2.Zero);
            player.Position = new Vector2((Game1.DisplayWidth / 2) - player.CollisionRec.Width,
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
            if (Game1.MainGameState == GameState.PLAYING)
            {
                currentMapData.Update(this, gameTime);
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
