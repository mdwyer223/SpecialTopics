using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class SingleTriggerObject : ImmovableObject
    {
        //Rectangle trigger; // make this its own separate class (ie pressure plate or switch)
        protected Trigger trigger;

        public SingleTriggerObject(Texture2D texture, Texture2D triggerFirstImage, 
            Texture2D triggerSecondImage, Vector2 objectStart, Vector2 triggerStart, float scaleFactor, float triggerScale,
            int displayWidth, float secondsToCrossScreen)
            : base(texture, scaleFactor, secondsToCrossScreen, objectStart)
        {
            this.trigger = new Trigger(triggerFirstImage, triggerSecondImage, 
                displayWidth, secondsToCrossScreen,triggerScale, triggerStart);
        }

        public virtual void Update(Map data, GameTime gametime)
        {
            if (!dead)
            {
                foreach (ImmovableObject obj in data.CurrentData.ImmovableObjects)
                {
                    if (data.Player.isColliding(trigger.CollisionRec) ||
                        trigger.CollisionRec.Intersects(obj.CollisionRec))
                    {
                        visible = false;
                        dead = true;
                        trigger.toggle();
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(Image, DrawnRec, Color.White);
            }
            if (trigger != null)
            {
                trigger.Draw(spriteBatch);
            }
        }
    }
}
