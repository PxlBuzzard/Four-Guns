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
    public class NonPlayer : Actor
    {
        public NonPlayer() { }

        public NonPlayer(ContentManager theContentManager, Vector2 position)
            : base(theContentManager, position)
        {
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
        }
    }
}
