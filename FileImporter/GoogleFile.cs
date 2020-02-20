using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace FileImporter
{
    public class ParsedLibraryProblem
    {
        public ParsedLibraryProblem(StreamReader reader)
        {
            var headerLine = reader.ReadLine();
            var values = headerLine.Split(' ');
            BookCount = int.Parse(values[0]);
            LibraryCounts = int.Parse(values[1]);
            Days = int.Parse(values[2]);

            var secondLine = reader.ReadLine();
            var secondLineValues = secondLine.Split(' ');

            var books = new Dictionary<int, Book>();
            for (var i = 0; i < secondLineValues.Length; i++)
            {
                var book = new Book(i, int.Parse(secondLineValues[i]));
                books.Add(i, book);
            }

            var libraryId = 0;
            var libraries = new Dictionary<int, Library>();
            while (!reader.EndOfStream)
            {
                var firstLibraryLine = reader.ReadLine();
                var secondLibraryLine = reader.ReadLine();
                var library = new Library(libraryId, firstLibraryLine, secondLibraryLine);
                libraries.Add(libraryId, library);
                libraryId += 1;
            }

            Books = books.ToImmutableDictionary();
            Libraries = libraries.ToImmutableDictionary();
        }

        public ImmutableDictionary<int, Library> Libraries { get; }

        public ImmutableDictionary<int, Book> Books { get; }

        public int BookCount { get; }
        public int LibraryCounts { get; }
        public int Days { get; }
        
        
    }
}