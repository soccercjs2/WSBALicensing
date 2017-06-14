using Licensing.Data.Context;
using Licensing.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class OrderWorker
    {
        private LicensingContext _context;

        public OrderWorker(LicensingContext context)
        {
            _context = context;
        }

        public void DeleteOrder(Order order)
        {
            _context.Entry(order).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
