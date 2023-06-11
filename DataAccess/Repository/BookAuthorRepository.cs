using BusinessObject.Models;
using DataAccess.Intentions;

namespace DataAccess.Repository
{
    public class BookAuthorRepository : BaseRepository, IBookAuthorRepository
    {
        public BookAuthorRepository(EBookStoreDBContext context)
            : base(context) { }
    }
}
