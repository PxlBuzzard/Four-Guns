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
    /// Fully = trees, rocks. projectiles and actors collide.
    /// Semi = water, etc only players collide 
    /// </summary>
    public enum collision_type { fully, semi };
    
    public class StatGameObj : GameObject
    {   
        #region fields

        public collision_type collisiontype; //The collision type
        protected float zIndex; // the zIndex that tells the draw code what order to draw in

        protected int collBoxWidth;  // the collision box size
        protected int collBoxHeight;

        public int CollBoxWidth
        {
            get { return collBoxWidth; }
        }
        public int CollBoxHeight
        {
            get { return collBoxHeight; }
        }
        
        #endregion 

        /// <summary>
        /// Creates a static game object (Collidable)
        /// </summary>
        /// <param name="sprite">The sprite</param>
        /// <param name="thePosition">The x/y position</param>
        /// <param name="ct">The collision type, fully or semi
        /// Fully = trees, rocks. projectiles AND actors collide.
        /// Semi = water, etc only players collide </param>
        public StatGameObj(Texture2D sprite, Vector2 thePosition, collision_type ct)
            : base(sprite, thePosition)
        {
            position = thePosition;
            collisiontype = ct;
            
            //If fully collidable, the rectangle is the size of the sprite but half the height
            if (collisiontype == collision_type.fully)
            {
                collBoxWidth = TileLayer.TileWidth;
                collBoxHeight = TileLayer.TileHeight / 2;
                position.Y = position.Y + collBoxHeight;
            }
            //else, the rectangle is the size of the sprite (64x64)
            else if (collisiontype == collision_type.semi)
            {
                collBoxWidth = TileLayer.TileWidth;
                collBoxHeight = TileLayer.TileHeight;
            }
        }

        public StatGameObj(string tileString, Vector2 thePosition)
            : base()
        {
            position = thePosition;
            switch (tileString)
            {
                case "Collidables/rock_1": //rock
                    collBoxWidth = TileLayer.TileWidth - 30;
                    position.X += 10;
                    collBoxHeight = 60;
                    position.Y += 4;
                    collisiontype = collision_type.fully;
                    break;
                case "Collidables/tree": //tree
                case "Collidables/tree_cherryblossom": //tree
                case "Collidables/tree_dead": //tree
                    collBoxWidth = TileLayer.TileWidth - 35;
                    position.X += 15;
                    collBoxHeight = 48;
                    position.Y += 16;
                    collisiontype = collision_type.fully;
                    break;
                case "Tiles/blanktile": //blank
                    collBoxWidth = TileLayer.TileWidth;
                    collBoxHeight = TileLayer.TileHeight;
                    collisiontype = collision_type.semi;
                    break;
            }

            zIndex = (1 / (position.Y)); // set the zIndex to 1/position.y so that the sprites get drawn in the correct order
        }

        public void Draw(SpriteBatch thespritebatch)
        {
            thespritebatch.Draw(sprite, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, zIndex);
        }

    }
}
