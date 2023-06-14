using BusinessObject.Models;
using Microsoft.AspNetCore.OData.Deltas;

namespace DataAccess.Intentions
{
    public interface IRoleRepository
    {
        public IQueryable<Role> GetRoles();

        public IQueryable<Role> GetRoleById(int id);

        public Task<Role> Create(Role role);

        public Task<Role?> Update(int key, Delta<Role> role);

        public Task<int> Delete(int id);
    }
}
