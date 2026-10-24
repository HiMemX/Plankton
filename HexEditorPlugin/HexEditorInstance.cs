using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;
using PluginApi;

namespace HexEditorPlugin
{
    internal class HexEditorInstance : IEditor
    {

        private readonly IHost host;
        private readonly HexEditorControl control;

        //public ArchiveEditorPreferences preferences = new();

        public string Title { get; set; } = "Hex Editor";
        public Icon? Icon => new Icon("Resources/HexEditor.ico");
        public Control Control => control;

        private TOCEntry? current;



        public HexEditorInstance(IHost host, EditorTarget initialTarget)
        {
            this.host = host;
            //this.preferences = preferences;

            control = new HexEditorControl() { Dock = DockStyle.Fill };

            host.Archive.Closed += Archive_Opened;
            host.Archive.Opened += Archive_Opened;
            host.Archive.AssetsModified += AssetsModified;

            Reveal(initialTarget);

            control.SaveRequested += (_, _) =>
            {
                if (current == null) return;

                byte[] modifiedData = control.GetBytes();

                current.data = modifiedData.ToList();
                try { current.CreateEntity(); }
                catch(Exception e){
                    host.Commands.Execute("plankton.console.add-error", ("HexEditorInstance.SaveRequested", e.ToString()));
                    return;
                }

                host.Archive.NotifyAssetsModified(this, new List<AssetInfo>() { new AssetInfo(host.Archive.Current, current) });
                control.SetModified(false);
            };
            
        }

        private void AssetsModified(object? sender, AssetEventArgs e)
        {
            if (e.Assets[0].Entry == current)
            {
                byte[] assetData;
                assetData = e.Assets[0].Entry.data.ToArray();

                control.LoadBytes(assetData);
            }
        }

        public void Archive_Opened(object? sender, ArchiveEventArgs e)
        {
            Dispose();
        }

        public bool CanReveal(EditorTarget target)
        {
            return target.Value is AssetInfo;

        }

        public void Reveal(EditorTarget target)
        {
            current = (target.Value as AssetInfo).Entry;

            byte[] assetData;
            assetData = current.data.ToArray();

            control.LoadBytes(assetData);

            
        }

        public void Dispose()
        {
            host.Archive.Opened -= Archive_Opened;
            host.Archive.AssetsModified -= AssetsModified;
            control.Dispose();
        }
    }
}
