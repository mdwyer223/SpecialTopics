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
    public class MultiTriggerObject : ImmovableObject
    {
        protected Trigger[] triggers;

        public MultiTriggerObject(Game1 game, Texture2D texture, Texture2D triggerFirstImage,
            Texture2D triggerSecondImage, Vector2 objectStart, Trigger[] triggers, float scaleFactor,
            int displayWidth, float secondsToCrossScreen)
            : base(texture, scaleFactor, secondsToCrossScreen, objectStart)
        {
            this.triggers = triggers;
        }

        public virtual void Update(Map data, GameTime gametime)
        {
            if (!dead)
            {
                foreach (ImmovableObject obj in data.CurrentData.ImmovableObjects)
                {
                    foreach (Trigger trigger in triggers)
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(Image, drawnRec, Color.White);
            }
            foreach (Trigger trigger in triggers)
            {
                if (trigger != null)
                {
                    trigger.Draw(spriteBatch);
                }
            }
        }
    }
}
