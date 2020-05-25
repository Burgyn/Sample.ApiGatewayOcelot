using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Ordering.Domain
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll(int buyerId);

        Order Get(int id);

        Task CreateAsync(Order user);
    }
}
