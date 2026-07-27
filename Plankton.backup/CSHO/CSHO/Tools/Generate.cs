using System;

namespace CSHO{
    public partial class Handler{
        public ulong GenerateAssetID(){
            byte[] AssetID = new byte[8];
            
            while (true){
                RNG.NextBytes(AssetID); 
                if (GetAsset((ulong)BitConverter.ToInt64(AssetID, 0)) == null){return (ulong)BitConverter.ToInt64(AssetID, 0);}
            }
        }
    }
}