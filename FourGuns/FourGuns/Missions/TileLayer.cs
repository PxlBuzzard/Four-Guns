#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace FourGuns
{
    /// <summary>
    /// TileLayer class
    /// </summary>
    public class TileLayer
    {
        public static string[] tileTextures;
        /// <summary>
        /// width of a tile
        /// </summary>
        static int tileWidth = 64;
        /// <summary>
        /// height of a tile
        /// </summary>
        static int tileHeight = 64;

        /// <summary>
        /// property of tileWidth
        /// </summary>
        public static int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = (int)MathHelper.Clamp(value, 20f, 100f); }

        }

        /// <summary>
        /// property of tileHeight
        /// </summary>
        public static int TileHeight
        {
            get { return tileHeight; }
            set { tileHeight = (int)MathHelper.Clamp(value, 20f, 100f); }

        }

        /// <summary>
        /// list of tile textures
        /// </summary>
        List<Texture2D> tiles = new List<Texture2D>();

        /// <summary>
        /// two dimention array for the map
        /// </summary>
        int[,] map;
        /// <summary>
        /// float variable for the alpha layer
        /// </summary>
        float alpha = 1f;

        /// <summary>
        /// property for alpha
        /// </summary>
        public float Alpha
        {
            get { return alpha; }
            set
            {
                alpha = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        /// <summary>
        /// property which returns tiletextures
        /// </summary>
        public string[] TileTextures
        {
            get { return tileTextures; }
          
        }
        /// <summary>
        /// property which return the width in pixels for the map
        /// </summary>
        public int WidthInPixels
        {
            get { return map.GetLength(1) * tileWidth; }
        }
        /// <summary>
        /// property which return the height in pixels for the map
        /// </summary>
        public int HeightInPixels
        {
            get { return map.GetLength(0) * tileHeight; }
        }

        /// <summary>
        /// property which returns map array
        /// </summary>
        public int[,] Map { get { return map; } }

        /// <summary>
        /// contructor for TileLayer
        /// </summary>
        /// <param name="width">int width value</param>
        /// <param name="height">int height value</param>
        public TileLayer(int width, int height)
        {
            map = new int[height, width];
        }

        /// <summary>
        /// contructor for TileLayer
        /// </summary>
        /// <param name="existingMap">map 2D array</param>
        public TileLayer(int[,] existingMap)
        {
            map = (int[,])existingMap.Clone();
        }
        /// <summary>
        /// property which returns the length of the height of the array
        /// </summary>
        public int Height
        {
            get { return map.GetLength(0); }
        }
        /// <summary>
        /// property which returns the length of the width of the array
        /// </summary>
        public int Width
        {
            get { return map.GetLength(1); }
        }

        /// <summary>
        /// Fromfile method which was taken out of XNA 4.0
        /// </summary>
        /// <param name="content">content</param>
        /// <param name="filename">tilename</param>
        /// <returns>TileLayer</returns>
        public static TileLayer FromFile(ContentManager content, string filename)
        {
            TileLayer tileLayer;
            bool readingTextures = false;
            bool readingLayout = false;
            List<String> textureNames = new List<string>();
            List<List<int>> tempLayout = new List<List<int>>();

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();

                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    if (line.Contains("[Textures]"))
                    {
                        readingTextures = true;
                        readingLayout = false;
                    }
                    else if (line.Contains("[Layout]"))
                    {
                        readingLayout = true;
                        readingTextures = false;
                    }
                    else if (readingTextures)
                    {
                        textureNames.Add(line);
                    }
                    else if (readingLayout)
                    {
                        List<int> row = new List<int>();

                        string[] cells = line.Split(' ');
                        foreach (string c in cells)
                        {
                            if (!string.IsNullOrEmpty(c))
                            {
                                row.Add(int.Parse(c));
                            }
                        }

                        tempLayout.Add(row);
                    }
                }
            }

            int width = tempLayout[0].Count;
            int height = tempLayout.Count;

            tileLayer = new TileLayer(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tileLayer.SetCellIndex(x, y, tempLayout[y][x]);
                }
            }

            tileTextures = new string[textureNames.count];
            tileTextures = textureNames.ToArray();

            tileLayer.LoadTileTextures(content, textureNames.ToArray());

            return tileLayer;
        }

        /// <summary>
        /// method to handle loading txt files
        /// </summary>
        /// <param name="content">content</param>
        /// <param name="textureNames"> array of strings for the texture names</param>
        public void LoadTileTextures(ContentManager content, params string[] textureNames)
        {
            Texture2D texture;

            foreach (string textureName in textureNames)
            {
                texture = content.Load<Texture2D>(textureName);
                tiles.Add(texture);
            }
        }

        public void AddTexture(Texture2D texture)
        {
            tiles.Add(texture);
        }

        /// <summary>
        /// adds index to each cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cellIndex"></param>
        public void SetCellIndex(int x, int y, int cellIndex)
        {
            map[y, x] = cellIndex;
        }

        /// <summary>
        /// draw method 
        /// </summary>
        /// <param name="batch">spritebatch</param>
        /// <param name="camera">camera object</param>
        public void Draw(SpriteBatch batch)
        {
            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            for (int x = 0; x < tileMapWidth; x++)
            {
                for (int y = 0; y < tileMapHeight; y++)
                {
                    int textureIndex = map[y, x];

                    if (textureIndex == -1)
                    {
                        continue;
                    }

                    Texture2D texture = tiles[textureIndex];

                    batch.Draw(texture, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight),
                         new Color(new Vector4(1f,1f,1f,Alpha)));
                }
            }
        }

        /// <summary>
        /// draw method for collidables
        /// </summary>
        /// <param name="batch">spritebatch</param>
        public void DrawCollidables(SpriteBatch batch)
        {
            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            for (int x = 0; x < tileMapWidth; x++)
            {
                for (int y = 0; y < tileMapHeight; y++)
                {
                    int textureIndex = map[y, x];

                    if (textureIndex == -1)
                    {
                        continue;
                    }

                    Texture2D texture = tiles[textureIndex];
                    if (y != 0)
                    {
                        float z = (1f / ((float)y * (float)tileHeight));
                        batch.Draw(texture, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), null,
                             new Color(new Vector4(1f, 1f, 1f, Alpha)), 0f, Vector2.Zero, SpriteEffects.None, z);
                    }
                }
            }
        }
    }
}
