using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    [Serializable]
    public class GameData
    {
        public MapVars mapVars;

        public GameData()
        {
            Map map = new Map();
            map.changeMap(new MainRoom());

            mapVars = new MapVars(map);
        }

        public GameData(Map inMap)
        {
            mapVars = new MapVars(inMap);
        }  

        public GameData clone()
        {
            GameData newData = new GameData();
            newData.mapVars = this.mapVars;

            return newData;
        }

        //public Map getMap()
        //{
        //    return new Map(new MapVars(mapVars.mapDataVar, mapVars.playerVar));
        //}

    }
}
