using BusinessObject.Models;

namespace DataAccess.Intentions
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetRoles();

        public Task<Role?> GetRoleById(int id);

        public Task<Role> Create(Role role);

        public Task<int> Update(Role entity);

        public Task<int> Delete(int id);
    }
}
