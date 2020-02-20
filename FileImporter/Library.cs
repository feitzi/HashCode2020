using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using MoreLinq;

namespace FileImporter
{
    public class Library
    {
        public Library(int libraryId, string firstLine, string secondLine, IDictionary<int, Book> allBooks)
        {
            LibraryId = libraryId;
            var firstLineValues = firstLine.Split(" ");
            var booksInLibrary = int.Parse(firstLineValues[0]);
            SetupTime = int.Parse(firstLineValues[1]);
            BooksPerDay = int.Parse(firstLineValues[2]);

            var secondLineValues = secondLine.Split(" ");
            BookIdsInLibrary = secondLineValues.Select(x => allBooks[int.Parse(x)]).ToDictionary(x => x.Id);
            BooksOrderedByScore =
                BookIdsInLibrary.Select(x => x.Value).OrderByDescending(x => x.Score).ToImmutableList();
        }

        public int LibraryId { get; }

        public int SetupTime { get; }
        public int BooksPerDay { get; }
        public Dictionary<int, Book> BookIdsInLibrary { get; private set; }

        public ImmutableList<Book> BooksOrderedByScore { get; }


        public void MarkBooksAsProcessedByOtherLibrary(List<Book> processedBooks)
        {
            processedBooks.ForEach(x => BookIdsInLibrary.Remove(x.Id));
        }

        public KeyValuePair<int, List<Book>> MaxPointsForPeriod(int availableDays, IDictionary<int, Book> books)
        {
            var workingPeriod = availableDays - SetupTime;
            if (workingPeriod <= 0) return new KeyValuePair<int, List<Book>>(0, new List<Book>());

            var possibleScore = 0;
            var maxPossibleBooksToProcess = workingPeriod * BooksPerDay;
            maxPossibleBooksToProcess = maxPossibleBooksToProcess > BooksOrderedByScore.Count()
                ? BooksOrderedByScore.Count()
                : maxPossibleBooksToProcess;

            var booksWhichAreProcessed = new List<Book>();
            for (int i = 0; i < maxPossibleBooksToProcess; i++)
            {
                var book = BooksOrderedByScore[i];
                possibleScore += book.Score;
                booksWhichAreProcessed.Add(book);
            }
            return new KeyValuePair<int, List<Book>>(possibleScore, booksWhichAreProcessed);
        }
    }
}