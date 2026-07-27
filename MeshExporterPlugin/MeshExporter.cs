using PluginApi;
using System.Windows.Forms;

namespace MeshExporterPlugin
{
    public class MeshExporter : IPlugin
    {
        public string Name => "MeshExporter";
        public string Description => "Adds the option to export Models using Assimp";

        public void Initialize(IHost host)
        {
            MessageBox.Show("Hello world!");
        }
    }
}
