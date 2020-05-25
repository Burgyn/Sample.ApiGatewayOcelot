using Microsoft.AspNetCore.Mvc;
using Sample.Ordering.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Ordering.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{buyerId}")]
        public IEnumerable<Domain.Order> Get(int buyerId) => _repository.GetAll(buyerId);

        [HttpGet("{buyerId}/{id}")]
        public Domain.Order Get(int buyerId, int id) => _repository.Get(buyerId);

        [HttpPost("{buyerId}")]
        public async Task<ActionResult> Create(int buyerId, Domain.Order order)
        {
            await _repository.CreateAsync(order);

            return Created(string.Empty, new { order.Id });
        }
    }
}