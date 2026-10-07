using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PluginApi
{
    public interface IPlugin
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }

        void Initialize(IHost host);
    }

    public interface IHost
    {
        IArchiveService Archive { get; }
        ICommandService Commands { get; }
        IEditorService Editors { get; }
        IUIService UI { get; }
        IPreferencesService Preferences { get; }

    }



}
