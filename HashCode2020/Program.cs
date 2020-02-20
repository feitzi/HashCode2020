using System;
using FileImporter;
using Serilog;

namespace HashCode2020
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            
           var inputFile = FileImporter.FileImporter.ReadAllFromFile("c_urgent.in");
            
            Log.Information("Input is {@First}", inputFile.HeaderLine);
            
        }
    }
}