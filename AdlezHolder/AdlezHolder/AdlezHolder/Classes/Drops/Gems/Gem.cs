using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public abstract class Gem : Item
    {
        protected int damage, tier;
        protected bool collected;
        float applyDamageTime, chance;

        public new GemStruct SaveData
        {
            get
            {
                GemStruct data = new GemStruct();
                data.damage = Damage;
                data.chance = Chance;
                data.duration = Duration;
                if (this.GetType() == typeof(FireStone))
                    data.type = GemType.FIRE;
                else if (this.GetType() == typeof(IceStone))
                    data.type = GemType.FREEZE;
                else if (this.GetType() == typeof(VampiricStone))
                    data.type = GemType.LS;
                else if (this.GetType() == typeof(PoisonStone))
                    data.type = GemType.POISON;
                else if (this.GetType() == typeof(LightningStone))
                    data.type = GemType.STUN;   
                                
                return data;
            }
            set
            {
                damage = value.damage;
                chance = value.chance;
                Duration = value.duration;
            }


        }

        public int Damage
        {
            get { return damage; }
            protected set { damage = value; }
        }

        public float Chance
        {
            get { return chance; }
            protected set { chance = value; }
        }

        public float Duration
        {
            get { return applyDamageTime; }
            protected set { applyDamageTime = value; }
        }        

        public Gem(GemStruct gemData)
        {
            this.SaveData = gemData;
        }

        public Gem(Texture2D texture, float scaleFactor, Vector2 startPosition, string tag, int value, int numberOf)
            :base(texture, scaleFactor, startPosition, tag, true, false, false, value, numberOf)
        {
        }
    }
}
