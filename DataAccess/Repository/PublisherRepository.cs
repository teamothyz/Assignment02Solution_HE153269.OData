using BusinessObject.Common;
using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class PublisherRepository : BaseRepository, IPublisherRepository
    {
        public PublisherRepository(EBookStoreDBContext context)
            : base(context) { }

        public async Task<PagingModel<Publisher>> GetPublishers(string? name = null, 
            string? city = null,
            int pageIndex = 1, 
            int pageSize = 10)
        {
            var query = from pub in _dbContext.Publishers select pub;
            if (name != null)
            {
                query = from pub in query
                        where pub.Name.ToLower().Contains(name.ToLower())
                        select pub;
            }

            if (city != null)
            {
                query = from pub in query
                        where pub.City.ToLower().Contains(city.ToLower())
                        select pub;
            }

            var items = await query.AsNoTracking()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageIndex)
                .ToListAsync();
            var total = await query.CountAsync();
            return new PagingModel<Publisher>
            {
                Items = items,
                TotalPages = (int)Math.Ceiling((double)total / pageSize)
            };
        }

        public async Task<Publisher?> GetPublisherById(int id)
        {
            return await _dbContext.Publishers.Where(pub => pub.Id == id)
                .AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<Publisher> Create(Publisher entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<int> Update(Publisher entity)
        {
            return await _dbContext.Publishers.Where(pub => pub.Id == entity.Id)
                .ExecuteUpdateAsync(pubs => pubs
                .SetProperty(pub => pub.Name, pub => entity.Name)
                .SetProperty(pub => pub.State, pub => entity.State)
                .SetProperty(pub => pub.City, pub => entity.City)
                .SetProperty(pub => pub.Country, pub => entity.Country));
        }

        public async Task<int> Delete(int id)
        {
            return await _dbContext.Publishers.Where(pub => pub.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
