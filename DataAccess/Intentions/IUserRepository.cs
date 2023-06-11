using BusinessObject.Models;

namespace DataAccess.Intentions
{
    public interface IUserRepository
    {
        public Task<User?> Login(string email, string password);

        public Task<User?> GetUserById(int id);

        public Task<User> Create(User user);

        public Task<int> Update(User entity);

        public Task<int> Delete(int id);
    }
}
