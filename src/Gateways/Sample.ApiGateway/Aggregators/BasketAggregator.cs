using Microsoft.AspNetCore.Http;
using MMLib.SwaggerForOcelot.Aggregates;
using Newtonsoft.Json.Linq;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using Sample.Basket.Domain;
using Sample.Users.Domain;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Sample.ApiGateway.Aggregators
{
    [AggregateResponse("Basket with buyer and busket items.", typeof(Response))]
    public class BasketAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var user = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var basket = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var basketJson = JObject.Parse(basket);
            basketJson.Add("buyer", JObject.Parse(user));
            basketJson.Remove("buyerId");

            var stringContent = new StringContent(basketJson.ToString())
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(
                stringContent,
                HttpStatusCode.OK,
                new List<KeyValuePair<string, IEnumerable<string>>>(),
                "OK");
        }

    }
    public class Response
    {
        /// <summary>
        /// Gets or sets the buyer.
        /// </summary>
        public User Buyer { get; set; }

        /// <summary>
        /// Gets or sets the total price.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IEnumerable<BasketItem> Items { get; set; }
    }
}
