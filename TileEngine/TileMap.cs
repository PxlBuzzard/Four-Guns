using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileEngine
{
    public class TileMap
    {
        /// <summary>
        /// List of tilelayers
        /// </summary>
        public List<TileLayer> Layers = new List<TileLayer>();

        public void Draw(SpriteBatch spritebatch, Camera camera)
        {
            foreach (TileLayer layer in Layers)
            {
                layer.Draw(spritebatch, camera);
            }
        }
    }
}
