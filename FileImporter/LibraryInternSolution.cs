using System;
using System.Collections.Generic;
using MoreLinq;

namespace FileImporter
{

    public class LibraryInternSolution
    {
        public Library libary { get; }
        
        public List<Book> BooksProcessed { get; }
        
        public int MaxScore { get; }
        public int DaysToFast { get; }


        public LibraryInternSolution(Library libary, List<Book> booksProcessed, int maxScore, int daysToFast)
        {
            this.libary = libary;
            BooksProcessed = booksProcessed;
            MaxScore = maxScore;
            DaysToFast = daysToFast;
        }
    }
}