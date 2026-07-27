using System.Windows.Forms;

namespace PluginApi
{
    public interface IPlugin
    {
        public string Name { get; }
        public string Description { get; }
        public void Initialize(IHost host);
    }

    public interface IHost
    {
        public event EventHandler<FileEventArgs>? OpenedArchive;
        public event EventHandler<FileEventArgs>? ClosedArchive;
    }

    public sealed class FileEventArgs : EventArgs
    {
        public string filename = "";
        public string gamestring = "";

        public static FileEventArgs Empty => new FileEventArgs();

        public FileEventArgs(string filename="", string gamestring = "")
        {
            this.filename = filename;
            this.gamestring = gamestring;
        }

    }

}
