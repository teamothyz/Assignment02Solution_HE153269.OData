using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataAccess.Repository
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(EBookStoreDBContext context)
            : base(context) { }

        public IQueryable<Role> GetRoles()
        {
            return _dbContext.Roles.AsNoTracking();
        }

        public IQueryable<Role> GetRoleById(int id)
        {
            return _dbContext.Roles.AsNoTracking()
                .Where(role => role.Id == id);
        }

        public async Task<Role> Create(Role role)
        {
            await _dbContext.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(role).State = EntityState.Detached;
            return role;
        }

        public async Task<Role?> Update(int key, Delta<Role> role)
        {
            var entity = await _dbContext.Roles.FindAsync(key);
            if (entity == null) return null;

            role.Patch(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<int> Delete(int id)
        {
            var entity = await _dbContext.Roles.FindAsync(id);
            if (entity == null) return 0;

            _dbContext.Roles.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
