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
        private Dictionary<int, Domain.Order> _orders;

        public DummyRepository()
        {
            _orders = JsonSerializer
                .Deserialize<IEnumerable<Domain.Order>>(Properties.Resources.Orders)
                .ToDictionary(p => p.Id, p => p);
        }

        public Task CreateAsync(Domain.Order order)
        {
            var id = _orders.Keys.Max() + 1;
            order.Id = id;
            _orders.Add(order.Id, order);

            return Task.CompletedTask;
        }

        public Domain.Order Get(int id) => _orders[id];

        public IEnumerable<Domain.Order> GetAll(int buyerId)
            => _orders.Values.Where(p=> p.BuyerId == buyerId);
    }
}
