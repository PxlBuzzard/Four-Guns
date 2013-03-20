#region Using Statements
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
#endregion

namespace FourGuns
{
    /// <summary>
    /// A generic menu system.
    /// </summary>
    public abstract class Menu
    {
        #region Class Variables
        public Rectangle bgSize; //location of the background texture
        public Texture2D bgTexture; //the menu background texture
        public Texture2D menuHeaderImage; //header image
        public Rectangle headerRectangle; //header location
        public Texture2D bgTextureMain; //the menu background texture
        protected bool showHeaderImage;
        public Song menuMusic;

        public List<Button> buttons; //list of buttons in a menu
        public List<Text> textBlocks; //list of texts in a menu
        public int activeButton; //currently highlighted button
        public FourGuns.GameState stateToLoadOnEscape; //the logical state to load if user presses escape key
        #endregion

        public Menu() { }

        /// <summary>
        /// Runs once on game load to initialize the menus.
        /// </summary>
        public void LoadContent(ContentManager theContentManager)
        {
            //instantiate buttons and text
            buttons = new List<Button>();
            textBlocks = new List<Text>();

            //initialize the background
            bgSize = new Rectangle(0, 0, FourGuns.screenWidth, FourGuns.screenHeight);
            bgTextureMain = theContentManager.Load<Texture2D>("Menu/menu");
            bgTexture = theContentManager.Load<Texture2D>("Menu/menubg");

            //create the header image
            if (showHeaderImage)
            {
                menuHeaderImage = theContentManager.Load<Texture2D>("Menu/menuheader");
                headerRectangle = new Rectangle(400, 0, menuHeaderImage.Width, menuHeaderImage.Height);
            }

            //start the menu music
            menuMusic = theContentManager.Load<Song>("Sounds/Music/main_theme");
            MediaPlayer.Play(menuMusic);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// Update the buttons and checks for state change.
        /// </summary>
        public virtual void Update(Player player) 
        {
            player.UpdateInputs(true);

            #region Move in the menus
            if (!player.usingGamePad)
            {
                /*
                //updates buttons for mouse input
                if (player.currentMouseState != player.prevMouseState)
                    buttons[activeButton].isActive = false;
                 */

                //keyboard navigation
                if ((player.currentKeyboardState.IsKeyDown(player.downKey) &&
                    !player.prevKeyboardState.IsKeyDown(player.downKey)) ||
                    (player.currentKeyboardState.IsKeyDown(Keys.Down) &&
                    !player.prevKeyboardState.IsKeyDown(Keys.Down)))
                        NextButton(false);

                else if ((player.currentKeyboardState.IsKeyDown(player.upKey) &&
                    !player.prevKeyboardState.IsKeyDown(player.upKey)) ||
                    (player.currentKeyboardState.IsKeyDown(Keys.Up) &&
                    !player.prevKeyboardState.IsKeyDown(Keys.Up)))
                        NextButton(true);
            }
            else
            {
                //gamepad navigation
                if ((player.currentGamepadState.IsButtonDown(Buttons.DPadDown) &&
                    !player.prevGamepadState.IsButtonDown(Buttons.DPadDown)) ||
                    (player.currentGamepadState.IsButtonDown(Buttons.LeftThumbstickDown) &&
                    !player.prevGamepadState.IsButtonDown(Buttons.LeftThumbstickDown)))
                        NextButton(false);
                else if ((player.currentGamepadState.IsButtonDown(Buttons.DPadUp) &&
                    !player.prevGamepadState.IsButtonDown(Buttons.DPadUp)) ||
                    (player.currentGamepadState.IsButtonDown(Buttons.LeftThumbstickUp) &&
                    !player.prevGamepadState.IsButtonDown(Buttons.LeftThumbstickUp)))
                        NextButton(true);
            }

            // Updates the buttons
            for (int i = 0; i <= buttons.Count - 1; i++)
            {
                buttons[i].Update(player);
                if (buttons[i].isActive)
                    activeButton = i;
            }
            #endregion

            #region Escape/Start key check
            if (player.currentKeyboardState.IsKeyDown(Keys.Escape) &&
                !player.prevKeyboardState.IsKeyDown(Keys.Escape))
            {
                FourGuns.currentState.Pop();
            }

            else if (player.currentGamepadState.IsButtonDown(Buttons.Start) &&
                !player.prevGamepadState.IsButtonDown(Buttons.Start))
            {
                FourGuns.currentState.Pop();
            }
            #endregion

            player.UpdateInputs(false);
        }

        /// <summary>
        /// Highlights the next button in the direction of the input.
        /// </summary>
        public void NextButton(bool moveUp)
        {
            buttons[activeButton].isActive = false;

            if (moveUp)
                activeButton--;
            else
                activeButton++;

            //loop around to the other side of the menu if needed
            if (activeButton >= buttons.Count)
                activeButton = 0;
            else if (activeButton < 0)
                activeButton = buttons.Count - 1;

            buttons[activeButton].isActive = true;
        }

        /// <summary>
        /// Draw the menu on the screen.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch) 
        {
            //background
            spriteBatch.Draw(bgTexture, bgSize, Color.White);

            //draw the main menu background
            if (FourGuns.currentState.Peek() == FourGuns.GameState.MainMenu || FourGuns.currentState.Peek() == FourGuns.GameState.HelpMenu 
                    || FourGuns.currentState.Peek() == FourGuns.GameState.MissionMenu)
                spriteBatch.Draw(bgTextureMain, bgSize, Color.White);

            //buttons
            foreach (Button button in buttons)
                button.Draw(spriteBatch);

            //text
            foreach (Text text in textBlocks)
                text.Draw(spriteBatch);

            //draw header image
            if (showHeaderImage)
                spriteBatch.Draw(menuHeaderImage, headerRectangle, Color.White);
        }
    }
}
