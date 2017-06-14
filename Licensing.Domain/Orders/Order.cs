using Licensing.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Orders
{
    public class Order
    {
        public int OrderId { get; set; }
        public int AmsOrderNumber { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
