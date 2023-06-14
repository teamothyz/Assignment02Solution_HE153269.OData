using BusinessObject.Models;
using Microsoft.AspNetCore.OData.Deltas;

namespace DataAccess.Intentions
{
    public interface IAuthorRepository
    {
        public IQueryable<Author> GetAuthors();

        public IQueryable<Author> GetAuthorById(int id);

        public Task<Author> Create(Author author);

        public Task<Author?> Update(int key, Delta<Author> author);

        public Task<int> Delete(int key);
    }
}
