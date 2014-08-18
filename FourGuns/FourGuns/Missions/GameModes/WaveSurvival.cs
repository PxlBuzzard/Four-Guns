#region Using Statements
using System;
//using System.Collections.Generic;
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
    /// Wave Survival gamemode.
    /// </summary>
    public class WaveSurvival : Gamemode
    {
        #region Fields
        private Random rnd;
        private DynamicObjectPool<Zombie> enemyPool;
        private Vector2 spawnPosition;
        private int mapWidth;
        private int mapHeight;

        private Vector2 wavesRemainingPosition;
        private Vector2 enemiesRemainingPosition;

        private Texture2D UIheader;
        private Vector2 headerPosition;

        //wave survival variables
        private int timeSinceLastSpawn;
        private int[] enemiesInWave;
        private int enemiesSpawnedInWave;
        private int enemiesKilledInWave;

        private int currentWave;
        private int totalWaves;
        #endregion

        public WaveSurvival(ContentManager theContentManager, TileLayer map)
        {
            //instantiate things
            activeEnemies = new List<Enemy>();
            rnd = new Random();
            enemyPool = new DynamicObjectPool<Zombie>(15, new Zombie(theContentManager, Vector2.Zero));

            //save map size
            mapWidth = map.WidthInPixels;
            mapHeight = map.HeightInPixels;

            //load the header pic
            UIheader = theContentManager.Load<Texture2D>("UI/wavesurvivalheader");
            headerPosition = new Vector2(FourGuns.screenWidth / 2 - 147, 0);

            //set position of strings onscreen
            wavesRemainingPosition = new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Waves Survived: 5 of 5").X / 2, 10);
            enemiesRemainingPosition = new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Enemies Remaining: 10").X / 2, 35);

            //create enemy waves
            totalWaves = 5;
            currentWave = 1;
            enemiesKilledInWave = 0;
            enemiesSpawnedInWave = 0;
            enemiesInWave = new int[6];
            for (int i = 1; i <= totalWaves; i++)
            {
                enemiesInWave[i] = i * 5;
            }
        }

        /// <summary>
        /// Updates anything related to Wave Survival.
        /// </summary>
        public override void Update(GameTime gameTime, Camera2D theCamera, List<Player> players, CollisionManager collManager)
        {
            //update and kill enemies
            for (int i = 0; i <= activeEnemies.Count - 1; i++)
            {
                activeEnemies[i].Update(gameTime, players);
                if (activeEnemies[i].Health <= 0)
                {
                    activeEnemies.RemoveAt(i);
                    enemiesKilledInWave++;
                }
            }

            //spawn new enemy
            if (enemiesSpawnedInWave < enemiesInWave[currentWave] && (activeEnemies.Count < 1 || timeSinceLastSpawn > (2000 / FourGuns.playerCount)))
            {
                Enemy newEnemy = enemyPool.Get();

                //refresh variables
                GenerateSpawnPosition(theCamera, collManager);
                newEnemy.Position = spawnPosition;
                newEnemy.Health = newEnemy.maxHealth;


                activeEnemies.Add(newEnemy);
                timeSinceLastSpawn = 0;

                enemiesSpawnedInWave++;
            }

            //start new wave if you finish the current one
            if (enemiesKilledInWave >= enemiesInWave[currentWave])
                BeginNextWave();

            timeSinceLastSpawn += gameTime.ElapsedGameTime.Milliseconds;
        }

        /// <summary>
        /// Create a spawn position offscreen but on the map.
        /// </summary>
        /// <param name="theCamera">the camera</param>
        private void GenerateSpawnPosition(Camera2D theCamera, CollisionManager collManager)
        {
            int check = 0;
            do 
            {
                if (check > 100)
                    break;

                if (theCamera.TopLeft.X > 64)
                    spawnPosition.X = rnd.Next((int)theCamera.TopLeft.X);
                else if (theCamera.BottomRight.X < mapWidth - 64)
                    spawnPosition.X = (int)theCamera.BottomRight.X + rnd.Next(mapWidth - (int)theCamera.BottomRight.X);
                else
                    spawnPosition.X = rnd.Next(mapWidth);

                if (theCamera.TopLeft.Y > 64)
                    spawnPosition.Y = rnd.Next((int)theCamera.TopLeft.Y);
                else if (theCamera.BottomRight.Y < mapHeight - 64)
                    spawnPosition.Y = (int)theCamera.BottomRight.Y + rnd.Next(mapHeight - (int)theCamera.BottomRight.Y);
                else
                    spawnPosition.Y = rnd.Next(mapHeight);

                check++;
                //check for legal spawn position
            } while (collManager.collTiles[(int)spawnPosition.Y / 64, (int)spawnPosition.X / 64].Count != 0);
        }

        /// <summary>
        /// Begin a new wave.
        /// </summary>
        private void BeginNextWave()
        {
            //if final wave was just finished
            if (currentWave >= totalWaves)
            {
                FinishWS();
            }
            else
            {
                enemiesKilledInWave = 0;
                enemiesSpawnedInWave = 0;
                currentWave++;

                //10 second wait between waves
                timeSinceLastSpawn = -8000;
            }
        }

        private void FinishWS()
        {
            FourGuns.currentState.Push(FourGuns.GameState.WinMenu);
        }

        /// <summary>
        /// Resets a killed player.
        /// </summary>
        /// <param name="killer">player who did the killing</param>
        /// <param name="killed">player who was killed</param>
        public override void Kill(Player killed)
        {
            //respawn the killed player with full health
            killed.Position = killed.initPosition;
            killed.Health = 100;

            //refill the killed players' ammo
            foreach (Weapon weapon in killed.currentWeapons)
                weapon.currentAmmo = weapon.MaxAmmo;
        }

        /// <summary>
        /// Resets a killed player.
        /// </summary>
        /// <param name="killer">player who did the killing</param>
        /// <param name="killed">player who was killed</param>
        public override void Kill(Player killer, Player killed)
        {
            Kill(killed);
        }

        /// <summary>
        /// Kills an enemy.
        /// </summary>
        /// <param name="killed">enemy being killed</param>
        public override void Kill(Player killer, Enemy killed)
        {
            activeEnemies.Remove(killed);
            killer.score++;
            enemiesKilledInWave++;
        }

        /// <summary>
        /// Draw the enemies.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in activeEnemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        public override void DrawUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UIheader, headerPosition, Color.White);
            spriteBatch.DrawString(FourGuns.font, "Waves Survived: " + currentWave + " of " + totalWaves, wavesRemainingPosition, Color.White);
            spriteBatch.DrawString(FourGuns.font, "Enemies Remaining: " + (enemiesInWave[currentWave] - enemiesKilledInWave), enemiesRemainingPosition, Color.White);
        }
    }
}
