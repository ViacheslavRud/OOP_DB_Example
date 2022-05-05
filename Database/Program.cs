using System;
using System.Collections.Generic;
using System.Linq;
using Database.Database;
using Database.Services;

namespace Database
{
    public class Controller
    {
        private readonly IUserService _userService;
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public Controller(IUserService userService, IBookService bookService, IAuthorService authorService)
        {
            _userService = userService;
            _bookService = bookService;
            _authorService = authorService;
        }

        public void RunMainLoop()
        {
            // TODO VALIDATE ALL INPUT!!!!

            User user = null;
            while (user == null)
            {
                Console.WriteLine("Do you want to login or sign up? (type login/signup)");
                string userInput = Console.ReadLine()!.ToLower();
                string username;

                switch (userInput)
                {
                    case "login":

                        Console.WriteLine("Enter your username:");
                        username = Console.ReadLine();
                        user = _userService.Login(username);
                        if (user == null)
                        {
                            Console.WriteLine("Username not found. Please try again.");
                            continue;
                        }

                        Console.WriteLine("You successfully logged in!");

                        break;
                    case "signup":
                        while (user == null)
                        {
                            Console.WriteLine("Enter your username:");
                            username = Console.ReadLine();
                            Console.WriteLine("Do you want be admin? (type yes/no)");
                            bool admin = bool.Parse(Console.ReadLine());

                            try
                            {
                                user = _userService.SignUp(username, admin ? Role.Admin : Role.User);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                user = null;
                            }
                        }

                        Console.WriteLine("You successfully logged in!");
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }

            while (true)
            {
                Console.WriteLine("What you want to do? Put a number" +
                                  "\n 1. Add author" +
                                  "\n 2. Add book" +
                                  "\n 3. Show all authors" +
                                  "\n 4. Show all books" +
                                  "\n 0. Quit");

                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter name of author");
                        string name = Console.ReadLine();
                        int id = _bookService.AddAuthor(name);
                        Console.WriteLine("Author id is " + id);
                        break;
                    case 2:
                        Console.WriteLine("Enter name of book");
                        string title = Console.ReadLine();
                        Console.WriteLine("Enter AuthorId of the book");
                        int authorId = int.Parse(Console.ReadLine());

                        _bookService.AddBook(title, new[] {authorId, 2, 3});

                        // Console.WriteLine("Book id is " + entry.Entity.BookId);
                        break;
                    case 3:
                        var authors = _authorService.GetAllAuthors();
                        foreach (var author in authors)
                        {
                            Console.WriteLine(author.AuthorId + " " + author.Name);
                        }

                        break;
                    case 4:
                        var booksDetails = _bookService.GetALLBookDetails();
                        foreach (var booksDetail in booksDetails)
                        {
                            Console.WriteLine(booksDetail.BookId + " " + booksDetail.Titile + " " +
                                              string.Join(", ", booksDetail.Authors));
                        }

                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Wrong number");
                        break;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string dbName = "TestDatabase.db";

            using var dbContext = new LibraryContext(dbName);
            IUserService userService = new UserService(dbContext);
            IBookService bookService = new BookService(dbContext);
            IAuthorService authorService = new AuthorService(dbContext);

            var controller = new Controller(userService, bookService, authorService);
            
            controller.RunMainLoop();
        }
    }
}