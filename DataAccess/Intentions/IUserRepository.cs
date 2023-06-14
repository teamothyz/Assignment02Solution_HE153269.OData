using BusinessObject.Models;
using Microsoft.AspNetCore.OData.Deltas;

namespace DataAccess.Intentions
{
    public interface IUserRepository
    {
        public Task<User?> Login(string email, string password);

        public IQueryable<User> GetUsers();

        public IQueryable<User> GetUserById(int id);

        public Task<User> Create(User entity);

        public Task<User?> Update(int key, Delta<User> user);

        public Task<int> Delete(int id);
    }
}
