using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreAPI.Controllers
{
    public class AuthorsController : ODataController
    {
        protected readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Author> Get()
        {
            return _authorRepository.GetAuthors();
        }

        [EnableQuery]
        public SingleResult<Author> Get(int key)
        {
            IQueryable<Author> result = _authorRepository.GetAuthorById(key);
            return SingleResult.Create(result);
        }

        public async Task<IActionResult> Post(Author book)
        {
            book = await _authorRepository.Create(book);
            return Created(book);
        }

        public async Task<IActionResult> Patch(int key, Delta<Author> author)
        {
            var entity = await _authorRepository.Update(key, author);
            if (entity == null) return NotFound();
            return Updated(entity);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var count = await _authorRepository.Delete(key);
            if (count == 0) return NotFound();
            return NoContent();
        }
    }
}
