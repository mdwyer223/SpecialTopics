using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class Character : AnimatedSprite
    {
        enum EquippedItem { SWORD, BOW, BOMB, NONE }

        Texture2D texture;        
        EquippedItem selectedItem;
        FullAnimation move, Idle;

        bool canMoveLeft, canMoveRight, canMoveUp, canMoveDown;

        int healthPointsMax, currentHealthPoints;
        int immunityTimer = 0;
        const int IMMUNITY_TIME = 1;

        public int Speed
        {
            get { return speed; }
        }

        
        public Character(Texture2D defaultTexture, float scaleFactor, int displayWidth, float secondsToCrossScreen, Vector2 start)
            : base(defaultTexture, scaleFactor, displayWidth, secondsToCrossScreen, start)
        {
            ContentManager content = Game1.GameContent;
            
            Texture2D[] left, right, forward, backward;

            left = new Texture2D[3];
            right = new Texture2D[3];
            forward = new Texture2D[3];
            backward = new Texture2D[3];

            backward[0] = content.Load<Texture2D>("Alistar/Back");
            backward[1] = content.Load<Texture2D>("Alistar/BackwardRightFoot");
            backward[2] = content.Load<Texture2D>("Alistar/BackwardLeftFoot");

            forward[0] = content.Load<Texture2D>("Alistar/Front");
            forward[1] = content.Load<Texture2D>("Alistar/ForwardRightFoot");
            forward[2] = content.Load<Texture2D>("Alistar/ForwardLeftFoot");

            left[0] = content.Load<Texture2D>("Alistar/Left");
            left[1] = content.Load<Texture2D>("Alistar/LeftRightFoot");
            left[2] = content.Load<Texture2D>("Alistar/LeftLeftFoot");

            right[0] = content.Load<Texture2D>("Alistar/Right");
            right[1] = content.Load<Texture2D>("Alistar/RightRightFoot");
            right[2] = content.Load<Texture2D>("Alistar/RightLeftFoot");
            move = new FullAnimation(backward, forward, left, right,.2f);

            left = new Texture2D[1];
            right = new Texture2D[1];
            forward = new Texture2D[1];
            backward = new Texture2D[1];

            forward[0] = content.Load<Texture2D>("Alistar/Front");
            backward[0] = content.Load<Texture2D>("Alistar/Back");
            left[0] = content.Load<Texture2D>("Alistar/Left");
            right[0] = content.Load<Texture2D>("Alistar/Right");
            Idle = new FullAnimation(backward, forward, left, right,.2f);

            selectedItem = EquippedItem.NONE;

            playAnimation(Idle);

            currentHealthPoints = healthPointsMax = 200;

            canMoveDown = true;
            canMoveUp = true;
            canMoveRight = true;
            canMoveLeft = true;
        }

        public override void Update(Map data, GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();

            if(immunityTimer != (IMMUNITY_TIME * 1000))
            {
                immunityTimer += gameTime.ElapsedGameTime.Milliseconds;
            }

            if (keys.IsKeyDown(Keys.W))
            {
                direction = Orientation.UP;
                selectMove();
                //position.Y -= speed;
            }
            else if (keys.IsKeyDown(Keys.S))
            {
                direction = Orientation.DOWN;
                selectMove();
                //position.Y += speed;
            }
            else if (keys.IsKeyDown(Keys.A))
            {
                direction = Orientation.LEFT;
                selectMove();
                //position.X -= speed;
            }
            else if (keys.IsKeyDown(Keys.D))
            {
                direction = Orientation.RIGHT;
                selectMove();
                //position.X += speed;
            }
            else
            {
                playAnimation(Idle);
            }

            fixSpacing(data.CurrentData.AllObjects.ToArray(), keys);

            base.Update(data, gameTime);
        }

        public void damage(int damagePoints)
        {
            if(immunityTimer != (IMMUNITY_TIME * 1000))
            {
                currentHealthPoints -= damagePoints;
                if (currentHealthPoints <= 0)
                {
                    currentHealthPoints = 0;
                }

                immunityTimer = 0;
            }
        }

        public void heal(int healPoints)
        {
            if (currentHealthPoints + healPoints <= healthPointsMax)
            {
                currentHealthPoints += healPoints;
            }
        }


        private void fixSpacing(ImmovableObject[] objects, KeyboardState keys)
        {
            Rectangle futureRec = new Rectangle(CollisionRec.X, CollisionRec.Y,
                CollisionRec.Width, CollisionRec.Height);

            if (canMoveUp && keys.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
                futureRec.Y -= speed * 2;
            }
            else if (canMoveDown && keys.IsKeyDown(Keys.S))
            {
                position.Y += speed;
                futureRec.Y += speed * 2;
            }
            else if (canMoveLeft && keys.IsKeyDown(Keys.A))
            {
                position.X -= speed;
                futureRec.X -= speed * 2;
            }
            else if (canMoveRight && keys.IsKeyDown(Keys.D))
            {
                position.X += speed;
                futureRec.X += speed * 2;
            }

            canMoveRight = true;
            canMoveDown = true;
            canMoveUp = true;
            canMoveLeft = true;

            List<ImmovableObject> objectsColliding = new List<ImmovableObject>();

            for (int i = 0; i < objects.Length; i++)
            {
                if (!objects[i].IsDead)
                {
                    if (futureRec.Intersects(objects[i].CollisionRec))
                    {
                        objectsColliding.Add(objects[i]);

                    }
                }
            }

            for (int i = 0; i < objectsColliding.Count; i++)
            {
                if (objectsColliding[i].GetType() == typeof(ImmovableObject))
                {
                    canMoveRight = direction != Orientation.RIGHT;
                    canMoveDown = direction != Orientation.DOWN;
                    canMoveUp = direction != Orientation.UP;
                    canMoveLeft = direction != Orientation.LEFT;
                    break;

                    Vector2 vecToMove = measureCollison(objects[i].CollisionRec);
                    position += vecToMove;
                }
                
                if (objectsColliding[i].GetType() == typeof(MovableObject))
                {
                    if (!keys.IsKeyDown(Keys.LeftShift))
                    {
                        canMoveRight = direction != Orientation.RIGHT;
                        canMoveDown = direction != Orientation.DOWN;
                        canMoveUp = direction != Orientation.UP;
                        canMoveLeft = direction != Orientation.LEFT;
                        Vector2 vecToMove = measureCollison(objects[i].CollisionRec);
                        position += vecToMove;
                    }
                    else
                    {// change can moves to properties
                        canMoveRight = objects[i].canMoveRight;
                        canMoveDown = objects[i].canMoveDown;
                        canMoveUp = objects[i].canMoveUp;
                        canMoveLeft = objects[i].canMoveLeft;                        
                    }
                }
                
            }
        }

        private void selectMove()
        {
            switch (selectedItem)
            {
                case EquippedItem.NONE:
                    playAnimation(move);
                    break;
                case EquippedItem.SWORD:

                    break;
                case EquippedItem.BOW:

                    break;
                case EquippedItem.BOMB:

                    break;

            }
        }

    }
}
