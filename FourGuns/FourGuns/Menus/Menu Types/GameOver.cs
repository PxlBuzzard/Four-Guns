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
    public class GameOver : Menu
    {
        public GameOver(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager);
            stateToLoadOnEscape = FourGuns.GameState.MainMenu;
            showHeaderImage = true;

            base.buttons.Add(new Button("Game Over!",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Game Over!").X / 2,
                    FourGuns.screenHeight / 2 - 60), FourGuns.GameState.GameOver));

            base.buttons.Add(new Button("Load",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Load").X / 2,
                    FourGuns.screenHeight / 2), FourGuns.GameState.Playing, "load"));

            base.buttons.Add(new Button("Exit to Menu",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Exit to Menu").X / 2,
                    FourGuns.screenHeight / 2 + 40), FourGuns.GameState.MainMenu));

            base.buttons.Add(new Button("Exit Game",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Exit Game").X / 2,
                    FourGuns.screenHeight / 2 + 80), FourGuns.GameState.Exit));
        }
    }
}