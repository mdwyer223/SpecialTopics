using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class Minotaur : RangedEnemy
    {

        public override BaseSpriteStruct SaveData
        {
            get
            {
                BaseSpriteStruct myData = base.SaveData;
                myData.saveId = "EMi";
                return myData;
            }
            set
            {
                base.SaveData = value;
            }
        }

        public Minotaur(Texture2D defaultTexture, float scaleFactor, Vector2 startPosition)
            :base(defaultTexture, scaleFactor, 9, startPosition)
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

            AttackRange = (CollisionRec.Height + CollisionRec.Width) * 2;

            HitPoints = MaxHealthPoints = 300;
        }

        protected override void setAttributes()
        {
            Strength = 5;
            PlayerCollision = false;
        }

        protected override void createProjetile()
        {
            base.createProjetile();
            switch (direction)
            {
                case Orientation.UP:
                    projectileRec.Width = projectileRec.Height = TopRec.Width;
                    projectileRec.Y = CollisionRec.Y - projectileRec.Height;
                    projectileRec.X = CollisionRec.X + (CollisionRec.Width - projectileRec.Width) / 2;
                    projectileText = Game1.GameContent.Load<Texture2D>("AlistarSwordAttack/BackSwoosh");
                    break;
                case Orientation.DOWN:
                    projectileRec.Width = projectileRec.Height = TopRec.Width;
                    projectileRec.Y = CollisionRec.Y + CollisionRec.Height;
                    projectileRec.X = CollisionRec.X + (CollisionRec.Width - projectileRec.Width) / 2;
                    projectileText = Game1.GameContent.Load<Texture2D>("AlistarSwordAttack/FrontSwoosh");
                    break;
                case Orientation.LEFT:
                    projectileRec.Height = projectileRec.Width = LeftRec.Height;
                    projectileRec.X = CollisionRec.X - projectileRec.Width;
                    projectileRec.Y = CollisionRec.Y + (CollisionRec.Height - projectileRec.Height) / 2;
                    projectileText = Game1.GameContent.Load<Texture2D>("AlistarSwordAttack/LeftSwoosh");
                    break;
                case Orientation.RIGHT:
                    projectileRec.Height = projectileRec.Width = LeftRec.Height;
                    projectileRec.X = CollisionRec.X + CollisionRec.Width;
                    projectileRec.Y = CollisionRec.Y + (CollisionRec.Height - projectileRec.Height) / 2;
                    projectileText = Game1.GameContent.Load<Texture2D>("AlistarSwordAttack/RightSwoosh");
                    break;
            }
        }



    }
}
