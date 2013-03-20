#region Using Statements
using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace FourGuns
{
    /// <summary>
    /// Makes a clickable button for a menu.
    /// </summary>
    public class Button
    {
        #region Fields
        protected Vector2 Position; //position the button starts at
        private Rectangle BoundingBox; //the size of the button
        protected Vector2 textPosition; //position the text starts inside the button
        public string Text; //text of the button
        public FourGuns.GameState stateToLoad; //game state to load on button click
        private string extraAction; //action other than state load (such as saving/loading)
        public bool isActive; //is the button highlighted
        #endregion

        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="theText">text inside the button</param>
        /// <param name="thePosition">position of the button onscreen</param>
        /// <param name="theState">Game state to load on click</param>
        public Button(string theText, Vector2 thePosition, FourGuns.GameState theState)
        {
            Position = thePosition;
            textPosition = new Vector2(Position.X + 5, Position.Y + 5);
            Text = theText;
            isActive = false;
            stateToLoad = theState;
            BoundingBox = new Rectangle((int)thePosition.X, (int)thePosition.Y, (int)FourGuns.font.MeasureString(Text).X + 10, (int)FourGuns.font.MeasureString(Text).Y + 10);
        }

        /// Creates a button that has a special action attached to it.
        /// </summary>
        /// <param name="theText">text inside the button</param>
        /// <param name="thePosition">position of the button onscreen</param>
        /// <param name="theState">state to load on click</param>
        /// <param name="action">name of the special action to execute</param>
        public Button(string theText, Vector2 thePosition, FourGuns.GameState theState, string action)
        {
            Position = thePosition;
            textPosition = new Vector2(Position.X + 5, Position.Y + 5);
            Text = theText;
            isActive = false;
            extraAction = action.ToLower();
            stateToLoad = theState;
            BoundingBox = new Rectangle((int)thePosition.X, (int)thePosition.Y, (int)FourGuns.font.MeasureString(Text).X + 10, (int)FourGuns.font.MeasureString(Text).Y + 10);
        }

        /// <summary>
        /// Draw the button to the screen.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!isActive)
                spriteBatch.DrawString(FourGuns.font, Text, textPosition, Color.Black);
            else
                spriteBatch.DrawString(FourGuns.font, Text, textPosition, Color.White);
        }

        /// <summary>
        /// Updates mouse state and execute action if necessary.
        /// </summary>
        /// <param name="currentMouseState">current state of the mouse</param>
        /// <param name="lastMouseState">last state of the mouse</param>
        public void Update(Player player)
        {
            if (!player.usingGamePad)
            {

                /*
                //mouse check
                if (CheckMouse(player.currentMouseState, player.prevMouseState))
                    Click();
                 */

                //keyboard check
                if (isActive &&
                    (player.currentKeyboardState.IsKeyDown(Keys.Enter) &&
                    !player.prevKeyboardState.IsKeyDown(Keys.Enter))) 
                {
                    Click();
                }
                else if (isActive && 
                    (player.currentKeyboardState.IsKeyDown(player.interactKey) &&
                    !player.prevKeyboardState.IsKeyDown(player.interactKey))) 
                        Click();
                else if (isActive && 
                    (player.currentKeyboardState.IsKeyDown(player.shootKey) &&
                    !player.prevKeyboardState.IsKeyDown(player.shootKey)))
                        Click();
            }
            //gamepad check
            else if (isActive &&
                    player.currentGamepadState.IsButtonDown(Buttons.A) &&
                    !player.prevGamepadState.IsButtonDown(Buttons.A))
                        Click();
        }

        /// <summary>
        /// Will do a special action on button click.
        /// </summary>
        private void ExtraActions()
        {
            //this if statement is broken
            if (extraAction != "")
            {
                if (extraAction == "loadsurveypage")
                {
                    //System.Diagnostics.Process.Start("https://docs.google.com/spreadsheet/viewform?formkey=dEUtamRoLURyQ2lWb0p1cE9ta2xxUGc6MQ#gid=0");
                }
                else if (extraAction == "updatekillsleft")
                {

                }
                /*
                if (extraAction == "save")
                {
                    Game1.cSave.GameSaveRequested = true;
                }
                else if (extraAction == "load")
                {
                    Game1.cSave.GameLoadRequested = true;
                }
                 */
            }
        }

        /// <summary>
        /// Switches game state and handles extra actions on button click.
        /// </summary>
        public void Click() 
        {
            FourGuns.currentState.Add(stateToLoad);
            ExtraActions();
        }

        /// <summary>
        /// Return true if mouse is inside of button's bounding box.
        /// </summary>
        /// <param name="currentMouseState">current state of the mouse</param>
        /// <param name="lastMouseState">last state of the mouse</param>
        /// <returns>true if mouse is inside bounding box</returns>
        private bool CheckMouse(MouseState currentMouseState, MouseState lastMouseState)
        {
            if (currentMouseState.X <= BoundingBox.Right &&
                currentMouseState.X >= BoundingBox.Left &&
                currentMouseState.Y <= BoundingBox.Bottom &&
                currentMouseState.Y >= BoundingBox.Top)
            {
                isActive = true;
                
                if (currentMouseState.LeftButton == ButtonState.Pressed &&
                    lastMouseState.LeftButton == ButtonState.Released)
                        return true;
            }
            return false;
        }
    }
}
