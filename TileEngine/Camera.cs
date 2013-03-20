using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TileEngine
{
    /// <summary>
    /// Camera Class
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// speed of camera
        /// </summary>
        float speed = 5;
        /// <summary>
        /// Vector2 position of camera
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        /// properties of speed
        /// </summary>
        public float Speed
        {
            get { return speed; }
            set { speed = (float)Math.Max(value, 1f); }
        }

        /// <summary>
        /// update method
        /// </summary>
        public void Update()
        {
            // controls movement of the camera using the keyboard and gamepad
            KeyboardState keyState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            Vector2 motion = Vector2.Zero;

            motion = new Vector2(gamePadState.ThumbSticks.Left.X, -gamePadState.ThumbSticks.Left.Y);

            if (keyState.IsKeyDown(Keys.Up))
            {
                motion.Y--;
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                motion.Y++;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                motion.X--;
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                motion.X++;
            }

            if (motion != Vector2.Zero)
            {
                //motion.Normalize();
                Position += motion * Speed;
            }

        }
 
    }
}
