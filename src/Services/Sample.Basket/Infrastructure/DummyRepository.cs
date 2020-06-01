using Sample.Basket.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sample.Basket.Infrastructure
{
    public class DummyRepository : IBasketRepository
    {
        private Dictionary<int, Domain.Basket> _baskets;

        public DummyRepository()
        {
            _baskets = JsonSerializer
                .Deserialize<IEnumerable<Domain.Basket>>(Properties.Resources.Baskets)
                .ToDictionary(p => p.BuyerId, p => p);
        }

        public Task CreateAsync(Domain.Basket basket)
        {
            _baskets.Add(basket.BuyerId, basket);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _baskets.Remove(id);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Domain.Basket basket)
        {
            _baskets[basket.BuyerId] = basket;

            return Task.CompletedTask;
        }

        public Domain.Basket Get(int id) => _baskets[id];

        public IEnumerable<Domain.Basket> GetAll()
            => _baskets.Values;
    }
}
