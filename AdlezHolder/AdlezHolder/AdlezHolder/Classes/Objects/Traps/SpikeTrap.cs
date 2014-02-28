using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class SpikeTrap : BaseSprite
    {
        int intervalActive, intervalInactive, 
            activeTimer, inactiveTimer, currentFrame;
        bool active, inactive;
        Texture2D[] spikes;

        public override BaseSpriteStruct SaveData
        {
            get
            {
                BaseSpriteStruct myData = base.SaveData;
                myData.saveId = "BSt";
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
        /// <param name="spikes">0 is the standard image, last index is the spikes fully up</param>
        /// <param name="scaleFactor"></param>
        /// <param name="startPosition"></param>
        /// <param name="timeActive"></param>
        /// <param name="timeInactive"></param>
        public SpikeTrap(Texture2D[] spikes, float scaleFactor, Vector2 startPosition, int timeActive, int timeInactive)
            : base(spikes[0], scaleFactor, Game1.DisplayWidth, 0, startPosition)
        {
            this.spikes = spikes;
            this.intervalActive = timeActive;
            this.intervalInactive = timeInactive;

            inactive = true;
            active = false;
        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (active)
            {
                activeTimer++;
                if (activeTimer >= intervalActive)
                {
                    active = false;
                    inactive = true;
                    activeTimer = 0;
                }
                if(currentFrame + 1 < spikes.Length)
                {
                    currentFrame++;
                }
            }
            else if (inactive)
            {
                inactiveTimer++;
                if (inactiveTimer >= intervalInactive)
                {
                    inactive = false;
                    active = true;
                    inactiveTimer = 0;
                }
                if (currentFrame - 1 >= 0)
                {
                    currentFrame--;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spikes[currentFrame], drawnRec, color);
        }
    }
}
