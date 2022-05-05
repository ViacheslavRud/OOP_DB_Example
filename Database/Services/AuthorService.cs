using System.Collections.Generic;
using System.Linq;
using Database.Database;

namespace Database.Services
{
    public interface IAuthorService
    {
        List<Author> GetAllAuthors();
    }

    public class AuthorService : IAuthorService
    {
        private readonly LibraryContext _libraryContext;

        public AuthorService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        
        public List<Author> GetAllAuthors()
        {
            return _libraryContext.Authors.ToList();
        }
    }
}