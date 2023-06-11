using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(EBookStoreDBContext context)
            : base(context) { }

        public async Task<User?> Login(string email, string password)
        {
            return await _dbContext.Users.AsNoTracking()
                .Where(user => user.Email.ToLower() == email.ToLower()
                && user.Password == password)
                .Include(user => user.Role).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _dbContext.Users.AsNoTracking()
                .Where(user => user.Id == id)
                .Include(user => user.Role).FirstOrDefaultAsync();
        }

        public async Task<User> Create(User entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Users.AsNoTracking()
                .Where(user => user.Id == entity.Id)
                .Include(user => user.Role).FirstAsync();
        }

        public async Task<int> Update(User entity)
        {
            return await _dbContext.Users.Where(user => user.Id == user.Id)
                 .ExecuteUpdateAsync(users => users
                 .SetProperty(user => user.Source, user => entity.Source)
                 .SetProperty(user => user.FirstName, user => entity.FirstName)
                 .SetProperty(user => user.MiddleName, user => entity.MiddleName)
                 .SetProperty(user => user.LastName, user => entity.LastName));
        }

        public async Task<int> Delete(int id)
        {
            return await _dbContext.Users.Where(user => user.Id == id)
                 .ExecuteDeleteAsync();
        }
    }
}
