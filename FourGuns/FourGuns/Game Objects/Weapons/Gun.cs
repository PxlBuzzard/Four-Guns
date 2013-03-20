#region Using Statements
using System;
using System.Reflection;
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
    /// Holds the abstract methods for every Gun in the game.
    /// </summary>
    public class Gun : Weapon
    {
        #region Fields
        protected ContentManager theContentManager;
        protected Color bulletColor; // color modifier of the bullet
        protected Texture2D bulletSprite; //custom bullet sprite

        public enum GunWeight
            { Light, Medium, Heavy }
        public GunWeight gunWeight; //which weight category the gun is

        protected ObjectPool<Bullet> bulletPool; //bullet pool for the gun
        #endregion

        protected Gun() { }

        /// <summary>
        /// Fills in the gun variables every gun uses
        /// </summary>
        /// <param name="theContentManager"></param>
        private Gun(ContentManager theContentManager)
        {
            activeBullets = new List<Bullet>();
            this.theContentManager = theContentManager;
        }

        /// <summary>
        /// Generic pistol, should only be used for testing purposes.
        /// </summary>
        public Gun(ContentManager theContentManager, Color bColor, bool ChargeGun) : this(theContentManager)
        {
            bulletColor = bColor;
            IsCharge = ChargeGun;

            if (IsCharge)
            {
                theWeaponName = "ChargePistol";
                damagePerAttack = 10;
                ammoIncrement = 20;
                timeBetweenAttack = 200;
                maxAmmo = 100;
            }
            else
            {
                theWeaponName = "AmmoPistol";
                damagePerAttack = 10;
                ammoIncrement = 1;
                timeBetweenAttack = 200;
                maxAmmo = 20;
            }
            gunWeight = Gun.GunWeight.Light;
            attackSound = theContentManager.Load<SoundEffect>("Sounds/Weapons/Guns/Pistol");
            currentAmmo = maxAmmo;
            bulletSprite = theContentManager.Load<Texture2D>("Sprites/Gun/Bullet/bullet");

            bulletPool = new ObjectPool<Bullet>(10, new Bullet(bulletSprite, bulletColor));
        }

        /// <summary>
        /// Creates a custom gun.
        /// </summary>
        /// <param name="weaponName">name of the gun</param>
        /// <param name="dmgPerAtk">damage per shot</param>
        /// <param name="bColor">color of its bullets</param>
        /// <param name="gunSound">firing sound</param>
        /// <param name="weaponWeight">Light, Medium, or Heavy</param>
        /// <param name="isChargeGun">overheat (true) or ammo (false)</param>
        /// <param name="maxAmmoOrCharge">Maximum ammo/charge capacity</param>
        /// <param name="chargeAmountOrBulletsFired">Charge increment or number of bullets to fire</param>
        /// <param name="timeBetweenShots">The time between consecutive attacks</param>
        /// <param name="theBulletSprite">file path of the bullet sprite being used</param>
        public Gun(ContentManager theContentManager, string weaponName,
            int dmgPerAtk, Color bColor, string gunSound,
            GunWeight weaponWeight, bool isChargeGun, int maxAmmoOrCharge,
            int chargeAmountOrBulletsFired, int timeBetweenShots, string theBulletSprite)
            : this(theContentManager)
        {
            bulletColor = bColor;
            theWeaponName = weaponName;
            damagePerAttack = dmgPerAtk;
            gunWeight = weaponWeight;
            attackSound = theContentManager.Load<SoundEffect>(gunSound);
            IsCharge = isChargeGun;
            ammoIncrement = chargeAmountOrBulletsFired;
            maxAmmo = maxAmmoOrCharge;
            timeBetweenAttack = timeBetweenShots;
            bulletSprite = theContentManager.Load<Texture2D>(theBulletSprite);
            currentAmmo = maxAmmo;
            bulletPool = new ObjectPool<Bullet>(10, new Bullet(bulletSprite, bulletColor));
        }

        /// <summary>
        /// Handles generic shooting from a gun.
        /// </summary>
        public override void Shoot(Player player, GameTime gameTime)
        {
            if (ShootCheck(gameTime))
            {
                //decrement current ammo amount and reset shoot timer
                currentAmmo -= ammoIncrement;
                timeLastFired = (float)gameTime.TotalGameTime.TotalMilliseconds;

                //get bullet from pool and add to active bullets
                Bullet temp = bulletPool.Get();
                temp.damage = damagePerAttack;
                Vector2 playerPosition = new Vector2(player.Position.X + 32, player.Position.Y + 32);
                temp.pIndex = player.playerIndex;
                temp.Fire(player.facing, playerPosition);
                activeBullets.Add(temp);
                attackSound.Play(0.1f, 0.0f, 0.0f);
            }
        }

        /// <returns>if the gun is allowed to shoot</returns>
        protected bool ShootCheck(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - timeLastFired > timeBetweenAttack)
            {
                if (isCharge)
                    return (currentAmmo - ammoIncrement >= 0);
                else
                    return (currentAmmo > 0);
            }
            return false;
        }

        /// <summary>
        /// Update all bullets fired from a gun that are onscreen.
        /// </summary>
        public override void Update(GameTime gametime)
        {
            for (int i = 0; i <= activeBullets.Count - 1; i++)
            {
                activeBullets[i].Update();
                if (!activeBullets[i].isAlive)
                {
                    activeBullets[i].isAlive = true;
                    bulletPool.Return(activeBullets[i]);
                    activeBullets.RemoveAt(i);
                }
            }

            //change this to gameTime in the future
            if (isCharge && currentAmmo < maxAmmo)
                currentAmmo += 1;
        }

        /// <summary>
        /// Draws all the bullets fired from the gun.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in activeBullets)
            {
                bullet.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Create a string of the gun for saving/loading.
        /// </summary>
        /// <returns>a string holding a guns' variables</returns>
        public override string CreateWeaponString()
        {
            string gunHash = theWeaponName + "_" + damagePerAttack + "_" + bulletColor.PackedValue + "_" + 
                attackSound.Name + "_" + gunWeight + "_" + isCharge + "_" + maxAmmo + "_" + 
                ammoIncrement + "_" + timeBetweenAttack + "_" + bulletSprite.Name;
            return gunHash;
        }

        ///// <summary>
        ///// Creates a new Gun from a string.
        ///// </summary>
        ///// <param name="weaponString">the weapon string</param>
        ///// <returns>a gun</returns>
        //public static Gun WeaponFromString(ContentManager theContentManager, string weaponString)
        //{
        //    string[] parse = weaponString.Split('_');
        //    Color aBulletColor = new Color();
        //    aBulletColor.PackedValue = Convert.ToUInt32(parse[2]);
        //    GunWeight weight = (GunWeight)Enum.Parse(typeof(GunWeight), parse[4]);

        //    return new Gun(theContentManager, parse[0], Convert.ToInt32(parse[1]), aBulletColor, parse[3],
        //        weight, Convert.ToBoolean(parse[5]), Convert.ToInt32(parse[6]), Convert.ToInt32(parse[7]),
        //        Convert.ToInt32(parse[8]), parse[9]);
        //}

        /// <returns>The name of the gun</returns>
        public override string ToString()
        {
            return "Gun Name: " + theWeaponName;
        }
    }
}
