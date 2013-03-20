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
    /// A player-controlled character.
    /// </summary>
    public class Player : Actor
    {
        #region Fields
        public PlayerIndex playerIndex; // the players index (what player number they are)

        public GamePadState currentGamepadState;    // The current state of the player's gamepad
        public GamePadState prevGamepadState;       // the previous state of the player's gamepad
        public KeyboardState currentKeyboardState;  // the current state of the keyboard
        public KeyboardState prevKeyboardState;     // the previous state of the keyboard
        public MouseState currentMouseState;        // the current state of the mouse
        public MouseState prevMouseState;           // the previous state of the mouse
        public bool usingGamePad;       // whether or not the player is using a gamepad or not

        public Vector2 initPosition;    // the start position of the player

        public int score;       // the player's current score
        public int coins;       // the amount of money the player has
        public string profileName;  // the name of the player's profile

        #region Keymapping for Keyboard
        // the keys that map to actions for the keyboard
        public Keys upKey;
        public Keys rightKey;
        public Keys downKey;
        public Keys leftKey;
        public Keys facingUpKey;
        public Keys facingRightKey;
        public Keys facingDownKey;
        public Keys facingLeftKey;
        public Keys shootKey;
        public Keys interactKey;
        public Keys nextWeaponKey;
        public Keys previousWeaponKey;
        public Keys run;
        #endregion

        protected bool activePlayer;    // Whether or not the player is active
        public bool ActivePlayer
        {
            get { return activePlayer; }
        }

        Vector2 playerScorePosition;    // the position for the players score
        Vector2 gunBarPosition;      // an array of positions for the player's inventory

        Texture2D redHealth; // red circle for hp
        Texture2D greenHealth; // green circle for hp
        Texture2D UIbackground; //background for the UI
        Texture2D[] gunList;
        Texture2D[] gunSelected;
        Vector2 circleOrigin; //the center of the circles
        Vector2 healthPosition; //position of the circle for hp
        float hpScale; //size of the green circle
        Texture2D ammoCircle;
        Vector2 ammoOrigin;
        float ammoRotation;
        float defaultRotation;
        Vector2 UIBackgroundOrigin;

        public List<Weapon> currentWeapons;     // a list of the weapons that the player is holding
        #region counter for currentWeapon
        // an iterator for the weapons that the player has
        private int currentWeapon;
        public int CurrentWeapon
        {
            get { return currentWeapon; }
            set
            {
                if (value > currentWeapons.Count - 1)
                {
                    currentWeapon = 0;
                }
                else if (value < 0)                     
                {
                    currentWeapon = currentWeapons.Count - 1;
                }
                else
                {
                    currentWeapon = value;
                }
            }
        }
        #endregion
        //public List<Weapon> armory;     // a list of all the guns the player owns
        protected float ammoRectMultiplyer; // the multiplyer to make the ammo rectangle fit under the health bar

        public bool isRunning; //Tells if the player is running or not
        
        #endregion

        /// <summary>
        /// Initialize a new player.
        /// </summary>
        /// <param name="position">starting position of the player</param>
        /// <param name="playerIndex">which player controls this player</param>
        public Player(ContentManager theContentManager, Vector2 position, PlayerIndex playerIndex)
            : base(theContentManager, position)
        {
            //spawn location
            initPosition = position;

            currentWeapons = new List<Weapon>();
            currentWeapon = 0;

            //instantiate pistol with bullet color based on player
            if (playerIndex == PlayerIndex.One)
            {
                currentWeapons.Add(new Gun(theContentManager, Color.Red, true));
                currentWeapons.Add(new Gun(theContentManager, "Triple Shot", 15, Color.Red,
                    "Sounds/Weapons/Guns/Pistol", Gun.GunWeight.Heavy, true, 60,
                    20, 30, "Sprites/Gun/Bullet/bullet"));
                currentWeapons.Add(new Gun(theContentManager, "Charge Machine Gun", 15, Color.Red,
                    "Sounds/Weapons/Guns/Pistol", Gun.GunWeight.Heavy, true, 400,
                    20, 50, "Sprites/Gun/Bullet/bullet"));
            }
            else if (playerIndex == PlayerIndex.Two)
            {
                currentWeapons.Add(new Gun(theContentManager, Color.Black, true));
                currentWeapons.Add(new Gun(theContentManager, "Triple Shot", 15, Color.Black,
                    "Sounds/Weapons/Guns/Pistol", Gun.GunWeight.Heavy, true, 60,
                    20, 30, "Sprites/Gun/Bullet/bullet"));
                currentWeapons.Add(new Gun(theContentManager, "Charge Machine Gun", 15, Color.Black,
                    "Sounds/Weapons/Guns/Pistol", Gun.GunWeight.Heavy, true, 400,
                    20, 50, "Sprites/Gun/Bullet/bullet"));
            }
            else if (playerIndex == PlayerIndex.Three)
            {
                currentWeapons.Add(new Gun(theContentManager, Color.Pink, true));
                currentWeapons.Add(new Gun(theContentManager, "Triple Shot", 15, Color.Pink,
                    "Sounds/Weapons/Guns/Pistol", Gun.GunWeight.Heavy, true, 60,
                    20, 30, "Sprites/Gun/Bullet/bullet"));
                currentWeapons.Add(new Gun(theContentManager, "Charge Machine Gun", 15, Color.Pink,
                    "Sounds/Weapons/Guns/Pistol", Gun.GunWeight.Heavy, true, 400,
                    20, 50, "Sprites/Gun/Bullet/bullet"));
            }
            else
            {
                currentWeapons.Add(new Gun(theContentManager, Color.Blue, true));
                currentWeapons.Add(new Gun(theContentManager, "Triple Shot", 15, Color.Blue,
                    "Sounds/Weapons/Guns/Pistol", Gun.GunWeight.Heavy, true, 60,
                    20, 30, "Sprites/Gun/Bullet/bullet"));
                currentWeapons.Add(new Gun(theContentManager, "Charge Machine Gun", 15, Color.Blue,
                    "Sounds/Weapons/Guns/Pistol", Gun.GunWeight.Heavy, true, 400,
                    20, 50, "Sprites/Gun/Bullet/bullet"));
            }

            //assign local playerIndex
            this.playerIndex = playerIndex;

            //facing direction on spawn
            facing = Facing.S;

            //find what input the player is using
            if (GamePad.GetState(playerIndex).IsConnected)
                usingGamePad = true;

            //this is a fix for the menus
            UpdateInputs(true);
            UpdateInputs(false);

            //set default keyboard keys
            if (!usingGamePad)
            {
                upKey = Keys.W;
                rightKey = Keys.D;
                downKey = Keys.S;
                leftKey = Keys.A;
                facingUpKey = Keys.Up;
                facingDownKey = Keys.Down;
                facingRightKey = Keys.Right;
                facingLeftKey = Keys.Left;
                shootKey = Keys.Space;
                interactKey = Keys.E;
                nextWeaponKey = Keys.E;
                previousWeaponKey = Keys.Q;
                run = Keys.LeftControl;
            }

            spriteWidth = 64;    // the width of one sprite is 64 px
            spriteHeight = 64;  // the height of one sprite is 64 px

            //gun bar
            gunList = new Texture2D[3];
            gunSelected = new Texture2D[3];
            gunList[0] = theContentManager.Load<Texture2D>("UI/Gun Bar/gun0");
            gunSelected[0] = theContentManager.Load<Texture2D>("UI/Gun Bar/gun0selected");
            gunList[1] = theContentManager.Load<Texture2D>("UI/Gun Bar/gun1");
            gunSelected[1] = theContentManager.Load<Texture2D>("UI/Gun Bar/gun1selected");
            gunList[2] = theContentManager.Load<Texture2D>("UI/Gun Bar/gun2");
            gunSelected[2] = theContentManager.Load<Texture2D>("UI/Gun Bar/gun2selected");
            activePlayer = (playerIndex == PlayerIndex.One);  // Player one is an active player by default

            speedMod = 5;    // the player starts of with a speed modifier of 5

            maxHealth = 100;    // players have a maximum health of 100
            Health = maxHealth; // players start with max health

            #region Player specific attributes
            redHealth = theContentManager.Load<Texture2D>("UI/redcircle");
            greenHealth = theContentManager.Load<Texture2D>("UI/greencircle");
            circleOrigin = new Vector2(100, 100);
            ammoCircle = theContentManager.Load<Texture2D>("UI/ammocircle");
            ammoOrigin = new Vector2(105, 105);

            switch (playerIndex)
            {
                //the player's sprite and health bar positioning is dependent on what player they are
                case (PlayerIndex.One):
                    {
                        healthPosition = new Vector2(0, 0);
                        playerScorePosition = new Vector2(140, 0);
                        gunBarPosition = new Vector2(115, 23);
                        sprite = theContentManager.Load<Texture2D>("Sprites/Player/TrooperSprites/RedTrooper");
                        UIbackground = theContentManager.Load<Texture2D>("UI/UIbackgroundtopleft");
                        defaultRotation = 0.0f;
                        UIBackgroundOrigin = healthPosition;
                        break;
                    }
                case (PlayerIndex.Two):
                    {
                        healthPosition = new Vector2(FourGuns.screenWidth, 0);
                        playerScorePosition = new Vector2(healthPosition.X - 140, healthPosition.Y);
                        gunBarPosition = new Vector2(healthPosition.X - 175, 23);
                        sprite = theContentManager.Load<Texture2D>("Sprites/Player/TrooperSprites/GreenTrooper");
                        UIbackground = theContentManager.Load<Texture2D>("UI/UIbackgroundtopright");
                        defaultRotation = MathHelper.PiOver2;
                        UIBackgroundOrigin = new Vector2(healthPosition.X - UIbackground.Width, healthPosition.Y);
                        break;
                    }
                case (PlayerIndex.Three):
                    {
                        healthPosition = new Vector2(0, FourGuns.screenHeight);
                        playerScorePosition = new Vector2(140, healthPosition.Y - 45);
                        gunBarPosition = new Vector2(115, healthPosition.Y - 23);
                        sprite = theContentManager.Load<Texture2D>("Sprites/Player/TrooperSprites/PinkTrooper");
                        UIbackground = theContentManager.Load<Texture2D>("UI/UIbackgroundbottomleft");
                        defaultRotation = MathHelper.PiOver2 * 3;
                        UIBackgroundOrigin = new Vector2(healthPosition.X, healthPosition.Y - UIbackground.Height);
                        break;
                    }
                case (PlayerIndex.Four):
                    {
                        healthPosition = new Vector2(FourGuns.screenWidth, FourGuns.screenHeight);
                        playerScorePosition = new Vector2(healthPosition.X - 150, healthPosition.Y - 45);
                        gunBarPosition = new Vector2(healthPosition.X - 175, healthPosition.Y - 23);
                        sprite = theContentManager.Load<Texture2D>("Sprites/Player/TrooperSprites/BlueTrooper");
                        UIbackground = theContentManager.Load<Texture2D>("UI/UIbackgroundbottomright");
                        defaultRotation = MathHelper.Pi;
                        UIBackgroundOrigin = new Vector2(healthPosition.X - UIbackground.Width, healthPosition.Y - UIbackground.Height);
                        break;
                    }
            }
            #endregion
        }

        /// <summary>
        /// Update all relevant inputs for the player.
        /// </summary>
        public void UpdateInputs(bool getNewInput)
        {
            // if the player needs new inputs get them
            if (getNewInput)
            {
                if (usingGamePad)
                    currentGamepadState = GamePad.GetState(playerIndex);

                else
                {
                    currentKeyboardState = Keyboard.GetState();
                    currentMouseState = Mouse.GetState();
                }
            }
            else //set previous states to current
            {
                if (usingGamePad)
                    prevGamepadState = currentGamepadState;
                else
                {
                    prevKeyboardState = currentKeyboardState;
                    prevMouseState = currentMouseState;
                }
            }
        }

        /// <summary>
        /// Updates the player, including collision checks and input updates.
        /// </summary>
        public override void Update(GameTime gameTime, List<Player> playerList)
        {
            // check to see if the player isn't playing yet, and if they hit start have them join in
            if (!activePlayer)
                CheckForInput(playerList);
            else
            {
                
                //zero out move speed
                speed = Vector2.Zero;
                aimingDirection = Vector2.Zero;
                aiming = false;

                UpdateInputs(true);

                //update the players' weapons
                foreach (Weapon weapon in currentWeapons)
                    weapon.Update(gameTime);

                //check for movement
                if (!usingGamePad)
                    UpdateKeyboardMouse(gameTime);
                else
                    UpdateGamepad(gameTime);

                //update the health bar
               hpScale = (float)Health / 100.0f;

                // Update the ammo bar
               ammoRectMultiplyer = (float)currentWeapons[CurrentWeapon].MaxAmmo / MathHelper.PiOver2;
               ammoRotation = defaultRotation + ((float)currentWeapons[CurrentWeapon].MaxAmmo - (float)currentWeapons[CurrentWeapon].currentAmmo) / ammoRectMultiplyer;
                
                // Update the player's update
                UpdateInputs(false);
            }
        }

        #region Update Inputs
        /// <summary>
        /// Updates keyboard inputs relevant to the player.
        /// </summary>
        public void UpdateKeyboardMouse(GameTime gameTime)
        {
            //Escape key check
            if (currentKeyboardState.IsKeyDown(Keys.Escape) &&
                !prevKeyboardState.IsKeyDown(Keys.Escape))
            {
                FourGuns.currentState.Add(FourGuns.GameState.PauseMenu);
                FourGuns.menuController = this;
            }

            //If run key pressed, speed up
            if (currentKeyboardState.IsKeyDown(run))
            {
                speedMod = MathHelper.Clamp(speedMod * 1.05f, 5f, 10f);
                isRunning = true;
            }
            //If run key is released, slow down
            if (speedMod > 5 && currentKeyboardState.IsKeyUp(run))
            {
                speedMod = MathHelper.Clamp(speedMod * .95f, 5f, 10f);
                isRunning = false;
            }

            //Move keys
            if (currentKeyboardState.IsKeyDown(upKey))
            {
                speed.Y -= speedMod;
            }
            if (currentKeyboardState.IsKeyDown(downKey))
            {
                speed.Y += speedMod;
            }
            if (currentKeyboardState.IsKeyDown(leftKey))
            {
                speed.X -= speedMod;
            }
            if (currentKeyboardState.IsKeyDown(rightKey))
            {
                speed.X += speedMod;
            }

            // Reduce the speed if moving diagonally so the player doesn't gain speed
            if (speed != Vector2.Zero)
            {
                speed.Normalize();
            }
            speed = speed * speedMod;

            //facing direction/shooting
            if (currentKeyboardState.IsKeyDown(facingUpKey))
            {
                aimingDirection.Y += speedMod;
            }
            if (currentKeyboardState.IsKeyDown(facingDownKey))
            {
                aimingDirection.Y -= speedMod;
            }
            if (currentKeyboardState.IsKeyDown(facingLeftKey))
            {
                aimingDirection.X -= speedMod;
            }
            if (currentKeyboardState.IsKeyDown(facingRightKey))
            {
                aimingDirection.X += speedMod;
            }

            // Swapps weapon if any of the swap weapon keys are pressed
            if (currentKeyboardState.IsKeyDown(nextWeaponKey) &&
                !prevKeyboardState.IsKeyDown(nextWeaponKey))
            {
                CurrentWeapon = CurrentWeapon + 1;
            }
            if (currentKeyboardState.IsKeyDown(previousWeaponKey) &&
                !prevKeyboardState.IsKeyDown(previousWeaponKey))
            {
                CurrentWeapon = CurrentWeapon - 1;
            }

            aiming = (aimingDirection != Vector2.Zero);

            base.Update(gameTime);

            //shoots gun if any arrow key, the fire key, or the left mouse button is held down
            if ((aiming || currentMouseState.LeftButton == ButtonState.Pressed ||
                currentKeyboardState.IsKeyDown(shootKey)) && !isRunning)
                currentWeapons[CurrentWeapon].Shoot(this, gameTime);

            /*
            //mouse aiming
            if (currentMouseState != prevMouseState ||
                currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (currentMouseState.X > position.X + 128)
                    aimingDirection.X += speedMod;
                else if (currentMouseState.X < position.X - 64)
                    aimingDirection.X -= speedMod;

                if (currentMouseState.Y > position.Y + 128)
                    aimingDirection.Y -= speedMod;
                else if (currentMouseState.Y < position.Y - 64)
                    aimingDirection.Y += speedMod;
            }
             */
        }

        /// <summary>
        /// Updates gamepad inputs relevant to the player.
        /// </summary>
        public void UpdateGamepad(GameTime gameTime)
        {
            //Start key check
            if (currentGamepadState.IsButtonDown(Buttons.Start) &&
                !prevGamepadState.IsButtonDown(Buttons.Start))
            {
                FourGuns.currentState.Add(FourGuns.GameState.PauseMenu);
                FourGuns.menuController = this;
            }

            //Run key
            if (currentGamepadState.IsButtonDown(Buttons.LeftTrigger))
            {
                speedMod = 10;
                isRunning = true;
            }
            //If run key is released, stop running
            if (speedMod > 5 && currentGamepadState.IsButtonUp(Buttons.LeftTrigger))
            {
                speedMod = 5;
                isRunning = false;
            }

            //update speed and aiming direction
            //speed = currentGamepadState.ThumbSticks.Left * speedMod;
            speed.X = currentGamepadState.ThumbSticks.Left.X * speedMod;
            speed.Y = currentGamepadState.ThumbSticks.Left.Y * -speedMod;

            aimingDirection = currentGamepadState.ThumbSticks.Right * speedMod;
            aiming = (aimingDirection != Vector2.Zero);

            base.Update(gameTime);

            //Fire trigger
            if (currentGamepadState.IsButtonDown(Buttons.RightTrigger) && !isRunning)
                currentWeapons[CurrentWeapon].Shoot(this, gameTime);

            // Weapon Swapping
            if (currentGamepadState.IsButtonDown(Buttons.LeftShoulder) &&
                !prevGamepadState.IsButtonDown(Buttons.LeftShoulder) ||
                currentGamepadState.IsButtonDown(Buttons.DPadUp) &&
                !prevGamepadState.IsButtonDown(Buttons.DPadUp))
            {
                CurrentWeapon = CurrentWeapon - 1;
            }
            if (currentGamepadState.IsButtonDown(Buttons.RightShoulder) &&
                !prevGamepadState.IsButtonDown(Buttons.RightShoulder) ||
                currentGamepadState.IsButtonDown(Buttons.DPadDown) &&
                !prevGamepadState.IsButtonDown(Buttons.DPadDown))
            {
                CurrentWeapon = CurrentWeapon + 1;
            }
        }
        #endregion

        /// <summary>
        /// Draw the player and all associated bullets.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Weapon weapon in currentWeapons)
                weapon.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        /// <summary>
        /// If the player is not active, check for start button press to make player active.
        /// </summary>
        public void CheckForInput(List<Player> playerList)
        {
            UpdateInputs(true);
            bool keyboard = false;

            //if start or enter is pressed, add player to game
            if (!activePlayer && (currentGamepadState.Buttons.Start == ButtonState.Pressed ||
                currentKeyboardState.IsKeyDown(Keys.Enter) == true))
            {
                //check to see if the keyboard is already being used by another player
                foreach (Player player in playerList)
                {
                    if (player.ActivePlayer && player.usingGamePad == false)
                        keyboard = true;
                }
                if (!keyboard)
                {
                    activePlayer = true;
                    FourGuns.playerCount++;
                }
            }

            UpdateInputs(false);
        }

        /// <summary>
        /// Draws the corner UI.
        /// </summary>
        public void DrawHealth(SpriteBatch spriteBatch)
        {
            //UI background
            spriteBatch.Draw(UIbackground, UIBackgroundOrigin, Color.White);

            // ammoBars
            spriteBatch.Draw(ammoCircle, healthPosition, null, Color.White, ammoRotation, ammoOrigin, 1f, SpriteEffects.None, 0);

            //health circles
            spriteBatch.Draw(redHealth, healthPosition, null, Color.White, 0.0f, circleOrigin, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(greenHealth, healthPosition, null, Color.White, 0.0f, circleOrigin, hpScale, SpriteEffects.None, 0);

            //player score
            spriteBatch.DrawString(FourGuns.font, "" + score, playerScorePosition, Color.White);

            //weapon bar
            for (int i = 0; i < 3; i++)
            {
                Vector2 pos = new Vector2(gunBarPosition.X + 20 * (i + 1) - 20, gunBarPosition.Y);

                if (i == currentWeapon)
                    spriteBatch.Draw(gunSelected[i], pos, Color.White);
                else
                    spriteBatch.Draw(gunList[i], pos, Color.White);
            }
        }
    }
}
