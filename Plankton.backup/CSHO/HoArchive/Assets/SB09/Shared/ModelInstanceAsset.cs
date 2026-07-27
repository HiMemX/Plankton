
namespace SB09Assets{
    public class ModelInstanceAsset{
        public ulong modelPrototypeID;
        public ulong lightKitID;
        public uint instanceParameterCount;
        public uint renderCustomizerCount;
        public uint instanceParams;
        public uint renderCustomizers;
        public ushort shadowType;
        public ushort shadowFlags;
        public uint shadowColorOverride;
        public uint shadowMaxDepthOverride;
        public uint shadowStartDepthOverride;
        public uint shadowMinBlurOverride;
        public uint shadowMaxBlurOverride;
        public ulong parentID;

        public ModelInstanceAsset(HoArchive.BinaryReaderEndian file){
            modelPrototypeID = file.ReadUInt64E();
            lightKitID = file.ReadUInt64E();
            instanceParameterCount = file.ReadUInt32E();
            renderCustomizerCount = file.ReadUInt32E();
            instanceParams = file.ReadUInt32E();
            renderCustomizers = file.ReadUInt32E();
            shadowType = file.ReadUInt16E();
            shadowFlags = file.ReadUInt16E();
            shadowColorOverride = file.ReadUInt32E();
            shadowMaxDepthOverride = file.ReadUInt32E();
            shadowStartDepthOverride = file.ReadUInt32E();
            shadowMinBlurOverride = file.ReadUInt32E();
            shadowMaxBlurOverride = file.ReadUInt32E();
            parentID = file.ReadUInt64E();
        }

        public void Save(HoArchive.BinaryWriterEndian file){
            file.WriteE(modelPrototypeID);
            file.WriteE(lightKitID);
            file.WriteE(instanceParameterCount);
            file.WriteE(renderCustomizerCount);
            file.WriteE(instanceParams);
            file.WriteE(renderCustomizers);
            file.WriteE(shadowType);
            file.WriteE(shadowFlags);
            file.WriteE(shadowColorOverride);
            file.WriteE(shadowMaxDepthOverride);
            file.WriteE(shadowStartDepthOverride);
            file.WriteE(shadowMinBlurOverride);
            file.WriteE(shadowMaxBlurOverride);
            file.WriteE(parentID);
        }

        public void Update(HoArchive.TOCEntry entry){
            instanceParameterCount = 0;
            renderCustomizerCount = 0;
            instanceParams = 0x120;
            renderCustomizers = 0x120;
        }
    }
}