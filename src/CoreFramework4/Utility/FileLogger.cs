using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CoreFramework4
{
    public static class FileLogger
    {
        public static void Log(string filePath, string text)
        {
            //if (!File.Exists(filePath)) throw new FileNotFoundException(filePath + " not found");
            File.AppendAllText(@filePath, text);
        }
    }
}
