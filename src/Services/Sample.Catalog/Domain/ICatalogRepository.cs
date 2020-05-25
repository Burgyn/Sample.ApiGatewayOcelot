using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Catalog.Domain
{
    public interface ICatalogRepository
    {
        IEnumerable<Product> GetAll();

        Product Get(int id);

        Task CreateAsync(Product user);

        Task UpdateAsync(Product user);

        Task DeleteAsync(int id);
    }
}
