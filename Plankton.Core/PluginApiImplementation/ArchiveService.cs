using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHO;
using PluginApi;

namespace Plankton.PluginApiImplementation
{
    internal sealed class ArchiveService : IArchiveService
    {
        public Handler Current { get; private set; } = new();

        public event EventHandler<ArchiveEventArgs>? Opened;
        public event EventHandler<ArchiveEventArgs>? Closed;

        public event EventHandler<AssetEventArgs>? AssetsModified;
        public event EventHandler<AssetEventArgs>? AssetsModifiedPreview;
        public event EventHandler<AssetEventArgs>? AssetsDeleted;
        public event EventHandler<AssetEventArgs>? AssetsCreated;

        public void NotifyArchiveOpened(object? sender, ArchiveEventArgs e)
        {
            Opened?.Invoke(sender, e);
        }

        public void NotifyArchiveClosed(object? sender, ArchiveEventArgs e)
        {
            Closed?.Invoke(sender, e);
        }

        public void Open(string path)
        {
            Current.Open(path);
            NotifyArchiveOpened(this, new ArchiveEventArgs(Current));
        }



        public void NotifyAssetsModified(
            object? sender,
            IEnumerable<AssetInfo> assets)
        {
            RaiseAssetEvent(
                sender,
                AssetsModified,
                assets);
        }

        public void NotifyAssetsModifiedPreview(
            object? sender,
            IEnumerable<AssetInfo> assets)
        {
            RaiseAssetEvent(
                sender,
                AssetsModifiedPreview,
                assets);
        }

        public void NotifyAssetsDeleted(
            object? sender,
            IEnumerable<AssetInfo> assets)
        {
            RaiseAssetEvent(
                sender,
                AssetsDeleted,
                assets);
        }

        public void NotifyAssetsCreated(
            object? sender,
            IEnumerable<AssetInfo> assets)
        {
            RaiseAssetEvent(
                sender,
                AssetsCreated,
                assets);
        }


        private void RaiseAssetEvent(
            object? sender,
            EventHandler<AssetEventArgs>? eventHandler,
            IEnumerable<AssetInfo> assets)
        {
            ArgumentNullException.ThrowIfNull(assets);

            Handler archive = Current;


            // Snapshot it now. This avoids problems if the caller passes
            // a mutable List and modifies it after Notify... returns,
            // or if several listeners inspect the same collection.
            AssetInfo[] assetArray = assets.ToArray();

            if (assetArray.Length == 0)
                return;

            eventHandler?.Invoke(
                sender,
                new AssetEventArgs(
                    assetArray,
                    archive));
        }
    }
}
