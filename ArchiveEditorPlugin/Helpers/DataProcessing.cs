using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.ArchiveEditorPlugin.Helpers
{
    public static class DataProcessing
    {
        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static string FormatBytes(long bytes)
        {
            if (bytes < 0)
                throw new ArgumentOutOfRangeException(nameof(bytes));

            string[] units = { "Bytes", "kB", "MB", "GB" };

            decimal size = bytes;
            int unit = 0;

            while (size >= 1000 && unit < units.Length - 1)
            {
                size /= 1000;
                unit++;
            }

            string number = size.ToString(
                unit == 0 ? "0" : "0.##",
                CultureInfo.InvariantCulture
            );

            return $"{number} {(bytes == 1 ? "Byte" : units[unit])}";
        }
    }

}
