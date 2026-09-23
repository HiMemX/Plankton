using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Docking;
using Krypton.Workspace;

namespace Plankton
{
    public partial class CoreForm : Form
    {
        private KryptonDockingManager dockingManager;
        private KryptonWorkspace workspace;

        public CoreForm()
        {
            InitializeComponent();

            workspace = new KryptonWorkspace
            {
                Dock = DockStyle.Fill
            };

            workspace.Parent = dockingPanel;
            Controls.Add(workspace);

            dockingManager = new KryptonDockingManager();
            // configure docking hierarchy here
        }
    }
}
