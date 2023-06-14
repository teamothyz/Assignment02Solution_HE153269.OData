using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreAPI.Controllers
{
    public class BooksController : ODataController
    {
        protected readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Book> Get()
        {
            return _bookRepository.GetBooks();
        }

        [EnableQuery]
        public SingleResult<Book> Get(int key)
        {
            IQueryable<Book> result = _bookRepository.GetBookById(key);
            return SingleResult.Create(result);
        }

        public async Task<IActionResult> Post(Book book)
        {
            book = await _bookRepository.Create(book);
            return Created(book);
        }

        public async Task<IActionResult> Patch(int key, Delta<Book> book)
        {
            var entity = await _bookRepository.Update(key, book);
            if (entity == null) return NotFound();
            return Updated(entity);
        } 

        public async Task<IActionResult> Delete(int key)
        {
            var count = await _bookRepository.Delete(key);
            if (count == 0) return NotFound();
            return NoContent();
        }
    }
}
