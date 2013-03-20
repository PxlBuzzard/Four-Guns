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
    /// A class to manage the menus.
    /// </summary>
    public class MenuManager
    {
        #region Class Variables
        public ContentManager theContentManager;
        public MissionManager theMissionManager;

        //the menu classes
        public MainMenu mainMenu;
        public HelpMenu helpMenu;
        public PauseMenu pauseMenu;
        public MissionMenu missionMenu;
        public WinMenu winMenu;
        #endregion

        /// <summary>
        /// Instantiate the menu manager.
        /// </summary>
        /// <param name="player">player who paused the menu</param>
        public MenuManager(ContentManager contentManager, Player player, MissionManager missionManager)
        {
            theMissionManager = missionManager;
            theContentManager = contentManager;
            mainMenu = new MainMenu(theContentManager);
            helpMenu = new HelpMenu(theContentManager);
            pauseMenu = new PauseMenu(theContentManager);
            missionMenu = new MissionMenu(theContentManager, theMissionManager);
            winMenu = new WinMenu(theContentManager);
        }

        /// <summary>
        /// Runs the update for the current menu.
        /// </summary>
        /// <param name="player">The player controlling the menu</param>
        public void UpdateCurrentMenu(Player player)
        {
            switch (FourGuns.currentState.Peek())
            {
                case FourGuns.GameState.MainMenu:
                    mainMenu.Update(player, theContentManager);
                    break;

                case FourGuns.GameState.PauseMenu:
                    pauseMenu.Update(player);
                    break;

                case FourGuns.GameState.HelpMenu:
                    helpMenu.Update(player);
                    break;

                case FourGuns.GameState.MissionMenu:
                    missionMenu.Update(player, this);
                    break;
                case FourGuns.GameState.WinMenu:
                    winMenu.Update(theMissionManager, player);
                    break;
            }
        }

        /// <summary>
        /// Draw the current menu to the screen.
        /// </summary>
        public void DrawCurrentMenu(SpriteBatch spriteBatch)
        {
            switch (FourGuns.currentState.Peek())
            {
                case FourGuns.GameState.MainMenu:
                    mainMenu.Draw(spriteBatch);
                    break;

                case FourGuns.GameState.PauseMenu:
                    pauseMenu.Draw(spriteBatch);
                    break;

                case FourGuns.GameState.HelpMenu:
                    helpMenu.Draw(spriteBatch);
                    break;

                case FourGuns.GameState.MissionMenu:
                    missionMenu.Draw(spriteBatch);
                    break;
                case FourGuns.GameState.WinMenu:
                    winMenu.Draw(spriteBatch);
                    break;
            }
        }
    }
}
