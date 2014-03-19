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
        public MapStruct mapStruct;

        public GameData()
        {
            Map map = new Map();
            map.changeMap(new MainRoom());

            //mapStruct = map.SaveData;
        }

        public GameData(Map inMap)
        {
            //mapStruct = inMap.SaveData;
        }  

        public GameData clone()
        {
            GameData newData = new GameData();
            newData.mapStruct = this.mapStruct;

            return newData;
        }

    }
}
