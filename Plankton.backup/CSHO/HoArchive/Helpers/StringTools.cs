
namespace HoArchive{
    public static class StringTools{
        public static string ConditionalTrim(string input, int length){
            if (input.Length > length){input = input.Remove(length);}
            return input;
        }
        
    }
}