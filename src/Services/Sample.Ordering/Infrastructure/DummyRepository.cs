using Sample.Ordering.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sample.Ordering.Infrastructure
{
    public class DummyRepository : IOrderRepository
    {
        private Dictionary<int, Domain.Order> _users;

        public DummyRepository()
        {
            _users = JsonSerializer
                .Deserialize<IEnumerable<Domain.Order>>(Properties.Resources.Orders)
                .ToDictionary(p => p.Id, p => p);
        }

        public Task CreateAsync(Domain.Order user)
        {
            _users.Add(user.BuyerId, user);

            return Task.CompletedTask;
        }

        public Domain.Order Get(int id) => _users[id];

        public IEnumerable<Domain.Order> GetAll(int buyerId)
            => _users.Values.Where(p=> p.BuyerId == buyerId);
    }
}
