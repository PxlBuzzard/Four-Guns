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
    /// Generic actor class to make player and enemies.
    /// </summary>
    public class Actor : MovGameObj
    {
        #region fields

        public int maxHealth;    // The actor's maximum possible health
        
        private int health;     // The actor's current health
        public int Health       // the property for health
        {
            get { return health; }
            // health can never be more than the maximum health
            set
            {
                if (value > maxHealth)
                {
                    health = maxHealth;
                }
                else
                {
                    health = value;
                }
            }
        }

        protected bool aiming;      // whether or not the actor is aiming
        protected bool moving;      // whether or not the actor is moving
        protected Vector2 aimingDirection;  // the direction the actor is aiming

        #region spritesheet

        protected int timeSinceLastChange = 0;      // the time since the last spritesheet change

        private int currFrame;      // the current frame that the actor is on (on the spritesheet)
        protected int CurrFrame
        {
            get { return currFrame; }
            set
            {
                if (value > 3)  // the spritesheet has 4 
                {
                    value = 0;
                }
                currFrame = value;
            }
        }

        private int currDirection;  // the direction the actor is facing on the spritesheet
        protected int CurrDirection
        {
            get { return currDirection; }
            set { currDirection = value; }
        }

        protected int spriteWidth;  // the sprite's width of one frame
        protected int spriteHeight; // the sprite's height of one frame

        public int SpriteWidth
        {
            get { return spriteWidth; }
            set { spriteWidth = value; }
        }
        public int SpriteHeight
        {
            get { return spriteHeight; }
            set { spriteHeight = value; }
        }

        protected Vector2 prevPosition;
        public Vector2 PrevPosition
        {
            get { return prevPosition; }
            set { prevPosition = value; }
        }

        protected Rectangle spriteOnSpriteSheet;    // the recangle for the current sprite on the spritesheet

        #endregion

        #endregion

        /// <summary>
        /// Initialize a new actor.
        /// </summary>
        /// <param name="position">starting position</param>
        public Actor(ContentManager theContentManager, Vector2 position)
            : base(theContentManager, position)
        {
            spriteOnSpriteSheet = new Rectangle(0, 0, 64, 64);
        }

        public Actor() { }

        /// <summary>
        /// Update facing direction, spritesheet, and z-index.
        /// </summary>
        public virtual void Update(GameTime gametime)
        {
            // count the time since there was a change
            timeSinceLastChange += gametime.ElapsedGameTime.Milliseconds;

            //set previous position to the position before update
            prevPosition = position;

            //move actors
            position += speed;

            #region switch sprite direction
            // checks to see if the actor is moving
            if (speed.X == 0 && speed.Y == 0)
            {
                CurrFrame = 1;  // if they are stationary then set the current frame to standing
            }
            else
            {
                if (timeSinceLastChange > 100)  // if it is time to change the animation then change it
                {
                    CurrFrame++;
                    timeSinceLastChange = 0;
                }

                if (!aiming)// if the actor is not aiming then change the direction that they are facing based on movement direction
                {
                    if (speed.X > speedMod / 2 &&
                        speed.Y > speedMod / 2)
                    {
                        facing = Facing.SE;
                    }
                    if (speed.X < speedMod / 2 &&
                        speed.Y > speedMod / 2)
                    {
                        facing = Facing.S;
                    }
                    if (speed.X > speedMod / 2 &&
                        speed.Y < speedMod / 2)
                    {
                        facing = Facing.E;
                    }
                    if (speed.X > speedMod / 2 &&
                        speed.Y < -speedMod / 2)
                    {
                        facing = Facing.NE;
                    }
                    if (speed.X < -speedMod / 2 &&
                        speed.Y > speedMod / 2)
                    {
                        facing = Facing.SW;
                    }
                    if (speed.X < -speedMod / 2 &&
                        speed.Y < speedMod / 2)
                    {
                        facing = Facing.W;
                    }
                    if (speed.X < speedMod / 2 &&
                        speed.Y < -speedMod / 2)
                    {
                        facing = Facing.N;
                    }
                    if (speed.X < -speedMod / 2 &&
                        speed.Y < -speedMod / 2)
                    {
                        facing = Facing.NW;
                    }
                }
            }
            if (aiming) // if the actor is aiming then face them in the direction that they are aiming
            {
                if (aimingDirection.X > speedMod / 2 &&
                    aimingDirection.Y > speedMod / 2)
                {
                    facing = Facing.NE;
                }
                if (aimingDirection.X < speedMod / 2 &&
                    aimingDirection.Y > speedMod / 2)
                {
                    facing = Facing.N;
                }
                if (aimingDirection.X > speedMod / 2 &&
                    aimingDirection.Y < speedMod / 2)
                {
                    facing = Facing.E;
                }
                if (aimingDirection.X > speedMod / 2 &&
                    aimingDirection.Y < -speedMod / 2)
                {
                    facing = Facing.SE;
                }
                if (aimingDirection.X < -speedMod / 2 &&
                    aimingDirection.Y > speedMod / 2)
                {
                    facing = Facing.NW;
                }
                if (aimingDirection.X < -speedMod / 2 &&
                    aimingDirection.Y < speedMod / 2)
                {
                    facing = Facing.W;
                }
                if (aimingDirection.X < speedMod / 2 &&
                    aimingDirection.Y < -speedMod / 2)
                {
                    facing = Facing.S;
                }
                if (aimingDirection.X < -speedMod / 2 &&
                    aimingDirection.Y < -speedMod / 2)
                {
                    facing = Facing.SW;
                }
            }
            #endregion

            #region spritesheet swapping

            switch (facing) // change the actors sprite direction based on the direction they are facing
            {
                case Facing.S:
                    {
                        currDirection = 0;
                        break;
                    }
                case Facing.SW:
                    {
                        currDirection = 1;
                        break;
                    }
                case Facing.W:
                    {
                        currDirection = 2;
                        break;
                    }
                case Facing.NW:
                    {
                        currDirection = 3;
                        break;
                    }
                case Facing.N:
                    {
                        currDirection = 4;
                        break;
                    }
                case Facing.NE:
                    {
                        currDirection = 5;
                        break;
                    }
                case Facing.E:
                    {
                        currDirection = 6;
                        break;
                    }
                case Facing.SE:
                    {
                        currDirection = 7;
                        break;
                    }
            }

            // set the spritesheet rectangle to match the current direcion and frame of the actor
            spriteOnSpriteSheet.X = currFrame * spriteWidth;
            spriteOnSpriteSheet.Y = currDirection * spriteHeight;
            spriteOnSpriteSheet.Width = spriteWidth;
            spriteOnSpriteSheet.Height = spriteHeight;

            #endregion

            if (position.Y > 0)
            {
                zIndex = (1 / (position.Y + 1));    // set the zIndex to 1/position.y so that the sprites get drawn in the correct order
            }
        }

        public virtual void Update(GameTime gametime, List<Player> playerList) { }

        /// <summary>
        /// Draw the Actor the the screen.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, spriteOnSpriteSheet, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, zIndex);
        }
    }
}
