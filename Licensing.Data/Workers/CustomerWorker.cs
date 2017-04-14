using Licensing.Data.Context;
using Licensing.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class CustomerWorker
    {
        private LicensingContext _context;

        public CustomerWorker(LicensingContext context)
        {
            _context = context;
        }

        public Customer GetCustomer(string barNumber)
        {
            return _context.Customers.Where(c => c.BarNumber == barNumber).FirstOrDefault();
        }

        public void SetCustomer(Customer customer)
        {
            _context.Entry(customer).State = customer.CustomerId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
