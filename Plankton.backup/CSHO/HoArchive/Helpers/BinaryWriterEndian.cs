using System.IO;
using System;
using System.Linq;
using System.Text;

namespace HoArchive{
    public class BinaryWriterEndian : BinaryWriter{
        public bool endianness; // 0: big, 1: little
        public BinaryWriterEndian(string path, bool endian) : base(new FileStream(path, FileMode.Create)){
            endianness = endian;
        }

        public void WriteString(string input){
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            Write(bytes);
        }

        public void WriteStringE(string input){
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            if (endianness){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }

        public void WriteWideStringE(string input){
            byte[] bytes = Encoding.Unicode.GetBytes(input);
            byte temp;
            if (endianness == false){
                for (int i=0; i<bytes.Count(); i += 2){
                    temp = bytes[i+1];
                    bytes[i+1] = bytes[i];
                    bytes[i] = temp;
                }
            }
            Write(bytes);
        }
        

        public void WriteE(byte[] input){
            byte[] bytes = input;
            if (endianness){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }
        
        public void WriteE(byte input){
            Write(input);
        }
        
        public void WriteE(ushort input){
            byte[] bytes = BitConverter.GetBytes(input);
            if (endianness == false){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }

        public void WriteE(uint input){
            byte[] bytes = BitConverter.GetBytes(input);
            if (endianness == false){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }

        public void WriteE(ulong input){
            byte[] bytes = BitConverter.GetBytes(input);
            if (endianness == false){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }

        public void WriteE(float input){
            byte[] bytes = BitConverter.GetBytes(input);
            if (endianness == false){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }


        public void PadAlign(uint align, byte pad){
            PadTo(MathTools.RoundUpTo((uint)BaseStream.Position, align), pad);
        }

        public void Pad(uint amount, byte pad){
            for (int i=0; i<amount; i++){
                Write(pad);
            }
        }

        public void PadTo(uint offset, byte pad){
            for (int i=(int)BaseStream.Position; i<offset; i++){
                Write(pad);
            }
        }
    }
}