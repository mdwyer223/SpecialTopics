using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AdlezHolder
{
    public enum GameState
    {
        CUTSCENE, PLAYING, INVENTORY,
        SHOP, MAINMENU, PAUSEMENU,
        GAMEOVER, INFOSCREEN
    }

    public enum ParticleState
    {
        OFF, RAIN, SANDSTORM
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        World world;
        MainMenu menu;
        DisplayComponent healthDisplay;
        PauseComponent pauseMenu;
        CutscenePlayer cutscene;
        Cutscene scene;
        ParticleHandler pHandler;
        InformationScreen infoScreen;

        MouseState mouse;

        // make viewport static too?
        // would it work it changing screensizes

        static ContentManager otherContent;
        public static ContentManager GameContent
        {
            get { return otherContent; }
        }

        static GameState mainGameState = GameState.INFOSCREEN;
        public static GameState MainGameState
        {
            get { return mainGameState; }
            set { mainGameState = value; }
        }
        static ParticleState pState = ParticleState.OFF;
        public static ParticleState ParticleState
        {
            get { return pState; }
            set { pState = value; }
        }
        static int displayWidth;
        public static int DisplayWidth
        {
            get { return displayWidth; }
        }

        static int displayHeight;
        public static int DisplayHeight
        {
            get { return displayHeight; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";         
            otherContent = new ContentManager(Content.ServiceProvider);
            otherContent.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            displayWidth = GraphicsDevice.Viewport.Width;
            displayHeight = GraphicsDevice.Viewport.Height; 
            
            infoScreen = new InformationScreen();

            world = new World(this);
            Components.Add(world);

            world.Visible = false;
            world.Enabled = false;

            menu = new MainMenu(this);
            Components.Add(menu);

            menu.Visible = false;
            menu.Enabled = false;

            pauseMenu = new PauseComponent(this);
            Components.Add(pauseMenu);

            pauseMenu.Visible = pauseMenu.Enabled = false;

            healthDisplay = new DisplayComponent(this);
            Components.Add(healthDisplay);

            healthDisplay.Visible = healthDisplay.Enabled = false;

            cutscene = new CutscenePlayer(this);
            Components.Add(cutscene);
            scene = new Cutscene();

            cutscene.Enabled = false;

            pHandler = new ParticleHandler(this);
            Components.Add(pHandler);
            pHandler.Visible = pHandler.Enabled = true;

            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            mouse = Mouse.GetState();

            if (mainGameState == GameState.PLAYING)
            {
                healthDisplay.getPlayerHealth(world.Map.Player.MaxHitPoints, 
                    world.Map.Player.HitPoints);
                healthDisplay.getPlayer(world.Map.Player);

                world.Visible = true;
                world.Enabled = true;

                healthDisplay.Visible = true;
                healthDisplay.Enabled = true;

                pauseMenu.Enabled = false;
                pauseMenu.Visible = false;

                menu.Visible = false;
                menu.Enabled = false;

                cutscene.Enabled = false;
            }
            else if (mainGameState == GameState.MAINMENU)
            {
                menu.Enabled = true;
                menu.Visible = true;

                world.Visible = false;
                world.Enabled = false;

                healthDisplay.Visible = false;
                healthDisplay.Enabled = false;

                pauseMenu.Enabled = false;
                pauseMenu.Visible = false;

                cutscene.Enabled = false;

                //if menu.isLoadingFile
                //world = new world(SaveFile);
                //components.add(world) <-- maybe, test the top first
            }

            else if (mainGameState == GameState.PAUSEMENU)
            {
                pauseMenu.Visible = pauseMenu.Enabled = true;
                pauseMenu.getInventory(world.Map.Player.PlayerInvent.ItemList, world.Map.Player.PlayerInvent.MaxSlots);

                menu.Visible = false;
                menu.Enabled = false;

                world.Visible = true;
                world.Enabled = false;

                healthDisplay.Visible = false;
                healthDisplay.Enabled = false;
            }
            else if (mainGameState == GameState.CUTSCENE)
            {
                world.Map.changeMap(new LeftPassage());
                cutscene.Data = world.Map.CurrentData;

                menu.Visible = false;
                menu.Enabled = false;

                world.Enabled = false;
                world.Visible = true;

                cutscene.Enabled = true;

                cutscene.playCutscene(scene, world.Map.Player);
                if (cutscene.Scene != null && cutscene.Scene.Over)
                {
                    mainGameState = GameState.PLAYING;
                    world.Map.changeMap(new MainRoom(world.Map.Player));
                }
            }
            else if (mainGameState == GameState.GAMEOVER)
            {
                menu.Enabled = false;
                menu.Visible = false;

                world.Visible = false;
                world.Enabled = true;

                healthDisplay.Visible = true;
                healthDisplay.Enabled = true;

                pauseMenu.Enabled = false;
                pauseMenu.Visible = false;

                cutscene.Enabled = false;

                world.Map.Player.cutsceneMove(Orientation.UP);
                world.Map.changeMap(new MainRoom(world.Map.Player));
            }
            else if (mainGameState == GameState.INFOSCREEN)
            {
                if (keys.IsKeyDown(Keys.Space))
                {
                    mainGameState = GameState.MAINMENU;
                }
            }
            base.Update(gameTime);

            pHandler.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (mainGameState == GameState.MAINMENU)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Content.Load<Texture2D>("TitleScreen"), new Rectangle(0,0, DisplayWidth, DisplayHeight), Color.White);
                spriteBatch.End();
            }
            else if (mainGameState == GameState.INFOSCREEN)
            {
                spriteBatch.Begin();
                infoScreen.Draw(spriteBatch);
                spriteBatch.End();
            }

            base.Draw(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.RightShift))
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Random/Particle"), new Rectangle(mouse.X, mouse.Y, 2, 2), Color.Red);
                spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont1"), 
                    "Position:  " + mouse.X + " " + mouse.Y, new Vector2(0, Game1.DisplayHeight - 30),Color.White);
                spriteBatch.End();
            }

            pHandler.Draw(gameTime);
        }
    }
}
