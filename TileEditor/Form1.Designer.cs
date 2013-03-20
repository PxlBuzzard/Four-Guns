namespace TileEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTileMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contentPathTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.drawRadioButton1 = new System.Windows.Forms.RadioButton();
            this.eraseRadioButton = new System.Windows.Forms.RadioButton();
            this.layerListBox = new System.Windows.Forms.ListBox();
            this.addLayerButton = new System.Windows.Forms.Button();
            this.removeLayerButton = new System.Windows.Forms.Button();
            this.removeTextureButton = new System.Windows.Forms.Button();
            this.addTextureButton = new System.Windows.Forms.Button();
            this.textureListBox = new System.Windows.Forms.ListBox();
            this.texturePreviewBox = new System.Windows.Forms.PictureBox();
            this.tileDisplay1 = new TileEditor.TileDisplay();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.texturePreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.Location = new System.Drawing.Point(712, 27);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(19, 619);
            this.vScrollBar1.TabIndex = 1;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.Location = new System.Drawing.Point(12, 647);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(700, 20);
            this.hScrollBar1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1059, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTileMapToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newTileMapToolStripMenuItem
            // 
            this.newTileMapToolStripMenuItem.Name = "newTileMapToolStripMenuItem";
            this.newTileMapToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.newTileMapToolStripMenuItem.Text = "New Tile Map";
            this.newTileMapToolStripMenuItem.Click += new System.EventHandler(this.newTileMapToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contentPathTextBox
            // 
            this.contentPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.contentPathTextBox.Location = new System.Drawing.Point(756, 36);
            this.contentPathTextBox.Name = "contentPathTextBox";
            this.contentPathTextBox.ReadOnly = true;
            this.contentPathTextBox.Size = new System.Drawing.Size(279, 20);
            this.contentPathTextBox.TabIndex = 4;
            // 
            // drawRadioButton1
            // 
            this.drawRadioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawRadioButton1.AutoSize = true;
            this.drawRadioButton1.Checked = true;
            this.drawRadioButton1.Location = new System.Drawing.Point(874, 63);
            this.drawRadioButton1.Name = "drawRadioButton1";
            this.drawRadioButton1.Size = new System.Drawing.Size(50, 17);
            this.drawRadioButton1.TabIndex = 6;
            this.drawRadioButton1.TabStop = true;
            this.drawRadioButton1.Text = "Draw";
            this.drawRadioButton1.UseVisualStyleBackColor = true;
            // 
            // eraseRadioButton
            // 
            this.eraseRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eraseRadioButton.AutoSize = true;
            this.eraseRadioButton.Location = new System.Drawing.Point(872, 86);
            this.eraseRadioButton.Name = "eraseRadioButton";
            this.eraseRadioButton.Size = new System.Drawing.Size(52, 17);
            this.eraseRadioButton.TabIndex = 7;
            this.eraseRadioButton.Text = "Erase";
            this.eraseRadioButton.UseVisualStyleBackColor = true;
            // 
            // layerListBox
            // 
            this.layerListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.layerListBox.FormattingEnabled = true;
            this.layerListBox.Location = new System.Drawing.Point(756, 109);
            this.layerListBox.Name = "layerListBox";
            this.layerListBox.Size = new System.Drawing.Size(279, 108);
            this.layerListBox.TabIndex = 8;
            this.layerListBox.SelectedIndexChanged += new System.EventHandler(this.layerListBox_SelectedIndexChanged);
            // 
            // addLayerButton
            // 
            this.addLayerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addLayerButton.Location = new System.Drawing.Point(817, 223);
            this.addLayerButton.Name = "addLayerButton";
            this.addLayerButton.Size = new System.Drawing.Size(75, 23);
            this.addLayerButton.TabIndex = 9;
            this.addLayerButton.Text = "Add";
            this.addLayerButton.UseVisualStyleBackColor = true;
            this.addLayerButton.Click += new System.EventHandler(this.addLayerButton_Click);
            // 
            // removeLayerButton
            // 
            this.removeLayerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeLayerButton.Location = new System.Drawing.Point(898, 223);
            this.removeLayerButton.Name = "removeLayerButton";
            this.removeLayerButton.Size = new System.Drawing.Size(75, 23);
            this.removeLayerButton.TabIndex = 10;
            this.removeLayerButton.Text = "Remove";
            this.removeLayerButton.UseVisualStyleBackColor = true;
            this.removeLayerButton.Click += new System.EventHandler(this.removeLayerButton_Click);
            // 
            // removeTextureButton
            // 
            this.removeTextureButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeTextureButton.Location = new System.Drawing.Point(898, 379);
            this.removeTextureButton.Name = "removeTextureButton";
            this.removeTextureButton.Size = new System.Drawing.Size(75, 23);
            this.removeTextureButton.TabIndex = 13;
            this.removeTextureButton.Text = "Remove";
            this.removeTextureButton.UseVisualStyleBackColor = true;
            this.removeTextureButton.Click += new System.EventHandler(this.removeTextureButton_Click);
            // 
            // addTextureButton
            // 
            this.addTextureButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addTextureButton.Location = new System.Drawing.Point(817, 379);
            this.addTextureButton.Name = "addTextureButton";
            this.addTextureButton.Size = new System.Drawing.Size(75, 23);
            this.addTextureButton.TabIndex = 12;
            this.addTextureButton.Text = "Add";
            this.addTextureButton.UseVisualStyleBackColor = true;
            this.addTextureButton.Click += new System.EventHandler(this.addTextureButton_Click);
            // 
            // textureListBox
            // 
            this.textureListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textureListBox.FormattingEnabled = true;
            this.textureListBox.Location = new System.Drawing.Point(756, 252);
            this.textureListBox.Name = "textureListBox";
            this.textureListBox.Size = new System.Drawing.Size(279, 121);
            this.textureListBox.TabIndex = 11;
            this.textureListBox.SelectedIndexChanged += new System.EventHandler(this.textureListBox_SelectedIndexChanged);
            // 
            // texturePreviewBox
            // 
            this.texturePreviewBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.texturePreviewBox.Location = new System.Drawing.Point(756, 408);
            this.texturePreviewBox.Name = "texturePreviewBox";
            this.texturePreviewBox.Size = new System.Drawing.Size(279, 239);
            this.texturePreviewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.texturePreviewBox.TabIndex = 14;
            this.texturePreviewBox.TabStop = false;
            // 
            // tileDisplay1
            // 
            this.tileDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tileDisplay1.Location = new System.Drawing.Point(12, 27);
            this.tileDisplay1.Name = "tileDisplay1";
            this.tileDisplay1.Size = new System.Drawing.Size(700, 620);
            this.tileDisplay1.TabIndex = 0;
            this.tileDisplay1.Text = "tileDisplay1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 679);
            this.Controls.Add(this.texturePreviewBox);
            this.Controls.Add(this.removeTextureButton);
            this.Controls.Add(this.addTextureButton);
            this.Controls.Add(this.textureListBox);
            this.Controls.Add(this.removeLayerButton);
            this.Controls.Add(this.addLayerButton);
            this.Controls.Add(this.layerListBox);
            this.Controls.Add(this.eraseRadioButton);
            this.Controls.Add(this.drawRadioButton1);
            this.Controls.Add(this.contentPathTextBox);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.tileDisplay1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.texturePreviewBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TileDisplay tileDisplay1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTileMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox contentPathTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RadioButton drawRadioButton1;
        private System.Windows.Forms.RadioButton eraseRadioButton;
        private System.Windows.Forms.ListBox layerListBox;
        private System.Windows.Forms.Button addLayerButton;
        private System.Windows.Forms.Button removeLayerButton;
        private System.Windows.Forms.Button removeTextureButton;
        private System.Windows.Forms.Button addTextureButton;
        private System.Windows.Forms.ListBox textureListBox;
        private System.Windows.Forms.PictureBox texturePreviewBox;
    }
}

