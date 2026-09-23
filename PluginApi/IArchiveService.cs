using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHO;
using HoArchive;

namespace PluginApi
{

    public interface IArchiveService
    {
        Handler Current { get; }

        event EventHandler<ArchiveEventArgs>? Opened;
        event EventHandler<ArchiveEventArgs>? Closed;

        event EventHandler<AssetEventArgs>? AssetsModified;
        event EventHandler<AssetEventArgs>? AssetsModifiedPreview;
        event EventHandler<AssetEventArgs>? AssetsDeleted;
        event EventHandler<AssetEventArgs>? AssetsCreated;

        void NotifyAssetsModified(object? sender, IEnumerable<AssetInfo> assets);
        void NotifyAssetsModifiedPreview(object? sender, IEnumerable<AssetInfo> assets);
        void NotifyAssetsDeleted(object? sender, IEnumerable<AssetInfo> assets);
        void NotifyAssetsCreated(object? sender, IEnumerable<AssetInfo> assets);
    }



    public sealed class ArchiveEventArgs : EventArgs
    {
        public Handler Archive { get; }

        public ArchiveEventArgs(Handler archive)
        {
            Archive = archive;
        }
    }

    public sealed class AssetEventArgs : EventArgs
    {
        public IReadOnlyList<AssetInfo> Assets { get; }
        public Handler Archive { get; }

        public AssetEventArgs(
            IEnumerable<AssetInfo> assets,
            Handler archive)
        {
            Assets = assets.ToArray();
            Archive = archive;
        }
    }

    public sealed class AssetInfo
    {
        public Handler Archive { get; }
        public TOCEntry Entry { get; }


        public AssetInfo(Handler archive, TOCEntry entry)
        {
            Archive = archive;
            Entry = entry;
        }
    }
}
