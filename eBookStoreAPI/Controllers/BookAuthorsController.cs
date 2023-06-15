using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.Authorization;

namespace eBookStoreAPI.Controllers
{
    public class BookAuthorsController : ODataController
    {
        protected readonly IBookAuthorRepository _bookAuthorRepository;

        public BookAuthorsController(IBookAuthorRepository bookAuthorRepository)
        {
            _bookAuthorRepository = bookAuthorRepository;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize]
        public IQueryable<BookAuthor> Get()
        {
            return _bookAuthorRepository.GetBookAuthors();
        }

        [EnableQuery]
        [Authorize]
        public SingleResult<BookAuthor> Get(int key)
        {
            IQueryable<BookAuthor> result = _bookAuthorRepository.GetBookAuthorById(key);
            return SingleResult.Create(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] BookAuthor bookAuthor)
        {
            bookAuthor = await _bookAuthorRepository.Create(bookAuthor);
            return Created(bookAuthor);
        }

        [HttpPatch]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Patch(int key, Delta<BookAuthor> bookAuthor)
        {
            var entity = await _bookAuthorRepository.Update(key, bookAuthor);
            if (entity == null) return NotFound();
            return Updated(entity);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int key)
        {
            var count = await _bookAuthorRepository.Delete(key);
            if (count == 0) return NotFound();
            return NoContent();
        }
    }
}
