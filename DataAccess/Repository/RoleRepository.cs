using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(EBookStoreDBContext context)
            : base(context) { }

        public async Task<List<Role>> GetRoles()
        {
            return await _dbContext.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<Role?> GetRoleById(int id)
        {
            return await _dbContext.Roles.AsNoTracking()
                .SingleOrDefaultAsync(role => role.Id == id);
        }

        public async Task<Role> Create(Role role)
        {
            await _dbContext.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(role).State = EntityState.Detached;
            return role;
        }

        public async Task<int> Update(Role entity)
        {
            return await _dbContext.Roles.Where(role => role.Id == entity.Id)
                .ExecuteUpdateAsync(roles => roles.SetProperty(role => role.Description, role => entity.Description));
        }

        public async Task<int> Delete(int id)
        {
            return await _dbContext.Roles.Where(role => role.Id == id).ExecuteDeleteAsync();
        }
    }
}
