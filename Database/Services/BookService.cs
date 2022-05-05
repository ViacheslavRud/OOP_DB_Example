using System.Collections.Generic;
using System.Linq;
using Database.Database;
using Database.DTO;

namespace Database.Services
{
    public interface IBookService
    {
        int AddAuthor(string name);
        void AddBook(string title, IEnumerable<int> authors);
        List<BookDetails> GetALLBookDetails();
    }

    public class BookService : IBookService
    {
        private readonly LibraryContext _libraryContext;

        public BookService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public int AddAuthor(string name)
        {
            var author = new Author
            {
                Name = name
            };

            _libraryContext.Authors.Add(author);
            _libraryContext.SaveChanges();

            return author.AuthorId;
        }

        public void AddBook(string title, IEnumerable<int> authors)
        {
            var book = new Book
            {
                Title = title
            };

            _libraryContext.Books.Add(book);
            _libraryContext.SaveChanges();

            foreach (int id in authors)
            {
                _libraryContext.BookAuthors.Add(new BookAuthor
                {
                    BookId = book.BookId,
                    AuthorId = id
                });
            }

            _libraryContext.SaveChanges();
        }

        public List<BookDetails> GetALLBookDetails()
        {
            var books =
                from b in _libraryContext.Books
                join ba in _libraryContext.BookAuthors on b.BookId equals ba.BookId
                join a in _libraryContext.Authors on ba.AuthorId equals a.AuthorId
                select new {b.BookId, b.Title, AuthorName = a.Name};

            var booksList = books.ToList();

            var groups = booksList.GroupBy(s => s.BookId);

            List<BookDetails> bookDetailsList = new();
            foreach (var group in groups)
            {
                bookDetailsList.Add(new BookDetails
                {
                    Titile = group.First().Title,
                    BookId = group.First().BookId,
                    Authors = group.Select(s => s.AuthorName).ToList()
                });
                //Console.WriteLine(bookId + " " + bookTitle + " " + string.Join(", ", authorNames));
            }

            return bookDetailsList;
        }
    }
}