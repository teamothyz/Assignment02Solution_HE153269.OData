using BusinessObject.Models;
using Microsoft.AspNetCore.OData.Deltas;

namespace DataAccess.Intentions
{
    public interface IBookAuthorRepository
    {
        public IQueryable<BookAuthor> GetBookAuthors();

        public IQueryable<BookAuthor> GetBookAuthorById(int id);

        public Task<BookAuthor> Create(BookAuthor bookAuthor);

        public Task<BookAuthor?> Update(int id, Delta<BookAuthor> bookAuthor);

        public Task<int> Delete(int id);
    }
}
