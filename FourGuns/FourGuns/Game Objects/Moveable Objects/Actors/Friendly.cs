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
    public class Friendly : NonPlayer
    {
        public Friendly(ContentManager theContentManager, Vector2 position)
            : base(theContentManager, position)
        {
        }
    }
}
