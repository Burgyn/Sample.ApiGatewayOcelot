using Microsoft.AspNetCore.Mvc;
using Sample.Users.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repository;

        public UsersController(IUsersRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        [HttpGet]
        public IEnumerable<User> Get() => _repository.GetAll();

        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public User Get(int id) => _repository.Get(id);

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            await _repository.CreateAsync(user);

            return Created(string.Empty, new { user.Id });
        }

        /// <summary>
        /// Update the specified user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, User user)
        {
            user.Id = id;

            await _repository.UpdateAsync(user);

            return Ok();
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
