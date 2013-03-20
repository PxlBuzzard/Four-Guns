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
    /// <summary>
    /// Weapon covers projectile and melee based weapons.
    /// </summary>
    public abstract class Weapon : GameObject
    {
        #region Class Variables
        public string theWeaponName; //the name of the weapon file
        public int damagePerAttack; //how much damage each bullet or melee swing does
        public SoundEffect attackSound; //the sound the weapon makes

        public bool isCharge
        {
            get { return IsCharge; }
        }
        protected bool IsCharge;
        protected int ammoIncrement;

        public List<Bullet> activeBullets; //List of all bullets fired from the gun
        protected int maxAmmo; //total ammo the gun can hold
        public int MaxAmmo
        {
            get { return maxAmmo; }
        }
        public int currentAmmo; //current ammo count

        protected int timeBetweenAttack; // the time it takes to fire consecutive attacks
        protected float timeLastFired;
        #endregion

        public Weapon() { }

        public Weapon(ContentManager theContentManager) { }

        public virtual void Shoot(Player player, GameTime gameTime) { }

        public virtual void Melee() { }

        public virtual string CreateWeaponString() { return ""; }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
