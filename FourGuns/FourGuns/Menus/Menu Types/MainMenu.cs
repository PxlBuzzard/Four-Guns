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
    /// Shows on startup and when applicable.
    /// </summary>
    public class MainMenu : Menu
    {
        public bool addedContinueButton;

        public MainMenu(ContentManager theContentManager)
        {
            showHeaderImage = false;
            base.LoadContent(theContentManager);
            stateToLoadOnEscape = FourGuns.GameState.MainMenu;

            base.buttons.Add(new Button("Start New Game",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Start New Game").X / 2,
                FourGuns.screenHeight - 140),
                FourGuns.GameState.MissionMenu));
            buttons[0].isActive = true;

            base.buttons.Add(new Button("How to Play",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("How to Play").X / 2,
                FourGuns.screenHeight - 110),
                FourGuns.GameState.HelpMenu));

            base.buttons.Add(new Button("Exit",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Exit").X / 2,
                FourGuns.screenHeight - 80),
                FourGuns.GameState.Exit));

            /*
            base.buttons.Add(new Button("Fill out a feedback survey!",
                new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Fill out a feedback survey!").X / 2,
                FourGuns.screenHeight / 2 + 120),
                FourGuns.GameState.MainMenu, "loadsurveypage"));
             */
        }

        /// <summary>
        /// Checks for easter egg unlock.
        /// </summary>
        /// <param name="player">menu controller</param>
        public void Update(Player player, ContentManager theContentManager)
        {
            base.Update(player);

            //Cheat code! Unlock a butt gun.
            if (player.usingGamePad)
            {
                if (player.currentGamepadState.Buttons.LeftStick == ButtonState.Pressed)
                {
                    player.currentWeapons[2] = (new Gun(theContentManager, "Butt Gun", 100, Color.White,
                        "Sounds/Weapons/Guns/Pistol", Gun.GunWeight.Heavy, true, 1000,
                        2, 0, "Sprites/Gun/Bullet/buttBullet"));

                    textBlocks.Add(new Text("Butt Gun unlocked!",
                        new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Butt Gun unlocked!").X / 2,
                            FourGuns.screenHeight / 2 - 80)));
                }
            }
            else
            {
                if (player.currentKeyboardState.IsKeyDown(Keys.P))
                {
                    player.currentWeapons[2] = (new Gun(theContentManager, "Butt Gun", 100, Color.White,
                        "Sounds/Weapons/Guns/Pistol", Gun.GunWeight.Heavy, true, 1000,
                        2, 0, "Sprites/Gun/Bullet/buttBullet"));

                    textBlocks.Add(new Text("Butt Gun unlocked!",
                        new Vector2(FourGuns.screenWidth / 2 - FourGuns.font.MeasureString("Butt Gun unlocked!").X / 2,
                            FourGuns.screenHeight / 2 - 80)));
                }
            }
        }
    }
}
