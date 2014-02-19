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
    public class SingleHoldTriggerObject : SingleTriggerObject
    {
        protected bool held, prevHold;

        public SingleHoldTriggerObject(Texture2D image, Texture2D triggerFirstImage, Texture2D triggerSecondImage,
            Vector2 objectStart, Vector2 triggerStart, float scaleFactor, float triggerScale, int displayWidth, float secondsToCrossScreen)
            : base(image, triggerFirstImage, triggerSecondImage, objectStart, triggerStart,
            scaleFactor, triggerScale, displayWidth, secondsToCrossScreen)
        {
        }

        public override void Update(Map data, GameTime gametime)
        {
            foreach (ImmovableObject obj in data.CurrentData.ImmovableObjects)
            {
                if (data.Player.isColliding(trigger.CollisionRec)
                    || trigger.CollisionRec.Intersects(obj.CollisionRec))
                {
                    visible = false;
                    dead = true;
                    held = true;
                    if (!prevHold)
                        trigger.toggle();
                }
                else
                {
                    held = false;
                }
            }

            if (!held)
            {
                visible = true;
                dead = false;
                if (prevHold)
                    trigger.toggle();
            }

            prevHold = held;
        }
    }
}
