using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.AspNetCore.OData.Deltas;
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

        public IQueryable<User> GetUsers()
        {
            return _dbContext.Users.AsNoTracking();
        }

        public IQueryable<User> GetUserById(int id)
        {
            return _dbContext.Users.AsNoTracking()
                .Where(user => user.Id == id)
                .Include(user => user.Role)
                .Include(user => user.Publisher);
        }

        public async Task<User> Create(User entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<User?> Update(int key, Delta<User> user)
        {
            var entity = await _dbContext.Users.FindAsync(key);
            if (entity == null) return null;

            user.Patch(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<int> Delete(int id)
        {
            var product = await _dbContext.Users.FindAsync(id);
            if (product == null) return 0;

            _dbContext.Users.Remove(product);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
