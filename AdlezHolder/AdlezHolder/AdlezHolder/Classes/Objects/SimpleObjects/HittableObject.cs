using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class HittableObject : ImmovableObject
    {
        int hitpoints, initialHitpoints;

        Texture2D[] phases;
        int currentPhase, breakFrameTimer;
        bool breaking;

        public override BaseSpriteStruct SaveData
        {
            get
            {
                BaseSpriteStruct myData = base.SaveData;
                myData.saveId = "IHi";
                return myData;
            }
            set
            {
                base.SaveData = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phases">0 is the standard image, the last two frames, should be the object acually breaking, so it can dissapear</param>
        /// <param name="scaleFactor"></param>
        /// <param name="secondsToCrossScreen"></param>
        /// <param name="start"></param>
        /// <param name="hitpoints"></param>
        public HittableObject(Texture2D[] phases, float scaleFactor, int secondsToCrossScreen, Vector2 start, 
            int hitpoints)
            : base(phases[0], scaleFactor, secondsToCrossScreen, start)
        {
            this.hitpoints = initialHitpoints = hitpoints;
            this.phases = phases;
            currentPhase = 0;

            this.visible = true;
            this.dead = false;
            this.breaking = false;
        }

        public virtual void toggle()
        {
            IsVisible = false;
            IsDead = true;
        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (breaking)
            {
                breakFrameTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (breakFrameTimer >= 100)
                {
                    playBreak();
                    breakFrameTimer = 0;
                }
            }

            if (!this.dead && !data.Player.Sword.isDead)
            {
                if (data.Player.Sword.CollisionRec.Intersects(this.CollisionRec))
                {
                    damage(data.Player.Sword.Damage);
                }
            }

            base.Update(data);
        }

        protected virtual void playBreak()
        {
            if (currentPhase != phases.Length - 1)
            {
                currentPhase++;
                setImage(phases[currentPhase]);
            }
            else
            {
                toggle();
            }
        }

        protected void damage(int damage)
        {
            hitpoints -= damage;

            if (hitpoints <= 0)
            {
                breaking = true;
            }

            for (int i = currentPhase; i < phases.Length - 2; i++)
            {
                if (initialHitpoints - ((((float)hitpoints / (float)initialHitpoints)) * 100) <= i * 20)
                {
                    currentPhase = i;
                    setImage(phases[i]);
                    break;
                }
            }

        }
    }
}
