/* Four Guns
 * Created by:
 * Sean Brennan
 * Daniel Jost
 * Jim Arnold
 * Jayson Fitch
 * Shiv Rawal
 */
#region Using Statements
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
#endregion

namespace FourGuns
{
    public class FourGuns : Microsoft.Xna.Framework.Game
    {
        #region Fields/Properties

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentManager theContentManager;
        //SpriteFont menuFont;
        public static SpriteFont font;
        public static Player menuController;
        public static Camera2D camera;

        public static int playerCount;

        List<Player> playerList;
        List<GameObject> anchorList;

        MenuManager theMenuManager;

        public enum GameState 
        {
            MainMenu,
            PauseMenu,
            HelpMenu,
            GameOver,
            Playing,
            MissionMenu,
            WinMenu,
            ResetStats,
            Exit
        }
        public static Stack<GameState> currentState;
        
        public static int screenWidth;
        public static int screenHeight;

        public static Texture2D pixel;

        CollisionManager collisionManager;

        public MissionManager missionManager;
        #endregion

        public FourGuns()
        {
            graphics = new GraphicsDeviceManager(this);

            //Initialize screen size to an ideal resolution for the Xbox 360
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.IsFullScreen = true;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// </summary>
        protected override void Initialize()
        {
            currentState = new Stack<GameState>();
            
            theContentManager = Content;

            screenWidth = this.graphics.PreferredBackBufferWidth;
            screenHeight = this.graphics.PreferredBackBufferHeight;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //creates a new camera
            camera = new Camera2D(screenWidth / 2, screenHeight / 2);

            //create a 1x1 pixel to use for UI drawing
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            //Load in generic players that can be overridden later
            playerList = new List<Player>();
            playerList.Add(new Player(theContentManager, new Vector2(150, 150), PlayerIndex.One));
            playerList.Add(new Player(theContentManager, new Vector2(screenWidth - 200, 50), PlayerIndex.Two));
            playerList.Add(new Player(theContentManager, new Vector2(250, screenHeight - 100), PlayerIndex.Three));
            playerList.Add(new Player(theContentManager, new Vector2(screenWidth - 200, screenHeight - 100), PlayerIndex.Four));
            playerCount = 1;

            anchorList = new List<GameObject>();
            anchorList.AddRange(playerList);

            //initialize collision manager
            collisionManager = new CollisionManager(playerList);

            //initalize mission manager
            missionManager = new MissionManager(theContentManager, collisionManager, camera, playerList);

            //create fonts
            font = Content.Load<SpriteFont>("Fonts/visitor");

            //instantiate the menus with player one in control
            menuController = playerList[0];
            theMenuManager = new MenuManager(theContentManager, menuController, missionManager);
            currentState.Push(GameState.Exit);
            currentState.Push(GameState.MainMenu);

            //hides the mouse
            IsMouseVisible = false;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //only update the game if the screen is active
            if (IsActive)
            {
                //this could become a switch statement if needed
                if (currentState.Peek() == GameState.Playing)
                {
                    //update the players
                    foreach (Player player in playerList)
                    {
                        player.Update(gameTime, playerList);
                    }

                    //updates camera location based on player location
                    camera.CenterCameraOn(anchorList);
                    camera.Update(gameTime);

                    //manages the mission and calls the collision update inside
                    missionManager.Update(gameTime, playerList);
                }
                //check for exit state
                else if (currentState.Peek() == GameState.Exit)
                    Exit();
                else
                    theMenuManager.UpdateCurrentMenu(menuController);
            }
            else
            {
                //pauses the game if the window is not active
                if (currentState.Peek() == GameState.Playing)
                {
                    currentState.Push(GameState.PauseMenu);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            #region Draw the background
            if (FourGuns.currentState.Peek() == FourGuns.GameState.Playing ||
                FourGuns.currentState.Peek() == FourGuns.GameState.PauseMenu)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null,
                    null, null, camera.GetTransformation(graphics.GraphicsDevice));
                missionManager.Draw(spriteBatch);
                spriteBatch.End();
            }
            #endregion

            #region Draw the actors in order of where they are on the screen
            if (FourGuns.currentState.Peek() == FourGuns.GameState.Playing ||
                FourGuns.currentState.Peek() == FourGuns.GameState.PauseMenu)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null,
                    null, null, camera.GetTransformation(graphics.GraphicsDevice));

                missionManager.DrawEnemies(spriteBatch);
                missionManager.DrawCollidables(spriteBatch);
                foreach (Player player in playerList)
                {
                    if (player.ActivePlayer)
                    {
                        player.Draw(spriteBatch);
                    }
                }
                spriteBatch.End();
            }
            #endregion

            #region Draw the user interface (hp bars and menus)
            spriteBatch.Begin();
            if (FourGuns.currentState.Peek() == FourGuns.GameState.Playing ||
                FourGuns.currentState.Peek() == FourGuns.GameState.PauseMenu)
            {
                foreach (Player player in playerList)
                {
                    if (player.ActivePlayer)
                    {
                        player.DrawHealth(spriteBatch);
                    }
                }

                missionManager.DrawUI(spriteBatch);
            }

            theMenuManager.DrawCurrentMenu(spriteBatch);

            spriteBatch.End();
            #endregion

            base.Draw(gameTime);
        }
    }
}
