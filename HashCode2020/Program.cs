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
            //
            // CalculateSolutionForInputFile("a_example.txt");
            // CalculateSolutionForInputFile("b_read_on.txt");
            // CalculateSolutionForInputFile("c_incunabula.txt");
            CalculateSolutionForInputFile("d_tough_choices.txt");
            // CalculateSolutionForInputFile("e_so_many_books.txt");
            // CalculateSolutionForInputFile("f_libraries_of_the_world.txt");
        }

        private static void CalculateSolutionForInputFile(string inputFileName)
        {
            var libraryProblem = FileImporter.FileImporter.ReadAllFromFile(inputFileName);

            // Log.Information("Input is {@First}", inputFile);

            var availableDays = libraryProblem.Days;
            var librarySolution = new List<LibraryInternSolution>();
            var availableLibrary = libraryProblem.Libraries.Select(x => x.Value).ToList();
            while (availableDays > 0)
            {
                var librariessSorted = availableLibrary
                    .Select(x =>
                        new LibraryInternSolution(x, x.MaxPointsForPeriod(availableDays, libraryProblem.Books)))
                    .OrderByDescending(x => x.KeyValue.Key);
                   
                var nextLibrary =  librariessSorted.FirstOrDefault();
                if (nextLibrary?.libary == null)
                {
                    break;
                }

                librarySolution.Add(nextLibrary);
                availableLibrary.Remove(nextLibrary.libary);
                availableLibrary.ForEach(x => x.MarkBooksAsProcessedByOtherLibrary(nextLibrary.KeyValue.Value));
                availableDays -= nextLibrary.libary.SetupTime;

                availableLibrary = librariessSorted.Where(x => x.KeyValue.Key > 0)
                    .Select(x => x.libary)
                    .ToList();
            }

            librarySolution = librarySolution.Where(x => x.KeyValue.Value.Any()).ToList();
            WriteOutputFile(librarySolution, inputFileName);
        }

        public static void WriteOutputFile(List<LibraryInternSolution> librarySolution, string inputFileName)
        {
            using var streamWriter = File.CreateText($"{inputFileName}_solution.txt");
            streamWriter.WriteLine(librarySolution.Count);


            librarySolution.ForEach(library =>
            {
                streamWriter.WriteLine($"{library.libary.LibraryId} {library.KeyValue.Value.Count}");
                streamWriter.WriteLine(string.Join(" ", library.KeyValue.Value.Select(x => x.Id)));
            });
        }
    }
}