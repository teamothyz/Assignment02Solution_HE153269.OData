using DataAccess.Intentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Security.Claims;
using User = BusinessObject.Models.User;

namespace eBookStoreAPI.Controllers
{
    public class UsersController : ODataController
    {
        protected readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "admin")]
        public IQueryable<User> Get()
        {
            return _userRepository.GetUsers();
        }

        [EnableQuery]
        [Authorize(Roles = "admin")]
        public SingleResult<User> Get(int key)
        {
            IQueryable<User> result = _userRepository.GetUserById(key);
            return SingleResult.Create(result);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            user = await _userRepository.Create(user);
            return Created(user);
        }

        [Authorize]
        public async Task<IActionResult> Patch(Delta<User> user)
        {
            var key = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var entity = await _userRepository.Update(key, user);
            if (entity == null) return NotFound();
            return Updated(entity);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Patch(int key, Delta<User> user)
        {
            var entity = await _userRepository.Update(key, user);
            if (entity == null) return NotFound();
            return Updated(entity);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int key)
        {
            var count = await _userRepository.Delete(key);
            if (count == 0) return NotFound();
            return NoContent();
        }
    }
}
