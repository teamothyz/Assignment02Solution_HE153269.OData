using BusinessObject.Models;
using Microsoft.AspNetCore.OData.Deltas;

namespace DataAccess.Intentions
{
    public interface IBookRepository
    {
        public IQueryable<Book> GetBooks();

        public IQueryable<Book> GetBookById(int id);

        public Task<Book> Create(Book entity);

        public Task<Book?> Update(int key, Delta<Book> book);

        public Task<int> Delete(int key);
    }
}
