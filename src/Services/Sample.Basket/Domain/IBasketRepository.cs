using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Basket.Domain
{
    public interface IBasketRepository
    {
        IEnumerable<Basket> GetAll();

        Basket Get(int id);

        Task CreateAsync(Basket user);

        Task UpdateAsync(Basket user);

        Task DeleteAsync(int id);
    }
}
