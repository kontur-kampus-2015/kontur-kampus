using System;
using System.IO;

namespace CleanCode.Samples
{
    internal class DataSaver
    {
        public static void SaveData(string filename, byte[] data)
        {
            //open files
            var fs1 = new FileStream(filename, FileMode.OpenOrCreate);
            var fs2 = new FileStream(Path.ChangeExtension(filename, "bkp"), FileMode.OpenOrCreate);

            // write data
            fs1.Write(data, 0, data.Length);
            fs2.Write(data, 0, data.Length);

            // close files
            fs1.Close();
            fs2.Close();

            // save last-write time
            string tf = filename + ".time";
            var fs3 = new FileStream(tf, FileMode.OpenOrCreate);
            var t = BitConverter.GetBytes(DateTime.Now.Ticks);
            fs3.Write(t, 0, t.Length);
            fs3.Close();
        }

    }
}
