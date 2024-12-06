using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatGPT
{
    public class RandomData
    {

        /// Invoice Generator
        public static int increment = 0;
        public static string getNum()
        {
            DateTime barcodeGenerator = Convert.ToDateTime(DateTime.Now.ToString("d MMMM yyyy H:mm:ss"));
            barcodeGenerator.AddSeconds(-12101998);
            long unixTime = ((DateTimeOffset)barcodeGenerator).ToUnixTimeSeconds();
            increment++;
            int num = Convert.ToInt32((unixTime).ToString().Substring(0, 10)) + increment;
            return (num).ToString();
        }
        public static string GetBit()
        {
            increment++;
            byte[] buffer = Guid.NewGuid().ToByteArray();
            var FormNumber = BitConverter.ToUInt32(buffer, 0) ^ BitConverter.ToUInt32(buffer, 4) ^ BitConverter.ToUInt32(buffer, 8) ^ BitConverter.ToUInt32(buffer, 12);
            return FormNumber.ToString("X") + increment.ToString();
        }

    }
}