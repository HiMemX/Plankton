using System.Collections.Generic;

namespace CSHO{
    public partial class Handler{
        public string SetAssetData(ulong AssetID, List<byte> data){
            HoArchive.TOCEntry asset = GetAsset(AssetID);
            if (asset == null){return "ERR_ASSET_NOT_FOUND";}
            asset.data = data;
            return "";
        }
    }
}