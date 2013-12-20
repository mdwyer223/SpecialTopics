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
        MapDataHolder data;
        Character player;
        int playOrder = 0;
        int lastOrder = 7;
        MessageBox box = new MessageBox(Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), 1, 800, 400);

        Song music;

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
            get { return playOrder == lastOrder; }
        }

        public Cutscene()
        {
            music = Game1.GameContent.Load<Song>("Music/TravelingSong");
        }

        public virtual void play(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (MediaPlayer.State == MediaState.Paused || MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Resume();
            }
            else
            {
                if (music != null)
                {
                    MediaPlayer.Play(music);
                }
            }

            if (playOrder == 0)
            {
                player.Position = new Vector2(765, 362);
                playOrder++;
            }
            else if (playOrder == 1)
            {
                moveLeft(player, 1);
                if (player.Position.X >= 664 && player.Position.X <= 666)
                    playOrder++;
            }
            else if (playOrder == 2)
            {
                moveUp(player, 1);
                if (player.Position.Y >= 314 && player.Position.Y <= 316)
                    playOrder++;
            }
            else if (playOrder == 3)
            {
                moveLeft(player, 1);
                if (player.Position.X >= 374 && player.Position.X <= 376)
                    playOrder++;
            }
            else if (playOrder == 4)
            {
                moveUp(player, 1);
                if (player.Position.Y >= 254 && player.Position.Y <= 256)
                    playOrder++;
            }
            else if (playOrder == 5)
            {
                moveLeft(player, 1);
                if (player.Position.X >= 139 && player.Position.X <= 141)
                    playOrder++;
            }
            else if (playOrder == 6)
            {
                moveUp(player, 1);
                if (player.Position.Y >= 154 && player.Position.Y <= 156)
                {
                    playOrder++;
                    player.cutsceneMove(Orientation.UP);
                }
            }

            player.cutsceneUpdate(gameTime);

            if (playOrder == lastOrder)
            {
                music = null;
                MediaPlayer.Stop();
            }
        }

        protected virtual void moveTo(Vector2 newPosition, BaseSprite obj)
        {
            if (obj.Position.X - newPosition.X < obj.Position.Y - newPosition.Y)
            {
                if (obj.Position.X < newPosition.X)
                    moveRight(obj, 1);
                else
                    moveLeft(obj, 1);
            }
            else
            {
                if (obj.Position.Y < newPosition.Y)
                    moveDown(obj, 1);
                else
                    moveUp(obj, 1);
            }
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


    }
}
