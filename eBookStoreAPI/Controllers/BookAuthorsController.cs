using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Deltas;

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
        public IQueryable<BookAuthor> Get()
        {
            return _bookAuthorRepository.GetBookAuthors();
        }

        [EnableQuery]
        public SingleResult<BookAuthor> Get(int key)
        {
            IQueryable<BookAuthor> result = _bookAuthorRepository.GetBookAuthorById(key);
            return SingleResult.Create(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BookAuthor bookAuthor)
        {
            bookAuthor = await _bookAuthorRepository.Create(bookAuthor);
            return Created(bookAuthor);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int key, Delta<BookAuthor> bookAuthor)
        {
            var entity = await _bookAuthorRepository.Update(key, bookAuthor);
            if (entity == null) return NotFound();
            return Updated(entity);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int key)
        {
            var count = await _bookAuthorRepository.Delete(key);
            if (count == 0) return NotFound();
            return NoContent();
        }
    }
}
