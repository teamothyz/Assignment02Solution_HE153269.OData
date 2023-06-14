using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class PublisherRepository : BaseRepository, IPublisherRepository
    {
        public PublisherRepository(EBookStoreDBContext context)
            : base(context) { }

        public IQueryable<Publisher> GetPublishers()
        {
            return _dbContext.Publishers.AsNoTracking();
        }

        public IQueryable<Publisher> GetPublisherById(int id)
        {
            return _dbContext.Publishers.Where(pub => pub.Id == id).AsNoTracking();
        }

        public async Task<Publisher> Create(Publisher entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<Publisher?> Update(int key, Delta<Publisher> publisher)
        {
            var entity = await _dbContext.Publishers.FindAsync(key);
            if (entity == null) return null;

            publisher.Patch(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<int> Delete(int id)
        {
            var entity = await _dbContext.Publishers.FindAsync(id);
            if (entity == null) return 0;

            _dbContext.Publishers.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
