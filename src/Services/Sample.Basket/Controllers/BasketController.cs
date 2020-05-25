using Microsoft.AspNetCore.Mvc;
using Sample.Basket.Domain;
using System.Threading.Tasks;

namespace Sample.Basket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets the basket by buyerId.
        /// </summary>
        /// <param name="buyerId">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{buyerId}")]
        public Domain.Basket Get(int buyerId) => _repository.Get(buyerId);

        /// <summary>
        /// Creates the user basket.
        /// </summary>
        /// <param name="basket">The basket.</param>
        [HttpPost]
        public async Task<ActionResult> Create(Domain.Basket basket)
        {
            await _repository.CreateAsync(basket);

            return Created(string.Empty, new { });
        }

        /// <summary>
        /// Update the user basket.
        /// </summary>
        /// <param name="buyerId">The user identifier.</param>
        /// <param name="basket">The basket.</param>
        [HttpPut("{buyerId}")]
        public async Task<ActionResult> Update(int buyerId, Domain.Basket basket)
        {
            basket.BuyerId = buyerId;

            await _repository.UpdateAsync(basket);

            return Ok();
        }

        /// <summary>
        /// Deletes the user basket.
        /// </summary>
        /// <param name="buyerId">The identifier.</param>
        [HttpDelete("{buyerId}")]
        public async Task<ActionResult> Delete(int buyerId)
        {
            await _repository.DeleteAsync(buyerId);

            return NoContent();
        }
    }
}
