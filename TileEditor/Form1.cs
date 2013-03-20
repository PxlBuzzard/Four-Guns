using System;
//using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using TileEngine;

namespace TileEditor
{
    using Image = System.Drawing.Image;

    /// <summary>
    /// Code for Form1
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// new array of strings 
        /// </summary>
        string[] imageExtentions = new string[]
        {
            ".jpg", ".png", ".tga"
        };

        /// <summary>
        /// max width of the form 
        /// </summary>
        int maxWidth = 0;

        /// <summary>
        /// max Height of the form
        /// </summary>
        int maxHeight = 0;

        /// <summary>
        /// Spritebatch object to draw
        /// </summary>
        SpriteBatch spriteBatch;

        /// <summary>
        /// new texture2D which holds the tile texture
        /// </summary>
        Texture2D tileTexture;

        /// <summary>
        /// new camera object
        /// </summary>
        Camera camera = new Camera();

        /// <summary>
        /// current layer in use
        /// </summary>
        TileLayer currentLayer;
        /// <summary>
        ///  the x and y values of each individual cell
        /// </summary>
        int cellX, cellY;

        /// <summary>
        /// new tileMap 
        /// </summary>
        TileMap tileMap = new TileMap();


        /// <summary>
        /// dictionary of tilelayers
        /// </summary>
        Dictionary<string, TileLayer> tileLayerDict = new Dictionary<string, TileLayer>();
        /// <summary>
        /// dictionary of textures
        /// </summary>
        Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
        /// <summary>
        /// dictionary of preview images
        /// </summary>
        Dictionary<string, Image> previewDict = new Dictionary<string, Image>();
        /// <summary>
        /// dictionary of layers
        /// </summary>
        Dictionary<string, TileLayer> layerDict = new Dictionary<string, TileLayer>();

        /// <summary>
        /// property for graphicsdevice
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return tileDisplay1.GraphicsDevice; }
        }

        /// <summary>
        /// constructor for Form1
        /// </summary>
        public Form1()
        {

            InitializeComponent();

            tileDisplay1.OnDraw += new EventHandler(tileDisplay1_OnDraw);
            tileDisplay1.OnInitialize += new EventHandler(tileDisplay1_OnInitialize);

            Application.Idle += delegate { tileDisplay1.Invalidate(); };


            saveFileDialog1.Filter = "Layer File|*.Layer";

            Mouse.WindowHandle = tileDisplay1.Handle;

            while (string.IsNullOrEmpty(contentPathTextBox.Text))
            {
                //if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                //{
                contentPathTextBox.Text = @"..\..\..\FourGuns\FourGunsContent"; // folderBrowserDialog1.SelectedPath;
                //}
            }
        }

        /// <summary>
        /// method which runs when tiledisplay runs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tileDisplay1_OnInitialize(object sender, EventArgs e)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            string fourGunsPath = @"..\..\..\FourGuns\FourGunsContent\Tiles\";

            tileTexture = Texture2D.FromStream(GraphicsDevice, new FileStream(fourGunsPath + "Default.png", FileMode.Open));

        }

        /// <summary>
        /// Draw event for tiledisplay1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tileDisplay1_OnDraw(object sender, EventArgs e)
        {
            Logic();
            Render();


        }

        /// <summary>
        /// method which controls drawing the map in the XNA window
        /// </summary>
        private void Render()
        {
            GraphicsDevice.Clear(Color.Black);

            foreach (TileLayer layer in tileMap.Layers)
            {
                layer.Draw(spriteBatch, camera);

                tileMap.Draw(spriteBatch, camera);

                spriteBatch.Begin();
                for (int y = 0; y < layer.Height; y++)
                {
                    for (int x = 0; x < layer.Width; x++)
                    {
                        if (layer.GetCellIndex(x, y) == -1)
                        {
                            spriteBatch.Draw(
                            tileTexture,
                            new Rectangle(x * TileLayer.TileWidth - (int)camera.Position.X, y * TileLayer.TileHeight - (int)camera.Position.Y, TileLayer.TileWidth, TileLayer.TileHeight),
                            Color.White);
                        }

                    }
                }
                spriteBatch.End();
            }


            if (currentLayer != null)
            {

                if (cellX != -1 && cellY != -1)
                {

                    spriteBatch.Begin();

                    spriteBatch.Draw(
                            tileTexture,
                            new Rectangle(cellX * TileLayer.TileWidth - (int)camera.Position.X, cellY * TileLayer.TileHeight - (int)camera.Position.Y, TileLayer.TileWidth, TileLayer.TileHeight),
                            Color.Red);

                    spriteBatch.End();

                }
            }


        }

        /// <summary>
        /// contorls scrolling
        /// </summary>
        private void Logic()
        {
            camera.Position.X = hScrollBar1.Value * TileLayer.TileWidth;
            camera.Position.Y = vScrollBar1.Value * TileLayer.TileHeight;

            int mx = Mouse.GetState().X;
            int my = Mouse.GetState().Y;


            if (currentLayer != null)
            {

                if (mx >= 0 && mx < tileDisplay1.Width && my >= 0 && my < tileDisplay1.Height)
                {
                    cellX = mx / TileLayer.TileWidth;
                    cellY = my / TileLayer.TileHeight;

                    cellX += hScrollBar1.Value;
                    cellY += vScrollBar1.Value;

                    cellX = (int)MathHelper.Clamp(cellX, 0, currentLayer.Width - 1);
                    cellY = (int)MathHelper.Clamp(cellY, 0, currentLayer.Height - 1);

                    if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        if (drawRadioButton1.Checked && textureListBox.SelectedItem != null)
                        {
                            Texture2D texture = textureDict[textureListBox.SelectedItem as string];

                            int index = currentLayer.IsUsingTexture(texture);

                            if (index == -1)
                            {
                                currentLayer.AddTexture(texture);
                                index = currentLayer.IsUsingTexture(texture);
                            }

                            currentLayer.SetCellIndex(cellX, cellY, index);
                        }
                        else if (eraseRadioButton.Checked)
                        {
                            currentLayer.SetCellIndex(cellX, cellY, -1);
                        }
                    }
                }
                else
                {
                    cellX = cellY = -1;
                }


            }


        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// File>Open Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Layer File|*.Layer";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;

                string[] textureNames;

                TileLayer layer = TileLayer.FromFile(filename, out textureNames);

                AdjustScrollBars(layer);

                layerDict.Add(Path.GetFileName(filename), layer);
                tileMap.Layers.Add(layer);
                layerListBox.Items.Add(Path.GetFileName(filename));

                foreach (string textureName in textureNames)
                {
                    //if (textureDict.Contains(textureName))
                    //{
                    //    layer.AddTexture(textureDict[textureName]);
                    //    continue;
                    //}

                    string fullPath = contentPathTextBox.Text + "/" + textureName;

                    foreach (string ext in imageExtentions)
                    {
                        if (File.Exists(fullPath + ext))
                        {
                            fullPath += ext;
                            break;
                        }
                    }



                    Texture2D tex = Texture2D.FromStream(GraphicsDevice, new StreamReader(fullPath).BaseStream);
                    Image image = Image.FromFile(fullPath);

                    textureDict.Add(textureName, tex);
                    previewDict.Add(textureName, image);

                    textureListBox.Items.Add(textureName);

                    layer.AddTexture(tex);
                }
            }
        }

        /// <summary>
        /// method to adjust scroll bars
        /// </summary>
        /// <param name="layer"></param>
        private void AdjustScrollBars(TileLayer layer)
        {
            if (layer.WidthInPixels > tileDisplay1.Width)
            {
                maxWidth = (int)Math.Max(layer.Width, maxWidth);
                hScrollBar1.Visible = true;
                hScrollBar1.Minimum = 0;
                hScrollBar1.Maximum = maxWidth;

            }

            if (layer.HeightInPixels > tileDisplay1.Height)
            {
                maxHeight = (int)Math.Max(layer.Height, maxHeight);
                vScrollBar1.Visible = true;
                vScrollBar1.Minimum = 0;
                vScrollBar1.Maximum = maxHeight;

            }
        }

        /// <summary>
        /// File>Save Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (layerListBox.SelectedItem != null)
            {
                string filename = layerListBox.SelectedItem as string;
                saveFileDialog1.FileName = filename;

                TileLayer tileLayer = layerDict[filename];

                Dictionary<int, string> utilizedTextures = new Dictionary<int, string>();

                foreach (string textureName in textureListBox.Items)
                {
                    int index = tileLayer.IsUsingTexture(textureDict[textureName]);

                    if (index != -1)
                    {
                        utilizedTextures.Add(index, textureName);
                    }
                }

                List<string> utilizedTextureList = new List<string>();

                for (int k = 0; k < utilizedTextures.Count; k++)
                {
                    utilizedTextureList.Add(utilizedTextures[k]);
                }

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    tileLayer.Save(saveFileDialog1.FileName, utilizedTextureList.ToArray());
                }
            }

        }

        /// <summary>
        /// File>Exit Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Event for selected texture in textureListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textureListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textureListBox.SelectedItem != null)
            {
                texturePreviewBox.Image = previewDict[textureListBox.SelectedItem as string];
            }
        }

        /// <summary>
        /// Event for selected layer in layerListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (layerListBox.SelectedItem != null)
            {
                currentLayer = layerDict[layerListBox.SelectedItem as string];
            }
        }

        /// <summary>
        /// Event for addLayerButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addLayerButton_Click(object sender, EventArgs e)
        {
            NewLayerForm form = new NewLayerForm();

            form.ShowDialog();

            if (form.OKPressed)
            {
                TileLayer tilelayer = new TileLayer(
                int.Parse(form.width.Text),
                int.Parse(form.height.Text));

                layerDict.Add(form.name.Text, tilelayer);
                tileMap.Layers.Add(tilelayer);
                layerListBox.Items.Add(form.name.Text);

                AdjustScrollBars(tilelayer);


            }
        }

        /// <summary>
        /// Event for removeLayerButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeLayerButton_Click(object sender, EventArgs e)
        {
            if (currentLayer != null)
            {
                string filename = layerListBox.SelectedItem as string;

                tileMap.Layers.Remove(currentLayer);
                layerDict.Remove(filename);
                layerListBox.Items.Remove(layerListBox.SelectedItem);

                currentLayer = null;

            }
        }

        /// <summary>
        /// Event for addTextureButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTextureButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG Image|*.jpg|PNG Image|*.png|TGA Image|*.tga";
            openFileDialog1.InitialDirectory = contentPathTextBox.Text;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;

                Texture2D texture = Texture2D.FromStream(GraphicsDevice, new StreamReader(filename).BaseStream);
                Image image = Image.FromFile(filename);

                filename = filename.Replace(contentPathTextBox.Text + "\\", "");
                filename = filename.Remove(filename.LastIndexOf("."));

                textureListBox.Items.Add(filename);
                textureDict.Add(filename, texture);
                previewDict.Add(filename, image);

            }

        }

        /// <summary>
        /// placeholders otherwise form breaks to be implimented
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeTextureButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// placeholders otherwise form breaks to be implimented
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newTileMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
