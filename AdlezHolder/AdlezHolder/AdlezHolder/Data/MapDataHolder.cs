using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class MapDataHolder
    {
        protected List<BaseSprite> everything;

        protected List<ImmovableObject> objects;
        protected List<ImmovableObject> iObjects;
        protected List<MovableObject> mObjects;
        protected List<SpikeTrap> spikeTraps;
        protected List<ArrowTrap> arrowTraps;

        protected List<AnimatedSprite> anSprites;
        protected List<Enemy> enemies;
        protected List<Npc> npcs;

        protected List<TripWire> tripWires;
        protected List<Chest> chests;

        protected MessageBox box;

        protected Texture2D background;
        protected Rectangle backgroundRec;
        protected Vector2 position;

        protected Song music;

        public Rectangle BackgroundRec
        {
            get { return backgroundRec; }
            set { backgroundRec = value; }
        }

        protected Vector2 topRightCorner, topLeftCorner, 
            bottomRightCorner, bottomLeftCorner;
        protected Vector2 topRightCornerView, topLeftCornerView,
            bottomRightCornerView, bottomLeftCornerView;

        bool moveObjectsUp, moveObjectsDown,
            moveObjectsLeft, moveObjectsRight;

        public List<ImmovableObject> AllObjects
        {
            get { return objects; }
            protected set { objects = value; }
        }

        public List<BaseSprite> Everything
        {
            get { return everything; }
            protected set { everything = value; }
        }

        public List<ImmovableObject> ImmovableObjects
        {
            get { return iObjects; }
            protected set { iObjects = value; }
        }

        public List<MovableObject> MovableObjects
        {
            get { return mObjects; }
            protected set { mObjects = value; }
        }

        public List<Enemy> Enemies
        {
            get { return enemies; }
            protected set { enemies = value; }
        }

        public Vector2 Position
        {
            get { return new Vector2(backgroundRec.X, backgroundRec.Y); }
            protected set { position = value; }
        }

        public MapDataHolder()
        {
            everything = new List<BaseSprite>();

            objects = new List<ImmovableObject>();
            iObjects = new List<ImmovableObject>();
            mObjects = new List<MovableObject>();

            anSprites = new List<AnimatedSprite>();
            enemies = new List<Enemy>();
            npcs = new List<Npc>();

            tripWires = new List<TripWire>();
            arrowTraps = new List<ArrowTrap>();
            chests = new List<Chest>();

            //scene = new AlphaCutscene();
        }

        public MapDataHolder(Character player)
        {
            //object reference will allow for the change of the player
            //position

            everything = new List<BaseSprite>();

            objects = new List<ImmovableObject>();
            iObjects = new List<ImmovableObject>();
            mObjects = new List<MovableObject>();

            anSprites = new List<AnimatedSprite>();
            enemies = new List<Enemy>();
            npcs = new List<Npc>();

            tripWires = new List<TripWire>();
            arrowTraps = new List<ArrowTrap>();
            chests = new List<Chest>();
            
            //scene = new AlphaCutscene();
        }

        public virtual void LoadContent(ContentManager Content, int displayWidth)
        {
            
        }

        public virtual void Update(Map map, GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();

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

            topLeftCornerView = new Vector2(0, 0);
            topRightCornerView = new Vector2(Game1.DisplayWidth, 0);
            bottomLeftCornerView = new Vector2(0, Game1.DisplayHeight);
            bottomRightCornerView = new Vector2(Game1.DisplayWidth, Game1.DisplayHeight);

            updateCorners();

            adjustBackground(map.Player, keys);

            foreach (BaseSprite obj in everything)
            {
                if (!obj.IsDead && obj.GetType() != typeof(Character))
                {
                    obj.Update(map, gameTime);

                    adjust(map.Player, keys, obj);
                }
            }

            changeDrawOrder(everything);

            if (box != null)
            {
                box.update();
                if (!box.Visible)
                {
                    box = null;
                }
            }
        }

        protected virtual void adjustBackground(Character player, KeyboardState keys)
        {
            //see if there can be a way to have a free moving camera with the arrow keys
            if (player.Center.Y >= (Game1.DisplayHeight / 2) - 1 && player.Center.Y <= (Game1.DisplayHeight / 2) + 1)
            {
                player.CanMoveUp = player.CanMoveDown = false;

                if (topLeftCorner.Y + player.Speed < topLeftCornerView.Y)
                {
                    if (keys.IsKeyDown(Keys.W) && player.Direction == Orientation.UP)
                    {
                        this.backgroundRec.Y += player.Speed;
                        moveObjectsDown = true;
                    }
                }
                else
                {
                    player.CanMoveUp = true;
                    moveObjectsDown = false;
                }

                if (bottomLeftCorner.Y - player.Speed > bottomLeftCornerView.Y)
                {
                    if (keys.IsKeyDown(Keys.S) && player.Direction == Orientation.DOWN)
                    {
                        this.backgroundRec.Y -= player.Speed;
                        moveObjectsUp = true;
                    }
                }
                else
                {
                    player.CanMoveDown = true;
                    moveObjectsUp = false;
                }
            }
            else
            {
                moveObjectsUp = false;
                moveObjectsDown = false;
            }

            if (player.Center.X >= (Game1.DisplayWidth / 2) - 1 && player.Center.X <= (Game1.DisplayWidth / 2) + 1)
            {
                player.CanMoveRight = player.CanMoveLeft = false;

                if (bottomLeftCorner.X + player.Speed < bottomLeftCornerView.X)
                {
                    if (keys.IsKeyDown(Keys.A) && player.Direction == Orientation.LEFT)
                    {
                        this.backgroundRec.X += player.Speed;
                        moveObjectsRight = true;
                    }
                }
                else
                {
                    player.CanMoveLeft = true;
                    moveObjectsRight = false;
                }

                if (bottomRightCorner.X - player.Speed > bottomRightCornerView.X)
                {
                    if (keys.IsKeyDown(Keys.D) && player.Direction == Orientation.RIGHT)
                    {
                        this.backgroundRec.X -= player.Speed;
                        moveObjectsLeft = true;
                    }
                }
                else
                {
                    player.CanMoveRight = true;
                    moveObjectsLeft = false;
                }
            }
            else
            {
                moveObjectsLeft = false;
                moveObjectsRight = false;
            }
        }

        protected virtual void adjust(Character player, KeyboardState keys, TripWire obj)
        {
            //check if the player is attacking here*******************************
            if (keys.IsKeyDown(Keys.W) && player.Direction == Orientation.UP && moveObjectsDown)
            {
                Vector2 newPos = new Vector2(obj.Position.X, obj.Position.Y);
                newPos.Y += player.Speed;
                obj.Position = newPos;
            }
            else if (keys.IsKeyDown(Keys.S) && player.Direction == Orientation.DOWN && moveObjectsUp)
            {
                Vector2 newPos = new Vector2(obj.Position.X, obj.Position.Y);
                newPos.Y -= player.Speed;
                obj.Position = newPos;
            }

            if (keys.IsKeyDown(Keys.A) && player.Direction == Orientation.LEFT && moveObjectsRight)
            {
                Vector2 newPos = new Vector2(obj.Position.X, obj.Position.Y);
                newPos.X += player.Speed;
                obj.Position = newPos;
            }
            else if (keys.IsKeyDown(Keys.D) && player.Direction == Orientation.RIGHT && moveObjectsLeft)
            {
                Vector2 newPos = new Vector2(obj.Position.X, obj.Position.Y);
                newPos.X -= player.Speed;
                obj.Position = newPos;
            }
        }

        public virtual void adjust(Character player, KeyboardState keys, BaseSprite obj)
        {
            //check if the player is attacking here*******************************
            if (keys.IsKeyDown(Keys.W) && player.Direction == Orientation.UP && moveObjectsDown)
            {
                Vector2 newPos = new Vector2(obj.Position.X, obj.Position.Y);
                newPos.Y += player.Speed;
                obj.Position = newPos;
            }
            else if (keys.IsKeyDown(Keys.S) && player.Direction == Orientation.DOWN && moveObjectsUp)
            {
                Vector2 newPos = new Vector2(obj.Position.X, obj.Position.Y);
                newPos.Y -= player.Speed;
                obj.Position = newPos;
            }

            if (keys.IsKeyDown(Keys.A) && player.Direction == Orientation.LEFT && moveObjectsRight)
            {
                Vector2 newPos = new Vector2(obj.Position.X, obj.Position.Y);
                newPos.X += player.Speed;
                obj.Position = newPos;
            }
            else if (keys.IsKeyDown(Keys.D) && player.Direction == Orientation.RIGHT && moveObjectsLeft)
            {
                Vector2 newPos = new Vector2(obj.Position.X, obj.Position.Y);
                newPos.X -= player.Speed;
                obj.Position = newPos;
            }
        }

        public virtual void adjustObjectsBackgroundTripWires(Vector2 vecToMove)
        {
            backgroundRec.X += (int)vecToMove.X;
            backgroundRec.Y += (int)vecToMove.Y;

            foreach (BaseSprite obj in everything)
            {
                Vector2 pos = obj.Position;

                pos += vecToMove;

                obj.Position = pos;
            }

            foreach (TripWire obj in tripWires)
            {
                Vector2 pos = obj.Position;

                pos += vecToMove;

                obj.Position = pos;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, backgroundRec, Color.White);

            foreach (BaseSprite sprite in everything)
            {
                if (sprite.IsVisible)
                {
                    sprite.Draw(spriteBatch);
                }
            }

            if (box != null)
            {
                box.draw(spriteBatch);
            }
        }

        protected virtual void updateCorners()
        {
            topLeftCorner = new Vector2(backgroundRec.X, backgroundRec.Y);
            topRightCorner = new Vector2(backgroundRec.X + backgroundRec.Width, backgroundRec.Y);
            bottomLeftCorner = new Vector2(backgroundRec.X, backgroundRec.Y + backgroundRec.Height);
            bottomRightCorner = new Vector2(backgroundRec.X + backgroundRec.Width, backgroundRec.Y + backgroundRec.Height);
        }

        private void changeDrawOrder(List<BaseSprite> sprites)
        {
            BaseSprite temp;

            for (int i = 0; i < sprites.Count - 1; i++)
            {
                for (int j = 1; j < sprites.Count; j++)
                {
                    if (sprites[i].Position.Y > sprites[j].Position.Y)
                    {
                        temp = sprites[i];
                        sprites[i] = sprites[j];
                        sprites[j] = temp;
                    }

                    if (sprites[i].Position.Y < sprites[j - 1].Position.Y)
                    {
                        temp = sprites[i];
                        sprites[i] = sprites[j - 1];
                        sprites[j - 1] = temp;
                    }
                }
            }
        }

        public void addMovable(MovableObject newObject)
        {
            mObjects.Add(newObject);
            objects.Add(newObject);
            everything.Add(newObject);
        }

        public void addImmovable(ImmovableObject newObject)
        {
            iObjects.Add(newObject);
            objects.Add(newObject); 
            everything.Add(newObject);           
        }

        public void addEnemy(Enemy newEnemy)
        {
            enemies.Add(newEnemy);
            anSprites.Add(newEnemy);
            everything.Add(newEnemy);  
        }

        public void addNpc(Npc newNpc)
        {
            npcs.Add(newNpc);
            anSprites.Add(newNpc);
            everything.Add(newNpc);  
        }

        public void addChest(Chest chest)
        {
            chests.Add(chest);
            objects.Add(chest);
            everything.Add(chest);
        }

        // idk if you want to add objects to everything
        public void addTripWire(TripWire wire)
        {
            tripWires.Add(wire);
        }

        public void addSpikeTrap(SpikeTrap trap)
        {
            spikeTraps.Add(trap);
            everything.Add(trap);
        }

        public void addArrowTrap(ArrowTrap trap)
        {
            arrowTraps.Add(trap);
            everything.Add(trap);
        }

        public void addProjectile(Projectile p)
        {
            everything.Add(p);
        }

        public void addMBox(MessageBox b)
        {
            box = b;
        }

        public void changePlayer(Character player)
        {
            everything.Insert(0, player);
        }

        public void changeBackgroundX(int x)
        {
            backgroundRec.X += x;
        }

        public void changeBackgroundY(int y)
        {
            backgroundRec.Y += y;
        }       
    }
}
