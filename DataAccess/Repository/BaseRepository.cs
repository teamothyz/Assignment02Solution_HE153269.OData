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
    }
}
