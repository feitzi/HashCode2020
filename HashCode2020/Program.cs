using System;
using System.Collections.Generic;
using System.IO;
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
            //
            // CalculateSolutionForInputFile("a_example.txt");
            CalculateSolutionForInputFile("b_read_on.txt");
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
                        x.MaxPointsForPeriod(availableDays, libraryProblem.Books))
                    .OrderByDescending(x => x.MaxScore);

                var nextLibrary = librariessSorted.FirstOrDefault();
                if (nextLibrary?.libary == null)
                {
                    break;
                }

                librarySolution.Add(nextLibrary);
                availableLibrary.Remove(nextLibrary.libary);
                availableLibrary.ForEach(x => x.MarkBooksAsProcessedByOtherLibrary(nextLibrary.BooksProcessed));
                availableDays -= nextLibrary.libary.SetupTime;

                availableLibrary = librariessSorted.Where(x => x.MaxScore > 0)
                    .Select(x => x.libary)
                    .ToList();
            }

            librarySolution = librarySolution.Where(x => x.BooksProcessed.Any()).ToList();
            WriteOutputFile(librarySolution, inputFileName);
        }

        public static void WriteOutputFile(List<LibraryInternSolution> librarySolution, string inputFileName)
        {
            using var streamWriter = File.CreateText($"{inputFileName}_solution.txt");
            streamWriter.WriteLine(librarySolution.Count);


            librarySolution.ForEach(library =>
            {
                streamWriter.WriteLine($"{library.libary.LibraryId} {library.BooksProcessed.Count}");
                streamWriter.WriteLine(string.Join(" ", library.BooksProcessed.Select(x => x.Id)));
            });
        }
    }
}