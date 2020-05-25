using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Ordering.Domain
{
    /// <summary>
    /// User
    /// </summary>
    public class Order
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }

        public decimal TotalPrice => Items.Sum(p => p.TotalPrice);

        public IEnumerable<OrderItem> Items { get; set; }
    }
}
