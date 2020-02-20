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
            
           var inputFile = FileImporter.FileImporter.ReadAllFromFile("txture@innsbruck ");
            
            Log.Information("Input is {@First}", inputFile.HeaderLine);
            
        }
    }
}