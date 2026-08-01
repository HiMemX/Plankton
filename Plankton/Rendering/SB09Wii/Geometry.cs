using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using HoArchive;
using OpenTK.Graphics.OpenGL4;
using Plankton.Rendering.Base;
using SB09WiiAsset;

namespace Plankton.Rendering.SB09Wii
{
    internal class Geometry : Rendering.Base.GeometryBase
    {
        public GeometryAsset entity;

        public Geometry(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool)
        {
            entity = (GeometryAsset)asset.entity;
        }

        public override void Update(TOCEntry asset = null)
        {
            base.Update(asset);
            Init();
        }

        public override Vector3 GetAABBCenter()
        {
            return entity.GetAABBCenter();
        }

        public override Vector3 GetBoundSphereCenter()
        {
            return entity.GetBoundSphereCenter();
        }

        public override float GetBoundSphereRadius()
        {
            return entity.GetBoundSphereRadius();
        }

        public override List<ulong> GetBufferIDs()
        {
            return entity.GetBufferIDs();
        }

        public override ulong GetIndexBufferID()
        {
            return entity.GetIndexBufferID();
        }

        public override ulong GetMaterialID()
        {
            return entity.GetMaterialID();
        }

        public override List<ulong> GetRendTextureIDs()
        {
            return entity.GetRendTextureIDs();
        }

        public override void UpdateAssociates()
        {
            
        }

        public override void Init()
        {
            //Debug.debugWindow.AddEntry("Geometry.Init", "starting init");
            if (asset.delete) return;



            CanRender = false;
            // Stuff that needs to init:
            // VertexBuffer
            // ElementBuffer
            // VertexArray
            // InstanceBuffer
            //if (((GeometryAsset)geometry)._streams[0].stride == 16) { return; } // PFST rawblobs can go fuck themselves rn // 18.10.2025: This changes RIGHT NOW!

            BaseClass material = resourcePool.GetAssetFromPool(resourcePool.aMaterialPool, GetMaterialID());
            if (material == null) { return; }

            BaseClass effect = resourcePool.GetAssetFromPool(resourcePool.aEffectPool, ((MaterialBase)material).GetEffectID());
            if (effect == null) { return; }

            BaseClass shader = resourcePool.GetAssetFromPool(resourcePool.aShaderPool, ((EffectBase)effect).GetShaderID());
            if (shader == null) { return; }

            List<BaseClass> vertexbuffers = resourcePool.GetAssetsFromPool(resourcePool.rawblobPool, GetBufferIDs());
            BaseClass indexbuffer = resourcePool.GetAssetFromPool(resourcePool.rawblobPool, GetIndexBufferID());


            foreach (BaseClass buf in vertexbuffers)
            {
                if (buf == null) { return; }
            }
            if (indexbuffer == null) { return; }


            ShaderInfo info = ((ShaderBase)shader).GetShaderInfo();
            ShaderOpsFlags = ((GenericShader)((Shader)shader).entity)._shaderOps.flags;
            //Debug.debugWindow.AddEntry("Geometry", ShaderOpsFlags.ToString("X4"));

            // Preprocess Vertex Data
            (List<List<float>>, List<int>) blobs = ProcessVertexIndexBuffers(vertexbuffers, indexbuffer, info);
            List<List<float>> vertexblobs = blobs.Item1;
            List<int> indexblob = blobs.Item2;
            vertexdata = vertexblobs;
            indexdata = indexblob;

            //Debug.debugWindow.AddEntry("Geometry.Init", vertexdata.Count.ToString());

            List<float> combinedvertexblob = new();
            foreach (List<float> blob in vertexblobs)
            {
                combinedvertexblob.AddRange(blob);
            }

            elementCount = indexblob.Count;


            // Init vertex array
            Helpers.GenerateVertexArrayHandle(VertexArray);
            GL.BindVertexArray(VertexArray.handle);

            // Initiate VertexBuffer
            Helpers.GenerateHandle(VertexBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer.handle);
            GL.BufferData(BufferTarget.ArrayBuffer, combinedvertexblob.Count * sizeof(float), combinedvertexblob.ToArray(), BufferUsageHint.StaticDraw);



            List<ChannelInfo> unsortedinfos = info.GetChannelInfos();
            int offset = 0;
            Flags = 0;

            bool ispfst = ((GeometryAsset)entity)._streams[0].stride == 16;

            for (int s = 0; s < unsortedinfos.Count; s++)
            {
                Flags = (Flags << 1) | ((unsortedinfos[s].vtype == 255) ? 0 : 1);

                if (unsortedinfos[s].vtype == 255)
                {
                    continue;
                }

                GL.VertexAttribPointer(s, unsortedinfos[s].GetTupleCount(), VertexAttribPointerType.Float, false, 0, offset);
                GL.EnableVertexAttribArray(s);

                offset += vertexblobs[s].Count * 4;
            }

            // Init element buffer
            Helpers.GenerateHandle(ElementBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBuffer.handle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indexblob.Count * sizeof(int), indexblob.ToArray(), BufferUsageHint.StaticDraw);

            // Copy texturebuffers
            List<TextureBase> textures = GetTextures(resourcePool, (MaterialBase)material);
            List<TextureBase> rendTextures = GetRendTextures(resourcePool);

            if(info.materialSettings.ambientScaleIndex != 255)
            {
                int idx = info.materialSettings.ambientScaleIndex;
                Pointer32_f ptr = (Pointer32_f)((SB09WiiAsset.Material)material.asset.entity).materialParams[idx].__anon;
                ambientScale = new OpenTK.Graphics.Color4(ptr.f[0], ptr.f[1], ptr.f[2], 1);
            }
            if (info.materialSettings.environmentMapIndex != 255)
            {
                int idx = info.materialSettings.environmentMapIndex;
                Pointer32_SamplerParamData ptr = (Pointer32_SamplerParamData)((SB09WiiAsset.Material)material.asset.entity).materialParams[idx].__anon;
                textureSet.environmentMapID = ptr.samp[0].textureID;
                TextureBase t = (TextureBase)resourcePool.GetAssetFromPool(resourcePool.aTexturePool, ptr.samp[0].textureID);
                textureSet.environmentMap = t.buffer;
            }

            if (info.materialSettings.diffuseMapIndex != 255)
            {
                int idx = info.materialSettings.diffuseMapIndex;
                Pointer32_SamplerParamData ptr = (Pointer32_SamplerParamData)((SB09WiiAsset.Material)material.asset.entity).materialParams[idx].__anon;
                textureSet.diffuseMapID = ptr.samp[0].textureID;
                TextureBase t = (TextureBase)resourcePool.GetAssetFromPool(resourcePool.aTexturePool, ptr.samp[0].textureID);
                textureSet.diffuseMap = t.buffer;
            }
            if (info.rendParamSettings.diffuseMapIndex != 255)
            {
                int idx = info.rendParamSettings.diffuseMapIndex;
                Pointer32_SamplerParamData ptr = (Pointer32_SamplerParamData)((SB09WiiAsset.GeometryAsset)asset.entity)._rendParams[idx].__anon;
                textureSet.diffuseMapID = ptr.samp[0].textureID;
                TextureBase t = (TextureBase)resourcePool.GetAssetFromPool(resourcePool.aTexturePool, ptr.samp[0].textureID);
                textureSet.diffuseMap = t.buffer;
            }

            if (info.materialSettings.lightMapIndex != 255)
            {
                int idx = info.materialSettings.lightMapIndex;
                Pointer32_SamplerParamData ptr = (Pointer32_SamplerParamData)((SB09WiiAsset.Material)material.asset.entity).materialParams[idx].__anon;
                textureSet.lightMapID = ptr.samp[0].textureID;
                TextureBase t = (TextureBase)resourcePool.GetAssetFromPool(resourcePool.aTexturePool, ptr.samp[0].textureID);
                textureSet.lightMap = t.buffer;
            }
            if (info.rendParamSettings.lightMapIndex != 255)
            {
                int idx = info.rendParamSettings.lightMapIndex;
                Pointer32_SamplerParamData ptr = (Pointer32_SamplerParamData)((SB09WiiAsset.GeometryAsset)asset.entity)._rendParams[idx].__anon;
                textureSet.lightMapID = ptr.samp[0].textureID;
                TextureBase t = (TextureBase)resourcePool.GetAssetFromPool(resourcePool.aTexturePool, ptr.samp[0].textureID);
                textureSet.lightMap = t.buffer;
            }

            if (info.materialSettings.diffuseMap1Index != 255)
            {
                int idx = info.materialSettings.diffuseMap1Index;
                Pointer32_SamplerParamData ptr = (Pointer32_SamplerParamData)((SB09WiiAsset.Material)material.asset.entity).materialParams[idx].__anon;
                textureSet.diffuseMap1ID = ptr.samp[0].textureID;
                TextureBase t = (TextureBase)resourcePool.GetAssetFromPool(resourcePool.aTexturePool, ptr.samp[0].textureID);
                textureSet.diffuseMap1 = t.buffer;
            }
            if (info.rendParamSettings.diffuseMap1Index != 255)
            {
                int idx = info.rendParamSettings.diffuseMap1Index;
                Pointer32_SamplerParamData ptr = (Pointer32_SamplerParamData)((SB09WiiAsset.GeometryAsset)asset.entity)._rendParams[idx].__anon;
                textureSet.diffuseMap1ID = ptr.samp[0].textureID;
                TextureBase t = (TextureBase)resourcePool.GetAssetFromPool(resourcePool.aTexturePool, ptr.samp[0].textureID);
                textureSet.diffuseMap1 = t.buffer;
            }

            if (info.materialSettings.blendMapIndex != 255)
            {
                int idx = info.materialSettings.blendMapIndex;
                Pointer32_SamplerParamData ptr = (Pointer32_SamplerParamData)((SB09WiiAsset.Material)material.asset.entity).materialParams[idx].__anon;
                textureSet.blendMapID = ptr.samp[0].textureID;
                TextureBase t = (TextureBase)resourcePool.GetAssetFromPool(resourcePool.aTexturePool, ptr.samp[0].textureID);
                textureSet.blendMap = t.buffer;
            }
            if (info.rendParamSettings.blendMapIndex != 255)
            {
                int idx = info.rendParamSettings.blendMapIndex;
                Pointer32_SamplerParamData ptr = (Pointer32_SamplerParamData)((SB09WiiAsset.GeometryAsset)asset.entity)._rendParams[idx].__anon;
                textureSet.blendMapID = ptr.samp[0].textureID;
                TextureBase t = (TextureBase)resourcePool.GetAssetFromPool(resourcePool.aTexturePool, ptr.samp[0].textureID);
                textureSet.blendMap = t.buffer;
            }

            if (info.materialSettings.environmentScaleIndex != 255)
            {
                int idx = info.materialSettings.environmentScaleIndex;
                Pointer32_f ptr = (Pointer32_f)((SB09WiiAsset.Material)material.asset.entity).materialParams[idx].__anon;
                environmentScale = ptr.f[0];
            }

            List<MaterialSettings> settings = new List<MaterialSettings>() { info.materialSettings, info.rendParamSettings };
            List<List<TextureBase>> textureSets = new List<List<TextureBase>>() { textures, rendTextures }; // Not Rendering.TextureSet


            int bit = 0;
            TextureBase texture;



            // TEMPORARY UNTIL WE FIND OUT HOW RENDERMODES WORK
            isTransparent = (((MaterialBase)material).GetRenderModeID() != 0 || ((EffectBase)effect).GetRenderModeID() != 0);


            // Init instance buffer
            //GenerateHandle(geometry.InstanceBuffer);
            // TRY TO DEBUG THE OTHER STUFF FIRST // Alright, all debugged!
            Helpers.GenerateHandle(InstanceBuffer);
            //BufferGeometryInstances(entry);

            GL.BindBuffer(BufferTarget.ArrayBuffer, InstanceBuffer.handle);



            int stride = Marshal.SizeOf<InstanceInfo>();

            for (int i = 0; i < 4; i++)
            {
                GL.EnableVertexAttribArray(6 + i);
                GL.VertexAttribPointer(6 + i, 4, VertexAttribPointerType.Float, false, stride, (IntPtr)(i * 16));
                GL.VertexAttribDivisor(6 + i, 1);
            }

            GL.EnableVertexAttribArray(10);
            GL.VertexAttribIPointer(10, 1, VertexAttribIntegerType.UnsignedInt, stride, (IntPtr)64);
            GL.VertexAttribDivisor(10, 1);

            GL.EnableVertexAttribArray(11);
            GL.VertexAttribIPointer(11, 1, VertexAttribIntegerType.UnsignedInt, stride, (IntPtr)68);
            GL.VertexAttribDivisor(11, 1);

            GL.EnableVertexAttribArray(12);
            GL.VertexAttribIPointer(12, 1, VertexAttribIntegerType.UnsignedInt, stride, (IntPtr)72);
            GL.VertexAttribDivisor(12, 1);

            // lightkitIndex
            GL.EnableVertexAttribArray(13);
            GL.VertexAttribIPointer(13, 1, VertexAttribIntegerType.Int, stride, (IntPtr)76);
            GL.VertexAttribDivisor(13, 1);


            GL.BindVertexArray(0);



            //if(entry.uidSelf == 0xBB44B40D1E2CE3BC) { MessageBox.Show(geometry.InstanceMatrices.Count.ToString()); }

            CanRender = true;
        }

        public (MemoryStreamEndian, MemoryStreamEndian) ConvertFromPFSTStream(
            MemoryStreamEndian src,
            ChannelInfo posinfo,
            ChannelInfo norminfo,
            List<EvaluatorSkinSection> influenceSections)
        {
            MemoryStreamEndian pos = new MemoryStreamEndian(src.endianness);
            MemoryStreamEndian norm = new MemoryStreamEndian(src.endianness);

            foreach (EvaluatorSkinSection sect in influenceSections)
            {
                int linecount = (int)Math.Ceiling((float)sect.start / 2); // I took this math "directly" from the game code you cannot argue with me on this :)
                for (int i = 0; i < linecount; i++)
                {
                    (float x1, float x2) = src.ReadPQS(sect.pad);
                    (float y1, float y2) = src.ReadPQS(sect.pad);
                    (float z1, float z2) = src.ReadPQS(sect.pad);
                    (float nx1, float nx2) = src.ReadPQS(norminfo.vfrac, QuantizationType.BYTE);
                    (float ny1, float ny2) = src.ReadPQS(norminfo.vfrac, QuantizationType.BYTE);
                    (float nz1, float nz2) = src.ReadPQS(norminfo.vfrac, QuantizationType.BYTE);


                    src.Position -= 0x12;

                    if (sect.size == 1) src.Position += 20;
                    else src.Position += 36;

                    pos.WriteE(x1);
                    pos.WriteE(y1);
                    pos.WriteE(z1);
                    pos.WriteE(x2);
                    pos.WriteE(y2);
                    pos.WriteE(z2);

                    norm.WriteE(nx1);
                    norm.WriteE(ny1);
                    norm.WriteE(nz1);
                    norm.WriteE(nx2);
                    norm.WriteE(ny2);
                    norm.WriteE(nz2);

                    //if(i == 0) Debug.debugWindow.AddEntry("ConvertFromPFSTStream", x1, y1, z1);
                }

            }
            return (pos, norm);
        }

        public (List<List<float>>, List<int>) ProcessVertexIndexBuffers(List<BaseClass> vertexbuffers, BaseClass indexbuffer, ShaderInfo info)
        {

            List<List<float>> vertexblobs = new();

            List<int> indexblob = new();

            List<ChannelInfo> channelinfos = GetSortedChannelInfos(info, vertexbuffers.Count);

            bool ispfst = info.position.vstride == 16;

            // Initiating streams
            List<MemoryStreamEndian> vertexstreams = new();
            List<MemoryStreamEndian> outputstreams = new();

            foreach (BaseClass buffer in vertexbuffers)
            {

                vertexstreams.Add(new MemoryStreamEndian(buffer.asset.data.ToArray(), false));
                outputstreams.Add(new MemoryStreamEndian(false));
            }

            MemoryStreamEndian indexstream = new MemoryStreamEndian(indexbuffer.asset.data.ToArray(), false);

            MemoryStreamEndian pos = null;
            MemoryStreamEndian norm = null;
            if (ispfst)
            {

                //Debug.debugWindow.AddEntry("ProcessVertexIndexBuffers", vertexbuffers[0].asset.uidSelf.ToString("X16"));
                (pos, norm) = ConvertFromPFSTStream(vertexstreams[0], info.position, info.normal, ((SkinGeometry)entity)._influenceSections);
                outputstreams.Add(new MemoryStreamEndian(false));

            }


            // Structuring vertexstreams
            HoArchive.PrimitiveType primtype;
            byte temp;
            int indexcount;
            int currindexbase = 0;
            while (indexstream.Position < indexstream.Length)
            {
                temp = indexstream.ReadByte();
                if (temp == 0) { continue; }
                primtype = (HoArchive.PrimitiveType)temp;

                indexcount = indexstream.ReadUInt16E();

                // Process indices and put the according data into the output streams
                for (int i = 0; i < indexcount; i++)
                {
                    indexstream.ReadBytes(entity.wiiIndexSkin); // Used to be called animamount in Collin

                    if (!ispfst)
                    {
                        for (int s = 0; s < vertexbuffers.Count; s++)
                        {
                            CopyFromStream(outputstreams[s], vertexstreams[s], channelinfos[s], indexstream.ReadUInt16E());
                        }
                        continue;
                    }
                    // PFST Rawblob shenanigangs

                    uint posindex = indexstream.ReadUInt16E();
                    uint normindex = indexstream.ReadUInt16E();

                    CopyFromStream(outputstreams[0], pos, 12, 0, (int)posindex);
                    CopyFromStream(outputstreams[1], norm, 12, 0, (int)normindex);

                    for (int s = 1; s < vertexbuffers.Count; s++) // First stream is handled seperately as it is 2 streams combined (verts + normals)
                    {
                        CopyFromStream(outputstreams[s + 1], vertexstreams[s], info.GetChannelInfo(s), indexstream.ReadUInt16E());
                    }
                    //Debug.debugWindow.AddEntry("Indexing", indexbuffer.uidSelf.ToString("X16") + ", "  + indexstream.Position.ToString("X8"));
                }

                // Append indices to the index blob
                if (primtype == HoArchive.PrimitiveType.PRIM_TRILIST)
                {
                    for (int i = 0; i < indexcount; i++)
                    {
                        indexblob.Add(i + currindexbase);
                    }
                }
                else if (primtype == HoArchive.PrimitiveType.PRIM_TRISTRIP)
                {
                    for (int i = 0; i < indexcount - 2; i++)
                    {
                        // Switcharoo here because of backface culling
                        indexblob.Add(i + currindexbase + (i % 2));
                        indexblob.Add(i + currindexbase + 1 - (i % 2));
                        indexblob.Add(i + currindexbase + 2);
                    }
                }
                else
                {
                    throw new NotImplementedException(primtype.ToString());
                }
                currindexbase += indexcount;



            }

            List<ChannelInfo> unsortedchannelinfos = info.GetChannelInfos();

            if (!ispfst)
            {
                for (int s = 0; s < unsortedchannelinfos.Count; s++)
                {
                    if (unsortedchannelinfos[s].vtype == 255)
                    {
                        vertexblobs.Add(new List<float>());
                        continue;
                    }

                    vertexblobs.Add(ExtractEntriesFromVertexBuffer(outputstreams[unsortedchannelinfos[s].vindex], unsortedchannelinfos[s]));
                }
                return (vertexblobs, indexblob);
            }
            //Debug.debugWindow.AddEntry("Length", outputstreams[0].Length / 12);
            // pfst
            vertexblobs.Add(ExtractEntriesFromVertexBuffer(outputstreams[0], 1, 3, 4, 0));
            // Debug.debugWindow.AddEntry("Length", vertexblobs[0].Count / 3);
            vertexblobs.Add(ExtractEntriesFromVertexBuffer(outputstreams[1], 1, 3, 4, 0));
            for (int s = 2; s < unsortedchannelinfos.Count; s++)
            {
                if (unsortedchannelinfos[s].vtype == 255)
                {
                    vertexblobs.Add(new List<float>());
                    continue;
                }

                vertexblobs.Add(ExtractEntriesFromVertexBuffer(outputstreams[unsortedchannelinfos[s].vindex + 1], unsortedchannelinfos[s]));
            }
            return (vertexblobs, indexblob);


        }


        public List<TextureBase> GetTextures(ResourcePool resourcePool, MaterialBase material)
        {
            List<TextureBase> textures = new();
            TextureBase t;

            foreach (ulong id in material.GetTextureIDs())
            {
                t = (TextureBase)resourcePool.GetAssetFromPool(resourcePool.aTexturePool, id);
                textures.Add(t);
            }

            return textures;
        }

        public List<TextureBase> GetRendTextures(ResourcePool resourcePool)
        {
            List<TextureBase> textures = new();
            TextureBase t;

            foreach (ulong id in GetRendTextureIDs())
            {
                textures.Add((TextureBase)resourcePool.GetAssetFromPool(resourcePool.aTexturePool, id));
            }

            return textures;
        }



        public List<ChannelInfo> GetSortedChannelInfos(ShaderInfo info, int buffercount) // NOTE: THIS APPROACH ASSUMES THAT ALL VINDICES ARE TAKEN
        {
            List<ChannelInfo> output = new();
            for (int i = 0; i < buffercount; i++)
            {
                output.Add(info.GetChannelInfo(i));
            }
            return output;
        }

        public List<float> ExtractEntriesFromVertexBuffer(MemoryStreamEndian vertexbufferstream, float divisor, int tuplecount, int vtype, int offset)
        {
            vertexbufferstream.Position = offset;
            List<float> output = new();
            var functioncall = new Func<float>(() => 0);

            switch (vtype)
            {
                case 1: // byte3 // normal
                    functioncall = new Func<float>(() => vertexbufferstream.ReadSByte());
                    break;

                case 3: // ushort2 // uv
                    functioncall = new Func<float>(() => vertexbufferstream.ReadInt16E());
                    break;

                case 4: // float3 // position
                    functioncall = new Func<float>(() => vertexbufferstream.ReadFloat32E());
                    break;

                case 5: // byte4 // color
                    functioncall = new Func<float>(() => (float)vertexbufferstream.ReadByte() / 255.0f);
                    break;

                default:
                    break;
            }

            while (vertexbufferstream.Position < vertexbufferstream.Length)
            {
                for (int i = 0; i < tuplecount; i++)
                {
                    output.Add(functioncall() / divisor);
                }
            }

            return output;
        }

        public List<float> ExtractEntriesFromVertexBuffer(MemoryStreamEndian vertexbufferstream, ChannelInfo channelinfo)
        {
            //MemoryStreamEndian vertexbufferstream = new MemoryStreamEndian(vertexbuffer.data.ToArray(), handler.endian);


            float divisor = (float)Math.Pow(2, (double)channelinfo.vfrac);

            int tuplecount = channelinfo.GetTupleCount();

            return ExtractEntriesFromVertexBuffer(vertexbufferstream, divisor, tuplecount, channelinfo.vtype, channelinfo.voffset);

        }

        public void CopyFromStream(MemoryStreamEndian dest, MemoryStreamEndian src, int stride, int offset, int index)
        {
            src.Position = stride * index + offset;
            dest.Write(src.ReadBytes(stride));
        }

        public void CopyFromStream(MemoryStreamEndian dest, MemoryStreamEndian src, ChannelInfo info, int index)
        {
            CopyFromStream(dest, src, info.vstride, info.voffset, index);
        }


    }
}
