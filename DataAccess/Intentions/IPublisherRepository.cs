using BusinessObject.Models;
using Microsoft.AspNetCore.OData.Deltas;

namespace DataAccess.Intentions
{
    public interface IPublisherRepository
    {
        public IQueryable<Publisher> GetPublishers();

        public IQueryable<Publisher> GetPublisherById(int id);

        public Task<Publisher> Create(Publisher entity);

        public Task<Publisher?> Update(int key, Delta<Publisher> publisher);

        public Task<int> Delete(int id);
    }
}
