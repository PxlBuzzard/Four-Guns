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
    class ChargeMachineGun : Gun
    {
        /// <summary>
        /// Makes a default charge machine gun
        /// </summary>
        /// <param name="bColor">The color for the bullets</param>
        public ChargeMachineGun(ContentManager theContentManager, Color bColor)
            : base(theContentManager, "Charge Machine Gun", 5, bColor,
                    "Sounds/Weapons/Guns/Pistol", Gun.GunWeight.Heavy, true, 400, 
                    20, 50, "Sprites/Gun/Bullet/bullet")
        {
        }
    }
}
