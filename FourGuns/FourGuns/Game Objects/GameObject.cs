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
    public abstract class GameObject
    {
        #region fields
        protected Texture2D sprite;
        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        //protected Rectangle currentSprite;
        #endregion

        public GameObject() { }

        public GameObject(ContentManager theContentManager) { }

        public GameObject(Texture2D sprite, Vector2 position)
        {
            this.position = position;
            this.sprite = sprite;
        }

        public GameObject(ContentManager theContentManager, Vector2 position)
        {
            this.position = position;
        }

        public GameObject(ContentManager theContentManager, int projSpeed)
        {
        }

        public GameObject(Texture2D theSprite, Color theColor) {}
    }
}
