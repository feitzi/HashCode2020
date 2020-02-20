namespace FileImporter
{
    public class Book
    {
        public Book(int id, int score)
        {
            Id = id;
            Score = score;
        }

        public int Id { get; }
        public int Score { get; }
    }
}    