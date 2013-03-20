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
    /// Displays useful instructions.
    /// </summary>
    public class HelpMenu : Menu
    {
        #region Fields
        private Texture2D xboxHelp;
        private Texture2D keyboardHelp;
        private Rectangle helpSize;
        private bool usingGamePad;
        #endregion

        /// <summary>
        /// Instantiate the Help menu.
        /// </summary>
        public HelpMenu(ContentManager theContentManager)
        {
            showHeaderImage = false;
            base.LoadContent(theContentManager);
            stateToLoadOnEscape = FourGuns.GameState.MainMenu;
            
            //give a different set of instructions based on player input
            xboxHelp = theContentManager.Load<Texture2D>("Menu/xboxhelp");
            keyboardHelp = theContentManager.Load<Texture2D>("Menu/keyboardhelp");
            helpSize = new Rectangle(350, 175, 600, 399);

            base.buttons.Add(new Button("Return to Main Menu",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Return to Main Menu").X / 2,
                    FourGuns.screenHeight - 40),
                    FourGuns.GameState.MainMenu));

            buttons[0].isActive = true;
        }

        /// <summary>
        /// Find which input method the player is using
        /// </summary>
        /// <param name="player">player controlling the menu</param>
        public override void Update(Player player)
        {
            base.Update(player);

            this.usingGamePad = player.usingGamePad;
        }

        /// <summary>
        /// Draw the help picture for Xbox or keyboard.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //display instructions based on the input method
            if (usingGamePad)
                spriteBatch.Draw(xboxHelp, helpSize, Color.White);
            else
                spriteBatch.Draw(keyboardHelp, helpSize, Color.White);
        }
    }
}
