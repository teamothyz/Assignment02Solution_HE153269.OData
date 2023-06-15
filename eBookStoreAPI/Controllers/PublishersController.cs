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
    public class PublishersController : ODataController
    {
        protected readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize]
        public IQueryable<Publisher> Get()
        {
            return _publisherRepository.GetPublishers();
        }

        [EnableQuery]
        [Authorize]
        public SingleResult<Publisher> Get(int key)
        {
            IQueryable<Publisher> result = _publisherRepository.GetPublisherById(key);
            return SingleResult.Create(result);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] Publisher publisher)
        {
            publisher = await _publisherRepository.Create(publisher);
            return Created(publisher);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Patch(int key, Delta<Publisher> publisher)
        {
            var entity = await _publisherRepository.Update(key, publisher);
            if (entity == null) return NotFound();
            return Updated(entity);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int key)
        {
            var count = await _publisherRepository.Delete(key);
            if (count == 0) return NotFound();
            return NoContent();
        }
    }
}
