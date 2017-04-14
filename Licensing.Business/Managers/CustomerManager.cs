using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class CustomerManager
    {
        private LicensingContext _context;
        private CustomerWorker _customerWorker;

        public CustomerManager(LicensingContext context)
        {
            _context = context;
            _customerWorker = new CustomerWorker(context);
        }

        public Customer GetCustomer(string barNumber)
        {
            AmsUpdateManager amsUpdateManager = new AmsUpdateManager(_context);

            Customer customer = _customerWorker.GetCustomer(barNumber);

            if (customer == null)
            {
                customer = new Customer();
                customer.BarNumber = barNumber;
            }

            amsUpdateManager.UpdateCustomer(ref customer);
            _customerWorker.SetCustomer(customer);

            return customer;
        }

        public void SetCustomer(Customer customer)
        {
            _customerWorker.SetCustomer(customer);
        }
    }
}
