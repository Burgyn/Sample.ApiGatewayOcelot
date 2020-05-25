using Microsoft.AspNetCore.Mvc;
using Sample.Catalog.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Catalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _repository;

        public CatalogController(ICatalogRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        [HttpGet]
        public IEnumerable<Product> Get() => _repository.GetAll();

        /// <summary>
        /// Gets the product by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Product Get(int id) => _repository.Get(id);

        /// <summary>
        /// Creates the specified product.
        /// </summary>
        /// <param name="product">The user.</param>
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            await _repository.CreateAsync(product);

            return Created(string.Empty, new { product.Id });
        }

        /// <summary>
        /// Update the specified product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="product">The product.</param>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Product product)
        {
            product.Id = id;

            await _repository.UpdateAsync(product);

            return Ok();
        }

        /// <summary>
        /// Deletes the specified product.
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
