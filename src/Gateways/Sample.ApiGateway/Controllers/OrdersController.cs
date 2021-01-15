using System.Threading.Tasks;
using Kros.AspNetCore.ServiceDiscovery;
using Microsoft.AspNetCore.Mvc;
using Flurl;
using Flurl.Http;

namespace Sample.ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IServiceDiscoveryProvider _serviceDiscoveryProvider;

        public OrdersController(IServiceDiscoveryProvider serviceDiscoveryProvider)
        {
            _serviceDiscoveryProvider = serviceDiscoveryProvider;
        }

        [HttpGet("draft/{buyerId}")]
        public async Task<ActionResult> CreateOrderDraft(int buyerId)
        {
            var basketUri = _serviceDiscoveryProvider.GetService("basket");
            var orderingUri = _serviceDiscoveryProvider.GetService("ordering");

            var basket = await basketUri
                   .AppendPathSegment("api/basket")
                   .AppendPathSegment(buyerId)
                   .GetJsonAsync();

            var order = await orderingUri
                .AppendPathSegment("api/orders")
                .AppendPathSegment(buyerId)
                .PostJsonAsync((object)basket)
                .ReceiveJson();

            return Ok(new { order.id });
        }
    }
}
