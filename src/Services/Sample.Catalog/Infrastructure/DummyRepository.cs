using Sample.Catalog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sample.Catalog.Infrastructure
{
    public class DummyRepository : ICatalogRepository
    {
        private Dictionary<int, Product> _users;

        public DummyRepository()
        {
            _users = JsonSerializer
                .Deserialize<IEnumerable<Product>>(Properties.Resources.Catalog)
                .ToDictionary(p => p.Id, p => p);
        }

        public Task CreateAsync(Product user)
        {
            var id = _users.Keys.Max() + 1;
            user.Id = id;

            _users.Add(id, user);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _users.Remove(id);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Product user)
        {
            _users[user.Id] = user;

            return Task.CompletedTask;
        }

        public Product Get(int id) => _users[id];

        public IEnumerable<Product> GetAll()
            => _users.Values;
    }
}
