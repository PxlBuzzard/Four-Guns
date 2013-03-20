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
    /// Creates non-interactable text for a menu.
    /// </summary>
    public class Text : Button
    {
        /// <summary>
        /// Create a new line of text.
        /// </summary>
        /// <param name="theText">string of text</param>
        /// <param name="thePosition">position onscreen</param>
        public Text(string theText, Vector2 thePosition) : base(theText, thePosition, FourGuns.GameState.MainMenu)
        {
            Position = thePosition;
            textPosition = new Vector2(Position.X + 5, Position.Y + 5);
            Text = theText;
            isActive = false;
        }

        /// <summary>
        /// Draw the button to the screen.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FourGuns.font, Text, textPosition, Color.White);
        }
    }
}
