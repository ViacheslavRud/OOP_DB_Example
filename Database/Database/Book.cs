namespace Database.Database
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
    }

    public class BookAuthor
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }

    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
    }
}