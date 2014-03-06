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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PauseComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public enum SelectedMenu
        {
            RESUME, WEPSTAT, INVENTORY, WORLDMAP, LOAD, SAVE, QUIT
        }

        List<SelectedMenu> menus;
        List<Color> selected;
        List<String> tabNames;
        List<Vector2> positions;
        SelectedMenu currentMenu;
        KeyboardState keys, oldKeys;
        int menuIndex, prevSelected, vertDistance;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Vector2 position, textSize, nextText, inventPos, mapPos, resumePos, loadPos, savePos, quitPos, border, wepStatsPos;
        Rectangle screenRect;
        Boolean inTabs = true;
        Game1 game1;
        WepStats stats;
        InventoryMenu invent;
        WorldMap map;

        public PauseComponent(Game1 game)
            : base(game)
        {
            game.Content.RootDirectory = "Content";
            game1 = game;
            spriteFont = game.Content.Load<SpriteFont>("SpriteFont1");
            screenRect = new Rectangle((int)(game.GraphicsDevice.Viewport.Width * .05), (int)(game.GraphicsDevice.Viewport.Height * .05), (int)(game.GraphicsDevice.Viewport.Width * .9), (int)(game.GraphicsDevice.Viewport.Height * .9));

            vertDistance = (int)(screenRect.Height / 7); 
            textSize = spriteFont.MeasureString("Weapon Stats");

            position = new Vector2(screenRect.X, screenRect.Y);
            nextText = spriteFont.MeasureString("Resume");
            resumePos = new Vector2((position.X + textSize.X) - nextText.X, position.Y);
            nextText = spriteFont.MeasureString("Inventory");
            inventPos = new Vector2((position.X + textSize.X) - nextText.X, (position.Y + 2 * vertDistance));
            nextText = spriteFont.MeasureString("World Map");

            mapPos = new Vector2((position.X + textSize.X) - nextText.X, (position.Y + 3 * vertDistance));
            nextText = spriteFont.MeasureString("Weapon Stats");
            wepStatsPos = new Vector2((position.X + textSize.X) - nextText.X, (position.Y + vertDistance));
            nextText = spriteFont.MeasureString("Load");
            loadPos = new Vector2((position.X + textSize.X) - nextText.X, (position.Y + 4 * vertDistance));
            nextText = spriteFont.MeasureString("Save");
            savePos = new Vector2((position.X + textSize.X) - nextText.X, (position.Y + 5 * vertDistance));
            nextText = spriteFont.MeasureString("Quit");
            quitPos = new Vector2((position.X + textSize.X) - nextText.X, (position.Y + 6 * vertDistance));
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            border = new Vector2((position.X + textSize.X), 0);

            menus = new List<SelectedMenu>();

            menus.Add(SelectedMenu.RESUME);
            menus.Add(SelectedMenu.WEPSTAT);
            menus.Add(SelectedMenu.INVENTORY);
            menus.Add(SelectedMenu.WORLDMAP);
            menus.Add(SelectedMenu.LOAD);
            menus.Add(SelectedMenu.SAVE);
            menus.Add(SelectedMenu.QUIT);

            selected = new List<Color>();

            selected.Add(Color.Black);
            selected.Add(Color.White);
            selected.Add(Color.White);
            selected.Add(Color.White);
            selected.Add(Color.White);
            selected.Add(Color.White);
            selected.Add(Color.White);

            tabNames = new List<String>();

            tabNames.Add("Resume");
            tabNames.Add("Weapon Stats");
            tabNames.Add("Inventory");
            tabNames.Add("World Map");
            tabNames.Add("Load");
            tabNames.Add("Save");
            tabNames.Add("Quit");

            positions = new List<Vector2>();

            positions.Add(resumePos);
            positions.Add(wepStatsPos);
            positions.Add(inventPos);
            positions.Add(mapPos);
            positions.Add(loadPos);
            positions.Add(savePos);
            positions.Add(quitPos);

            stats = new WepStats(game1, border);
            invent = new InventoryMenu(border);
            map = new WorldMap(game1);
            menuIndex = 0;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();
            if (inTabs)
            {
                if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
                {
                    menuIndex--;
                    if (menuIndex < 0)
                    {
                        menuIndex = 6;
                    }
                    currentMenu = menus[menuIndex];
                    selected[menuIndex] = Color.Red;
                    selected[prevSelected] = Color.White;
                    prevSelected = menuIndex;
                }

                if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
                {
                    menuIndex++;
                    if (menuIndex > 6)
                    {
                        menuIndex = 0;
                    }
                    currentMenu = menus[menuIndex];
                    selected[menuIndex] = Color.Red;
                    selected[prevSelected] = Color.White;
                    prevSelected = menuIndex;
                }
            }

            if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
            {
                inTabs = false;
                for(int y = 0; y < 7; y++)
                {
                    selected[y] = Color.Gray;
                }
                selected[menuIndex] = Color.Red;
            }

            if (keys.IsKeyDown(Keys.Escape) && oldKeys.IsKeyUp(Keys.Escape))
            {
                inTabs = true;
                for (int y = 0; y < 7; y++)
                {
                    selected[y] = Color.White;
                }
                selected[menuIndex] = Color.Red;
            }

            oldKeys = keys;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            Rectangle rect = new Rectangle(Game1.DisplayWidth - (int)(Game1.DisplayWidth * 1.05), Game1.DisplayHeight - (int)(Game1.DisplayHeight * 1.05),
                (int)(Game1.DisplayWidth * 1.05), (int)(Game1.DisplayHeight * 1.05));

            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), rect, new Color(0,0,0, 130));

            if (inTabs)
            {
                for (int x = 0; x < 7; x++)
                {
                    spriteBatch.DrawString(spriteFont, tabNames[x], positions[x], selected[x]);
                }
            }
            else
            {
                if (currentMenu == SelectedMenu.QUIT)
                {
                    game1.Exit();
                }
                else if (currentMenu == SelectedMenu.WEPSTAT)
                {
                    stats.draw(spriteBatch, spriteFont);
                    stats.update();
                }
                else if (currentMenu == SelectedMenu.INVENTORY)
                {
                    invent.draw(spriteBatch, spriteFont);
                    invent.update();
                }
                else if (currentMenu == SelectedMenu.WORLDMAP)
                {
                    map.draw(spriteBatch, spriteFont);
                    map.update();
                }
                else if (currentMenu == SelectedMenu.RESUME)
                {
                    Game1.MainGameState = GameState.PLAYING;
                }
                else if (currentMenu == SelectedMenu.SAVE)
                {
                    spriteBatch.DrawString(spriteFont, "Unavailable in the alpha.", new Vector2(351, 208), Color.White);
                    SaveFile file = new SaveFile(1);
                    file.save(new GameData(game1.SaveData.Map));
                    Game1.MainGameState = GameState.PLAYING;
                }
                else if (currentMenu == SelectedMenu.LOAD)
                {
                    SaveFile file = new SaveFile(1);
                    file.load();
                    game1.loadGame(file.Data);
                    Game1.MainGameState = GameState.PLAYING;
                
                }

                for (int x = 0; x < 7; x++)
                {
                    spriteBatch.DrawString(spriteFont, tabNames[x], positions[x], selected[x]);
                }
            }
            spriteBatch.End();
        }

        public void getPlayer(Character player)
        {
            stats.updatePlayer(player);
            invent.updateInvent(player.PlayerInvent.ItemList, player.PlayerInvent.Counts, player.PlayerInvent.MaxSlots);
        }
    }
}
