using BusinessObject.Models;

namespace DataAccess.Intentions
{
    public interface IPublisherRepository
    {
        public Task<Publisher?> GetPublisherById(int id);

        public Task<Publisher> Create(Publisher entity);

        public Task<int> Update(Publisher entity);

        public Task<int> Delete(int id);
    }
}
