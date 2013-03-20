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
    /// Displays when the player pauses or the game loses focus.
    /// </summary>
    public class PauseMenu : Menu
    {
        public PauseMenu(ContentManager theContentManager)
        {
            showHeaderImage = true;
            base.LoadContent(theContentManager);
            stateToLoadOnEscape = FourGuns.GameState.Playing;

            base.buttons.Add(new Button("Resume",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Resume").X / 2,
                    FourGuns.screenHeight / 2 - 40), FourGuns.GameState.Playing));
            buttons[0].isActive = true;

            /*
            base.textBlocks.Add(new Text("Save (not implemented)",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Save (not implemented)").X / 2,
                    FourGuns.screenHeight / 2)));

            base.textBlocks.Add(new Text("Load (not implemented)",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Load (not implemented)").X / 2,
                    FourGuns.screenHeight / 2 + 40)));
             */

            base.buttons.Add(new Button("Exit to Menu",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Exit to Menu").X / 2,
                    FourGuns.screenHeight / 2), FourGuns.GameState.MainMenu));

            base.buttons.Add(new Button("Exit Game",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Exit Game").X / 2,
                    FourGuns.screenHeight / 2 + 40), FourGuns.GameState.Exit));

            /*
            base.buttons.Add(new Button("Fill out a feedback survey!",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Fill out a feedback survey!").X / 2,
                FourGuns.screenHeight / 2 + 160),
                FourGuns.GameState.PauseMenu, "loadsurveypage"));
             */
        }
    }
}