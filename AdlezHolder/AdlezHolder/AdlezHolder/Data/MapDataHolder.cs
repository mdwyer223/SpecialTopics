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
        protected List<Projectile> projectiles;
        protected List<Particle> particles;
        protected List<Item> items;

        protected Texture2D background;
        protected Rectangle backgroundRec;
        protected Vector2 position;

        protected Song music;

        protected Texture2D bright, dark;
        protected int brightnessValue;

        bool delay;

        protected string backgroundDirectory;

        public string BackgroundDirectory
        {
            get { return backgroundDirectory; }
        }



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

        public MapDataStruct SaveData
        {
            get
            {
                MapDataStruct mapData = new MapDataStruct();

                BaseSpriteStruct[] everythingData = new BaseSpriteStruct[Everything.Count];
                for(int i=0; i < everything.Count; i++)
                    everythingData[i] = everything[i].SaveData;
                mapData.everythingData = everythingData;
                mapData.position = this.position;

                if (this.GetType() == typeof(BossHall))
                    mapData.mapId = "bHall";
                else if (this.GetType() == typeof(BossRoom))
                    mapData.mapId = "bRoom1";
                else if (this.GetType() == typeof(BottomLeftCorner))
                    mapData.mapId = "bLCorner";
                else if (this.GetType() == typeof(BottomRightCorner))
                    mapData.mapId = "bRCorner";
                else if (this.GetType() == typeof(LeftHallway))
                    mapData.mapId = "lHallway";
                else if (this.GetType() == typeof(MainRoom))
                    mapData.mapId = "mainRoom1";
                else if (this.GetType() == typeof(RightHallway))
                    mapData.mapId = "rHallway";
                else if (this.GetType() == typeof(TopLeftCorner))
                    mapData.mapId = "tLCorner";
                else if (this.GetType() == typeof(TopRightCorner))
                    mapData.mapId = "tRCorner";
                else if (this.GetType() == typeof(LeftPassage))
                    mapData.mapId = "lPassage"; //this will probably be changed to a better name later
                else if (this.GetType() == typeof(Nwot))
                    mapData.mapId = "nwot";
                else if (this.GetType() == typeof(LeftBot))
                    mapData.mapId = "";
                else if (this.GetType() == typeof(LeftTop))
                    mapData.mapId = "";
                else if (this.GetType() == typeof(MainRoom2))
                    mapData.mapId = "";
                else if (this.GetType() == typeof(RightBot))
                    mapData.mapId = "";
                else if (this.GetType() == typeof(RightTop))
                    mapData.mapId = "";
                else if (this.GetType() == typeof(TopLeft))
                    mapData.mapId = "";
                else if (this.GetType() == typeof(TopRight))
                    mapData.mapId = "";

                mapData.backgroundPath = backgroundDirectory;
                return mapData;
            }
            set
            {
                List<BaseSprite> openList = everything;
                List<BaseSprite> closedList = new List<BaseSprite>();

                foreach (BaseSprite sprite in openList)
                {
                    for(int i=0; i < value.everythingData.Length; i++)
                    {
                        if (!closedList.Contains(sprite) && 
                            sprite.SaveData.saveId.Equals(value.everythingData[i].saveId))
                        {
                            closedList.Add(sprite);
                            sprite.SaveData = value.everythingData[i];
                        }
                    }

                }

                if (value.backgroundPath != null)
                    background = Game1.GameContent.Load<Texture2D>(value.backgroundPath);
                this.position = value.position;

            }
        }
        

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

        public void load(MapStruct inMDHStruct)
        {
            this.SaveData = inMDHStruct.mapData;
        }

        public MapDataHolder()
        {
            delay = false;
            dark = Game1.GameContent.Load<Texture2D>("Random/The best thing ever");
            bright = Game1.GameContent.Load<Texture2D>("Random/LightScreen");

            everything = new List<BaseSprite>();

            objects = new List<ImmovableObject>();
            iObjects = new List<ImmovableObject>();
            mObjects = new List<MovableObject>();

            anSprites = new List<AnimatedSprite>();
            enemies = new List<Enemy>();
            npcs = new List<Npc>();
            items = new List<Item>();
            particles = new List<Particle>();

            tripWires = new List<TripWire>();
            arrowTraps = new List<ArrowTrap>();
            chests = new List<Chest>();
        }

        public MapDataHolder(Character player)
        {
            //object reference will allow for the change of the player
            //position
            delay = false;
            dark = Game1.GameContent.Load<Texture2D>("Random/The best thing ever");
            bright = Game1.GameContent.Load<Texture2D>("Random/LightScreen");

            everything = new List<BaseSprite>();

            objects = new List<ImmovableObject>();
            iObjects = new List<ImmovableObject>();
            mObjects = new List<MovableObject>();

            anSprites = new List<AnimatedSprite>();
            enemies = new List<Enemy>();
            npcs = new List<Npc>();
            items = new List<Item>();
            particles = new List<Particle>();

            tripWires = new List<TripWire>();
            arrowTraps = new List<ArrowTrap>();
            chests = new List<Chest>();
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

            brightnessValue = getBrightnessValue(map);

            topLeftCornerView = new Vector2(0, 0);
            topRightCornerView = new Vector2(Game1.DisplayWidth, 0);
            bottomLeftCornerView = new Vector2(0, Game1.DisplayHeight);
            bottomRightCornerView = new Vector2(Game1.DisplayWidth, Game1.DisplayHeight);

            //update the tripwires and adjust them here

            updateCorners();

            adjustBackground(map.Player, keys);


            for (int i = 0; i < everything.Count; i++)
            {
                if (!everything[i].IsDead && everything[i].GetType() != typeof(Character))
                {
                    everything[i].Update(map, gameTime);
                    if (everything[i].GetType() == typeof(Projectile))
                    {
                        Projectile p = (Projectile)everything[i];
                        if (p.MaxRange)
                        {
                            everything.RemoveAt(i);
                            i--;
                            if (i < 0)
                                i = 0;
                        }
                    }
                    if (everything[i].GetType() == typeof(BombObj))
                    {
                        BombObj b = (BombObj)everything[i];
                        if (b.BlowUp)
                        {
                            everything.RemoveAt(i);
                            i--;
                            if (i < 0)
                                i = 0;
                        }
                    }
                    if (everything[i].GetType().IsSubclassOf(typeof(Item)))
                    {
                        Item item = (Item)everything[i];
                        if (item.Dead)
                        {
                            everything.RemoveAt(i);
                            i--;
                        }
                    }
                }
                else if (everything[i].IsDead)
                {
                    everything.RemoveAt(i);
                    i--;
                    if (i < 0)
                        i = 0;
                }

            }

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i] != null)
                {
                    particles[i].Update(gameTime);

                    List<BaseSprite> sprites = everything;
                    for (int j = 0; j < sprites.Count; j++)
                    {
                        if (sprites[j].CollisionRec.Intersects(particles[i].Rec))
                        {
                            if (sprites[j].GetType() == typeof(Character))
                            {
                                Character c = (Character)sprites[j];
                                c.damage(particles[i].Damage);
                            }
                            else if (sprites[j].GetType().IsSubclassOf(typeof(Enemy)))
                            {
                                Enemy e = (Enemy)sprites[j];
                                e.damage(this, particles[i].Damage);
                                for (int k = 0; k < particles[i].Types.Count; k++)
                                {
                                    switch (particles[i].Types[k])
                                    {
                                        case GemType.FIRE:
                                            {
                                                e.burn(particles[i].GemEffects[k].damage,
                                                    particles[i].GemEffects[k].duration);
                                                break;
                                            }
                                        case GemType.FREEZE:
                                            {
                                                e.freeze(particles[i].GemEffects[k].damage,
                                                    particles[i].GemEffects[k].duration);
                                                break;
                                            }
                                        case GemType.STUN:
                                            {
                                                e.stun(particles[i].GemEffects[k].duration);
                                                break;
                                            }
                                        case GemType.POISON:
                                            {
                                                e.freeze(particles[i].GemEffects[k].damage,
                                                    particles[i].GemEffects[k].duration);
                                                break;
                                            }
                                        case GemType.LS:
                                            {
                                                map.Player.heal((int)(particles[i].GemEffects[k].chance * particles[i].Damage));
                                                break;
                                            }
                                    }

                                    particles.RemoveAt(i);
                                    i--;
                                    if (i < 0)
                                        i = 0;
                                    break;
                                }
                            }
                            else if (sprites[j].GetType() == typeof(BombObj))
                            {
                                BombObj b = (BombObj)sprites[j];
                                b.rushDelay();
                            }

                            if (sprites[j].GetType().IsSubclassOf(typeof(Item))
                                && sprites[j].GetType() != typeof(BombObj))
                            {
                                particles.RemoveAt(i);
                                i--;
                                if (i < 0)
                                    i = 0;
                                break;
                            }
                        }
                    }

                    if (i >= 0 && i < particles.Count)
                    {
                        if (particles[i].OffScreen)
                        {
                            particles.RemoveAt(i);
                            i--;
                            if (i < 0)
                                i = 0;
                        }
                    }
                }
            }

            changeDrawOrder(everything);
        }

        protected virtual void adjustBackground(Character player, KeyboardState keys)
        {
            float moveToY = (Game1.DisplayHeight / 2) - player.Center.Y;
            float moveToX = (Game1.DisplayWidth / 2) - player.Center.X;
            if (backgroundRec.Height != 0 && backgroundRec.Width != 0)
            {

                if (topLeftCorner.Y + moveToY < topLeftCornerView.Y && bottomLeftCorner.Y + moveToY > bottomLeftCornerView.Y)
                {
                    adjustObjectsBackgroundTripWires(new Vector2(0, moveToY), true);
                }
                else if (bottomLeftCorner.Y + moveToY < bottomLeftCornerView.Y)
                {
                    moveToY = bottomLeftCornerView.Y - bottomLeftCorner.Y;
                    adjustObjectsBackgroundTripWires(new Vector2(0, moveToY), true);
                }
                else if (topLeftCorner.Y + moveToY > topLeftCornerView.Y)
                {
                    moveToY = topLeftCornerView.Y - topLeftCorner.Y;
                    adjustObjectsBackgroundTripWires(new Vector2(0, moveToY), true);
                }

                if (topLeftCorner.X + moveToX < topLeftCornerView.X && topRightCorner.X + moveToX > topRightCornerView.X)
                {
                    adjustObjectsBackgroundTripWires(new Vector2(moveToX, 0), true);
                }
                else if (topLeftCorner.X + moveToX > topLeftCornerView.X)
                {
                    moveToX = topLeftCornerView.X - topLeftCorner.X;
                    adjustObjectsBackgroundTripWires(new Vector2(moveToX, 0), true);
                }
                else if (topRightCorner.X + moveToX < topRightCornerView.X)
                {
                    moveToX = topRightCornerView.X - topRightCorner.X;
                    adjustObjectsBackgroundTripWires(new Vector2(moveToX, 0), true);
                }
            }

            /*
            //see if there can be a way to have a free moving camera with the arrow keys
            if (player.Center.Y >= (Game1.DisplayHeight / 2) - 1 && player.Center.Y <= (Game1.DisplayHeight / 2) + 1)
            {
                //TODO: change if
            }
            else
            {
                moveObjectsUp = false;
                moveObjectsDown = false;

                float botLeftMove = (Game1.DisplayHeight / 2) - player.Center.Y;
                float topLeftMove = (Game1.DisplayHeight / 2) - player.Center.Y;

                if (player.Center.Y < (Game1.DisplayHeight / 2)  &&
                    topLeftCorner.Y + topLeftMove <= topLeftCornerView.Y)
                {
                    adjustObjectsBackgroundTripWires(new Vector2(0, topLeftMove));
                }
                // may need to put an else here too, not sure yet
                if (player.Center.Y > (Game1.DisplayHeight / 2) && 
                    bottomLeftCorner.Y + botLeftMove >= bottomLeftCornerView.Y)
                {
                    adjustObjectsBackgroundTripWires(new Vector2(0, botLeftMove));
                }
                else if (player.Center.Y > (Game1.DisplayHeight / 2))
                {
                    adjustObjectsBackgroundTripWires(new Vector2(0, bottomLeftCornerView.Y - bottomLeftCorner.Y ));
                }
            }

            if (player.Center.X >= (Game1.DisplayWidth / 2) - 1 && player.Center.X <= (Game1.DisplayWidth / 2) + 1)
            {
                //TODO: change if
            }
            else
            {
                moveObjectsLeft = false;
                moveObjectsRight = false;

                float botLeftMove = (Game1.DisplayWidth / 2) - 1 - player.Center.X;
                float botRightMove = (Game1.DisplayWidth / 2) + 1 - player.Center.X;

                if (player.Center.X > (Game1.DisplayWidth / 2) + 1 &&
                   bottomRightCorner.X + botRightMove >= bottomRightCornerView.X)
                {
                    adjustObjectsBackgroundTripWires(new Vector2(botRightMove, 0));
                }

                if (player.Center.X < (Game1.DisplayWidth / 2) - 1 &&
                    bottomLeftCorner.X + botLeftMove <= bottomLeftCornerView.X)
                    
                {
                    adjustObjectsBackgroundTripWires(new Vector2(botLeftMove, 0));
                }
            }
             */
        }

        public virtual void adjustObjectsBackgroundTripWires(Vector2 vecToMove, bool player)
        {
            backgroundRec.X += (int)vecToMove.X;
            backgroundRec.Y += (int)vecToMove.Y;

            foreach (BaseSprite obj in everything)
            {
                if (obj.GetType() == typeof(Character))
                {
                    if (player)
                    {
                        Vector2 pos = obj.Position;

                        pos += vecToMove;

                        obj.Position = pos;
                    }
                }
                else
                {
                    Vector2 pos = obj.Position;

                    pos += vecToMove;

                    obj.Position = pos;
                }
            }

            foreach (TripWire obj in tripWires)
            {
                Vector2 pos = obj.Position;

                pos += vecToMove;

                obj.Position = pos;
            }

            foreach (Particle p in particles)
            {
                Vector2 pos = p.Position;

                pos += vecToMove;

                p.Position = pos;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (delay || Game1.MainGameState == GameState.CUTSCENE)
            {
                if (background != null)
                {
                    spriteBatch.Draw(background, backgroundRec, Color.White);
                }

                foreach (BaseSprite sprite in everything)
                {
                    if (sprite.IsVisible)
                    {
                        sprite.Draw(spriteBatch);
                    }
                }

                foreach (Particle p in particles)
                {
                    if (p != null)
                        p.Draw(spriteBatch);
                }
            }
            else
            {
                delay = true;
            }

            if (brightnessValue <= 0)
            {
                spriteBatch.Draw(dark, new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight),
                    new Color(255, 255, 255) * (float)((Math.Abs(brightnessValue) / 255f)));
            }

        }

        private int getBrightnessValue(Map data)
        {
            BaseSprite[] sprites = data.CurrentData.Everything.ToArray();
            Torch[] torches = new Torch[data.CurrentData.Everything.Count];
            int currentIndex = 0;

            for (int i = 0; i < sprites.Length - 1; i++)
            {
                if (sprites[i].GetType() == typeof(Torch))
                {
                    torches[currentIndex] = (Torch)sprites[i];
                    currentIndex++;
                }
            }

            Torch torch = torches[0];
            foreach (Torch t in torches)
            {
                if (t != null)
                {
                    if (measureDistance(data.Player.Position, t.Position) < measureDistance(data.Player.Position, torch.Position))
                    {
                        torch = t;
                    }
                }
            }

            float value;
            if (torch != null)
            {
                float distance = (float)(measureDistance(data.Player.Position, torch.Position));
                value = (80f / distance) * 160;
            }
            else
                value = 0;

            if (value > 0)
            {
                return (int)(value - 80);
            }
            else
            {
                return 0;
            }
        }

        private double measureDistance(Vector2 Point1, Vector2 Point2)
        {
            double angle;
            double distance;
            if (Point1.X == Point2.X)
                return Math.Abs(Point1.Y - Point2.Y);
            if (Point1.Y == Point2.Y)
                return Math.Abs(Point1.X - Point2.X);


            Vector2 trig;
            trig.X = Math.Abs(Point1.X - Point2.X);
            trig.Y = Math.Abs(Point1.Y - Point2.Y);

            angle = Math.Atan2(trig.X, trig.Y);

            distance = trig.X / Math.Sin(angle);

            return distance;
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

        public void addBaseSprite(BaseSprite sprite)
        {
            everything.Add(sprite);
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

        public void addItem(Item item)
        {
            items.Add(item);
            everything.Add(item);
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

        public void addParticle(Particle p)
        {
            particles.Add(p);
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
