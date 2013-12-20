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
    public class MovableObject : ImmovableObject
    {
        Orientation direction;
        Rectangle futureRec;
        
        //public bool canMoveRight, canMoveDown, canMoveLeft, canMoveUp;

        public MovableObject(Texture2D image, float scaleFactor, int displayWidth, float secondsToCrossScreen, Vector2 start)
            : base(image, scaleFactor, displayWidth, secondsToCrossScreen, start)
        {
            canMoveDown = true;
            canMoveUp = true;
            canMoveLeft = true;
            canMoveRight = true;
        }

        public override void Update(Map data, GameTime gametime)
        {
            if (dead)
                return;

            KeyboardState keys = Keyboard.GetState();

            direction = data.Player.Direction;

            futureRec = new Rectangle(CollisionRec.X, CollisionRec.Y, 
                CollisionRec.Width, CollisionRec.Height);

            // 
            foreach (ImmovableObject obj in data.CurrentData.Everything)
            {
                if (!obj.IsDead)
                {
                    if (futureRec.Intersects(obj.CollisionRec))
                    {
                        canMoveRight = direction == Orientation.RIGHT;
                        canMoveDown = direction == Orientation.DOWN;
                        canMoveUp = direction == Orientation.UP;
                        canMoveLeft = direction == Orientation.LEFT;

                        Vector2 vec = measureCollison(obj.CollisionRec);
                        position += measureCollison(obj.CollisionRec);
                       
                    }
                }
            }

            move(data.Player, keys);

        }

        private void move(Character player, KeyboardState keys)
        {
            Rectangle otherRec = player.CollisionRec;
            if (!keys.IsKeyDown(Keys.LeftShift))
            {
                canMoveDown = false;
                canMoveLeft = false;
                canMoveRight = false;
                canMoveUp = false;

                return;
            }

            if (TopRec.Intersects(otherRec))
            {
                if (canMoveUp && keys.IsKeyDown(Keys.W))
                {
                    position.Y = otherRec.Y + otherRec.Height;
                    futureRec.Y = otherRec.Y + otherRec.Height - player.Speed;
                }
                else if (canMoveDown && keys.IsKeyDown(Keys.S))
                {
                    position.Y = otherRec.Y + otherRec.Height;
                    futureRec.Y = otherRec.Y + otherRec.Height + player.Speed;
                }
            }
            else if (RightRec.Intersects(otherRec))
            {
                if (canMoveRight && keys.IsKeyDown(Keys.D))
                {
                    position.X = otherRec.X - CollisionRec.Width;
                    futureRec.X = otherRec.X - CollisionRec.Width + player.Speed;
                }
                else if (canMoveLeft && keys.IsKeyDown(Keys.A))
                {
                    position.X = otherRec.X - CollisionRec.Width;
                    futureRec.X = otherRec.X - CollisionRec.Width - player.Speed;
                }
                    
            }
            else if (LeftRec.Intersects(otherRec))
            {
                if (canMoveRight && keys.IsKeyDown(Keys.D))
                {
                    position.X = otherRec.X + otherRec.Width;
                    futureRec.X = otherRec.X + otherRec.Width + player.Speed;
                }
                else if (canMoveLeft && keys.IsKeyDown(Keys.A))
                {
                    position.X = otherRec.X + otherRec.Width;
                    futureRec.X = otherRec.X + otherRec.Width - player.Speed;
                }

            }
            else if (BottomRec.Intersects(otherRec))
            {
                if (keys.IsKeyDown(Keys.W))
                {
                    position.Y = otherRec.Y - CollisionRec.Height;
                    futureRec.Y = otherRec.Y - CollisionRec.Height - player.Speed;
                }
                else if (canMoveDown && keys.IsKeyDown(Keys.S))
                {
                    position.Y = otherRec.Y - CollisionRec.Height;
                    futureRec.Y = otherRec.Y - CollisionRec.Height + player.Speed;
                }
            }
        }

        private void pushUp(int speed)
        {
            position.Y -= speed;
        }

        private void pushDown(int speed)
        {
            position.Y += speed;
        }

        private void pushLeft(int speed)
        {
            position.X -= speed;
        }

        private void pushRight(int speed)
        {
            position.X += speed;
        }
    }
}
