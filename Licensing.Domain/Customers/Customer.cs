using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Licensing.Domain.Licenses;
using Licensing.Domain.ContactInformation;

namespace Licensing.Domain.Customers
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public int BarNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<License> Licenses { get; set; }
    }
}
