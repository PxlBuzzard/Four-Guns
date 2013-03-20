#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

// partially based on code posted by David Amador
namespace FourGuns
{
    /// <summary>
    /// A camera that can give the spritebatch a matrix to modify the location sprites drawn by it are drawn at
    /// </summary>
    public class Camera2D
    {
        /// <summary>
        /// the move speed of the camera in pixels per milliseconds
        /// </summary>
        float moveSpeed;

        /// <summary>
        /// the move speed property of the camera in pixels per milliseconds
        /// </summary>
        public float MoveSpeed
        {
            get { return moveSpeed; }
        }

        /// <summary>
        /// the zoom of the camera
        /// </summary>
        float zoom;
        /// <summary>
        /// the zoom property of the camera
        /// </summary>
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < 0.1f) zoom = 0.1f;
            } // (Negative zoom would flip image)
        }
        Matrix transform;
        /// <summary>
        /// the position of the camera
        /// </summary>
        Vector2 position;
        /// <summary>
        /// the position property of the camera
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// the position the camera wants to reach
        /// </summary>
        Vector2 destination;
        /// <summary>
        /// the property for the position the camera wants to reach
        /// </summary>
        public Vector2 Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        /// <summary>
        /// The top left corner of the camera's view
        /// </summary>
        public Vector2 TopLeft
        { 
            get 
            { 
                Vector2 temp = position;
                temp.X -= FourGuns.screenWidth / 2;
                temp.Y -= FourGuns.screenHeight / 2;
                return temp;
            }
        }

        /// <summary>
        /// The bottom left corner of the camera's view
        /// </summary>
        public Vector2 BottomRight
        {
            get
            {
                Vector2 temp = position;
                temp.X += FourGuns.screenWidth / 2;
                temp.Y += FourGuns.screenHeight / 2;
                return temp;
            }
        }

        /// <summary>
        /// the rotation of the camera
        /// </summary>
        protected float rotation;
        /// <summary>
        /// the rotation property of the camera
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private Vector3 translation;
        private Vector3 transViewport;
        private Vector3 zoomVector;

        /// <summary>
        /// Creates a 2D camera at the zero vector
        /// </summary>
        public Camera2D()
        {
            zoom = 1.0f;
            position = Vector2.Zero;
            rotation = 0.0f;
            moveSpeed = 0.5f;
            destination = position;
            translation = new Vector3();
            transViewport = new Vector3();
            zoomVector = new Vector3(0, 0, 1);
        }

        /// <summary>
        /// Creates a 2D camera at a position
        /// </summary>
        /// <param name="x">the x component of the camera's starting location</param>
        /// <param name="y">the y component of the camera's starting location</param>
        public Camera2D(int x, int y)
            : this()
        {
            position = new Vector2(x, y);
        }

        /// <summary>
        /// Sets the camera location to the average position of a list of GameObjects
        /// </summary>
        /// <param name="anchors">The list of GameObjects that an average position is derived from</param>
        public void CenterCameraOn(List<GameObject> anchors)
        {
            // creates a temporary vector2 for the camera's new position
            Vector2 averagePosition = new Vector2();
            // a counter for the number of anchors that are being averaged from
            int count = 0;

            // for each anchor, add its position to the average position
            foreach (GameObject anchor in anchors)
            {
                // if the anchor is a player, make sure they are active before using them in the average
                if (anchor as Player != null)
                {
                    if (!((Player)anchor).ActivePlayer)
                        break;
                }
                averagePosition += anchor.Position;
                count++;
            }
            
            // divide the average position by the number of things being averaged and set the cameras desired position (aka destination) from it
            this.destination = averagePosition / count;
        }

        /// <summary>
        /// Gets the transformation matrix that modifies the draw positions of things based on the camera position
        /// </summary>
        /// <param name="graphics">The graphics device that is in use</param>
        /// <returns>Returns the transformation matrix</returns>
        public Matrix GetTransformation(GraphicsDevice graphics)
        {
            translation.X = -position.X;
            translation.Y = -position.Y;

            transViewport.X = graphics.Viewport.Width * .5f;
            transViewport.Y = graphics.Viewport.Height * .5f;

            zoomVector.X = zoom;
            zoomVector.Y = zoom;
            // creates a matrix that takes into account camera position, rotation, zoom, and the distance of the zero vector from the center of the viewport
            transform = Matrix.CreateTranslation(translation) * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateScale(zoomVector) * Matrix.CreateTranslation(transViewport);
            return transform;
        }

        /// <summary>
        /// Update method for camera
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // sets the direction for move and normalizes it if it is not a zero vector
            Vector2 move = destination - position;
            if (!move.Equals(Vector2.Zero))
                move.Normalize();

            // gets the correct distance to move the camera based on speed and elapsed gametime
            move = move * moveSpeed * gameTime.ElapsedGameTime.Milliseconds;

            // if the move distance will overshoot the destination, position equals the destination
            if (move.Length() >= (destination - position).Length())
            {
                position = destination;
            }
            // otherwise, the move is added to the position
            else
            {
                position += move;
            }

        }
    }
}
