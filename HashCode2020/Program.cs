using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileImporter;
using Serilog;

namespace HashCode2020
{
    public class LibraryInternSolution
    {
        public Library libary { get; }
        public KeyValuePair<int, List<Book>> KeyValue { get; }

        public LibraryInternSolution(Library libary, KeyValuePair<int, List<Book>> keyValue)
        {
            this.libary = libary;
            KeyValue = keyValue;
        }
    }

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
            var librarySolution = new List<LibraryInternSolution>();
            var availableLibrary = libraryProblem.Libraries.Select(x => x.Value).ToList();
            while (availableDays > 0)
            {
                var nextLibrary = availableLibrary
                    .Select(x =>
                        new LibraryInternSolution(x, x.MaxPointsForPeriod(availableDays, libraryProblem.Books)))
                    .OrderByDescending(x => x.KeyValue.Key)
                    .FirstOrDefault();
                if (nextLibrary?.libary == null)
                {
                    break;
                }

                librarySolution.Add(nextLibrary);
                availableLibrary.Remove(nextLibrary.libary);
                availableLibrary.ForEach(x => x.MarkBooksAsProcessedByOtherLibrary(nextLibrary.KeyValue.Value));
            }

            WriteOutputFile(librarySolution);
        }

        public static void WriteOutputFile(List<LibraryInternSolution> librarySolution)
        {
            using var streamWriter = File.CreateText("output.txt");
            streamWriter.WriteLine(librarySolution.Count);


            librarySolution.ForEach(library =>
            {
                streamWriter.WriteLine($"{library.libary.LibraryId} {library.KeyValue.Value.Count}");
                streamWriter.WriteLine(string.Join(" ", library.KeyValue.Value.Select(x => x.Id)));
            });
        }
    }
}