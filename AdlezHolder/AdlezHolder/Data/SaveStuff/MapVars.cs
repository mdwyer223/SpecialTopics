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
    public class MapVars
    {
        public MapDataHolderVars mapDataVar;
        public CharacterVars playerVar;

        private MapVars()
        {
        }

        public MapVars(MapDataHolderVars inMDHVars, CharacterVars inPlayerVar)
        { 
            mapDataVar = inMDHVars;
            playerVar = inPlayerVar;            
        }

        public MapVars(Map inMap)
        {
            mapDataVar = new MapDataHolderVars(inMap.CurrentData);
            playerVar = new CharacterVars(inMap.Player);
            
        }
    }
}
