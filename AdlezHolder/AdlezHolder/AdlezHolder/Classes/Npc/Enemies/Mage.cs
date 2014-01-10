using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class Mage : RangedEnemy
    {
        ParticleEngine pEngine;

        public Mage(Texture2D defaultTexture, float scaleFactor, Vector2 startPosition)
            : base(defaultTexture, scaleFactor, 7, startPosition)
        {
            pEngine = new ParticleEngine();
            projectileText = Game1.GameContent.Load<Texture2D>("Random/Particle");
            projectileRec = new Rectangle(0, 0, 10, 10);


            // load in animations
            ContentManager content = Game1.GameContent;
            Texture2D[] left, right, forward, backward;

            left = new Texture2D[3];
            right = new Texture2D[3];
            forward = new Texture2D[3];
            backward = new Texture2D[3];

            string moveDirec = "ComputerPpl/Enemies/Mage/Move/";
            string attcDirec = "ComputerPpl/Enemies/Mage/Attack/";

            backward[0] = content.Load<Texture2D>(moveDirec + "B");
            backward[1] = content.Load<Texture2D>(moveDirec + "BR");
            backward[2] = content.Load<Texture2D>(moveDirec + "BL");

            forward[0] = content.Load<Texture2D>(moveDirec + "F");
            forward[1] = content.Load<Texture2D>(moveDirec + "FR");
            forward[2] = content.Load<Texture2D>(moveDirec + "FL");

            left[0] = content.Load<Texture2D>(moveDirec + "L");
            left[1] = content.Load<Texture2D>(moveDirec + "LR");
            left[2] = content.Load<Texture2D>(moveDirec + "LL");

            right[0] = content.Load<Texture2D>(moveDirec + "R");
            right[1] = content.Load<Texture2D>(moveDirec + "RR");
            right[2] = content.Load<Texture2D>(moveDirec + "RL");
            move = new FullAnimation(backward, forward, left, right, .2f);

            left = new Texture2D[1];
            right = new Texture2D[1];
            forward = new Texture2D[1];
            backward = new Texture2D[1];

            forward[0] = content.Load<Texture2D>(moveDirec + "F");
            backward[0] = content.Load<Texture2D>(moveDirec + "B");
            left[0] = content.Load<Texture2D>(moveDirec + "L");
            right[0] = content.Load<Texture2D>(moveDirec + "R");
            idle = new FullAnimation(backward, forward, left, right, .2f);

            //left = new Texture2D[2];
            //right = new Texture2D[2];
            //forward = new Texture2D[2];
            //backward = new Texture2D[2];

            //forward[0] = content.Load<Texture2D>(attcDirec + "F1");
            //forward[1] = content.Load<Texture2D>(attcDirec + "F2");
            //backward[0] = content.Load<Texture2D>(attcDirec + "B1");
            //backward[1] = content.Load<Texture2D>(attcDirec + "B2");
            //left[0] = content.Load<Texture2D>(attcDirec + "L1");
            //left[1] = content.Load<Texture2D>(attcDirec + "L2");
            //right[0] = content.Load<Texture2D>(attcDirec + "R1");
            //right[1] = content.Load<Texture2D>(attcDirec + "R2");
            //attackAn = new FullAnimation(backward, forward, left, right, .2f);

        }

        protected override void setAttributes()
        {
            AttackRangeMod = 2.5f;
            HitPoint = MaxHealthPoints = 70;
            Strength = 10;
        }

        protected override void projectileEnd(Map data, BaseSprite collidedObject, Vector2 position)
        {
            base.projectileEnd(data, collidedObject, position);
            List<Particle> pStuff = pEngine.generateMageBurst(position, Strength);
            foreach (Particle p in pStuff)
            {
                data.CurrentData.addParticle(p);
            }
        }


    }
}
