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
    /// Generic methods for gamemodes.
    /// </summary>
    public abstract class Gamemode
    {
        public List<Enemy> activeEnemies;

        /// <summary>
        /// Update the gamemode variables.
        /// </summary>
        public virtual void Update(GameTime gameTime, Camera2D theCamera, List<Player> players, CollisionManager collManager) { }

        /// <summary>
        /// Draw gamemode variables.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch) { }

        /// <summary>
        /// Draw any gamemode-specific UI elements.
        /// </summary>
        public virtual void DrawUI(SpriteBatch spriteBatch) { }

        /// <summary>
        /// Called when a player dies.
        /// </summary>
        /// <param name="killed">player who was killed</param>
        public virtual void Kill(Player killed) { }

        /// <summary>
        /// Called when an enemy dies.
        /// </summary>
        /// <param name="killed">enemy who was killed</param>
        public virtual void Kill(Enemy killed) { }

        /// <summary>
        /// Called when a player kills another player.
        /// </summary>
        /// <param name="killer">player who did the killing</param>
        /// <param name="killed">player who was killed</param>
        public virtual void Kill(Player killer, Player killed) { }

        /// <summary>
        /// Kill an enemy in Wave Survival.
        /// </summary>
        /// <param name="killer"></param>
        /// <param name="killed"></param>
        public virtual void Kill(Player killer, Enemy killed) { }
    }
}
