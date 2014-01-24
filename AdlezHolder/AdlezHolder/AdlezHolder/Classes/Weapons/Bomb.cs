using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Bomb
    {
        Texture2D texture;
        ParticleEngine pEngine;
        List<BombObj> bombs;

        int damage, delay, speed, numParticles;
        float scaleFactor;

        List<Gem> gemList;
        int gemSlots;

        GemStruct lifestealStruct, freezeStruct, poisonStruct,
            lightningStruct, fireStruct;

        public int Damage
        {
            get { return damage; }
        }

        public int Delay
        {
            get { return delay / 1000; }
        }

        public int MaxGemSlots
        {
            get { return gemSlots; }
        }

        public List<Gem> Gems
        {
            get { return gemList; }
        }

        public GemStruct LifeStealStruct
        {
            get { return lifestealStruct; }
        }

        public GemStruct FireStruct
        {
            get { return fireStruct; }
        }

        public GemStruct PoisonStruct
        {
            get { return poisonStruct; }
        }

        public GemStruct StunStruct
        {
            get { return lightningStruct; }
        }

        public GemStruct IceStruct
        {
            get { return freezeStruct; }
        }

        public Bomb(float scaleFactor)
        {
            bombs = new List<BombObj>();
            pEngine = new ParticleEngine();
            gemList = new List<Gem>();
            gemSlots = 3;

            lifestealStruct = new GemStruct();
            freezeStruct = new GemStruct();
            fireStruct = new GemStruct();
            poisonStruct = new GemStruct();
            lightningStruct = new GemStruct();

            damage = 50;
            delay = 3000;

            this.scaleFactor = scaleFactor;
            texture = Game1.GameContent.Load<Texture2D>("Weapons/Bomb");
        }

        public void Update(Map data, GameTime gameTime)
        {
            for (int i = 0; i < bombs.Count; i++)
            {
                if (bombs[i] != null)
                {
                    bombs[i].Update(data, gameTime);

                    if (bombs[i].BlowUp)
                    {
                        blowUp(data, bombs[i].Center);
                        bombs.RemoveAt(i);
                        i--;
                        if (i < 0)
                            i = 0;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BombObj bomb in bombs)
            {
                if (bomb != null)
                {
                    //bomb.Draw(spriteBatch);
                }
            }
        }

        public void calcStats()
        {
            float burnChance = 0f, poisonChance = 0f, lightningChance = 0f,
               freezeChance = 0f, vampirePercent = 0f;
            float burnDuration = 0f, poisonDuration = 0f, stunDuration = 0f,
                freezeDuration = 0f;
            int burnDamage = 0, poisonDamage = 0, freezeDamage = 0;

            for (int i = 0; i < gemList.Count; i++)
            {
                if (gemList[i].GetType() == typeof(FireStone))
                {
                    burnChance += gemList[i].Chance;
                    burnDamage += gemList[i].Damage;
                    burnDuration += gemList[i].Duration;
                }
                if (gemList[i].GetType() == typeof(VampiricStone))
                {
                    vampirePercent += gemList[i].Chance;
                }
                if (gemList[i].GetType() == typeof(PoisonStone))
                {
                    poisonChance += gemList[i].Chance;
                    poisonDamage += gemList[i].Damage;
                    poisonDuration += gemList[i].Duration;
                }
                if (gemList[i].GetType() == typeof(IceStone))
                {
                    freezeChance += gemList[i].Chance;
                    IceStone stone = (IceStone)(gemList[i]);
                    freezeDamage += (int)(stone.CritDamage * damage) + damage;
                    freezeDuration += gemList[i].Duration;
                    stone = null;
                }
                if (gemList[i].GetType() == typeof(LightningStone))
                {
                    lightningChance += gemList[i].Chance;
                    stunDuration += gemList[i].Duration;
                }
            }

            lifestealStruct.chance = vampirePercent;

            fireStruct.chance = burnChance;
            fireStruct.damage = burnDamage;
            fireStruct.duration = burnDuration;

            poisonStruct.chance = poisonChance;
            poisonStruct.damage = poisonDamage;
            poisonStruct.duration = poisonDuration;

            freezeStruct.chance = freezeChance;
            freezeStruct.damage = freezeDamage;
            freezeStruct.duration = freezeDuration;

            lightningStruct.chance = lightningChance;
            lightningStruct.duration = stunDuration;
        }

        public void blowUp(Map data, Vector2 start)
        {
            List<Particle> particles = pEngine.generateExplosion(start, damage);
            calcStats();
            Random rand = new Random();

            foreach (Particle p in particles)
            {
                if (rand.NextDouble() < lightningStruct.chance)
                {
                    p.addGemStruct(lightningStruct);
                    p.addGemType(GemType.STUN);
                }
                if (rand.NextDouble() < poisonStruct.chance)
                {
                    p.addGemStruct(poisonStruct);
                    p.addGemType(GemType.POISON);
                }
                if (rand.NextDouble() < freezeStruct.chance)
                {
                    p.addGemStruct(freezeStruct);
                    p.addGemType(GemType.FREEZE);
                }
                if (rand.NextDouble() < fireStruct.chance)
                {
                    p.addGemStruct(fireStruct);
                    p.addGemType(GemType.FIRE);
                }
                if (rand.NextDouble() < lifestealStruct.chance)
                {
                    p.addGemStruct(lifestealStruct);
                    p.addGemType(GemType.LS);
                }
                data.CurrentData.addParticle(p);
            }

            particles = null;
        }

        public void addGem(Gem gem)
        {
            if (gemList.Count == MaxGemSlots)
                return;

            this.gemList.Add(gem);
        }

        public void addBomb(Vector2 start, MapDataHolder data)
        {
            BombObj obj = new BombObj(texture, scaleFactor,
                start, delay);
            bombs.Add(obj);
            data.addBaseSprite(obj);

        }
    }
}
