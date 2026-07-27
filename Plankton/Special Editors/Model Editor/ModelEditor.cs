using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Plankton.Rendering;

namespace Plankton.Special_Editors.Model_Editor
{
    public partial class ModelEditor : UserControl
    {


        public ModelEditor()
        {
            InitializeComponent();
            geometryBaseRenderer.Render += Render;
            geometryBaseRenderer.PreRender += PreRender;
        }

        public void InitRenderer()
        {
            geometryBaseRenderer.InitRenderer();
        }

        public void StartRenderLoop()
        {
            geometryBaseRenderer.StartRenderLoop();
        }

        public void PreRender(object sender, EventArgs e)
        {
            UserConfigurationApplicator.Apply(UserConfigurationManager.userConfig, geometryBaseRenderer);
        }

        public void Render(object sender, EventArgs e)
        {
            geometryBaseRenderer.DrawGrid();
        }

    }
}
