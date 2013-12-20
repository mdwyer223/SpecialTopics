using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class CharacterVars : BaseSpriteVars
    {
        public Sword sword;
        public int speed;
        //Bow bow;
        //Bomb bomb;

        public int curHealth;

        private CharacterVars()
        {
        }

        public CharacterVars(Character inPlayer)
            : base(inPlayer)
        {
            sword = inPlayer.Sword;
            curHealth = inPlayer.HitPoints;
            speed = inPlayer.Speed;            
        }
    }
}
