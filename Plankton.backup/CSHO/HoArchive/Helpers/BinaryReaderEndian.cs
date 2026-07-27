using System.IO;
using System;
using System.Linq;

namespace HoArchive{
    public class BinaryReaderEndian : BinaryReader{
        public bool endianness; // 0 = big, 1 = little
        // If function PostFix = "E", then it will read the data type according to the endianness

        public void Align(int align){
            BaseStream.Position = ((BaseStream.Position + align - 1) / align) * align;
        }
        
        public byte[] ReadBytesE(int amount){
            byte[] value = ReadBytes(amount);
            if (!endianness)
                return value;
            return value.Reverse().ToArray();
        }

        public string ReadString(int stringlen){ // Big Endian
            return new string(ReadChars(stringlen)); 
        }
        
        public string ReadUntil(byte terminator){ // Big Endian
            byte curr_char;
            string output = "";
            while(true){
                curr_char = ReadByte();
                if (curr_char == terminator){break;}
                output += (char)curr_char;
            }
            return output;
        }
        

        public float ReadFloat32E(){
            uint value = ReadUInt32();
            if (endianness)
                return BitConverter.Int32BitsToSingle((int)value);
            return BitConverter.Int32BitsToSingle(BitConverter.ToInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0));
        }

        public BinaryReaderEndian(string path, bool endian) : base(new FileStream(path, FileMode.Open)){
            endianness = endian;
        }

        public ushort ReadUInt16E(){
            ushort value = ReadUInt16();
            if (endianness)
                return value;
            return BitConverter.ToUInt16(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        public uint ReadUInt32E(){
            uint value = ReadUInt32();
            if (endianness)
                return value;
            return BitConverter.ToUInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        public int ReadInt32E(){
            int value = ReadInt32();
            if (endianness)
                return value;
            return BitConverter.ToInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        public ulong ReadUInt64E(){
            ulong value = ReadUInt64();
            if (endianness)
                return value;
            return BitConverter.ToUInt64(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }


        public string ReadStringE(int stringlen){
            char[] chars = ReadChars(stringlen);
            if (endianness){
                Array.Reverse(chars);
            }
            return new string(chars);
        }

        public string ReadWideStringE(int maxlength){
            uint curr_char;
            string output = "";
            for (int i=0; i<maxlength; i++){
                curr_char = ReadUInt16E();
                output += curr_char.ToString();
            }
            return output;
        }
        public string ReadWideStringE(int maxlength, int terminator){
            ushort curr_char;
            string output = "";
            for (int i=0; i<maxlength; i++){
                curr_char = ReadUInt16E();
                if (curr_char == terminator){break;}
                output += (char)curr_char;
            }
            return output;
        }
        

    }
}