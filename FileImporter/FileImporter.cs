using System;
using System.IO;
using System.Linq;

namespace FileImporter
{
    public static class FileImporter
    {
        public static ParsedLibraryProblem ReadAllFromFile(string fileName)
        {
            FileStream fileStream = File.OpenRead(fileName);
            var streamReader = new StreamReader(fileStream);

            return new ParsedLibraryProblem(streamReader);

        }
    }

}