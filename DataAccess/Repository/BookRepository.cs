using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Deltas;

namespace DataAccess.Repository
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(EBookStoreDBContext context)
            : base(context) { }

        public IQueryable<Book> GetBooks()
        {
            return _dbContext.Books.AsNoTracking();
        }

        public IQueryable<Book> GetBookById(int id)
        {
            return _dbContext.Books.Where(book => book.Id == id).AsNoTracking();
        }

        public async Task<Book> Create(Book entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<Book?> Update(int key, Delta<Book> book)
        {
            var entity = await _dbContext.Books.FindAsync(key);
            if (entity == null) return null;

            book.Patch(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<int> Delete(int key)
        {
            var entity = await _dbContext.Books.FindAsync(key);
            if (entity == null) return 0;

            _dbContext.Books.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
