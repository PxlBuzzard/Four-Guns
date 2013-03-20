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
    /// Displays missions for the player to select.
    /// </summary>
    public class MissionMenu : Menu
    {
        public MissionMenu(ContentManager theContentManager, MissionManager missionManager)
        {
            showHeaderImage = false;
            base.LoadContent(theContentManager);
            stateToLoadOnEscape = FourGuns.GameState.MainMenu;
            
            //Create a list of the missions available
            int i = 140;
            foreach (Mission mission in missionManager.missions)
            {
                base.buttons.Add(new Button(mission.missionName,
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString(mission.missionName).X / 2,
                FourGuns.screenHeight - i),
                FourGuns.GameState.Playing));
                i -= 40;
            }

            base.buttons.Add(new Button("Back",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Back").X / 2,
                FourGuns.screenHeight - 40),
                FourGuns.GameState.MainMenu));
            buttons[0].isActive = true;
        }

        /// <summary>
        /// Loads a mission once it is selected and adds a continue
        /// button to the main menu.
        /// </summary>
        /// <param name="player">menu controller</param>
        public void Update(Player player, MenuManager menu)
        {
            base.Update(player);

            //load the selected mission
            if (FourGuns.currentState.Peek() == FourGuns.GameState.Playing)
            {
                menu.theMissionManager.currentMission = activeButton;
                menu.theMissionManager.missions[activeButton].LoadContent
                    (menu.theContentManager, menu.theMissionManager.collisionManager);

                //add continue button 
                if (!menu.mainMenu.addedContinueButton)
                {
                    menu.mainMenu.buttons.Add(new Button("Continue",
                    new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Continue").X / 2,
                    FourGuns.screenHeight - 40),
                    FourGuns.GameState.Playing));

                    menu.mainMenu.addedContinueButton = true;
                    menu.mainMenu.buttons[0].isActive = false;
                    menu.mainMenu.buttons[3].isActive = true;
                }
            }
        }
    }
}