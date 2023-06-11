using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models
{
    public class EBookStoreDBContext : DbContext
    {
        public EBookStoreDBContext() { }

        public EBookStoreDBContext(DbContextOptions<EBookStoreDBContext> options)
            : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                builder.UseSqlServer(config.GetConnectionString("EBookStoreDB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BookAuthor>().HasKey(table => new { table.AuthorId, table.BookId });
        }
    }
}
