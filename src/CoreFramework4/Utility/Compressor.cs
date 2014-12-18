using System.IO;
using System.IO.Compression;

namespace CoreFramework4
{
    public static class Compressor
    {
        public static byte[] Compress(byte[] data)
        {
            var output = new MemoryStream();
            var gzip = new GZipStream(output,CompressionMode.Compress, true);
            gzip.Write(data, 0, data.Length);
            gzip.Close();
            return output.ToArray();
        }

        public static byte[] Decompress(byte[] data)
        {
            var input = new MemoryStream();
            input.Write(data, 0, data.Length);
            input.Position = 0;
            var gzip = new GZipStream(input, CompressionMode.Decompress, true);
            var output = new MemoryStream();
            byte[] buff = new byte[64];
            int read = -1;
            read = gzip.Read(buff, 0, buff.Length);
            while (read > 0)
            {
                output.Write(buff, 0, read);
                read = gzip.Read(buff, 0, buff.Length);
            }
            gzip.Close();
            return output.ToArray();
        }
    }
}
