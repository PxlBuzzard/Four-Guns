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
    /// Creates a generic projectile.
    /// </summary>
    public class Projectile : MovGameObj
    {
        #region Fields
        protected float rotation;   // the rotation (in radians) of the projectile
        public Color projectileColor;   // the color of the projectile
        public bool isAlive;    // whether or not the projectile is "alive"
        protected bool isFired; // if the projectile has been fired or not
        public bool IsFired
        {
            get { return isFired; }
            set { isFired = value; }
        }
        #endregion

        #region Constructors
        public Projectile() { speedMod = 17; }

        public Projectile(ContentManager theContentManager, int projSpeed)
            : base(theContentManager, projSpeed)
        {
            speedMod = projSpeed;
        }

        public Projectile(ContentManager theContentManager) : base(theContentManager)
        {
            speedMod = 17;
        }

        public Projectile(Texture2D theProjectileSprite, Color theProjectileColor)
            : base(theProjectileSprite, theProjectileColor)
        {
            sprite = theProjectileSprite;
            projectileColor = theProjectileColor;
            speedMod = 17;
        }
        #endregion

        /// <summary>
        /// Called once to fire a bullet from a gun.
        /// </summary>
        /// <param name="facing">player/bullet direction</param>
        /// <param name="playerPosition">starting position</param>
        public void Fire(Facing facing, Vector2 playerPosition)
        {
            this.facing = facing;
            isFired = true;

            speed = Vector2.Zero;

            //Set position and rotation based on player variables
            switch (this.facing)
            {
                case Facing.S:
                    {
                        rotation = MathHelper.PiOver2;
                        speed = new Vector2(0, 1);
                        position.X = playerPosition.X + 8;
                        position.Y = playerPosition.Y + 20;
                        break;
                    }
                case Facing.SW:
                    {
                        rotation = 3 * MathHelper.PiOver4;
                        speed = new Vector2(-1, 1);
                        position.X = playerPosition.X - 12;
                        position.Y = playerPosition.Y + 10;
                        break;
                    }
                case Facing.W:
                    {
                        rotation = MathHelper.Pi;
                        speed = new Vector2(-1, 0);
                        position.X = playerPosition.X - 20;
                        position.Y = playerPosition.Y;
                        break;
                    }
                case Facing.NW:
                    {
                        rotation = 5 * MathHelper.PiOver4;
                        speed = new Vector2(-1, -1);
                        position.X = playerPosition.X - 18;
                        position.Y = playerPosition.Y - 10;
                        break;
                    }
                case Facing.N:
                    {
                        rotation = MathHelper.PiOver2 * 3;
                        speed = new Vector2(0, -1);
                        position.X = playerPosition.X;
                        position.Y = playerPosition.Y - 26;
                        break;
                    }
                case Facing.NE:
                    {
                        rotation = 7 * MathHelper.PiOver4;
                        speed = new Vector2(1, -1);
                        position.X = playerPosition.X + 15;
                        position.Y = playerPosition.Y - 12;
                        break;
                    }
                case Facing.E:
                    {
                        rotation = 0;
                        speed = new Vector2(1, 0);
                        position.X = playerPosition.X + 21;
                        position.Y = playerPosition.Y - 4;
                        break;
                    }
                case Facing.SE:
                    {
                        rotation = MathHelper.PiOver4;
                        speed = new Vector2(1, 1);
                        position.X = playerPosition.X + 15;
                        position.Y = playerPosition.Y + 5;
                        break;
                    }
            }
            speed.Normalize();
            speed = speed * speedMod;
        }

        /// <summary>
        /// Update the projectile position onscreen.
        /// </summary>
        public void Update()
        {
            position += speed;

            //z-index the projectile
            zIndex =  (1 / (position.Y - 40));
            if (zIndex < 0)
            {
                zIndex = 0;
            }
        }

        /// <summary>
        /// Draw the bullet.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, projectileColor, rotation,
                Vector2.Zero, 1, SpriteEffects.None, zIndex);
        }
    }
}
