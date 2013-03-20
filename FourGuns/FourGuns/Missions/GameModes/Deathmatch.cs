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
    /// Deathmatch gamemode.
    /// </summary>
    public class Deathmatch : Gamemode
    {
        /// <summary>
        /// Updates anything related to Deathmatch.
        /// </summary>
        public override void Update(GameTime gameTime, Camera2D theCamera, List<Player> players, CollisionManager collManager)
        {
            
        }

        private void FinishDeathmatch(Player player)
        {
            FourGuns.currentState.Push(FourGuns.GameState.WinMenu);
        }

        /// <summary>
        /// Resets a killed player.
        /// </summary>
        /// <param name="killer">player who did the killing</param>
        /// <param name="killed">player who was killed</param>
        public override void Kill(Player killer, Player killed)
        {
            //give the killer a point
            killer.score++;

            if (killer.score == 15)
            {
                FinishDeathmatch(killer);
            }

            //respawn the killed player with full health
            killed.Position = killed.initPosition;
            killed.Health = 100;

            //refill the killed players' ammo
            foreach (Weapon weapon in killed.currentWeapons)
                weapon.currentAmmo = weapon.MaxAmmo;
        }
    }
}
