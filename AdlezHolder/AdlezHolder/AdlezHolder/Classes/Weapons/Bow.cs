using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Bow
    {
        int damage, range;
        float speed;

        Color color;
        Vector2 velocity, start;
        Texture2D texture;

        List<Gem> gemList;
        int gemSlots;

        GemStruct lifestealStruct, freezeStruct, poisonStruct,
            lightningStruct, fireStruct;
        public new BowStruct SaveData
        {
            get
            {
                BowStruct myStruct = new BowStruct();
                myStruct.damage = Damage;
                myStruct.speed = speed;
                myStruct.range = Range;

                myStruct.maxGems = MaxGemSlots;
                myStruct.gemsData = new GemStruct[MaxGemSlots];
                for (int i = 0; i < gemList.Count; i++)
                    myStruct.gemsData[i] = this.gemList[i].SaveData;

                return myStruct;
            }
            set
            {
                damage = value.damage;
                speed = (int)(value.speed + .5f);
                gemSlots = value.maxGems;
                this.gemList = new List<Gem>();

                foreach (GemStruct gem in value.gemsData)
                    switch (gem.type)
                    {
                        case GemType.FIRE:
                            gemList.Add(new FireStone(gem));
                            break;
                        case GemType.FREEZE:
                            gemList.Add(new IceStone(gem));
                            break;
                        case GemType.LS:
                            gemList.Add(new VampiricStone(gem));
                            break;
                        case GemType.POISON:
                            gemList.Add(new PoisonStone(gem));
                            break;
                        case GemType.STUN:
                            gemList.Add(new LightningStone(gem));
                            break;
                    }
            }
        }
        
        public Vector2 Velocity
        {
            get{ return velocity; }
        }

        public int Damage
        {
            get { return damage; }
        }
       
        public int Range
        {
            get{ return range; }
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

        public Bow(float scaleFactor)
        {   
            color = Color.White;
            gemList = new List<Gem>();
            gemSlots = 3;

            lifestealStruct = new GemStruct();
            freezeStruct = new GemStruct();
            fireStruct = new GemStruct();
            poisonStruct = new GemStruct();
            lightningStruct = new GemStruct();

            speed = 3;
            damage = 25;
            range = 300;

            gemList.Add(new IceStone(.02f, Vector2.Zero, 1, 1));
        }

        public void Update(Map data, GameTime gameTime)
        {
            if (data.Player.Direction == Orientation.DOWN)
            {
                velocity = new Vector2(0, speed);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowDown");
                start = new Vector2(data.Player.Position.X, data.Player.Position.Y + data.Player.CollisionRec.Height + 1);
            }
            else if (data.Player.Direction == Orientation.UP)
            {
                velocity = new Vector2(0, -speed);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowUp");
                start = new Vector2(data.Player.Position.X, data.Player.Position.Y - 30);
            }
            else if (data.Player.Direction == Orientation.RIGHT)
            {
                velocity = new Vector2(speed, 0);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowRight");
                start = new Vector2(data.Player.Position.X + data.Player.CollisionRec.Width + 1, data.Player.Position.Y);
            }
            else if (data.Player.Direction == Orientation.LEFT)
            {
                velocity = new Vector2(-speed, 0);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowLeft");
                start = new Vector2(data.Player.Position.X - 30, data.Player.Position.Y);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
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

        public void addArrow(Map data)
        {
            calcStats();
            Random rand = new Random();
            Projectile p = new Projectile(damage, range, speed, .035f,
                    velocity, start, texture);

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

            data.CurrentData.addProjectile(p); 
        }

        public void addGem(Gem gem)
        {
            if (gemList.Count == MaxGemSlots)
                return;

            this.gemList.Add(gem);
        }

        public void changeImage(Texture2D texture)
        {
            this.texture = texture;
        }
    }
}
