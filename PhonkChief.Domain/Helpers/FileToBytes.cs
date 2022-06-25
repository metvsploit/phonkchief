using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PhonkChief.Domain.Helpers
{
    public class FileToBytes
    {
        public byte[] ConvertToBytes(string path)
        {
            byte[] data = File.ReadAllBytes(path);
            return data;
        }
    }
}
