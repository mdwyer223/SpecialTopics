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
        GAMEOVER, INFOSCREEN, TALKING, BLANK, INTRO
    }

    public enum ParticleState
    {
        OFF, RAIN, SANDSTORM, SNOW
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static CutscenePlayer cutscenePlayer;
        Cutscene currentScene;
        bool action = false;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //GameTime game;
        
        static World world;
        MainMenu menu;
        DisplayComponent healthDisplay;
        PauseComponent pauseMenu;
        DeathAnimation deathAni;
        ParticleHandler pHandler;
        InformationScreen infoScreen;
        InGameEditor editor;

        StoryIntro intro;

        MouseState mouse;

        // make viewport static too?
        // would it work it changing screensizes

        static ContentManager otherContent;
        public static ContentManager GameContent
        {
            get { return otherContent; }
        }
        static GameState previousGameState = GameState.BLANK;
        public static GameState PreviousGameState
        {
            get { return previousGameState; }
            set { previousGameState = value; }
        }
        static GameState mainGameState = GameState.INTRO;
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
            intro = new StoryIntro();

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

            pHandler = new ParticleHandler(this);
            Components.Add(pHandler);
            pHandler.Visible = pHandler.Enabled = true;

            cutscenePlayer = new CutscenePlayer(this, world);
            Components.Add(cutscenePlayer);
            currentScene = new AlphaCutscene(world);

            cutscenePlayer.Enabled = false;

            editor = new InGameEditor(this, world);
            Components.Add(editor);
            editor.Enabled = editor.Visible = true;

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


            if (world.Map != null)
            {
                editor.getMap(world.Map);
            }

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

                cutscenePlayer.Enabled = false;
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

                cutscenePlayer.Enabled = false;
            }

            else if (mainGameState == GameState.PAUSEMENU)
            {
                pauseMenu.Visible = pauseMenu.Enabled = true;
                pauseMenu.getPlayer(world.Map.Player);

                menu.Visible = false;
                menu.Enabled = false;

                world.Visible = true;
                world.Enabled = false;

                healthDisplay.Visible = false;
                healthDisplay.Enabled = false;
            }
            else if (mainGameState == GameState.CUTSCENE)
            {
                cutscenePlayer.Data = world.Map.CurrentData;

                menu.Visible = false;
                menu.Enabled = false;

                healthDisplay.Visible = false;

                world.Enabled = true;
                world.Visible = true;

                cutscenePlayer.Enabled = true;
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

                cutscenePlayer.Enabled = false;

                world.Map.Player.cutsceneMove(Orientation.UP);
            }
            else if (mainGameState == GameState.INFOSCREEN)
            {
                if (keys.IsKeyDown(Keys.Space))
                {
                    mainGameState = GameState.MAINMENU;
                }
            }
            else if (mainGameState == GameState.INTRO)
            {
                intro.Update(gameTime);
                if (intro.Over)
                {
                    mainGameState = GameState.MAINMENU;
                }
            }
            else if (mainGameState == GameState.TALKING)
            {
                world.Visible = true;
                world.Enabled = true;

                healthDisplay.Visible = false;
                healthDisplay.Enabled = false;

                pauseMenu.Enabled = false;
                pauseMenu.Visible = false;

                menu.Visible = false;
                menu.Enabled = false;

                cutscenePlayer.Enabled = true;
            }
            base.Update(gameTime);

            //previousGameState = mainGameState;
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
            else if (mainGameState == GameState.INTRO)
            {
                spriteBatch.Begin();
                intro.Draw(spriteBatch);
                spriteBatch.End();
            }

            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(Content.Load<Texture2D>("Random/Particle"), new Rectangle(mouse.X, mouse.Y, 2, 2), Color.Red);
            spriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont1"), "X: " + mouse.X + " Y: " + mouse.Y, new Vector2(5, 460), Color.White);
            spriteBatch.End();
        }

        public static void newCutscene(Cutscene scene, Character player)
        {
            if (scene != null)
            {
                scene.setWorld(world);
                cutscenePlayer.playCutscene(scene, player);
                mainGameState = GameState.CUTSCENE;
            }
        }

        public void loadGame(GameData dataFile)
        {
            world.load(dataFile);
        }
    }
}
