using BusinessObject.Models;

namespace DataAccess.Repository
{
    public class BaseRepository
    {
        protected readonly EBookStoreDBContext _dbContext;

        public BaseRepository(EBookStoreDBContext context) 
        {
            _dbContext = context;
        }

        public async Task<T?> Update<T>(T entity)
        {
            if (entity == null) return default;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
