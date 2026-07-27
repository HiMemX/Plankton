using System;

namespace HoArchive{
    public class float3{
        public float x;
        public float y;
        public float z;

        public float3(BinaryReaderEndian file){
            x = file.ReadFloat32E();
            y = file.ReadFloat32E();
            z = file.ReadFloat32E();
        }

        public void Save(BinaryWriterEndian file){
            file.WriteE(x);
            file.WriteE(y);
            file.WriteE(z);
        }
    }

    public static class MathTools{
        public static uint RoundUpTo(uint num, uint up){
            return CeilDiv(num, up) * up;
        }
        public static uint CeilDiv(uint x, uint y){
            return ((x + y - 1) / y);
        }
        public static uint LowerCaseBKDR(string input){
            uint output = 0;
            foreach(char chr in input){
                output = LowerCase(Convert.ToByte(chr)) + output * 0x83;
            }
            return output;
        }
        public static byte LowerCase(byte chr){
            if (chr > 0x40 && chr < 0x5B){
                chr += 0x20;
            }
            return chr;
        }
    }
}