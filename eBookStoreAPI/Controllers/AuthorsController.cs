using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IQueryable<Author> Get()
        {
            return _authorRepository.GetAuthors();
        }

        [EnableQuery]
        [Authorize]
        public SingleResult<Author> Get(int key)
        {
            IQueryable<Author> result = _authorRepository.GetAuthorById(key);
            return SingleResult.Create(result);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] Author book)
        {
            book = await _authorRepository.Create(book);
            return Created(book);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Patch(int key, Delta<Author> author)
        {
            var entity = await _authorRepository.Update(key, author);
            if (entity == null) return NotFound();
            return Updated(entity);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int key)
        {
            var count = await _authorRepository.Delete(key);
            if (count == 0) return NotFound();
            return NoContent();
        }
    }
}
