using Microsoft.EntityFrameworkCore;

namespace Database.Database
{
    public class LibraryContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; }

        private readonly string _fileName;

        public LibraryContext(string fileName)
        {
            _fileName = fileName;
            
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=" + _fileName);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(a => a.BookId);
            modelBuilder.Entity<Author>().HasKey(s => s.AuthorId);
            
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new {ba.BookId, ba.AuthorId});
            
            modelBuilder.Entity<BookAuthor>()
                .HasOne<Book>()
                .WithMany()
                .HasForeignKey(ba=> ba.BookId);
            
            modelBuilder.Entity<BookAuthor>()
                .HasOne<Author>()
                .WithMany()
                .HasForeignKey(ba => ba.AuthorId);
            
            
            modelBuilder.Entity<City>().HasKey(s => s.CityId);
            modelBuilder.Entity<Country>().HasKey(s => s.CountryId);
            
            modelBuilder.Entity<Country>()
                .HasOne<City>()
                .WithOne()
                .HasForeignKey<City>(c => c.CityId);

            modelBuilder.Entity<User>().HasKey(s => s.Id);
        }
    }
}