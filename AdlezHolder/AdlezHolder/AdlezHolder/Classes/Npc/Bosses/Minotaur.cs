using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class Minotaur : Enemy
    {
        Orientation waveDirec;
        Texture2D waveText;
        Rectangle waveRec;
        bool hasCross;
        int waveDis;

        public Minotaur(Texture2D defaultTexture, float scaleFactor, int SecondsToCrossScreen, Vector2 startPosition)
            :base(defaultTexture, scaleFactor, SecondsToCrossScreen, startPosition)
        {
            // load in animations
            ContentManager content = Game1.GameContent;
            Texture2D[] left, right, forward, backward;

            left = new Texture2D[3];
            right = new Texture2D[3];
            forward = new Texture2D[3];
            backward = new Texture2D[3];

            string moveDirec = "ComputerPpl/Bosses/D1 Boss/Move/";
            string attcDirec = "ComputerPpl/Bosses/D1 Boss/Attack/";

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

            left = new Texture2D[2];
            right = new Texture2D[2];
            forward = new Texture2D[2];
            backward = new Texture2D[2];

            forward[0] = content.Load<Texture2D>(attcDirec + "F1");
            forward[1] = content.Load<Texture2D>(attcDirec + "F2");
            backward[0] = content.Load<Texture2D>(attcDirec + "B1");
            backward[1] = content.Load<Texture2D>(attcDirec + "B2");
            left[0] = content.Load<Texture2D>(attcDirec + "L1");
            left[1] = content.Load<Texture2D>(attcDirec + "L2");
            right[0] = content.Load<Texture2D>(attcDirec + "R1");
            right[1] = content.Load<Texture2D>(attcDirec + "R2");
            attackAn = new FullAnimation(backward, forward, left, right, .2f);
        }

        protected override void setAttributes()
        {
            Strength = 5;
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
                    isAttacking = true;
                    hasCross = true;
                    createWave();
                } 
            }

            // shoot wave 
            if (hasCross)
            {
                // move rec based on direction
                switch (waveDirec)
                {
                    case Orientation.UP:
                        waveRec.Y -= speed * 4;
                        break;
                    case Orientation.DOWN:
                        waveRec.Y += speed * 4;
                        break;
                    case Orientation.LEFT:
                        waveRec.X -= speed * 4;
                        break;
                    case Orientation.RIGHT:
                        waveRec.X += speed * 4;
                        break;
                }
                // move untill hit a range
                waveDis += speed;

                if (waveDis >= AttackRange / 2)
                {
                    hasCross = false;
                    waveDis = 0;
                }
                else if (waveRec.Intersects(data.Player.CollisionRec))// check for objects
                {
                    data.Player.damage(Strength);
                }                
            }
        }

        protected override void wander()
        {
            base.wander();
            hasCross = false;
            waveDis = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (hasCross)
                spriteBatch.Draw(waveText, waveRec, Color.White);
        }

        private void createWave()
        {
            switch (direction)
            {
                case Orientation.UP:
                    waveRec.Width = waveRec.Height = TopRec.Width;
                    waveRec.Y = CollisionRec.Y - waveRec.Height;
                    waveRec.X = CollisionRec.X + (CollisionRec.Width - waveRec.Width) / 2;
                    waveText = Game1.GameContent.Load<Texture2D>("AlistarAttack/BackSwoosh");
                    break;
                case Orientation.DOWN:
                    waveRec.Width = waveRec.Height = TopRec.Width;
                    waveRec.Y = CollisionRec.Y + CollisionRec.Height;
                    waveRec.X = CollisionRec.X + (CollisionRec.Width - waveRec.Width) / 2;
                    waveText = Game1.GameContent.Load<Texture2D>("AlistarAttack/FrontSwoosh");
                    break;
                case Orientation.LEFT:
                    waveRec.Height = waveRec.Width = LeftRec.Height;
                    waveRec.X = CollisionRec.X - waveRec.Width;
                    waveRec.Y = CollisionRec.Y + (CollisionRec.Height - waveRec.Height) / 2;
                    waveText = Game1.GameContent.Load<Texture2D>("AlistarAttack/LeftSwoosh");
                    break;
                case Orientation.RIGHT:
                    waveRec.Height = waveRec.Width = LeftRec.Height;
                    waveRec.X = CollisionRec.X + CollisionRec.Width;
                    waveRec.Y = CollisionRec.Y + (CollisionRec.Height - waveRec.Height) / 2;
                    waveText = Game1.GameContent.Load<Texture2D>("AlistarAttack/RightSwoosh");
                    break;
            }
            waveDirec = direction;
        }

    }
}
