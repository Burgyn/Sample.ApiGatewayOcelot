using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Sample.ApiGateway.Aggregators
{
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
}
