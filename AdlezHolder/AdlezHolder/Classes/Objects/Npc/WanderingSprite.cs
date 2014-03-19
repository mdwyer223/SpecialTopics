using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class WanderingSprite : AnimatedSprite
    {
        int moveCount, moveIndex;
        bool isWaiting;
        Random rand;
        Vector2 startPoint;
        protected FullAnimation idle, move;

        protected bool canMoveRight, canMoveDown, canMoveLeft, canMoveUp;// TODO: make property

        const float SEC_WAITING = 3;
        const float SEC_MOVING = 1;
        const int TICK_IN_A_SEC = 60;

        public int maxWanderRange { get; protected set; }

        public WanderingSprite(Texture2D defaultTexture, float scaleFactor, int SecondsToCrossScreen,
            int displayWidth, Vector2 startPosition)
            : base(defaultTexture, scaleFactor, displayWidth, SecondsToCrossScreen,startPosition)
        {
            rand = new Random();
            startPoint = startPosition;

            Texture2D[] ani = new Texture2D[1];
            ani[0] = defaultTexture;
            idle = new FullAnimation(ani, 5);
            move = new FullAnimation(ani, 5);
            
        }

        public override void Update(Map data, GameTime gameTime)
        {
            setCanMoves(data);
            base.Update(data, gameTime);
        }

        protected void setCanMoves(Map map)
        {// TODO: make MapData.add smart, make an AdvancedSprite,
            canMoveRight = true;
            canMoveLeft = true;
            canMoveUp = true;
            canMoveDown = true;
            foreach (BaseSprite sprite in map.CurrentData.Everything)
            {
                canMoveDown = canMoveDown && !BottomRec.Intersects(sprite.CollisionRec);
                canMoveUp = canMoveUp && !TopRec.Intersects(sprite.CollisionRec);
                canMoveLeft = canMoveLeft && !LeftRec.Intersects(sprite.CollisionRec);
                canMoveRight = canMoveRight && !RightRec.Intersects(sprite.CollisionRec);
            }

        }
        
        public virtual void wander()
        {
            moveCount++;
            if (moveCount >= SEC_MOVING * TICK_IN_A_SEC)
            {
                if (!isWaiting)
                {
                    moveIndex = rand.Next(4);
                    moveCount = (int)-((rand.Next(0,4) - SEC_MOVING) * TICK_IN_A_SEC);
                    isWaiting = true;
                }
                else
                {
                    moveCount = 0;
                    isWaiting = false;
                }
            }

            if (isWaiting)
            {
                playAnimation(idle);
                return;
            }
            // Wander : check canMoves
            if (moveIndex == 0 && canMoveRight)
            {
                position.X += speed;
                direction = Orientation.RIGHT;
            }
            else if (moveIndex == 1 && canMoveDown)
            {
                position.Y += speed;
                direction = Orientation.DOWN;
            }
            else if (moveIndex == 2 && canMoveLeft)
            {
                position.X -= speed;
                direction = Orientation.LEFT;
            }
            else if (moveIndex == 3 && canMoveUp)
            {
                position.Y -= speed;
                direction = Orientation.UP;
            }
            else
                moveIndex = rand.Next(4);
            playAnimation(move);
        }        

        protected void resetWander()
        {
            moveCount = 0;
        }

    }
}
