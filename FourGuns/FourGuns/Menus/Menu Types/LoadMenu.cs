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
    /// Shows a help screen as the game "loads."
    /// </summary>
    public class LoadMenu : Menu
    {
        public LoadMenu(ContentManager theContentManager)
        {
            showHeaderImage = false;
            base.LoadContent(theContentManager);
            stateToLoadOnEscape = FourGuns.GameState.MainMenu;

            base.textBlocks.Add(new Text("You win!",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Player   wins!").X / 2,
                    FourGuns.screenHeight / 2)));

            base.buttons.Add(new Button("Exit to Menu",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Exit to Menu").X / 2,
                    FourGuns.screenHeight / 2 + 40), FourGuns.GameState.MainMenu));

            buttons[0].isActive = true;
        }

        /// <summary>
        /// Draw the winning player.
        /// </summary>
        /// <param name="player">player who won</param>
        public void Update(MissionManager missionManager, Player player)
        {
            base.Update(player);
            base.textBlocks[0].Text = "Player " +" wins!";
        }
    }
}