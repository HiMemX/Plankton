using CSHO;
using HoArchive;
using libWiiSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plankton.Special_Editors
{
    

    public partial class TextureEditor : Form
    {
        InterpolatingPictureBox textureViewer;

        TOCEntry currTexture = null;
        Bitmap colormap;
        Bitmap alphamap;
        Bitmap combinedmap;
        string name;

        OpenFileDialog openDialog;
        SaveFileDialog saveDialog;

        TextureEditorImportPrompt prompt;

        public TextureEditor()
        {
            openDialog = new OpenFileDialog();
            saveDialog = new SaveFileDialog();
            prompt = null;

            saveDialog.DefaultExt = ".png";

            InitializeComponent();

            textureViewer = new InterpolatingPictureBox();
            textureViewer.Dock = DockStyle.Fill;
            textureViewer.SizeMode = PictureBoxSizeMode.Zoom;
            textureViewer.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            panel1.Controls.Add(textureViewer);

            imageModeComboBox.SelectedIndex = 0;
            imageModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;


        }

        public void Update(Handler handler, TreeNode node)
        {
            if (!updateCheckBox.Checked) { return; }

            string platform = handler.Archive.Header.platform;
            string target = handler.Archive.Header.target;

            if (platform != "WII" || target != "SB09" || !(node is assetTreeNode)) { return; }

            TOCEntry entry = ((assetTreeNode)node).asset;
            if (entry.wmlTypeID == wmlTypeID.Texture)
            {
                TOCEntry rawblob = handler.GetAsset(((SB09WiiAsset.Texture)entry.entity).imageBlobID);

                if (rawblob == null) { return; }

                currTexture = rawblob;
            }
            else if(entry.wmlTypeID == wmlTypeID.RawBlob)
            {
                if(!entry.data.Skip(0x20).Take(0x04).SequenceEqual(new byte[] { 0x00, 0x20, 0xAF, 0x30 })) { return; }

                currTexture = entry;
            }
            else { return; }

            name = handler.GetName(currTexture.uidSelf);
            Text = "Texture Editor - " + name + " [" + currTexture.uidSelf.ToString("X16") + "]";

            LoadTexture(currTexture.data.ToArray());
        }

        public void LoadTexture(byte[] rawblob) // This code is straight from The GoodEditor 2 lmao tyvm Penney
        {
            List<Bitmap> maps = SB09WiiTPL.BitmapsFromRawblob(rawblob);
            colormap = maps[0];
            alphamap = maps[1];
            combinedmap = maps[2];

            DisplayImage();
        }

        private void TextureEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void DisplayImage()
        {
            switch (imageModeComboBox.SelectedIndex)
            {
                case 0:
                    textureViewer.Image = combinedmap;
                    break;
                case 1:
                    textureViewer.Image = colormap;
                    break;
                case 2:
                    textureViewer.Image = alphamap;
                    break;
            }
        }

        private void imageModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayImage();
        }


        private void exportButton_Click(object sender, EventArgs e)
        {
            if (currTexture == null) { return; }

            saveDialog.FileName = name + " [" + currTexture.uidSelf.ToString("X16") + "]";
            if (saveDialog.ShowDialog() != DialogResult.OK) { return; }
            string filepath = saveDialog.FileName;

            textureViewer.Image.Save(filepath);
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            if (currTexture == null) { return; }

            if (openDialog.ShowDialog() != DialogResult.OK) { return; }

            string filepath = openDialog.FileName;

            prompt = new TextureEditorImportPrompt();
            if(prompt.ShowDialog() != DialogResult.OK) { return; }

            combinedmap = new Bitmap(Image.FromFile(filepath));
            colormap = new Bitmap(combinedmap);
            alphamap = new Bitmap(combinedmap);


            Color oldcolor;
            bool alphaused = false;
            for (int y = 0; y < alphamap.Height; y++)
            {
                for (int x = 0; x < alphamap.Width; x++)
                {
                    oldcolor = colormap.GetPixel(x, y);
                    colormap.SetPixel(x, y, Color.FromArgb(255, oldcolor.R, oldcolor.G, oldcolor.B));
                    alphamap.SetPixel(x, y, Color.FromArgb(255, oldcolor.A, oldcolor.A, oldcolor.A));
                    if(oldcolor.A != 255) { alphaused = true; }
                }
            }

            currTexture.data = SB09WiiTPL.RawblobFromBitmaps(colormap, alphamap, alphaused,
                (uint)prompt.wrapSComboBox.SelectedIndex,
                (uint)prompt.wrapTComboBox.SelectedIndex,
                (uint)prompt.minFilterComboBox.SelectedIndex,
                (uint)prompt.magFilterComboBox.SelectedIndex
            ).ToList();//header.ToArray().Concat(tpl.ToByteArray()).ToList();

            LoadTexture(currTexture.data.ToArray());
        }

        private void rotateCCButton_Click(object sender, EventArgs e)
        {
            if (currTexture == null) { return; }
            textureViewer.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            textureViewer.Refresh();
        }

        private void rotateCButton_Click(object sender, EventArgs e)
        {
            if (currTexture == null) { return; }
            textureViewer.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            textureViewer.Refresh();
        }

        private void mirrorHButton_Click(object sender, EventArgs e)
        {
            if (currTexture == null) { return; }
            textureViewer.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            textureViewer.Refresh();
        }

        private void mirrorVButton_Click(object sender, EventArgs e)
        {
            if (currTexture == null) { return; }
            textureViewer.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            textureViewer.Refresh();
        }

    }
    public class InterpolatingPictureBox : PictureBox // https://stackoverflow.com/questions/35795032/how-to-show-an-image-in-picturebox-if-the-picture-can-be-from-10x10-to-500x500
    {
        public System.Drawing.Drawing2D.InterpolationMode InterpolationMode { get; set; }

        protected override void OnPaint(PaintEventArgs eventArgs)
        {
            eventArgs.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(eventArgs);
        }
    }
}
