using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AdlezHolder
{
    class DeathAnimation : Cutscene
    {
        //Rectangle dFront, dBack, dLeft, dRight;
        int playOrder;
        float speed = 3;
        Vector2 currentVelo;
        public DeathAnimation()
        {
           
        }

        public override void play(GameTime gameTime)
        {
            if (player.Direction == Orientation.RIGHT)
            {
                currentVelo = new Vector2(-1, 0);
            }
            else if (player.Direction == Orientation.LEFT)
            {
                currentVelo = new Vector2(1, 0);
            }
            else if (player.Direction == Orientation.UP)
            {
                currentVelo = new Vector2(0, 1);
            }
            else if (player.Direction == Orientation.DOWN)
            {
                currentVelo = new Vector2(0, -1);
            }

            if (playOrder == 0)
            {
                Texture2D[] images = new Texture2D[4];

                images[0] = Game1.GameContent.Load<Texture2D>("Alistar/Dead Alistar/Dead Back");
                images[1] = Game1.GameContent.Load<Texture2D>("Alistar/Dead Alistar/Dead Front");
                images[2] = Game1.GameContent.Load<Texture2D>("Alistar/Dead Alistar/Dead Left");
                images[3] = Game1.GameContent.Load<Texture2D>("Alistar/Dead Alistar/Dead Right");

                player.setIdle(images);
                playOrder++;
            }
            else if (playOrder == 1)
            {
                deacySpeed();
                adjustPlayer(currentVelo, speed);

                if (speed <= .01f)
                    over = true;
            }
        }

        public void deacySpeed()
        {
            speed *= .9f;
        }

        protected void adjustPlayer(Vector2 velo, float newSpeed)
        {
            player.Position += (velo * newSpeed);
        }


        
    }
}
