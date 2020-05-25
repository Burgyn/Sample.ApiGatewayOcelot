using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Basket.Domain
{
    /// <summary>
    /// User
    /// </summary>
    public class Basket
    {
        public int BuyerId { get; set; }

        public decimal TotalPrice => Items.Sum(p => p.TotalPrice);

        public IEnumerable<BasketItem> Items { get; set; }
    }
}
