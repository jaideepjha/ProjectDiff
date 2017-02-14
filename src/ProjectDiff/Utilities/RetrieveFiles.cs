using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Utilities
{
    public static class RetrieveFiles
    {
        public static List<string> GetFiles(string sourceDirectory)
        {
            var csFiles = new List<string>();
            try
            {
                csFiles = Directory.EnumerateFiles(sourceDirectory, "*.cs", SearchOption.AllDirectories).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return csFiles;
        }
    }
}
