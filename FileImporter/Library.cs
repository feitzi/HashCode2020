using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FileImporter
{
    public class Library {
        public int LibraryId { get; }

        public int BooksInLibrary { get; }
        public int SetupTime { get; }
        public int BooksPerDay { get; }
        public ImmutableHashSet<int> BookIdsInLibrary { get; }
        public Library(int libraryId, string firstLine, string secondLine)
        {
            LibraryId = libraryId;
            var firstLineValues = firstLine.Split(" ");
            BooksInLibrary = int.Parse(firstLineValues[0]);
            SetupTime = int.Parse(firstLineValues[1]);
            BooksPerDay = int.Parse(firstLineValues[2]);

            var secondLineValues = secondLine.Split(" ");
            BookIdsInLibrary = secondLineValues.Select(x => int.Parse(x)).ToImmutableHashSet();
        }
    }
}