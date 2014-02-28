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

        public override BaseSpriteStruct SaveData
        {
            get
            {
                BaseSpriteStruct myData = base.SaveData;
                myData.saveId = "IMo";
                return myData;
            }
            set
            {
                base.SaveData = value;
            }
        }

        protected MovableObject()
        {
        }

        public MovableObject(Texture2D image, float scaleFactor, int displayWidth, float secondsToCrossScreen, Vector2 start)
            : base(image, scaleFactor, secondsToCrossScreen, start)
        {
        }

        public override void Update(Map data, GameTime gametime)
        {
            if (dead)
                return;

            KeyboardState keys = Keyboard.GetState();

            futureRec = new Rectangle(CollisionRec.X, CollisionRec.Y,
                CollisionRec.Width, CollisionRec.Height);

            canMoveDown = true;
            canMoveLeft = true;
            canMoveRight = true;
            canMoveUp = true;
                        
            foreach (BaseSprite obj in data.CurrentData.Everything)
            {
                if (!obj.IsDead && !obj.Equals(this))
                {
                    if (obj.GetType() != typeof(Character) && futureRec.Intersects(obj.CollisionRec))
                    {
                        canMoveRight = !RightRec.Intersects(obj.CollisionRec);
                        canMoveDown = !BottomRec.Intersects(obj.CollisionRec);
                        canMoveUp = !TopRec.Intersects(obj.CollisionRec);
                        canMoveLeft = !LeftRec.Intersects(obj.CollisionRec);

                        Vector2 vec = measureCollison(obj.CollisionRec);
                        position += measureCollison(obj.CollisionRec);

                    }
                    
                }
            }

            move(data.Player, data.Player.CollisionRec, keys);
        }

        private void move(Character player, Rectangle playerRec, KeyboardState keys)
        {
            if (!keys.IsKeyDown(Keys.LeftShift))
            {
                canMoveDown = false;
                canMoveLeft = false;
                canMoveRight = false;
                canMoveUp = false;

                return;
            }

            if (TopRec.Intersects(playerRec))
            {
                if (canMoveUp && keys.IsKeyDown(Keys.W))
                {
                    position.Y = playerRec.Y + playerRec.Height - player.Speed;
                    futureRec.Y = playerRec.Y + playerRec.Height - player.Speed * 2;
                    
                }
                else if (canMoveDown && keys.IsKeyDown(Keys.S))
                {
                    position.Y = playerRec.Y + playerRec.Height + player.Speed;
                    futureRec.Y = playerRec.Y + playerRec.Height + player.Speed * 2;
                    
                }
            }
            else if (RightRec.Intersects(playerRec))
            {
                if (canMoveRight && keys.IsKeyDown(Keys.D))
                {
                    position.X = playerRec.X - CollisionRec.Width + player.Speed;
                    futureRec.X = playerRec.X - CollisionRec.Width + player.Speed * 2;
                    
                }
                else if (canMoveLeft && keys.IsKeyDown(Keys.A))
                {
                    position.X = playerRec.X - CollisionRec.Width - player.Speed;
                    futureRec.X = playerRec.X - CollisionRec.Width - player.Speed * 2;
                    
                }
            }
            else if (LeftRec.Intersects(playerRec))
            {                
                if (keys.IsKeyDown(Keys.A) && canMoveLeft)
                {
                    position.X = playerRec.X + playerRec.Width - player.Speed;
                    futureRec.X = playerRec.X + playerRec.Width - player.Speed * 2;
                    
                }
                else if (keys.IsKeyDown(Keys.D) && canMoveRight)
                {
                    position.X = playerRec.X + playerRec.Width + player.Speed;
                    futureRec.X = playerRec.X + playerRec.Width + player.Speed * 2;
                    
                }
            }
            else if (BottomRec.Intersects(playerRec))
            {
                if (canMoveUp && keys.IsKeyDown(Keys.W))
                {
                    position.Y = playerRec.Y - CollisionRec.Height - player.Speed;
                    futureRec.Y = playerRec.Y - CollisionRec.Height - player.Speed * 2;
                    
                }
                else if (canMoveDown && keys.IsKeyDown(Keys.S))
                {
                    position.Y = playerRec.Y - CollisionRec.Height + player.Speed;
                    futureRec.Y = playerRec.Y - CollisionRec.Height + player.Speed * 2;
                    
                }
            }


        }

    }
}
