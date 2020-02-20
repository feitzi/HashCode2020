using System.Collections.Generic;

namespace FileImporter
{
    public class  GoogleFile
    {
        public string HeaderLine { get; }
        public List<string> Lines { get; }

        public string FileName { get;}

        public GoogleFile(string headerLine, List<string> lines, string fileName)
        {
            HeaderLine = headerLine;
            Lines = lines;
            FileName = fileName;
        }
    }
}