using BusinessObject.Common;
using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class BookRepository : BaseRepository, IBookAuthorRepository
    {
        public BookRepository(EBookStoreDBContext context)
            : base(context) { }

        public async Task<PagingModel<Book>> GetBooks(string? title = null,
            string? authorName = null,
            string? publisherName = null,
            bool? ascByPrice = null,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var query = from book in _dbContext.Books
                        join bookAuthor in _dbContext.BookAuthors
                        on book.Id equals bookAuthor.BookId

                        join author in _dbContext.Authors
                        on bookAuthor.AuthorId equals author.Id

                        join publisher in _dbContext.Publishers
                        on book.PublisherId equals publisher.Id

                        select new { book, author = author.FirstName + " " + author.LastName, publisher = publisher.Name };

            if (title != null)
            {
                query = from entity in query
                        where entity.book.Title.ToLower().Contains(title.ToLower())
                        select entity;
            }

            if (publisherName != null)
            {
                query = from entity in query
                        where entity.publisher.ToLower().Contains(publisherName.ToLower())
                        select entity;
            }

            if (authorName != null)
            {
                query = from entity in query
                        where entity.author.ToLower().Contains(authorName.ToLower())
                        select entity;
            }

            if (ascByPrice == true)
            {
                query = from entity in query orderby entity.book.Price ascending select entity;
            }
            else if (ascByPrice == false)
            {
                query = from entity in query orderby entity.book.Price descending select entity;
            }

            var total = await query.CountAsync();
            var items = await query.AsNoTracking()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(entity => entity.book)
                .Include(book => book.Publisher)
                .ToListAsync();

            return new PagingModel<Book>
            {
                Items = items,
                TotalPages = (int)Math.Ceiling((double)total / pageSize)
            };
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _dbContext.Books.Where(book => book.Id == id)
                .AsNoTracking()
                .Include(book => book.Publisher)
                .SingleOrDefaultAsync();
        }

        public async Task<Book> Create(Book entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Books
                .Where(book => book.Id == entity.Id)
                .AsNoTracking().FirstAsync();
        }
    }
}
