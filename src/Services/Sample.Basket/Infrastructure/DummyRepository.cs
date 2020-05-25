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
        private Dictionary<int, Domain.Basket> _users;

        public DummyRepository()
        {
            _users = JsonSerializer
                .Deserialize<IEnumerable<Domain.Basket>>(Properties.Resources.Baskets)
                .ToDictionary(p => p.BuyerId, p => p);
        }

        public Task CreateAsync(Domain.Basket user)
        {
            _users.Add(user.BuyerId, user);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _users.Remove(id);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Domain.Basket user)
        {
            _users[user.BuyerId] = user;

            return Task.CompletedTask;
        }

        public Domain.Basket Get(int id) => _users[id];

        public IEnumerable<Domain.Basket> GetAll()
            => _users.Values;
    }
}
