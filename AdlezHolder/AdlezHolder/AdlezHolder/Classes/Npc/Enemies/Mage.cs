using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class Mage : Enemy
    {
        Orientation blastDirec;
        Texture2D blastText;
        Rectangle blastRec;
        bool hasCross;
        int blastDis;

        ParticleEngine pEngine;

        public Mage(Texture2D defaultTexture, float scaleFactor, Vector2 startPosition)
            : base(defaultTexture, scaleFactor, 7, startPosition)
        {
            pEngine = new ParticleEngine();
            blastText = Game1.GameContent.Load<Texture2D>("White Block");
            blastRec = new Rectangle(0, 0, 10, 10);


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

        protected override void attack(Map data)
        {
            base.attack(data);
            Vector2 velocity = data.Player.Center - Center;

            if (Math.Abs(data.Player.Center.X - Center.X) < CollisionRec.Height / 2
                || Math.Abs(data.Player.Center.Y - Center.Y) < CollisionRec.Width / 2)
            {
                if (!hasCross)
                {
                    blastDirec = direction;
                    isAttacking = true;
                    hasCross = true;                    
                    createShot();
                }
            }

            // shoot wave 
            if (hasCross)
            {
                // move rec based on direction
                switch (blastDirec)
                {
                    case Orientation.UP:
                        blastRec.Y -= speed * 4;
                        break;
                    case Orientation.DOWN:
                        blastRec.Y += speed * 4;
                        break;
                    case Orientation.LEFT:
                        blastRec.X -= speed * 4;
                        break;
                    case Orientation.RIGHT:
                        blastRec.X += speed * 4;
                        break;
                }
                // move untill hit a range
                blastDis += speed;

                if (blastDis >= AttackRange / 3)
                {
                    createBurst(new Vector2(blastRec.X, blastRec.Y), data.CurrentData);
                }
                else if (blastRec.Intersects(data.Player.CollisionRec))// check for objects
                {
                    data.Player.damage(Strength);
                    hasCross = false;
                    blastDis = 0;
                }
                
                foreach(BaseSprite sprite in data.CurrentData.AllObjects)
                    if (blastRec.Intersects(sprite.CollisionRec))
                    {
                        createBurst(new Vector2(blastRec.X, blastRec.Y), data.CurrentData);
                        break;
                    }


            }
        }

        protected override void wander()
        {
            base.wander();
            hasCross = false;
            blastDis = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (hasCross)
                spriteBatch.Draw(blastText, blastRec, Color.Blue);
        }

        private void createShot()
        {
            blastRec.X = (int)Center.X;
            blastRec.Y = (int)Center.Y;
        }

        private void createBurst(Vector2 startPos, MapDataHolder data)
        {

            hasCross = false;
            blastDis = 0;
            List<Particle> pStuff = pEngine.generateMageBurst(startPos, Strength);
            foreach(Particle p in pStuff)
            {
                data.addParticle(p);
            }
        }
    }
}
