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
        private Dictionary<int, Product> _products;

        public DummyRepository()
        {
            _products = JsonSerializer
                .Deserialize<IEnumerable<Product>>(Properties.Resources.Catalog)
                .ToDictionary(p => p.Id, p => p);
        }

        public Task CreateAsync(Product product)
        {
            var id = _products.Keys.Max() + 1;
            product.Id = id;

            _products.Add(id, product);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _products.Remove(id);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Product product)
        {
            _products[product.Id] = product;

            return Task.CompletedTask;
        }

        public Product Get(int id) => _products[id];

        public IEnumerable<Product> GetAll()
            => _products.Values;
    }
}
