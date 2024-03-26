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
            TPL tpl = TPL.Load(rawblob.Skip(0x20).ToArray());

            colormap = new Bitmap(tpl.ExtractTexture(0));
            combinedmap = new Bitmap(tpl.ExtractTexture(0));
            alphamap = new Bitmap(colormap.Width, colormap.Height);

            if (tpl.NumOfTextures == 2) { // Second channel is used for alpha transparency
                Color backgroundcolor = textureViewer.BackColor;
                alphamap = new Bitmap(tpl.ExtractTexture(1));

                Color oldcolor;
                int alpha;
                Color newcolor;
                for (int y = 0; y < alphamap.Height; y++)
                {
                    for (int x = 0; x < alphamap.Width; x++)
                    {
                        oldcolor = colormap.GetPixel(x, y);
                        alpha = alphamap.GetPixel(x, y).R; // Just one channel
                        newcolor = Color.FromArgb(alpha, oldcolor.R, oldcolor.G, oldcolor.B);
                        combinedmap.SetPixel(x, y, newcolor);
                    }
                }
            }

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

            int maxsize = Math.Max(combinedmap.Height, combinedmap.Width);
            if (prompt.imageScaleCheckbox.Checked && maxsize > 512)
            {
                //Size size = new Size(Math.Min(512, (int)Math.Pow(2, Math.Ceiling(Math.Log(combinedmap.Width) / Math.Log(2)))), Math.Min(512, (int)Math.Pow(2, Math.Ceiling(Math.Log(combinedmap.Height) / Math.Log(2)))));
                
                Size size = new Size((int)((float)combinedmap.Width / (float)maxsize * 512f), (int)((float)combinedmap.Height / (float)maxsize * 512f));
                combinedmap = new Bitmap(combinedmap, size);
                colormap = new Bitmap(colormap, size);
                alphamap = new Bitmap(alphamap, size);
            }

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

            DisplayImage();


            TPL tpl = new TPL();
            List<Image> images = new() { colormap };
            List<TPL_TextureFormat> formats = new() { TPL_TextureFormat.RGB565 };
            List<TPL_PaletteFormat> palettes = new() { TPL_PaletteFormat.RGB565 };
            if (alphaused)
            {
                images.Add(alphamap);
                formats.Add(TPL_TextureFormat.I4);
                palettes.Add(TPL_PaletteFormat.IA8);
            }
           // MessageBox.Show(alphaused.ToString());
            tpl.CreateFromImages(images.ToArray(), formats.ToArray(), palettes.ToArray());

            foreach(TPL_TextureHeader textureheader in tpl.tplTextureHeaders)
            {
                textureheader.WrapS = (uint)prompt.wrapSComboBox.SelectedIndex;
                textureheader.WrapT = (uint)prompt.wrapTComboBox.SelectedIndex;
            }

            MemoryStreamEndian header = new MemoryStreamEndian(new byte[0x20], false) ;
            header.Pad(0x03, 0x00);
            header.WriteE(alphaused);
            header.WriteE(alphaused ? 1f : 0f);
            header.WriteE((short)images.Count);
            header.WriteE(alphaused ? (byte)0 : (byte)1);
            header.Pad(1, 0);
            header.WriteE(0x78C87406);
            header.WriteE(0xEE000000);
            header.Pad(0x0C, 0x00);


            currTexture.data = header.ToArray().Concat(tpl.ToByteArray()).ToList();

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
}
