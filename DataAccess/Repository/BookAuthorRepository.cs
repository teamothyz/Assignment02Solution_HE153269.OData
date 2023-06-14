using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Deltas;

namespace DataAccess.Repository
{
    public class BookAuthorRepository : BaseRepository, IBookAuthorRepository
    {
        public BookAuthorRepository(EBookStoreDBContext context)
            : base(context) { }

        public IQueryable<BookAuthor> GetBookAuthors()
        {
            return _dbContext.BookAuthors.AsNoTracking();
        }

        public IQueryable<BookAuthor> GetBookAuthorById(int id)
        {
            return _dbContext.BookAuthors.Where(ba => ba.Id == id).AsNoTracking();
        }

        public async Task<BookAuthor> Create(BookAuthor bookAuthor)
        {
            await _dbContext.BookAuthors.AddAsync(bookAuthor);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(bookAuthor).State = EntityState.Detached;
            return bookAuthor;
        }

        public async Task<BookAuthor?> Update(int id, Delta<BookAuthor> bookAuthor)
        {
            var entity = await _dbContext.BookAuthors.SingleOrDefaultAsync(ba => ba.Id == id);
            if (entity == null) return null;

            bookAuthor.Patch(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<int> Delete(int id)
        {
            var entity = await _dbContext.BookAuthors.FindAsync(id);
            if (entity == null) return 0;

            _dbContext.BookAuthors.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
