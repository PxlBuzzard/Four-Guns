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
    public class Interactive : StatGameObj
    {
        public Interactive(Texture2D sprite, Vector2 position)
            : base(sprite, position, collision_type.fully)
        {
        }
    }
}
