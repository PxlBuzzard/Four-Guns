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
    public abstract class MovGameObj : GameObject
    {
        protected Vector2 speed;    // the current speed that the object is moving
        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        protected float speedMod; // a modifyer if the object needs to move faster than normal

        // an enum that contains all the directions that a movable game object can face
        public enum Facing
        {
            N, NE, E, SE, S, SW, W, NW
        }

        public Facing facing;   // the current direction that the object is facing

        protected float zIndex; // the zIndex that tells the draw code what order to draw in

        public MovGameObj() { }

        public MovGameObj(Texture2D sprite, Vector2 position)
            : base(sprite, position)
        {
        }

        public MovGameObj(Texture2D theSprite, Color theColor)
            : base(theSprite, theColor) { }

        public MovGameObj(ContentManager theContentManager, Vector2 position)
            : base(theContentManager, position)
        {
        }

        public MovGameObj(ContentManager theContentManager, int projSpeed)
            : base(theContentManager, projSpeed)
        {
        }

        public MovGameObj(ContentManager theContentManager)
        {
        }
    }
}
