using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab15
{
    internal class BinaryChanges
    {

        public static string StrConvertToBin(string data)
        {
            String binaryStr = "";

            foreach (char c in data.ToCharArray())
            {
                binaryStr += Convert.ToString(c, 2).PadLeft(8, '0');
            }

            while (binaryStr.Length % 8 != 0)
            {
                binaryStr = "0" + binaryStr;
            }

            return binaryStr;
        }

     
        public static string BinConvertToStr(string data)
        {
            List<Byte> listOfBytes = new List<Byte>();

            for (int i = 0; i + 8 - 1 <= data.Length; i += 8)
            {
                listOfBytes.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(listOfBytes.ToArray());
        }
    }
}
