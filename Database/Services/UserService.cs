using System;
using System.Linq;
using Database.Database;

namespace Database.Services
{
    /// <summary>
    /// Authentication is really stupid, don't do it like this!
    /// </summary>
    public interface IUserService
    {
        User Login(string username);
        User SignUp(string username, Role role = Role.User);
    }

    public class UserService : IUserService
    {
        private readonly LibraryContext _libraryContext;

        public UserService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public User Login(string username)
        {
            return _libraryContext.Users.FirstOrDefault(s => s.Username == username);
        }

        public User SignUp(string username, Role role = Role.User)
        {
            var user = _libraryContext.Users.FirstOrDefault(s => s.Username == username);
            if (user != null)
                throw new Exception("User already exists");
            user = new User {Username = username, Role = role};
            _libraryContext.Users.Add(user);
            _libraryContext.SaveChanges();
            return user;
        }
    }
}