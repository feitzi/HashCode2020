using System;
using System.Collections.Generic;
using System.Linq;
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

            var libraryProblem = FileImporter.FileImporter.ReadAllFromFile("a_example.txt");

            // Log.Information("Input is {@First}", inputFile);

            var availableDays = libraryProblem.Days;
            var librarySolution = new List<Library>();
            var availableLibrary = libraryProblem.Libraries.Select(x => x.Value).ToList();
            while (availableDays > 0)
            {
                var nextLibrary = availableLibrary
                    .OrderByDescending(x => x.MaxPointsForPeriod(availableDays, libraryProblem.Books)).FirstOrDefault();
                if (nextLibrary == null)
                {
                    break;
                }
                librarySolution.Add(nextLibrary);
                availableLibrary.Remove(nextLibrary);
            }
            
            

        }
    }
}