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
    /// <summary>
    /// Manages all the missions.
    /// </summary>
    public class MissionManager
    {
        #region Fields
        public List<Mission> missions; //all missions
        public int currentMission; //current mission number
        public CollisionManager collisionManager; //collision manager
        public ContentManager theContentManager;
        protected Camera2D theCamera; //the camera
        public List<Player> players; //all players
        #endregion

        /// <summary>
        /// Instantiate the mission manager.
        /// </summary>
        /// <param name="collManager">the collision manager</param>
        /// <param name="camera">the camera</param>
        public MissionManager(ContentManager contentManager, CollisionManager collManager, Camera2D camera, List<Player> players)
        {
            collisionManager = collManager;
            theContentManager = contentManager;
            theCamera = camera;
            missions = new List<Mission>();
            this.players = players;

            //creates a hardcoded mission, will be removed later
            string[] tileFiles = new string[] { "Content/Maps/WStheMap/layer1.layer", "Content/Maps/WStheMap/layer2.layer" };
            string[] tileFiles2 = new string[] { "Content/Maps/DMtheMap/map3.Layer", "Content/Maps/DMtheMap/map3Coll.Layer" };
            missions.Add(new Mission(tileFiles, "ws", players)); //temporary wave survival mission
            missions.Add(new Mission(tileFiles2, "dm", players)); //temporary Deathmatch
            missions[0].missionName = "Wave Survival";
            missions[1].missionName = "Deathmatch";
        }

        public void CreateMission(int[,] tiles, string gamemodeType)
        {

        }

        /// <summary>
        /// Updates the current mission.
        /// </summary>
        public void Update(GameTime gametime, List<Player> players)
        {
            missions[currentMission].Update(gametime, theContentManager, theCamera, players);
        }

        /// <summary>
        /// Draws the current mission.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) 
        {
            missions[currentMission].Draw(spriteBatch);
        }

        /// <summary>
        /// Draw the enemies.
        /// </summary>
        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            missions[currentMission].DrawEnemies(spriteBatch);
        }

        /// <summary>
        /// Draws gamemode UI.
        /// </summary>
        public void DrawUI(SpriteBatch spriteBatch)
        {
            missions[currentMission].gameMode.DrawUI(spriteBatch);
        }

        /// <summary>
        /// Draws the current mission.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawCollidables(SpriteBatch spriteBatch)
        {
            missions[currentMission].DrawCollidables(spriteBatch);
        }
    }
}
