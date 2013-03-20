#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// A bullet class.
    /// </summary>
    public class Bullet : Projectile
    {
        public int damage; //bullet damage
        public PlayerIndex pIndex;

        /// <summary>
        /// Creates a generic bullet.
        /// </summary>
        public Bullet() { speedMod = 17; }

        /// <summary>
        /// Creates a generic bullet.
        /// </summary>
        public Bullet(ContentManager theContentManager) :
            base(theContentManager)
        {
            sprite = theContentManager.Load<Texture2D>("Sprites/Gun/Bullet/bullet");
            isAlive = true;
        }

        /// <summary>
        /// Creates a bullet using a custom sprite and color.
        /// </summary>
        /// <param name="theBulletSprite">bullet texture</param>
        /// <param name="theBulletColor">color of the bullet</param>
        public Bullet(Texture2D theBulletSprite, Color theBulletColor) :
            base(theBulletSprite, theBulletColor)
        {
            isAlive = true;
        }

        /// <summary>
        /// Creates a bullet from scratch using a custom sprite.
        /// </summary>
        /// <param name="customBulletSprite">file path of the bullet sprite</param>
        /// <param name="bulletSprite">file path of the bullet sprite</param>
        /// <param name="theBulletColor">color of the bullet</param>
        public Bullet(ContentManager theContentManager, string bulletSprite, Color theBulletColor) 
            : base(theContentManager)
        {
            sprite = theContentManager.Load<Texture2D>(bulletSprite);
            projectileColor = theBulletColor;
            isAlive = true;
        }
    }
}
