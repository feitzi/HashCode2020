using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FileImporter
{
    public class Library
    {
        public Library(int libraryId, string firstLine, string secondLine, IDictionary<int, Book> allBooks)
        {
            LibraryId = libraryId;
            var firstLineValues = firstLine.Split(" ");
            BooksInLibrary = int.Parse(firstLineValues[0]);
            SetupTime = int.Parse(firstLineValues[1]);
            BooksPerDay = int.Parse(firstLineValues[2]);

            var secondLineValues = secondLine.Split(" ");
            BookIdsInLibrary = secondLineValues.Select(x => allBooks[int.Parse(x)]).ToImmutableDictionary(x => x.Id);
            BooksOrderedByScore =
                BookIdsInLibrary.Select(x => x.Value).OrderByDescending(x => x.Score).ToImmutableList();
        }

        public int LibraryId { get; }

        public int BooksInLibrary { get; }
        public int SetupTime { get; }
        public int BooksPerDay { get; }
        public ImmutableDictionary<int, Book> BookIdsInLibrary { get; }

        public ImmutableList<Book> BooksOrderedByScore { get; }

        public int MaxPointsForPeriod(int availableDays, IDictionary<int, Book> books)
        {
            var workingPeriod = availableDays - SetupTime;
            if (workingPeriod <= 0) return 0;

            var possibleScore = 0;
            var maxPossibleBooksToProcess = workingPeriod * BooksPerDay;
            maxPossibleBooksToProcess = maxPossibleBooksToProcess > BooksOrderedByScore.Count()
                ? BooksOrderedByScore.Count()
                : maxPossibleBooksToProcess;
            for (int i = 0; i < maxPossibleBooksToProcess; i++)
            {
                possibleScore += books[i].Score;
            }

            return possibleScore;
        }
    }
}