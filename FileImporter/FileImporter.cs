using System;
using System.IO;
using System.Linq;

namespace FileImporter
{
    public static class FileImporter
    {
        public static GoogleFile ReadAllFromFile(string fileName)
        {
            var allLines = File.ReadAllLines(fileName);
            return new GoogleFile(allLines.First(), allLines.Skip(1).ToList(), fileName);
        }
    }
}