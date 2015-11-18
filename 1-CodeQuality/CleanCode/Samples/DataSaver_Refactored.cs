using System;
using System.IO;

namespace CleanCode.Samples
{
    internal class DataSaver_Refactored
    {
        public static void SaveData_Refactored(string filename, byte[] data)
        {
            File.WriteAllBytes(filename, data);
            File.WriteAllBytes(Path.ChangeExtension(filename, "bkp"), data);
            File.WriteAllBytes(filename + ".time", BitConverter.GetBytes(DateTime.Now.Ticks));
        }

    }
}