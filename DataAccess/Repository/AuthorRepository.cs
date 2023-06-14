using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(EBookStoreDBContext context)
            : base(context) { }

        public IQueryable<Author> GetAuthors()
        {
            return _dbContext.Authors.AsNoTracking();
        }

        public IQueryable<Author> GetAuthorById(int id)
        {
            return _dbContext.Authors.Where(au => au.Id == id).AsNoTracking();
        }

        public async Task<Author> Create(Author author)
        {
            await _dbContext.AddAsync(author);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(author).State = EntityState.Detached;
            return author;
        }

        public async Task<Author?> Update(int key, Delta<Author> author)
        {
            var entity = await _dbContext.Authors.FindAsync(key);
            if (entity == null) return null;

            author.Patch(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<int> Delete(int key)
        {
            var entity = await _dbContext.Authors.FindAsync(key);
            if (entity == null) return 0;

            _dbContext.Authors.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
