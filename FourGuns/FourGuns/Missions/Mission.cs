#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    public class Mission
    {
        #region Fields
        public string missionName; //name of the mission
        public string missionDesc; //mission description
        public string gamemodeString; //what gamemode this map is
        public int goldOnCompletion; //gold recieved on mission completion
        private string[] tileStrings; //2D array of tiles
        public int mapWidth; //map width
        public int mapHeight; //map height
        private List<Player> players;
        public Gamemode gameMode; //gamemode of the mission
        protected CollisionManager collisionManager; //collision manager
        public List<TileLayer> tileLayers; //map tiles
        #endregion

        /// <summary>
        /// Instantiate a new mission.
        /// </summary>
        /// <param name="tiles">tile array</param>
        /// <param name="gamemode">gamemode type</param>
        public Mission(string[] tilelayers, string gamemode, List<Player> playerList)
        {
            tileStrings = tilelayers;
            gamemodeString = gamemode;
            players = playerList;
        }

        /// <summary>
        /// Load the content of the mission.
        /// </summary>
        /// <param name="collManager">the collision manager</param>
        public void LoadContent(ContentManager theContentManager, CollisionManager collManager)
        {
            //reset players
            ResetPlayers();

            //build the map
            tileLayers = new List<TileLayer>();
            tileLayers.Add(TileLayer.FromFile(theContentManager, tileStrings[0]));
            tileLayers.Add(TileLayer.FromFile(theContentManager, tileStrings[1]));

            //instantiate collision manager
            collisionManager = collManager;
            collisionManager.SetCollisionArray(tileLayers, tileStrings);

            //define map width and height
            mapHeight = tileLayers[0].HeightInPixels;
            mapWidth = tileLayers[0].WidthInPixels;

            //instantiate the gamemode
            switch (gamemodeString)
            {
                case "dm":
                    gameMode = new Deathmatch();
                    break;
                case "ws":
                    gameMode = new WaveSurvival(theContentManager, tileLayers[0]);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Reset players if they have played a mission already.
        /// </summary>
        private void ResetPlayers()
        {
            foreach (Player player in players)
            {
                player.Health = 100;
                player.Position = player.initPosition;
                player.score = 0;
            }
        }

        /// <summary>
        /// Update the mission.
        /// </summary>
        /// <param name="theCamera">the camera</param>
        public void Update(GameTime gameTime, ContentManager theContentManager, Camera2D theCamera, List<Player> players)
        {
            collisionManager.Update(this, theCamera);
            gameMode.Update(gameTime, theCamera, players, collisionManager);
        }

        /// <summary>
        /// Draw the tile map.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            tileLayers[0].Draw(spriteBatch);
        }

        /// <summary>
        /// Draw the enemies.
        /// </summary>
        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            gameMode.Draw(spriteBatch);
        }

        /// <summary>
        /// Draw the collidables on the map.
        /// </summary>
        public void DrawCollidables(SpriteBatch spriteBatch)
        {
            tileLayers[1].DrawCollidables(spriteBatch);
        }
    }
}
