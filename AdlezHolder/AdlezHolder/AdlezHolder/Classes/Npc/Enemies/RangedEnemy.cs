using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public abstract class RangedEnemy : Enemy
    {
        protected Orientation projectileDirec;
        protected Texture2D projectileText;
        protected Rectangle projectileRec;
        protected int projectileDis;
        
        bool hasCross;
        int delayTimer;
        int pierceCount;

        private int maxDelay = 30;
        public int FireRate
        {
            get{ return 60 / maxDelay; }
            protected set { maxDelay = 60 / value; }
        }
                
        public int ProjectileDamage
        {
            get;
            protected set;
        }

        public int ProjectileSpeed
        {
            get;
            protected set;
        }

        public Color ProjectileColor
        {
            get;
            protected set;
        }

        private bool wallCollision = true;
        public bool WallCollision
        {
            get { return wallCollision; }
            protected set { wallCollision = value; }
        }

        private bool playerCollision = true;
        public bool PlayerCollision
        {
            get { return playerCollision; }
            protected set { playerCollision = value; }
        }

        private int pierce = 1;
        public int Pierce
        {
            get { return pierce; }
            protected set { pierce = value; }
        }

        public RangedEnemy(Texture2D defaultTexture, float scaleFactor, int SecondsToCrossScreen, Vector2 startPosition)
            : base(defaultTexture, scaleFactor, SecondsToCrossScreen, startPosition)
        {
            ProjectileSpeed = speed * 3;
        }

        protected override void attack(Map data)
        {
            base.attack(data);
            Vector2 velocity = data.Player.Center - Center;

            if (Math.Abs(data.Player.Center.X - Center.X) < CollisionRec.Height / 2
                || Math.Abs(data.Player.Center.Y - Center.Y) < CollisionRec.Width / 2)
            {
                if (!hasCross && delayTimer > maxDelay)
                {
                    projectileDirec = direction;
                    isAttacking = true;
                    hasCross = true;
                    createProjetile();
                }
            }

            // shoot wave 
            if (hasCross)
            {
                delayTimer = 0;
                // move rec based on direction
                switch (projectileDirec)
                {
                    case Orientation.UP:
                        projectileRec.Y -= ProjectileSpeed;
                        break;
                    case Orientation.DOWN:
                        projectileRec.Y += ProjectileSpeed;
                        break;
                    case Orientation.LEFT:
                        projectileRec.X -= ProjectileSpeed;
                        break;
                    case Orientation.RIGHT:
                        projectileRec.X += ProjectileSpeed;
                        break;
                }
                // move untill hit a range
                projectileDis += ProjectileSpeed;

                projectileDuring(projectileDis);

                if (projectileDis >= AttackRange)
                {
                    projectileEnd(data, null, new Vector2(projectileRec.X, projectileRec.Y));
                    hasCross = false;
                    projectileDis = 0;
                }
                else if (projectileRec.Intersects(data.Player.CollisionRec))
                {
                    //projectileEnd(data.Player);
                    data.Player.damage(ProjectileDamage);
                    if (playerCollision)
                    {
                        hasCross = false;
                        projectileDis = 0;
                    }
                }

                if(WallCollision)
                    foreach (BaseSprite sprite in data.CurrentData.Everything)
                        if (projectileRec.Intersects(sprite.CollisionRec) &&
                            sprite.GetType().IsSubclassOf(typeof(ImmovableObject)))
                        {
                            pierceCount++;
                            if (pierceCount >= pierce)
                            {
                                projectileEnd(data, sprite, new Vector2(projectileRec.X, projectileRec.Y));
                                hasCross = false;
                                projectileDis = 0;
                                pierceCount = 0;
                            }
                            break;
                        }
            }
            else
            {
                delayTimer++;
            }
           
        }

        protected override void wander()
        {
            base.wander();
            isAttacking = false;
            hasCross = false;            
            projectileDis = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (hasCross)
                spriteBatch.Draw(projectileText, projectileRec, ProjectileColor);
            base.Draw(spriteBatch);
        }

        protected virtual void createProjetile()
        {
            projectileRec.X = (int)Center.X;
            projectileRec.Y = (int)Center.Y;
        }

        protected virtual void projectileEnd(Map data, BaseSprite collidedObject, Vector2 position)
        {
        }

        protected virtual void projectileDuring(int projectileDis)
        {
        }
        
    }
}
