using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace AdlezHolder
{
    public class Cutscene
    {
        protected MapDataHolder data;
        protected Character player;
        protected bool over;


        public MapDataHolder Data
        {
            set { data = value; }
        }

        public Character Player
        {
            set { this.player = value; }
        }

        public bool Over
        {
            get { return over; }
        }

        public virtual void play(GameTime gameTime)
        {
        }

        protected virtual void moveTo(Vector2 newPosition, BaseSprite obj, int speed)
        {
            if (isAtPosition(newPosition, obj) != true)
            {
                if (isAtYPosition(newPosition, obj) != true)
                    moveY(newPosition, obj, speed);
                else
                {
                    if (isAtXPosition(newPosition, obj) != true)
                        moveX(newPosition, obj, speed);
                }
            }
        }

        protected virtual void moveX(Vector2 newPosition, BaseSprite obj, int speed)
        {
            if (obj.Position.X < newPosition.X)
                moveRight(obj, speed);
            else
                moveLeft(obj, speed);
        }

        protected virtual void moveY(Vector2 newPosition, BaseSprite obj, int speed)
        {
            if (obj.Position.Y < newPosition.Y)
                moveDown(obj, speed);
            else
                moveUp(obj, speed);
        }

        protected virtual void moveUp(BaseSprite obj, int speed)
        {
            Vector2 newPosition = obj.Position;
            newPosition.Y -= speed;
            obj.Position = newPosition;

            if (obj.GetType() == typeof(Character))
            {
                player.cutsceneMove(Orientation.UP);
            }
        }

        protected virtual void moveDown(BaseSprite obj, int speed)
        {
            Vector2 newPosition = obj.Position;
            newPosition.Y += speed;
            obj.Position = newPosition;

            if (obj.GetType() == typeof(Character))
            {
                player.cutsceneMove(Orientation.DOWN);
            }
        }

        protected virtual void moveLeft(BaseSprite obj, int speed)
        {
            Vector2 newPosition = obj.Position;
            newPosition.X -= speed;
            obj.Position = newPosition;

            if (obj.GetType() == typeof(Character))
            {
                player.cutsceneMove(Orientation.LEFT);
            }
        }

        protected virtual void moveRight(BaseSprite obj, int speed)
        {
            Vector2 newPosition = obj.Position;
            newPosition.X += speed;
            obj.Position = newPosition;

            if (obj.GetType() == typeof(Character))
            {
                player.cutsceneMove(Orientation.RIGHT);
            }
        }

        protected virtual void moveBackground(Vector2 vecToMove)
        {
            Vector2 position = data.Position;
            position += vecToMove;

            data.changeBackgroundX((int)vecToMove.X);
            data.changeBackgroundY((int)vecToMove.Y);
        }

        protected virtual void moveObjects(Vector2 vecToMove)
        {
            foreach (BaseSprite sprite in data.Everything)
            {
                sprite.Position += vecToMove;
            }
        }

        protected virtual Boolean isAtPosition(Vector2 position, BaseSprite obj)
        {
            if (obj.Position.X <= (position.X + 1) && obj.Position.X >= (position.X - 1) && obj.Position.Y <= (position.Y + 1) && obj.Position.Y >= (position.Y - 1))
                return true;
            else
                return false;
        }

        protected virtual Boolean isAtYPosition(Vector2 position, BaseSprite obj)
        {
            if (obj.Position.Y <= (position.Y + 1) && obj.Position.Y >= (position.Y - 1))
                return true;
            else
                return false;
        }

        protected virtual Boolean isAtXPosition(Vector2 position, BaseSprite obj)
        {
            if (obj.Position.X <= (position.X + 1) && obj.Position.X >= (position.X - 1))
                return true;
            else
                return false;
        }

        public Boolean CanStartDialog()
        {
            if (Game1.MainGameState == GameState.TALKING)
                return true;
            else
                return false;
        }
    }
}
