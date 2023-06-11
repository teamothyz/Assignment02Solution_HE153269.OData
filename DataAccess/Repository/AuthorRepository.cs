using BusinessObject.Models;
using DataAccess.Intentions;

namespace DataAccess.Repository
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(EBookStoreDBContext context)
            : base(context) { }
    }
}
